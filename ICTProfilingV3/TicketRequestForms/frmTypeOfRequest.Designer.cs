namespace ICTProfilingV3.TicketRequestForms
{
    partial class frmTypeOfRequest
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.labelControl16 = new DevExpress.XtraEditors.LabelControl();
            this.lblEpisNo = new DevExpress.XtraEditors.LabelControl();
            this.btnRepair = new DevExpress.XtraEditors.SimpleButton();
            this.btnDeliveries = new DevExpress.XtraEditors.SimpleButton();
            this.btnTechSpecs = new DevExpress.XtraEditors.SimpleButton();
            this.panel1.SuspendLayout();
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
            this.panel1.Size = new System.Drawing.Size(494, 37);
            this.panel1.TabIndex = 82;
            // 
            // labelControl16
            // 
            this.labelControl16.Appearance.Font = new System.Drawing.Font("Segoe UI", 16F);
            this.labelControl16.Appearance.ForeColor = System.Drawing.Color.White;
            this.labelControl16.Appearance.Options.UseFont = true;
            this.labelControl16.Appearance.Options.UseForeColor = true;
            this.labelControl16.Location = new System.Drawing.Point(12, 3);
            this.labelControl16.Name = "labelControl16";
            this.labelControl16.Size = new System.Drawing.Size(150, 30);
            this.labelControl16.TabIndex = 2;
            this.labelControl16.Text = "Type of Request";
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
            // btnRepair
            // 
            this.btnRepair.ImageOptions.Image = global::ICTProfilingV3.Properties.Resources.ide_32x321;
            this.btnRepair.Location = new System.Drawing.Point(328, 43);
            this.btnRepair.Name = "btnRepair";
            this.btnRepair.Size = new System.Drawing.Size(152, 99);
            this.btnRepair.TabIndex = 84;
            this.btnRepair.Text = "Repair";
            this.btnRepair.Click += new System.EventHandler(this.btnRepair_Click);
            // 
            // btnDeliveries
            // 
            this.btnDeliveries.ImageOptions.Image = global::ICTProfilingV3.Properties.Resources.boorderitem_32x321;
            this.btnDeliveries.Location = new System.Drawing.Point(170, 43);
            this.btnDeliveries.Name = "btnDeliveries";
            this.btnDeliveries.Size = new System.Drawing.Size(152, 99);
            this.btnDeliveries.TabIndex = 83;
            this.btnDeliveries.Text = "Deliveries";
            this.btnDeliveries.Click += new System.EventHandler(this.btnDeliveries_Click);
            // 
            // btnTechSpecs
            // 
            this.btnTechSpecs.ImageOptions.Image = global::ICTProfilingV3.Properties.Resources.paste_32x321;
            this.btnTechSpecs.Location = new System.Drawing.Point(12, 43);
            this.btnTechSpecs.Name = "btnTechSpecs";
            this.btnTechSpecs.Size = new System.Drawing.Size(152, 99);
            this.btnTechSpecs.TabIndex = 0;
            this.btnTechSpecs.Text = "TechSpecs";
            this.btnTechSpecs.Click += new System.EventHandler(this.btnTechSpecs_Click);
            // 
            // frmTypeOfRequest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(494, 152);
            this.Controls.Add(this.btnRepair);
            this.Controls.Add(this.btnDeliveries);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnTechSpecs);
            this.Name = "frmTypeOfRequest";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnTechSpecs;
        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraEditors.LabelControl labelControl16;
        private DevExpress.XtraEditors.LabelControl lblEpisNo;
        private DevExpress.XtraEditors.SimpleButton btnDeliveries;
        private DevExpress.XtraEditors.SimpleButton btnRepair;
    }
}