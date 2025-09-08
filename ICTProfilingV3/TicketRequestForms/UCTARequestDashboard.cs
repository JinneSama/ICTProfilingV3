using Models.Entities;
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
using ICTProfilingV3.ActionsForms;
using ICTProfilingV3.Interfaces;
using ICTProfilingV3.DataTransferModels;
using Microsoft.Extensions.DependencyInjection;
using ICTProfilingV3.DataTransferModels.Models;
using ICTProfilingV3.Services.Employees;
using ICTProfilingV3.Core.Common;

namespace ICTProfilingV3.TicketRequestForms
{
    public partial class UCTARequestDashboard : DevExpress.XtraEditors.XtraUserControl
    {
        private readonly IUCManager _ucManager;
        private readonly ITicketRequestService _ticketService;
        private readonly IStaffService _staffService;
        private readonly IEquipmentService _equipmentService;
        private readonly IServiceProvider _serviceProvider;
        private readonly IICTRoleManager _roleManager;
        private readonly UserStore _userStore;

        public string filterText { get; set; }

        public UCTARequestDashboard(ITicketRequestService ticketService, IUCManager ucManager, 
            IServiceProvider serviceProvider, IStaffService staffService, IEquipmentService equipmentService,
            IICTRoleManager roleManager, UserStore userStore)
        {
            _ticketService = ticketService;
            _equipmentService = equipmentService;
            _serviceProvider = serviceProvider;
            _staffService = staffService;
            _ucManager = ucManager;
            _roleManager = roleManager;
            _userStore = userStore;

            InitializeComponent();
            LoadDropdowns();
            FilterGrid();
        }

        private void LoadDropdowns()
        {
            var users = _staffService.GetAll().ToList().
                Select(x => x.Users);
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

            slueEquipment.Properties.DataSource = _equipmentService.GetAll().ToList();
            lueStatus.Properties.DataSource = Enum.GetValues(typeof(TicketStatus)).Cast<TicketStatus>().Select(x => new
            {
                Id = x,
                Value = EnumHelper.GetEnumDescription(x)
            });
        }

        private async Task LoadTickets()
        {
            var tickets = await _ticketService.GetTicketRequests();
            gcTARequests.DataSource = new BindingList<TicketRequestDTM>(tickets.ToList());
        }

        private async void btnNewRequest_Click(object sender, EventArgs e)
        {
            var frm = _serviceProvider.GetRequiredService<frmTypeOfRequest>();
            frm.ShowDialog();

            await LoadTickets();
        }

        #region NavigateToProcess
        private void hplProcess_Click(object sender, EventArgs e)
        {
            var row = (TicketRequestDTM)gridRequest.GetFocusedRow();
            var mainForm = _serviceProvider.GetRequiredService<frmMain>();
            if (checkStatus(row))
            {
                MessageBox.Show("Please Assign the Ticket First!", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
                return;
            }
            if (row.TypeOfRequest == RequestType.TechSpecs)
            {
                var navigation = _serviceProvider.GetRequiredService<IControlNavigator<UCTechSpecs>>();
                navigation.NavigateTo(mainForm.mainPanel, act =>
                {
                    act.IsTechSpecs = true;
                    act.filterText = row.Id.ToString();
                });
            }

            if (row.TypeOfRequest == RequestType.Deliveries)
            {
                var navigation = _serviceProvider.GetRequiredService<IControlNavigator<UCDeliveries>>();
                navigation.NavigateTo(mainForm.mainPanel, act => act.filterText = row.Id.ToString());
            }

            if (row.TypeOfRequest == RequestType.Repairs)
            {
                var navigation = _serviceProvider.GetRequiredService<IControlNavigator<UCRepair>>();
                navigation.NavigateTo(mainForm.mainPanel, act => act.filterText = row.Id.ToString());
            }
        }

        private bool checkStatus(TicketRequestDTM row)
        {
            return row.EnumStatus == TicketStatus.Accepted;
        }
        #endregion

        private async void UCTARequestDashboard_Load(object sender, EventArgs e)
        {
            await LoadTickets();
            ApplyFilterText();
        }

        private async void btnAssignTo_Click(object sender, EventArgs e)
        {
            var canAssign = await _ticketService.CanAssignTicket();
            if (!canAssign)
            {
                MessageBox.Show("You do not have Permission to Perform this Action, Please Contact Receiving/Helpdesk/Administrator", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var row = (TicketRequestDTM)gridRequest.GetFocusedRow();
            var frm = _serviceProvider.GetRequiredService<frmAssignTicket>();
            frm.SetTicket(row.Id, row.TypeOfRequest);
            frm.ShowDialog();

            await LoadTickets();
        }
        #region Filters
        public void ApplyFilterText()
        {
            if (filterText != null) gridRequest.ActiveFilterCriteria = new BinaryOperator("Id", filterText);
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
            if (slueTaskOf.EditValue != null) criteria = GroupOperator.And(criteria, new BinaryOperator("AssignedTo", row.UserName));
            if (lueOffice.EditValue != null) criteria = GroupOperator.And(criteria, new BinaryOperator("Office", office));
            if (ctrlNo != 0) criteria = GroupOperator.And(criteria, new BinaryOperator("Id", ctrlNo));
            if (slueEquipment.EditValue != null) criteria = GroupOperator.And(criteria, new FunctionOperator(FunctionOperatorType.Contains, new OperandProperty("Equipments"), new OperandValue(equipment)));

            if (deFrom.EditValue != null && deTo.EditValue != null)
            {
                var fromFilter = new BinaryOperator("DateRequested", dateFrom, BinaryOperatorType.GreaterOrEqual);
                var toFilter = new BinaryOperator("DateRequested", dateTo, BinaryOperatorType.LessOrEqual);
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
        private void slueTaskOf_EditValueChanged(object sender, EventArgs e)
        {
            FilterGrid();
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
        private void spinCtrlNo_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) FilterGrid();
        }

        #endregion
        private void gridRequest_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            var row = (TicketRequestDTM)gridRequest.GetFocusedRow();
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
            var navigation = _serviceProvider.GetRequiredService<IControlNavigator<UCActions>>();
            navigation.NavigateTo(tabAction, act => act.setActions(actionType));
        }

        private async void btnDeleteTA_Click(object sender, EventArgs e)
        {
            var res = await _roleManager.HasDesignation(Designation.TicketAdmin, _userStore.UserRole);
            if(!res)
            {
                MessageBox.Show("You do not have Permission to Delete Tickets, Please Contact Helpdesk/Administrator", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Deleting this Ticket will Delete it's corresponding Process. Delete Anyway?",
                "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.Cancel)
                return;

            var row = (TicketRequestDTM)gridRequest.GetFocusedRow();
            var ticket = await _ticketService.GetByIdAsync(row.Id);
            ticket.IsDeleted = true;
            ticket.TicketStatus = TicketStatus.Deleted;
            await _ticketService.SaveChangesAsync();
        }
    }
}
