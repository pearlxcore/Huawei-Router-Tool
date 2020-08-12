using ByteSizeLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Net.NetworkInformation;
using System.Threading;
using System.Xml.Linq;
using SpeedTest.Models;
using System.Text.RegularExpressions;
using System.Net.Http;
using MetroFramework.Controls;
using System.Net.Sockets;
using GitHubUpdate;

namespace Huawei_Router_Tool_GUI
{
    public partial class Form1 : MetroFramework.Forms.MetroForm
    {
        List<string> LteBand = new List<string>();
        static int countCheck;
        public static string selected_band, final, string_display;
        TextBox lastSelected;
        string logoutStatus = "";
        string runAPIstatus = "";
        bool runProc = true;
        static string path, band_tool, bat, args, server_detail, logininfo, authinfo;
        public static string Msisdn;
        public bool PrintDebugMessages { get; set; } = true;
        public string BaseAddress { get; set; } = "http://192.168.1.1/";
        //public string SessionToken
        public WebClient _wc = new WebClient();
        public string _requestTokenSMS;
        public string _tokenSMS;
        public string _sessionCookieSMS;
        public string _sessionIDSMS;
        public string _sessionID = "";
        public string _token = "";
        public string _requestTokenOne = "";
        public string _requestTokenTwo = "";
        public string _requestToken = "";
        public string _sessionCookie = "";
        public static SpeedTestClient client;
        public static Settings settings;
        public static readonly object lockObject = new object();
        public static readonly string[] pingHosts = new[] { "1.1.1.1", "8.8.8.8", "1.0.0.1", "8.8.4.4" };
        public XmlDocument doc1;
        public XmlDocument login;
        public XmlDocument doc1_API;
        public XmlDocument doc2;
        public static string SeriesRSRQ;
        public static string SeriesRSRP;
        public static string SeriesRSSI;
        public static string Seriessinr;
        public static string RSRQ, RSRP, RSSI, sinr, nei_cellid, txpower, ip_webpage, cell_id, ulbandwidth, dlbandwidth, earfcn, mcc, mnc, DownloadRate, UploadRate, DETECT_ERROR_CODE;
        public string copyNumber;
        public int USSDresult;
        public string outstring;
        public bool loginSuccess = true;
        public static string _CurrentSessionID;
        public static string _CurrentToken;
        private string BandQuickSwitch = "";
        private bool isConnected = true;
        private bool restartApplication = false;

        public enum Band
        {

            B_0000000000000001 = 1,
            B_0000000000000002 = 2,
            B_0000000000000004 = 3,
            B_0000000000000008 = 4,
            B_0000000000000010 = 5,
            B_0000000000000020 = 6,
            B_0000000000000040 = 7,
            B_0000000000000080 = 8,
            B_0000000000000100 = 9,
            B_0000000000000200 = 10,
            B_0000000000000400 = 11,
            B_0000000000000800 = 12,
            B_0000000000001000 = 13,
            B_0000000000002000 = 14,
            B_0000000000004000 = 15,
            B_0000000000008000 = 16,
            B_0000000000010000 = 17,
            B_0000000000020000 = 18,
            B_0000000000040000 = 19,
            B_0000000000080000 = 20,
            B_0000000000100000 = 21,
            B_0000000000200000 = 22,
            B_0000000000400000 = 23,
            B_0000000000800000 = 24,
            B_0000000001000000 = 25,
            B_0000000002000000 = 26,
            B_0000000004000000 = 27,
            B_0000000008000000 = 28,
            B_0000000010000000 = 29,
            B_0000000020000000 = 30,
            B_0000000040000000 = 31,
            B_0000000080000000 = 32,
            B_0000000100000000 = 33,
            B_0000000200000000 = 34,
            B_0000000400000000 = 35,
            B_0000000800000000 = 36,
            B_0000001000000000 = 37,
            B_0000002000000000 = 38,
            B_0000004000000000 = 39,
            B_0000008000000000 = 40,
            B_0000010000000000 = 41,
            B_0000020000000000 = 42,
            B_0000040000000000 = 43,
            B_0000080000000000 = 44,
            B_0000100000000000 = 45,
            B_0000200000000000 = 46,
            B_0000400000000000 = 47,
            B_0000800000000000 = 48,
            B_0001000000000000 = 49,
            B_0002000000000000 = 50
        }

        public enum ErrorCode
        {
            ERROR_PASSWORD_MUST_AT_LEAST_6_CHARS = 9003,
            ERROR_BUSY = 100004,
            ERROR_CHECK_SIM_CARD_CAN_UNUSEABLE = 101004,
            ERROR_CHECK_SIM_CARD_PIN_LOCK = 101002,
            ERROR_CHECK_SIM_CARD_PUN_LOCK = 101003,
            ERROR_COMPRESS_LOG_FILE_FAILED = 103102,
            ERROR_CRADLE_CODING_FAILED = 118005,
            ERROR_CRADLE_GET_CRURRENT_CONNECTED_USER_IP_FAILED = 118001,
            ERROR_CRADLE_GET_CRURRENT_CONNECTED_USER_MAC_FAILED = 118002,
            ERROR_CRADLE_GET_WAN_INFORMATION_FAILED = 118004,
            ERROR_CRADLE_SET_MAC_FAILED = 118003,
            ERROR_CRADLE_UPDATE_PROFILE_FAILED = 118006,
            ERROR_DEFAULT = -1,
            ERROR_DEVICE_AT_EXECUTE_FAILED = 103001,
            ERROR_DEVICE_COMPRESS_LOG_FILE_FAILED = 103015,
            ERROR_DEVICE_GET_API_VERSION_FAILED = 103006,
            ERROR_DEVICE_GET_AUTORUN_VERSION_FAILED = 103005,
            ERROR_DEVICE_GET_LOG_INFORMATON_LEVEL_FAILED = 103014,
            ERROR_DEVICE_GET_PC_AISSST_INFORMATION_FAILED = 103012,
            ERROR_DEVICE_GET_PRODUCT_INFORMATON_FAILED = 103007,
            ERROR_DEVICE_NOT_SUPPORT_REMOTE_OPERATE = 103010,
            ERROR_DEVICE_PIN_MODIFFY_FAILED = 103003,
            ERROR_DEVICE_PIN_VALIDATE_FAILED = 103002,
            ERROR_DEVICE_PUK_DEAD_LOCK = 103011,
            ERROR_DEVICE_PUK_MODIFFY_FAILED = 103004,
            ERROR_DEVICE_RESTORE_FILE_DECRYPT_FAILED = 103016,
            ERROR_DEVICE_RESTORE_FILE_FAILED = 103018,
            ERROR_DEVICE_RESTORE_FILE_VERSION_MATCH_FAILED = 103017,
            ERROR_DEVICE_SET_LOG_INFORMATON_LEVEL_FAILED = 103013,
            ERROR_DEVICE_SET_TIME_FAILED = 103101,
            ERROR_DEVICE_SIM_CARD_BUSY = 103008,
            ERROR_DEVICE_SIM_LOCK_INPUT_ERROR = 103009,
            ERROR_DHCP_ERROR = 104001,
            ERROR_DIALUP_ADD_PRORILE_ERROR = 107724,
            ERROR_DIALUP_DIALUP_MANAGMENT_PARSE_ERROR = 107722,
            ERROR_DIALUP_GET_AUTO_APN_MATCH_ERROR = 107728,
            ERROR_DIALUP_GET_CONNECT_FILE_ERROR = 107720,
            ERROR_DIALUP_GET_PRORILE_LIST_ERROR = 107727,
            ERROR_DIALUP_MODIFY_PRORILE_ERROR = 107725,
            ERROR_DIALUP_SET_AUTO_APN_MATCH_ERROR = 107729,
            ERROR_DIALUP_SET_CONNECT_FILE_ERROR = 107721,
            ERROR_DIALUP_SET_DEFAULT_PRORILE_ERROR = 107726,
            ERROR_DISABLE_AUTO_PIN_FAILED = 101008,
            ERROR_DISABLE_PIN_FAILED = 101006,
            ERROR_ENABLE_AUTO_PIN_FAILED = 101009,
            ERROR_ENABLE_PIN_FAILED = 101005,
            ERROR_FIRST_SEND = 1,
            ERROR_FORMAT_ERROR = 100005,
            ERROR_GET_CONFIG_FILE_ERROR = 100008,
            ERROR_GET_CONNECT_STATUS_FAILED = 102004,
            ERROR_GET_NET_TYPE_FAILED = 102001,
            ERROR_GET_ROAM_STATUS_FAILED = 102003,
            ERROR_GET_SERVICE_STATUS_FAILED = 102002,
            ERROR_LANGUAGE_GET_FAILED = 109001,
            ERROR_LANGUAGE_SET_FAILED = 109002,
            ERROR_LOGIN_TOO_FREQUENTLY = 108003,
            ERROR_LOGIN_MODIFY_PASSWORD_FAILED = 108004,
            ERROR_LOGIN_NO_EXIST_USER = 108001,
            ERROR_LOGIN_PASSWORD_ERROR = 108002,
            ERROR_LOGIN_TOO_MANY_TIMES = 108007,
            ERROR_LOGIN_TOO_MANY_USERS_LOGINED = 108005,
            ERROR_LOGIN_USERNAME_OR_PASSWORD_ERROR = 108006,
            ERROR_NET_CURRENT_NET_MODE_NOT_SUPPORT = 112007,
            ERROR_NET_MEMORY_ALLOC_FAILED = 112009,
            ERROR_NET_NET_CONNECTED_ORDER_NOT_MATCH = 112006,
            ERROR_NET_REGISTER_NET_FAILED = 112005,
            ERROR_NET_SIM_CARD_NOT_READY_STATUS = 112008,
            ERROR_FIRMWARE_NOT_SUPPORTED = 100002,
            ERROR_NO_DEVICE = -2,
            ERROR_NO_RIGHT = 100003,
            ERROR_NO_SIM_CARD_OR_INVALID_SIM_CARD = 101001,
            ERROR_ONLINE_UPDATE_ALREADY_BOOTED = 110002,
            ERROR_ONLINE_UPDATE_CANCEL_DOWNLODING = 110007,
            ERROR_ONLINE_UPDATE_CONNECT_ERROR = 110009,
            ERROR_ONLINE_UPDATE_GET_DEVICE_INFORMATION_FAILED = 110003,
            ERROR_ONLINE_UPDATE_GET_LOCAL_GROUP_COMMPONENT_INFORMATION_FAILED = 110004,
            ERROR_ONLINE_UPDATE_INVALID_URL_LIST = 110021,
            ERROR_ONLINE_UPDATE_LOW_BATTERY = 110024,
            ERROR_ONLINE_UPDATE_NEED_RECONNECT_SERVER = 110006,
            ERROR_ONLINE_UPDATE_NOT_BOOT = 110023,
            ERROR_ONLINE_UPDATE_NOT_FIND_FILE_ON_SERVER = 110005,
            ERROR_ONLINE_UPDATE_NOT_SUPPORT_URL_LIST = 110022,
            ERROR_ONLINE_UPDATE_SAME_FILE_LIST = 110008,
            ERROR_ONLINE_UPDATE_SERVER_NOT_ACCESSED = 110001,
            ERROR_PARAMETER_ERROR = 100006,
            ERROR_PB_CALL_SYSTEM_FUCNTION_ERROR = 115003,
            ERROR_PB_LOCAL_TELEPHONE_FULL_ERROR = 115199,
            ERROR_PB_NULL_ARGUMENT_OR_ILLEGAL_ARGUMENT = 115001,
            ERROR_PB_OVERTIME = 115002,
            ERROR_PB_READ_FILE_ERROR = 115005,
            ERROR_PB_WRITE_FILE_ERROR = 115004,
            ERROR_SAFE_ERROR = 106001,
            ERROR_SAVE_CONFIG_FILE_ERROR = 100007,
            ERROR_SD_DIRECTORY_EXIST = 114002,
            ERROR_SD_FILE_EXIST = 114001,
            ERROR_SD_FILE_IS_UPLOADING = 114007,
            ERROR_SD_FILE_NAME_TOO_LONG = 114005,
            ERROR_SD_FILE_OR_DIRECTORY_NOT_EXIST = 114004,
            ERROR_SD_IS_OPERTED_BY_OTHER_USER = 114004,
            ERROR_SD_NO_RIGHT = 114006,
            ERROR_SET_NET_MODE_AND_BAND_FAILED = 112003,
            ERROR_SET_NET_MODE_AND_BAND_WHEN_DAILUP_FAILED = 112001,
            ERROR_SET_NET_SEARCH_MODE_FAILED = 112004,
            ERROR_SET_NET_SEARCH_MODE_WHEN_DAILUP_FAILED = 112002,
            ERROR_SMS_DELETE_SMS_FAILED = 113036,
            ERROR_SMS_LOCAL_SPACE_NOT_ENOUGH = 113053,
            ERROR_SMS_NULL_ARGUMENT_OR_ILLEGAL_ARGUMENT = 113017,
            ERROR_SMS_OVERTIME = 113018,
            ERROR_SMS_QUERY_SMS_INDEX_LIST_ERROR = 113020,
            ERROR_SMS_SAVE_CONFIG_FILE_FAILED = 113047,
            ERROR_SMS_SET_SMS_CENTER_NUMBER_FAILED = 113031,
            ERROR_SMS_TELEPHONE_NUMBER_TOO_LONG = 113054,
            ERROR_STK_CALL_SYSTEM_FUCNTION_ERROR = 116003,
            ERROR_STK_NULL_ARGUMENT_OR_ILLEGAL_ARGUMENT = 116001,
            ERROR_STK_OVERTIME = 116002,
            ERROR_STK_READ_FILE_ERROR = 116005,
            ERROR_STK_WRITE_FILE_ERROR = 116004,
            ERROR_UNKNOWN = 100001,
            ERROR_UNLOCK_PIN_FAILED = 101007,
            ERROR_USSD_AT_SEND_FAILED = 111018,
            ERROR_USSD_CODING_ERROR = 111017,
            ERROR_USSD_EMPTY_COMMAND = 111016,
            ERROR_USSD_ERROR = 111001,
            ERROR_USSD_FUCNTION_RETURN_ERROR = 111012,
            ERROR_USSD_IN_USSD_SESSION = 111013,
            ERROR_USSD_NET_NOT_SUPPORT_USSD = 111022,
            ERROR_USSD_NET_NO_RETURN = 111019,
            ERROR_USSD_NET_OVERTIME = 111020,
            ERROR_USSD_TOO_LONG_CONTENT = 111014,
            ERROR_USSD_XML_SPECIAL_CHARACTER_TRANSFER_FAILED = 111021,
            ERROR_WIFI_PBC_CONNECT_FAILED = 117003,
            ERROR_WIFI_STATION_CONNECT_AP_PASSWORD_ERROR = 117001,
            ERROR_WIFI_STATION_CONNECT_AP_WISPR_PASSWORD_ERROR = 117004,
            ERROR_WIFI_WEB_PASSWORD_OR_DHCP_OVERTIME_ERROR = 117002,
            ERROR_WRITE_ERROR = 100009,
            ERROR_THE_SD_CARD_IS_CURRENTLY_IN_USE = 114003,
            ERROR_VOICE_CALL_BUSY = 120001,
            ERROR_INVALID_TOKEN = 125001,
            ERROR_SESSION = 125002,
            ERROR_WRONG_SESSION_TOKEN = 125003
        }

        public Form1()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            #region addAPI
            comboBoxAPIList.Items.AddRange(new string[] { "api/ussd/send", "api/lan/HostInfo", "api/cradle/factory-mac", "api/led/circle-switch","api/cradle/basic-info","api/cradle/status-info","api/device/autorun-version","api/device/fastbootswitch", "api/device/control",
"api/device/information","api/device/powersaveswitch","api/dhcp/settings", "api/device/signal",
"api/dialup/auto-apn","api/dialup/connection","api/dialup/dial","api/dialup/mobile-dataswitch","api/dialup/profiles","api/filemanager/upload","api/global/module-switch","api/host/info",
"api/language/current-language","api/monitoring/check-notifications","api/monitoring/clear-traffic",
"api/monitoring/converged-status","api/monitoring/month_statistics",
"api/monitoring/month_statistics_wlan",
"api/monitoring/start_date",
"api/monitoring/start_date_wlan",
"api/monitoring/status",
"api/monitoring/traffic-statistics",
"api/net/current-plmn",
"api/net/net-mode",
"api/net/net-mode-list",
"api/net/network",
"api/net/plmn-list",
"api/net/register",
"api/online-update/ack-newversion",
"api/online-update/cancel-downloading",
"api/online-update/check-new-version",
"api/online-update/status",
"api/online-update/url-list",
"api/online-update/autoupdate-config",
"api/online-update/configuration",
"api/ota/status",
"api/pb/pb-match",
"api/pin/operate",
"api/pin/simlock",
"api/pin/status",
"api/redirection/homepage",
"api/security/dmz",
"api/security/firewall-switch",
"api/security/lan-ip-filter",
"api/security/nat",
"api/security/sip",
"api/security/special-applications",
"api/security/upnp",
"api/security/virtual-servers",
"api/sms/backup-sim",
"api/sms/cancel-send",
"api/sms/cofig",
"api/sms/config",
"api/sms/delete-sms",
"api/sms/save-sms",
"api/sms/send-sms",
"api/sms/send-status",
"api/sms/set-read",
"api/sms/sms-count",
"api/sms/sms-list",
"api/sntp/sntpswitch",
"api/user/login",
"api/user/logout",
"api/user/password",
"api/user/remind",
"api/user/session",
"api/user/state-login",
"api/ussd/get",
"api/wlan/basic-settings",
"api/wlan/handover-setting",
"api/wlan/host-list",
"api/wlan/mac-filter",
"api/wlan/multi-basic-settings",
"api/wlan/multi-security-settings",
"api/wlan/multi-switch-settings",
"api/wlan/oled-showpassword",
"api/wlan/security-settings",
"api/wlan/station-information",
"api/wlan/wifi-dataswitch",
"api/webserver/white_list_switch",
"api/device/mode",
"config/deviceinformation/config.xml",
"config/dialup/config.xml",
"config/dialup/connectmode.xml",
"config/firewall/config.xml",
"config/global/config.xml",
"config/global/languagelist.xml",
"config/global/net-type.xml",
"config/network/net-mode.xml",
"config/network/networkband_",
"config/network/networkmode.xml",
"config/pcassistant/config.xml",
"config/pincode/config.xml",
"config/sms/config.xml",
"config/update/config.xml",
"config/wifi/configure.xml",
"config/wifi/countryChannel.xml", });
            #endregion addAPI
            logoutStatus = "";
            runAPIstatus = "";
            mcc = ""; mnc = ""; path = ""; band_tool = ""; args = ""; server_detail = ""; logininfo = ""; authinfo = "";
            _sessionID = "";
            _token = "";
            _requestToken = "";
            _requestTokenOne = "";
            _requestTokenTwo = "";
            _sessionCookie = "";
            dataGridView1.ScrollBars = ScrollBars.Both;


            
        }

        private void Processlog(string text)
        {

        }

        private void ShowMsgBoxInfo(string text)
        {
            MessageBox.Show(text, "Huawei Router Tool", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ShowMsgBoxError(string text)
        {
            MessageBox.Show(text, "Huawei Router Tool", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void ShowMsgBoxDialog(string text)
        {
            MessageBox.Show(text, "Huawei Router Tool", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
        }

        public void LogDebug(string message)
        {
            if (PrintDebugMessages)
            {
                richTextBoxLog.ForeColor = Color.Black;
                richTextBoxLog.Text += "[*] " + DateTime.Now + " : " + message + Environment.NewLine;
            }
        }

        private void LogError(string message)
        {
            if (PrintDebugMessages)
            {
                richTextBoxLog.ForeColor = Color.Red;
                richTextBoxLog.Text += message;
            }
        }

        private bool PingServer()
        {
            try
            {
                Ping x = new Ping();
                PingReply reply = x.Send(IPAddress.Parse(textBoxIP.Text));

                if (reply.Status == IPStatus.Success)
                    return true;
                else
                    return false;
            }
            catch
            {
                return false;

            }

        }


        private void Initialise()
        {
            if (string.IsNullOrEmpty(_sessionCookie) || string.IsNullOrEmpty(_requestToken))
            {
                GetTokens();
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

        private void GetTokens()
        {
            //LogDebug("Fetching new session and token information..");

            try
            {
                XmlDocument GetTokens_doc = Get("api/webserver/SesTokInfo");
                _sessionID = GetTokens_doc.SelectSingleNode("//response/SesInfo").InnerText;
                _token = GetTokens_doc.SelectSingleNode("//response/TokInfo").InnerText;

                //LogDebug(string.Format("\n[*] New session ID: {0}", _sessionID));
                //LogDebug(string.Format("\n[*] New token ID: {0}", _token));

                _requestToken = _token;
                _sessionCookie = _sessionID;
            }
            catch
            {
                LogError("Unable to connect to the remote server. Make sure PC is connected to the network!");
            }


        }

        private WebClient NewWebClient()
        {
            var wc = new WebClient();
            wc.Headers.Add(HttpRequestHeader.Cookie, _sessionCookie);
            //wc.Headers.Add("Cache-Control", "no-cache");
            wc.Headers.Add("__RequestVerificationToken", _requestToken);
            wc.Headers.Add("Accept", "*/*");
            wc.Headers.Add("User-Agent", "Mozilla/5.0 (Windows; U; Windows NT 5.1; en-US; rv:1.9.2.12) Gecko/20101026 Firefox/3.6.12");
            return wc;
        }

        private  XmlDocument Get_API(string path)
        {
           

            var wc = NewWebClient();
            try
            {
                
                var data = wc.DownloadString("http://" + textBoxIP.Text + "/" + path);
                HandleHeaders(wc);
                XmlDocument Get_doc = new XmlDocument();
                if(data != string.Empty)
                {
                    Get_doc.LoadXml(data);
                    doc1_API = Get_doc;
                }
                else
                {
                    doc1_API = null;
                }
                
            }
            catch (WebException webEx)
            {
                LogDebug(webEx.Message + ".");
            }


            return doc1_API;
        }

        public XmlDocument Get(string path)
        {
            var wc = NewWebClient();
            try
            {
                var data = wc.DownloadString("http://" + textBoxIP.Text + "/" + path);
                HandleHeaders(wc);
                XmlDocument Get_doc = new XmlDocument();
                Get_doc.LoadXml(data);
                doc1 = Get_doc;
            }
            catch (WebException webEx)
            {
                LogDebug(webEx.Message + ".");

            }

            return doc1;

        }

        public XmlDocument Get_new(string path)
        {
            var wc = NewWebClient_new();
            try
            {
                var data = wc.DownloadString("http://" + textBoxIP.Text + "/" + path);
                HandleHeaders(wc);
                XmlDocument Get_doc = new XmlDocument();
                Get_doc.LoadXml(data);
                doc1 = Get_doc;
            }
            catch (WebException webEx)
            {
                LogDebug(webEx.Message + ".");

            }
            return doc1;
        }

        private WebClient NewWebClient_new()
        {
            var wc = new WebClient();
            wc.Headers.Add(HttpRequestHeader.Cookie, _CurrentSessionID);
            //wc.Headers.Add("Cache-Control", "no-cache");
            wc.Headers.Add("__RequestVerificationToken", _CurrentToken);
            wc.Headers.Add("Accept", "*/*");
            wc.Headers.Add("User-Agent", "Mozilla/5.0 (Windows; U; Windows NT 5.1; en-US; rv:1.9.2.12) Gecko/20101026 Firefox/3.6.12");
            return wc;
        }

        private void HandleHeaders(WebClient wc)
        {
            if (!string.IsNullOrEmpty(wc.ResponseHeaders["__RequestVerificationTokenOne"]))
            {
                _requestTokenOne = wc.ResponseHeaders["__RequestVerificationTokenOne"];
                //LogDebug(string.Format("\n[*] Recieved new RVT1: {0}", _requestTokenOne));
            }
            if (!string.IsNullOrEmpty(wc.ResponseHeaders["__RequestVerificationTokenTwo"]))
            {
                _requestTokenTwo = wc.ResponseHeaders["__RequestVerificationTokenTwo"];
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
        private XmlDocument Post(string path, string data)
        {
            var wc = NewWebClient();
            try
            {
                var response = wc.UploadData("http://" + textBoxIP.Text + "/" + path, Encoding.UTF8.GetBytes(data));
                var responseString = Encoding.Default.GetString(response);
                HandleHeaders(wc);
                XmlDocument Post_doc = new XmlDocument();
                Post_doc.LoadXml(responseString);
                doc2 = Post_doc;
            }
            catch (Exception e)
            {
                ShowMsgBoxError(e.Message);

            }

            return doc2;
        }

        public async Task GetInfo()
        {
            var simlock = Get("api/pin/simlock");
            var Info = Get("api/device/information");
            var plmn_list = Get("api/net/current-plmn");

            string Null = "NA";
            


            if (Info.SelectSingleNode("//response/DeviceName") != null)
            {
                string DeviceName = Info.SelectSingleNode("//response/DeviceName").InnerText;
                textBoxModel.Text = DeviceName;
                Logger.log(string.Format("Device name              : {0}", Info.SelectSingleNode("//response/DeviceName").InnerText));
            }
            else
            {
                textBoxModel.Text = "NA";
                Logger.log(string.Format("Device name              : {0}", Null));
            }


            if (Info.SelectSingleNode("//response/SerialNumber") != null)
            {
                string SerialNumber = Info.SelectSingleNode("//response/SerialNumber").InnerText;
                textBoxSerialNumber.Text = SerialNumber;
                Logger.log(string.Format("Serial Number            : {0}", Info.SelectSingleNode("//response/SerialNumber").InnerText));
            }
            else
            {
                textBoxSerialNumber.Text = "NA";
                Logger.log(string.Format("Serial Number            : {0}", Null));
            }

            if (Info.SelectSingleNode("//response/Imei") != null)
            {
                string Imei = Info.SelectSingleNode("//response/Imei").InnerText;
                textBoxImei.Text = Imei;
                Logger.log(string.Format("Imei                     : {0}", Info.SelectSingleNode("//response/Imei").InnerText));
            }
            else
            {
                textBoxImei.Text = "NA";
                Logger.log(string.Format("Imei                     : {0}", Null));
            }

            if (Info.SelectSingleNode("//response/Iccid") != null)
            {
                string Iccid = Info.SelectSingleNode("//response/Iccid").InnerText;
                textBoxIccid.Text = Iccid;
                Logger.log(string.Format("Iccid                    : {0}", Info.SelectSingleNode("//response/Iccid").InnerText));
            }
            else
            {
                textBoxIccid.Text = "NA";
                Logger.log(string.Format("Iccid                    : {0}", Null));
            }

            if (Info.SelectSingleNode("//response/Iccid") != null)
            {
                string Iccid = Info.SelectSingleNode("//response/Iccid").InnerText;
                textBoxIccid.Text = Iccid;
                Logger.log(string.Format("Iccid                    : {0}", Info.SelectSingleNode("//response/Iccid").InnerText));
            }
            else
            {
                textBoxIccid.Text = "NA";
                Logger.log(string.Format("Iccid                    : {0}", Null));
            }

            if (Info.SelectSingleNode("//response/Msisdn") != null)
            {
                Msisdn = Info.SelectSingleNode("//response/Msisdn").InnerText;
                textBoxMsisdn.Text = Msisdn;
                Logger.log(string.Format("Msisdn                   : {0}", Info.SelectSingleNode("//response/Msisdn").InnerText));
            }
            else
            {
                textBoxMsisdn.Text = "NA";
                Logger.log(string.Format("Msisdn                   : {0}", Null));
            }

            if (Info.SelectSingleNode("//response/HardwareVersion") != null)
            {
                string HardwareVersion = Info.SelectSingleNode("//response/HardwareVersion").InnerText;
                textBoxHardwareVersion.Text = HardwareVersion;
                Logger.log(string.Format("Hardware Version         : {0}", Info.SelectSingleNode("//response/HardwareVersion").InnerText));
            }
            else
            {
                textBoxHardwareVersion.Text = "NA";
                Logger.log(string.Format("Hardware Version         : {0}", Null));
            }

            if (Info.SelectSingleNode("//response/SoftwareVersion") != null)
            {
                string SoftwareVersion = Info.SelectSingleNode("//response/SoftwareVersion").InnerText;
                textBoxSoftwareVersion.Text = SoftwareVersion;
                Logger.log(string.Format("Software Version         : {0}", Info.SelectSingleNode("//response/SoftwareVersion").InnerText));
            }
            else
            {
                textBoxSoftwareVersion.Text = "NA";
                Logger.log(string.Format("Software Version         : {0}", Null));
            }

            if (Info.SelectSingleNode("//response/WebUIVersion") != null)
            {
                string WebUIVersion = Info.SelectSingleNode("//response/WebUIVersion").InnerText;
                textBoxWebuiVersion.Text = WebUIVersion;
                Logger.log(string.Format("Web UI Version           : {0}", Info.SelectSingleNode("//response/WebUIVersion").InnerText));
            }
            else
            {
                textBoxWebuiVersion.Text = "NA";
                Logger.log(string.Format("Web UI Version           : {0}", Null));
            }

            if (Info.SelectSingleNode("//response/MacAddress1") != null)
            {
                string MacAddress1 = Info.SelectSingleNode("//response/MacAddress1").InnerText;
                textBoxMacAddress.Text = MacAddress1;
                Logger.log(string.Format("Mac Address1             : {0}", Info.SelectSingleNode("//response/MacAddress1").InnerText));
            }
            else
            {
                textBoxMacAddress.Text = "NA";
                Logger.log(string.Format("MacAddress1              : {0}", Null));
            }

            if (plmn_list.SelectSingleNode("//response/Numeric") != null)
            {
                string Numeric = plmn_list.SelectSingleNode("//response/Numeric").InnerText;
                //mcc = Numeric.Substring(0, 3);
                //mnc = Numeric.Substring(3, 2);
                //string mcc_mnc = mcc + "-" + mnc;
                textBoxMNC.Text = Numeric.ToString();

            }
            else
            {
                textBoxMNC.Text = "NA";
            }


            if (simlock.SelectSingleNode("//response/SimLockEnable") != null)
            {
                string SimLockEnable = simlock.SelectSingleNode("//response/SimLockEnable").InnerText;
                textBoxsimLock.Text = SimLockEnable.Replace("0", "Unlocked").Replace("1", "Locked");
                Logger.log(string.Format("Simlock Status           : {0}", simlock.SelectSingleNode("//response/SimLockEnable").InnerText));
            }
            else
            {
                textBoxsimLock.Text = "NA";
                Logger.log(string.Format("Simlock Status           : {0}", Null));
            }

            if (textBoxMNC.Text != "NA")
            {
                linkLabel2.Visible = true;

            }
            
          
        }

       

        private async void Form1_Load(object sender, EventArgs e)
        {
            //checking new version if available at github

            var version = System.Reflection.Assembly.GetEntryAssembly().GetName().Version;

            var checker = new UpdateChecker("pearlxcore", "Huawei-Router-Tool", "7"); // latest version on github release. Update type will use type Major

            UpdateType update = await checker.CheckUpdate();


            if (update == UpdateType.Major) // Major.Minor.x.x
            {
                //uptodate
            }
            else
            {
                DialogResult dialog = MessageBox.Show("New version available. Download now?", "Huawei Router Tool", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (dialog == DialogResult.OK)
                {
                    System.Diagnostics.Process.Start("https://github.com/pearlxcore/Huawei-Router-Tool/releases/tag/v7");
                }
            }

            try
            {
                path = Path.GetTempPath() + @"HuaweiRouterTool\";
                band_tool = path + @"huawei_band_tool\";

                if (Directory.Exists(path))
                {
                    Directory.Delete(path, true);
                }

                //Directory.CreateDirectory(path);
                Directory.CreateDirectory(band_tool);

                File.WriteAllBytes(band_tool + "huawei_band_tool.exe", Properties.Resources.huawei_band_tool);
            }
            catch
            {
                //MessageBox.Show("An error occured", "Huawei Router Tool", MessageBoxButtons.OK, MessageBoxIcon.Hand);

            }


            textBoxUsername.Text = Huawei_Router_Tool_GUI.Properties.Settings.Default.Username;
            textBoxPassword.Text = Huawei_Router_Tool_GUI.Properties.Settings.Default.Password;
            textBoxIP.Text = Huawei_Router_Tool_GUI.Properties.Settings.Default.IPAddress;
            checkBoxRememberUserpass.CheckState = Huawei_Router_Tool_GUI.Properties.Settings.Default.Checkbox;
            autoLoginChk.Checked = Huawei_Router_Tool_GUI.Properties.Settings.Default.AutoLogin;

            //automatic login if checkBoxRememberUserpass state is true
            //if (checkBoxRememberUserpass.Checked == true)
            if (autoLoginChk.Checked == true)
            {
                buttonLogin.PerformClick();
                //ButtonLogin_Click_3(null);
            }
            //end of automatic login if checkBoxRememberUserpass state is true

            //MessageBox.Show("this is beta version, so pls do me a favor..\n\npls test if there any critical bug or glitch..\n\npls test if the app is too heavy for ur machine..\n\npls test all the function are working or not..\n\npls dont share this program.. this is specifically build for tester..\n\npls state ur machine specs (os platform)..\n\nTQ\n\n- pearlxcore -\n\nFixed :\n-signal monitor button locked\n-login button locked", "Huawei Router Tool", MessageBoxButtons.OK, MessageBoxIcon.Information);



        }

        public void GetTraffic()
        {

            string NULL = "NA";
            var provider = Get("api/net/current-plmn");
            var traffic = Get("api/monitoring/traffic-statistics");
            var signal = Get("api/device/signal");
            var Info = Get("api/device/information");

            //FullName
            if (provider.SelectSingleNode("//response/FullName") != null)
            {
                string FullName = provider.SelectSingleNode("//response/FullName").InnerText;
                textBoxNetworkProvider.Text = FullName;

            }
            else
            {
                textBoxNetworkProvider.Text = "NA";
                Logger.log("provider                 : NA");
            }

            //workmode
            if (Info.SelectSingleNode("//response/workmode") != null)
            {
                string workmode = Info.SelectSingleNode("//response/workmode").InnerText;
                textBoxWorkmode.Text = workmode;

            }
            else
            {
                textBoxWorkmode.Text = "NA";
                Logger.log("workmode                 : NA");
            }

            //CurrentDownloadRate
            if (traffic.SelectSingleNode("//response/CurrentDownloadRate") != null)
            {
                string CurrentDownloadRate = traffic.SelectSingleNode("//response/CurrentDownloadRate").InnerText;
                int a = Int32.Parse(CurrentDownloadRate);
                DownloadRate = ByteSize.FromBytes(a).ToString(); // 1 MB
                textBoxCurrentDownloadRate.Text = DownloadRate.ToString();

            }
            else
            {
                textBoxCurrentDownloadRate.Text = NULL;
            }

            //CurrentUploadRate
            if (traffic.SelectSingleNode("//response/CurrentUploadRate") != null)
            {
                string CurrentUploadRate = traffic.SelectSingleNode("//response/CurrentUploadRate").InnerText;
                int b = Int32.Parse(CurrentUploadRate);
                UploadRate = ByteSize.FromBytes(b).ToString(); // 1 MB
                textBoxCurrentUploadRate.Text = UploadRate.ToString();

            }
            else
            {
                textBoxCurrentUploadRate.Text = NULL;
            }

            //currentconnecttime
            if (traffic.SelectSingleNode("//response/CurrentConnectTime") != null)
            {
                string CurrentConnectTime = traffic.SelectSingleNode("//response/CurrentConnectTime").InnerText;
                int c = Int32.Parse(CurrentConnectTime);
                int hours = c / 3600;
                int mins = (c % 3600) / 60;
                c = c % 60;
                textBoxCurrentConnectTime.Text = (string.Format("{0:D2}:{1:D2}:{2:D2}", hours, mins, c));

            }
            else
            {
                textBoxCurrentConnectTime.Text = NULL;
            }

            //totalupload
            if (traffic.SelectSingleNode("//response/TotalUpload") != null)
            {
                string TotalUpload = traffic.SelectSingleNode("//response/TotalUpload").InnerText;
                var uploads = Convert.ToInt64(TotalUpload);
                var tupload = ByteSize.FromBytes(uploads).ToString();
                textBoxTotalUpload.Text = tupload.ToString();
            }
            else
            {
                textBoxTotalUpload.Text = NULL;
            }

            //totaldownload
            if (traffic.SelectSingleNode("//response/TotalDownload") != null)
            {
                string TotalDownload = traffic.SelectSingleNode("//response/TotalDownload").InnerText;
                var download = Convert.ToInt64(TotalDownload);
                var tdownload = ByteSize.FromBytes(download).ToString();
                textBoxTotalDownload.Text = tdownload.ToString();
            }
            else
            {
                textBoxTotalDownload.Text = NULL;
            }

            //totaltime
            if (traffic.SelectSingleNode("//response/TotalConnectTime") != null)
            {
                string TotalConnectTime = traffic.SelectSingleNode("//response/TotalConnectTime").InnerText;
                int d = Int32.Parse(TotalConnectTime);
                int thours = d / 3600;
                int tmins = (d % 3600) / 60;
                d = d % 60;
                textBoxTotalConnectTime.Text = (string.Format("{0:D2}:{1:D2}:{2:D2}", thours, tmins, d));

            }
            else
            {
                textBoxTotalConnectTime.Text = NULL;
            }

            if (signal.SelectSingleNode("//response/rsrq") != null)
            {
                RSRQ = signal.SelectSingleNode("//response/rsrq").InnerText;
                SeriesRSRQ = RSRQ.Replace("dB", "");
                textBoxRSRQ.Text = RSRQ.ToString();
            }
            else
            {
                textBoxRSRQ.Text = NULL;
            }

            if (signal.SelectSingleNode("//response/rsrp") != null)
            {
                RSRP = signal.SelectSingleNode("//response/rsrp").InnerText;
                SeriesRSRP = RSRP.Replace("dBm", "");
                textBoxRSRP.Text = RSRP.ToString();
            }
            else
            {
                textBoxRSRP.Text = NULL;
            }

            if (signal.SelectSingleNode("//response/rssi") != null)
            {
                RSSI = signal.SelectSingleNode("//response/rssi").InnerText;
                SeriesRSSI = RSSI.Replace("dBm", "");
                textBoxRSSI.Text = RSSI.ToString();
            }
            else
            {
                textBoxRSSI.Text = NULL;
            }

            if (signal.SelectSingleNode("//response/sinr") != null)
            {
                sinr = signal.SelectSingleNode("//response/sinr").InnerText;
                Seriessinr = sinr.Replace("dB", "");
                textBoxSINR.Text = sinr.ToString();
            }
            else
            {
                textBoxSINR.Text = NULL;
            }


            try
            {
                Logger.logSignal(string.Format(" SINR : {0}  |  RSSI : {1}  |  RSRP : {2}  |  RSRQ : {3}", sinr.ToString(), RSSI.ToString(), RSRP.ToString(), RSRQ.ToString()));

            }
            catch
            {

            }



            //nei_cellid
            if (signal.SelectSingleNode("//response/nei_cellid") != null)
            {
                nei_cellid = signal.SelectSingleNode("//response/nei_cellid").InnerText;
                nei_cellid.Replace("No", " No");
            }
            else
            {
                nei_cellid = "NA";
            }

            //txpower
            if (signal.SelectSingleNode("//response/txpower") != null)
            {
                txpower = signal.SelectSingleNode("//response/txpower").InnerText;
            }
            else
            {
                txpower = "NA";
            }

            //cell_id
            if (signal.SelectSingleNode("//response/cell_id") != null)
            {
                cell_id = signal.SelectSingleNode("//response/cell_id").InnerText;
            }
            else
            {
                cell_id = "NA";
            }

            //ulbandwidth
            if (signal.SelectSingleNode("//response/ulbandwidth") != null)
            {
                ulbandwidth = signal.SelectSingleNode("//response/ulbandwidth").InnerText;
            }
            else
            {
                ulbandwidth = "NA";
            }

            //dlbandwidth
            if (signal.SelectSingleNode("//response/dlbandwidth") != null)
            {
                dlbandwidth = signal.SelectSingleNode("//response/dlbandwidth").InnerText;
            }
            else
            {
                dlbandwidth = "NA";
            }

            //earfcn
            if (signal.SelectSingleNode("//response/earfcn") != null)
            {
                earfcn = signal.SelectSingleNode("//response/earfcn").InnerText;
            }
            else
            {
                earfcn = "NA";
            }

            metroButtonSIGNALMONITOR.Enabled = true;


        }

        private async Task GetDeviceSetting()
        {
            this.Invoke((MethodInvoker)delegate {



                //get session id n token
                var Sestoken = Get("api/webserver/SesTokInfo");
                _CurrentSessionID = Sestoken.SelectSingleNode("//response/SesInfo").InnerText;
                _CurrentToken = Sestoken.SelectSingleNode("//response/TokInfo").InnerText;

                var FWUpdate1 = Get_new("api/webserver/white_list_switch");

                if (FWUpdate1.OuterXml.ToString() != string.Empty)
                {
                    if (FWUpdate1.OuterXml.ToString().Contains("response"))
                    {
                        string setting1 = xmlformat.Beautify(FWUpdate1);

                        File.WriteAllText(path + @"setting1.XML", setting1);
                        var lines1 = File.ReadAllLines(path + @"setting1.XML");
                        File.WriteAllLines(path + @"setting1.XML", lines1.Skip(1).ToArray());

                        DataSet DataSet1 = new DataSet();
                        DataSet1.ReadXml(path + @"setting1.XML");
                        foreach (DataRow dr1 in DataSet1.Tables["response"].Rows)
                        {
                            if (dr1["whitelist_enable"].ToString() == "0")
                            {
                                checkBoxFW1.Checked = false;
                            }
                            else
                            {
                                checkBoxFW1.Checked = true;

                            }


                        }
                    }
                    else if (FWUpdate1.OuterXml.ToString().Contains("error"))
                    {
                        LogDebug("Failed to retrieve white list switch : ERROR " + FWUpdate1.SelectSingleNode("//error/code").InnerText.ToString() + " [" + ((ErrorCode)(int.Parse(FWUpdate1.SelectSingleNode("//error/code").InnerText))).ToString() + "]");

                    }
                }
                else
                {
                    LogDebug("Failed to retrieve white list switch.");

                }




                var FWUpdate2 = Get_new("api/online-update/configuration");
                if (FWUpdate2.OuterXml.ToString() != string.Empty)
                {
                    if (FWUpdate2.OuterXml.ToString().Contains("response"))
                    {
                        string setting2 = xmlformat.Beautify(FWUpdate2);

                        File.WriteAllText(path + @"setting2.XML", setting2);
                        var lines2 = File.ReadAllLines(path + @"setting2.XML");
                        File.WriteAllLines(path + @"setting2.XML", lines2.Skip(1).ToArray());
                        DataSet DataSet2 = new DataSet();
                        DataSet2.ReadXml(path + @"setting2.XML");
                        foreach (DataRow dr2 in DataSet2.Tables["response"].Rows)
                        {
                            if (dr2["server_force_enable"].ToString() == "0")
                            {
                                checkBoxFW2.Checked = false;
                            }
                            else
                            {
                                checkBoxFW2.Checked = true;

                            }


                        }
                    }
                    else if (FWUpdate2.OuterXml.ToString().Contains("error"))
                    {
                        LogDebug("Failed to retrieve firmware update configuration : ERROR " + FWUpdate2.SelectSingleNode("//error/code").InnerText.ToString() + " [" + ((ErrorCode)(int.Parse(FWUpdate2.SelectSingleNode("//error/code").InnerText))).ToString() + "]");

                    }

                }
                else
                {
                    LogDebug("Failed to retrieve firmware update configuration");

                }


                var FWUpdate3 = Get("api/online-update/autoupdate-config");
                if (FWUpdate3.OuterXml.ToString() != string.Empty)
                {
                    if (FWUpdate3.OuterXml.ToString().Contains("response"))
                    {
                        string setting3 = xmlformat.Beautify(FWUpdate3);

                        File.WriteAllText(path + @"setting3.XML", setting3);
                        var lines3 = File.ReadAllLines(path + @"setting3.XML");
                        File.WriteAllLines(path + @"setting3.XML", lines3.Skip(1).ToArray());

                        DataSet DataSet3 = new DataSet();
                        DataSet3.ReadXml(path + @"setting3.XML");
                        foreach (DataRow dr3 in DataSet3.Tables["response"].Rows)
                        {
                            if (dr3["auto_update"].ToString() == "0")
                            {
                                checkBoxFW3.Checked = false;
                            }
                            else
                            {
                                checkBoxFW3.Checked = true;

                            }


                        }
                    }
                    else if (FWUpdate3.OuterXml.ToString().Contains("error"))
                    {
                        LogDebug("Failed to retrieve auto firmware update configuration : ERROR " + FWUpdate3.SelectSingleNode("//error/code").InnerText.ToString() + " [" + ((ErrorCode)(int.Parse(FWUpdate3.SelectSingleNode("//error/code").InnerText))).ToString() + "]");

                    }
                    
                }
                else
                {
                    LogDebug("Failed to retrieve auto firmware update configuration.");

                }


                var DataSwitch = Get("api/dialup/mobile-dataswitch");
                if (DataSwitch.OuterXml.ToString() != string.Empty)
                {
                    if (DataSwitch.OuterXml.ToString().Contains("response"))
                    {
                        string setting4 = xmlformat.Beautify(DataSwitch);

                        File.WriteAllText(path + @"setting4.XML", setting4);
                        var lines4 = File.ReadAllLines(path + @"setting4.XML");
                        File.WriteAllLines(path + @"setting4.XML", lines4.Skip(1).ToArray());

                        DataSet DataSet4 = new DataSet();
                        DataSet4.ReadXml(path + @"setting4.XML");
                        foreach (DataRow dr4 in DataSet4.Tables["response"].Rows)
                        {
                            if (dr4["dataswitch"].ToString() == "0")
                            {
                                checkBoxMobileConnection.Checked = false;
                            }
                            else
                            {
                                checkBoxMobileConnection.Checked = true;

                            }


                        }
                    }
                    else if (DataSwitch.OuterXml.ToString().Contains("error"))
                    {
                        LogDebug("Failed to retrieve mobile data switch setting : ERROR " + DataSwitch.SelectSingleNode("//error/code").InnerText.ToString() + " [" + ((ErrorCode)(int.Parse(DataSwitch.SelectSingleNode("//error/code").InnerText))).ToString() + "]");

                    }
                    
                }
                else
                {
                    LogDebug("Failed to retrieve mobile data switch setting.");
                }


                var DMZ = Get("api/security/dmz");
                if (DMZ.OuterXml.ToString() != string.Empty)
                {
                    if (DMZ.OuterXml.ToString().Contains("response"))
                    {
                        string setting5 = xmlformat.Beautify(DMZ);

                        File.WriteAllText(path + @"setting5.XML", setting5);
                        var lines5 = File.ReadAllLines(path + @"setting5.XML");
                        File.WriteAllLines(path + @"setting5.XML", lines5.Skip(1).ToArray());

                        DataSet DataSet5 = new DataSet();
                        DataSet5.ReadXml(path + @"setting5.XML");
                        foreach (DataRow dr5 in DataSet5.Tables["response"].Rows)
                        {
                            if (dr5["DmzStatus"].ToString() == "0")
                            {
                                checkBoxDMZ.Checked = false;
                            }
                            else
                            {
                                checkBoxDMZ.Checked = true;

                            }

                            if (dr5["DmzIPAddress"].ToString() == "null")
                            {
                                textBoxDMZ.Text = "";
                            }
                            else
                            {
                                textBoxDMZ.Text = dr5["DmzIPAddress"].ToString();

                            }



                        }
                    }
                    else if (DMZ.OuterXml.ToString().Contains("error"))
                    {
                        LogDebug("Failed to retrieve DMZ setting : ERROR " + DMZ.SelectSingleNode("//error/code").InnerText.ToString() + " [" + ((ErrorCode)(int.Parse(DMZ.SelectSingleNode("//error/code").InnerText))).ToString() + "]");

                    }
                    
                }
                else
                {
                    LogDebug("Failed to retrieve DMZ setting.");
                }


                var SIP = Get("api/security/sip");
                if (SIP.OuterXml.ToString() != string.Empty)
                {
                    if (SIP.OuterXml.ToString().Contains("response"))
                    {
                        string setting6 = xmlformat.Beautify(SIP);

                        File.WriteAllText(path + @"setting6.XML", setting6);
                        var lines6 = File.ReadAllLines(path + @"setting6.XML");
                        File.WriteAllLines(path + @"setting6.XML", lines6.Skip(1).ToArray());

                        DataSet DataSet6 = new DataSet();
                        DataSet6.ReadXml(path + @"setting6.XML");
                        foreach (DataRow dr6 in DataSet6.Tables["response"].Rows)
                        {
                            if (dr6["SipStatus"].ToString() == "0")
                            {
                                checkBoxSIP.Checked = false;
                            }
                            else
                            {
                                checkBoxSIP.Checked = true;

                            }

                            if (dr6["SipPort"].ToString() == "0")
                            {
                                textBoxSIP.Text = "";
                            }
                            else
                            {
                                textBoxSIP.Text = dr6["SipPort"].ToString();

                            }

                        }
                    }
                    else if (SIP.OuterXml.ToString().Contains("error"))
                    {
                        LogDebug("Failed to retrieve SIP setting : ERROR " + SIP.SelectSingleNode("//error/code").InnerText.ToString() + " [" + ((ErrorCode)(int.Parse(SIP.SelectSingleNode("//error/code").InnerText))).ToString() + "]");

                    }
                    
                }
                else
                {
                    LogDebug("Failed to retrieve SIP setting.");

                }


                var UPnp = Get("api/security/upnp");
                if (UPnp.OuterXml.ToString() != string.Empty)
                {
                    if (UPnp.OuterXml.ToString().Contains("response"))
                    {
                        string setting7 = xmlformat.Beautify(UPnp);

                        File.WriteAllText(path + @"setting7.XML", setting7);
                        var lines7 = File.ReadAllLines(path + @"setting7.XML");
                        File.WriteAllLines(path + @"setting7.XML", lines7.Skip(1).ToArray());

                        DataSet DataSet7 = new DataSet();
                        DataSet7.ReadXml(path + @"setting7.XML");
                        foreach (DataRow dr7 in DataSet7.Tables["response"].Rows)
                        {
                            if (dr7["UpnpStatus"].ToString() == "0")
                            {
                                checkBoxUPnP.Checked = false;
                            }
                            else
                            {
                                checkBoxUPnP.Checked = true;

                            }



                        }
                    }
                    else if (UPnp.OuterXml.ToString().Contains("error"))
                    {
                        LogDebug("Failed to retrieve UPnp setting : ERROR " + UPnp.SelectSingleNode("//error/code").InnerText.ToString() + " [" + ((ErrorCode)(int.Parse(UPnp.SelectSingleNode("//error/code").InnerText))).ToString() + "]");

                    }
                    
                }
                else
                {
                    LogDebug("Failed to retrieve UPnp setting.");

                }


                var NAT = Get("api/security/nat");
                if (NAT.OuterXml.ToString() != string.Empty)
                {
                    if (NAT.OuterXml.ToString().Contains("response"))
                    {
                        string setting8 = xmlformat.Beautify(NAT);

                        File.WriteAllText(path + @"setting8.XML", setting8);
                        var lines8 = File.ReadAllLines(path + @"setting8.XML");
                        File.WriteAllLines(path + @"setting8.XML", lines8.Skip(1).ToArray());

                        DataSet DataSet8 = new DataSet();
                        DataSet8.ReadXml(path + @"setting8.XML");
                        foreach (DataRow dr8 in DataSet8.Tables["response"].Rows)
                        {
                            if (dr8["NATType"].ToString() == "0")
                            {
                                checkBoxNAT.Checked = false;
                            }
                            else
                            {
                                checkBoxNAT.Checked = true;

                            }



                        }
                    }
                    else if (NAT.OuterXml.ToString().Contains("error"))
                    {
                        LogDebug("Failed to retrieve NAT setting : ERROR " + NAT.SelectSingleNode("//error/code").InnerText.ToString() + " [" + ((ErrorCode)(int.Parse(NAT.SelectSingleNode("//error/code").InnerText))).ToString() + "]");

                    }
                    
                }
                else
                {
                    LogDebug("Failed to retrieve NAT setting.");

                }



            });

        }

        private async Task GetConnectedDevice()
        {

            listView1.Enabled = false;
            buttonRefreshConnectedClient.Enabled = false;

            listView1.Clear();
            listView1.View = View.Details;

            listView1.GridLines = true;

            listView1.FullRowSelect = true;
            listView1.Columns.Add("Host Name", 120);


            listView1.Columns.Add("MAC Address", 120);

            listView1.Columns.Add("Vendor", 120);


            listView1.Columns.Add("IP Address", 120);

            listView1.Columns.Add("Associated Time (Second)", 120);

            listView1.Columns.Add("Associated SSID", 120);

            var API = Get("api/wlan/host-list");
            if (API.OuterXml.ToString() != string.Empty)
            {
                if (API.OuterXml.ToString().Contains("response"))
                {
                    string test = xmlformat.Beautify(API);


                    File.WriteAllText(path + @"ConnectedDevice.XML", test);
                    var lines = File.ReadAllLines(path + @"ConnectedDevice.XML");
                    File.WriteAllLines(path + @"ConnectedDevice.XML", lines.Skip(1).ToArray());

                    DataSet ds = new DataSet();

                    ds.ReadXml(path + @"ConnectedDevice.XML");

                    ListViewItem item;

                    if (ds.Tables.Contains("host"))
                    {
                        buttonRefreshConnectedClient.Enabled = true;

                        listView1.Enabled = true;
                        foreach (DataRow dr in ds.Tables["Host"].Rows)
                        {

                            item = new ListViewItem(new string[] {

                                    dr["HostName"].ToString(),


                dr["MacAddress"].ToString(),

                                LookupMac(dr["MacAddress"].ToString()).Result,


                dr["IpAddress"].ToString(),


                dr["AssociatedTime"].ToString(),

                dr["AssociatedSsid"].ToString()});

                            listView1.Items.Add(item);
                            System.Threading.Thread.Sleep(500);

                        }
                    }
                    else
                    {
                        LogDebug("Failed to retrieve connected client device");
                    }
                }
                else if (API.OuterXml.ToString().Contains("error"))
                {
                    LogDebug("Failed to retrieve connected client device : ERROR " + API.SelectSingleNode("//error/code").InnerText.ToString() + " [" + ((ErrorCode)(int.Parse(API.SelectSingleNode("//error/code").InnerText))).ToString() + "]");
                }
            }
            else
            {
                LogDebug("Failed to retrieve connected client device");
            }







        }

        private async void backgroundWorkerDeviceInfo_DoWork(object sender, DoWorkEventArgs e)
        {
            LogDebug("Retrieving device info..");

            //starting background task
            Task f = Task.Factory.StartNew(async () =>
            {
                var net_mode_list = Get("api/net/net-mode-list");
                var selected_mode = net_mode_list.SelectSingleNode("//response/LTEBandList/LTEBand");

                foreach (XmlNode nodes in selected_mode)
                {
                    foreach (XmlNode node in nodes)
                    {
                        if (node.Value.Contains("LTE BC"))
                        {
                            var value = node.Value;
                            var val = value.Split('/');
                            foreach (var item in val)
                            {
                                LteBand.Add(item.Replace("LTE BC", ""));
                            }
                            textBoxSupportedBand.Text = value.Replace("LTE BC", "");
                        }
                    }
                }
                LteBand.ToArray();
                foreach (var items in LteBand)
                {
                    checkedListBox1.Items.Add(items);
                }

                //display on program
                GetInfo();
                LogDebug("Retrieving device setting..");
                GetDeviceSetting();
                LogDebug("Retrieving MAC filter..");
                MacFilter();
                LogDebug("Retrieving connected client device..");
                GetConnectedDevice();

                //loop to monitor traffic
                LogDebug("Monitoring network traffic started.");

                while (true)
                {
                    if (PingServer() == false)
                    {
                        buttonLogin.Text = "Login";
                        LogDebug("Monitoring network traffic stopped.");
                        ShowMsgBoxError("Disconnected from " + textBoxIP.Text);
                        this.Invoke((MethodInvoker)delegate
                        {
                            metroTabControl1.Enabled = false;
                        });
                        break;
                    }

                    if (loginState() == true || restartApplication == true)
                    {

                        GetTraffic();
                        
                    }
                    else
                    {
                        backgroundWorkerDeviceInfo.CancelAsync();
                        LogDebug("Monitoring network traffic stopped.");
                        break;
                    }
                }
            });

            
        }

        private void net_mode()
        {
            var net_mode = Get("api/net/net-mode");

            string NetworkMode = net_mode.SelectSingleNode("//response/NetworkMode").InnerText;
            if (NetworkMode != "")
            {
                textBoxNetworkMode.Text = NetworkMode.ToString();
            }
            else
            {
                textBoxNetworkMode.Text = "NA";
            }

            string NetworkBand = net_mode.SelectSingleNode("//response/NetworkBand").InnerText;
            if (NetworkBand != "")
            {
                textBoxNetworkband.Text = NetworkBand.ToString();
            }
            else
            {
                textBoxNetworkband.Text = "NA";
            }

            string LTEBand = net_mode.SelectSingleNode("//response/LTEBand").InnerText;
            if (LTEBand != "")
            {
                textBoxLTE_Band.Text = LTEBand.ToString();
            }
            else
            {
                textBoxLTE_Band.Text = "NA";
            }


        }



        private void saveDeviceInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void backgroundWorkerLetMeSeeUrs_DoWork(object sender, DoWorkEventArgs e)
        {

        }


        private void GetWLANSetting()
        {
            LogDebug("Retrieving WLAN basic setting..");

            var basic_settings = Get("api/wlan/basic-settings");
            string basic = xmlformat.Beautify(basic_settings);

            File.WriteAllText(path + @"basic_settings.XML", basic);
            var basic_lines = File.ReadAllLines(path + @"basic_settings.XML");
            File.WriteAllLines(path + @"basic_settings.XML", basic_lines.Skip(1).ToArray());

            DataSet ds_basic = new DataSet();
            ds_basic.ReadXml(path + @"basic_settings.XML");

            if (ds_basic.Tables.Contains("response"))
            {
                foreach (DataRow dr in ds_basic.Tables["response"].Rows)

                {


                    textBoxSSID.Text = dr["WifiSsid"].ToString();

                    if (dr["WifiHide"].ToString() == "1")
                    {
                        checkBoxEnableSSID.Checked = true;
                    }
                    else
                    {
                        checkBoxEnableSSID.Checked = false;
                    }

                }
            }
            else
            {
                LogDebug("Failed to retrieve WLAN basic setting : ERROR " + basic_settings.SelectSingleNode("//error/code").InnerText.ToString() + " [" + ((ErrorCode)(int.Parse(basic_settings.SelectSingleNode("//error/code").InnerText))).ToString() + "]");

            }

            LogDebug("Retrieving WLAN security setting..");

            var WLAN_security_settings = Get("api/wlan/security-settings");
            string WLAN_security = xmlformat.Beautify(WLAN_security_settings);
            File.WriteAllText(path + @"security_settings.XML", WLAN_security);
            var security_lines = File.ReadAllLines(path + @"security_settings.XML");
            File.WriteAllLines(path + @"security_settings.XML", security_lines.Skip(1).ToArray());

            DataSet ds_security = new DataSet();
            ds_security.ReadXml(path + @"security_settings.XML");

            if (ds_security.Tables.Contains("response"))
            {
                foreach (DataRow dr in ds_security.Tables["response"].Rows)

                {

                    if (dr["WifiAuthmode"].ToString() == "OPEN")
                    {
                        comboBoxWLANSec.SelectedItem = "OPEN";
                    }
                    else if (dr["WifiAuthmode"].ToString() == "WPA2-PSK")
                    {
                        comboBoxWLANSec.SelectedItem = "WPA2-PSK";
                    }
                    else if (dr["WifiAuthmode"].ToString() == "WPA/WPA2-PSK")
                    {
                        comboBoxWLANSec.SelectedItem = "WPA/WPA2-PSK";
                    }

                    textBoxPre_shared.Text = dr["WifiWpapsk"].ToString();

                }
            }
            else
            {
                LogDebug("Faile to retrieve WLAN security setting : ERROR " + WLAN_security_settings.SelectSingleNode("//error/code").InnerText.ToString() + " [" + ((ErrorCode)(int.Parse(WLAN_security_settings.SelectSingleNode("//error/code").InnerText))).ToString() + "]");
            }
        }

        private void Ping()
        {
            if(comboBox2.SelectedItem != null)
            {
                var resultList = new List<long>(1);
                var pingSender = new Ping();
                var options = new PingOptions();
                options.DontFragment = true;
                byte[] sendData = Encoding.ASCII.GetBytes(new string('a', 32));
                var timeout = 2048;

                lock (lockObject)
                {
                    Console.WriteLine();
                    LogDebug("\n[*] Pinging : " + comboBox2.Text); 
                    resultList.Clear();
                    string host = comboBox2.SelectedItem.ToString();

                    PingReply reply = pingSender.Send(host, timeout, sendData, options);
                    switch (reply.Status)
                    {
                        case IPStatus.Success:
                            resultList.Add(reply.RoundtripTime);
                            break;
                        case IPStatus.TimedOut:
                        case IPStatus.TimeExceeded:
                        case IPStatus.TtlExpired:
                        case IPStatus.DestinationUnreachable:
                        case IPStatus.DestinationHostUnreachable:
                        case IPStatus.DestinationPortUnreachable:
                        case IPStatus.DestinationNetworkUnreachable:
                        case IPStatus.DestinationProtocolUnreachable:
                            resultList.Add(timeout);
                            break;
                    }

                    var average = resultList.Sum() / resultList.Count;
                    if (resultList.Count > 0)
                    {
                        textBox7.Text = average.ToString() + "ms";
                        LogDebug("\n[*] Ping result : " + average.ToString());

                    }
                    else
                    {

                        textBox7.Text = "Error";
                        LogError("\n[*] Ping error");

                    }


                }
            }
            else
            {

            }
        
            
            


        }

        private async Task MacFilter()
        {

            //get session id n token
            var Sestoken = Get("api/webserver/SesTokInfo");
            _CurrentSessionID = Sestoken.SelectSingleNode("//response/SesInfo").InnerText;
            _CurrentToken = Sestoken.SelectSingleNode("//response/TokInfo").InnerText;

            var API = Get_new("api/wlan/mac-filter");
            if(API.OuterXml.ToString() != string.Empty)
            {
                if (API.OuterXml.ToString().Contains("response"))
                {
                    string test = xmlformat.Beautify(API);

                    //richTextBox2.Text = richTextBox1.Text;
                    File.WriteAllText(path + @"MACFilter.XML", test);
                    var lines = File.ReadAllLines(path + @"MACFilter.XML");
                    File.WriteAllLines(path + @"MACFilter.XML", lines.Skip(1).ToArray());

                    DataSet ds = new DataSet();

                    ds.ReadXml(path + @"MACFilter.XML");



                    if (ds.Tables.Contains("response"))
                    {
                        foreach (DataRow dr in ds.Tables["response"].Rows)

                        {

                            textBoxBlockMAC0.Text = dr["WifiMacFilterMac0"].ToString();
                            textBoxBlockMAC1.Text = dr["WifiMacFilterMac1"].ToString();
                            textBoxBlockMAC2.Text = dr["WifiMacFilterMac2"].ToString();
                            textBoxBlockMAC3.Text = dr["WifiMacFilterMac3"].ToString();
                            textBoxBlockMAC4.Text = dr["WifiMacFilterMac4"].ToString();
                            textBoxBlockMAC5.Text = dr["WifiMacFilterMac5"].ToString();
                            textBoxBlockMAC6.Text = dr["WifiMacFilterMac6"].ToString();
                            textBoxBlockMAC7.Text = dr["WifiMacFilterMac7"].ToString();
                            textBoxBlockMAC8.Text = dr["WifiMacFilterMac8"].ToString();
                            textBoxBlockMAC9.Text = dr["WifiMacFilterMac9"].ToString();
                        }

                    }
                  
                }
                else if (API.OuterXml.ToString().Contains("error"))
                {
                    LogDebug("Failed to retrieve MAC filter : ERROR " + API.SelectSingleNode("//error/code").InnerText.ToString() + " [" + ((ErrorCode)(int.Parse(API.SelectSingleNode("//error/code").InnerText))).ToString() + "]");

                }

            }
            else
            {
                LogDebug("Failed to retrieve MAC filter.");
            }
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
           
            if (backgroundWorkerPerformanceTool.IsBusy)
            {
                backgroundWorkerPerformanceTool.CancelAsync();
                backgroundWorkerPerformanceTool.RunWorkerAsync("Ping");
            }
            else
            {
                backgroundWorkerPerformanceTool.RunWorkerAsync("Ping");

            }
        }
        
        private void backgroundWorkerBandTool_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            button2G.Enabled = true;
            button3G.Enabled = true;
            button4G.Enabled = true;
            buttonApplyBand.Enabled = true;
            buttonAuto.Enabled = true;
            buttonGetCurrentStat.Enabled = true;
        }


        private void backgroundWorkerAPI_DoWork(object sender, DoWorkEventArgs e)
        {
            runAPI();
        }

        private void runAPI()
        {
            //var API_VER = Get("api/device/api-version");
            richTextBox2.Text = "";
            richTextBox1.Text = "";
            if (comboBoxAPIList.SelectedItem != null)
            {
                buttonpostAPI.Enabled = true;

                LogDebug("Sending HTTP GET request to " + comboBoxAPIList.Text);

                XmlDocument APItype;

                APItype = Get_API(comboBoxAPIList.Text);

                if(APItype == null)
                {
                    richTextBox1.Text = "ERROR : RESPONSE_CONTAIN_EMPTY_DATA";
                    richTextBox2.Text = "";
                    richTextBox2.Text = "";
                    LogDebug("Request failed : ERROR [RESPONSE_CONTAIN_EMPTY_DATA]");
                    return;
                }


                if (!string.IsNullOrEmpty(APItype.OuterXml.ToString()))
                {
                    richTextBox1.Text = xmlformat.Beautify(APItype);

                    richTextBox1.Lines = richTextBox1.Lines.Skip(1).ToArray();
                    richTextBox2.Text = richTextBox1.Text.Replace("<response>", "<request>").Replace("</response>", "</request>");
                    runAPIstatus = "1";

                    if (APItype.OuterXml.ToString().Contains("error"))
                    {
                        richTextBox1.Text = "ERROR : " + ((ErrorCode)(int.Parse(APItype.SelectSingleNode("//error/code").InnerText))).ToString();
                        richTextBox2.Text = "";
                        LogDebug("Request failed : ERROR " + APItype.SelectSingleNode("//error/code").InnerText.ToString() + " [" + ((ErrorCode)(int.Parse(APItype.SelectSingleNode("//error/code").InnerText))).ToString() + "]");
                    }
                    else
                    {
                        LogDebug("Request accepted.");
                    }
                }
                else
                {
                    richTextBox1.Text = "ERROR : RESPONSE_CONTAIN_EMPTY_DATA";
                    richTextBox2.Text = "";
                    richTextBox2.Text = "";
                    LogDebug("Request failed : ERROR [RESPONSE_CONTAIN_EMPTY_DATA]");
                }

                APItype.RemoveAll();
                GC.Collect();

            }
            else
                return;
            
        }

        

       

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                if (Directory.Exists(Path.GetTempPath() + @"HuaweiRouterTool\"))
                {
                    Directory.Delete(path, true);
                }
            }
            catch
            {

            }
           







            //Environment.Exit(1);
        }


        private void backgroundWorkerAPI_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

            richTextBox2.Text = richTextBox1.Text;
        }

        private void backgroundWorkerSpeedtest_DoWork(object sender, DoWorkEventArgs e)
        {
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            groupBox2.Enabled = false;
            groupBox3.Enabled = false;

            try
            {
                double downloadSpeed = 0, uploadSpeed = 0;
                Server server = null;
                var time = DateTime.Now;
                PerformTest(out downloadSpeed, out uploadSpeed, out server);
                LogDebug("\n[*] Speed test result : Download : " + textBox5.Text + " | Upload : " + textBox6.Text);

            }
            catch
            {

            }
            


        }

        private void backgroundWorkerSpeedtest_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
          
            groupBox2.Enabled = true;
            groupBox3.Enabled = true;
        }

      

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.Text != "")
            {
                button3.Enabled = true;
            }
        }

        private void richTextBoxLog_TextChanged(object sender, EventArgs e)
        {
            richTextBoxLog.SelectionStart = richTextBoxLog.Text.Length;
            // scroll it automatically
            richTextBoxLog.ScrollToCaret();
        }

        private void CopyAll_Click(Object sender, System.EventArgs e)
        {
            ListView.SelectedIndexCollection sel = listView1.SelectedIndices;
            if(sel.Count == 0)
            {
                ShowMsgBoxError("Please select a device");
            }
            else
            {
                ListViewItem selItem = listView1.Items[sel[0]];
                Clipboard.SetText("Host Name       : " + selItem.SubItems[0].Text + Environment.NewLine + "MAC Address     : " + selItem.SubItems[1].Text + Environment.NewLine + "Vendor          : " + selItem.SubItems[2].Text + Environment.NewLine + "IP Address      : " + selItem.SubItems[3].Text + Environment.NewLine + "Associated Time : " + selItem.SubItems[4].Text + " seconds" + Environment.NewLine + "Associated SSID : " + selItem.SubItems[5].Text);
            }
        }

        private void CopyMAC_Click(Object sender, System.EventArgs e)
        {
            ListView.SelectedIndexCollection sel = listView1.SelectedIndices;
            if (sel.Count == 0)
            {
                ShowMsgBoxError("Please select a device");
            }
            else
            {
                ListViewItem selItem = listView1.Items[sel[0]];
                Clipboard.SetText(selItem.SubItems[1].Text);
            }
        }

        static async Task<string> LookupMac(string MacAddress)
        {
            var uri = new Uri("http://api.macvendors.com/" + WebUtility.UrlEncode(MacAddress));
            using (var wc = new HttpClient())
                return await wc.GetStringAsync(uri);
        }

        private void backgroundWorkerCOnnectedDeviceAndMacFilter_DoWork(object sender, DoWorkEventArgs e)
        {
            GetConnectedDevice();
            //MacFilter();
            //GetWLANSetting();
        }

        private void textBoxSIP_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
         (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void comboBoxAPIList_SelectedIndexChanged(object sender, EventArgs e)
        {
            runAPI();
        }


        private void buttonApplyWLAN_Click(object sender, EventArgs e)
        {
            var isSuccessBasic = "";
            var isSuccessSecurity = "";

            try
            {
                LogDebug("\n[*] Applying WLAN security setting..");
                string WLAN_security;

                WLAN_security = "<?xml version:\"1.0\" encoding=\"UTF-8\"?><request><WifiAuthmode>" + comboBoxWLANSec.Text + "</WifiAuthmode><WifiBasicencryptionmodes>NONE</WifiBasicencryptionmodes><WifiWpaencryptionmodes>AES</WifiWpaencryptionmodes><WifiWepKey1></WifiWepKey1><WifiWepKey2></WifiWepKey2><WifiWepKey3></WifiWepKey3><WifiWepKey4></WifiWepKey4><WifiWepKeyIndex>1</WifiWepKeyIndex><WifiWpapsk>" + textBoxPre_shared.Text + "</WifiWpapsk><WifiWpsenbl>1</WifiWpsenbl><WifiWpscfg>0</WifiWpscfg><WifiRestart>0</WifiRestart></request>";
                var WLANdoc = Post("api/wlan/security-settings", WLAN_security);
                if (WLANdoc.OuterXml == "<?xml version=\"1.0\" encoding=\"UTF-8\"?><response>OK</response>")
                {
                    isSuccessSecurity = "1";

                    LogDebug("\n[*] security setting applied");
                }
                else
                {

                    LogDebug("\n[*] Request failed.");
                }

                string isEnabledSSID;
                if (checkBoxEnableSSID.Checked)
                {
                    isEnabledSSID = "1";
                }
                else
                {
                    isEnabledSSID = "0";
                }

                LogDebug("\n[*] Applying WLAN basic setting..");
                string data;

                data = "<?xml version:\"1.0\" encoding=\"UTF-8\"?><request><WifiSsid>" + textBoxSSID.Text + "</WifiSsid><WifiChannel>0</WifiChannel><WifiHide>" + isEnabledSSID.ToString() + "</WifiHide><WifiCountry>MY</WifiCountry><WifiMode>b/g/n</WifiMode><WifiRate>0</WifiRate><WifiTxPwrPcnt>100</WifiTxPwrPcnt><WifiMaxAssoc>10</WifiMaxAssoc><WifiEnable>1</WifiEnable><WifiFrgThrshld>2346</WifiFrgThrshld><WifiRtsThrshld>2347</WifiRtsThrshld><WifiDtmIntvl>1</WifiDtmIntvl><WifiBcnIntvl>100</WifiBcnIntvl><WifiWme>1</WifiWme><WifiPamode>0</WifiPamode><WifiIsolate>0</WifiIsolate><WifiProtectionmode>1</WifiProtectionmode><Wifioffenable>1</Wifioffenable><Wifiofftime>600</Wifiofftime><wifibandwidth>20</wifibandwidth><wifiautocountryswitch>1</wifiautocountryswitch><wifiantennanum>2</wifiantennanum><wifiguestofftime>0</wifiguestofftime><WifiRestart>1</WifiRestart></request>";

                var WLANdoc2 = Post("api/wlan/basic-settings", data);
                if (WLANdoc2.OuterXml == "<?xml version=\"1.0\" encoding=\"UTF-8\"?><response>OK</response>")
                {
                    isSuccessBasic = "1";

                    LogDebug("\n[*] basic setting applied");
                }
                else
                {

                    LogDebug("\n[*] Request failed.");
                }

                if(isSuccessBasic == "1" && isSuccessSecurity == "1")
                {
                    MessageBox.Show("Setting applied", "Huawei Router Tool", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else
                {
                    MessageBox.Show("Request failed", "Huawei Router Tool", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }

            }
            catch
            {
                MessageBox.Show("An error occured", "Huawei Router Tool", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }


        }

        private bool loginUser()
        {
            _sessionID = "";
            _token = "";
            _requestToken = "";
            _requestTokenOne = "";
            _requestTokenTwo = "";
            _sessionCookie = "";
            Initialise();

            //LogDebug("Generating authentication hashes..");
            authinfo = SHA256andB64(textBoxUsername.Text + SHA256andB64(textBoxPassword.Text) + _requestToken);
            logininfo = string.Format("<?xml version=\"1.0\" encoding=\"UTF-8\"?><request><Username>{0}</Username><Password>{1}</Password><password_type>4</password_type>", textBoxUsername.Text, authinfo);

            LogDebug("Attempting to log in..");

            login = Post("api/user/login", logininfo);

            if (login.ToString() == string.Empty)
            {
                ShowMsgBoxError("No response.");
                LogDebug("No response.");
                return false;
            }
            else if (login.OuterXml.ToString().Contains("error"))
            {
                ShowMsgBoxError("Request failed : ERROR " + ((ErrorCode)(int.Parse(login.SelectSingleNode("//error/code").InnerText))).ToString());
                LogDebug("Request failed : ERROR " + login.SelectSingleNode("//error/code").InnerText.ToString() + " [" + ((ErrorCode)(int.Parse(login.SelectSingleNode("//error/code").InnerText))).ToString() + "]");
                return false;
            }
            else
                return true;
        }


        private bool loginState()
        {
            

            XmlDocument checkLoginState;
            checkLoginState = Get_API("api/user/state-login");

            if (checkLoginState.OuterXml.ToString().Contains("<State>0</State>"))
                return true;
            else
                return false;

        }

      

        private bool logout()
        {
            LogDebug("Attempting to log out..");


            var data = "<?xml version=\"1.0\" encoding=\"UTF-8\"?><request><Logout>1</Logout></request>";
            XmlDocument logout = PostSMS("api/user/logout", data);

            if (logout.ToString() == string.Empty)
            {
                ShowMsgBoxError("No response.");
                LogDebug("No response.");
                return false;
            }
            else if (logout.OuterXml.ToString().Contains("error"))
            {
                ShowMsgBoxError("Request failed : ERROR " + ((ErrorCode)(int.Parse(logout.SelectSingleNode("//error/code").InnerText))).ToString());
                LogDebug("Request failed : ERROR " + logout.SelectSingleNode("//error/code").InnerText.ToString() + " [" + ((ErrorCode)(int.Parse(logout.SelectSingleNode("//error/code").InnerText))).ToString() + "]");
                return false;
            }
            else
                return true;
        }






        private void ButtonLogin_Click_3(object sender, EventArgs e)
        {
            if(textBoxIP.Text == string.Empty || textBoxUsername.Text == string.Empty || textBoxPassword.Text == string.Empty)
            { ShowMsgBoxError("Please specify IP address, username and password"); return; }

            if (buttonLogin.Text == "Login")
            {
                if (PingServer() == false)
                {
                    ShowMsgBoxError("An error occur while connecting to " + textBoxIP.Text);
                    return;
                }

                //if already logged in, return
                if (loginState() == true)
                {
                    ShowMsgBoxInfo("Already logged in.");
                    metroTabControl1.Enabled = true;
                    buttonLogin.Text = "Logout";

                }
                else
                {
                    if (loginUser() == false)
                        return;

                    buttonLogin.Text = "Logout";

                    //save credential
                    if (checkBoxRememberUserpass.Checked)
                    {
                        Huawei_Router_Tool_GUI.Properties.Settings.Default.Username = textBoxUsername.Text;
                        Huawei_Router_Tool_GUI.Properties.Settings.Default.Password = textBoxPassword.Text;
                        Huawei_Router_Tool_GUI.Properties.Settings.Default.IPAddress = textBoxIP.Text;
                        Huawei_Router_Tool_GUI.Properties.Settings.Default.Checkbox = checkBoxRememberUserpass.CheckState;
                        Huawei_Router_Tool_GUI.Properties.Settings.Default.AutoLogin = autoLoginChk.Checked;
                        Huawei_Router_Tool_GUI.Properties.Settings.Default.Save();
                    }
                    else
                    {
                        Huawei_Router_Tool_GUI.Properties.Settings.Default.Reset();
                    }

                    LogDebug("Login successful.");


                    if (backgroundWorkerDeviceInfo.IsBusy != true)
                        backgroundWorkerDeviceInfo.RunWorkerAsync();



                }




                ip_webpage = textBoxIP.Text;
              
            }
            else
            {
                if (logout() == false)
                    return;

                buttonLogin.Text = "Login";
                ShowMsgBoxInfo("Logged out.");
                LogDebug("Logged out.");
                this.Invoke((MethodInvoker)delegate
                {
                    metroTabControl1.Enabled = false;
                });

            }

        }



        private bool rebootRouter()
        {
            var reboot = "<?xml version:\"1.0\" encoding=\"UTF-8\"?><request><Control>1</Control></request>";
            LogDebug("Requesting router to reboot...");
            var REBOOT_doc = PostSMS("api/device/control", reboot);
            if (REBOOT_doc.ToString() == string.Empty)
            {
                ShowMsgBoxError("No response.");
                LogDebug("No response.");
                return false;
            }
            else if (REBOOT_doc.OuterXml.ToString().Contains("error"))
            {
                ShowMsgBoxError("Request failed : ERROR " + ((ErrorCode)(int.Parse(REBOOT_doc.SelectSingleNode("//error/code").InnerText))).ToString());
                LogDebug("Request failed : ERROR " + REBOOT_doc.SelectSingleNode("//error/code").InnerText.ToString() + " [" + ((ErrorCode)(int.Parse(REBOOT_doc.SelectSingleNode("//error/code").InnerText))).ToString() + "]");
                return false;
            }

            return true;
        }

        private void ButtonReboot_Click_1(object sender, EventArgs e)
        {
            if (PingServer() == false)
            {
                ShowMsgBoxError("An error occur while connecting to " + textBoxIP.Text);
                return;
            }

            if (loginState() == true)
            {
                DialogResult dialog = MessageBox.Show("Reboot router?", "Huawei Router Tool", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if(dialog == DialogResult.OK)
                {
                    if (rebootRouter() == false)
                        return;
                    else
                    {
                        logout();
                        LogDebug("Rebooting..");
                        ShowMsgBoxInfo("Rebooting..");
                        buttonLogin.Text = "Login";
                        metroTabControl1.Enabled = false;
                        
                    }
                }
            }
            else { ShowMsgBoxError("Not logged in."); return; }
        }

        private bool shutdownRouter() 
        {
            var shutdown_data = "<?xml version:\"1.0\" encoding=\"UTF-8\"?><request><Control>4</Control></request>";
            LogDebug("Requesting router to shutdown..");
            var shutdown = PostSMS("api/device/control", shutdown_data);

            if(shutdown.ToString() == string.Empty)
            {
                ShowMsgBoxError("No response.");
                LogDebug("No response.");
                return false;
            }
            else if (shutdown.OuterXml.ToString().Contains("error"))
            {
                ShowMsgBoxError("Request failed : ERROR " + ((ErrorCode)(int.Parse(shutdown.SelectSingleNode("//error/code").InnerText))).ToString());
                LogDebug("Request failed : ERROR " + shutdown.SelectSingleNode("//error/code").InnerText.ToString() + " [" + ((ErrorCode)(int.Parse(shutdown.SelectSingleNode("//error/code").InnerText))).ToString() + "]");
                return false;
            }

            return true;
        }

        private void ButtonShutdown_Click_1(object sender, EventArgs e)
        {
            if (PingServer() == false)
            {
                ShowMsgBoxError("An error occur while connecting to " + textBoxIP.Text);
                return;
            }

            if (loginState() == true)
            {
                DialogResult dialog = MessageBox.Show("Shutdown router?", "Huawei Router Tool", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (dialog == DialogResult.OK)
                {

                    if (shutdownRouter() == false)
                        return;
                    else
                    {
                        logout();
                        LogDebug("Shutting down..");
                        ShowMsgBoxInfo("Shutting down..");
                        buttonLogin.Text = "Login";
                        metroTabControl1.Enabled = false;

                    }

                }
            }
            else { ShowMsgBoxError("Not logged in."); return; }
        }


        private void MetroButton1_Click(object sender, EventArgs e)
        {
            BackgroundWorker bgw = new BackgroundWorker();
            bgw.WorkerSupportsCancellation = true;
            bgw.DoWork += delegate 
            {
                LogDebug("Retrieving connected client device..");
                GetConnectedDevice();
            };
            bgw.RunWorkerCompleted += delegate 
            {
                bgw.CancelAsync();
            };
            bgw.RunWorkerAsync();
            
        }

        private void Bgw_DoWork(object sender, DoWorkEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Button14_Click_1(object sender, EventArgs e)
        {

        }

        private async void ButtonBlockDevice_Click(object sender, EventArgs e)
        {
            if (textBoxBlockMAC0.Text == "" && textBoxBlockMAC1.Text == "" && textBoxBlockMAC2.Text == "" && textBoxBlockMAC3.Text == "" && textBoxBlockMAC4.Text == "" && textBoxBlockMAC5.Text == "" && textBoxBlockMAC6.Text == "" && textBoxBlockMAC7.Text == "" && textBoxBlockMAC8.Text == "" && textBoxBlockMAC9.Text == "")
            {
                ShowMsgBoxError("Enter at least 1 MAC address");
                return;
            }

            string mac0 = textBoxBlockMAC0.Text;
            string mac1 = textBoxBlockMAC1.Text;
            string mac2 = textBoxBlockMAC2.Text;
            string mac3 = textBoxBlockMAC3.Text;
            string mac4 = textBoxBlockMAC4.Text;
            string mac5 = textBoxBlockMAC5.Text;
            string mac6 = textBoxBlockMAC6.Text;
            string mac7 = textBoxBlockMAC7.Text;
            string mac8 = textBoxBlockMAC8.Text;
            string mac9 = textBoxBlockMAC9.Text;

            Regex r = new Regex("^([0-9a-fA-F]{2}(?:[:-]?[0-9a-fA-F]{2}){5})$");

            if (r.IsMatch(mac0) || r.IsMatch(mac1) || r.IsMatch(mac2) || r.IsMatch(mac3) || r.IsMatch(mac4) || r.IsMatch(mac5) || r.IsMatch(mac6) || r.IsMatch(mac7) || r.IsMatch(mac8) || r.IsMatch(mac9))
            {
                string data = "<request><macfilterimmediatework>1</macfilterimmediatework><WifiMacFilterStatus>2</WifiMacFilterStatus><WifiMacFilterMac0>" + textBoxBlockMAC0.Text + "</WifiMacFilterMac0><wifihostname0></wifihostname0><WifiMacFilterMac1>" + textBoxBlockMAC1.Text + "</WifiMacFilterMac1><wifihostname1></wifihostname1><WifiMacFilterMac2>" + textBoxBlockMAC2.Text + "</WifiMacFilterMac2><wifihostname2></wifihostname2><WifiMacFilterMac3>" + textBoxBlockMAC3.Text + "</WifiMacFilterMac3><wifihostname3></wifihostname3><WifiMacFilterMac4>" + textBoxBlockMAC4.Text + "</WifiMacFilterMac4><wifihostname4></wifihostname4><WifiMacFilterMac5>" + textBoxBlockMAC5.Text + "</WifiMacFilterMac5><wifihostname5></wifihostname5><WifiMacFilterMac6>" + textBoxBlockMAC6.Text + "</WifiMacFilterMac6><wifihostname6></wifihostname6><WifiMacFilterMac7>" + textBoxBlockMAC7.Text + "</WifiMacFilterMac7><wifihostname7></wifihostname7><WifiMacFilterMac8>" + textBoxBlockMAC8.Text + "</WifiMacFilterMac8><wifihostname8></wifihostname8><WifiMacFilterMac9>" + textBoxBlockMAC9.Text + "</WifiMacFilterMac9><wifihostname9></wifihostname9></request>";

                LogDebug("Blocking device(s)..");
                var POST_MAC_doc = PostSMS("api/wlan/mac-filter", data);

                if (POST_MAC_doc == null)
                {
                    ShowMsgBoxError("No response.");
                    LogDebug("No response.");
                    return;
                }
                else if (POST_MAC_doc.ToString().Contains("ok"))
                {
                    ShowMsgBoxInfo("Request accepted.");
                    LogDebug("Request accepted.");
                }
                else if (POST_MAC_doc.OuterXml.ToString().Contains("error"))
                {
                    ShowMsgBoxError("Request failed : ERROR " + ((ErrorCode)(int.Parse(POST_MAC_doc.SelectSingleNode("//error/code").InnerText))).ToString());
                    LogDebug("Request failed : ERROR " + POST_MAC_doc.SelectSingleNode("//error/code").InnerText.ToString() + " [" + ((ErrorCode)(int.Parse(POST_MAC_doc.SelectSingleNode("//error/code").InnerText))).ToString() + "]");
                }
            }
            else
            {
                ShowMsgBoxError("MAC address not valid.");
            }

            await MacFilter();
        }

        private void ButtonUnblockAll_Click(object sender, EventArgs e)
        {
            string data = "<request><macfilterimmediatework>1</macfilterimmediatework><WifiMacFilterStatus>0</WifiMacFilterStatus><WifiMacFilterMac0></WifiMacFilterMac0><wifihostname0></wifihostname0><WifiMacFilterMac1></WifiMacFilterMac1><wifihostname1></wifihostname1><WifiMacFilterMac2></WifiMacFilterMac2><wifihostname2></wifihostname2><WifiMacFilterMac3></WifiMacFilterMac3><wifihostname3></wifihostname3><WifiMacFilterMac4></WifiMacFilterMac4><wifihostname4></wifihostname4><WifiMacFilterMac5></WifiMacFilterMac5><wifihostname5></wifihostname5><WifiMacFilterMac6></WifiMacFilterMac6><wifihostname6></wifihostname6><WifiMacFilterMac7></WifiMacFilterMac7><wifihostname7></wifihostname7><WifiMacFilterMac8></WifiMacFilterMac8><wifihostname8></wifihostname8><WifiMacFilterMac9></WifiMacFilterMac9><wifihostname9></wifihostname9></request>";

            LogDebug("\n[*] Blocking all device..");
            var POST_MAC_UNBLOCK_doc = PostSMS("api/wlan/mac-filter", data);
            if (POST_MAC_UNBLOCK_doc == null)
            {
                ShowMsgBoxError("No response.");
                LogDebug("No response.");
                return;
            }
            else if (POST_MAC_UNBLOCK_doc.ToString().Contains("ok"))
            {
                ShowMsgBoxInfo("Request accepted.");
                LogDebug("Request accepted.");
            }
            else if (POST_MAC_UNBLOCK_doc.OuterXml.ToString().Contains("error"))
            {
                ShowMsgBoxError("Request failed : ERROR " + ((ErrorCode)(int.Parse(POST_MAC_UNBLOCK_doc.SelectSingleNode("//error/code").InnerText))).ToString());
                LogDebug("Request failed : ERROR " + POST_MAC_UNBLOCK_doc.SelectSingleNode("//error/code").InnerText.ToString() + " [" + ((ErrorCode)(int.Parse(POST_MAC_UNBLOCK_doc.SelectSingleNode("//error/code").InnerText))).ToString() + "]");
            }
        }

        private void CheckBoxDMZ_MouseClick(object sender, MouseEventArgs e)
        {
            string data;

            if (checkBoxDMZ.Checked)
            {
                if (textBoxDMZ.Text == "")
                {
                    ShowMsgBoxError("Enter IP address.");
                    if (checkBoxDMZ.Checked)
                    {
                        checkBoxDMZ.Checked = false;
                    }
                    else
                    {
                        checkBoxDMZ.Checked = true;
                    }
                }
                else
                {
                    LogDebug("Enabling DMZ..");

                    data = "<?xml version:\"1.0\" encoding=\"UTF-8\"?><request><DmzStatus>1</DmzStatus><DmzIPAddress>" + textBoxDMZ.Text + "</DmzIPAddress></request>";
                    var DMZ_doc = PostSMS("api/security/dmz", data);

                    if (DMZ_doc.OuterXml == "<?xml version=\"1.0\" encoding=\"UTF-8\"?><response>OK</response>")
                    {
                        ShowMsgBoxInfo("Request accepted.");
                        LogDebug("Request accepted.");
                    }
                    else
                    {
                        ShowMsgBoxError("Request failed : ERROR " + DMZ_doc.SelectSingleNode("//error/code").InnerText.ToString() + " [" + ((ErrorCode)(int.Parse(DMZ_doc.SelectSingleNode("//error/code").InnerText))).ToString() + "]");
                        LogDebug("Request failed : ERROR " + DMZ_doc.SelectSingleNode("//error/code").InnerText.ToString() + " [" + ((ErrorCode)(int.Parse(DMZ_doc.SelectSingleNode("//error/code").InnerText))).ToString() + "]");
                        checkBoxDMZ.Checked = false;
                    }
                }
            }
            else
            {
                LogDebug("Disabling DMZ..");

                data = "<?xml version:\"1.0\" encoding=\"UTF-8\"?><request><DmzStatus>0</DmzStatus><DmzIPAddress>" + textBoxDMZ.Text + "</DmzIPAddress></request>";
                var DMZ_doc = PostSMS("api/security/dmz", data);
                if (DMZ_doc.OuterXml == "<?xml version=\"1.0\" encoding=\"UTF-8\"?><response>OK</response>")
                {
                    ShowMsgBoxInfo("Request accepted.");
                    LogDebug("Request accepted.");
                }
                else
                {
                    ShowMsgBoxError("Request failed : ERROR " + DMZ_doc.SelectSingleNode("//error/code").InnerText.ToString() + " [" + ((ErrorCode)(int.Parse(DMZ_doc.SelectSingleNode("//error/code").InnerText))).ToString() + "]");
                    LogDebug("Request failed : ERROR " + DMZ_doc.SelectSingleNode("//error/code").InnerText.ToString() + " [" + ((ErrorCode)(int.Parse(DMZ_doc.SelectSingleNode("//error/code").InnerText))).ToString() + "]");
                    checkBoxDMZ.Checked = true;
                }
            }
        }

        private void CheckBoxMobileConnection_MouseClick(object sender, MouseEventArgs e)
        {
            string data;

            if (checkBoxMobileConnection.Checked)
            {
                LogDebug("Enabling mobile connection..");

                data = "<?xml version:\"1.0\" encoding=\"UTF-8\"?><request><dataswitch>1</dataswitch></request>";
                var MOBILE_CONNECTION_doc = PostSMS("api/dialup/mobile-dataswitch", data);
                if (MOBILE_CONNECTION_doc.OuterXml == "<?xml version=\"1.0\" encoding=\"UTF-8\"?><response>OK</response>")
                {
                    ShowMsgBoxInfo("Request accepted.");
                    LogDebug("Request accepted.");
                }
                else
                {
                    ShowMsgBoxError("Request failed : ERROR " + MOBILE_CONNECTION_doc.SelectSingleNode("//error/code").InnerText.ToString() + " [" + ((ErrorCode)(int.Parse(MOBILE_CONNECTION_doc.SelectSingleNode("//error/code").InnerText))).ToString() + "]");
                    LogDebug("Request failed : ERROR " + MOBILE_CONNECTION_doc.SelectSingleNode("//error/code").InnerText.ToString() + " [" + ((ErrorCode)(int.Parse(MOBILE_CONNECTION_doc.SelectSingleNode("//error/code").InnerText))).ToString() + "]");
                    checkBoxMobileConnection.Checked = false;
                }
            }
            else
            {
                LogDebug("Disabling mobile connection..");

                data = "<?xml version:\"1.0\" encoding=\"UTF-8\"?><request><dataswitch>0</dataswitch></request>";
                var MOBILE_CONNECTION_doc = PostSMS("api/dialup/mobile-dataswitch", data);
                if (MOBILE_CONNECTION_doc.OuterXml == "<?xml version=\"1.0\" encoding=\"UTF-8\"?><response>OK</response>")
                {
                    ShowMsgBoxInfo("Request accepted.");
                    LogDebug("Request accepted.");
                }
                else
                {
                    ShowMsgBoxError("Request failed : ERROR " + MOBILE_CONNECTION_doc.SelectSingleNode("//error/code").InnerText.ToString() + " [" + ((ErrorCode)(int.Parse(MOBILE_CONNECTION_doc.SelectSingleNode("//error/code").InnerText))).ToString() + "]");
                    LogDebug("Request failed : ERROR " + MOBILE_CONNECTION_doc.SelectSingleNode("//error/code").InnerText.ToString() + " [" + ((ErrorCode)(int.Parse(MOBILE_CONNECTION_doc.SelectSingleNode("//error/code").InnerText))).ToString() + "]");
                    checkBoxMobileConnection.Checked = true;
                }
            }
        }

        private void CheckBoxUPnP_MouseClick(object sender, MouseEventArgs e)
        {
            string data;

            if (checkBoxUPnP.Checked)
            {
                LogDebug("Enabling UPnP..");


                data = "<?xml version:\"1.0\" encoding=\"UTF-8\"?><request><UpnpStatus>1</UpnpStatus></request>";
                var UPnP_doc = PostSMS("api/security/upnp", data);
                if (UPnP_doc.OuterXml == "<?xml version=\"1.0\" encoding=\"UTF-8\"?><response>OK</response>")
                {
                    ShowMsgBoxInfo("Request accepted.");
                    LogDebug("Request accepted.");
                }
                else
                {
                    ShowMsgBoxError("Request failed : ERROR " + UPnP_doc.SelectSingleNode("//error/code").InnerText.ToString() + " [" + ((ErrorCode)(int.Parse(UPnP_doc.SelectSingleNode("//error/code").InnerText))).ToString() + "]");
                    LogDebug("Request failed : ERROR " + UPnP_doc.SelectSingleNode("//error/code").InnerText.ToString() + " [" + ((ErrorCode)(int.Parse(UPnP_doc.SelectSingleNode("//error/code").InnerText))).ToString() + "]");
                    checkBoxUPnP.Checked = false;
                }
            }
            else
            {
                LogDebug("Disabling UPnP..");

                data = "<?xml version:\"1.0\" encoding=\"UTF-8\"?><request><UpnpStatus>0</UpnpStatus></request>";
                var UPnP_doc = PostSMS("api/security/upnp", data);
                if (UPnP_doc.OuterXml == "<?xml version=\"1.0\" encoding=\"UTF-8\"?><response>OK</response>")
                {
                    ShowMsgBoxInfo("Request accepted.");
                    LogDebug("Request accepted.");
                }
                else
                {

                    ShowMsgBoxError("Request failed : ERROR " + UPnP_doc.SelectSingleNode("//error/code").InnerText.ToString() + " [" + ((ErrorCode)(int.Parse(UPnP_doc.SelectSingleNode("//error/code").InnerText))).ToString() + "]");
                    LogDebug("Request failed : ERROR " + UPnP_doc.SelectSingleNode("//error/code").InnerText.ToString() + " [" + ((ErrorCode)(int.Parse(UPnP_doc.SelectSingleNode("//error/code").InnerText))).ToString() + "]");
                    checkBoxUPnP.Checked = true;
                }
            }
        }

        private void CheckBoxNAT_MouseClick(object sender, MouseEventArgs e)
        {
            string data;

            if (checkBoxNAT.Checked)
            {
                LogDebug("Enabling NAT..");

                data = "<?xml version:\"1.0\" encoding=\"UTF-8\"?><request><NATType>1</NATType></request>";
                var NAT_doc = PostSMS("api/security/nat", data);
                if (NAT_doc.OuterXml == "<?xml version=\"1.0\" encoding=\"UTF-8\"?><response>OK</response>")
                {
                    ShowMsgBoxInfo("Request accepted.");
                    LogDebug("Request accepted.");
                }
                else
                {
                    ShowMsgBoxError("Request failed : ERROR " + NAT_doc.SelectSingleNode("//error/code").InnerText.ToString() + " [" + ((ErrorCode)(int.Parse(NAT_doc.SelectSingleNode("//error/code").InnerText))).ToString() + "]");
                    LogDebug("Request failed : ERROR " + NAT_doc.SelectSingleNode("//error/code").InnerText.ToString() + " [" + ((ErrorCode)(int.Parse(NAT_doc.SelectSingleNode("//error/code").InnerText))).ToString() + "]");
                    checkBoxNAT.Checked = false;
                }

            }
            else
            {
                LogDebug("Disabling NAT..");

                data = "<?xml version:\"1.0\" encoding=\"UTF-8\"?><request><NATType>0</NATType></request>";
                var NAT_doc = PostSMS("api/security/nat", data);
                if (NAT_doc.OuterXml == "<?xml version=\"1.0\" encoding=\"UTF-8\"?><response>OK</response>")
                {
                    ShowMsgBoxInfo("Request accepted.");
                    LogDebug("Request accepted.");
                }
                else
                {
                    ShowMsgBoxError("Request failed : ERROR " + NAT_doc.SelectSingleNode("//error/code").InnerText.ToString() + " [" + ((ErrorCode)(int.Parse(NAT_doc.SelectSingleNode("//error/code").InnerText))).ToString() + "]");
                    LogDebug("Request failed : ERROR " + NAT_doc.SelectSingleNode("//error/code").InnerText.ToString() + " [" + ((ErrorCode)(int.Parse(NAT_doc.SelectSingleNode("//error/code").InnerText))).ToString() + "]");
                    checkBoxNAT.Checked = true;
                }
            }
        }

        private void CheckBoxSIP_MouseClick(object sender, MouseEventArgs e)
        {
            string data;
            if (checkBoxSIP.Checked)
            {
                if (textBoxSIP.Text == "")
                {
                    ShowMsgBoxError("Enter Port");
                    if (checkBoxSIP.Checked)
                    {
                        checkBoxSIP.Checked = false;
                    }
                    else
                    {
                        checkBoxSIP.Checked = true;

                    }
                }
                else
                {
                    LogDebug("Enabling SIP ALG..");

                    data = "<?xml version:\"1.0\" encoding=\"UTF-8\"?><request><SipStatus>1</SipStatus><SipPort>" + textBoxSIP.Text + "</SipPort></request>";
                    var SIP_doc = PostSMS("api/security/sip", data);
                    if (SIP_doc.OuterXml == "<?xml version=\"1.0\" encoding=\"UTF-8\"?><response>OK</response>")
                    {
                        ShowMsgBoxInfo("Request accepted.");
                        LogDebug("Request accepted.");
                    }
                    else
                    {
                        ShowMsgBoxError("Request failed : ERROR " + SIP_doc.SelectSingleNode("//error/code").InnerText.ToString() + " [" + ((ErrorCode)(int.Parse(SIP_doc.SelectSingleNode("//error/code").InnerText))).ToString() + "]");
                        LogDebug("Request failed : ERROR " + SIP_doc.SelectSingleNode("//error/code").InnerText.ToString() + " [" + ((ErrorCode)(int.Parse(SIP_doc.SelectSingleNode("//error/code").InnerText))).ToString() + "]");
                        checkBoxSIP.Checked = false;
                    }
                }

                
            }
            else
            {
                LogDebug("Disabling SIP ALG..");

                data = "<?xml version:\"1.0\" encoding=\"UTF-8\"?><request><SipStatus>0</SipStatus><SipPort>" + textBoxSIP.Text + "</SipPort></request>";
                var SIP_doc = PostSMS("api/security/sip", data);
                if (SIP_doc.OuterXml == "<?xml version=\"1.0\" encoding=\"UTF-8\"?><response>OK</response>")
                {
                    ShowMsgBoxInfo("Request accepted.");
                    LogDebug("Request accepted.");
                }
                else
                {
                    ShowMsgBoxError("Request failed : ERROR " + SIP_doc.SelectSingleNode("//error/code").InnerText.ToString() + " [" + ((ErrorCode)(int.Parse(SIP_doc.SelectSingleNode("//error/code").InnerText))).ToString() + "]");
                    LogDebug("Request failed : ERROR " + SIP_doc.SelectSingleNode("//error/code").InnerText.ToString() + " [" + ((ErrorCode)(int.Parse(SIP_doc.SelectSingleNode("//error/code").InnerText))).ToString() + "]");
                    checkBoxSIP.Checked = true;
                }
            }
        }

        public bool scram_login()
        {
            XmlDocument _scram_login = Get("api/user/state-login");
            if (_scram_login.SelectSingleNode("//response/extern_password_type") == null)
                return false;

            if (_scram_login.SelectSingleNode("//response/extern_password_type").InnerText == "1" && _scram_login.SelectSingleNode("//response/extern_password_type").InnerText != "undefined")
                return true;

            return false;
        }

        private static string XSSResolveCannotParseChar(string xmlStr)
        {
            if (xmlStr != "undefined" && xmlStr != null && xmlStr != "")
            {
                return xmlStr.Replace("&", "&amp;").Replace("'", "&apos;").Replace("\"", "&quot;").Replace("<", "&lt;").Replace(">", "gt;");
            }

            return xmlStr;
        }

        private void ButtonSaveDevicePW_Click(object sender, EventArgs e)
        {
            if (textBoxCurrentPW.Text == "" || textBoxNewPW.Text == "")
            {
                ShowMsgBoxError("Enter username and password");
            }
            else
            {
                LogDebug("Applying new router password..");

                string data = "";
                XmlDocument DEVICE_PASSWORD_doc;
                if (scram_login() == true)
                {
                    data = "<?xml version:\"1.0\" encoding=\"UTF-8\"?><request><username>" + textBoxUsername.Text + "</username><currentpassword>" + XSSResolveCannotParseChar(textBoxCurrentPW.Text) + "</currentpassword><newpassword>" + XSSResolveCannotParseChar(textBoxNewPW.Text) + "</newpassword></request> ";
                    DEVICE_PASSWORD_doc = PostSMS("api/user/password_scram", data);
                }
                else
                {
                    byte[] encodeCurrentPW = System.Text.Encoding.UTF8.GetBytes(textBoxCurrentPW.Text);
                    byte[] encodeNewPW = System.Text.Encoding.UTF8.GetBytes(textBoxNewPW.Text);

                    // convert the byte array to a Base64 string

                    string encodedCurrentPW = Convert.ToBase64String(encodeCurrentPW);
                    string encodedNewPW = Convert.ToBase64String(encodeNewPW);

                    data = "<?xml version:\"1.0\" encoding=\"UTF-8\"?><request><Username>" + textBoxUsername.Text + "</Username><CurrentPassword>" + encodedCurrentPW + "</CurrentPassword><NewPassword>" + encodedNewPW + "</NewPassword></request> ";
                    DEVICE_PASSWORD_doc = Post("api/user/password", data);
                }

                if (DEVICE_PASSWORD_doc.OuterXml == "<?xml version=\"1.0\" encoding=\"UTF-8\"?><response>OK</response>")
                {
                    //save new password
                    Huawei_Router_Tool_GUI.Properties.Settings.Default.Password = textBoxNewPW.Text;
                    Huawei_Router_Tool_GUI.Properties.Settings.Default.Save();

                    ShowMsgBoxInfo("Request accepted.");
                    LogDebug("Request accepted.");

                    // program freeze while application.restart executed. will fix this later

                    //restartApplication = true;
                    //if (backgroundWorkerDeviceInfo.IsBusy == true)
                    //    backgroundWorkerDeviceInfo.CancelAsync();
                    //System.Threading.Thread.Sleep(2000);

                    Application.Restart();
                }
                else
                {
                    ShowMsgBoxError("Request failed : ERROR " + ((ErrorCode)(int.Parse(DEVICE_PASSWORD_doc.SelectSingleNode("//error/code").InnerText))).ToString());
                    LogDebug("Request failed : ERROR " + DEVICE_PASSWORD_doc.SelectSingleNode("//error/code").InnerText.ToString() + " [" + ((ErrorCode)(int.Parse(DEVICE_PASSWORD_doc.SelectSingleNode("//error/code").InnerText))).ToString() + "]");
                }
            }
        }

        private void CheckBoxFW1_MouseClick(object sender, MouseEventArgs e)
        {
            string data;

            if (checkBoxFW1.Checked)
            {
                LogDebug("Enabling device to update without logging in..");

                data = "<?xml version:\"1.0\" encoding=\"UTF-8\"?><request><whitelist_enable>1</whitelist_enable></request>";
                var FW1_doc = PostSMS("api/webserver/white_list_switch", data);
                if (FW1_doc.OuterXml == "<?xml version=\"1.0\" encoding=\"UTF-8\"?><response>OK</response>")
                {
                    ShowMsgBoxInfo("Request accepted.");
                    LogDebug("Request accepted.");
                }
                else
                {
                    ShowMsgBoxError("Request failed : ERROR " + FW1_doc.SelectSingleNode("//error/code").InnerText.ToString() + " [" + ((ErrorCode)(int.Parse(FW1_doc.SelectSingleNode("//error/code").InnerText))).ToString() + "]");
                    LogDebug("Request failed : ERROR " + FW1_doc.SelectSingleNode("//error/code").InnerText.ToString() + " [" + ((ErrorCode)(int.Parse(FW1_doc.SelectSingleNode("//error/code").InnerText))).ToString() + "]");
                    checkBoxFW1.Checked = false;
                }
            }
            else
            {
                LogDebug("Disabling device to update without logging in..");

                data = "<?xml version:\"1.0\" encoding=\"UTF-8\"?><request><whitelist_enable>0</whitelist_enable></request>";
                var FW1_doc = PostSMS("api/webserver/white_list_switch", data);
                if (FW1_doc.OuterXml == "<?xml version=\"1.0\" encoding=\"UTF-8\"?><response>OK</response>")
                {
                    ShowMsgBoxInfo("Request accepted.");
                    LogDebug("Request accepted.");
                }
                else
                {
                    ShowMsgBoxError("Request failed : ERROR " + FW1_doc.SelectSingleNode("//error/code").InnerText.ToString() + " [" + ((ErrorCode)(int.Parse(FW1_doc.SelectSingleNode("//error/code").InnerText))).ToString() + "]");
                    LogDebug("Request failed : ERROR " + FW1_doc.SelectSingleNode("//error/code").InnerText.ToString() + " [" + ((ErrorCode)(int.Parse(FW1_doc.SelectSingleNode("//error/code").InnerText))).ToString() + "]");
                    checkBoxFW1.Checked = true;
                }
            }
        }

        private void CheckBoxFW2_MouseClick(object sender, MouseEventArgs e)
        {
            string data;

            if (checkBoxFW2.Checked)
            {
                LogDebug("Enabling device to automatically install critical updates (force update)..");

                data = "<?xml version:\"1.0\" encoding=\"UTF-8\"?><request><autoUpdateInterval>0</autoUpdateInterval><auto_update_enable>1</auto_update_enable><server_force_enable>1</server_force_enable></request>";
                var FW2_doc = PostSMS("api/online-update/configuration", data);
                if (FW2_doc.OuterXml == "<?xml version=\"1.0\" encoding=\"UTF-8\"?><response>OK</response>")
                {
                    ShowMsgBoxInfo("Request accepted.");
                    LogDebug("Request accepted.");
                }
                else
                {
                    ShowMsgBoxError("Request failed : ERROR " + FW2_doc.SelectSingleNode("//error/code").InnerText.ToString() + " [" + ((ErrorCode)(int.Parse(FW2_doc.SelectSingleNode("//error/code").InnerText))).ToString() + "]");
                    LogDebug("Request failed : ERROR " + FW2_doc.SelectSingleNode("//error/code").InnerText.ToString() + " [" + ((ErrorCode)(int.Parse(FW2_doc.SelectSingleNode("//error/code").InnerText))).ToString() + "]");
                    checkBoxFW2.Checked = false;
                }
            }
            else
            {
                LogDebug("Disabling device to automatically install critical updates (force update)..");

                data = "<?xml version:\"1.0\" encoding=\"UTF-8\"?><request><autoUpdateInterval>0</autoUpdateInterval><auto_update_enable>1</auto_update_enable><server_force_enable>0</server_force_enable></request>";
                var FW2_doc = PostSMS("api/online-update/configuration", data);
                if (FW2_doc.OuterXml == "<?xml version=\"1.0\" encoding=\"UTF-8\"?><response>OK</response>")
                {
                    ShowMsgBoxInfo("Request accepted.");
                    LogDebug("Request accepted.");
                }
                else
                {
                    ShowMsgBoxError("Request failed : ERROR " + FW2_doc.SelectSingleNode("//error/code").InnerText.ToString() + " [" + ((ErrorCode)(int.Parse(FW2_doc.SelectSingleNode("//error/code").InnerText))).ToString() + "]");
                    LogDebug("Request failed : ERROR " + FW2_doc.SelectSingleNode("//error/code").InnerText.ToString() + " [" + ((ErrorCode)(int.Parse(FW2_doc.SelectSingleNode("//error/code").InnerText))).ToString() + "]");
                    checkBoxFW2.Checked = true;
                }
            }
        }

        private void CheckBoxFW3_MouseClick(object sender, MouseEventArgs e)
        {
            string data;

            if (checkBoxFW3.Checked)
            {
                LogDebug("Enabling auto-download (auto-update after a restart)");

                data = "<?xml version:\"1.0\" encoding=\"UTF-8\"?><request><auto_update>1</auto_update><ui_download>0</ui_download></request>";
                var FW3_doc = PostSMS("api/online-update/autoupdate-config", data);
                if (FW3_doc.OuterXml == "<?xml version=\"1.0\" encoding=\"UTF-8\"?><response>OK</response>")
                {
                    ShowMsgBoxInfo("Request accepted.");
                    LogDebug("Request accepted.");
                }
                else
                {
                    ShowMsgBoxError("Request failed : ERROR " + FW3_doc.SelectSingleNode("//error/code").InnerText.ToString() + " [" + ((ErrorCode)(int.Parse(FW3_doc.SelectSingleNode("//error/code").InnerText))).ToString() + "]");
                    LogDebug("Request failed : ERROR " + FW3_doc.SelectSingleNode("//error/code").InnerText.ToString() + " [" + ((ErrorCode)(int.Parse(FW3_doc.SelectSingleNode("//error/code").InnerText))).ToString() + "]");
                    checkBoxFW3.Checked = false;
                }
            }
            else
            {
                LogDebug("Disabling auto-download (auto-update after a restart)");

                data = "<?xml version:\"1.0\" encoding=\"UTF-8\"?><request><auto_update>0</auto_update><ui_download>0</ui_download></request>";
                var FW3_doc = PostSMS("api/online-update/autoupdate-config", data);
                if (FW3_doc.OuterXml == "<?xml version=\"1.0\" encoding=\"UTF-8\"?><response>OK</response>")
                {
                    ShowMsgBoxInfo("Request accepted.");
                    LogDebug("Request accepted.");
                }
                else
                {
                    ShowMsgBoxError("Request failed : ERROR " + FW3_doc.SelectSingleNode("//error/code").InnerText.ToString() + " [" + ((ErrorCode)(int.Parse(FW3_doc.SelectSingleNode("//error/code").InnerText))).ToString() + "]");
                    LogDebug("Request failed : ERROR " + FW3_doc.SelectSingleNode("//error/code").InnerText.ToString() + " [" + ((ErrorCode)(int.Parse(FW3_doc.SelectSingleNode("//error/code").InnerText))).ToString() + "]");
                    checkBoxFW3.Checked = true;
                }
            }
        }

        private void ComboBoxDeviceMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            string data;

            if (comboBoxDeviceMode.SelectedItem == "Project Mode")
            {
                LogDebug("Changing device mode to Project Mode..");

                data = "<?xml version:\"1.0\" encoding=\"UTF-8\"?><request><mode>0</mode></request>";
                var DEVICE_MODE_doc = PostSMS("api/device/mode", data);
                if (DEVICE_MODE_doc.OuterXml == "<?xml version=\"1.0\" encoding=\"UTF-8\"?><response>OK</response>")
                {
                    ShowMsgBoxInfo("Request accepted.");
                    LogDebug("Request accepted.");
                }
                else
                {
                    ShowMsgBoxError("Request failed : ERROR " + DEVICE_MODE_doc.SelectSingleNode("//error/code").InnerText.ToString() + " [" + ((ErrorCode)(int.Parse(DEVICE_MODE_doc.SelectSingleNode("//error/code").InnerText))).ToString() + "]");
                    LogDebug("Request failed : ERROR " + DEVICE_MODE_doc.SelectSingleNode("//error/code").InnerText.ToString() + " [" + ((ErrorCode)(int.Parse(DEVICE_MODE_doc.SelectSingleNode("//error/code").InnerText))).ToString() + "]");
                }
            }
            else if (comboBoxDeviceMode.SelectedItem == "Debug Mode")
            {
                LogDebug("Changing device mode to Debug Mode..");

                ShowMsgBoxInfo("Internet connection may be disconnected when the debug mode is activated.");
                data = "<?xml version:\"1.0\" encoding=\"UTF-8\"?><request><mode>1</mode></request>";
                var DEVICE_MODE_doc = PostSMS("api/device/mode", data);
                if (DEVICE_MODE_doc.OuterXml == "<?xml version=\"1.0\" encoding=\"UTF-8\"?><response>OK</response>")
                {
                    ShowMsgBoxInfo("Request accepted.");
                    LogDebug("Request accepted.");
                }
                else
                {
                    ShowMsgBoxError("Request failed : ERROR " + DEVICE_MODE_doc.SelectSingleNode("//error/code").InnerText.ToString() + " [" + ((ErrorCode)(int.Parse(DEVICE_MODE_doc.SelectSingleNode("//error/code").InnerText))).ToString() + "]");
                    LogDebug("Request failed : ERROR " + DEVICE_MODE_doc.SelectSingleNode("//error/code").InnerText.ToString() + " [" + ((ErrorCode)(int.Parse(DEVICE_MODE_doc.SelectSingleNode("//error/code").InnerText))).ToString() + "]");
                }
            }
        }

        private void ButtonRestore_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Restore all setting to default?", "Huawei Router Tool", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                DialogResult dialogs = MessageBox.Show("Device will be rebooted. The network and router password will be reset to default. Are you sure?", "Huawei Router Tool", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogs == DialogResult.Yes)
                {
                    LogDebug("Restoring all setting to default..");
                    string data;

                    data = "<?xml version:\"1.0\" encoding=\"UTF-8\"?><request><Control>2</Control></request>";
                    var RESTORE_SETTING_doc = Post("api/device/control", data);
                    if (RESTORE_SETTING_doc.OuterXml == "<?xml version=\"1.0\" encoding=\"UTF-8\"?><response>OK</response>")
                    {
                        ShowMsgBoxInfo("Request accepted.");
                        LogDebug("Request accepted.");
                        Application.Restart();
                    }
                    else
                    {
                        ShowMsgBoxError("Request failed : ERROR " + RESTORE_SETTING_doc.SelectSingleNode("//error/code").InnerText.ToString() + " [" + ((ErrorCode)(int.Parse(RESTORE_SETTING_doc.SelectSingleNode("//error/code").InnerText))).ToString() + "]");
                        LogDebug("Request failed : ERROR " + RESTORE_SETTING_doc.SelectSingleNode("//error/code").InnerText.ToString() + " [" + ((ErrorCode)(int.Parse(RESTORE_SETTING_doc.SelectSingleNode("//error/code").InnerText))).ToString() + "]");
                    }
                }
                else
                {
                    return;
                }
            }
            else
            {
                return;
            }
        }

        private void Button2G_Click(object sender, EventArgs e)
        {
            if (bgw_networkQuickSwitch.IsBusy != true) { bgw_networkQuickSwitch.RunWorkerAsync("2G"); BandQuickSwitch = "2G"; }
        }

        private void Button3G_Click(object sender, EventArgs e)
        {
            if (bgw_networkQuickSwitch.IsBusy != true) { bgw_networkQuickSwitch.RunWorkerAsync("3G"); BandQuickSwitch = "3G"; }
        }

        private void Button4G_Click(object sender, EventArgs e)
        {
            if (bgw_networkQuickSwitch.IsBusy != true) { bgw_networkQuickSwitch.RunWorkerAsync("4G"); BandQuickSwitch = "4G"; }
        }

        private void ButtonAuto_Click(object sender, EventArgs e)
        {
            if (bgw_networkQuickSwitch.IsBusy != true) { bgw_networkQuickSwitch.RunWorkerAsync("Auto"); BandQuickSwitch = "Auto"; }
        }

        private void ButtonApplyBand_Click(object sender, EventArgs e)
        {
            if(checkedListBox1.CheckedItems.Count == 0) { ShowMsgBoxError("LTE band not selected."); return; }

            this.Enabled = false;
            long total = 0;

            foreach (object item in checkedListBox1.CheckedItems)
            {
                var get_enum = ((Band)(int.Parse(item.ToString())));

                //string band hex
                var band_hex = get_enum.ToString().Replace("B_", "");
                long decValue = Int64.Parse(band_hex, System.Globalization.NumberStyles.HexNumber);
                total = total + decValue;

            }


            //dec to hex
            string hexValue = total.ToString("X");


            var data = "<?xml version:\"1.0\" encoding=\"UTF-8\"?><request><NetworkMode>99</NetworkMode><NetworkBand>3FFFFFFF</NetworkBand><LTEBand>" + hexValue + "</LTEBand></request>";

            string band_selected = "";
            foreach (Object item in checkedListBox1.CheckedItems)
            {
                band_selected += item + ",";
            }
            band_selected = band_selected.Remove(band_selected.Length - 1);

            LogDebug("Locking band " + band_selected + "..");

            var POST_doc = PostSMS("api/net/net-mode", data);
            string result = POST_doc.OuterXml.ToString();

            if (result == "<?xml version=\"1.0\" encoding=\"UTF-8\"?><response>OK</response>")
            {
                ShowMsgBoxInfo("Request accepted.");
                LogDebug("Request accepted.");
            }
            else if (result.Contains("error"))
            {
                ShowMsgBoxError("Request failed : ERROR " + POST_doc.SelectSingleNode("//error/code").InnerText.ToString() + " : " + ((ErrorCode)(int.Parse(POST_doc.SelectSingleNode("//error/code").InnerText))).ToString());
                LogDebug("Request failed : ERROR " + POST_doc.SelectSingleNode("//error/code").InnerText.ToString() + " [" + ((ErrorCode)(int.Parse(POST_doc.SelectSingleNode("//error/code").InnerText))).ToString() + "]");

            }
            this.Enabled = true;


            //    for multiple band, convert hex of band to binary, add up all binary. convert to hex
            //    for instane band 3,7,8
            //    band 3 = 0000000000000004, binary = 100
            //    band 7 = 0000000000000040, binary = 1000000
            //    band 8 = 0000000000000080, binary = 10000000
            //    add up become = ‭11000100‬
            //    convert 11000100 to hex = C4

        }









        private void rtbSMSContent_TextChanged(object sender, EventArgs e)
        {
            int CharLeft = 160 - rtbSMSContent.Text.Length;
            label56.Text = CharLeft.ToString();
           

        }

        private void InitialiseSMS()
        {
            if (string.IsNullOrEmpty(_sessionCookie) || string.IsNullOrEmpty(_requestToken))
            {
                GetTokenSMS();
            }
        }

        

        private void GetTokenSMS()
        {

            try
            {
                XmlDocument GetTokens_doc = Get("api/webserver/SesTokInfo");
                _sessionIDSMS = GetTokens_doc.SelectSingleNode("//response/SesInfo").InnerText;
                _tokenSMS = GetTokens_doc.SelectSingleNode("//response/TokInfo").InnerText;

                //LogDebug(string.Format("\n[*] New session ID: {0}", _sessionID));
                //LogDebug(string.Format("\n[*] New token ID: {0}", _token));

                _requestTokenSMS = _tokenSMS;
                _sessionCookieSMS = _sessionIDSMS;
            }
            catch
            {
                LogError("\n[*] Unable to connect to the remote server. Make sure PC is connected to the network!");
            }


        }

        private WebClient NewWebClientSMS()
        {
            var wc = new WebClient();
            wc.Headers.Add(HttpRequestHeader.Cookie, _sessionCookieSMS);
            //wc.Headers.Add("Cache-Control", "no-cache");
            wc.Headers.Add("__RequestVerificationToken", _requestTokenSMS);
            wc.Headers.Add("Accept", "*/*");
            wc.Headers.Add("User-Agent", "Mozilla/5.0 (Windows; U; Windows NT 5.1; en-US; rv:1.9.2.12) Gecko/20101026 Firefox/3.6.12");
            return wc;
        }

        

        private void HandleHeaderSMS(WebClient wc)
        {
            if (!string.IsNullOrEmpty(wc.ResponseHeaders["__RequestVerificationToken"]))
            {
                _requestTokenSMS = wc.ResponseHeaders["__RequestVerificationToken"];
                //LogDebug(string.Format("\n[*] Recieved new RVT: {0}", _requestToken));
            }
            if (!string.IsNullOrEmpty(wc.ResponseHeaders["Set-Cookie"]))
            {
                _sessionCookieSMS = wc.ResponseHeaders["Set-Cookie"];
                //LogDebug(string.Format("\n[*] Recieved new cookie: {0}", _sessionCookie));
            }
        }

        public XmlDocument PostSMS(string path, string data)
        {
            GetSesTOK();
            var wc = PostWebClient();
            var response = wc.UploadData("http://" + textBoxIP.Text + "/" + path, Encoding.UTF8.GetBytes(data));
            var responseString = Encoding.Default.GetString(response);
            //PostHandleHeaders(wc);
            XmlDocument Post_doc = new XmlDocument();
            Post_doc.LoadXml(responseString);
            return Post_doc;
        }

        public void GetSesTOK()
        {
            //get session id n token
            var Sestoken = Get("api/webserver/SesTokInfo");
            _CurrentSessionID = Sestoken.SelectSingleNode("//response/SesInfo").InnerText;
            _CurrentToken = Sestoken.SelectSingleNode("//response/TokInfo").InnerText;
        }

        public static WebClient PostWebClient()
        {

            var wc = new WebClient();
            wc.Headers.Add(HttpRequestHeader.Cookie, _CurrentSessionID);
            //wc.Headers.Add("Cache-Control", "no-cache");
            wc.Headers.Add("__RequestVerificationToken", _CurrentToken);
            wc.Headers.Add("Accept", "*/*");
            wc.Headers.Add("User-Agent", "Mozilla/5.0 (Windows; U; Windows NT 5.1; en-US; rv:1.9.2.12) Gecko/20101026 Firefox/3.6.12");
            return wc;
        }


        private void btnSMSSend_Click(object sender, EventArgs e)
        {
            string recipient = rtbSMSRecipient.Text;
            string content = rtbSMSContent.Text;

            if (recipient != string.Empty && content != string.Empty)
            {
                var data = "<?xml version:\"1.0\" encoding=\"UTF-8\"?><request><Index>-1</Index><Phones><Phone>" + recipient + "</Phone></Phones><Sca></Sca><Content>" + content + "</Content><Length> " + content.Length.ToString() + "</Length><Reserved>1</Reserved><Date>" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").ToString() + "</Date></request>";
                
                LogDebug("Sending SMS to " + recipient);
                
                var POST_doc = PostSMS("api/sms/send-sms", data);
                string compare = POST_doc.OuterXml.ToString();

                if (POST_doc == null)
                {
                    LogDebug("Failed to send SMS.");
                    ShowMsgBoxError("Failed to send SMS.");
                }
                else if (compare.Contains("OK"))
                {
                    var send_status = Get_new("api/sms/send-status");

                    if (!string.IsNullOrEmpty(send_status.OuterXml.ToString()))
                    {
                        string response = xmlformat.Beautify(send_status);
                        XmlDocument xml = new XmlDocument();
                        xml.LoadXml(response);
                        var test = xml.SelectSingleNode("response");
                        bool isFail = false;

                        foreach (XmlNode node in test)
                        {
                            if (node.Name == "Phone")
                            {
                                if (node.Value == recipient)
                                {
                                    if (node.Name == "FailPhone")
                                    {
                                        if (node.Value == "1")
                                            isFail = true;
                                    }
                                }
                            }
                        }

                        if (!isFail)
                        {
                            ShowMsgBoxInfo("Request accepted.");
                            LogDebug("Request accepted.");
                        }
                        else
                        {
                            ShowMsgBoxError("Request failed.");
                            LogDebug("Request failed.");
                        }

                        response = "";
                        xml = null;
                    }
                }
                else if (compare.Contains("error"))
                {
                    ShowMsgBoxError("Request failed : ERROR " + POST_doc.SelectSingleNode("//error/code").InnerText.ToString() + " [" + ((ErrorCode)(int.Parse(POST_doc.SelectSingleNode("//error/code").InnerText))).ToString() + "]");
                    LogDebug("Request failed : ERROR " + POST_doc.SelectSingleNode("//error/code").InnerText.ToString() + " [" + ((ErrorCode)(int.Parse(POST_doc.SelectSingleNode("//error/code").InnerText))).ToString() + "]");
                }
            }
            else
            {
                ShowMsgBoxError("Please fill recipients and content");
            }
            
        }

        private void metroButton1_Click_3(object sender, EventArgs e)
        {
            var data = "<?xml version:\"1.0\" encoding=\"UTF-8\"?><request><PageIndex>1</PageIndex><ReadCount>20</ReadCount><BoxType>1</BoxType><SortType>0</SortType><Ascending>0</Ascending><UnreadPreferred>0</UnreadPreferred></request>";
            LogDebug("Retrieving SMS..");
            var POST_doc = PostSMS("api/sms/sms-list", data);
            string compare = POST_doc.OuterXml.ToString();

            if (POST_doc == null)
            {
                LogDebug("Failed to retrieve SMS");
            }
            else if(POST_doc.OuterXml.ToString().Contains("response"))
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("From");
                dt.Columns.Add("Content");
                dt.Columns.Add("Date");

                XmlDocument serverDoc = new XmlDocument();
                serverDoc.LoadXml(compare);
                XmlNodeList xml =
                serverDoc.SelectNodes("response/Messages/Message");
                foreach (XmlNode node in xml)
                {
                    // get information needed to extract temprature i.e XML location, and the XPath :
                    var Phone = node.SelectSingleNode("Phone").InnerText;
                    var Content = node.SelectSingleNode("Content").InnerText;
                    var Date = node.SelectSingleNode("Date").InnerText;

                    dt.Rows.Add(Phone, Content, Date);


                    //rtbSMSContent.Text += Phone + "\n";
                    //rtbSMSContent.Text += Content + "\n";
                    //rtbSMSContent.Text += Date + "\n";
                    //rtbSMSContent.Text += "\n\n";

                    dataGridView1.DataSource = dt;
                }
            }
            else if (POST_doc.OuterXml.ToString().Contains("error"))
            {
                LogDebug("Request failed : ERROR " + POST_doc.SelectSingleNode("//error/code").InnerText.ToString() + " [" + ((ErrorCode)(int.Parse(POST_doc.SelectSingleNode("//error/code").InnerText))).ToString() + "]");
                ShowMsgBoxError("Request failed : ERROR " + POST_doc.SelectSingleNode("//error/code").InnerText.ToString() + " [" + ((ErrorCode)(int.Parse(POST_doc.SelectSingleNode("//error/code").InnerText))).ToString() + "]");
            }

        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                //get selected item value
                int currentMouseOverRow = dataGridView1.HitTest(e.X, e.Y).RowIndex;
                if (dataGridView1.GetCellCount(DataGridViewElementStates.Selected) > 0)
                {
                    try
                    {
                        copyNumber = "";
                        //get each selected pkg full path
                        foreach (DataGridViewCell cell in dataGridView1.SelectedCells)
                        {
                            int selectedrowindex = cell.RowIndex;
                            DataGridViewRow selectedRow = dataGridView1.Rows[selectedrowindex];
                            //path + filename.pkg
                            copyNumber = Convert.ToString(selectedRow.Cells[0].Value);
                            if (copyNumber != string.Empty)
                                contextMenuStrip1.Show(dataGridView1, new Point(e.X, e.Y));
                        }
                    }
                    catch (System.Runtime.InteropServices.ExternalException)
                    {
                        ShowMsgBoxError("The Clipboard could not be accessed. Please try again.");
                    }
                }
            }
        }

        private bool isCheckingUSSD()
        {
            XmlDocument APItype = Get_API("api/ussd/status");

            var result = xmlformat.Beautify(APItype);



            var status = "";
            StringReader reader = new StringReader(result);

            while ((result = reader.ReadLine()) != null)
            {
                if (result.Contains("result"))
                    status = result.Replace("<result>", "").Replace("</result>", "").Replace(" ", "");
            };


            //if ussd ready = 0
            if (status == "0")
            {

                return false;



            }
            else
                return true;

        }

        private void copyPhoneNumberToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(copyNumber);
            ShowMsgBoxInfo("Phone Number copied to clipboard");
        }

        private int checkUSSD()
        {
            USSDresult = 0;
            outstring = "";
            XmlDocument APItype = Get_API("api/ussd/get");

            var result = xmlformat.Beautify(APItype);
            if (result.Contains("<response>"))
            {
                USSDresult = 1;
                outstring = result.Replace("<response>", "").Replace("<content>", "").Replace("</content>", "").Replace("</response>", "");
            }
            else
            {
                StringReader reader = new StringReader(result);
                string line = reader.ReadLine();
                if (line.Contains("<error>"))
                {
                    USSDresult = 0;
                }
            }
            return USSDresult;
        }

        private void metroButton2_Click(object sender, EventArgs e)
        {
            richTextBox3.Clear();

            if (isCheckingUSSD() == false)
            {
                LogDebug("Sending USSD command..");

                var data = "<?xml version:\"1.0\" encoding=\"UTF-8\"?><request><content>" + tbUSSDCMD.Text + "</content><codeType>CodeType</codeType><timeout></timeout></request>";
                var POST_doc = PostSMS("api/ussd/send", data);
                string compare = POST_doc.OuterXml.ToString();

                if (POST_doc == null)
                {
                    richTextBox3.Text += "No response." + "\n";
                    LogDebug("No response.");

                }
                else
                {

                    if (compare == "<?xml version=\"1.0\" encoding=\"UTF-8\"?><response>OK</response>")
                    {
                        richTextBox3.Text += "Request accepted. Please wait.." + "\n";
                        LogDebug("Request accepted. Waiting for response..");



                        while (USSDresult != 1)
                        {

                            checkUSSD();
                        }

                        LogDebug("Response received.");



                        richTextBox3.Text = outstring;
                        richTextBox3.Lines = richTextBox3.Lines.Skip(2).ToArray();

                        string trimmed = richTextBox3.Text.Trim();
                        richTextBox3.Text = trimmed;
                        listBox1.Items.Clear();
                        using (var reader = new StringReader(richTextBox3.Text))
                        {
                            for (string line = reader.ReadLine(); line != null; line = reader.ReadLine())
                            {
                                if (Regex.IsMatch(line, @"^\d+"))
                                {
                                }

                                listBox1.Items.Add(line);

                            }
                        }

                        //if(listBox1.GetItemText(listBox1.SelectedItem);)

                        USSDresult = 0;
                        outstring = "";
                    }
                    else
                    {
                        richTextBox3.Text += "Request failed : ERROR " + POST_doc.SelectSingleNode("//error/code").InnerText.ToString() + " : " + ((ErrorCode)(int.Parse(POST_doc.SelectSingleNode("//error/code").InnerText))).ToString() + "\n";
                        LogDebug("Request failed : ERROR " + POST_doc.SelectSingleNode("//error/code").InnerText.ToString() + " : " + ((ErrorCode)(int.Parse(POST_doc.SelectSingleNode("//error/code").InnerText))).ToString());

                    }

                }


            }
            else
            { LogDebug("USSD in progress. Try again later."); richTextBox3.Text += "USSD in progress. Try again later." + "\n"; return; }
                

            

        }

        private void metroButton3_Click(object sender, EventArgs e)
        {
            
        }

        private void metroButton3_Click_1(object sender, EventArgs e)
        {
           
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string text = listBox1.GetItemText(listBox1.SelectedItem);

            if (Regex.IsMatch(text, @"^\d+"))
            {
                string part = text.Substring(0, text.IndexOf(' '));
                tbUSSDCMD.Text = part;
            }
            
        }

        private void metroButton3_Click_2(object sender, EventArgs e)
        {
            
        }

       


        private void replyMessageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtbSMSRecipient.Text = copyNumber;
            rtbSMSContent.Text = "";
            rtbSMSContent.Focus();


        }

        private void showHidePass_Click(object sender, EventArgs e)
        {
            if (textBoxPassword.UseSystemPasswordChar == true)
            {
                textBoxPassword.UseSystemPasswordChar = false;
            }
            else
            {
                textBoxPassword.UseSystemPasswordChar = true;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (textBoxPassword.UseSystemPasswordChar == true)
            {
                showHidePass.Text = "Show";
            }
            else
            {
                showHidePass.Text = "Hide";
            }
        }

        private void backgroundWorkerDeviceInfo_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //enable control
            buttonShutdown.Enabled = true;
            buttonReboot.Enabled = true;
            metroTabControl1.Enabled = true;
            buttonWebpage.Enabled = true;
            saveDeviceInfoToolStripMenuItem1.Enabled = true;
        }

        private void btnRefreshMAC_Click(object sender, EventArgs e)
        {
            BackgroundWorker bgw = new BackgroundWorker();
            bgw.WorkerSupportsCancellation = true;
            bgw.DoWork += delegate
            {
                LogDebug("Retrieving MAC filter..");
                MacFilter();
            };
            bgw.RunWorkerCompleted += delegate
            {
                bgw.CancelAsync();
            };
            bgw.RunWorkerAsync();
        }

        private void bgw_networkQuickSwitch_DoWork(object sender, DoWorkEventArgs e)
        {
            this.Enabled = false;
            string buttonName = (string)e.Argument;
            string data = "";
            string NetworkMode = "";

            switch (buttonName)
            {
                case "Auto":
                    NetworkMode = "00";
                    break;
                case "2G":
                    NetworkMode = "01";
                    break;
                case "3G":
                    NetworkMode = "02";
                    break;
                case "4G":
                    NetworkMode = "03";
                    break;
            }

            data = "<?xml version:\"1.0\" encoding=\"UTF-8\"?><request><NetworkMode>" + NetworkMode + "</NetworkMode><NetworkBand>3FFFFFFF</NetworkBand><LTEBand>7FFFFFFFFFFFFFFF</LTEBand></request>";
            var POST_doc = PostSMS("api/net/net-mode", data);

            if (NetworkMode == "00")
                NetworkMode = "Auto";
            if (NetworkMode == "01")
                NetworkMode = "2G only";
            if (NetworkMode == "02")
                NetworkMode = "3G only";
            if (NetworkMode == "03")
                NetworkMode = "4G only";

            LogDebug("Attempting to change network mode to " + NetworkMode + "..");

            string result = POST_doc.OuterXml.ToString();

            if (result == "<?xml version=\"1.0\" encoding=\"UTF-8\"?><response>OK</response>")
            {
                ShowMsgBoxInfo("Request accepted.");
                LogDebug("Request accepted.");
            }
            else if (result.Contains("error"))
            {
                ShowMsgBoxError("Request failed : ERROR " + POST_doc.SelectSingleNode("//error/code").InnerText.ToString() + " : " + ((ErrorCode)(int.Parse(POST_doc.SelectSingleNode("//error/code").InnerText))).ToString());
                LogDebug("Request failed : ERROR " + POST_doc.SelectSingleNode("//error/code").InnerText.ToString() + " [" + ((ErrorCode)(int.Parse(POST_doc.SelectSingleNode("//error/code").InnerText))).ToString() + "]");
            }
        }

        private void bgw_networkQuickSwitch_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.Enabled = true;
        }

        private void CheckBoxOLED_Password_MouseClick_1(object sender, MouseEventArgs e)
        {
            string data;

            if (checkBoxOLED_Password.Checked)
            {
                LogDebug("Showing Wifi password on screen..");

                data = "<?xml version:\"1.0\" encoding=\"UTF-8\"?><request><oledshowpassword>1</oledshowpassword></request>";
                var OLED_doc = PostSMS("api/wlan/oled-showpassword", data);

                if (OLED_doc.OuterXml == "<?xml version=\"1.0\" encoding=\"UTF-8\"?><response>OK</response>")
                {
                    LogDebug("Request accepted.");
                    ShowMsgBoxInfo("Request accepted.");
                }
                else
                {
                    ShowMsgBoxError("Request failed : ERROR " + ((ErrorCode)(int.Parse(OLED_doc.SelectSingleNode("//error/code").InnerText))).ToString());
                    LogDebug("Request failed : ERROR " + OLED_doc.SelectSingleNode("//error/code").InnerText.ToString() + " [" + ((ErrorCode)(int.Parse(OLED_doc.SelectSingleNode("//error/code").InnerText))).ToString() + "]");
                    checkBoxOLED_Password.Checked = false;
                }

            }
            else
            {
                LogDebug("Hiding Wifi password on screen..");

                data = "<?xml version:\"1.0\" encoding=\"UTF-8\"?><request><oledshowpassword>0</oledshowpassword></request>";
                var OLED_doc = Post("api/wlan/oled-showpassword", data);

                if (OLED_doc.OuterXml == "<?xml version=\"1.0\" encoding=\"UTF-8\"?><response>OK</response>")
                {
                    LogDebug("Request accepted.");
                    ShowMsgBoxInfo("Request accepted.");

                }
                else
                {
                    ShowMsgBoxError("Request failed : ERROR " + ((ErrorCode)(int.Parse(OLED_doc.SelectSingleNode("//error/code").InnerText))).ToString());
                    LogDebug("Request failed : ERROR " + OLED_doc.SelectSingleNode("//error/code").InnerText.ToString() + " [" + ((ErrorCode)(int.Parse(OLED_doc.SelectSingleNode("//error/code").InnerText))).ToString() + "]");
                    checkBoxOLED_Password.Checked = true;

                }
            }
        }

      
        private void LinkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.cellmapper.net/map?MCC=" + Form1.mcc + "&MNC=" + Form1.mnc);
        }

        private void ListView1_MouseClick(object sender, MouseEventArgs e)
        {
            if (listView1.SelectedItems.Count == 1)
            {
                bool match = false;
                if (e.Button == System.Windows.Forms.MouseButtons.Right)
                {
                    foreach (ListViewItem item in listView1.Items)
                    {
                        if (item.Bounds.Contains(new Point(e.X, e.Y)))
                        {
                            MenuItem[] mi = new MenuItem[] { new MenuItem("Copy MAC address", CopyMAC_Click), new MenuItem("Copy all info", CopyAll_Click) };
                            listView1.ContextMenu = new ContextMenu(mi);
                            match = true;
                            break;
                        }
                    }
                }
            }
        }

      

        private void ButtonGetCurrentStat_Click(object sender, EventArgs e)
        {
            var netwrok_status = Get_API("api/net/net-mode");


            if (!string.IsNullOrEmpty(netwrok_status.OuterXml.ToString()))
            {
                LogDebug("Getting current network status..");

                if (netwrok_status.OuterXml.ToString().Contains("error"))
                {
                
                    LogDebug("Request failed : ERROR " + netwrok_status.SelectSingleNode("//error/code").InnerText.ToString() + " [" + ((ErrorCode)(int.Parse(netwrok_status.SelectSingleNode("//error/code").InnerText))).ToString() + "]");
                    ShowMsgBoxError("Request failed : ERROR " + netwrok_status.SelectSingleNode("//error/code").InnerText.ToString() + " [" + ((ErrorCode)(int.Parse(netwrok_status.SelectSingleNode("//error/code").InnerText))).ToString() + "]\n");

                }
                else if (netwrok_status.OuterXml.ToString().Contains("response"))
                {
                    string NetworkMode = netwrok_status.SelectSingleNode("//response/NetworkMode").InnerText;
                    string NetworkBand = netwrok_status.SelectSingleNode("//response/NetworkBand").InnerText;
                    string LTEBand = netwrok_status.SelectSingleNode("//response/LTEBand").InnerText;

                    textBoxNetworkMode.Text = NetworkMode;
                    textBoxNetworkband.Text = NetworkBand;
                    textBoxLTE_Band.Text = LTEBand;

                    LogDebug("Request accepted.");
                }
            }
            else
            {
                ShowMsgBoxError("Failed to get current network status.");
                LogDebug("Failed to get current network status.");
            }

          
        }

        private void ButtonApplyAPI_Click_1(object sender, EventArgs e)
        {
            runAPI();
        }

        private void MetroButton1_Click_2(object sender, EventArgs e)
        {
            webpage wp = new webpage();
            wp.ShowDialog();
        }

       

        private void comboBoxAPIList_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            runAPI();
        }

        private void ButtonpostAPI_Click(object sender, EventArgs e)
        {
            if (richTextBox2.Text == "")
                return;

            DialogResult dialogResult = MessageBox.Show("Data : \n\n" + richTextBox2.Text + "\n\nPost to API : " + comboBoxAPIList.Text + "\n\nConfirm?", "Huawei Router Tool", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                LogDebug("Sending HTTP POST request to " + comboBoxAPIList.Text);

                var data = "<?xml version:\"1.0\" encoding=\"UTF-8\"?>" + richTextBox2.Text;
                var POST_doc = PostSMS(comboBoxAPIList.Text, data);

                if (POST_doc == null)
                {
                    ShowMsgBoxError("No response.");
                    LogDebug("No response.");
                }
                else if (POST_doc.OuterXml.ToString().Contains("OK"))
                {
                    ShowMsgBoxInfo("Request accepted.");
                    LogDebug("Request accepted.");
                    runAPI();
                }
                else if (POST_doc.OuterXml.ToString().Contains("error"))
                {
                    ShowMsgBoxError("Request failed : ERROR " + POST_doc.SelectSingleNode("//error/code").InnerText.ToString() + " : " + ((ErrorCode)(int.Parse(POST_doc.SelectSingleNode("//error/code").InnerText))).ToString());
                    LogDebug("Request failed : ERROR " + POST_doc.SelectSingleNode("//error/code").InnerText.ToString() + " [" + ((ErrorCode)(int.Parse(POST_doc.SelectSingleNode("//error/code").InnerText))).ToString() + "]");
                }
            }
        }

        private void ComboBoxAPIList_SelectedIndexChanged_2(object sender, EventArgs e)
        {
            runAPI();
        }

        private void PictureBox2_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.paypal.com/paypalme2/pearlxcore?locale.x=en_US");
        }

        private void MetroButton1_Click_1(object sender, EventArgs e)
        {
            SignalMonitor SM = new SignalMonitor();
            SM.ShowDialog();
            LogDebug("Signal Monitor window opened.");
        }

        private void LinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/pearlxcore");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            LogDebug("Starting speedtest..");

            ShowMsgBoxInfo("Result may inaccurate. Alternatively you can test the internet speed using speed test official website or apps.");

            if (backgroundWorkerSpeedtest.IsBusy)
            {
                backgroundWorkerSpeedtest.CancelAsync();
                backgroundWorkerSpeedtest.RunWorkerAsync();
            }
            else
            {
                backgroundWorkerSpeedtest.RunWorkerAsync();
            }
        }

        private void PerformTest(out double downloadSpeed, out double uploadSpeed, out Server server)
        {
            client = new SpeedTestClient();
            settings = client.GetSettings();

            var servers = SelectServers();
            var bestServer = SelectBestServer(servers);
            textBox4.Text = server_detail.ToString();

            var ds = client.TestDownloadSpeed(bestServer, settings.Download.ThreadsPerUrl);
            PrintSpeeddown(ds);

            var us = client.TestUploadSpeed(bestServer, settings.Upload.ThreadsPerUrl);
            PrintSpeedup(us);

            downloadSpeed = ds;
            uploadSpeed = us;
            server = bestServer;
        }

        public static Server SelectBestServer(IEnumerable<Server> servers)
        {
            var bestServer = servers.OrderBy(x => x.Latency).First();
            PrintServerDetails(bestServer);
            return bestServer;
        }

        private static IEnumerable<Server> SelectServers()
        {
            var servers = settings.Servers.Take(10).ToList();

            foreach (var server in servers)
            {
                server.Latency = client.TestServerLatency(server);
                PrintServerDetails(server);
            }

            return servers;
        }


        private void PrintSpeeddown(double speed)
        {
            if (speed > 1024)
            {
                textBox5.Text = Math.Round(speed / 1024, 2) + " Mbps";
            }
            else
            {
                textBox5.Text = Math.Round(speed, 2) + " Mbps";
            }
        }

        private void PrintSpeedup(double speed)
        {
            if (speed > 1024)
            {
                textBox6.Text = Math.Round(speed / 1024, 2) + " Mbps";
            }
            else
            {
                textBox6.Text = Math.Round(speed, 2) + " Mbps";
            }
        }

        private static void PrintServerDetails(Server server)
        {
            server_detail = server.Sponsor + " (" + server.Name + "/" + server.Country + ") | " + "Distance : " + (int)server.Distance / 1000 + "Km | Latency : " + server.Latency;
        }

    }
}
