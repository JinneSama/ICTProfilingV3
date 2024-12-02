namespace ICTProfilingV3.ToolForms
{
    partial class frmUpdateNotification
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
            this.btnNow = new DevExpress.XtraEditors.SimpleButton();
            this.btnLater = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.SuspendLayout();
            // 
            // btnNow
            // 
            this.btnNow.Appearance.BorderColor = System.Drawing.Color.White;
            this.btnNow.Appearance.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNow.Appearance.ForeColor = System.Drawing.Color.Black;
            this.btnNow.Appearance.Options.UseBorderColor = true;
            this.btnNow.Appearance.Options.UseFont = true;
            this.btnNow.Appearance.Options.UseForeColor = true;
            this.btnNow.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.btnNow.Location = new System.Drawing.Point(281, 82);
            this.btnNow.Name = "btnNow";
            this.btnNow.Size = new System.Drawing.Size(160, 39);
            this.btnNow.TabIndex = 4;
            this.btnNow.Text = "Restart Now";
            this.btnNow.Click += new System.EventHandler(this.btnNow_Click);
            // 
            // btnLater
            // 
            this.btnLater.Appearance.BorderColor = System.Drawing.Color.White;
            this.btnLater.Appearance.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLater.Appearance.ForeColor = System.Drawing.Color.Black;
            this.btnLater.Appearance.Options.UseBorderColor = true;
            this.btnLater.Appearance.Options.UseFont = true;
            this.btnLater.Appearance.Options.UseForeColor = true;
            this.btnLater.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
            this.btnLater.Location = new System.Drawing.Point(115, 82);
            this.btnLater.Name = "btnLater";
            this.btnLater.Size = new System.Drawing.Size(160, 39);
            this.btnLater.TabIndex = 3;
            this.btnLater.Text = "Restart Later";
            this.btnLater.Click += new System.EventHandler(this.btnLater_Click);
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.labelControl1.Appearance.ForeColor = System.Drawing.Color.White;
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Appearance.Options.UseForeColor = true;
            this.labelControl1.Appearance.Options.UseTextOptions = true;
            this.labelControl1.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControl1.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelControl1.Location = new System.Drawing.Point(0, 0);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(562, 76);
            this.labelControl1.TabIndex = 2;
            this.labelControl1.Text = "An update is available. Would you like to update the application now?";
            // 
            // frmUpdateNotification
            // 
            this.Appearance.BackColor = System.Drawing.Color.RoyalBlue;
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(562, 148);
            this.Controls.Add(this.btnNow);
            this.Controls.Add(this.btnLater);
            this.Controls.Add(this.labelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmUpdateNotification";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnNow;
        private DevExpress.XtraEditors.SimpleButton btnLater;
        private DevExpress.XtraEditors.LabelControl labelControl1;
    }
}