using DevExpress.XtraSpellChecker.Parser;
using EntityManager.Managers.Role;
using EntityManager.Managers.User;
using ICTProfilingV3.BaseClasses;
using Models.Entities;
using Models.Enums;
using Models.Models;
using System;
using System.Linq;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace ICTProfilingV3.UsersForms
{
    public partial class frmAddEditUser : BaseForm
    {
        private readonly Users _users;
        private readonly IICTUserManager _userManager;
        private IICTRoleManager roleManager;
        private SaveType _saveType;
        public frmAddEditUser(IICTUserManager userManager)
        {
            InitializeComponent();
            _userManager = userManager;
            roleManager = new ICTRoleManager();
            _saveType = SaveType.Insert;
            LoadDropdowns();
        }
        public frmAddEditUser(Users users, IICTUserManager userManager)
        {
            InitializeComponent();
            _users = users;
            _userManager = userManager;
            roleManager = new ICTRoleManager();
            _saveType = SaveType.Update;
            LoadDropdowns();
            LoadUser();
        }
        private void LoadDropdowns()
        {
            lueUserRole.Properties.DataSource = roleManager.GetRoles().Select(x => new
            {
                Id = x.Id,
                Name = x.Name,
            });
        }

        private async void LoadUser()
        {
            txtUsername.Text = _users.UserName;
            txtFullname.Text = _users.FullName;
            var role = await roleManager.FindById(_users.Roles?.FirstOrDefault()?.RoleId);
            lueUserRole.EditValue = role?.Name;
            txtPosition.Text = _users.Position;
        }

        private void btnSave_Click(object sender, System.EventArgs e)
        {
            if (_saveType == SaveType.Insert) InsertUser();
            else UpdateUser();
        }

        private async void UpdateUser()
        {
            var userModel = new UserModel
            {
                UserId = _users.Id,
                Username = txtUsername.Text,
                Fullname = txtFullname.Text,
                Position = txtPosition.Text,
                role = (string)lueUserRole.EditValue,
                Password = txtPassword.Text
            };
            await _userManager.UpdateUser(userModel);
            this.Close();
        }

        private async void InsertUser()
        {
            var user = new Users
            {
                UserName = txtUsername.Text,
                FullName = txtFullname.Text,
                Position = txtPosition.Text,
                Email = txtUsername.Text + "@gmail.com"
            };

            var res = await _userManager.CreateUser(user, txtPassword.Text);
            string id = res.userId;
            await roleManager.AssignRoleToUser(id, (string)lueUserRole.EditValue);
            if (res.result.Succeeded) this.Close();
            else MessageBox.Show(string.Join(Environment.NewLine,res.result.Errors));
        }

        private void btnCancel_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }
    }
}