namespace ICTProfilingV3.ToolForms
{
    partial class frmJSONViewer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmJSONViewer));
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblTechSpecs = new DevExpress.XtraEditors.LabelControl();
            this.lblEpisNo = new DevExpress.XtraEditors.LabelControl();
            this.treeViewJSON = new System.Windows.Forms.TreeView();
            this.btnOld = new DevExpress.XtraEditors.SimpleButton();
            this.btnNew = new DevExpress.XtraEditors.SimpleButton();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.panel1.Controls.Add(this.btnNew);
            this.panel1.Controls.Add(this.btnOld);
            this.panel1.Controls.Add(this.lblTechSpecs);
            this.panel1.Controls.Add(this.lblEpisNo);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(788, 45);
            this.panel1.TabIndex = 112;
            // 
            // lblTechSpecs
            // 
            this.lblTechSpecs.Appearance.Font = new System.Drawing.Font("Segoe UI", 16F);
            this.lblTechSpecs.Appearance.ForeColor = System.Drawing.Color.White;
            this.lblTechSpecs.Appearance.Options.UseFont = true;
            this.lblTechSpecs.Appearance.Options.UseForeColor = true;
            this.lblTechSpecs.Location = new System.Drawing.Point(24, 7);
            this.lblTechSpecs.Name = "lblTechSpecs";
            this.lblTechSpecs.Size = new System.Drawing.Size(119, 30);
            this.lblTechSpecs.TabIndex = 2;
            this.lblTechSpecs.Text = "JSON Viewer";
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
            // treeViewJSON
            // 
            this.treeViewJSON.Location = new System.Drawing.Point(0, 51);
            this.treeViewJSON.Name = "treeViewJSON";
            this.treeViewJSON.Size = new System.Drawing.Size(788, 499);
            this.treeViewJSON.TabIndex = 113;
            // 
            // btnOld
            // 
            this.btnOld.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOld.Appearance.BackColor = System.Drawing.Color.Turquoise;
            this.btnOld.Appearance.ForeColor = System.Drawing.Color.Black;
            this.btnOld.Appearance.Options.UseBackColor = true;
            this.btnOld.Appearance.Options.UseForeColor = true;
            this.btnOld.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnReadLogs.ImageOptions.Image")));
            this.btnOld.Location = new System.Drawing.Point(646, 8);
            this.btnOld.Margin = new System.Windows.Forms.Padding(2);
            this.btnOld.Name = "btnOld";
            this.btnOld.Size = new System.Drawing.Size(131, 30);
            this.btnOld.TabIndex = 115;
            this.btnOld.Text = "Old Values";
            this.btnOld.Click += new System.EventHandler(this.btnOld_Click);
            // 
            // btnNew
            // 
            this.btnNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNew.Appearance.BackColor = System.Drawing.Color.Turquoise;
            this.btnNew.Appearance.ForeColor = System.Drawing.Color.Black;
            this.btnNew.Appearance.Options.UseBackColor = true;
            this.btnNew.Appearance.Options.UseForeColor = true;
            this.btnNew.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("simpleButton1.ImageOptions.Image")));
            this.btnNew.Location = new System.Drawing.Point(511, 8);
            this.btnNew.Margin = new System.Windows.Forms.Padding(2);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(131, 30);
            this.btnNew.TabIndex = 116;
            this.btnNew.Text = "New Values";
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // frmJSONViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(788, 547);
            this.Controls.Add(this.treeViewJSON);
            this.Controls.Add(this.panel1);
            this.Name = "frmJSONViewer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        public DevExpress.XtraEditors.LabelControl lblTechSpecs;
        private DevExpress.XtraEditors.LabelControl lblEpisNo;
        private System.Windows.Forms.TreeView treeViewJSON;
        private DevExpress.XtraEditors.SimpleButton btnNew;
        private DevExpress.XtraEditors.SimpleButton btnOld;
    }
}