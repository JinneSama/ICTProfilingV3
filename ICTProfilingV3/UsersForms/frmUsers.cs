using EntityManager.Managers.Role;
using EntityManager.Managers.User;
using ICTProfilingV3.BaseClasses;
using Models.ViewModels;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.AccessControl;
using System.Windows.Forms;

namespace ICTProfilingV3.UsersForms
{
    public partial class frmUsers : BaseForm
    {
        private IICTUserManager userManager;
        private IICTRoleManager roleManager;
        public frmUsers()
        {
            InitializeComponent();
            userManager = new ICTUserManager();
            roleManager = new ICTRoleManager();
            LoadDropdowns();
            LoadUsers();
        }

        private void LoadDropdowns()
        {
            bsUserRoles.DataSource = roleManager.GetRoles().Select(x => new
            {
                Id = x.Id,
                Name = x.Name,
            });
        }

        private void LoadUsers()
        {
            var users = userManager.GetUsers().Where(x => x.IsDeleted == false).ToList();
            var userViewModel = users.Select(x => new UsersViewModel
            {
                Id = x.Id,
                Fullname = x.FullName,
                Position = x.Position,
                UserRole = string.Join(",", x.Roles.Select(s => s.RoleId)),
                Username = x.UserName
            }).ToList();
            gcUsers.DataSource = new BindingList<UsersViewModel>(userViewModel);
        }

        private void btnAdd_Click(object sender, System.EventArgs e)
        {
            var frm = new frmAddEditUser(userManager);
            frm.ShowDialog();
            LoadUsers();
        }

        private void btnDelete_Click(object sender, System.EventArgs e)
        {
            if (MessageBox.Show("Delete this User?", "Confirmation!",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) == DialogResult.Cancel) return;
            var row = (UsersViewModel)gridUsers.GetFocusedRow();
            userManager.DeleteUser(row.Id);
            LoadUsers();
        }

        private async void btnEdit_Click(object sender, System.EventArgs e)
        {
            var row = (UsersViewModel)gridUsers.GetFocusedRow();
            var user = await userManager.FindUserAsync(row.Id);
            var frm = new frmAddEditUser(user,userManager);
            frm.ShowDialog();
            LoadUsers();
        }
    }
}