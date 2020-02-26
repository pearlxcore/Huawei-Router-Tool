using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace Send_SMS
{
    public partial class Form1 : Form
    {
        static string _sessionID = "";
        static string _token = "";
        static string _requestToken = "";
        static string _requestTokenOne = "";
        static string _requestTokenTwo = "";
        static string _sessionCookie = "";
        static string ip_addr = "192.168.8.1";
        static string username = "admin";
        static string password = "playstation4";
        static string recipient = "0176612934";
        static string content = "TEST";
        static string _CurrentSessionID = "";
        static string _CurrentToken = "";

        public Form1()
        {
            InitializeComponent();
        }

        #region Login

        private void Logger(string text)
        {
            tbLog.Text += "[INFO] " + text + Environment.NewLine;
        }

        private void button1_Click(object sender, EventArgs e)
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
                Logger("Logged in.");
            else
                Logger("Operation failed.");
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
            catch (Exception e)
            {

            }

            return doc;
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

        #endregion Login

        #region SMS

        private void btnSendSMS_Click(object sender, EventArgs e)
        {
            //get session id n token
            var Sestoken = GET(ip_addr, "api/webserver/SesTokInfo");
            _CurrentSessionID = Sestoken.SelectSingleNode("//response/SesInfo").InnerText;
            _CurrentToken = Sestoken.SelectSingleNode("//response/TokInfo").InnerText;
            Logger("_CurrentSessionID : " + _CurrentSessionID);
            Logger("_CurrentToken : " + _CurrentToken);

            var data = "<?xml version:\"1.0\" encoding=\"UTF-8\"?><request><Index>-1</Index><Phones><Phone>" + recipient + "</Phone></Phones><Sca></Sca><Content>" + content + "</Content><Length> " + content.Length.ToString() + "</Length><Reserved>1</Reserved><Date>" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").ToString() + "</Date></request>";
            var POST_doc = Post("api/sms/send-sms", data);
            Logger(POST_doc.OuterXml.ToString());


        }

        private XmlDocument Post(string path, string data)
        {
            var wc = PostWebClient();
            var response = wc.UploadData("http://" + ip_addr + "/" + path, Encoding.UTF8.GetBytes(data));
            var responseString = Encoding.Default.GetString(response);
            //PostHandleHeaders(wc);
            XmlDocument Post_doc = new XmlDocument();
            Post_doc.LoadXml(responseString);
            return Post_doc;
        }

        private static WebClient PostWebClient()
        {
            var wc = new WebClient();
            wc.Headers.Add(HttpRequestHeader.Cookie, _CurrentSessionID);
            //wc.Headers.Add("Cache-Control", "no-cache");
            wc.Headers.Add("__RequestVerificationToken", _CurrentToken);
            wc.Headers.Add("Accept", "*/*");
            wc.Headers.Add("User-Agent", "Mozilla/5.0 (Windows; U; Windows NT 5.1; en-US; rv:1.9.2.12) Gecko/20101026 Firefox/3.6.12");
            return wc;
        }



        #endregion SMS


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
