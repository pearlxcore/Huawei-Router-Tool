namespace Huawei_Router_Tool_GUI
{
    partial class webpage
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(webpage));
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.buttonDeviceInfo = new MetroFramework.Controls.MetroButton();
            this.buttonWLANbasic = new MetroFramework.Controls.MetroButton();
            this.label3 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.buttonStatistic = new MetroFramework.Controls.MetroButton();
            this.buttonNetworkSetting = new MetroFramework.Controls.MetroButton();
            this.buttonDHCP = new MetroFramework.Controls.MetroButton();
            this.buttonSystemSetting = new MetroFramework.Controls.MetroButton();
            this.SuspendLayout();
            // 
            // webBrowser1
            // 
            this.webBrowser1.Location = new System.Drawing.Point(23, 133);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(1072, 571);
            this.webBrowser1.TabIndex = 0;
            // 
            // buttonDeviceInfo
            // 
            this.buttonDeviceInfo.Highlight = true;
            this.buttonDeviceInfo.Location = new System.Drawing.Point(23, 91);
            this.buttonDeviceInfo.Name = "buttonDeviceInfo";
            this.buttonDeviceInfo.Size = new System.Drawing.Size(133, 30);
            this.buttonDeviceInfo.Style = MetroFramework.MetroColorStyle.Lime;
            this.buttonDeviceInfo.TabIndex = 41;
            this.buttonDeviceInfo.Text = "Device Information";
            this.buttonDeviceInfo.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.buttonDeviceInfo.Click += new System.EventHandler(this.ButtonLogin_Click);
            // 
            // buttonWLANbasic
            // 
            this.buttonWLANbasic.Highlight = true;
            this.buttonWLANbasic.Location = new System.Drawing.Point(162, 91);
            this.buttonWLANbasic.Name = "buttonWLANbasic";
            this.buttonWLANbasic.Size = new System.Drawing.Size(133, 30);
            this.buttonWLANbasic.Style = MetroFramework.MetroColorStyle.Lime;
            this.buttonWLANbasic.TabIndex = 42;
            this.buttonWLANbasic.Text = "WLAN Basic Setting";
            this.buttonWLANbasic.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.buttonWLANbasic.Click += new System.EventHandler(this.MetroButton1_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(188)))), ((int)(((byte)(0)))));
            this.label3.Location = new System.Drawing.Point(23, 70);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 15);
            this.label3.TabIndex = 43;
            this.label3.Text = "Quick Link:";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Gray;
            this.panel2.Location = new System.Drawing.Point(25, 56);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1069, 1);
            this.panel2.TabIndex = 44;
            // 
            // buttonStatistic
            // 
            this.buttonStatistic.Highlight = true;
            this.buttonStatistic.Location = new System.Drawing.Point(301, 91);
            this.buttonStatistic.Name = "buttonStatistic";
            this.buttonStatistic.Size = new System.Drawing.Size(133, 30);
            this.buttonStatistic.Style = MetroFramework.MetroColorStyle.Lime;
            this.buttonStatistic.TabIndex = 45;
            this.buttonStatistic.Text = "Statistic";
            this.buttonStatistic.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.buttonStatistic.Click += new System.EventHandler(this.ButtonStatistic_Click);
            // 
            // buttonNetworkSetting
            // 
            this.buttonNetworkSetting.Highlight = true;
            this.buttonNetworkSetting.Location = new System.Drawing.Point(440, 91);
            this.buttonNetworkSetting.Name = "buttonNetworkSetting";
            this.buttonNetworkSetting.Size = new System.Drawing.Size(133, 30);
            this.buttonNetworkSetting.Style = MetroFramework.MetroColorStyle.Lime;
            this.buttonNetworkSetting.TabIndex = 46;
            this.buttonNetworkSetting.Text = "Network Statistic";
            this.buttonNetworkSetting.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.buttonNetworkSetting.Click += new System.EventHandler(this.ButtonNetworkSetting_Click);
            // 
            // buttonDHCP
            // 
            this.buttonDHCP.Highlight = true;
            this.buttonDHCP.Location = new System.Drawing.Point(579, 91);
            this.buttonDHCP.Name = "buttonDHCP";
            this.buttonDHCP.Size = new System.Drawing.Size(133, 30);
            this.buttonDHCP.Style = MetroFramework.MetroColorStyle.Lime;
            this.buttonDHCP.TabIndex = 47;
            this.buttonDHCP.Text = "DHCP";
            this.buttonDHCP.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.buttonDHCP.Click += new System.EventHandler(this.ButtonDHCP_Click);
            // 
            // buttonSystemSetting
            // 
            this.buttonSystemSetting.Highlight = true;
            this.buttonSystemSetting.Location = new System.Drawing.Point(718, 91);
            this.buttonSystemSetting.Name = "buttonSystemSetting";
            this.buttonSystemSetting.Size = new System.Drawing.Size(133, 30);
            this.buttonSystemSetting.Style = MetroFramework.MetroColorStyle.Lime;
            this.buttonSystemSetting.TabIndex = 48;
            this.buttonSystemSetting.Text = "System Setting";
            this.buttonSystemSetting.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.buttonSystemSetting.Click += new System.EventHandler(this.ButtonSystemSetting_Click);
            // 
            // webpage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = MetroFramework.Drawing.MetroBorderStyle.FixedSingle;
            this.ClientSize = new System.Drawing.Size(1118, 727);
            this.Controls.Add(this.buttonSystemSetting);
            this.Controls.Add(this.buttonDHCP);
            this.Controls.Add(this.buttonNetworkSetting);
            this.Controls.Add(this.buttonStatistic);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.buttonWLANbasic);
            this.Controls.Add(this.buttonDeviceInfo);
            this.Controls.Add(this.webBrowser1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "webpage";
            this.Resizable = false;
            this.ShadowType = MetroFramework.Forms.MetroForm.MetroFormShadowType.DropShadow;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Style = MetroFramework.MetroColorStyle.Lime;
            this.Text = "Router Webpage";
            this.TextAlign = System.Windows.Forms.VisualStyles.HorizontalAlign.Center;
            this.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.WebBrowser webBrowser1;
        private MetroFramework.Controls.MetroButton buttonDeviceInfo;
        private MetroFramework.Controls.MetroButton buttonWLANbasic;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel2;
        private MetroFramework.Controls.MetroButton buttonStatistic;
        private MetroFramework.Controls.MetroButton buttonNetworkSetting;
        private MetroFramework.Controls.MetroButton buttonDHCP;
        private MetroFramework.Controls.MetroButton buttonSystemSetting;
    }
}