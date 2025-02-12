namespace ICTProfilingV3.ToolForms
{
    partial class frmUpdater
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
            this.progressUpdate = new DevExpress.XtraEditors.ProgressBarControl();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.lblByteSize = new DevExpress.XtraEditors.LabelControl();
            this.lblState = new DevExpress.XtraEditors.LabelControl();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.progressUpdate.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.panel1.Controls.Add(this.labelControl16);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(501, 29);
            this.panel1.TabIndex = 6;
            // 
            // labelControl16
            // 
            this.labelControl16.Appearance.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl16.Appearance.ForeColor = System.Drawing.Color.White;
            this.labelControl16.Appearance.Options.UseFont = true;
            this.labelControl16.Appearance.Options.UseForeColor = true;
            this.labelControl16.Location = new System.Drawing.Point(12, 5);
            this.labelControl16.Name = "labelControl16";
            this.labelControl16.Size = new System.Drawing.Size(115, 21);
            this.labelControl16.TabIndex = 1;
            this.labelControl16.Text = "Updating EPiSv3";
            // 
            // progressUpdate
            // 
            this.progressUpdate.Location = new System.Drawing.Point(12, 35);
            this.progressUpdate.Name = "progressUpdate";
            this.progressUpdate.Properties.ShowTitle = true;
            this.progressUpdate.Size = new System.Drawing.Size(477, 20);
            this.progressUpdate.TabIndex = 7;
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            // 
            // lblByteSize
            // 
            this.lblByteSize.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblByteSize.Appearance.Options.UseFont = true;
            this.lblByteSize.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblByteSize.Location = new System.Drawing.Point(12, 58);
            this.lblByteSize.Name = "lblByteSize";
            this.lblByteSize.Size = new System.Drawing.Size(184, 13);
            this.lblByteSize.TabIndex = 8;
            // 
            // lblState
            // 
            this.lblState.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblState.Appearance.Options.UseFont = true;
            this.lblState.Appearance.Options.UseTextOptions = true;
            this.lblState.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.lblState.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblState.Location = new System.Drawing.Point(202, 58);
            this.lblState.Name = "lblState";
            this.lblState.Size = new System.Drawing.Size(287, 13);
            this.lblState.TabIndex = 9;
            // 
            // frmUpdater
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(501, 74);
            this.Controls.Add(this.lblState);
            this.Controls.Add(this.lblByteSize);
            this.Controls.Add(this.progressUpdate);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmUpdater";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.progressUpdate.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraEditors.LabelControl labelControl16;
        private DevExpress.XtraEditors.ProgressBarControl progressUpdate;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private DevExpress.XtraEditors.LabelControl lblByteSize;
        private DevExpress.XtraEditors.LabelControl lblState;
    }
}