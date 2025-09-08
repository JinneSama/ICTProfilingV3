using ICTProfilingV3.BaseClasses;
using ICTProfilingV3.DataTransferModels.ViewModels;
using ICTProfilingV3.Interfaces;
using ICTProfilingV3.Services.ApiUsers;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace ICTProfilingV3.UsersForms
{
    public partial class frmUsers : BaseForm
    {
        private readonly IICTUserManager _userManager;
        private readonly IICTRoleManager _roleManager;
        private readonly IServiceProvider _serviceProvider;
        public frmUsers(IICTUserManager userManager, IICTRoleManager roleManager, IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _userManager = userManager;
            _roleManager = roleManager;
            _serviceProvider = serviceProvider;

            LoadDropdowns();
            LoadUsers();
        }

        private void LoadDropdowns()
        {
            bsUserRoles.DataSource = _roleManager.GetRoles().Select(x => new
            {
                Id = x.Id,
                Name = x.Name,
            });
        }

        private void LoadUsers()
        {
            var users = _userManager.GetUsers().Where(x => x.IsDeleted == false).ToList();
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
            var frm = _serviceProvider.GetRequiredService<frmAddEditUser>();
            frm.ShowDialog();
            LoadUsers();
        }

        private void btnDelete_Click(object sender, System.EventArgs e)
        {
            if (MessageBox.Show("Delete this User?", "Confirmation!",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) == DialogResult.Cancel) return;
            var row = (UsersViewModel)gridUsers.GetFocusedRow();
            _userManager.DeleteUser(row.Id);
            LoadUsers();
        }

        private async void btnEdit_Click(object sender, System.EventArgs e)
        {
            var row = (UsersViewModel)gridUsers.GetFocusedRow();
            var user = await _userManager.FindUserAsync(row.Id);
            var frm = _serviceProvider.GetRequiredService<frmAddEditUser>();
            frm.InitForm(user);
            frm.ShowDialog();
            LoadUsers();
        }
    }
}