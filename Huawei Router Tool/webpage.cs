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
    public partial class webpage : MetroFramework.Forms.MetroForm
    {
        public webpage()
        {
            InitializeComponent();
            try
            {
                webBrowser1.Navigate(Form1.ip_webpage + "/html/home.html");

            }
            catch
            {
                MessageBox.Show("Error occured.", "Huawei Router Tool", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }

        }

        private void ButtonLogin_Click(object sender, EventArgs e)
        {
            webBrowser1.Navigate(Form1.ip_webpage + "/html/deviceinformation.html");
        }

        private void MetroButton1_Click(object sender, EventArgs e)
        {
            webBrowser1.Navigate(Form1.ip_webpage + "/html/wlanbasicsettings.html");

        }

        private void ButtonStatistic_Click(object sender, EventArgs e)
        {
            webBrowser1.Navigate(Form1.ip_webpage + "/html/statistic.html");

        }

        private void ButtonNetworkSetting_Click(object sender, EventArgs e)
        {
            webBrowser1.Navigate(Form1.ip_webpage + "/html/mobilenetworksettings.html");

        }

        private void ButtonDHCP_Click(object sender, EventArgs e)
        {
            webBrowser1.Navigate(Form1.ip_webpage + "/html/dhcp.html");

        }

        private void ButtonSystemSetting_Click(object sender, EventArgs e)
        {
            webBrowser1.Navigate(Form1.ip_webpage + "/html/systemsettings.html");

        }
    }
}
