using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Huawei_Router_Tool_GUI
{
    public partial class API_Remarks : Form
    {
        public API_Remarks(string text)
        {
            InitializeComponent();

            if (text == "api/device/control")
            {
                string texts = "Method: POST\nrequest:\nCode:\n<request>\n <control></control>\n</request>\n\nanswer:\nCode:\n<Response>OK</response>\n\nRemarks:\nControl:\n\n1 - restart of the device\n2 - reset the configuration(no information on how to enter a saved configuration file. You may use the file http://192.168.8.1/nvram.bak )\n3 - backup configuration(configuration is available at http://192.168.8.1/nvram.bak . This is a base64-encoded)\n4 - shutdown";
                richTextBox1.Text = texts;

            }
        }
    }
}
