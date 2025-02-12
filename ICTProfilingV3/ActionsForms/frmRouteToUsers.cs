using DevExpress.XtraEditors;
using EntityManager.Managers.User;
using Models.Repository;
using Models.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Models.Enums;
using Models.Entities;
using ICTProfilingV3.BaseClasses;

namespace ICTProfilingV3.ActionsForms
{
    public partial class frmRouteToUsers : BaseForm
    {
        private readonly IICTUserManager _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ActionTreeViewModel _actionTreeViewModel;
        private readonly SaveType saveType;
        private bool isSave = false;
        public List<UsersViewModel> _routedUsers { get; set; }

        public frmRouteToUsers(ActionTreeViewModel actionTreeViewModel, SaveType saveType, List<UsersViewModel> routedUsers)
        {
            InitializeComponent();
            _userManager = new ICTUserManager();
            _unitOfWork = new UnitOfWork();
            _routedUsers = routedUsers;
            LoadUsers();
            _actionTreeViewModel = actionTreeViewModel;
            this.saveType = saveType;
        }

        private void LoadUsers()
        {
            if(_routedUsers == null) _routedUsers = new List<UsersViewModel>();
            var users = _userManager.GetUsers().Where(x => x.IsDeleted == false);
            var userViewModel = users.Select(x => new UsersViewModel
            {
                Id = x.Id,
                Username = x.UserName,
                Fullname = x.FullName,
                Mark = _routedUsers.Where(w => w.Id == x.Id).Count() <= 0 ? false : true
            }); 
            gcUsers.DataSource = userViewModel;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            isSave = true;
            this.Close();
        }

        private void frmRouteToUsers_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isSave) InsertRoutedUsers();
        }

        private void InsertRoutedUsers()
        {
            List<UsersViewModel> users = new List<UsersViewModel>();
            for (int i = 0; i < gridUsers.RowCount; i++)
            {
                var row = (UsersViewModel)gridUsers.GetRow(i);
                if (row.Mark) users.Add(row);
            }
            _routedUsers = users;
        }
    }
}