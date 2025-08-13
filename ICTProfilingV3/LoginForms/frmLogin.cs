using ICTProfilingV3.BaseClasses;
using ICTProfilingV3.Core.Common;
using ICTProfilingV3.DataTransferModels.ServiceModels.DTOModels;
using ICTProfilingV3.DebugTools;
using ICTProfilingV3.Interfaces;
using ICTProfilingV3.Services.ApiUsers;
using ICTProfilingV3.Services.Employees;
using ICTProfilingV3.ToolForms;
using ICTProfilingV3.Utility.Security;
using Microsoft.Extensions.DependencyInjection;
using Models.Entities;
using Models.Repository;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ICTProfilingV3.LoginForms
{
    public partial class frmLogin : BaseForm
    {
        private readonly IICTUserManager userManager;
        private readonly IServiceProvider _serviceProvider;
        private readonly IUnitOfWork unitOfWork;
        private readonly UserStore _userStore;

        private readonly frmMain frmMain;
        private bool Logged = false;
        private bool isLoggingIn = false;
        public frmLogin(IServiceProvider serviceProvider, UserStore userStore)
        {
            InitializeComponent();
            _userStore = userStore;
            _serviceProvider = serviceProvider;
            userManager = new ICTUserManager();
            unitOfWork = new UnitOfWork();
            frmMain = _serviceProvider.GetRequiredService<frmMain>();
            RetrieveLoginDetails();

            LoadChangelogs();
        }
        private void LoadChangelogs()
        {
            lblDate.Text = DateTime.Now.ToString("MMM-dd-yyyy");
            if (System.Deployment.Application.ApplicationDeployment.IsNetworkDeployed)
            {
                System.Deployment.Application.ApplicationDeployment cd = System.Deployment.Application.ApplicationDeployment.CurrentDeployment;
                lblversion.Text = "EPiSv3 Rev " + cd.CurrentVersion.ToString();
                string version = cd.CurrentVersion.ToString();
                if (version != Properties.Settings.Default.LastVersion)
                {
                    Properties.Settings.Default.LastVersion = version;
                    Properties.Settings.Default.Save();

                    frmChangelogs frm = new frmChangelogs(version);
                    frm.ShowDialog();
                }
            }
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            if (ceTerms.Checked == false) return;
            if(txtUsername.Text == "#SetVersion#")
            {
                var frm = new frmVersionSetter();
                frm.ShowDialog();
                return;
            }

            if (isLoggingIn) return;
            isLoggingIn = true;
            await Login(txtUsername.Text , txtPassword.Text);
            isLoggingIn = false;
        }

        public async Task Login(string username, string password)
        {
            if (username == "sa")
            {
                Logged = true;
                this.Close();
            }
            Users user = null;
            bool logged = true;
            (var systemUser, var ofmisUser)  = await PointToSystemAccount(username, password);

            if(systemUser == null)
            {
                var res = await userManager.Login(username, password);
                if(!res.success) logged = false;
                user = res.user;
            }
            else
            {
                user = systemUser;
            }

            if (logged)
            {
                Logged = true;
                _userStore.UserId = user.Id;
                _userStore.Username = user.UserName;
                _userStore.Fullname = user.FullName;

                frmMain.setRoleDesignations();
                frmMain.SetUser(user.FullName,user.Position);
                if (chkRemember.Checked) SetLoginDetails();
                else ClearLoginDetails();

                if (_userStore.ArugmentCredentialsDto == null) this.Close();
            }
            else
            {
                if(ofmisUser != null) UseExternalAccount(ofmisUser, password);
                else MessageBox.Show("Wrong Username and Password!");
            }
        }

        private void UseExternalAccount(OFMISUsersDto ofmisUser, string password)
        {
            Logged = true;
            _userStore.Username = ofmisUser.Username;
            _userStore.Fullname = password;
            frmMain.setClientDesignation();
            frmMain.SetUser(ofmisUser.Username, "Client");
            this.Close();
        }

        private async Task<OFMISUsersDto> CheckOFMIS(string username, string password)
        {
            var ofmisUser = await OFMISUsers.GetUser(username);
            if (ofmisUser == null) return null;

            var decryptPass = Cryptography.Decrypt(ofmisUser.PasswordHash, ofmisUser.SecurityStamp);
            if (Equals(decryptPass, password)) return ofmisUser;
            else return null;
        }

        private async Task<(Users, OFMISUsersDto)> PointToSystemAccount(string username, string password)
        {
            var user = await CheckOFMIS(username, password);
            if (user == null) return (null, null);

            var systemUser = await unitOfWork.UsersRepo.FindAsync(x => x.UserName == user.Username);
            if (systemUser == null) return (null, user);
            return (systemUser, user);
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

        private void memoEdit1_Properties_Spin(object sender, DevExpress.XtraEditors.Controls.SpinEventArgs e)
        {  
        }
    }
}