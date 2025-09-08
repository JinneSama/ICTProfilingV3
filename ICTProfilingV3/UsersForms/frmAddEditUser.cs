using ICTProfilingV3.BaseClasses;
using ICTProfilingV3.DataTransferModels.Models;
using ICTProfilingV3.Interfaces;
using Models.Entities;
using Models.Enums;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ICTProfilingV3.UsersForms
{
    public partial class frmAddEditUser : BaseForm
    {
        private readonly IICTUserManager _userManager;
        private readonly IICTRoleManager _roleManager;
        private readonly IServiceProvider _serviceProvider;
        private readonly IControlMapper<Users> _userMapper;
        private readonly IControlMapper<UserModel> _userModelMapper;

        private SaveType _saveType;
        private Users _users;
        public frmAddEditUser(IICTUserManager userManager, IICTRoleManager roleManager, IControlMapper<Users> userMapper, IServiceProvider serviceProvider,
            IControlMapper<UserModel> userModelMapper)
        {
            InitializeComponent();
            _userManager = userManager;
            _roleManager = roleManager;
            _userMapper = userMapper;
            _userModelMapper = userModelMapper;
            _serviceProvider = serviceProvider;

            _saveType = SaveType.Insert;
            LoadDropdowns();
        }

        public void InitForm(Users users)
        {
            _users = users;
            _saveType = SaveType.Update;
            LoadUser();
        }
        private void LoadDropdowns()
        {
            lueUserRole.Properties.DataSource = _roleManager.GetRoles().Select(x => new
            {
                Id = x.Id,
                Name = x.Name,
            });
        }

        private async void LoadUser()
        {
            _userMapper.MapControl(_users, this);
            var role = await _roleManager.FindById(_users.Roles?.FirstOrDefault()?.RoleId);
            lueUserRole.EditValue = role?.Name;
        }

        private async void btnSave_Click(object sender, System.EventArgs e)
        {
            if (_saveType == SaveType.Insert) await InsertUser();
            else await UpdateUser();
        }

        private async Task UpdateUser()
        {
            var userModel = new UserModel();
            userModel.UserId = _users.Id;
            userModel.role = (string)lueUserRole.EditValue;

            _userModelMapper.MapToEntity(userModel, this);

            await _userManager.UpdateUser(userModel);
            this.Close();
        }

        private async Task InsertUser()
        {
            var user = new Users();
            _userMapper.MapToEntity(user, this);
            user.Email = txtUserName.Text + "@gmail.com";

            var res = await _userManager.CreateUser(user, txtPassword.Text);
            string id = res.userId;
            await _roleManager.AssignRoleToUser(id, (string)lueUserRole.EditValue);

            if (res.result.Succeeded) 
                this.Close();
            else 
                MessageBox.Show(string.Join(Environment.NewLine,res.result.Errors));
        }

        private void btnCancel_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }
    }
}