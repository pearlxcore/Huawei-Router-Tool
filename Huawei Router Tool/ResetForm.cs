using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Huawei_Router_Tool_GUI
{
    public class ResetForm
    {
        public static void ResetAllControls(Control form)
        {
            foreach (Control control in form.Controls)
            {
                if (control is TextBox)
                {
                    TextBox textBox = (TextBox)control;
                    textBox.Text = null;
                }

                if (control is ComboBox)
                {
                    ComboBox comboBox = (ComboBox)control;
                    if (comboBox.Items.Count > 0)
                        comboBox.SelectedIndex = 0;
                }

                if (control is CheckBox)
                {
                    CheckBox checkBox = (CheckBox)control;
                    checkBox.Checked = false;
                }

                if (control is ListBox)
                {
                    ListBox listBox = (ListBox)control;
                    listBox.ClearSelected();
                }

                if (control is ListView)
                {
                    ListView listBox = (ListView)control;
                    listBox.Clear();
                }

                if (control is CheckedListBox)
                {
                    CheckedListBox CheckedListBox = (CheckedListBox)control;
                    CheckedListBox.Items.Clear();
                }

                if (control is RichTextBox)
                {
                    RichTextBox RichTextBox1 = (RichTextBox)control;
                    if(RichTextBox1.Name == "richTextBox1")
                    {
                        RichTextBox1.Clear();
                    }

                    RichTextBox RichTextBox2 = (RichTextBox)control;
                    if (RichTextBox2.Name == "richTextBox2")
                    {
                        RichTextBox2.Clear();
                    }
                }
            }
        }
    }
}
