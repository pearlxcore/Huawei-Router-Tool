using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Drawing.Drawing2D;


namespace Huawei_Router_Tool_GUI
{
    public partial class SignalMonitor : MetroFramework.Forms.MetroForm
    {
        private int GridlinesOffset = 0;
        TextBox txtRSRQ = Application.OpenForms["Form1"].Controls["textBoxRSRQ"] as TextBox;

        public SignalMonitor()
        {
            if (Form1.SeriesRSRQ != null && Form1.SeriesRSRP != null && Form1.SeriesRSSI != null && Form1.Seriessinr != null && Form1.nei_cellid != null && Form1.txpower != null)
            {
                InitializeComponent();
                initializedChart();




                for (int i = 0; i < 60; i++)
                {


                    chart1.Series["RSRQ"].Points.AddY(Form1.SeriesRSRQ);
                    chart2.Series["RSSI"].Points.AddY(Form1.SeriesRSSI);
                    chart3.Series["RSRP"].Points.AddY(Form1.SeriesRSRP);
                    chart4.Series["SINR"].Points.AddY(Form1.Seriessinr);

                }

                timer1.Start();
            }
            else
            {
                MessageBox.Show("An error occured", "Huawei Router Tool", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }





        }

        private void initializedChart()
        {
            
            //set color(optional)
            Color axisColor = Color.FromArgb(100, 100, 100);
            Color gridColor = Color.FromArgb(200, 200, 200);
            Color backColor = Color.FromArgb(246, 246, 246);
            Color lineColor = Color.FromArgb(50, 50, 200);

            //label font
            chart1.ChartAreas[0].AxisY.LabelStyle.Font = new System.Drawing.Font("Segoe UI", 8f);
            chart1.ChartAreas[0].AxisX.LabelStyle.Enabled = false;
            chart1.ChartAreas[0].AxisY.LabelStyle.Enabled = true;
            chart1.ChartAreas[0].AxisY.LabelStyle.ForeColor = Color.YellowGreen;

            //graph color
            chart1.Series[0].Color = Color.YellowGreen;
            //border
            chart1.ChartAreas[0].BorderWidth = 1;
            chart1.ChartAreas[0].BorderColor = Color.Silver;
            chart1.ChartAreas[0].BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;

            //y-axis x-axis only color
            chart1.ChartAreas[0].AxisX.LineColor = Color.Silver;
            chart1.ChartAreas[0].AxisY.LineColor = Color.Silver;

            //back color
            chart1.ChartAreas[0].BackColor = Color.Black;

            //gridline
            chart1.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.Silver;
            chart1.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.Silver;
            chart1.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            chart1.ChartAreas[0].AxisY.MajorGrid.Interval = 1;

            // 60 seconds interval.
            chart1.ChartAreas[0].AxisX.Minimum = 0;
            chart1.ChartAreas[0].AxisX.Maximum = 60;
            chart1.ChartAreas[0].AxisY.Minimum = -12;
            chart1.ChartAreas[0].AxisY.Maximum = -2;

            //tickmark disable
            chart1.ChartAreas[0].AxisX.MajorTickMark.Enabled = false;
            chart1.ChartAreas[0].AxisY.MajorTickMark.Enabled = false;


            //////////////////////////////////
            ///

            //label font
            chart2.ChartAreas[0].AxisY.LabelStyle.Font = new System.Drawing.Font("Segoe UI", 8f);
            chart2.ChartAreas[0].AxisX.LabelStyle.Enabled = false;
            chart2.ChartAreas[0].AxisY.LabelStyle.Enabled = true;
            chart2.ChartAreas[0].AxisY.LabelStyle.ForeColor = Color.YellowGreen;

            //graph color
            chart2.Series[0].Color = Color.YellowGreen;
            //border
            chart2.ChartAreas[0].BorderWidth = 1;
            chart2.ChartAreas[0].BorderColor = Color.Silver;
            chart2.ChartAreas[0].BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;

            //y-axis x-axis only color
            chart2.ChartAreas[0].AxisX.LineColor = Color.Silver;
            chart2.ChartAreas[0].AxisY.LineColor = Color.Silver;

            //back color
            chart2.ChartAreas[0].BackColor = Color.Black;

            //gridline
            chart2.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.Silver;
            chart2.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.Silver;
            chart2.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            chart2.ChartAreas[0].AxisY.MajorGrid.Interval = 4;

            // 60 seconds interval.
            chart2.ChartAreas[0].AxisX.Minimum = 0;
            chart2.ChartAreas[0].AxisX.Maximum = 60;
            chart2.ChartAreas[0].AxisY.Minimum = -95;
            chart2.ChartAreas[0].AxisY.Maximum = -55;

            //tickmark disable
            chart2.ChartAreas[0].AxisX.MajorTickMark.Enabled = false;
            chart2.ChartAreas[0].AxisY.MajorTickMark.Enabled = false;



            ///////////////////////////////////////////////////////////////////
            ///

            //label font
            chart3.ChartAreas[0].AxisY.LabelStyle.Font = new System.Drawing.Font("Segoe UI", 8f);
            chart3.ChartAreas[0].AxisX.LabelStyle.Enabled = false;
            chart3.ChartAreas[0].AxisY.LabelStyle.Enabled = true;
            chart3.ChartAreas[0].AxisY.LabelStyle.ForeColor = Color.YellowGreen;

            //graph color
            chart3.Series[0].Color = Color.YellowGreen;
            //border
            chart3.ChartAreas[0].BorderWidth = 1;
            chart3.ChartAreas[0].BorderColor = Color.Silver;
            chart3.ChartAreas[0].BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;

            //y-axis x-axis only color
            chart3.ChartAreas[0].AxisX.LineColor = Color.Silver;
            chart3.ChartAreas[0].AxisY.LineColor = Color.Silver;

            //back color
            chart3.ChartAreas[0].BackColor = Color.Black;

            //gridline
            chart3.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.Silver;
            chart3.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.Silver;
            chart3.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            chart3.ChartAreas[0].AxisY.MajorGrid.Interval = 4;

            // 60 seconds interval.
            chart3.ChartAreas[0].AxisX.Minimum = 0;
            chart3.ChartAreas[0].AxisX.Maximum = 60;
            chart3.ChartAreas[0].AxisY.Minimum = -120;
            chart3.ChartAreas[0].AxisY.Maximum = -80;

            //tickmark disable
            chart3.ChartAreas[0].AxisX.MajorTickMark.Enabled = false;
            chart3.ChartAreas[0].AxisY.MajorTickMark.Enabled = false;


            /////////////////////////////////////////////////////////////////
            ///

            //label font
            chart4.ChartAreas[0].AxisY.LabelStyle.Font = new System.Drawing.Font("Segoe UI", 8f);
            chart4.ChartAreas[0].AxisX.LabelStyle.Enabled = false;
            chart4.ChartAreas[0].AxisY.LabelStyle.Enabled = true;
            chart4.ChartAreas[0].AxisY.LabelStyle.ForeColor = Color.YellowGreen;

            //graph color
            chart4.Series[0].Color = Color.YellowGreen;
            //border
            chart4.ChartAreas[0].BorderWidth = 1;
            chart4.ChartAreas[0].BorderColor = Color.Silver;
            chart4.ChartAreas[0].BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;

            //y-axis x-axis only color
            chart4.ChartAreas[0].AxisX.LineColor = Color.Silver;
            chart4.ChartAreas[0].AxisY.LineColor = Color.Silver;

            //back color
            chart4.ChartAreas[0].BackColor = Color.Black;

            //gridline
            chart4.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.Silver;
            chart4.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.Silver;
            chart4.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            chart4.ChartAreas[0].AxisY.MajorGrid.Interval = 1;

            // 60 seconds interval.
            chart4.ChartAreas[0].AxisX.Minimum = 0;
            chart4.ChartAreas[0].AxisX.Maximum = 60;
            chart4.ChartAreas[0].AxisY.Minimum = 6;
            chart4.ChartAreas[0].AxisY.Maximum = 16;

            //tickmark disable
            chart4.ChartAreas[0].AxisX.MajorTickMark.Enabled = false;
            chart4.ChartAreas[0].AxisY.MajorTickMark.Enabled = false;


        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            if (Form1.SeriesRSRQ != null && Form1.SeriesRSRP != null && Form1.SeriesRSSI != null && Form1.Seriessinr != null && Form1.nei_cellid != null && Form1.txpower != null)
            {
                label8.Text = Form1.nei_cellid.Replace("No1:", "No 1 : ").Replace("No2:", ", No 2 : ").Replace("No3:", ", No 3 : ").Replace("No4:", ", No 4 : ").Replace("No5:", ", No 5 : ").Replace("No6:", ", No 6 : ").Replace("No7:", ", No 7 : ").Replace("No8:", ", No 8 : ").Replace("No9:", ", No 9 : ").Replace("No10:", ", No 10 : ");
                label9.Text = Form1.txpower.Replace("PPusch:", "PPusch : ").Replace("PPucch:", ", PPucch : ").Replace("PSrs:", ", PSrs : ").Replace("PPrach:", ", PPrach : ");
                labelCELL_ID.Text = Form1.cell_id;
                labelUPBW.Text = Form1.ulbandwidth;
                labelDLBW.Text = Form1.dlbandwidth;
                labelEARFCN.Text = Form1.earfcn;
                labelDLRate.Text = Form1.DownloadRate;
                labelULRate.Text = Form1.UploadRate;

                //rsrq
                int rsrq;
                if (int.TryParse(Form1.SeriesRSRQ, out rsrq))
                {
                    if (rsrq >= -5)
                    {

                        progressBar1.ForeColor = Color.FromArgb(0, 153, 0);
                        progressBar1.Value = 100;
                        progressBar1.Value = 99;
                    }
                    else if (rsrq >= -9 && rsrq <= -6)
                    {

                        progressBar1.ForeColor = Color.FromArgb(0, 128, 255);
                        progressBar1.Value = 76;
                        progressBar1.Value = 75;

                    }
                    else if (rsrq >= -12 && rsrq <= -9)
                    {
                        progressBar1.ForeColor = Color.FromArgb(204, 204, 0);

                        progressBar1.Value = 51;
                        progressBar1.Value = 50;

                    }
                    else if (rsrq <= -12)
                    {
                        progressBar1.ForeColor = Color.FromArgb(204, 0, 0);

                        progressBar1.Value = 26;
                        progressBar1.Value = 25;

                    }
                    label20.Text = Form1.RSRQ;
                }
                else
                {
                    MessageBox.Show("RSRQ parsing error");
                }


                //rsrp
                int rsrp;
                if (int.TryParse(Form1.SeriesRSRP, out rsrp))
                {
                    if (rsrp >= -84)
                    {
                        progressBar2.ForeColor = Color.FromArgb(0, 153, 0);
                        progressBar2.Value = 100;
                        progressBar2.Value = 99;
                    }
                    else if (rsrp <= -85 && rsrp >= -102)
                    {
                        progressBar2.ForeColor = Color.FromArgb(0, 128, 255);
                        progressBar2.Value = 76;
                        progressBar2.Value = 75;
                    }
                    else if (rsrp <= -103 && rsrp >= -111)
                    {
                        progressBar2.ForeColor = Color.FromArgb(204, 204, 0);

                        progressBar2.Value = 51;
                        progressBar2.Value = 50;
                    }
                    else if (rsrp <= -112)
                    {
                        progressBar2.ForeColor = Color.FromArgb(204, 0, 0);

                        progressBar2.Value = 26;
                        progressBar2.Value = 25;
                    }
                    label21.Text = Form1.RSRP.ToString();
                }
                else
                {
                    MessageBox.Show("RSRP parsing error");

                }


                //rssi
                int rssi;
                if (int.TryParse(Form1.SeriesRSSI, out rssi))
                {
                    if (rssi >= -65)
                    {
                        progressBar3.ForeColor = Color.FromArgb(0, 153, 0);
                        progressBar3.Value = 100;
                        progressBar3.Value = 99;
                    }
                    else if (rssi <= -66 && rssi >= -75)
                    {
                        progressBar3.ForeColor = Color.FromArgb(0, 128, 255);
                        progressBar3.Value = 76;
                        progressBar3.Value = 75;
                    }
                    else if (rssi <= -75 && rssi >= -85)
                    {
                        progressBar3.ForeColor = Color.FromArgb(204, 204, 0);
                        progressBar3.Value = 51;
                        progressBar3.Value = 50;
                    }
                    else if (rssi <= -84)
                    {
                        progressBar3.ForeColor = Color.FromArgb(204, 0, 0);
                        progressBar3.Value = 26;
                        progressBar3.Value = 25;
                    }
                    label26.Text = Form1.RSSI.ToString();
                }
                else
                {
                    MessageBox.Show("SINR parsing error");
                }

                //sinr
                int sinrr;
                if (int.TryParse(Form1.Seriessinr, out sinrr))
                {
                    if (sinrr >= 12.5)
                    {
                        progressBar4.ForeColor = Color.FromArgb(0, 153, 0);
                        progressBar4.Value = 100;
                        progressBar4.Value = 99;
                    }
                    else if (sinrr >= 10 && sinrr <= 12.4)
                    {
                        progressBar4.ForeColor = Color.FromArgb(0, 128, 255);
                        progressBar4.Value = 76;
                        progressBar4.Value = 75;
                    }
                    else if (sinrr >= 7 && sinrr <= 9)
                    {
                        progressBar4.ForeColor = Color.FromArgb(204, 204, 0);
                        progressBar4.Value = 51;
                        progressBar4.Value = 50;
                    }
                    else if (sinrr <= 6)
                    {
                        progressBar4.ForeColor = Color.FromArgb(204, 0, 0);
                        progressBar4.Value = 26;
                        progressBar4.Value = 25;
                    }
                    label27.Text = Form1.sinr.ToString();
                }
                else
                {
                    MessageBox.Show("SINR parsing error");
                }


                //////////////////
                chart1.Series["RSRQ"].Points.AddY(Form1.SeriesRSRQ);
                chart1.Series["RSRQ"].Points.RemoveAt(0);

                // Make gridlines move.
                chart1.ChartAreas[0].AxisX.MajorGrid.IntervalOffset = -GridlinesOffset;

                // Calculate next offset.
                GridlinesOffset++;
                GridlinesOffset %= (int)chart1.ChartAreas[0].AxisX.MajorGrid.Interval;

                /////////////////////////////////////////////////////
                ///

                chart2.Series["RSSI"].Points.AddY(Form1.SeriesRSSI);
                chart2.Series["RSSI"].Points.RemoveAt(0);

                // Make gridlines move.
                chart2.ChartAreas[0].AxisX.MajorGrid.IntervalOffset = -GridlinesOffset;

                // Calculate next offset.
                GridlinesOffset++;
                GridlinesOffset %= (int)chart2.ChartAreas[0].AxisX.MajorGrid.Interval;

                ///////////////////////////////////////////////////
                ///

                chart3.Series["RSRP"].Points.AddY(Form1.SeriesRSRP);
                chart3.Series["RSRP"].Points.RemoveAt(0);

                // Make gridlines move.
                chart3.ChartAreas[0].AxisX.MajorGrid.IntervalOffset = -GridlinesOffset;

                // Calculate next offset.
                GridlinesOffset++;
                GridlinesOffset %= (int)chart3.ChartAreas[0].AxisX.MajorGrid.Interval;

                ///////////////////////////////////////////////////////
                ///

                chart4.Series["SINR"].Points.AddY(Form1.Seriessinr);
                chart4.Series["SINR"].Points.RemoveAt(0);

                // Make gridlines move.
                chart4.ChartAreas[0].AxisX.MajorGrid.IntervalOffset = -GridlinesOffset;

                // Calculate next offset.
                GridlinesOffset++;
                GridlinesOffset %= (int)chart4.ChartAreas[0].AxisX.MajorGrid.Interval;
            }
            else
            {
                MessageBox.Show("An error occured", "Huawei Router Tool", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            

        }

        private void SignalMonitor_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Save signal reading?", "Huawei Router Tool", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                if (folderBrowserDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {

                    string path = folderBrowserDialog1.SelectedPath;

                    if (File.Exists(Path.GetTempPath() + @"\HuaweiRouterTool\Signal Log.txt"))
                    {
                        try
                        {
                            File.Copy(Path.GetTempPath() + @"\HuaweiRouterTool\Signal Log.txt", path + @"\Signal Log.txt", true);

                            if (File.Exists(path + @"\Signal Log.txt"))
                            {
                                DialogResult openfile = MessageBox.Show("Signal log created. Open signal log?", "Huawei Router Tool", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                if (openfile == DialogResult.Yes)
                                {
                                    var fileToOpen = path + @"\Signal Log.txt";
                                    var process = new Process();
                                    process.StartInfo = new ProcessStartInfo()
                                    {
                                        UseShellExecute = true,
                                        FileName = fileToOpen
                                    };

                                    process.Start();

                                }
                                else
                                {
                                    this.Hide();

                                }
                            }
                            else
                            {
                                MessageBox.Show("An error occured", "Huawei Router Tool", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                            }
                        }
                        catch
                        {
                            MessageBox.Show("An error occured", "Huawei Router Tool", MessageBoxButtons.OK, MessageBoxIcon.Hand);

                        }

                    }
                    else
                    {
                        MessageBox.Show("An error occured", "Huawei Router Tool", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    }
                    //File.WriteAllText(path, randomstring);
                }
                this.Hide();
            }
            else
            {
                this.Hide();
            }
        }
    }
}
