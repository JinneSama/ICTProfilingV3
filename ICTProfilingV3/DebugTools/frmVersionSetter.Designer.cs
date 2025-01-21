namespace ICTProfilingV3.DebugTools
{
    partial class frmVersionSetter
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
            this.btnAddBrand = new DevExpress.XtraEditors.SimpleButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.labelControl16 = new DevExpress.XtraEditors.LabelControl();
            this.lblEpisNo = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txtBrand = new DevExpress.XtraEditors.TextEdit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtBrand.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btnAddBrand
            // 
            this.btnAddBrand.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddBrand.Appearance.BackColor = System.Drawing.Color.RoyalBlue;
            this.btnAddBrand.Appearance.ForeColor = System.Drawing.Color.Black;
            this.btnAddBrand.Appearance.Options.UseBackColor = true;
            this.btnAddBrand.Appearance.Options.UseForeColor = true;
            this.btnAddBrand.ImageOptions.Image = global::ICTProfilingV3.Properties.Resources.save_16x161;
            this.btnAddBrand.Location = new System.Drawing.Point(320, 68);
            this.btnAddBrand.Margin = new System.Windows.Forms.Padding(2);
            this.btnAddBrand.Name = "btnAddBrand";
            this.btnAddBrand.Size = new System.Drawing.Size(97, 28);
            this.btnAddBrand.TabIndex = 98;
            this.btnAddBrand.Text = "Save";
            this.btnAddBrand.Click += new System.EventHandler(this.btnAddBrand_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.panel1.Controls.Add(this.labelControl16);
            this.panel1.Controls.Add(this.lblEpisNo);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(429, 37);
            this.panel1.TabIndex = 97;
            // 
            // labelControl16
            // 
            this.labelControl16.Appearance.Font = new System.Drawing.Font("Segoe UI", 16F);
            this.labelControl16.Appearance.ForeColor = System.Drawing.Color.White;
            this.labelControl16.Appearance.Options.UseFont = true;
            this.labelControl16.Appearance.Options.UseForeColor = true;
            this.labelControl16.Location = new System.Drawing.Point(24, 4);
            this.labelControl16.Name = "labelControl16";
            this.labelControl16.Size = new System.Drawing.Size(104, 30);
            this.labelControl16.TabIndex = 2;
            this.labelControl16.Text = "Set Version";
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
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(11, 46);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(35, 13);
            this.labelControl3.TabIndex = 100;
            this.labelControl3.Text = "Version";
            // 
            // txtBrand
            // 
            this.txtBrand.Location = new System.Drawing.Point(86, 43);
            this.txtBrand.Name = "txtBrand";
            this.txtBrand.Size = new System.Drawing.Size(331, 20);
            this.txtBrand.TabIndex = 99;
            // 
            // frmVersionSetter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(429, 105);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.txtBrand);
            this.Controls.Add(this.btnAddBrand);
            this.Controls.Add(this.panel1);
            this.Name = "frmVersionSetter";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtBrand.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnAddBrand;
        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraEditors.LabelControl labelControl16;
        private DevExpress.XtraEditors.LabelControl lblEpisNo;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit txtBrand;
    }
}