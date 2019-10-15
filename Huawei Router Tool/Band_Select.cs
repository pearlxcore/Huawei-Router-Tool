using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using CheckBox = System.Windows.Forms.CheckBox;

namespace Huawei_Router_Tool_GUI
{
    public partial class Band_Select : Form
    {
        static int countCheck;
        public static string selected_band, final, string_display;
        public Band_Select()
        {
            InitializeComponent();
            
        }

        

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void checkBox7_Click(object sender, EventArgs e)
        {

        }

        private void checkBox2_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox2.CheckState == CheckState.Checked)
            {

                countCheck++;

            }

            if (checkBox2.CheckState == CheckState.Unchecked)
            {
                countCheck--;

            }
        }

        private void checkBox3_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox3.CheckState == CheckState.Checked)
            {

                countCheck++;

            }

            if (checkBox3.CheckState == CheckState.Unchecked)
            {
                countCheck--;

            }
        }

        private void checkBox4_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox4.CheckState == CheckState.Checked)
            {

                countCheck++;

            }

            if (checkBox4.CheckState == CheckState.Unchecked)
            {
                countCheck--;

            }
        }

        private void checkBox5_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox5.CheckState == CheckState.Checked)
            {

                countCheck++;

            }

            if (checkBox5.CheckState == CheckState.Unchecked)
            {
                countCheck--;

            }
        }

        private void checkBox6_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox6.CheckState == CheckState.Checked)
            {

                countCheck++;

            }

            if (checkBox6.CheckState == CheckState.Unchecked)
            {
                countCheck--;

            }
        }

        private void checkBox7_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox7.CheckState == CheckState.Checked)
            {

                countCheck++;

            }

            if (checkBox7.CheckState == CheckState.Unchecked)
            {
                countCheck--;

            }
        }

        private void checkBox1_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox1.CheckState == CheckState.Checked)
            {

                countCheck++;

            }

            if (checkBox1.CheckState == CheckState.Unchecked)
            {
                countCheck--;

            }
        }

        

        private void button1_Click(object sender, EventArgs e)
        {
            if (countCheck == 0)
            {
                MessageBox.Show("Please select at least 1 band!", "Huawei Router Tool", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            if (countCheck > 3)
            {
                MessageBox.Show("Maximum band only 3!", "Huawei Router Tool", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                checkBox1.Checked = false;
                checkBox2.Checked = false;
                checkBox3.Checked = false;
                checkBox4.Checked = false;
                checkBox5.Checked = false;
                checkBox6.Checked = false;
                checkBox7.Checked = false;
                countCheck = 0;
            }
            else
            {
                textBox1.Text = "";
                final = "";
                selected_band = "";
                if (checkBox1.Checked)
                {
                    selected_band = "B1";
                }
                if (checkBox2.Checked)
                {
                    if(string.IsNullOrEmpty(selected_band))
                    {
                        selected_band = "B3";
                    }
                    else
                    {
                        selected_band = selected_band + "B3";
                    }
                }
                if (checkBox3.Checked)
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
                if (checkBox4.Checked)
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
                if (checkBox5.Checked)
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
                if (checkBox6.Checked)
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
                if (checkBox7.Checked)
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
                textBox1.Text= final;
                Form1.SetValueForText1 = textBox1.Text;
                countCheck = 0;
                this.Close();
                
                

            }
        }
    }
}
