namespace ICTProfilingV3.LoginForms
{
    partial class frmLogin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLogin));
            this.pictureEdit2 = new DevExpress.XtraEditors.PictureEdit();
            this.pictureEdit1 = new DevExpress.XtraEditors.PictureEdit();
            this.txtPassword = new DevExpress.XtraEditors.TextEdit();
            this.txtUsername = new DevExpress.XtraEditors.TextEdit();
            this.lblversion = new DevExpress.XtraEditors.LabelControl();
            this.chkRemember = new DevExpress.XtraEditors.CheckEdit();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.ceTerms = new DevExpress.XtraEditors.CheckEdit();
            this.memoTerms = new DevExpress.XtraEditors.MemoEdit();
            this.btnLogin = new DevExpress.XtraEditors.SimpleButton();
            this.lblDate = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPassword.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUsername.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkRemember.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ceTerms.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoTerms.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureEdit2
            // 
            this.pictureEdit2.EditValue = global::ICTProfilingV3.Properties.Resources.PGIS;
            this.pictureEdit2.Location = new System.Drawing.Point(14, 143);
            this.pictureEdit2.Name = "pictureEdit2";
            this.pictureEdit2.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pictureEdit2.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.pictureEdit2.Size = new System.Drawing.Size(355, 80);
            this.pictureEdit2.TabIndex = 91;
            // 
            // pictureEdit1
            // 
            this.pictureEdit1.EditValue = global::ICTProfilingV3.Properties.Resources.nv_logo;
            this.pictureEdit1.Location = new System.Drawing.Point(105, 12);
            this.pictureEdit1.Name = "pictureEdit1";
            this.pictureEdit1.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.pictureEdit1.Properties.Appearance.BackColor2 = System.Drawing.Color.Transparent;
            this.pictureEdit1.Properties.Appearance.Options.UseBackColor = true;
            this.pictureEdit1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pictureEdit1.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.pictureEdit1.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Squeeze;
            this.pictureEdit1.Size = new System.Drawing.Size(175, 125);
            this.pictureEdit1.TabIndex = 90;
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(11, 81);
            this.txtPassword.Margin = new System.Windows.Forms.Padding(2);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPassword.Properties.Appearance.Options.UseFont = true;
            this.txtPassword.Properties.UseSystemPasswordChar = true;
            this.txtPassword.Size = new System.Drawing.Size(331, 28);
            this.txtPassword.TabIndex = 73;
            // 
            // txtUsername
            // 
            this.txtUsername.Location = new System.Drawing.Point(11, 49);
            this.txtUsername.Margin = new System.Windows.Forms.Padding(2);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUsername.Properties.Appearance.Options.UseFont = true;
            this.txtUsername.Size = new System.Drawing.Size(331, 28);
            this.txtUsername.TabIndex = 72;
            // 
            // lblversion
            // 
            this.lblversion.Appearance.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblversion.Appearance.Options.UseFont = true;
            this.lblversion.Appearance.Options.UseTextOptions = true;
            this.lblversion.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lblversion.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.lblversion.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblversion.Location = new System.Drawing.Point(14, 229);
            this.lblversion.Name = "lblversion";
            this.lblversion.Size = new System.Drawing.Size(355, 27);
            this.lblversion.TabIndex = 92;
            this.lblversion.Text = "Version";
            // 
            // chkRemember
            // 
            this.chkRemember.Location = new System.Drawing.Point(114, 114);
            this.chkRemember.Name = "chkRemember";
            this.chkRemember.Properties.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.chkRemember.Properties.Appearance.Options.UseForeColor = true;
            this.chkRemember.Properties.Caption = "Remember Me?";
            this.chkRemember.Size = new System.Drawing.Size(113, 19);
            this.chkRemember.TabIndex = 93;
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.ceTerms);
            this.groupControl1.Controls.Add(this.memoTerms);
            this.groupControl1.Controls.Add(this.btnLogin);
            this.groupControl1.Controls.Add(this.lblDate);
            this.groupControl1.Controls.Add(this.txtUsername);
            this.groupControl1.Controls.Add(this.chkRemember);
            this.groupControl1.Controls.Add(this.txtPassword);
            this.groupControl1.Location = new System.Drawing.Point(14, 262);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(353, 341);
            this.groupControl1.TabIndex = 94;
            // 
            // ceTerms
            // 
            this.ceTerms.Location = new System.Drawing.Point(114, 274);
            this.ceTerms.Name = "ceTerms";
            this.ceTerms.Properties.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.ceTerms.Properties.Appearance.Options.UseForeColor = true;
            this.ceTerms.Properties.Caption = "Accept Terms?";
            this.ceTerms.Size = new System.Drawing.Size(113, 19);
            this.ceTerms.TabIndex = 97;
            // 
            // memoTerms
            // 
            this.memoTerms.EditValue = resources.GetString("memoTerms.EditValue");
            this.memoTerms.Location = new System.Drawing.Point(11, 139);
            this.memoTerms.Name = "memoTerms";
            this.memoTerms.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.memoTerms.Properties.Appearance.Options.UseFont = true;
            this.memoTerms.Properties.ReadOnly = true;
            this.memoTerms.Properties.Spin += new DevExpress.XtraEditors.Controls.SpinEventHandler(this.memoEdit1_Properties_Spin);
            this.memoTerms.Size = new System.Drawing.Size(331, 129);
            this.memoTerms.TabIndex = 96;
            // 
            // btnLogin
            // 
            this.btnLogin.Appearance.BackColor = System.Drawing.Color.Green;
            this.btnLogin.Appearance.BorderColor = System.Drawing.Color.Transparent;
            this.btnLogin.Appearance.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLogin.Appearance.Options.UseBackColor = true;
            this.btnLogin.Appearance.Options.UseBorderColor = true;
            this.btnLogin.Appearance.Options.UseFont = true;
            this.btnLogin.Location = new System.Drawing.Point(11, 299);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(331, 30);
            this.btnLogin.TabIndex = 95;
            this.btnLogin.Text = "Login";
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // lblDate
            // 
            this.lblDate.Appearance.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDate.Appearance.Options.UseFont = true;
            this.lblDate.Appearance.Options.UseTextOptions = true;
            this.lblDate.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lblDate.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.lblDate.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblDate.Location = new System.Drawing.Point(11, 17);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(331, 27);
            this.lblDate.TabIndex = 94;
            this.lblDate.Text = "Date";
            // 
            // frmLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(379, 610);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.lblversion);
            this.Controls.Add(this.pictureEdit2);
            this.Controls.Add(this.pictureEdit1);
            this.IconOptions.Icon = ((System.Drawing.Icon)(resources.GetObject("frmLogin.IconOptions.Icon")));
            this.Name = "frmLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmLogin_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPassword.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUsername.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkRemember.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ceTerms.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoTerms.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraEditors.TextEdit txtPassword;
        private DevExpress.XtraEditors.TextEdit txtUsername;
        private DevExpress.XtraEditors.PictureEdit pictureEdit2;
        private DevExpress.XtraEditors.PictureEdit pictureEdit1;
        private DevExpress.XtraEditors.LabelControl lblversion;
        private DevExpress.XtraEditors.CheckEdit chkRemember;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.SimpleButton btnLogin;
        private DevExpress.XtraEditors.LabelControl lblDate;
        private DevExpress.XtraEditors.CheckEdit ceTerms;
        private DevExpress.XtraEditors.MemoEdit memoTerms;
    }
}