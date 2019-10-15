using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SpeedTest;
using SpeedTest.Models;

namespace Huawei_Router_Tool_GUI
{
    public class SpeedTestClient : ISpeedTestClient
    {
        private const string ConfigUrl = "http://www.speedtest.net/speedtest-config.php";

        private static readonly string[] ServersUrls = new[]
        {
            "http://www.speedtest.net/speedtest-servers-static.php",
            "http://c.speedtest.net/speedtest-servers-static.php",
            "http://www.speedtest.net/speedtest-servers.php",
            "http://c.speedtest.net/speedtest-servers.php"
        };

        //private readonly int[] downloadSizes = { 350, 500, 750, 1000, 1500, 2000, 2500, 3000, 3500, 4000 };
        private readonly int[] downloadSizes = { 3000, 3000, 3000 };
        private const string Chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private const int MaxUploadSize = 10; // 400 KB

        #region ISpeedTestClient

        /// <summary>
        /// Download speedtest.net settings
        /// </summary>
        /// <returns>speedtest.net settings</returns>
        public Settings GetSettings()
        {
            using (var client = new SpeedTestHttpClient())
            {
                var settings = client.GetConfig<Settings>(ConfigUrl).GetAwaiter().GetResult();

                var serversConfig = new ServersList();
                foreach (var serversUrl in ServersUrls)
                {
                    try
                    {
                        serversConfig = client.GetConfig<ServersList>(serversUrl).GetAwaiter().GetResult();
                        if (serversConfig.Servers.Count > 0) break;
                    }
                    catch
                    {
                        //
                    }
                }

                if (serversConfig.Servers.Count <= 0)
                {
                    throw new InvalidOperationException("SpeedTest does not return any server");
                }

                var ignoredIds = settings.ServerConfig.IgnoreIds.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                serversConfig.CalculateDistances(settings.Client.GeoCoordinate);
                settings.Servers = serversConfig.Servers
                    .Where(s => !ignoredIds.Contains(s.Id.ToString()))
                    .OrderBy(s => s.Distance)
                    .ToList();

                return settings;
            }
        }

        /// <summary>
        /// Test latency (ping) to server
        /// </summary>
        /// <returns>Latency in milliseconds (ms)</returns>
        public int TestServerLatency(Server server, int retryCount = 3)
        {
            var latencyUri = CreateTestUrl(server, "latency.txt");
            var timer = new Stopwatch();

            using (var client = new SpeedTestHttpClient())
            {
                for (var i = 0; i < retryCount; i++)
                {
                    string testString;
                    try
                    {
                        timer.Start();
                        testString = client.GetStringAsync(latencyUri).ConfigureAwait(false).GetAwaiter().GetResult();
                    }
                    catch (WebException)
                    {
                        continue;
                    }
                    finally
                    {
                        timer.Stop();
                    }

                    if (!testString.StartsWith("test=test"))
                    {
                        throw new InvalidOperationException("Server returned incorrect test string for latency.txt");
                    }
                }
            }

            return (int)timer.ElapsedMilliseconds / retryCount;
        }

        /// <summary>
        /// Test download speed to server
        /// </summary>
        /// <returns>Download speed in Kbps</returns>
        public double TestDownloadSpeed(Server server, int simultaniousDownloads = 2, int retryCount = 2)
        {
            var testData = GenerateDownloadUrls(server, retryCount);

            return TestSpeed(testData, async (client, url) =>
            {
                var data = await client.GetByteArrayAsync(url).ConfigureAwait(false);
                return data.Length;
            }, simultaniousDownloads);
        }

        /// <summary>
        /// Test upload speed to server
        /// </summary>
        /// <returns>Upload speed in Kbps</returns>
        public double TestUploadSpeed(Server server, int simultaniousUploads = 2, int retryCount = 2)
        {
            var testData = GenerateUploadData(retryCount);
            return TestSpeed(testData, async (client, uploadData) =>
            {
                await client.PostAsync(server.Url, new StringContent(uploadData));
                return uploadData.Length;
            }, simultaniousUploads);
        }

        #endregion

        #region Helpers

        private static double TestSpeed<T>(IEnumerable<T> testData, Func<HttpClient, T, Task<int>> doWork, int concurencyCount = 2)
        {
            var timer = new Stopwatch();
            var throttler = new SemaphoreSlim(concurencyCount);

            timer.Start();
            var downloadTasks = testData.Select(async data =>
            {
                await throttler.WaitAsync().ConfigureAwait(false);
                var client = new SpeedTestHttpClient();
                try
                {
                    var size = await doWork(client, data).ConfigureAwait(false);
                    return size;
                }
                finally
                {
                    client.Dispose();
                    throttler.Release();
                }
            }).ToArray();

            Task.WaitAll(downloadTasks);
            timer.Stop();

            double totalSize = downloadTasks.Sum(task => task.Result);
            return (totalSize * 8 / 1024) / ((double)timer.ElapsedMilliseconds / 1000);
        }

        private static IEnumerable<string> GenerateUploadData(int retryCount)
        {
            var random = new Random();
            var result = new List<string>();

            for (var sizeCounter = 1; sizeCounter < MaxUploadSize + 1; sizeCounter++)
            {
                var size = sizeCounter * 200 * 1024;
                var builder = new StringBuilder(size);

                builder.AppendFormat("content{0}=", sizeCounter);

                for (var i = 0; i < size; ++i)
                    builder.Append(Chars[random.Next(Chars.Length)]);

                for (var i = 0; i < retryCount; i++)
                {
                    result.Add(builder.ToString());
                }
            }

            return result;
        }

        private static string CreateTestUrl(Server server, string file)
        {
            return new Uri(new Uri(server.Url), ".").OriginalString + file;
        }

        private IEnumerable<string> GenerateDownloadUrls(Server server, int retryCount)
        {
            var downloadUriBase = CreateTestUrl(server, "random{0}x{0}.jpg?r={1}");
            foreach (var downloadSize in downloadSizes)
            {
                for (var i = 0; i < retryCount; i++)
                {
                    yield return string.Format(downloadUriBase, downloadSize, i);
                }
            }
        }

        #endregion
    }
}
