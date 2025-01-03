namespace ICTProfilingV3.DashboardForms
{
    partial class UCDashboard
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.labelControl16 = new DevExpress.XtraEditors.LabelControl();
            this.lblEpisNo = new DevExpress.XtraEditors.LabelControl();
            this.tabRepair = new DevExpress.XtraTab.XtraTabControl();
            this.tabRequest = new DevExpress.XtraTab.XtraTabPage();
            this.tabRepairs = new DevExpress.XtraTab.XtraTabPage();
            this.tabPGN = new DevExpress.XtraTab.XtraTabPage();
            this.tabM365 = new DevExpress.XtraTab.XtraTabPage();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabRepair)).BeginInit();
            this.tabRepair.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.panel1.Controls.Add(this.labelControl16);
            this.panel1.Controls.Add(this.lblEpisNo);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1384, 37);
            this.panel1.TabIndex = 84;
            // 
            // labelControl16
            // 
            this.labelControl16.Appearance.Font = new System.Drawing.Font("Segoe UI", 16F);
            this.labelControl16.Appearance.ForeColor = System.Drawing.Color.White;
            this.labelControl16.Appearance.Options.UseFont = true;
            this.labelControl16.Appearance.Options.UseForeColor = true;
            this.labelControl16.Location = new System.Drawing.Point(24, 4);
            this.labelControl16.Name = "labelControl16";
            this.labelControl16.Size = new System.Drawing.Size(101, 30);
            this.labelControl16.TabIndex = 2;
            this.labelControl16.Text = "Dashboard";
            // 
            // lblEpisNo
            // 
            this.lblEpisNo.Appearance.Font = new System.Drawing.Font("Segoe UI", 16F);
            this.lblEpisNo.Appearance.ForeColor = System.Drawing.Color.White;
            this.lblEpisNo.Appearance.Options.UseFont = true;
            this.lblEpisNo.Appearance.Options.UseForeColor = true;
            this.lblEpisNo.Location = new System.Drawing.Point(24, 3);
            this.lblEpisNo.Name = "lblEpisNo";
            this.lblEpisNo.Size = new System.Drawing.Size(0, 30);
            this.lblEpisNo.TabIndex = 1;
            // 
            // tabRepair
            // 
            this.tabRepair.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabRepair.Appearance.BackColor = System.Drawing.Color.MediumBlue;
            this.tabRepair.Appearance.Options.UseBackColor = true;
            this.tabRepair.AppearancePage.Header.BackColor = System.Drawing.Color.LightGray;
            this.tabRepair.AppearancePage.Header.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabRepair.AppearancePage.Header.Options.UseBackColor = true;
            this.tabRepair.AppearancePage.Header.Options.UseFont = true;
            this.tabRepair.AppearancePage.HeaderActive.BackColor = System.Drawing.Color.RoyalBlue;
            this.tabRepair.AppearancePage.HeaderActive.Options.UseBackColor = true;
            this.tabRepair.Location = new System.Drawing.Point(0, 37);
            this.tabRepair.Name = "tabRepair";
            this.tabRepair.SelectedTabPage = this.tabRequest;
            this.tabRepair.Size = new System.Drawing.Size(1384, 611);
            this.tabRepair.TabIndex = 85;
            this.tabRepair.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tabRequest,
            this.tabRepairs,
            this.tabPGN,
            this.tabM365});
            // 
            // tabRequest
            // 
            this.tabRequest.Name = "tabRequest";
            this.tabRequest.Size = new System.Drawing.Size(1382, 583);
            this.tabRequest.Text = "Requests Data";
            // 
            // tabRepairs
            // 
            this.tabRepairs.Name = "tabRepairs";
            this.tabRepairs.Size = new System.Drawing.Size(1382, 583);
            this.tabRepairs.Text = "Repair Data";
            // 
            // tabPGN
            // 
            this.tabPGN.Name = "tabPGN";
            this.tabPGN.Size = new System.Drawing.Size(1382, 583);
            this.tabPGN.Text = "PGN Data";
            // 
            // tabM365
            // 
            this.tabM365.Name = "tabM365";
            this.tabM365.Size = new System.Drawing.Size(1382, 583);
            this.tabM365.Text = "M365 Data";
            // 
            // UCDashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabRepair);
            this.Controls.Add(this.panel1);
            this.Name = "UCDashboard";
            this.Size = new System.Drawing.Size(1384, 648);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabRepair)).EndInit();
            this.tabRepair.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraEditors.LabelControl labelControl16;
        private DevExpress.XtraEditors.LabelControl lblEpisNo;
        private DevExpress.XtraTab.XtraTabControl tabRepair;
        private DevExpress.XtraTab.XtraTabPage tabRequest;
        private DevExpress.XtraTab.XtraTabPage tabRepairs;
        private DevExpress.XtraTab.XtraTabPage tabPGN;
        private DevExpress.XtraTab.XtraTabPage tabM365;
    }
}
