using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Test_API
{
    public class HuaweiAPI
    {
        private static string _sessionID = "";
        private static string _token = "";
        private static string _requestToken = "";
        private static string _requestTokenOne = "";
        private static string _requestTokenTwo = "";
        private static string _sessionCookie = "";

        private static string Debug(string text)
        {
            Console.WriteLine("[Debug] " + text);
            return text;
        }


        /// <summary>
        /// Check for login state
        /// </summary>
        /// <param name="ip_address"></param>
        public static void LoginState(string ip_address)
        {
            XmlDocument checkLoginState;
            checkLoginState = GET(ip_address, "api/user/state-login");
            Console.WriteLine(xmlformat.Beautify(checkLoginState));

        }

        /// <summary>
        /// Login into router
        /// </summary>
        /// <param name="ip_address"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        public static void UserLogin(string ip_address, string username, string password)
        {
            string logininfo = "";
            string authinfo = "";


            _sessionID = "";
            _token = "";
            _requestToken = "";
            _requestTokenOne = "";
            _requestTokenTwo = "";
            _sessionCookie = "";

            Initialise(ip_address);

            //Debug("Encrypting authentication info..");
            authinfo = SHA256andB64(username + SHA256andB64(password) + _requestToken);
            logininfo = string.Format("<?xml version=\"1.0\" encoding=\"UTF-8\"?><request><Username>{0}</Username><Password>{1}</Password><password_type>4</password_type>", username, authinfo);

            XmlDocument login;
            //Debug("Running POST request..");
            login = POST(ip_address, logininfo, "api/user/login");
            if (xmlformat.Beautify(login).Contains("OK"))
                Console.WriteLine("Logged in.");
            else
                Console.WriteLine("Operation failed.");


        }

        /// <summary>
        /// Logout router
        /// </summary>
        /// <param name="ip_address"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        public static void UserLogout(string ip_address, string username, string password)
        {
            string data = "<?xml version:\"1.0\" encoding=\"UTF-8\"?><request><Logout>1</Logout></request>";

            XmlDocument logout;
            logout = POST(ip_address, data, "api/user/logout");

            if (xmlformat.Beautify(logout).Contains("OK"))
                Console.WriteLine("Logged out.");
            else
                Console.WriteLine("Operation failed.");
        }

        /// <summary>
        /// Reboot router
        /// </summary>
        /// <param name="ip_address"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        public static void RebootDevice(string ip_address, string username, string password)
        {
            string data = "<?xml version:\"1.0\" encoding=\"UTF-8\"?><request><Control>1</Control></request>";

            XmlDocument reboot;
            reboot = POST(ip_address, data, "api/device/control");

            if (xmlformat.Beautify(reboot).Contains("OK"))
                Console.WriteLine("Device rebooting.");
            else
                Console.WriteLine("Operation failed.");
        }

        /// <summary>
        /// Shutdown router
        /// </summary>
        /// <param name="ip_address"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        public static void ShutdownDevice(string ip_address, string username, string password)
        {
            string data = "<?xml version:\"1.0\" encoding=\"UTF-8\"?><request><Control>4</Control></request>";

            XmlDocument shutdown;
            shutdown = POST(ip_address, data, "api/device/control");

            if (xmlformat.Beautify(shutdown).Contains("OK"))
                Console.WriteLine("Device shutting down.");
            else
                Console.WriteLine("Operation failed.");
        }

        /// <summary>
        /// Check device info
        /// </summary>
        /// <param name="ip_address"></param>
        public static void DeviceInfo(string ip_address)
        {
            XmlDocument DeviceInfo;
            DeviceInfo = GET(ip_address, "api/device/information");

            if (!xmlformat.Beautify(DeviceInfo).Contains("<error>"))
            {
                foreach (XmlNode node in DeviceInfo.DocumentElement)
                {
                    Console.WriteLine(node.Name + " : " + node.InnerText);

                }
            }
            else
            {
                Console.WriteLine("Operation failed.");
            }

        }

        /// <summary>
        /// Check for Public land mobile network info
        /// </summary>
        /// <param name="ip_address"></param>
        public static void PLMNInfo(string ip_address)
        {
            XmlDocument PLMNInfo;
            PLMNInfo = GET(ip_address, "api/net/current-plmn");

            if (!xmlformat.Beautify(PLMNInfo).Contains("<error>"))
            {
                foreach (XmlNode node in PLMNInfo.DocumentElement)
                {
                    Console.WriteLine(node.Name + " : " + node.InnerText);

                }
            }
            else
            {
                Console.WriteLine("Operation failed.");
            }

        }

        /// <summary>
        /// Check sim lock status
        /// </summary>
        /// <param name="ip_address"></param>
        public static void SimLockStatus(string ip_address)
        {
            XmlDocument SimLockStatus;
            SimLockStatus = GET(ip_address, "api/pin/simlock");

            if (!xmlformat.Beautify(SimLockStatus).Contains("<error>"))
            {
                foreach (XmlNode node in SimLockStatus.DocumentElement)
                {
                    Console.WriteLine(node.Name + " : " + node.InnerText);

                }
            }
            else
            {
                Console.WriteLine("Operation failed.");
            }

        }

        /// <summary>
        /// Monitor traffic statistic
        /// </summary>
        /// <param name="ip_address"></param>
        public static void TrafficStatsMonitoring(string ip_address)
        {
            while (true)
            {
                XmlDocument TrafficStatsMonitoring;
                TrafficStatsMonitoring = GET(ip_address, "api/monitoring/traffic-statistics");

                if (!xmlformat.Beautify(TrafficStatsMonitoring).Contains("<error>"))
                {
                    foreach (XmlNode node in TrafficStatsMonitoring.DocumentElement)
                    {
                        Console.WriteLine(node.Name + " : " + node.InnerText);

                    }
                }
                else
                {
                    Console.WriteLine("Operation failed.");
                    break;
                }

                System.Threading.Thread.Sleep(1000);
                Console.Clear();
            }
            

        }

        /// <summary>
        /// Monitor router signal
        /// </summary>
        /// <param name="ip_address"></param>
        public static void SignalMonitoring(string ip_address)
        {
            while (true)
            {
                XmlDocument SignalMonitoring;
                SignalMonitoring = GET(ip_address, "api/device/signal");

                if (!xmlformat.Beautify(SignalMonitoring).Contains("<error>"))
                {
                    foreach (XmlNode node in SignalMonitoring.DocumentElement)
                    {
                        Console.WriteLine(node.Name + " : " + node.InnerText);

                    }
                }
                else
                {
                    Console.WriteLine("Operation failed.");
                    break;
                }

                System.Threading.Thread.Sleep(1000);
                Console.Clear();
            }


        }


        private static XmlDocument POST(string ip_address, string data, string api_type)
        {
            XmlDocument doc = new XmlDocument();
            var wc = NewWebClient();
            try
            {
                var response = wc.UploadData("http://" + ip_address + "/" + api_type, Encoding.UTF8.GetBytes(data));
                var responseString = Encoding.Default.GetString(response);
                HandleHeaders(wc);
                doc.LoadXml(responseString);
            }
            catch
            {


            }

            return doc;
        }

        private static XmlDocument GET(string ip_address, string api_type)
        {
            XmlDocument doc = new XmlDocument();

            var wc = NewWebClient();
            try
            {
                var data = wc.DownloadString("http://" + ip_address + "/" + api_type);
                HandleHeaders(wc);
                doc.LoadXml(data);
            }
            catch (Exception e)
            {

            }

            return doc;
        }



        private static WebClient NewWebClient()
        {
            var wc = new WebClient();
            wc.Headers.Add(HttpRequestHeader.Cookie, _sessionCookie);
            //wc.Headers.Add("Cache-Control", "no-cache");
            wc.Headers.Add("__RequestVerificationToken", _requestToken);
            wc.Headers.Add("Accept", "*/*");
            wc.Headers.Add("User-Agent", "Mozilla/5.0 (Windows; U; Windows NT 5.1; en-US; rv:1.9.2.12) Gecko/20101026 Firefox/3.6.12");
            return wc;
        }

        private static void HandleHeaders(WebClient wc)
        {
            if (!string.IsNullOrEmpty(wc.ResponseHeaders["__RequestVerificationTokenOne"]))
            {
                _requestTokenOne = wc.ResponseHeaders["__RequestVerificationTokenOne"];
            }
            if (!string.IsNullOrEmpty(wc.ResponseHeaders["__RequestVerificationTokenTwo"]))
            {
                _requestTokenTwo = wc.ResponseHeaders["__RequestVerificationTokenTwo"];
            }
            if (!string.IsNullOrEmpty(wc.ResponseHeaders["__RequestVerificationToken"]))
            {
                _requestToken = wc.ResponseHeaders["__RequestVerificationToken"];
            }
            if (!string.IsNullOrEmpty(wc.ResponseHeaders["Set-Cookie"]))
            {
                _sessionCookie = wc.ResponseHeaders["Set-Cookie"];
            }
        }

        private static void Initialise(string ip_address)
        {
            //Debug("Initializing..");

            if (string.IsNullOrEmpty(_sessionCookie) || string.IsNullOrEmpty(_requestToken))
            {
                //Debug("Getting Token..");

                GetTokens(ip_address);
            }
        }

        private static void GetTokens(string ip_address)
        {

            try
            {
                XmlDocument GetTokens_doc = GET(ip_address, "api/webserver/SesTokInfo");
                _sessionID = GetTokens_doc.SelectSingleNode("//response/SesInfo").InnerText;
                _token = GetTokens_doc.SelectSingleNode("//response/TokInfo").InnerText;

                //LogDebug(string.Format("\n[*] New session ID: {0}", _sessionID));
                //LogDebug(string.Format("\n[*] New token ID: {0}", _token));

                _requestToken = _token;
                _sessionCookie = _sessionID;
            }
            catch
            {
            }


        }

        private static string SHA256andB64(string text)
        {
            var hashBytes = System.Text.Encoding.UTF8.GetBytes(SHA256(text));
            return System.Convert.ToBase64String(hashBytes);
        }

        private static string SHA256(string text)
        {
            StringBuilder Sb = new StringBuilder();

            using (SHA256 hash = SHA256Managed.Create())
            {
                Encoding enc = Encoding.UTF8;
                Byte[] result = hash.ComputeHash(enc.GetBytes(text));

                foreach (Byte b in result)
                    Sb.Append(b.ToString("x2"));
            }
            return Sb.ToString();
        }
    }

    public static class xmlformat
    {
        public static string Beautify(this XmlDocument doc)
        {
            StringBuilder sb = new StringBuilder();
            XmlWriterSettings settings = new XmlWriterSettings
            {
                Indent = true,
                IndentChars = "  ",
                NewLineChars = "\r\n",
                NewLineHandling = NewLineHandling.Replace
            };
            using (XmlWriter writer = XmlWriter.Create(sb, settings))
            {
                doc.Save(writer);
            }
            return sb.ToString();
        }
    }
}
