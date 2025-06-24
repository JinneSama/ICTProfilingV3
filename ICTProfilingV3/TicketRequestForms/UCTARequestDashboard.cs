using Models.Entities;
using Models.Repository;
using Models.ViewModels;
using System;
using System.ComponentModel;
using System.Data;
using Models.Enums;
using System.Linq;
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
using ICTProfilingV3.ActionsForms;
using Models.Models;
using Models.HRMISEntites;
using System.Data.Entity;
using ICTProfilingV3.Interfaces;
using ICTProfilingV3.DataTransferModels;

namespace ICTProfilingV3.TicketRequestForms
{
    public partial class UCTARequestDashboard : DevExpress.XtraEditors.XtraUserControl, IDisposeUC
    {
        private IUnitOfWork unitOfWork;
        private readonly IUCManager<Control> _ucManager;
        private readonly IICTUserManager userManager;
        private readonly IICTRoleManager roleManager;
        private readonly ITicketRequestService _ticketService;
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
            FilterGrid();
        }
        public UCTARequestDashboard(ITicketRequestService ticketService)
        {
            InitializeComponent();
            _ticketService = ticketService;
            unitOfWork = new UnitOfWork();
            userManager = new ICTUserManager();
            roleManager = new ICTRoleManager();
            var main = Application.OpenForms["frmMain"] as frmMain;
            _ucManager = main._ucManager;
            LoadDropdowns();
            FilterGrid();
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

            lueOffice.Properties.DataSource = HRMISEmployees.ChiefOfOffices.OrderBy(x => x.Office).Select(x => new
            {
                Office = x.Office,
                Division = x.Division,
                Full = $"{x.Office} {x.Division}"
            });

            slueEquipment.Properties.DataSource = unitOfWork.EquipmentRepo.GetAll().ToList();
            lueStatus.Properties.DataSource = Enum.GetValues(typeof(TicketStatus)).Cast<TicketStatus>().Select(x => new
            {
                Id = x,
                Value = EnumHelper.GetEnumDescription(x)
            });
        }

        private async Task LoadTickets()
        {
            //var res = await unitOfWork.TicketRequestRepo.GetAll(
            //    x => x.Repairs,
            //    x => x.Repairs.PPEs,
            //    x => x.Repairs.PPEs.PPEsSpecs,
            //    x => x.Repairs.PPEs.PPEsSpecs.Select(s => s.Model),
            //    x => x.Repairs.PPEs.PPEsSpecs.Select(s => s.Model.Brand),
            //    x => x.Repairs.PPEs.PPEsSpecs.Select(s => s.Model.Brand.EquipmentSpecs),
            //    x => x.Repairs.PPEs.PPEsSpecs.Select(s => s.Model.Brand.EquipmentSpecs.Equipment),
            //    x => x.TechSpecs,
            //    x => x.TechSpecs.TechSpecsICTSpecs,
            //    x => x.TechSpecs.TechSpecsICTSpecs.Select(s => s.EquipmentSpecs),
            //    x => x.TechSpecs.TechSpecsICTSpecs.Select(s => s.EquipmentSpecs.Equipment),
            //    x => x.Deliveries,
            //    x => x.Deliveries.Supplier,
            //    x => x.Deliveries.DeliveriesSpecs,
            //    x => x.Deliveries.DeliveriesSpecs.Select(s => s.Model),
            //    x => x.Deliveries.DeliveriesSpecs.Select(s => s.Model.Brand),
            //    x => x.Deliveries.DeliveriesSpecs.Select(s => s.Model.Brand.EquipmentSpecs),
            //    x => x.Deliveries.DeliveriesSpecs.Select(s => s.Model.Brand.EquipmentSpecs.Equipment),
            //    x => x.ITStaff,
            //    x => x.ITStaff.Users).Where(w => w.IsRepairTechSpecs != true).OrderBy(x => x.DateCreated).ToListAsync();

            //var tickets = res.Select(x => new TicketRequestViewModel
            //{
            //    TicketRequest = x
            //}).ToList();

            //gcTARequests.DataSource = new BindingList<TicketRequestViewModel>(tickets);
            var tickets = await _ticketService.GetTicketRequests();
            gcTARequests.DataSource = new BindingList<TicketRequestDTM>(tickets.ToList());
        }

        private async void btnNewRequest_Click(object sender, EventArgs e)
        {
            var frm = new frmTypeOfRequest();
            frm.ShowDialog();

            unitOfWork = new UnitOfWork();
            await LoadTickets();
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

        private async void UCTARequestDashboard_Load(object sender, EventArgs e)
        {
            await LoadTickets();
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

            var frm2 = new frmAssignTicket(row.TicketRequest);
            frm2.ShowDialog();

            unitOfWork = new UnitOfWork();
            await LoadTickets();

            //var frm = new frmAssignTo(row.TicketRequest);
            //frm.ShowDialog();
        }
        private void FilterGrid()
        {
            gridRequest.ActiveFilterCriteria = null;

            var row = (Users)slueTaskOf.Properties.View.GetFocusedRow();
            var process = lueProcessType.EditValue;
            var ctrlNo = spinCtrlNo.Value;
            var dateFrom = deFrom.DateTime;
            var dateTo = deTo.DateTime;
            var isCompleted = ceShowCompleted.Checked;
            var office = (string)lueOffice.EditValue;
            var equipment = (string)slueEquipment.EditValue;
            var ticketStatus = lueStatus.EditValue;

            var criteria = gridRequest.ActiveFilterCriteria;

            if (isCompleted)
                foreach (var status in Enum.GetValues(typeof(TicketStatus)).Cast<TicketStatus>())
                    criteria = GroupOperator.Or(criteria, new BinaryOperator("Status", EnumHelper.GetEnumDescription(status)));
            else
                foreach (var status in Enum.GetValues(typeof(TicketStatus)).Cast<TicketStatus>())
                {
                    if (status == TicketStatus.Completed) continue;
                    criteria = GroupOperator.Or(criteria, new BinaryOperator("Status", EnumHelper.GetEnumDescription(status)));
                }

            if (lueStatus.EditValue != null) criteria = GroupOperator.And(criteria, new BinaryOperator("Status", EnumHelper.GetEnumDescription((TicketStatus)ticketStatus)));
            if (lueProcessType.EditValue != null) criteria = GroupOperator.And(criteria, new BinaryOperator("TypeOfRequest", EnumHelper.GetEnumDescription((RequestType)process)));
            if (slueTaskOf.EditValue != null) criteria = GroupOperator.And(criteria, new BinaryOperator("TicketRequest.ITStaff.Users.UserName", row.UserName));
            if (lueOffice.EditValue != null) criteria = GroupOperator.And(criteria, new BinaryOperator("Office", office));
            if (ctrlNo != 0) criteria = GroupOperator.And(criteria, new BinaryOperator("TicketRequest.Id", ctrlNo));
            if (slueEquipment.EditValue != null) criteria = GroupOperator.And(criteria, new FunctionOperator(FunctionOperatorType.Contains, new OperandProperty("Equipments"), new OperandValue(equipment)));

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
            lueOffice.EditValue = null;
            slueTaskOf.EditValue = null;
            lueProcessType.EditValue = null;
            deFrom.EditValue = null;
            deTo.EditValue = null;
            spinCtrlNo.Value = 0;
            gridRequest.ActiveFilterCriteria = null;
            slueEquipment.EditValue = null;
            lueStatus.EditValue = null;
            ceShowCompleted.Checked = false;
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

        private void gridRequest_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            var row = (TicketRequestDTM)gridRequest.GetFocusedRow();
            //var row = (TicketRequestViewModel)gridRequest.GetFocusedRow();
            if (row == null)
            {
                tabAction.Controls.Clear();
                return;
            }

            var actionType = new ActionType()
            {
                Id = row.Id,
                RequestType = row.TypeOfRequest
            };
            LoadActions(actionType);
        }

        private void LoadActions(ActionType actionType)
        {
            tabAction.Controls.Clear();
            tabAction.Controls.Add(new UCActions(actionType)
            {
                Dock = DockStyle.Fill
            });
        }

        private void ceShowCompleted_CheckedChanged(object sender, EventArgs e)
        {
            FilterGrid();
        }

        private void lueOffice_EditValueChanged(object sender, EventArgs e)
        {
            FilterGrid();
        }

        private void slueEquipment_EditValueChanged(object sender, EventArgs e)
        {
            FilterGrid();
        }

        private void lueStatus_EditValueChanged(object sender, EventArgs e)
        {
            FilterGrid();
        }
    }
}
