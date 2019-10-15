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
    public partial class About : Form
    {
        public About()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void About_Load(object sender, EventArgs e)
        {
            richTextBox1.Text = "V3 Changelog : \n\n- Added : View & manage connected device.\n- Added : Device setting.\n- Added : More APIs to API list.\n- Removed : Speedtest tool. Its broken suddenly >.<\n\nV2 Changelog :\n\n- Added : Reboot and shutdown option (Reported only works on certain Huawei router).\n- Added : Export device information.\n- Added : Write to API (Do at your own risk!).\n- Improved : Output value for Monitor Device.\n- Improved : Selection band option.\n- Improved : Speed test display a better result.\n- Improved : Logging output.\n\nV1 Changelog :\n\n- Initial release.";
        }

        private void linkLabel1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/pearlxcore");
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.paypal.com/paypalme2/pearlxcore?locale.x=en_US");
        
        }
    }
}
