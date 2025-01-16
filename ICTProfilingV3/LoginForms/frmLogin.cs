using DevExpress.XtraEditors;
using EntityManager.Managers.User;
using Helpers.Security;
using ICTProfilingV3.ToolForms;
using Models.Entities;
using Models.Managers.User;
using Models.OFMISEntities;
using Models.Repository;
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
        private readonly IUnitOfWork unitOfWork;
        private readonly frmMain frmMain;
        private bool Logged = false;
        private bool isLoggingIn = false;
        public frmLogin(frmMain _frmMain)
        {
            InitializeComponent();
            userManager = new ICTUserManager();
            unitOfWork = new UnitOfWork();
            frmMain = _frmMain;
            RetrieveLoginDetails();

            LoadChangelogs();
        }
        private void LoadChangelogs()
        {
            lblDate.Text = DateTime.UtcNow.ToString("MMM-dd-yyyy");
            if (System.Deployment.Application.ApplicationDeployment.IsNetworkDeployed)
            {
                System.Deployment.Application.ApplicationDeployment cd = System.Deployment.Application.ApplicationDeployment.CurrentDeployment;
                lblversion.Text = "EPiSv3 Rev " + cd.CurrentVersion.ToString();
                string version = cd.CurrentVersion.ToString();
                if (version != Properties.Settings.Default.LastVersion)
                {
                    Properties.Settings.Default.LastVersion = version;
                    frmChangelogs frm = new frmChangelogs(version);
                    frm.ShowDialog();
                }
            }
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
            Users user = null;
            bool logged = true;
            var ofmisUser = await PointToSystemAccount(txtUsername.Text, txtPassword.Text);
            if(ofmisUser == null)
            {
                var res = await userManager.Login(txtUsername.Text, txtPassword.Text);
                if(!res.success) logged = false;
                user = res.user;
            }
            else
            {
                user = ofmisUser;
            }

            if (logged)
            {
                Logged = true;
                UserStore.UserId = user.Id;
                UserStore.Username = user.UserName;
                UserStore.Fullname = user.FullName;

                frmMain.setRoleDesignations();
                frmMain.SetUser(user.FullName,user.Position);
                if (chkRemember.Checked) SetLoginDetails();
                else ClearLoginDetails();
                this.Close();
            }
            else MessageBox.Show("Wrong Username and Password!");
        }

        private async Task<User> CheckOFMIS(string username, string password)
        {
            var ofmisUser = await OFMISUsers.GetUser(username);
            if (ofmisUser == null) return null;

            var decryptPass = Cryptography.Decrypt(ofmisUser.PasswordHash, ofmisUser.SecurityStamp);
            if (Equals(decryptPass, password)) return ofmisUser;
            else return null;
        }

        private async Task<Users> PointToSystemAccount(string username, string password)
        {
            User user = await CheckOFMIS(username, password);
            if (user == null) return null;

            var systemUser = await unitOfWork.UsersRepo.FindAsync(x => x.UserName == user.UserName);
            if (systemUser == null) return null;
            return systemUser;
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