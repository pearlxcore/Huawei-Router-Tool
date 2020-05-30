using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace ObjectToXML
{
    class Program
    {
        public List<NewPwd> ListData { get; set; }
        static void Main(string[] args)
        {
            //NewPwd ListData = new NewPwd();
            //ListData.Current_pwd = "test1";
            //ListData.New_pwd = "test2";
            //ListData.Confirm_pwd = "test3";

            //string xml = GetXMLFromObject(ListData);

            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "");
            XmlSerializer s = new XmlSerializer(typeof(NewPwd));
            StringWriter myWriter = new StringWriter();

            object[] myObject = { "test1", "test2", "test3" };
            s.Serialize(myWriter, myObject, ns);

            Console.WriteLine(s);
            Console.ReadKey();
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
    }
    public class NewPwd
    {
        public string Current_pwd { get; set; }
        public string New_pwd { get; set; }
        public string Confirm_pwd { get; set; }

    }
}
