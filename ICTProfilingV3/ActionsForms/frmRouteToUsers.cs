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
using ICTProfilingV3.Services.ApiUsers;
using ICTProfilingV3.Interfaces;
using ICTProfilingV3.DataTransferModels;

namespace ICTProfilingV3.ActionsForms
{
    public partial class frmRouteToUsers : BaseForm
    {
        private ActionTreeDTM _actionTreeViewModel;
        private SaveType _saveType;
        private bool _isSave = false;
        public List<UsersDTM> _routedUsers { get; set; }

        private readonly IICTUserManager _userManager;

        public frmRouteToUsers(IICTUserManager userManager)
        {
            _userManager = userManager;
            InitializeComponent();
        }
        public void SetActionDetails(ActionTreeDTM actionTreeViewModel, SaveType saveType, List<UsersDTM> routedUsers)
        {
            _routedUsers = routedUsers;
            _actionTreeViewModel = actionTreeViewModel;
            _saveType = saveType;
            LoadUsers();
        }

        private void LoadUsers()
        {
            if(_routedUsers == null) _routedUsers = new List<UsersDTM>();
            var users = _userManager.GetUsers().Where(x => x.IsDeleted == false);
            var userViewModel = users.Select(x => new UsersDTM
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
            _isSave = true;
            this.Close();
        }

        private void frmRouteToUsers_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_isSave)
            {
                List<UsersDTM> users = new List<UsersDTM>();
                for (int i = 0; i < gridUsers.RowCount; i++)
                {
                    var row = (UsersDTM)gridUsers.GetRow(i);
                    if (row.Mark) users.Add(row);
                }
                _routedUsers = users;
            }
        }
    }
}