using DevExpress.XtraEditors;
using DevExpress.XtraLayout.Helpers;
using EntityManager.Managers.Role;
using ICTProfilingV3.BaseClasses;
using Models.Entities;
using Models.Enums;
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

namespace ICTProfilingV3.UsersForms
{
    public partial class frmUserRoles : BaseForm
    {
        private IICTRoleManager _ICTRoleManager;
        public frmUserRoles()
        {
            InitializeComponent();
            _ICTRoleManager = new ICTRoleManager();
            populateDropdows();
            populateGrid();
        }

        private void populateDropdows()
        {
            var designation = Enum.GetValues(typeof(Designation)).Cast<Designation>().Select(x => new
            {
                Designation = x
            });
            bsUserLevel.DataSource = designation;
        }

        private void populateGrid()
        {
            var roles = _ICTRoleManager.GetRoles().ToList();
            var res = roles.Select(x => new RolesViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Designations = string.Join(",", x.RoleDesignations?.Select(s => s.Designation.ToString()) ?? new List<string>())
            }).ToList();
            gcRoles.DataSource = new BindingList<RolesViewModel>(res);
        }

        private void gridRoles_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            var row = (RolesViewModel)gridRoles.GetFocusedRow();
            if (row.Id == null) InsertRole(row);
            else UpdateRole(row);
        }

        private async void UpdateRole(RolesViewModel row)
        {
            await _ICTRoleManager.UpdateRole(row.Id, row.Designations);
        }

        private async void InsertRole(RolesViewModel row)
        {
            await _ICTRoleManager.CreateRole(row.Name, row.Designations);
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            var row = (RolesViewModel)gridRoles.GetFocusedRow();
            await _ICTRoleManager.DeleteRole(row.Id);
        }
    }
}