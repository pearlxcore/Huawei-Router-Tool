using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_API
{
    public class Program
    {
        static string[] GET_api = new string[] { "api/lan/HostInfo", "api/cradle/factory-mac", "api/led/circle-switch","api/cradle/basic-info","api/cradle/status-info","api/device/autorun-version","api/device/fastbootswitch", "api/device/control",
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
"config/wifi/countryChannel.xml" };
        public static void Main(string[] args)
        {

            //Console.WriteLine("Checking login state..\nPress any key to start.\n");
            //Console.ReadKey();
            //test.loginState("192.168.8.1");
            //Console.ReadKey();




            //HuaweiAPIDLL.HuaweiAPIDLL.loginState("");

            main();
            Console.ReadKey();
           

        }



        static void main()
        {
            Console.Write(" [1] Login\n");
            Console.Write(" [2] Logout\n");
            Console.Write(" [3] Reboot router\n");
            Console.Write(" [4] Shutdown router\n");
            Console.Write(" [5] View GET APIs\n");
            Console.Write(" [6] View POST APIs\n");

            Console.Write("\nChoose operation : ");

            string menu = Console.ReadLine();
            Console.WriteLine("\n");


            switch (menu)
            {
                case "1":
                    HuaweiAPI.UserLogin("192.168.8.1", "admin", "admin");

                    break;

                case "2":
                    HuaweiAPI.UserLogout("192.168.8.1", "admin", "admin");

                    break;

                case "3":
                    HuaweiAPI.RebootDevice("192.168.8.1", "admin", "admin");

                    break;

                case "4":
                    HuaweiAPI.ShutdownDevice("192.168.8.1", "admin", "admin");

                    break;

                case "5":
                    foreach(var item in GET_api)
                    {
                        Console.WriteLine(item);
                    }

                    break;

                case "6":
                    HuaweiAPI.UserLogin("192.168.8.1", "admin", "admin");

                    break;
            }

            Console.WriteLine("\n");

            main();
            Console.ReadLine();

        }

        static void Gray(string msg)
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write(msg);
            Console.ResetColor();
        }
    }
}
