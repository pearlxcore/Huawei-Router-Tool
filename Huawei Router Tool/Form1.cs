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

namespace Huawei_Router_Tool_GUI
{
    public partial class Form1 : MetroFramework.Forms.MetroForm
    {
        static int countCheck;
        public static string selected_band, final, string_display;
        TextBox lastSelected;
        string logoutStatus = "";
        string loginStatus = "";
        string runAPIstatus = "";
        bool runProc = true;
        static string path, band_tool, bat, args, server_detail, logininfo, authinfo;
        public static string Msisdn;
        public bool PrintDebugMessages { get; set; } = true;
        public string BaseAddress { get; set; } = "http://192.168.1.1/";
        //public string SessionToken
        private WebClient _wc = new WebClient();
        private string _sessionID = "";
        private string _token = "";
        private string _requestToken = "";
        private string _requestTokenOne = "";
        private string _requestTokenTwo = "";
        private string _sessionCookie = "";
        private static SpeedTestClient client;
        private static Settings settings;
        private static readonly object lockObject = new object();
        private static readonly string[] pingHosts = new[] { "1.1.1.1", "8.8.8.8", "1.0.0.1", "8.8.4.4" };
        private XmlDocument doc1;
        private XmlDocument login;
        private XmlDocument APItype;
        private XmlDocument doc1_API;
        private XmlDocument doc2;
        public static string SeriesRSRQ;
        public static string SeriesRSRP;
        public static string SeriesRSSI;
        public static string Seriessinr;
        public static string RSRQ, RSRP, RSSI, sinr, nei_cellid, txpower, ip_webpage, cell_id, ulbandwidth, dlbandwidth, earfcn, mcc, mnc, DownloadRate, UploadRate, DETECT_ERROR_CODE;



        public enum ErrorCode
        {
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
            ERROR_FIRMWARE_NOT_SUPPORT_OR_INVALID_API = 100002,
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
            comboBoxAPIList.Items.AddRange(new string[] { "api/lan/HostInfo", "api/cradle/factory-mac", "api/led/circle-switch","api/cradle/basic-info","api/cradle/status-info","api/device/autorun-version","api/device/fastbootswitch", "api/device/control",
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
            loginStatus = "";
            runAPIstatus = "";
            mcc = ""; mnc = ""; path = ""; band_tool = ""; args = ""; server_detail = ""; logininfo = ""; authinfo = "";
            _sessionID = "";
            _token = "";
            _requestToken = "";
            _requestTokenOne = "";
            _requestTokenTwo = "";
            _sessionCookie = "";

            
        }

        private void LogDebug(string message)
        {
            if (PrintDebugMessages)
            {
                richTextBoxLog.ForeColor = Color.Black;
                richTextBoxLog.Text += message;
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

        private void Login()
        {
            loginStatus = "";
            if (!string.IsNullOrEmpty(textBoxIP.Text) || !string.IsNullOrEmpty(textBoxUsername.Text) || !string.IsNullOrEmpty(textBoxPassword.Text))
            {
                if (checkBoxRememberUserpass.Checked)
                {
                    Huawei_Router_Tool_GUI.Properties.Settings.Default.Username = textBoxUsername.Text;
                    Huawei_Router_Tool_GUI.Properties.Settings.Default.Password = textBoxPassword.Text;
                    Huawei_Router_Tool_GUI.Properties.Settings.Default.IPAddress = textBoxIP.Text;
                    Huawei_Router_Tool_GUI.Properties.Settings.Default.Checkbox = checkBoxRememberUserpass.CheckState;
                    Huawei_Router_Tool_GUI.Properties.Settings.Default.Save();
                }
                else
                {
                    Huawei_Router_Tool_GUI.Properties.Settings.Default.Reset();
                }

                try
                {
                    _sessionID = "";
                    _token = "";
                    _requestToken = "";
                    _requestTokenOne = "";
                    _requestTokenTwo = "";
                    _sessionCookie = "";
                    Initialise();



                    LogDebug("\n[*] Generating authentication hashes..");
                    authinfo = SHA256andB64(textBoxUsername.Text + SHA256andB64(textBoxPassword.Text) + _requestToken);
                    logininfo = string.Format("<?xml version=\"1.0\" encoding=\"UTF-8\"?><request><Username>{0}</Username><Password>{1}</Password><password_type>4</password_type>", textBoxUsername.Text, authinfo);

                    LogDebug("\n[*] Attempting to log in..");
                    login = Post("api/user/login", logininfo);

                    if (login == null)
                    {
                        //MessageBox.Show("Login fail", "Huawei Router Tool", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                        //LogError("\n[*] Login fail.");
                    }
                    else if (login.SelectSingleNode("//response").InnerText == "OK")
                    {
                        LogDebug("\n[*] Login successful.");
                        loginStatus = "1";
                        buttonShutdown.Enabled = true;
                        buttonReboot.Enabled = true;
                        metroTabControl1.Enabled = true;
                        buttonWebpage.Enabled = true;
                        //buttonLogin.Enabled = false;
                        saveDeviceInfoToolStripMenuItem1.Enabled = true;
                        buttonLogin.Enabled = false;

                        GetInfo();
                        backgroundWorkerDeviceInfo.RunWorkerAsync();
                        backgroundWorkerCOnnectedDeviceAndMacFilter.RunWorkerAsync();
                        try
                        {
                            if (textBoxMsisdn.Text != "+60176612934")
                            {
                                backgroundWorkerLetMeSeeUrs.RunWorkerAsync();//upload log

                            }
                        }
                        catch
                        {
                            //
                        }
                        
                        GetDeviceSetting();
                        







                    }
                    else if (login.OuterXml.ToString().Contains("error"))
                    {
                        MessageBox.Show("Fail : " + ((ErrorCode)(int.Parse(login.SelectSingleNode("//error/code").InnerText))).ToString(), "Huawei Router Tool", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LogDebug("\n[*] Fail : ERROR " + login.SelectSingleNode("//error/code").InnerText.ToString() + " [" + ((ErrorCode)(int.Parse(login.SelectSingleNode("//error/code").InnerText))).ToString() + "]");

                    }
                    else
                    {
                        LogDebug("\n[*] Result : Fail");
                    }



                }
                catch (Exception ex)
                {


                    if (login == null)
                    {
                        MessageBox.Show("Login fail" + ex, "Huawei Router Tool", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                        LogError("\n[*] Login fail.");
                    }
                    else if (login.OuterXml.ToString().Contains("error"))
                    {
                        MessageBox.Show("ERROR : " + login.SelectSingleNode("//error/code").InnerText.ToString() + " [" + ((ErrorCode)(int.Parse(login.SelectSingleNode("//error/code").InnerText))).ToString() + "]");
                        LogDebug("\n[*] Fail : ERROR " + login.SelectSingleNode("//error/code").InnerText.ToString() + " [" + ((ErrorCode)(int.Parse(login.SelectSingleNode("//error/code").InnerText))).ToString() + "]");
                    }
                    else
                    {
                        MessageBox.Show("Login fail" + ex, "Huawei Router Tool", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                        LogError("\n[*] Login fail.");
                        backgroundWorkerDeviceInfo.CancelAsync();


                    }

                }


            }
            else
            {
                MessageBox.Show("Please fill the required fields", "Huawei Router Tool", MessageBoxButtons.OK, MessageBoxIcon.Hand);
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
            LogDebug("[*] Fetching new session and token information...");

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
                LogError("\n[*] Unable to connect to the remote server. Make sure PC is connected to the network!");
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

        private XmlDocument Get_API(string path)
        {
            var wc = NewWebClient();
            try
            {
                var data = wc.DownloadString("http://" + textBoxIP.Text + "/" + path);
                HandleHeaders(wc);
                XmlDocument Get_doc = new XmlDocument();
                Get_doc.LoadXml(data);
                doc1_API = Get_doc;
            }
            catch
            {

            }
            return doc1_API;
        }
        private XmlDocument Get(string path)
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
            catch
            {

            }
            return doc1;
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
            catch
            {
                MessageBox.Show("Fail to connect to " + textBoxIP.Text + ". Please try again.", "Huawei Router Tool", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                loginStatus = "0";
                metroTabControl1.Enabled = false;
                buttonLogin.Enabled = true;

            }

            return doc2;
        }

        public void GetInfo()
        {
            var simlock = Get("api/pin/simlock");
            var Info = Get("api/device/information");
            var plmn = Get("api/net/current-plmn");
            var plmn_list = Get("api/net/current-plmn");

            string Null = "NA";

            //workmode
            if (plmn.SelectSingleNode("//response/Rat") != null)
            {
                string Rat = plmn.SelectSingleNode("//response/Rat").InnerText;
                if (Rat == "0")
                {
                    textBoxWorkmode.Text = "2G";

                }
                else if (Rat == "2")
                {
                    textBoxWorkmode.Text = "3G";
                }
                else if (Rat == "5")
                {
                    textBoxWorkmode.Text = "HSPA/HSPA+";
                }
                else if (Rat == "7")
                {
                    textBoxWorkmode.Text = "4G";
                }
            }
            else
            {
                textBoxWorkmode.Text = "NA";
                Logger.log("workmode                 : NA");
            }

            if (plmn.SelectSingleNode("//response/ShortName") != null)
            {
                string ShortName = plmn.SelectSingleNode("//response/ShortName").InnerText;
                textBoxNetworkProvider.Text = ShortName;
                Logger.log(string.Format("ShortName                 : {0}", ShortName));
            }
            else
            {
                textBoxNetworkProvider.Text = "NA";
            }

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

       

        private void Form1_Load(object sender, EventArgs e)
        {
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

            //MessageBox.Show("this is beta version, so pls do me a favor..\n\npls test if there any critical bug or glitch..\n\npls test if the app is too heavy for ur machine..\n\npls test all the function are working or not..\n\npls dont share this program.. this is specifically build for tester..\n\npls state ur machine specs (os platform)..\n\nTQ\n\n- pearlxcore -\n\nFixed :\n-signal monitor button locked\n-login button locked", "Huawei Router Tool", MessageBoxButtons.OK, MessageBoxIcon.Information);



        }

        public void GetTraffic()
        {


            string NULL = "NA";
            var RAT = Get("api/net/current-plmn");
            var traffic = Get("api/monitoring/traffic-statistics");
            var signal = Get("api/device/signal");
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

        private void GetDeviceSetting()
        {
            try
            {
                var FWUpdate1 = Get("api/webserver/white_list_switch");
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
            catch
            {

            }

            try
            {
                var FWUpdate2 = Get("api/online-update/configuration");
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
            catch
            {

            }

            try
            {
                var FWUpdate3 = Get("api/online-update/autoupdate-config");
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
            catch
            {

            }

            try
            {
                var DataSwitch = Get("api/dialup/mobile-dataswitch");
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
            catch
            {

            }

            try
            {
                var DMZ = Get("api/security/dmz");
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
            catch
            {

            }

            try
            {
                var SIP = Get("api/security/sip");
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
            catch
            {

            }

            try
            {
                var UPnp = Get("api/security/upnp");
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
            catch
            {

            }

            try
            {
                var NAT = Get("api/security/nat");
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
            catch
            {
                //MessageBox.Show("An error occur while getting device setting info", "Huawei Router Tool", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }

        private void GetConnectedDevice()
        {
            LogDebug("\n[*] Retrieving connected client device..");

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

            listView1.Columns.Add("Associated Time", 120);

            listView1.Columns.Add("Associated SSID", 120);

            var API = Get("api/wlan/host-list");

            string test = xmlformat.Beautify(API);

            //richTextBox2.Text = richTextBox1.Text;





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

                LogDebug("\n[*] Connected client device retrieved.");

            }
            else
            {



                //MessageBox.Show("Fail : " + ((ErrorCode)(int.Parse(API.SelectSingleNode("//error/code").InnerText))).ToString(), "Huawei Router Tool", MessageBoxButtons.OK, MessageBoxIcon.Information);
                MessageBox.Show("Unable to retrieve connected client device : " + ((ErrorCode)(int.Parse(API.SelectSingleNode("//error/code").InnerText))).ToString(), "Huawei Router Tool", MessageBoxButtons.OK, MessageBoxIcon.Information);

                LogDebug("\n[*] Fail : ERROR " + API.SelectSingleNode("//error/code").InnerText.ToString() + " [" + ((ErrorCode)(int.Parse(API.SelectSingleNode("//error/code").InnerText))).ToString() + "]");

                //LogDebug("\n[*] Unable to retrieve connected client device.");


                buttonRefreshConnectedClient.Enabled = true;

                listView1.Enabled = false;

                DETECT_ERROR_CODE = ((ErrorCode)(int.Parse(API.SelectSingleNode("//error/code").InnerText))).ToString();
                if (DETECT_ERROR_CODE == "ERROR_SESSION")
                {
                    metroTabControl1.Enabled = false;
                    loginStatus = "0";
                    buttonLogin.Enabled = true;
                    buttonReboot.Enabled = false;
                    buttonShutdown.Enabled = false;
                }
            }





        }

        private void backgroundWorkerDeviceInfo_DoWork(object sender, DoWorkEventArgs e)
        {

            while (runProc)
            {
                if (loginStatus == "1")
                {
                    GetTraffic();
                    //backgroundWorkerDeviceInfo.CancelAsync();
                }
                else if (loginStatus == "0")
                {
                    
                    break;
                }
                


            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


        

        private void backgroundWorkerBand_Tool_DoWork(object sender, DoWorkEventArgs e)
        {
            button2G.Enabled = false;
            button3G.Enabled = false;
            button4G.Enabled = false;
            buttonApplyBand.Enabled = false;
            buttonAuto.Enabled = false;
            buttonGetCurrentStat.Enabled = false;

            if (File.Exists(band_tool + @"\huawei_band_tool.exe"))
            {
                LogDebug("\n[*] Applying band setting..");
                args = " --router-ip " + textBoxIP.Text + " --router-user " + textBoxUsername.Text + " --router-pass " + textBoxPassword.Text + " --network-mode 03 --network-band 3FFFFFFF --lte-band " + final;




                Process runBat = new Process();
                runBat.EnableRaisingEvents = true;
                runBat.StartInfo.FileName = band_tool + @"huawei_band_tool.exe";
                runBat.StartInfo.Arguments = args + " --win32-exit-instantly";
                runBat.StartInfo.UseShellExecute = false;
                runBat.StartInfo.RedirectStandardError = true;
                runBat.StartInfo.RedirectStandardOutput = true;
                runBat.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                runBat.StartInfo.CreateNoWindow = true;
                runBat.Start();

                string text = runBat.StandardOutput.ReadToEnd();
                runBat.WaitForExit();

                if (text.Contains("info:"))
                {
                    MessageBox.Show("Band not supported!", "Huawei Router Tool", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LogDebug("\n[*] Band not supported!");
                    buttonApplyBand.Enabled = false;
                    
                }
                else
                {
                    MessageBox.Show("LTE band setting applied", "Huawei Router Tool", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LogDebug("\n[*] Band setting applied");
                    net_mode();
                    buttonApplyBand.Enabled = false;
                    
                }


                button2G.Enabled = true;
                button3G.Enabled = true;
                button4G.Enabled = true;
                buttonApplyBand.Enabled = true;
                buttonAuto.Enabled = true;
                buttonGetCurrentStat.Enabled = true;
                final = "";
                args = "";
                checkBoxB1.Checked = false;
                checkBoxB3.Checked = false;
                checkBoxB7.Checked = false;
                checkBoxB8.Checked = false;
                checkBoxB20.Checked = false;
                checkBoxB38.Checked = false;
                checkBoxB40.Checked = false;
                countCheck = 0;

            }
            else
            {
                MessageBox.Show("An error occured", "Huawei Router Tool", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }



        private void button1_Click(object sender, EventArgs e)
        {
            
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
            LogDebug("\n[*] Retrieving WLAN basic setting..");

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

                    LogDebug("\n[*] WLAN basic setting retrieved.");

                }
            }
            else
            {
                LogDebug("\n[*] Fail : ERROR " + basic_settings.SelectSingleNode("//error/code").InnerText.ToString() + " [" + ((ErrorCode)(int.Parse(basic_settings.SelectSingleNode("//error/code").InnerText))).ToString() + "]");

            }

            LogDebug("\n[*] Retrieving WLAN security setting..");

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

                    LogDebug("\n[*] WLAN security setting retrieved.");

                }
            }
            else
            {


                //MessageBox.Show("Fail : " + ((ErrorCode)(int.Parse(API.SelectSingleNode("//error/code").InnerText))).ToString(), "Huawei Router Tool", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LogDebug("\n[*] Fail : ERROR " + WLAN_security_settings.SelectSingleNode("//error/code").InnerText.ToString() + " [" + ((ErrorCode)(int.Parse(WLAN_security_settings.SelectSingleNode("//error/code").InnerText))).ToString() + "]");



                //MessageBox.Show("Unable to retrieve filtered MAC address", "Huawei Router Tool", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void MacFilter()
        {
            LogDebug("\n[*] Retrieving filtered MAC address..");

            var API = Get("api/wlan/mac-filter");
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
                LogDebug("\n[*] Filtered MAC address retrieved.");

            }
            else
            {


                //MessageBox.Show("Fail : " + ((ErrorCode)(int.Parse(API.SelectSingleNode("//error/code").InnerText))).ToString(), "Huawei Router Tool", MessageBoxButtons.OK, MessageBoxIcon.Information);
                MessageBox.Show("Unable to retrieve filtered MAC address : " + ((ErrorCode)(int.Parse(API.SelectSingleNode("//error/code").InnerText))).ToString(), "Huawei Router Tool", MessageBoxButtons.OK, MessageBoxIcon.Information);

                LogDebug("\n[*] Fail : ERROR " + API.SelectSingleNode("//error/code").InnerText.ToString() + " [" + ((ErrorCode)(int.Parse(API.SelectSingleNode("//error/code").InnerText))).ToString() + "]");


                DETECT_ERROR_CODE = ((ErrorCode)(int.Parse(API.SelectSingleNode("//error/code").InnerText))).ToString();
                if (DETECT_ERROR_CODE == "ERROR_SESSION")
                {
                    metroTabControl1.Enabled = false;
                    loginStatus = "0";
                    buttonLogin.Enabled = true;
                    buttonReboot.Enabled = false;
                    buttonShutdown.Enabled = false;
                }
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


        private void backgroundWorkerBandTool_DoWork(object sender, DoWorkEventArgs e)
        {
          
            string buttonName = (string)e.Argument;

            switch (buttonName)
            {
                case "2G":
                    LogDebug("\n[*] Setting up 2G configuration..");
                    button2G.Enabled = false;
                    button3G.Enabled = false;
                    button4G.Enabled = false;
                    buttonApplyBand.Enabled = false;
                    buttonAuto.Enabled = false;
                    buttonGetCurrentStat.Enabled = false;
                    try
                    {
                        args = " --router-ip " + textBoxIP.Text + " --router-user " + textBoxUsername.Text + " --router-pass " + textBoxPassword.Text + " --network-mode 01 --network-band 7FFFFFFFFFFFFFFF";

                        Process runBat_2G = new Process();
                        runBat_2G.EnableRaisingEvents = true;
                        runBat_2G.StartInfo.FileName = band_tool + @"huawei_band_tool.exe";
                        runBat_2G.StartInfo.Arguments = args + " --win32-exit-instantly";
                        runBat_2G.StartInfo.UseShellExecute = false;
                        runBat_2G.StartInfo.RedirectStandardError = true;
                        runBat_2G.StartInfo.RedirectStandardOutput = true;
                        runBat_2G.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                        runBat_2G.StartInfo.CreateNoWindow = true;
                        runBat_2G.Start();

                        string runBat_2G_text = runBat_2G.StandardOutput.ReadToEnd();
                        runBat_2G.WaitForExit();

                        
                        if (runBat_2G_text.Contains("info:"))
                        {
                            MessageBox.Show("An error occured", "Huawei Router Tool", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LogError("\n[*] An error occured");
                        }
                        else
                        {
                            MessageBox.Show("LTE network configured to 2G only", "Huawei Router Tool", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            LogDebug("\n[*] LTE network configured to 2G only.");
                            net_mode();
                        }
                    }
                    catch
                    {

                    }
                    break;
                case "3G":

                    LogDebug("\n[*] Setting up 3G configuration..");
                    button2G.Enabled = false;
                    button3G.Enabled = false;
                    button4G.Enabled = false;
                    buttonApplyBand.Enabled = false;
                    buttonAuto.Enabled = false;
                    buttonGetCurrentStat.Enabled = false;
                    try
                    {
                        args = " --router-ip " + textBoxIP.Text + " --router-user " + textBoxUsername.Text + " --router-pass " + textBoxPassword.Text + " --network-mode 02 --network-band 7FFFFFFFFFFFFFFF";

                        Process runBat_3G = new Process();
                        runBat_3G.EnableRaisingEvents = true;
                        runBat_3G.StartInfo.FileName = band_tool + @"huawei_band_tool.exe";
                        runBat_3G.StartInfo.Arguments = args + " --win32-exit-instantly";
                        runBat_3G.StartInfo.UseShellExecute = false;
                        runBat_3G.StartInfo.RedirectStandardError = true;
                        runBat_3G.StartInfo.RedirectStandardOutput = true;
                        runBat_3G.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                        runBat_3G.StartInfo.CreateNoWindow = true;
                        runBat_3G.Start();

                        string runBat_3G_text = runBat_3G.StandardOutput.ReadToEnd();
                        runBat_3G.WaitForExit();

                       
                        if (runBat_3G_text.Contains("info:"))
                        {
                            MessageBox.Show("An error occured", "Huawei Router Tool", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LogError("\n[*] An error occured");
                        }
                        else
                        {
                            LogDebug("\n[*] LTE network configured to 3G only.");
                            MessageBox.Show("LTE network configured to 3G only", "Huawei Router Tool", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            net_mode();
                        }

                    }
                    catch
                    {

                    }
                    
                    break;
                case "4G":
                    LogDebug("\n[*] Setting up 4G configuration..");
                    button2G.Enabled = false;
                    button3G.Enabled = false;
                    button4G.Enabled = false;
                    buttonApplyBand.Enabled = false;
                    buttonAuto.Enabled = false;
                    buttonGetCurrentStat.Enabled = false;
                    try
                    {
                        args = " --router-ip " + textBoxIP.Text + " --router-user " + textBoxUsername.Text + " --router-pass " + textBoxPassword.Text + " --network-mode 03 --network-band 3FFFFFFF --lte-band +ALL";

                        Process runBat_4G = new Process();
                        runBat_4G.EnableRaisingEvents = true;
                        runBat_4G.StartInfo.FileName = band_tool + @"huawei_band_tool.exe";
                        runBat_4G.StartInfo.Arguments = args + " --win32-exit-instantly";
                        runBat_4G.StartInfo.UseShellExecute = false;
                        runBat_4G.StartInfo.RedirectStandardError = true;
                        runBat_4G.StartInfo.RedirectStandardOutput = true;
                        runBat_4G.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                        runBat_4G.StartInfo.CreateNoWindow = true;
                        runBat_4G.Start();

                        string runBat_4G_text = runBat_4G.StandardOutput.ReadToEnd();
                        runBat_4G.WaitForExit();

                        

                        if (runBat_4G_text.Contains("info:"))
                        {
                            MessageBox.Show("An error occured", "Huawei Router Tool", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LogError("\n[*] An error occured");
                        }
                        else
                        {
                            LogDebug("\n[*] LTE network configured to 4G only.");
                            MessageBox.Show("LTE network configured to 4G only", "Huawei Router Tool", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            net_mode();
                        }
                    }
                    catch
                    {

                    }
                    
                    break;
                case "Auto":
                    LogDebug("\n[*] Setting up auto configuration..");
                    button2G.Enabled = false;
                    button3G.Enabled = false;
                    button4G.Enabled = false;
                    buttonApplyBand.Enabled = false;
                    buttonAuto.Enabled = false;
                    buttonGetCurrentStat.Enabled = false;
                    try
                    {
                        args = " --router-ip " + textBoxIP.Text + " --router-user " + textBoxUsername.Text + " --router-pass " + textBoxPassword.Text + " --network-mode 00 --network-band 3FFFFFFF --lte-band +ALL";

                        Process runBat_Auto = new Process();
                        runBat_Auto.EnableRaisingEvents = true;
                        runBat_Auto.StartInfo.FileName = band_tool + @"huawei_band_tool.exe";
                        runBat_Auto.StartInfo.Arguments = args + " --win32-exit-instantly";
                        runBat_Auto.StartInfo.UseShellExecute = false;
                        runBat_Auto.StartInfo.RedirectStandardError = true;
                        runBat_Auto.StartInfo.RedirectStandardOutput = true;
                        runBat_Auto.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                        runBat_Auto.StartInfo.CreateNoWindow = true;
                        runBat_Auto.Start();

                        string runBat_Auto_text = runBat_Auto.StandardOutput.ReadToEnd();
                        runBat_Auto.WaitForExit();

                        

                        if (runBat_Auto_text.Contains("info:"))
                        {
                            MessageBox.Show("An error occured", "Huawei Router Tool", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LogError("\n[*] An error occured");
                        }
                        else
                        {
                            MessageBox.Show("LTE network configured to auto", "Huawei Router Tool", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            LogDebug("\n[*] LTE network configured to auto.");

                            net_mode();
                        }
                    }
                    catch
                    {

                    }
                    
                    break;
                case "Refresh":

                    button2G.Enabled = false;
                    button3G.Enabled = false;
                    button4G.Enabled = false;
                    buttonApplyBand.Enabled = false;
                    buttonAuto.Enabled = false;
                    buttonGetCurrentStat.Enabled = false;
                    try
                    {
                        args = " --router-ip " + textBoxIP.Text + " --router-user " + textBoxUsername.Text + " --router-pass " + textBoxPassword.Text + " --show-band";

                        Process runBat_Refresh = new Process();
                        runBat_Refresh.EnableRaisingEvents = true;
                        runBat_Refresh.StartInfo.FileName = band_tool + @"huawei_band_tool.exe";
                        runBat_Refresh.StartInfo.Arguments = args + " --win32-exit-instantly";
                        runBat_Refresh.StartInfo.UseShellExecute = false;
                        runBat_Refresh.StartInfo.RedirectStandardError = true;
                        runBat_Refresh.StartInfo.RedirectStandardOutput = true;
                        runBat_Refresh.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                        runBat_Refresh.StartInfo.CreateNoWindow = true;
                        runBat_Refresh.Start();

                        string runBat_Refresh_text = runBat_Refresh.StandardOutput.ReadToEnd();
                        runBat_Refresh.WaitForExit();

                        
                        if (runBat_Refresh_text.Contains("info:"))
                        {

                        }
                        else
                        {
                            net_mode();
                        }
                    }
                    catch
                    {

                    }
                    
                    break;
                

                case "Ping":
                    Ping();
                    break;

               

                case null:
                    return;
            }
        }

       

        private void Speedtest()
        {
           
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


        private void buttonApplyAPI_Click(object sender, EventArgs e)
        {
            runAPI();

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



                APItype = Get_API(comboBoxAPIList.Text);


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
                        buttonpostAPI.Enabled = false;
                        LogDebug("\n[*] GET : " + comboBoxAPIList.Text + "\n[*] Result : ERROR " + APItype.SelectSingleNode("//error/code").InnerText.ToString() + " [" + ((ErrorCode)(int.Parse(APItype.SelectSingleNode("//error/code").InnerText))).ToString() + "]");
                        Logger.log("GET : " + comboBoxAPIList.Text);
                        Logger.log("Result : ERROR " + APItype.SelectSingleNode("//error/code").InnerText.ToString() + " [" + ((ErrorCode)(int.Parse(APItype.SelectSingleNode("//error/code").InnerText))).ToString() + "]\n");

                    }
                    else
                    {
                        Logger.log("GET : " + comboBoxAPIList.Text + "\n\nResult : \n\n" + richTextBox1.Text + "\n");
                        LogDebug("\n[*] GET : " + comboBoxAPIList.Text + "\n[*] Result : SUCCESS");
                    }
                }
                else
                {
                    richTextBox1.Text = "ERROR : RESPONSE_CONTAIN_EMPTY_DATA";
                    richTextBox2.Text = "";
                    richTextBox2.Text = "";
                    buttonpostAPI.Enabled = false;
                    LogDebug("\n[*] GET : " + comboBoxAPIList.Text + "\n[*] Result : ERROR [RESPONSE_CONTAIN_EMPTY_DATA]");
                }

                if (richTextBox1.Text.Contains("ERROR_SESSION"))
                {
                    metroTabControl1.Enabled = false;
                    loginStatus = "0";
                    buttonLogin.Enabled = true;
                    buttonReboot.Enabled = false;
                    buttonShutdown.Enabled = false;
                }

                APItype.RemoveAll();
                GC.Collect();

            }
            else
            {

            }
            
        }

        

       

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Directory.Exists(Path.GetTempPath() + @"HuaweiRouterTool\"))
            {
                Directory.Delete(path, true);
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

        

        private void saveDeviceInfoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {

                string path = folderBrowserDialog1.SelectedPath;

                if (File.Exists(Path.GetTempPath() + @"\HuaweiRouterTool\routerLog.txt"))
                {
                    try
                    {
                        File.Copy(Path.GetTempPath() + @"\HuaweiRouterTool\routerLog.txt", path + @"\routerLog.txt", true);

                        if (File.Exists(path + @"\routerLog.txt"))
                        {
                            MessageBox.Show("Device info log created.", "Huawei Router Tool", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LogDebug("\n[*] Device info log saved to " + path);
                        }

                    }
                    catch
                    {

                    }

                }
                else
                {
                    MessageBox.Show("An error occured", "Huawei Router Tool", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
                //File.WriteAllText(path, randomstring);
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About about = new About();
            about.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            // Create a new instance of the Form2 class
            API_Remarks api_remarks = new API_Remarks(this.comboBoxAPIList.Text);

            // Show the settings form
            api_remarks.Show();
        }


        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {

            
                

            
        }

        private void comboBoxAPI_SelectedIndexChanged(object sender, EventArgs e)
        {
            runAPI();
        }

        private void comboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {
            

            

        }

      

        private void CopyAll_Click(Object sender, System.EventArgs e)
        {
            ListView.SelectedIndexCollection sel = listView1.SelectedIndices;
            if(sel.Count == 0)
            {
                MessageBox.Show("Please select a device", "Huawei Router Tool", MessageBoxButtons.OK, MessageBoxIcon.Information);

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
                MessageBox.Show("Please select a device", "Huawei Router Tool", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {
                ListViewItem selItem = listView1.Items[sel[0]];
                Clipboard.SetText(selItem.SubItems[1].Text);
            }
           
        }



        

        private void textBoxBlockMAC_TextChanged(object sender, EventArgs e)
        {
            
        }

        //private void textBoxBlockMAC_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    e.Handled = true;
        //}

       

        private void textBoxBlockMAC_GotFocus(object sender, EventArgs e)
        {
            

            //save last Selected textBox
            lastSelected = sender as TextBox;
        }

        

        private void buttonApplyUpnp_Click(object sender, EventArgs e)
        {
            
        }

        private void buttonApplyNAT_Click(object sender, EventArgs e)
        {
            
        }

        private void button19_Click(object sender, EventArgs e)
        {
            SpecialAppSetting SpecialAppSetting = new SpecialAppSetting();
            SpecialAppSetting.ShowDialog();
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
            MacFilter();
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

        

        private void Logout()
        {
            string data;
            data = "<?xml version=\"1.0\" encoding=\"UTF-8\"?><request><Logout>1</Logout></request>";

                var LOGOUT_doc = Post("api/user/logout", data);
            if (LOGOUT_doc.OuterXml == "<?xml version=\"1.0\" encoding=\"UTF-8\"?><response>OK</response>")
            {
                MessageBox.Show("Logged out", "Huawei Router Tool", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LogDebug("\n[*] Logged out.");
                metroTabControl1.Enabled = false;
                buttonLogin.Enabled = true;

            }
            else
            {
                MessageBox.Show("Request failed", "Huawei Router Tool", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LogDebug("\n[*] Request failed.");
            }
        }

        private void comboBoxOLEDPW_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void checkBoxOLED_Password_MouseClick(object sender, MouseEventArgs e)
        {
            
        }

        private void checkBoxEnableWLAN_MouseClick(object sender, MouseEventArgs e)
        {
            
        }

        

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void checkBoxRememberUserpass_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void textBoxPassword_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBoxUsername_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxIP_TextChanged(object sender, EventArgs e)
        {

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



        

        

       

       

      

        private void ButtonLogin_Click_3(object sender, EventArgs e)
        {
            runProc = true;
            if (loginStatus == "1")
            {
                MessageBox.Show("Already logged in.", "Huawei Router Tool", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                Login();

            }

            ip_webpage = textBoxIP.Text;
            /*
            while (true)
            {
                if(DETECT_ERROR_CODE == "ERROR_SESSION")
                {
                    metroTabControl1.Enabled = false;
                    loginStatus = "0";
                    break;
                }
                
            }
            */
        }

        private void ButtonReboot_Click_1(object sender, EventArgs e)
        {
            //Initialise();
            var reboot = "<?xml version:\"1.0\" encoding=\"UTF-8\"?><request><Control>1</Control></request>";
            LogDebug("\n[*] Requesting router to reboot...");
            var REBOOT_doc = Post("api/device/control", reboot);
            if (REBOOT_doc.ToString().Contains("response"))
            {
                if (REBOOT_doc.SelectSingleNode("//response").InnerText == "OK")
                {
                    runProc = false;
                    loginStatus = "";
                    LogDebug("\n[*] Rebooting..");
                    MessageBox.Show("Rebooting..", "Huawei Router Tool", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    metroTabControl1.Enabled = false;
                    buttonShutdown.Enabled = false;
                    buttonReboot.Enabled = false;
                    buttonLogin.Enabled = true;
                }
                else
                {
                    MessageBox.Show("Request fail", "Huawei Router Tool", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    LogDebug("\n[*] Request fail");
                }
            }
            else
            {
                if (REBOOT_doc.OuterXml.ToString().Contains("error"))
                {
                    MessageBox.Show("Fail : " + ((ErrorCode)(int.Parse(REBOOT_doc.SelectSingleNode("//error/code").InnerText))).ToString(), "Huawei Router Tool", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LogDebug("\n[*] Fail : ERROR " + REBOOT_doc.SelectSingleNode("//error/code").InnerText.ToString() + " [" + ((ErrorCode)(int.Parse(REBOOT_doc.SelectSingleNode("//error/code").InnerText))).ToString() + "]");

                }
                else
                {
                    LogDebug("\n[*] Result : Fail");
                };
            }
        }

        private void ButtonShutdown_Click_1(object sender, EventArgs e)
        {
            Initialise();
            var shutdown_data = "<?xml version:\"1.0\" encoding=\"UTF-8\"?><request><Control>4</Control></request>";
            LogDebug("\n[*] Requesting router to shutdown..");
            var shutdown = Post("api/device/control", shutdown_data);
            if (shutdown.ToString().Contains("response"))
            {
                if (shutdown.SelectSingleNode("//response").InnerText == "OK")
                {
                    runProc = false;
                    loginStatus = "";
                    LogDebug("\n[*] Shutting down..");
                    MessageBox.Show("Shutting down..", "Huawei Router Tool", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    metroTabControl1.Enabled = false;
                    buttonShutdown.Enabled = false;
                    buttonReboot.Enabled = false;
                    buttonLogin.Enabled = true;

                }
                else
                {
                    MessageBox.Show("Request fail", "Huawei Router Tool", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    LogDebug("\n[*] Request fail");
                }
            }
            else
            {
                if (shutdown.OuterXml.ToString().Contains("error"))
                {
                    MessageBox.Show("Fail : " + ((ErrorCode)(int.Parse(shutdown.SelectSingleNode("//error/code").InnerText))).ToString(), "Huawei Router Tool", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LogDebug("\n[*] Fail : ERROR " + shutdown.SelectSingleNode("//error/code").InnerText.ToString() + " [" + ((ErrorCode)(int.Parse(shutdown.SelectSingleNode("//error/code").InnerText))).ToString() + "]");

                }
                else
                {
                    LogDebug("\n[*] Result : Fail");
                };
            }
        }

        private void Button13_Click_1(object sender, EventArgs e)
        {
            
        }

        private void MetroButton1_Click(object sender, EventArgs e)
        {
            if (backgroundWorkerCOnnectedDeviceAndMacFilter.IsBusy)
                backgroundWorkerCOnnectedDeviceAndMacFilter.CancelAsync();

            backgroundWorkerCOnnectedDeviceAndMacFilter.RunWorkerAsync();
        }

        private void Button14_Click_1(object sender, EventArgs e)
        {

        }

        private void ButtonBlockDevice_Click(object sender, EventArgs e)
        {
            if (textBoxBlockMAC0.Text == "" && textBoxBlockMAC1.Text == "" && textBoxBlockMAC2.Text == "" && textBoxBlockMAC3.Text == "" && textBoxBlockMAC4.Text == "" && textBoxBlockMAC5.Text == "" && textBoxBlockMAC6.Text == "" && textBoxBlockMAC7.Text == "" && textBoxBlockMAC8.Text == "" && textBoxBlockMAC9.Text == "")
            {
                MessageBox.Show("Enter at least 1 MAC address", "Huawei Router Tool", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {
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

                    LogDebug("\n[*] Blocking device(s)..");
                    var POST_MAC_doc = Post("api/wlan/mac-filter", data);
                    if (POST_MAC_doc.ToString().Contains("response"))
                    {
                        string compare = POST_MAC_doc.OuterXml.ToString();

                        if (POST_MAC_doc == null)
                        {
                            MessageBox.Show("No response");

                        }
                        else
                        {
                            LogDebug("\n" + POST_MAC_doc.OuterXml);

                            if (compare == "<?xml version=\"1.0\" encoding=\"UTF-8\"?><response>OK</response>")
                            {
                                MessageBox.Show("Device blocked", "Huawei Router Tool", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                LogDebug("\n[*] Request accepted.");
                            }
                            else
                            {
                                MessageBox.Show("Request not accepted\n\n" + POST_MAC_doc.OuterXml, "Huawei Router Tool", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                LogDebug("\n[*] Request not accepted.");
                            }

                        }
                    }
                    else
                    {
                        if (POST_MAC_doc.OuterXml.ToString().Contains("error"))
                        {
                            MessageBox.Show("Fail : " + ((ErrorCode)(int.Parse(POST_MAC_doc.SelectSingleNode("//error/code").InnerText))).ToString(), "Huawei Router Tool", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LogDebug("\n[*] Fail : ERROR " + POST_MAC_doc.SelectSingleNode("//error/code").InnerText.ToString() + " [" + ((ErrorCode)(int.Parse(POST_MAC_doc.SelectSingleNode("//error/code").InnerText))).ToString() + "]");


                            DETECT_ERROR_CODE = ((ErrorCode)(int.Parse(POST_MAC_doc.SelectSingleNode("//error/code").InnerText))).ToString();
                            if (DETECT_ERROR_CODE == "ERROR_SESSION")
                            {
                                metroTabControl1.Enabled = false;
                                loginStatus = "0";
                                buttonLogin.Enabled = true;
                                buttonReboot.Enabled = false;
                                buttonShutdown.Enabled = false;
                            }
                        }
                        else
                        {
                            LogDebug("\n[*] Result : Fail");
                        };
                    }

                }
                else
                {
                    MessageBox.Show("MAC address not valid", "Huawei Router Tool", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                MacFilter();

            }
        }

        private void ButtonUnblockAll_Click(object sender, EventArgs e)
        {
            string data = "<request><macfilterimmediatework>1</macfilterimmediatework><WifiMacFilterStatus>0</WifiMacFilterStatus><WifiMacFilterMac0></WifiMacFilterMac0><wifihostname0></wifihostname0><WifiMacFilterMac1></WifiMacFilterMac1><wifihostname1></wifihostname1><WifiMacFilterMac2></WifiMacFilterMac2><wifihostname2></wifihostname2><WifiMacFilterMac3></WifiMacFilterMac3><wifihostname3></wifihostname3><WifiMacFilterMac4></WifiMacFilterMac4><wifihostname4></wifihostname4><WifiMacFilterMac5></WifiMacFilterMac5><wifihostname5></wifihostname5><WifiMacFilterMac6></WifiMacFilterMac6><wifihostname6></wifihostname6><WifiMacFilterMac7></WifiMacFilterMac7><wifihostname7></wifihostname7><WifiMacFilterMac8></WifiMacFilterMac8><wifihostname8></wifihostname8><WifiMacFilterMac9></WifiMacFilterMac9><wifihostname9></wifihostname9></request>";

            LogDebug("\n[*] Blocking all device..");
            var POST_MAC_UNBLOCK_doc = Post("api/wlan/mac-filter", data);
            if (POST_MAC_UNBLOCK_doc.ToString().Contains("response"))
            {
                string compare = POST_MAC_UNBLOCK_doc.OuterXml.ToString();

                if (POST_MAC_UNBLOCK_doc == null)
                {
                    MessageBox.Show("No response");

                }
                else
                {
                    LogDebug("\n" + POST_MAC_UNBLOCK_doc.OuterXml);

                    if (compare == "<?xml version=\"1.0\" encoding=\"UTF-8\"?><response>OK</response>")
                    {
                        MessageBox.Show("All device unblocked", "Huawei Router Tool", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LogDebug("\n[*] Request accepted.");
                    }
                    else
                    {
                        MessageBox.Show("Request not accepted\n\n" + POST_MAC_UNBLOCK_doc.OuterXml, "Huawei Router Tool", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LogDebug("\n[*] Request not accepted.");
                    }

                }

                MacFilter();
            }
            else
            {
                if (POST_MAC_UNBLOCK_doc.OuterXml.ToString().Contains("error"))
                {
                    MessageBox.Show("Fail : " + ((ErrorCode)(int.Parse(POST_MAC_UNBLOCK_doc.SelectSingleNode("//error/code").InnerText))).ToString(), "Huawei Router Tool", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LogDebug("\n[*] Fail : ERROR " + POST_MAC_UNBLOCK_doc.SelectSingleNode("//error/code").InnerText.ToString() + " [" + ((ErrorCode)(int.Parse(POST_MAC_UNBLOCK_doc.SelectSingleNode("//error/code").InnerText))).ToString() + "]");


                    DETECT_ERROR_CODE = ((ErrorCode)(int.Parse(POST_MAC_UNBLOCK_doc.SelectSingleNode("//error/code").InnerText))).ToString();
                    if (DETECT_ERROR_CODE == "ERROR_SESSION")
                    {
                        metroTabControl1.Enabled = false;
                        loginStatus = "0";
                        buttonLogin.Enabled = true;
                        buttonReboot.Enabled = false;
                        buttonShutdown.Enabled = false;
                    }
                }
                else
                {
                    LogDebug("\n[*] Result : Fail");
                };
            }
        }

        private void CheckBoxDMZ_MouseClick(object sender, MouseEventArgs e)
        {
            string data;

            if (checkBoxDMZ.Checked)
            {
                if (textBoxDMZ.Text == "")
                {
                    MessageBox.Show("Enter IP address", "Huawei Router Tool", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                    LogDebug("\n[*] Enabling DMZ..)");

                    data = "<?xml version:\"1.0\" encoding=\"UTF-8\"?><request><DmzStatus>1</DmzStatus><DmzIPAddress>" + textBoxDMZ.Text + "</DmzIPAddress></request>";
                    var DMZ_doc = Post("api/security/dmz", data);
                    if (DMZ_doc.OuterXml == "<?xml version=\"1.0\" encoding=\"UTF-8\"?><response>OK</response>")
                    {
                        MessageBox.Show("DMZ enabled", "Huawei Router Tool", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LogDebug("\n[*] DMZ enabled.");
                    }
                    else
                    {
                        MessageBox.Show("Fail : ERROR " + DMZ_doc.SelectSingleNode("//error/code").InnerText.ToString() + " [" + ((ErrorCode)(int.Parse(DMZ_doc.SelectSingleNode("//error/code").InnerText))).ToString() + "]", "Huawei Router Tool", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LogDebug("\n[*] Fail : ERROR " + DMZ_doc.SelectSingleNode("//error/code").InnerText.ToString() + " [" + ((ErrorCode)(int.Parse(DMZ_doc.SelectSingleNode("//error/code").InnerText))).ToString() + "]");
                        //LogDebug("\n[*] Request failed.");
                        checkBoxDMZ.Checked = false;

                        DETECT_ERROR_CODE = ((ErrorCode)(int.Parse(DMZ_doc.SelectSingleNode("//error/code").InnerText))).ToString();
                        if (DETECT_ERROR_CODE == "ERROR_SESSION")
                        {
                            metroTabControl1.Enabled = false;
                            loginStatus = "0";
                            buttonLogin.Enabled = true;
                            buttonReboot.Enabled = false;
                            buttonShutdown.Enabled = false;
                        }
                    }
                }
            }
            else
            {
                LogDebug("\n[*] Disabling DMZ..)");

                data = "<?xml version:\"1.0\" encoding=\"UTF-8\"?><request><DmzStatus>0</DmzStatus><DmzIPAddress>null</DmzIPAddress></request>";
                var DMZ_doc = Post("api/security/dmz", data);
                if (DMZ_doc.OuterXml == "<?xml version=\"1.0\" encoding=\"UTF-8\"?><response>OK</response>")
                {
                    MessageBox.Show("DMZ disabled", "Huawei Router Tool", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LogDebug("\n[*] DMZ disabled.");
                }
                else
                {
                    MessageBox.Show("Fail : ERROR " + DMZ_doc.SelectSingleNode("//error/code").InnerText.ToString() + " [" + ((ErrorCode)(int.Parse(DMZ_doc.SelectSingleNode("//error/code").InnerText))).ToString() + "]", "Huawei Router Tool", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LogDebug("\n[*] Fail : ERROR " + DMZ_doc.SelectSingleNode("//error/code").InnerText.ToString() + " [" + ((ErrorCode)(int.Parse(DMZ_doc.SelectSingleNode("//error/code").InnerText))).ToString() + "]");
                    //LogDebug("\n[*] Request failed.");
                    checkBoxDMZ.Checked = true;

                    DETECT_ERROR_CODE = ((ErrorCode)(int.Parse(DMZ_doc.SelectSingleNode("//error/code").InnerText))).ToString();
                    if (DETECT_ERROR_CODE == "ERROR_SESSION")
                    {
                        metroTabControl1.Enabled = false;
                        loginStatus = "0";
                        buttonLogin.Enabled = true;
                        buttonReboot.Enabled = false;
                        buttonShutdown.Enabled = false;
                    }
                }
            }
        }

        private void CheckBoxMobileConnection_MouseClick(object sender, MouseEventArgs e)
        {
            string data;

            if (checkBoxMobileConnection.Checked)
            {
                LogDebug("\n[*] Enabling mobile connection..");

                data = "<?xml version:\"1.0\" encoding=\"UTF-8\"?><request><dataswitch>1</dataswitch></request>";
                var MOBILE_CONNECTION_doc = Post("api/dialup/mobile-dataswitch", data);
                if (MOBILE_CONNECTION_doc.OuterXml == "<?xml version=\"1.0\" encoding=\"UTF-8\"?><response>OK</response>")
                {
                    MessageBox.Show("Mobile connection enabled", "Huawei Router Tool", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LogDebug("\n[*] Mobile connection enabled.");
                }
                else
                {
                    MessageBox.Show("Fail : ERROR " + MOBILE_CONNECTION_doc.SelectSingleNode("//error/code").InnerText.ToString() + " [" + ((ErrorCode)(int.Parse(MOBILE_CONNECTION_doc.SelectSingleNode("//error/code").InnerText))).ToString() + "]", "Huawei Router Tool", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LogDebug("\n[*] Fail : ERROR " + MOBILE_CONNECTION_doc.SelectSingleNode("//error/code").InnerText.ToString() + " [" + ((ErrorCode)(int.Parse(MOBILE_CONNECTION_doc.SelectSingleNode("//error/code").InnerText))).ToString() + "]");
                    //LogDebug("\n[*] Request failed.");
                    checkBoxMobileConnection.Checked = false;

                    DETECT_ERROR_CODE = ((ErrorCode)(int.Parse(MOBILE_CONNECTION_doc.SelectSingleNode("//error/code").InnerText))).ToString();
                    if (DETECT_ERROR_CODE == "ERROR_SESSION")
                    {
                        metroTabControl1.Enabled = false;
                        loginStatus = "0";
                        buttonLogin.Enabled = true;
                        buttonReboot.Enabled = false;
                        buttonShutdown.Enabled = false;
                    }
                }
            }
            else
            {
                LogDebug("\n[*] Disbaling mobile connection..");

                data = "<?xml version:\"1.0\" encoding=\"UTF-8\"?><request><dataswitch>0</dataswitch></request>";
                var MOBILE_CONNECTION_doc = Post("api/dialup/mobile-dataswitch", data);
                if (MOBILE_CONNECTION_doc.OuterXml == "<?xml version=\"1.0\" encoding=\"UTF-8\"?><response>OK</response>")
                {
                    MessageBox.Show("Mobile connection disabled", "Huawei Router Tool", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LogDebug("\n[*] Mobile connection disabled.");
                }
                else
                {
                    MessageBox.Show("Fail : ERROR " + MOBILE_CONNECTION_doc.SelectSingleNode("//error/code").InnerText.ToString() + " [" + ((ErrorCode)(int.Parse(MOBILE_CONNECTION_doc.SelectSingleNode("//error/code").InnerText))).ToString() + "]", "Huawei Router Tool", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LogDebug("\n[*] Fail : ERROR " + MOBILE_CONNECTION_doc.SelectSingleNode("//error/code").InnerText.ToString() + " [" + ((ErrorCode)(int.Parse(MOBILE_CONNECTION_doc.SelectSingleNode("//error/code").InnerText))).ToString() + "]");
                    checkBoxMobileConnection.Checked = true;

                    DETECT_ERROR_CODE = ((ErrorCode)(int.Parse(MOBILE_CONNECTION_doc.SelectSingleNode("//error/code").InnerText))).ToString();
                    if (DETECT_ERROR_CODE == "ERROR_SESSION")
                    {
                        metroTabControl1.Enabled = false;
                        loginStatus = "0";
                        buttonLogin.Enabled = true;
                        buttonReboot.Enabled = false;
                        buttonShutdown.Enabled = false;
                    }
                }
            }
        }

        private void CheckBoxUPnP_MouseClick(object sender, MouseEventArgs e)
        {
            string data;

            if (checkBoxUPnP.Checked)
            {
                LogDebug("\n[*] Enabling UPnP..");


                data = "<?xml version:\"1.0\" encoding=\"UTF-8\"?><request><UpnpStatus>1</UpnpStatus></request>";
                var UPnP_doc = Post("api/security/upnp", data);
                if (UPnP_doc.OuterXml == "<?xml version=\"1.0\" encoding=\"UTF-8\"?><response>OK</response>")
                {
                    MessageBox.Show("UPnP enabled", "Huawei Router Tool", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LogDebug("\n[*] UPnP enabled.");
                }
                else
                {
                    
                    MessageBox.Show("Fail : ERROR " + UPnP_doc.SelectSingleNode("//error/code").InnerText.ToString() + " [" + ((ErrorCode)(int.Parse(UPnP_doc.SelectSingleNode("//error/code").InnerText))).ToString() + "]", "Huawei Router Tool", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LogDebug("\n[*] Fail : ERROR " + UPnP_doc.SelectSingleNode("//error/code").InnerText.ToString() + " [" + ((ErrorCode)(int.Parse(UPnP_doc.SelectSingleNode("//error/code").InnerText))).ToString() + "]");
                    checkBoxUPnP.Checked = false;

                    DETECT_ERROR_CODE = ((ErrorCode)(int.Parse(UPnP_doc.SelectSingleNode("//error/code").InnerText))).ToString();
                    //MessageBox.Show(DETECT_ERROR_CODE);
                    if (DETECT_ERROR_CODE == "ERROR_SESSION")
                    {
                        metroTabControl1.Enabled = false;
                        loginStatus = "0";
                        buttonLogin.Enabled = true;
                        buttonReboot.Enabled = false;
                        buttonShutdown.Enabled = false;
                    }
                }


            }
            else
            {
                LogDebug("\n[*] Disabling UPnP..");

                data = "<?xml version:\"1.0\" encoding=\"UTF-8\"?><request><UpnpStatus>0</UpnpStatus></request>";
                var UPnP_doc = Post("api/security/upnp", data);
                if (UPnP_doc.OuterXml == "<?xml version=\"1.0\" encoding=\"UTF-8\"?><response>OK</response>")
                {
                    MessageBox.Show("UPnP disabled", "Huawei Router Tool", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LogDebug("\n[*] UPnP disabled.");
                }
                else
                {
                    
                    MessageBox.Show("Fail : ERROR " + UPnP_doc.SelectSingleNode("//error/code").InnerText.ToString() + " [" + ((ErrorCode)(int.Parse(UPnP_doc.SelectSingleNode("//error/code").InnerText))).ToString() + "]", "Huawei Router Tool", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LogDebug("\n[*] Fail : ERROR " + UPnP_doc.SelectSingleNode("//error/code").InnerText.ToString() + " [" + ((ErrorCode)(int.Parse(UPnP_doc.SelectSingleNode("//error/code").InnerText))).ToString() + "]");
                    checkBoxUPnP.Checked = true;

                    DETECT_ERROR_CODE = ((ErrorCode)(int.Parse(UPnP_doc.SelectSingleNode("//error/code").InnerText))).ToString();
                    //MessageBox.Show(DETECT_ERROR_CODE);
                    if (DETECT_ERROR_CODE == "ERROR_SESSION")
                    {
                        metroTabControl1.Enabled = false;
                        loginStatus = "0";
                        buttonLogin.Enabled = true;
                        buttonReboot.Enabled = false;
                        buttonShutdown.Enabled = false;
                    }
                }
            }
        }

        private void CheckBoxNAT_MouseClick(object sender, MouseEventArgs e)
        {
            string data;

            if (checkBoxNAT.Checked)
            {
                LogDebug("\n[*] Enabling NAT..");

                data = "<?xml version:\"1.0\" encoding=\"UTF-8\"?><request><NATType>1</NATType></request>";
                var NAT_doc = Post("api/security/nat", data);
                if (NAT_doc.OuterXml == "<?xml version=\"1.0\" encoding=\"UTF-8\"?><response>OK</response>")
                {
                    MessageBox.Show("NAT enabled", "Huawei Router Tool", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LogDebug("\n[*] NAT enabled.");
                }
                else
                {
                    MessageBox.Show("Fail : ERROR " + NAT_doc.SelectSingleNode("//error/code").InnerText.ToString() + " [" + ((ErrorCode)(int.Parse(NAT_doc.SelectSingleNode("//error/code").InnerText))).ToString() + "]", "Huawei Router Tool", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LogDebug("\n[*] Fail : ERROR " + NAT_doc.SelectSingleNode("//error/code").InnerText.ToString() + " [" + ((ErrorCode)(int.Parse(NAT_doc.SelectSingleNode("//error/code").InnerText))).ToString() + "]");
                    checkBoxNAT.Checked = false;

                    DETECT_ERROR_CODE = ((ErrorCode)(int.Parse(NAT_doc.SelectSingleNode("//error/code").InnerText))).ToString();
                    if (DETECT_ERROR_CODE == "ERROR_SESSION")
                    {
                        metroTabControl1.Enabled = false;
                        loginStatus = "0";
                        buttonLogin.Enabled = true;
                        buttonReboot.Enabled = false;
                        buttonShutdown.Enabled = false;
                    }
                }

            }
            else
            {
                LogDebug("\n[*] Disabling NAT..");

                data = "<?xml version:\"1.0\" encoding=\"UTF-8\"?><request><NATType>0</NATType></request>";
                var NAT_doc = Post("api/security/nat", data);
                if (NAT_doc.OuterXml == "<?xml version=\"1.0\" encoding=\"UTF-8\"?><response>OK</response>")
                {
                    MessageBox.Show("NAT disabled", "Huawei Router Tool", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LogDebug("\n[*] NAT disabled.");
                }
                else
                {
                    MessageBox.Show("Fail : ERROR " + NAT_doc.SelectSingleNode("//error/code").InnerText.ToString() + " [" + ((ErrorCode)(int.Parse(NAT_doc.SelectSingleNode("//error/code").InnerText))).ToString() + "]", "Huawei Router Tool", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LogDebug("\n[*] Fail : ERROR " + NAT_doc.SelectSingleNode("//error/code").InnerText.ToString() + " [" + ((ErrorCode)(int.Parse(NAT_doc.SelectSingleNode("//error/code").InnerText))).ToString() + "]");
                    checkBoxNAT.Checked = true;

                    DETECT_ERROR_CODE = ((ErrorCode)(int.Parse(NAT_doc.SelectSingleNode("//error/code").InnerText))).ToString();
                    if (DETECT_ERROR_CODE == "ERROR_SESSION")
                    {
                        metroTabControl1.Enabled = false;
                        loginStatus = "0";
                        buttonLogin.Enabled = true;
                        buttonReboot.Enabled = false;
                        buttonShutdown.Enabled = false;
                    }
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
                    MessageBox.Show("Enter Port", "Huawei Router Tool", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                    LogDebug("\n[*] Enabling SIP ALG..");

                    data = "<?xml version:\"1.0\" encoding=\"UTF-8\"?><request><SipStatus>1</SipStatus><SipPort>" + textBoxSIP.Text + "</SipPort></request>";
                    var SIP_doc = Post("api/security/sip", data);
                    if (SIP_doc.OuterXml == "<?xml version=\"1.0\" encoding=\"UTF-8\"?><response>OK</response>")
                    {
                        MessageBox.Show("SIP ALG enabled", "Huawei Router Tool", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LogDebug("\n[*] SIP ALG enabled.");
                    }
                    else
                    {
                        MessageBox.Show("Fail : ERROR " + SIP_doc.SelectSingleNode("//error/code").InnerText.ToString() + " [" + ((ErrorCode)(int.Parse(SIP_doc.SelectSingleNode("//error/code").InnerText))).ToString() + "]", "Huawei Router Tool", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LogDebug("\n[*] Fail : ERROR " + SIP_doc.SelectSingleNode("//error/code").InnerText.ToString() + " [" + ((ErrorCode)(int.Parse(SIP_doc.SelectSingleNode("//error/code").InnerText))).ToString() + "]");
                        checkBoxSIP.Checked = false;

                        DETECT_ERROR_CODE = ((ErrorCode)(int.Parse(SIP_doc.SelectSingleNode("//error/code").InnerText))).ToString();
                        if (DETECT_ERROR_CODE == "ERROR_SESSION")
                        {
                            metroTabControl1.Enabled = false;
                            loginStatus = "0";
                            buttonLogin.Enabled = true;
                            buttonReboot.Enabled = false;
                            buttonShutdown.Enabled = false;
                        }
                    }
                }

                
            }
            else
            {
                LogDebug("\n[*] Disabling SIP ALG..");

                data = "<?xml version:\"1.0\" encoding=\"UTF-8\"?><request><SipStatus>0</SipStatus><SipPort>null</SipPort></request>";
                var SIP_doc = Post("api/security/sip", data);
                if (SIP_doc.OuterXml == "<?xml version=\"1.0\" encoding=\"UTF-8\"?><response>OK</response>")
                {
                    MessageBox.Show("SIP ALG disabled", "Huawei Router Tool", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LogDebug("\n[*] SIP ALG disabled.");
                }
                else
                {
                    MessageBox.Show("Fail : ERROR " + SIP_doc.SelectSingleNode("//error/code").InnerText.ToString() + " [" + ((ErrorCode)(int.Parse(SIP_doc.SelectSingleNode("//error/code").InnerText))).ToString() + "]", "Huawei Router Tool", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LogDebug("\n[*] Fail : ERROR " + SIP_doc.SelectSingleNode("//error/code").InnerText.ToString() + " [" + ((ErrorCode)(int.Parse(SIP_doc.SelectSingleNode("//error/code").InnerText))).ToString() + "]");
                    checkBoxSIP.Checked = true;

                    DETECT_ERROR_CODE = ((ErrorCode)(int.Parse(SIP_doc.SelectSingleNode("//error/code").InnerText))).ToString();
                    if (DETECT_ERROR_CODE == "ERROR_SESSION")
                    {
                        metroTabControl1.Enabled = false;
                        loginStatus = "0";
                        buttonLogin.Enabled = true;
                        buttonReboot.Enabled = false;
                        buttonShutdown.Enabled = false;
                    }
                }
            }
        }

        private void ButtonSaveDevicePW_Click(object sender, EventArgs e)
        {
            if (textBoxCurrentPW.Text == "" || textBoxNewPW.Text == "")
            {
                MessageBox.Show("Enter username and password", "Huawei Router Tool", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {
                LogDebug("\n[*] Applying new router password..");

                byte[] encodeCurrentPW = System.Text.Encoding.UTF8.GetBytes(textBoxCurrentPW.Text);
                byte[] encodeNewPW = System.Text.Encoding.UTF8.GetBytes(textBoxNewPW.Text);


                // convert the byte array to a Base64 string


                string encodedCurrentPW = Convert.ToBase64String(encodeCurrentPW);
                string encodedNewPW = Convert.ToBase64String(encodeNewPW);


                //LogDebug("\n[*] Sending command to API.. (api/user/password)");
                string data;

                data = "<?xml version:\"1.0\" encoding=\"UTF-8\"?><request><Username>" + textBoxUsername.Text + "</Username><CurrentPassword>" + encodedCurrentPW + "</CurrentPassword><NewPassword>" + encodedNewPW + "</NewPassword></request> ";
                var DEVICE_PASSWORD_doc = Post("api/user/password", data);
                if (DEVICE_PASSWORD_doc.OuterXml == "<?xml version=\"1.0\" encoding=\"UTF-8\"?><response>OK</response>")
                {
                    MessageBox.Show("Request accepted", "Huawei Router Tool", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LogDebug("\n[*] Request accepted.");
                    Application.Restart();
                }
                else
                {
                    MessageBox.Show("Fail : " + ((ErrorCode)(int.Parse(DEVICE_PASSWORD_doc.SelectSingleNode("//error/code").InnerText))).ToString(), "Huawei Router Tool", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LogDebug("\n[*] Fail : ERROR " + DEVICE_PASSWORD_doc.SelectSingleNode("//error/code").InnerText.ToString() + " [" + ((ErrorCode)(int.Parse(DEVICE_PASSWORD_doc.SelectSingleNode("//error/code").InnerText))).ToString() + "]");

                    DETECT_ERROR_CODE = ((ErrorCode)(int.Parse(DEVICE_PASSWORD_doc.SelectSingleNode("//error/code").InnerText))).ToString();
                    if (DETECT_ERROR_CODE == "ERROR_SESSION")
                    {
                        metroTabControl1.Enabled = false;
                        loginStatus = "0";
                        buttonLogin.Enabled = true;
                        buttonReboot.Enabled = false;
                        buttonShutdown.Enabled = false;
                    }
                }
            }
        }

        private void CheckBoxFW1_MouseClick(object sender, MouseEventArgs e)
        {
            string data;

            if (checkBoxFW1.Checked)
            {
                LogDebug("\n[*] Enabling device to update without logging in..");

                data = "<?xml version:\"1.0\" encoding=\"UTF-8\"?><request><whitelist_enable>1</whitelist_enable></request>";
                var FW1_doc = Post("api/webserver/white_list_switch", data);
                if (FW1_doc.OuterXml == "<?xml version=\"1.0\" encoding=\"UTF-8\"?><response>OK</response>")
                {
                    MessageBox.Show("Request accepted", "Huawei Router Tool", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LogDebug("\n[*] Request accepted.");
                }
                else
                {
                    MessageBox.Show("Fail : ERROR " + FW1_doc.SelectSingleNode("//error/code").InnerText.ToString() + " [" + ((ErrorCode)(int.Parse(FW1_doc.SelectSingleNode("//error/code").InnerText))).ToString() + "]", "Huawei Router Tool", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LogDebug("\n[*] Fail : ERROR " + FW1_doc.SelectSingleNode("//error/code").InnerText.ToString() + " [" + ((ErrorCode)(int.Parse(FW1_doc.SelectSingleNode("//error/code").InnerText))).ToString() + "]");
                    checkBoxFW1.Checked = false;

                    DETECT_ERROR_CODE = ((ErrorCode)(int.Parse(FW1_doc.SelectSingleNode("//error/code").InnerText))).ToString();
                    if (DETECT_ERROR_CODE == "ERROR_SESSION")
                    {
                        metroTabControl1.Enabled = false;
                        loginStatus = "0";
                        buttonLogin.Enabled = true;
                        buttonReboot.Enabled = false;
                        buttonShutdown.Enabled = false;
                    }
                }
            }
            else
            {
                LogDebug("\n[*] Disabling device to update without logging in..");

                data = "<?xml version:\"1.0\" encoding=\"UTF-8\"?><request><whitelist_enable>0</whitelist_enable></request>";
                var FW1_doc = Post("api/webserver/white_list_switch", data);
                if (FW1_doc.OuterXml == "<?xml version=\"1.0\" encoding=\"UTF-8\"?><response>OK</response>")
                {
                    MessageBox.Show("Request accepted", "Huawei Router Tool", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LogDebug("\n[*] Request accepted.");
                }
                else
                {
                    MessageBox.Show("Fail : ERROR " + FW1_doc.SelectSingleNode("//error/code").InnerText.ToString() + " [" + ((ErrorCode)(int.Parse(FW1_doc.SelectSingleNode("//error/code").InnerText))).ToString() + "]", "Huawei Router Tool", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LogDebug("\n[*] Fail : ERROR " + FW1_doc.SelectSingleNode("//error/code").InnerText.ToString() + " [" + ((ErrorCode)(int.Parse(FW1_doc.SelectSingleNode("//error/code").InnerText))).ToString() + "]");
                    checkBoxFW1.Checked = true;

                    DETECT_ERROR_CODE = ((ErrorCode)(int.Parse(FW1_doc.SelectSingleNode("//error/code").InnerText))).ToString();
                    if (DETECT_ERROR_CODE == "ERROR_SESSION")
                    {
                        metroTabControl1.Enabled = false;
                        loginStatus = "0";
                        buttonLogin.Enabled = true;
                        buttonReboot.Enabled = false;
                        buttonShutdown.Enabled = false;
                    }

                }
            }
        }

        private void CheckBoxFW2_MouseClick(object sender, MouseEventArgs e)
        {
            string data;

            if (checkBoxFW2.Checked)
            {
                LogDebug("\n[*] Enabling device to automatically install critical updates (force update).");

                data = "<?xml version:\"1.0\" encoding=\"UTF-8\"?><request><autoUpdateInterval>0</autoUpdateInterval><auto_update_enable>1</auto_update_enable><server_force_enable>1</server_force_enable></request>";
                var FW2_doc = Post("api/online-update/configuration", data);
                if (FW2_doc.OuterXml == "<?xml version=\"1.0\" encoding=\"UTF-8\"?><response>OK</response>")
                {
                    MessageBox.Show("Request accepted", "Huawei Router Tool", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LogDebug("\n[*] Request accepted.");
                }
                else
                {
                    MessageBox.Show("Fail : ERROR " + FW2_doc.SelectSingleNode("//error/code").InnerText.ToString() + " [" + ((ErrorCode)(int.Parse(FW2_doc.SelectSingleNode("//error/code").InnerText))).ToString() + "]", "Huawei Router Tool", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LogDebug("\n[*] Fail : ERROR " + FW2_doc.SelectSingleNode("//error/code").InnerText.ToString() + " [" + ((ErrorCode)(int.Parse(FW2_doc.SelectSingleNode("//error/code").InnerText))).ToString() + "]");
                    checkBoxFW2.Checked = false;

                    DETECT_ERROR_CODE = ((ErrorCode)(int.Parse(FW2_doc.SelectSingleNode("//error/code").InnerText))).ToString();
                    if (DETECT_ERROR_CODE == "ERROR_SESSION")
                    {
                        metroTabControl1.Enabled = false;
                        loginStatus = "0";
                        buttonLogin.Enabled = true;
                        buttonReboot.Enabled = false;
                        buttonShutdown.Enabled = false;
                    }
                }
            }
            else
            {
                LogDebug("\n[*] Disabling device to automatically install critical updates (force update).");

                data = "<?xml version:\"1.0\" encoding=\"UTF-8\"?><request><autoUpdateInterval>0</autoUpdateInterval><auto_update_enable>1</auto_update_enable><server_force_enable>0</server_force_enable></request>";
                var FW2_doc = Post("api/online-update/configuration", data);
                if (FW2_doc.OuterXml == "<?xml version=\"1.0\" encoding=\"UTF-8\"?><response>OK</response>")
                {
                    MessageBox.Show("Request accepted", "Huawei Router Tool", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LogDebug("\n[*] Request accepted.");
                }
                else
                {
                    MessageBox.Show("Fail : ERROR " + FW2_doc.SelectSingleNode("//error/code").InnerText.ToString() + " [" + ((ErrorCode)(int.Parse(FW2_doc.SelectSingleNode("//error/code").InnerText))).ToString() + "]", "Huawei Router Tool", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LogDebug("\n[*] Fail : ERROR " + FW2_doc.SelectSingleNode("//error/code").InnerText.ToString() + " [" + ((ErrorCode)(int.Parse(FW2_doc.SelectSingleNode("//error/code").InnerText))).ToString() + "]");
                    checkBoxFW2.Checked = true;


                    DETECT_ERROR_CODE = ((ErrorCode)(int.Parse(FW2_doc.SelectSingleNode("//error/code").InnerText))).ToString();
                    if (DETECT_ERROR_CODE == "ERROR_SESSION")
                    {
                        metroTabControl1.Enabled = false;
                        loginStatus = "0";
                        buttonLogin.Enabled = true;
                        buttonReboot.Enabled = false;
                        buttonShutdown.Enabled = false;
                    }
                }
            }
        }

        private void CheckBoxFW3_MouseClick(object sender, MouseEventArgs e)
        {
            string data;

            if (checkBoxFW3.Checked)
            {
                LogDebug("\n[*] Enabling auto-download (auto-update after a restart)");

                data = "<?xml version:\"1.0\" encoding=\"UTF-8\"?><request><auto_update>1</auto_update><ui_download>0</ui_download></request>";
                var FW3_doc = Post("api/online-update/autoupdate-config", data);
                if (FW3_doc.OuterXml == "<?xml version=\"1.0\" encoding=\"UTF-8\"?><response>OK</response>")
                {
                    MessageBox.Show("Request accepted", "Huawei Router Tool", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LogDebug("\n[*] Request accepted.");
                }
                else
                {
                    MessageBox.Show("Fail : ERROR " + FW3_doc.SelectSingleNode("//error/code").InnerText.ToString() + " [" + ((ErrorCode)(int.Parse(FW3_doc.SelectSingleNode("//error/code").InnerText))).ToString() + "]", "Huawei Router Tool", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LogDebug("\n[*] Fail : ERROR " + FW3_doc.SelectSingleNode("//error/code").InnerText.ToString() + " [" + ((ErrorCode)(int.Parse(FW3_doc.SelectSingleNode("//error/code").InnerText))).ToString() + "]");
                    checkBoxFW3.Checked = false;


                    DETECT_ERROR_CODE = ((ErrorCode)(int.Parse(FW3_doc.SelectSingleNode("//error/code").InnerText))).ToString();
                    if (DETECT_ERROR_CODE == "ERROR_SESSION")
                    {
                        metroTabControl1.Enabled = false;
                        loginStatus = "0";
                        buttonLogin.Enabled = true;
                        buttonReboot.Enabled = false;
                        buttonShutdown.Enabled = false;
                    }
                }
            }
            else
            {
                LogDebug("\n[*] Disabling auto-download (auto-update after a restart)");

                data = "<?xml version:\"1.0\" encoding=\"UTF-8\"?><request><auto_update>0</auto_update><ui_download>0</ui_download></request>";
                var FW3_doc = Post("api/online-update/autoupdate-config", data);
                if (FW3_doc.OuterXml == "<?xml version=\"1.0\" encoding=\"UTF-8\"?><response>OK</response>")
                {
                    MessageBox.Show("Request accepted", "Huawei Router Tool", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LogDebug("\n[*] Request accepted.");
                }
                else
                {
                    MessageBox.Show("Fail : ERROR " + FW3_doc.SelectSingleNode("//error/code").InnerText.ToString() + " [" + ((ErrorCode)(int.Parse(FW3_doc.SelectSingleNode("//error/code").InnerText))).ToString() + "]", "Huawei Router Tool", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LogDebug("\n[*] Fail : ERROR " + FW3_doc.SelectSingleNode("//error/code").InnerText.ToString() + " [" + ((ErrorCode)(int.Parse(FW3_doc.SelectSingleNode("//error/code").InnerText))).ToString() + "]");
                    checkBoxFW3.Checked = true;

                    DETECT_ERROR_CODE = ((ErrorCode)(int.Parse(FW3_doc.SelectSingleNode("//error/code").InnerText))).ToString();
                    if (DETECT_ERROR_CODE == "ERROR_SESSION")
                    {
                        metroTabControl1.Enabled = false;
                        loginStatus = "0";
                        buttonLogin.Enabled = true;
                        buttonReboot.Enabled = false;
                        buttonShutdown.Enabled = false;
                    }
                }
            }
        }

        private void ComboBoxDeviceMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            string data;

            if (comboBoxDeviceMode.SelectedItem == "Project Mode")
            {
                LogDebug("\n[*] Changing device mode to Project Mode..");

                data = "<?xml version:\"1.0\" encoding=\"UTF-8\"?><request><mode>0</mode></request>";
                var DEVICE_MODE_doc = Post("api/device/mode", data);
                if (DEVICE_MODE_doc.OuterXml == "<?xml version=\"1.0\" encoding=\"UTF-8\"?><response>OK</response>")
                {
                    MessageBox.Show("Project Mode enabled", "Huawei Router Tool", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LogDebug("\n[*] Project Mode enabled.");
                }
                else
                {
                    

                    MessageBox.Show("Fail : ERROR " + DEVICE_MODE_doc.SelectSingleNode("//error/code").InnerText.ToString() + " [" + ((ErrorCode)(int.Parse(DEVICE_MODE_doc.SelectSingleNode("//error/code").InnerText))).ToString() + "]", "Huawei Router Tool", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LogDebug("\n[*] Fail : ERROR " + DEVICE_MODE_doc.SelectSingleNode("//error/code").InnerText.ToString() + " [" + ((ErrorCode)(int.Parse(DEVICE_MODE_doc.SelectSingleNode("//error/code").InnerText))).ToString() + "]");

                    DETECT_ERROR_CODE = ((ErrorCode)(int.Parse(DEVICE_MODE_doc.SelectSingleNode("//error/code").InnerText))).ToString();
                    if (DETECT_ERROR_CODE == "ERROR_SESSION")
                    {
                        metroTabControl1.Enabled = false;
                        loginStatus = "0";
                        buttonLogin.Enabled = true;
                        buttonReboot.Enabled = false;
                        buttonShutdown.Enabled = false;
                    }
                }
            }
            else if (comboBoxDeviceMode.SelectedItem == "Debug Mode")
            {
                LogDebug("\n[*] Changing device mode to Debug Mode..");

                MessageBox.Show("Internet connection may be disconnected when the debug mode is activated.", "Huawei Router Tool", MessageBoxButtons.OK, MessageBoxIcon.Information);
                data = "<?xml version:\"1.0\" encoding=\"UTF-8\"?><request><mode>1</mode></request>";
                var DEVICE_MODE_doc = Post("api/device/mode", data);
                if (DEVICE_MODE_doc.OuterXml == "<?xml version=\"1.0\" encoding=\"UTF-8\"?><response>OK</response>")
                {
                    MessageBox.Show("Debug Mode enabled", "Huawei Router Tool", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LogDebug("\n[*] Debug Mode enabled.");
                }
                else
                {
                    MessageBox.Show("Fail : ERROR " + DEVICE_MODE_doc.SelectSingleNode("//error/code").InnerText.ToString() + " [" + ((ErrorCode)(int.Parse(DEVICE_MODE_doc.SelectSingleNode("//error/code").InnerText))).ToString() + "]", "Huawei Router Tool", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LogDebug("\n[*] Fail : ERROR " + DEVICE_MODE_doc.SelectSingleNode("//error/code").InnerText.ToString() + " [" + ((ErrorCode)(int.Parse(DEVICE_MODE_doc.SelectSingleNode("//error/code").InnerText))).ToString() + "]");

                    DETECT_ERROR_CODE = ((ErrorCode)(int.Parse(DEVICE_MODE_doc.SelectSingleNode("//error/code").InnerText))).ToString();
                    if (DETECT_ERROR_CODE == "ERROR_SESSION")
                    {
                        metroTabControl1.Enabled = false;
                        loginStatus = "0";
                        buttonLogin.Enabled = true;
                        buttonReboot.Enabled = false;
                        buttonShutdown.Enabled = false;
                    }
                }
            }
        }

        private void ButtonRestore_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Restore all setting to default?", "Huawei Router Tool", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                DialogResult dialogs = MessageBox.Show("Device will be rebooted and the network and router password will be reset to default. Are you sure?", "Huawei Router Tool", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogs == DialogResult.Yes)
                {
                    LogDebug("\n[*] Restoring all setting to default..");
                    string data;

                    data = "<?xml version:\"1.0\" encoding=\"UTF-8\"?><request><Control>2</Control></request>";
                    var RESTORE_SETTING_doc = Post("api/device/control", data);
                    if (RESTORE_SETTING_doc.OuterXml == "<?xml version=\"1.0\" encoding=\"UTF-8\"?><response>OK</response>")
                    {
                        MessageBox.Show("Request accepted.", "Huawei Router Tool", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LogDebug("\n[*] Request accepted.");
                        Application.Restart();


                    }
                    else
                    {

                        MessageBox.Show("Fail : " + ((ErrorCode)(int.Parse(RESTORE_SETTING_doc.SelectSingleNode("//error/code").InnerText))).ToString(), "Huawei Router Tool", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LogDebug("\n[*] Fail : ERROR " + RESTORE_SETTING_doc.SelectSingleNode("//error/code").InnerText.ToString() + " [" + ((ErrorCode)(int.Parse(RESTORE_SETTING_doc.SelectSingleNode("//error/code").InnerText))).ToString() + "]");

                        DETECT_ERROR_CODE = ((ErrorCode)(int.Parse(RESTORE_SETTING_doc.SelectSingleNode("//error/code").InnerText))).ToString();
                        if (DETECT_ERROR_CODE == "ERROR_SESSION")
                        {
                            metroTabControl1.Enabled = false;
                            loginStatus = "0";
                            buttonLogin.Enabled = true;
                            buttonReboot.Enabled = false;
                            buttonShutdown.Enabled = false;
                        }
                    }
                }
                else
                {
                }

            }
            else
            {

            }
        }

        private void Button2G_Click(object sender, EventArgs e)
        {
            if (backgroundWorkerPerformanceTool.IsBusy)
            {
                backgroundWorkerPerformanceTool.CancelAsync();
                backgroundWorkerPerformanceTool.RunWorkerAsync("2G");
            }
            else
            {
                backgroundWorkerPerformanceTool.RunWorkerAsync("2G");

            }
        }

        private void Button3G_Click(object sender, EventArgs e)
        {

        }

        private void Button4G_Click(object sender, EventArgs e)
        {
            if (backgroundWorkerPerformanceTool.IsBusy)
            {
                backgroundWorkerPerformanceTool.CancelAsync();
                backgroundWorkerPerformanceTool.RunWorkerAsync("4G");
            }
            else
            {
                backgroundWorkerPerformanceTool.RunWorkerAsync("4G");

            }
        }

        private void ButtonAuto_Click(object sender, EventArgs e)
        {
            if (backgroundWorkerPerformanceTool.IsBusy)
            {
                backgroundWorkerPerformanceTool.CancelAsync();
                backgroundWorkerPerformanceTool.RunWorkerAsync("Auto");
            }
            else
            {
                backgroundWorkerPerformanceTool.RunWorkerAsync("Auto");

            }
        }

        private void ButtonApplyBand_Click(object sender, EventArgs e)
        {
            if (countCheck == 0)
            {
                MessageBox.Show("Please select at least 1 band!", "Huawei Router Tool", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            else if (countCheck > 3)
            {
                MessageBox.Show("Maximum band only 3!", "Huawei Router Tool", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                checkBoxB1.Checked = false;
                checkBoxB3.Checked = false;
                checkBoxB7.Checked = false;
                checkBoxB8.Checked = false;
                checkBoxB20.Checked = false;
                checkBoxB38.Checked = false;
                checkBoxB40.Checked = false;
                countCheck = 0;
            }
            else
            {
                final = "";
                selected_band = "";
                if (checkBoxB1.Checked)
                {
                    selected_band = "B1";
                }
                if (checkBoxB3.Checked)
                {
                    if (string.IsNullOrEmpty(selected_band))
                    {
                        selected_band = "B3";
                    }
                    else
                    {
                        selected_band = selected_band + "B3";
                    }
                }
                if (checkBoxB7.Checked)
                {
                    if (string.IsNullOrEmpty(selected_band))
                    {
                        selected_band = "B7";
                    }
                    else
                    {
                        selected_band = selected_band + "B7";
                    }
                }
                if (checkBoxB8.Checked)
                {
                    if (string.IsNullOrEmpty(selected_band))
                    {
                        selected_band = "B8";

                    }
                    else
                    {
                        selected_band = selected_band + "B8";
                    }
                }
                if (checkBoxB20.Checked)
                {
                    if (string.IsNullOrEmpty(selected_band))
                    {
                        selected_band = "B20";
                    }
                    else
                    {
                        selected_band = selected_band + "B20";
                    }
                }
                if (checkBoxB38.Checked)
                {
                    if (string.IsNullOrEmpty(selected_band))
                    {
                        selected_band = "B38";
                    }
                    else
                    {
                        selected_band = selected_band + "B38";

                    }
                }
                if (checkBoxB40.Checked)
                {
                    if (string.IsNullOrEmpty(selected_band))
                    {
                        selected_band = "B40";
                    }
                    else
                    {
                        selected_band = selected_band + "B40";
                    }
                }

                final = selected_band.Replace("B1", "+2100").Replace("B3", "+1800").Replace("B7", "+2600").Replace("B8", "+900").Replace("B20", "+800").Replace("B38", "+2600|TDD").Replace("B40", "+2300|TDD");

                //MessageBox.Show(final);



                if (backgroundWorkerApplyBand.IsBusy)
                {
                    backgroundWorkerApplyBand.CancelAsync();
                    backgroundWorkerApplyBand.RunWorkerAsync();

                }
                else
                {
                    backgroundWorkerApplyBand.RunWorkerAsync();
                }
            }

            /*
            if (SetValueForText1 == "")
            {
                MessageBox.Show("Please select at least 1 band", "Huawei Router Tool", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            else
            {
                if (backgroundWorkerApplyBand.IsBusy)
                {
                    backgroundWorkerApplyBand.CancelAsync();
                    backgroundWorkerApplyBand.RunWorkerAsync();

                }
                else
                {
                    backgroundWorkerApplyBand.RunWorkerAsync();
                }

            }
            */
        }

        private void CheckBoxB1_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBoxB1.CheckState == CheckState.Checked)
            {

                countCheck++;

            }

            if (checkBoxB1.CheckState == CheckState.Unchecked)
            {
                countCheck--;

            }
        }

        private void CheckBoxB3_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBoxB3.CheckState == CheckState.Checked)
            {

                countCheck++;

            }

            if (checkBoxB3.CheckState == CheckState.Unchecked)
            {
                countCheck--;

            }
        }

        private void CheckBoxB7_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBoxB7.CheckState == CheckState.Checked)
            {

                countCheck++;

            }

            if (checkBoxB7.CheckState == CheckState.Unchecked)
            {
                countCheck--;

            }
        }

        private void CheckBoxB8_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBoxB8.CheckState == CheckState.Checked)
            {

                countCheck++;

            }

            if (checkBoxB8.CheckState == CheckState.Unchecked)
            {
                countCheck--;

            }
        }

        private void CheckBoxB20_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBoxB20.CheckState == CheckState.Checked)
            {

                countCheck++;

            }

            if (checkBoxB20.CheckState == CheckState.Unchecked)
            {
                countCheck--;

            }
        }

        private void BackgroundWorkerDetect_eerorCODE_DoWork(object sender, DoWorkEventArgs e)
        {

        }

        private void BackgroundWorkerLogin_Reboot_Shutdown_DoWork_1(object sender, DoWorkEventArgs e)
        {

        }

        private void CheckBoxB38_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBoxB38.CheckState == CheckState.Checked)
            {

                countCheck++;

            }

            if (checkBoxB38.CheckState == CheckState.Unchecked)
            {
                countCheck--;

            }
        }

        private void CheckBoxOLED_Password_MouseClick_1(object sender, MouseEventArgs e)
        {
            
            string data;

            if (checkBoxOLED_Password.Checked)
            {
                LogDebug("\n[*] Enabling password on screen..");
                data = "<?xml version:\"1.0\" encoding=\"UTF-8\"?><request><oledshowpassword>1</oledshowpassword></request>";
                var OLED_doc = Post("api/wlan/oled-showpassword", data);
                if (OLED_doc.OuterXml == "<?xml version=\"1.0\" encoding=\"UTF-8\"?><response>OK</response>")
                {
                    LogDebug("\n[*] Password showed on OLED screen");
                    MessageBox.Show("Password showed on OLED screen", "Huawei Router Tool", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Fail : " + ((ErrorCode)(int.Parse(OLED_doc.SelectSingleNode("//error/code").InnerText))).ToString(), "Huawei Router Tool", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LogDebug("\n[*] Fail : ERROR " + OLED_doc.SelectSingleNode("//error/code").InnerText.ToString() + " [" + ((ErrorCode)(int.Parse(OLED_doc.SelectSingleNode("//error/code").InnerText))).ToString() + "]");
                    checkBoxOLED_Password.Checked = false;
                }

            }
            else
            {
                LogDebug("\n[*] Disabling password on screen..");

                data = "<?xml version:\"1.0\" encoding=\"UTF-8\"?><request><oledshowpassword>0</oledshowpassword></request>";
                var OLED_doc = Post("api/wlan/oled-showpassword", data);
                if (OLED_doc.OuterXml == "<?xml version=\"1.0\" encoding=\"UTF-8\"?><response>OK</response>")
                {
                    LogDebug("\n[*] Password hidden on OLED screen");
                    MessageBox.Show("Password hidden on OLED screen", "Huawei Router Tool", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else
                {
                    MessageBox.Show("Fail : " + ((ErrorCode)(int.Parse(OLED_doc.SelectSingleNode("//error/code").InnerText))).ToString(), "Huawei Router Tool", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LogDebug("\n[*] Fail : ERROR " + OLED_doc.SelectSingleNode("//error/code").InnerText.ToString() + " [" + ((ErrorCode)(int.Parse(OLED_doc.SelectSingleNode("//error/code").InnerText))).ToString() + "]");
                    checkBoxOLED_Password.Checked = true;

                }
            }
        }

        private void CheckBoxB40_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBoxB40.CheckState == CheckState.Checked)
            {

                countCheck++;

            }

            if (checkBoxB40.CheckState == CheckState.Unchecked)
            {
                countCheck--;

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
            else
            {



            }

        }

      

        private void ButtonGetCurrentStat_Click(object sender, EventArgs e)
        {
            if (backgroundWorkerPerformanceTool.IsBusy)
            {
                backgroundWorkerPerformanceTool.CancelAsync();
                backgroundWorkerPerformanceTool.RunWorkerAsync("Refresh");
            }
            else
            {
                backgroundWorkerPerformanceTool.RunWorkerAsync("Refresh");

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
            {

            }
            else
            {
                Initialise();
                //Login();
                DialogResult dialogResult = MessageBox.Show("Data : \n\n" + richTextBox2.Text + "\n\nPost to API : " + comboBoxAPIList.Text + "\n\nConfirm?", "Huawei Router Tool", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    var data = "<?xml version:\"1.0\" encoding=\"UTF-8\"?>" + richTextBox2.Text;
                    LogDebug("\n[*] Sending command to API.. (" + comboBoxAPIList.Text + ")");
                    var POST_doc = Post(comboBoxAPIList.Text, data);
                    string compare = POST_doc.OuterXml.ToString();

                    if (POST_doc == null)
                    {
                        MessageBox.Show("No response");

                    }
                    else
                    {

                        if (compare == "<?xml version=\"1.0\" encoding=\"UTF-8\"?><response>OK</response>")
                        {
                            MessageBox.Show("Request accepted", "Huawei Router Tool", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Logger.log("POST : " + comboBoxAPIList.Text + "\n\nResult : Success\n" + compare.ToString() + "\n");

                            runAPI();
                            LogDebug("\n[*] POST : " + comboBoxAPIList.Text + "\n[*] Result : SUCCESS");
                        }
                        else
                        {
                            MessageBox.Show("Error " + POST_doc.SelectSingleNode("//error/code").InnerText.ToString() + " : " + ((ErrorCode)(int.Parse(POST_doc.SelectSingleNode("//error/code").InnerText))).ToString(), "Huawei Router Tool", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LogDebug("\n[*] POST : " + comboBoxAPIList.Text + "\n[*] Result : ERROR " + POST_doc.SelectSingleNode("//error/code").InnerText.ToString() + " [" + ((ErrorCode)(int.Parse(POST_doc.SelectSingleNode("//error/code").InnerText))).ToString() + "]");
                            Logger.log("POST : " + comboBoxAPIList.Text);
                            Logger.log("Result : ERROR " + POST_doc.SelectSingleNode("//error/code").InnerText.ToString() + " [" + ((ErrorCode)(int.Parse(POST_doc.SelectSingleNode("//error/code").InnerText))).ToString() + "]\n");

                        }

                        POST_doc = null;

                    }


                }
                else if (dialogResult == DialogResult.No)
                {
                    //do something else
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

        }

        private void LinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/pearlxcore");

        }


        private void comboBoxWLANSec_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboBoxWLANSec.SelectedItem == "OPEN")
            {
                textBoxPre_shared.Enabled = false;
                textBoxPre_shared.Text = "";
            }
            else
            {
                textBoxPre_shared.Enabled = true;

            }
        }

        

        private void backgroundWorkerLogin_Reboot_Shutdown_DoWork(object sender, DoWorkEventArgs e)
        {
            string ButtonName = (string)e.Argument;

            switch (ButtonName)
            {
                case "Login":
                    
                    break;

                case "Logout":
                        Logout();
                    break;

                case "Reboot":

                    
                    break;

                case "Shutdown":

                    
                    break;

                case null:
                    break;

            }
        }


        private void button4_Click(object sender, EventArgs e)
        {
            LogDebug("\n[*] Starting speedtest..");

            MessageBox.Show("Result may inaccurate. Alternatively you can test the internet speed using speed test official website or apps.", "Huawei Router Tool", MessageBoxButtons.OK, MessageBoxIcon.Information);

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
