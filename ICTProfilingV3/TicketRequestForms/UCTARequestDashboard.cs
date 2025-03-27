using DevExpress.XtraEditors;
using Models.Entities;
using Models.HRMISEntites;
using Models.Repository;
using Models.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using Models.Enums;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ICTProfilingV3.TechSpecsForms;
using ICTProfilingV3.DeliveriesForms;
using ICTProfilingV3.RepairForms;
using DevExpress.Data.Filtering;
using EntityManager.Managers.User;
using Helpers.Interfaces;
using EntityManager.Managers.Role;
using Models.Managers.User;
using Helpers.Utility;

namespace ICTProfilingV3.TicketRequestForms
{
    public partial class UCTARequestDashboard : DevExpress.XtraEditors.XtraUserControl, IDisposeUC
    {
        private IUnitOfWork unitOfWork;
        private readonly IUCManager<Control> _ucManager;
        private readonly IICTUserManager userManager;
        private readonly IICTRoleManager roleManager;
        public string filterText { get; set; }  
        public UCTARequestDashboard()
        {
            InitializeComponent();
            unitOfWork = new UnitOfWork();
            userManager = new ICTUserManager();
            roleManager = new ICTRoleManager();
            var main = Application.OpenForms["frmMain"] as frmMain;
            _ucManager = main._ucManager;
            LoadDropdowns();
            LoadTickets();
        }
        private async Task<bool> CanAssign()
        {
            var user = await userManager.FindUserAsync(UserStore.UserId);
            if (user.Roles == null) return false;
            var role = await roleManager.GetRoleDesignations(user.Roles.FirstOrDefault().RoleId);
            if (role == null) return false;

            return role.Select(x => x.Designation).ToList().Contains(Designation.AssignTo);
        }

        private void LoadDropdowns()
        {
            var users = unitOfWork.ITStaffRepo.GetAll().
                Select(x => x.Users).ToList();
            slueTaskOf.Properties.DataSource = users;

            lueProcessType.Properties.DataSource = Enum.GetValues(typeof(RequestType)).Cast<RequestType>().Select(x => new
            {
                Id = x,
                Value = EnumHelper.GetEnumDescription(x)
            });
        }

        private void LoadTickets()
        {
            var res = unitOfWork.TicketRequestRepo.GetAll(x => x.Deliveries,
                x => x.TechSpecs,
                x => x.Repairs,
                x => x.ITStaff.Users).Where(w => w.IsRepairTechSpecs != true).ToList().Select(x => new TicketRequestViewModel
                {
                    TicketRequest = x
                }).OrderByDescending(x => x.TicketRequest.DateCreated);
            gcTARequests.DataSource = new BindingList<TicketRequestViewModel>(res.ToList());
        }

        private void btnNewRequest_Click(object sender, EventArgs e)
        {
            var frm = new frmTypeOfRequest();
            frm.ShowDialog();

            unitOfWork = new UnitOfWork();
            LoadTickets();
        }

        private void hplProcess_Click(object sender, EventArgs e)
        {
            if(checkStatus())
            {
                MessageBox.Show("Please Assign the Ticket First!", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
                return;
            }
            var row = (TicketRequestViewModel)gridRequest.GetFocusedRow();
            if (row.TicketRequest.RequestType == RequestType.TechSpecs) NavigateToProcess(new UCTechSpecs() {
                IsTechSpecs = true,
                Dock = DockStyle.Fill,
                filterText = row.TicketRequest.Id.ToString() 
            }, new string[] {"IsTechSpecs", "filterText"});

            if (row.TicketRequest.RequestType == RequestType.Deliveries) NavigateToProcess(new UCDeliveries()
            {
                Dock = DockStyle.Fill,
                filterText = row.TicketRequest.Id.ToString()
            }, new string[] { "filterText" });

            if (row.TicketRequest.RequestType == RequestType.Repairs) NavigateToProcess(new UCRepair()
            {
                Dock = DockStyle.Fill,
                filterText = row.TicketRequest.Id.ToString()
            }, new string[] { "filterText" });
        }

        private bool checkStatus()
        {
            var row = (TicketRequestViewModel)gridRequest.GetFocusedRow();
            if (row.TicketRequest.TicketStatus == TicketStatus.Accepted) return true;
            return false;
        }

        private void NavigateToProcess(Control uc, string[] propertiesToCopy)
        {
            _ucManager.ShowUCSystemDetails(uc.Name, uc , propertiesToCopy);
        }

        private void UCTARequestDashboard_Load(object sender, EventArgs e)
        {
            ApplyFilterText();
        }

        public void ApplyFilterText()
        {
            if (filterText != null) gridRequest.ActiveFilterCriteria = new BinaryOperator("TicketRequest.Id", filterText);
        }

        private async void btnAssignTo_Click(object sender, EventArgs e)
        {
            var canAssign = await CanAssign();
            if (!canAssign)
            {
                MessageBox.Show("You do not have Permission to Perform this Action, Please Contact Receiving/Helpdesk/Administrator", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var row = (TicketRequestViewModel)gridRequest.GetFocusedRow();
            var frm = new frmAssignTo(row.TicketRequest);
            frm.ShowDialog();

            unitOfWork = new UnitOfWork();
            LoadTickets();
        }
        private void FilterGrid()
        {
            gridRequest.ActiveFilterCriteria = null;

            var row = (Users)slueTaskOf.Properties.View.GetFocusedRow();
            var process = lueProcessType.EditValue;
            var ctrlNo = spinCtrlNo.Value;
            var dateFrom = deFrom.DateTime;
            var dateTo = deTo.DateTime;

            var criteria = gridRequest.ActiveFilterCriteria;
            if (lueProcessType.EditValue != null) criteria = GroupOperator.And(criteria, new BinaryOperator("TypeOfRequest", EnumHelper.GetEnumDescription((RequestType)process)));
            if (slueTaskOf.EditValue != null) criteria = GroupOperator.And(criteria, new BinaryOperator("TicketRequest.ITStaff.Users.UserName", row.UserName));
            if (ctrlNo != 0) criteria = GroupOperator.And(criteria, new BinaryOperator("TicketRequest.Id", ctrlNo));
            if (deFrom.EditValue != null && deTo.EditValue != null)
            {
                var fromFilter = new BinaryOperator("TicketRequest.DateCreated", dateFrom, BinaryOperatorType.GreaterOrEqual);
                var toFilter = new BinaryOperator("TicketRequest.DateCreated", dateTo, BinaryOperatorType.LessOrEqual);
                criteria = GroupOperator.And(criteria, GroupOperator.And(fromFilter, toFilter));
            }

            gridRequest.ActiveFilterCriteria = criteria;
        }

        private void btnFilterbyDate_Click(object sender, EventArgs e)
        {
            FilterGrid();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            slueTaskOf.EditValue = null;
            lueProcessType.EditValue = null;
            deFrom.EditValue = null;
            deTo.EditValue = null;
            spinCtrlNo.Value = 0;
            gridRequest.ActiveFilterCriteria = null;
        }

        private void lueProcessType_EditValueChanged(object sender, EventArgs e)
        {
            FilterGrid();
        }

        private void spinCtrlNo_EditValueChanged(object sender, EventArgs e)
        {
            FilterGrid();
        }

        private void spinCtrlNo_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) FilterGrid();
        }

        private void btnInfo_Click(object sender, EventArgs e)
        {
            var focusedRow = gridRequest.FocusedRowHandle;
            gridRequest.SetMasterRowExpanded(focusedRow, !gridRequest.GetMasterRowExpanded(focusedRow));
        }

        private void slueTaskOf_EditValueChanged(object sender, EventArgs e)
        {
            FilterGrid();
        }

        public void DisposeUC(Control parent)
        {
            foreach (Control ctrl in parent.Controls)
            {
                ctrl.Dispose();
                GC.Collect();
            }
            parent.Controls.Clear();
        }
    }
}
