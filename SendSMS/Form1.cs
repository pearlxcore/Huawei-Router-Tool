using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace SendSMS
{
    public partial class Form1 : Form
    {
        private static string _tokenSMS = "";
        private static string _sessionIDSMS = "";
        private string _requestToken = "";
        private string _sessionCookie = "";
        private string _requestTokenOneSMS = "";
        private string _requestTokenTwoSMS = "";

        public Form1()
        {
            InitializeComponent();
        }

        private void Logger(string text)
        {
            tbInfo.Text += "[INFO] " + text + Environment.NewLine;
        }

        private void InitialiseSMS()
        {
            if (string.IsNullOrEmpty(_sessionCookie) || string.IsNullOrEmpty(_requestToken))
            {
                Logger("Getting token..");
                GetTokenSMS();
            }
        }

        private void GetTokenSMS()
        {
            XmlDocument GetTokens_doc = Get("api/webserver/SesTokInfo");
            _sessionIDSMS = GetTokens_doc.SelectSingleNode("//response/SesInfo").InnerText;
            _tokenSMS = GetTokens_doc.SelectSingleNode("//response/TokInfo").InnerText;

            Logger("_sessionIDSMS : " + _sessionIDSMS.ToString());
            Logger("_tokenSMS : " + _tokenSMS.ToString());

          
        }

        private XmlDocument Get(string path)
        {
            var wc = NewWebClientSMS();

            var data = wc.DownloadString("http://192.168.8.1/" + path);
            //HandleHeaderSMS(wc);
            XmlDocument Get_doc = new XmlDocument();
            Get_doc.LoadXml(data);

            return Get_doc;
        }

        private WebClient NewWebClientSMS()
        {
            var wc = new WebClient();
            wc.Headers.Add(HttpRequestHeader.Cookie, _sessionCookie);
            //wc.Headers.Add("Cache-Control", "no-cache");
            wc.Headers.Add("__RequestVerificationToken", _requestToken);
            wc.Headers.Add("Accept", "*/*");
            wc.Headers.Add("User-Agent", "Mozilla/5.0 (Windows; U; Windows NT 5.1; en-US; rv:1.9.2.12) Gecko/20101026 Firefox/3.6.12");
            return wc;
        }

        private void HandleHeaderSMS(WebClient wc)
        {
            if (!string.IsNullOrEmpty(wc.ResponseHeaders["__RequestVerificationTokenOne"]))
            {
                _requestTokenOneSMS = wc.ResponseHeaders["__RequestVerificationTokenOne"];
                //LogDebug(string.Format("\n[*] Recieved new RVT1: {0}", _requestTokenOne));
            }
            if (!string.IsNullOrEmpty(wc.ResponseHeaders["__RequestVerificationTokenTwo"]))
            {
                _requestTokenTwoSMS = wc.ResponseHeaders["__RequestVerificationTokenTwo"];
                //LogDebug(string.Format("\n[*] Recieved new RVT2: {0}", _requestTokenTwo));
            }
            if (!string.IsNullOrEmpty(wc.ResponseHeaders["__RequestVerificationToken"]))
            {
                _requestToken = wc.ResponseHeaders["__RequestVerificationToken"];
                //LogDebug(string.Format("\n[*] Recieved new RVT: {0}", _requestToken));
            }
            if (!string.IsNullOrEmpty(wc.ResponseHeaders["Set-Cookie"]))
            {
                _sessionCookie = wc.ResponseHeaders["Set-Cookie"];
                //LogDebug(string.Format("\n[*] Recieved new cookie: {0}", _sessionCookie));
            }
        }

        private WebClient NewWebClientPOSTSMS()
        {
            var wc = new WebClient();
            wc.Headers.Add(HttpRequestHeader.Cookie, _sessionIDSMS);
            //wc.Headers.Add("Cache-Control", "no-cache");
            wc.Headers.Add("__RequestVerificationToken", _tokenSMS);
            wc.Headers.Add("Accept", "*/*");
            wc.Headers.Add("User-Agent", "Mozilla/5.0 (Windows; U; Windows NT 5.1; en-US; rv:1.9.2.12) Gecko/20101026 Firefox/3.6.12");
            return wc;
        }

        private XmlDocument PostSMS(string path, string data)
        {
            var wc = NewWebClientPOSTSMS();

            var response = wc.UploadData("http://192.168.8.1/" + path, Encoding.UTF8.GetBytes(data));
            var responseString = Encoding.Default.GetString(response);
            HandleHeaderSMS(wc);
            XmlDocument Post_doc = new XmlDocument();
            Post_doc.LoadXml(responseString);

            return Post_doc;
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            Logger("Starting..");

            Logger("Resetting value..");
            _tokenSMS = "";
            _sessionIDSMS = "";

            Logger("Initializing..");
            InitialiseSMS();

            

            string recipient = "0176612934";
            string content = "TEST";

            Logger("recipient : " + recipient);
            Logger("content : " + content);

            var data = "<?xml version:\"1.0\" encoding=\"UTF-8\"?><request><Index>-1</Index><Phones><Phone>"+ recipient + "</Phone></Phones><Sca></Sca><Content>"+ content + "</Content><Length> " + content.Length.ToString() + "</Length><Reserved>1</Reserved><Date>" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").ToString() + "</Date></request>";

            Logger("Body XML : " + data);
            var POST_doc = PostSMS("api/sms/send-sms", data);
            string compare = POST_doc.OuterXml.ToString();
            Logger(compare);
        }
    }
}
