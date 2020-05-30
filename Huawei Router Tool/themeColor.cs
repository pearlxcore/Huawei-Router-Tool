using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework.Controls;

namespace Huawei_Router_Tool_GUI
{
    class themeColor
    {
        public void Theme()
        {
            MessageBox.Show("method called");

            Form1 f1 = new Form1();
            foreach (Control x in f1.Controls)
            {
                if (x is MetroFormBase)
                {
                    MessageBox.Show("x is MetroFormBase");
                    ((MetroFormBase)x).Text = "TESTT";

                }
            }
        }
    } 
}
