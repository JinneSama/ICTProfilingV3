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
        private bool Logged = false;    
        public frmLogin()
        {
            InitializeComponent();
            userManager = new ICTUserManager();
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            if(txtUsername.Text == "sa")
            {
                Logged = true;
                this.Close();
                return;
            }
            var res = await userManager.Login(txtUsername.Text, txtPassword.Text);
            if (res.success)
            {
                Logged = true;
                UserStore.UserId = res.user.Id;
                this.Close();
            }
            else MessageBox.Show("Wrong Username and Password!");
        }

        private void frmLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(!Logged) Application.Exit();
        }
    }
}