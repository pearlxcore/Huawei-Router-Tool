using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Huawei_Router_Tool_GUI
{
    public class modifyPassword
    {
        //        static string base64EncodeChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/";
        //        static int base64DecodeChars = new string[
        //-1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
        //-1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
        //-1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, 62, -1, -1, -1, 63,
        //52, 53, 54, 55, 56, 57, 58, 59, 60, 61, -1, -1, -1, -1, -1, -1,
        //-1, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14,
        //15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, -1, -1, -1, -1, -1,
        //-1, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40,
        //41, 42, 43, 44, 45, 46, 47, 48, 49, 50, 51, -1, -1, -1, -1, -1];
        static int MODIFYPASSWORD_SHOW = 108008;
        static int hilink_login_eable = 1;
        static int hilink_login_diable = 0;
        static int hilink_login_status = 0;
        static string error = "";
        static string _current_pwd = "";
        static string _new_pwd = "";
        static string _confirm_pwd = "";
        static string _username = "";
        private static string _CurrentSessionID;
        private static string _CurrentToken;

        private static bool validatePassword(string current_pwd, string new_pwd, string confirm_pwd)
        {
            //clearAllErrorLabel(); //todo
            if (current_pwd == string.Empty)
            { error = "system_hint_current_password_empty"; return false; }
            //else if (checkPasswordChar(current_pwd) == false) // todo checkPasswordChar()
            //{ error = "system_hint_wrong_password"; return false; }
            else if (new_pwd == string.Empty)
            { error = "system_hint_new_password_empty"; return false; }
            else if (confirm_pwd == string.Empty)
            { error = "system_hint_new_confirm_password_empty"; return false; }
            else if (char.IsWhiteSpace(new_pwd[0]))
            { error = "input_cannot_begin_with_space"; return false; }
            else if (char.IsWhiteSpace(confirm_pwd[0]))
            { error = "input_cannot_begin_with_space"; return false; }
            else if (new_pwd != confirm_pwd)
            { error = "IDS_modify_password_wrong_msg"; return false; }
            //else if (checkPasswordChar(new_pwd) == false) // todo checkPasswordChar()
            //{ error = "IDS_password_type_notes"; return false; }
            else if (new_pwd == current_pwd)
            { error = "IDS_common_same_password_error"; return false; }
            else if (new_pwd.Length < 8)
            { error = "must_notless_then 8_char"; return false; }
            else { error = "wrong_password_?"; }


            return true;
        }

        public static string start()
        {
            string test = "";
            Form1 f1 = new Form1();
            var _validate = validatePassword(_current_pwd, _new_pwd, _confirm_pwd);
            if (_validate)
            {
                if (f1.scram_login() == false)
                {
                    //get session id n token
                    var Sestoken = f1.Get("api/webserver/SesTokInfo");
                    _CurrentSessionID = Sestoken.SelectSingleNode("//response/SesInfo").InnerText;
                    _CurrentToken = Sestoken.SelectSingleNode("//response/TokInfo").InnerText;

                    var data = "<?xml version:\"1.0\" encoding=\"UTF-8\"?><request><Username>" + _username + "</Username><CurrentPassword>" + _current_pwd + "</CurrentPassword><NewPassword>" + _new_pwd + "</NewPassword ></request>";
                    var POST_doc = f1.PostSMS("api/user/password_scram", data);
                    return test = POST_doc.ToString();

                }
            }

            return test;
        }

        public static string GetXMLFromObject(object o)
        {
            StringWriter sw = new StringWriter();
            XmlTextWriter tw = null;
            try
            {
                XmlSerializer serializer = new XmlSerializer(o.GetType());
                tw = new XmlTextWriter(sw);
                serializer.Serialize(tw, o);
            }
            catch (Exception ex)
            {
                //Handle Exception Code
            }
            finally
            {
                sw.Close();
                if (tw != null)
                {
                    tw.Close();
                }
            }
            return sw.ToString();
        }

        private static string XSSResolveCannotParseChar(string xmlStr)
        {
            if (xmlStr != "undefined" && xmlStr != null && xmlStr != "")
            {
                return xmlStr.Replace("&", "&amp;").Replace("'", "&apos;").Replace("\"", "&quot;").Replace("<", "&lt;").Replace(">", "gt;");
            }

            return xmlStr;
        }

        //private bool checkPasswordChar(string str)
        //{
        //    //int i;
        //    //char char_i;
        //    //int num_char_i;
        //    //if (str == "")
        //    //{
        //    //    return true;
        //    //}
        //    //for (i = 0; i < str.Length; i++)
        //    //{
        //    //    char_i = str[i];
        //    //    num_char_i = char_i.charCodeAt();
        //    //    if ((num_char_i > MACRO_SUPPORT_CHAR_MAX) || (num_char_i < MACRO_SUPPORT_CHAR_MIN))
        //    //    {
        //    //        return false;
        //    //    }
        //    //    else
        //    //    {
        //    //        continue;
        //    //    }
        //    //}
        //    //return true;
        //}
    }

    public class NewPwd
    {
        public string Current_pwd { get; set; }
        public string New_pwd { get; set; }
        public string Confirm_pwd { get; set; }

    }


}
