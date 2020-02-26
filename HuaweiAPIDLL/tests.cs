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
    public class HuaweiAPIDLL
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


        public static void LoginState(string ip_addr)
        {
            XmlDocument checkLoginState;
            checkLoginState = GET(ip_addr, "api/user/state-login");
            Console.WriteLine(xmlformat.Beautify(checkLoginState));

        }

        public static void UserLogin(string ip_addr, string username, string password)
        {
            string logininfo = "";
            string authinfo = "";


        _sessionID = "";
            _token = "";
            _requestToken = "";
            _requestTokenOne = "";
            _requestTokenTwo = "";
            _sessionCookie = "";

            Initialise(ip_addr);

            //Debug("Encrypting authentication info..");
            authinfo = SHA256andB64(username + SHA256andB64(password) + _requestToken);
            logininfo = string.Format("<?xml version=\"1.0\" encoding=\"UTF-8\"?><request><Username>{0}</Username><Password>{1}</Password><password_type>4</password_type>", username, authinfo);

            XmlDocument login;
            //Debug("Running POST request..");
            login = POST(ip_addr, logininfo, "api/user/login");
            if (xmlformat.Beautify(login).Contains("OK"))
                Console.WriteLine("Logged in.");
            else
                Console.WriteLine("Operation failed.");


        }

        public static void UserLogout(string ip_addr, string username, string password)
        {
            string data = "<?xml version:\"1.0\" encoding=\"UTF-8\"?><request><Logout>1</Logout></request>";

            XmlDocument logout;
            logout = POST(ip_addr, data, "api/user/logout");

            if (xmlformat.Beautify(logout).Contains("OK"))
                Console.WriteLine("Logged out.");
            else
                Console.WriteLine("Operation failed.");
        }

        public static void RebootDevice(string ip_addr, string username, string password)
        {
            string data = "<?xml version:\"1.0\" encoding=\"UTF-8\"?><request><Control>1</Control></request>";

            XmlDocument reboot;
            reboot = POST(ip_addr, data, "api/device/control");

            if (xmlformat.Beautify(reboot).Contains("OK"))
                Console.WriteLine("Device rebooting.");
            else
                Console.WriteLine("Operation failed.");
        }

        public static void ShutdownDevice(string ip_addr, string username, string password)
        {
            string data = "<?xml version:\"1.0\" encoding=\"UTF-8\"?><request><Control>4</Control></request>";

            XmlDocument shutdown;
            shutdown = POST(ip_addr, data, "api/device/control");

            if (xmlformat.Beautify(shutdown).Contains("OK"))
                Console.WriteLine("Device shutting down.");
            else
                Console.WriteLine("Operation failed.");
        }

        private static XmlDocument POST(string ip_addr, string data, string api_type)
        {
            XmlDocument doc = new XmlDocument();
            var wc = NewWebClient();
            try
            {
                var response = wc.UploadData("http://" + ip_addr + "/" + api_type, Encoding.UTF8.GetBytes(data));
                var responseString = Encoding.Default.GetString(response);
                HandleHeaders(wc);
                doc.LoadXml(responseString);
            }
            catch
            {
              

            }

            return doc;
        }

        private static XmlDocument GET(string ip_addr, string api_type)
        {
            XmlDocument doc = new XmlDocument();

            var wc = NewWebClient();
            try
            {
                var data = wc.DownloadString("http://" + ip_addr + "/" + api_type);
                HandleHeaders(wc);
                doc.LoadXml(data);
            }
            catch(Exception e)
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

        private static void Initialise(string ip_addr)
        {
            //Debug("Initializing..");

            if (string.IsNullOrEmpty(_sessionCookie) || string.IsNullOrEmpty(_requestToken))
            {
                //Debug("Getting Token..");

                GetTokens(ip_addr);
            }
        }

        private static void GetTokens(string ip_addr)
        {

            try
            {
                XmlDocument GetTokens_doc = GET(ip_addr, "api/webserver/SesTokInfo");
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
