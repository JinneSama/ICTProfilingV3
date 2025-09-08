using DevExpress.Data.Filtering;
using DevExpress.XtraEditors;
using ICTProfilingV3.ActionsForms;
using ICTProfilingV3.API.FilesApi;
using ICTProfilingV3.Core.Common;
using ICTProfilingV3.DataTransferModels.Models;
using ICTProfilingV3.DataTransferModels.ReportViewModel;
using ICTProfilingV3.DataTransferModels.ViewModels;
using ICTProfilingV3.EvaluationForms;
using ICTProfilingV3.Interfaces;
using ICTProfilingV3.PPEInventoryForms;
using ICTProfilingV3.ReportForms;
using ICTProfilingV3.Services.Employees;
using ICTProfilingV3.TechSpecsForms;
using ICTProfilingV3.TicketRequestForms;
using ICTProfilingV3.ToolForms;
using Microsoft.Extensions.DependencyInjection;
using Models.Entities;
using Models.Enums;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ICTProfilingV3.RepairForms
{
    public partial class UCRepair : XtraUserControl
    {
        private readonly IPPEInventoryService _ppeInventoryService;
        private readonly IICTUserManager _userManager;
        private readonly IUCManager _ucManager;
        private readonly IServiceProvider _serviceProvider;
        private readonly IStaffService _staffService;
        private readonly IEquipmentService _equipmentService;
        private readonly IRepairService _repairService;
        private readonly UserStore _userStore;

        private HTTPNetworkFolder _networkFolder;
        public string filterText { get; set; }
        public UCRepair(IUCManager ucManager, IServiceProvider serviceProvider, UserStore userStore, IICTUserManager userManager, 
            IStaffService staffService, IEquipmentService equipmentService, HTTPNetworkFolder networkFolder,
            IRepairService repairService, IPPEInventoryService ppeInventoryService)
        {
            _ucManager = ucManager;
            _serviceProvider = serviceProvider;
            _userStore = userStore;
            _userManager = userManager;
            _staffService = staffService;
            _equipmentService = equipmentService;
            _networkFolder = networkFolder;
            _repairService = repairService;
            _ppeInventoryService = ppeInventoryService;
            InitializeComponent();
        }

        private void LoadFilterDropdowns()
        {
            var users = _staffService.GetAll()
                .AsNoTracking()
                .Select(x => x.Users)
                .ToList();

            slueTaskOf.Properties.DataSource = users;

            slueEquipment.Properties.DataSource = _equipmentService.GetAll().ToList();
            FilterGrid();
        }

        private void LoadRepair()
        {
            var repair = _repairService.GetRepairViewModels();
            gcRepair.DataSource = repair; 
        }

        private void LoadActions()
        {
            var row = (RepairViewModel)gridRepair.GetFocusedRow();
            var navigation = _serviceProvider.GetRequiredService<IControlNavigator<UCActions>>();
            navigation.NavigateTo(tabAction, act => act.setActions(new ActionType { Id = row.Id, RequestType = RequestType.Repairs }));
        }
        private async Task LoadStaff()
        {
            var row = (RepairViewModel)gridRepair.GetFocusedRow();
            var res = await _staffService.GetStaffModel(row.Id);
            var navigation = _serviceProvider.GetRequiredService<IControlNavigator<UCAssignedTo>>();
            navigation.NavigateTo(staffPanel, act => act.InitUC(res));
        }
        private async Task LoadRepairDetails()
        {
            ClearAllFields();
            var row = (RepairViewModel)gridRepair.GetFocusedRow();
            if(row == null) return;
            var repair = await _repairService.GetByIdAsync(row.Id);

            spbTicketStatus.SelectedItemIndex = ((int)repair.TicketRequest.TicketStatus) - 1;
            var employee = HRMISEmployees.GetEmployeeById(repair.RequestedById);
            var chief = HRMISEmployees.GetChief(employee?.Office, employee?.Division, repair.RequestedById);

            lblRepairNo.Text = repair.Id.ToString();
            txtOffice.Text = string.Join(" ", employee?.Office , employee?.Division);
            txtChief.Text = HRMISEmployees.GetEmployeeById(chief?.ChiefId)?.Employee;
            txtReqBy.Text = employee?.Employee;
            txtContactNo.Text = repair.ContactNo;
            txtDeliveredBy.Text = HRMISEmployees.GetEmployeeById(repair.DeliveredById)?.Employee;

            txtFindings.Text = repair.Findings;
            txtRecommendation.Text = repair.Recommendations;
            txtRequestProblem.Text = repair.Problems;
            LoadPPEEquipment(row);
        }
        private void LoadEvaluationSheet()
        {
            var row = (RepairViewModel)gridRepair.GetFocusedRow();
            if(row == null) return;

            var navigation = _serviceProvider.GetRequiredService<IControlNavigator<UCEvaluationSheet>>();
            navigation.NavigateTo(tabEvaluation, act => act.InitForm(new ActionType { Id = row.Id, RequestType = RequestType.Repairs }));
        }

        private void LoadPPEEquipment(RepairViewModel row)
        {
            var navigation = _serviceProvider.GetRequiredService<IControlNavigator<UCPPEsSpecs>>();
            navigation.NavigateTo(tabEquipmentSpecs, act => act.InitUC(row.Repair.PPEs, forViewing: true));
        }

        private void ClearAllFields()
        {
            foreach (var item in this.Controls)
            {
                var ctrl = (Control)item;
                if (ctrl.HasChildren){
                    foreach (var child in ctrl.Controls)
                    {
                        if (child.GetType() == typeof(TextEdit))
                            ((TextEdit)child).Text = string.Empty;

                        if (child.GetType() == typeof(MemoEdit))
                            ((MemoEdit)child).Text = string.Empty;
                    }
                }
            }
        }

        private async void btnEdit_Click(object sender, System.EventArgs e)
        {
            var row = (RepairViewModel)gridRepair.GetFocusedRow();
            var frm = _serviceProvider.GetRequiredService<frmAddEditRepairNew>();
            frm.InitForm(row.Id);
            frm.ShowDialog();

            await LoadRepairDetails();
        }

        private void UCRepair_Load(object sender, EventArgs e)
        {
            LoadFilterDropdowns();
            LoadRepair();
            ApplyFilterText();
        }

        public void ApplyFilterText()
        {
            if (filterText != null) gridRepair.ActiveFilterCriteria = new BinaryOperator("Id", filterText);
        }

        private void btnLedger_Click(object sender, EventArgs e)
        {

        }

        private async void btnTR_Click(object sender, EventArgs e)
        {
            var row = (RepairViewModel)gridRepair.GetFocusedRow();
            var frm = _serviceProvider.GetRequiredService<frmEditSignatories>();
            frm.InitForm(row.Id);
            frm.ShowDialog();

            await PrintTR();
        }

        private async Task PrintTR()
        {
            var row = (RepairViewModel)gridRepair.GetFocusedRow();
            var repair = await _repairService.GetByFilterAsync(x => x.Id == row.Id,
                x => x.PPEs,
                x => x.PPEs.PPEsSpecs,
                x => x.PPEs.PPEsSpecs.Select(s => s.Model),
                x => x.PPEs.PPEsSpecs.Select(s => s.Model.Brand),
                x => x.PPEs.PPEsSpecs.Select(s => s.Model.Brand.EquipmentSpecs),
                x => x.PPEs.PPEsSpecs.Select(s => s.Model.Brand.EquipmentSpecs.Equipment)
                );
            var data = new RepairTRViewModel
            {
                PrintedBy = _userStore.Username,
                DatePrinted = DateTime.Now,
                RequestBy = HRMISEmployees.GetEmployeeById(repair.RequestedById),
                DeliveredBy = HRMISEmployees.GetEmployeeById(repair.DeliveredById),
                IssuedTo = HRMISEmployees.GetEmployeeById(repair.PPEs.IssuedToId),
                Repair = repair,
                ReceivedBy = await _userManager.FindUserAsync(repair.PreparedById),
                AssesedBy = await _userManager.FindUserAsync(repair.ReviewedById),
                NotedBy = await _userManager.FindUserAsync(repair.NotedById)
            };

            var rpt = new rptRepairTR
            {
                DataSource = new List<RepairTRViewModel> { data }
            };

            var frm = _serviceProvider.GetRequiredService<frmReportViewer>();
            frm.InitForm(rpt);
            frm.ShowDialog();
        }

        private async void gridRepair_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            await LoadRepairDetails();
            LoadActions();
            LoadEvaluationSheet();
            await LoadStaff();
        }

        private void hplTicket_Click(object sender, EventArgs e)
        {
            var row = (RepairViewModel)gridRepair.GetFocusedRow(); 
            
            var mainForm = _serviceProvider.GetRequiredService<frmMain>();
            var navigation = _serviceProvider.GetRequiredService<IControlNavigator<UCTARequestDashboard>>();
            navigation.NavigateTo(mainForm.mainPanel, act => act.filterText = row.Id.ToString());
        }

        private void hplPPE_Click(object sender, EventArgs e)
        {
            var row = (RepairViewModel)gridRepair.GetFocusedRow();

            var mainForm = _serviceProvider.GetRequiredService<frmMain>();
            var navigation = _serviceProvider.GetRequiredService<IControlNavigator<UCPPEs>>();
            navigation.NavigateTo(mainForm.mainPanel, act => act.filterText = row.PropertyNo.ToString());
        }

        private async void btnTechSpecs_Click(object sender, EventArgs e)
        {
            var row = (RepairViewModel)gridRepair.GetFocusedRow();
            var repair = await _repairService.GetByIdAsync(row.Id);
            if (repair.TechSpecsId == null)
            {
                if (MessageBox.Show("This Repair has no Rcommended Specs, Proceeding will Generate Recommended Specs to this Repair",
                    "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) == DialogResult.Cancel) return;
            }
            else
            {
                NavigateToRepairSpecs(repair);
                return;
            }

            var frm = _serviceProvider.GetRequiredService<frmAddEditTechSpecs>();
            await frm.InitForRepairForm(repair);
            frm.ShowDialog();

            var ts = frm._repairTechSpecs;
            if (ts == null) return;
            repair.TechSpecsId = ts.Id;
            await _repairService.SaveChangesAsync();
        }
        private void NavigateToRepairSpecs(Repairs repair)
        {
            var mainForm = _serviceProvider.GetRequiredService<frmMain>();
            var navigation = _serviceProvider.GetRequiredService<IControlNavigator<UCRepair>>();
            navigation.NavigateTo(mainForm.mainPanel, act => act.filterText = repair.TechSpecsId.ToString());
        }

        private void slueEquipment_EditValueChanged(object sender, EventArgs e)
        {
            FilterGrid();
        }

        private void slueTaskOf_EditValueChanged(object sender, EventArgs e)
        {
            FilterGrid();
        }

        private void checkEdit1_CheckedChanged(object sender, EventArgs e)
        {
            FilterGrid();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            deFrom.EditValue = null;
            deTo.EditValue = null;
            slueEquipment.EditValue = null;
            slueTaskOf.EditValue = null;
            ceCompleted.Checked = false;

            FilterGrid();
        }

        private void btnFilterbyDate_Click(object sender, EventArgs e)
        {
            FilterGrid();
        }

        private void FilterGrid()
        {
            gridRepair.ActiveFilterCriteria = null;

            var dateFrom = deFrom.DateTime;
            var dateTo = deTo.DateTime;
            var equipment = (string)slueEquipment.EditValue;
            var isCompleted = ceCompleted.Checked;
            var row = slueTaskOf.EditValue;

            var criteria = gridRepair.ActiveFilterCriteria;

            if (isCompleted)
                foreach (var status in Enum.GetValues(typeof(TicketStatus)).Cast<TicketStatus>())
                    criteria = GroupOperator.Or(criteria, new BinaryOperator("Status", status));
            else
                foreach (var status in Enum.GetValues(typeof(TicketStatus)).Cast<TicketStatus>())
                {
                    if (status == TicketStatus.Completed) continue;
                    criteria = GroupOperator.Or(criteria, new BinaryOperator("Status", status));
                }

            if (slueTaskOf.EditValue != null) 
                criteria = GroupOperator.And(criteria, new BinaryOperator("AssignedTo", row));
            if (slueEquipment.EditValue != null)
                criteria = GroupOperator.And(criteria, new FunctionOperator(FunctionOperatorType.Contains, new OperandProperty("Equipment"), new OperandValue(equipment)));

            if (deFrom.EditValue != null && deTo.EditValue != null)
            {
                var fromFilter = new BinaryOperator("DateCreated", dateFrom, BinaryOperatorType.GreaterOrEqual);
                var toFilter = new BinaryOperator("DateCreated", dateTo, BinaryOperatorType.LessOrEqual);
                criteria = GroupOperator.And(criteria, GroupOperator.And(fromFilter, toFilter));
            }

            gridRepair.ActiveFilterCriteria = criteria;
        }
    }
}
