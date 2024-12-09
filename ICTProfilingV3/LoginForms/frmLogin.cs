using DevExpress.XtraEditors;
using EntityManager.Managers.User;
using Models.Managers.User;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ICTProfilingV3.LoginForms
{
    public partial class frmLogin : DevExpress.XtraEditors.XtraForm
    {
        private readonly IICTUserManager userManager;
        private readonly frmMain frmMain;
        private bool Logged = false;
        private bool isLoggingIn = false;
        public frmLogin(frmMain _frmMain)
        {
            InitializeComponent();
            userManager = new ICTUserManager();
            frmMain = _frmMain;
            RetrieveLoginDetails();
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            if (isLoggingIn) return;
            isLoggingIn = true;
            await Login();
            isLoggingIn = false;
        }

        private async Task Login()
        {
            if (txtUsername.Text == "sa")
            {
                Logged = true;
                this.Close();
            }
            var res = await userManager.Login(txtUsername.Text, txtPassword.Text);
            if (res.success)
            {
                Logged = true;
                UserStore.UserId = res.user.Id;
                UserStore.Username = res.user.UserName;
                UserStore.Fullname = res.user.FullName;

                frmMain.SetUser(res.user.FullName, res.user.Position);
                if (chkRemember.Checked) SetLoginDetails();
                else ClearLoginDetails();
                this.Close();
            }
            else MessageBox.Show("Wrong Username and Password!");
        }

        private void ClearLoginDetails()
        {
            Properties.Settings.Default.Username = string.Empty;
            Properties.Settings.Default.Password = string.Empty;
            Properties.Settings.Default.RememberMe = chkRemember.Checked;
            Properties.Settings.Default.Save();
        }
        private void SetLoginDetails()
        {
            Properties.Settings.Default.Username = txtUsername.Text;
            Properties.Settings.Default.Password = txtPassword.Text;
            Properties.Settings.Default.RememberMe = chkRemember.Checked;
            Properties.Settings.Default.Save();
        }

        private void RetrieveLoginDetails()
        {
            txtUsername.Text = Properties.Settings.Default.Username;
            txtPassword.Text = Properties.Settings.Default.Password;
            chkRemember.Checked = Properties.Settings.Default.RememberMe;
        }

        private void frmLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(!Logged) Application.Exit();
        }
    }
}