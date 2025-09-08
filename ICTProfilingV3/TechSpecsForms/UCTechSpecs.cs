using DevExpress.Data.Filtering;
using Helpers.Interfaces;
using ICTProfilingV3.ActionsForms;
using ICTProfilingV3.API.FilesApi;
using ICTProfilingV3.Core.Common;
using ICTProfilingV3.DataTransferModels.Models;
using ICTProfilingV3.DataTransferModels.ReportViewModel;
using ICTProfilingV3.DataTransferModels.ViewModels;
using ICTProfilingV3.EvaluationForms;
using ICTProfilingV3.Interfaces;
using ICTProfilingV3.RepairForms;
using ICTProfilingV3.ReportForms;
using ICTProfilingV3.Services.Employees;
using ICTProfilingV3.TicketRequestForms;
using ICTProfilingV3.ToolForms;
using Microsoft.Extensions.DependencyInjection;
using Models.Entities;
using Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ICTProfilingV3.TechSpecsForms
{
    public partial class UCTechSpecs : DevExpress.XtraEditors.XtraUserControl
    {
        private readonly IControlMapper<TechSpecs> _tsMapper;
        private readonly IICTUserManager _userManager;
        private readonly IUCManager _ucManager;
        private readonly IServiceProvider _serviceProvider;
        private readonly IStaffService _staffService;
        private readonly IEquipmentService _equipmentService;
        private readonly ITechSpecsService _techSpecsService;
        private readonly IPurchaseReqService _prService;

        private readonly UserStore _userStore;
        private HTTPNetworkFolder _networkFolder;
        public string filterText { get; set; }
        public bool IsTechSpecs { get; set; }
        public UCTechSpecs(IUCManager ucManager, IServiceProvider serviceProvider, UserStore userStore,
            IICTUserManager userManager, IStaffService staffService, IEquipmentService equipmentService,
            ITechSpecsService techSpecsService, IPurchaseReqService prService, HTTPNetworkFolder networkFolder, 
            IControlMapper<TechSpecs> tsMapper)
        {
            _userStore = userStore;
            _techSpecsService = techSpecsService;
            _staffService = staffService;
            _equipmentService = equipmentService;
            _ucManager = ucManager;
            _serviceProvider = serviceProvider;
            _networkFolder = networkFolder;
            _userManager = userManager;
            _prService = prService;
            _userStore = userStore;
            _tsMapper = tsMapper;
            InitializeComponent();

            LoadFilterDropdowns();
        }
        private void LoadFilterDropdowns()
        {
            var users = _staffService.GetAll().
                Select(x => x.Users).ToList();
            slueTaskOf.Properties.DataSource = users;

            slueEquipment.Properties.DataSource = _equipmentService.GetAll().ToList();
            FilterGrid();
        }
        private void LoadAll()
        {
            LoadTechSpecs();
            LoadDropdowns();
        }
        private void LoadDropdowns()
        {
            var users = _userManager.GetUsers();
            sluePreparedById.Properties.DataSource = users;
            slueReviewedById.Properties.DataSource = users;
            slueNotedById.Properties.DataSource = users;
        }
        private async Task LoadDetails()
        {
            var row = (TechSpecsViewModel)gridTechSpecs.GetFocusedRow();
            if (row == null) return;
            if (!IsTechSpecs)
            {
                colRepair.Visible = true;
                colTicket.Visible = false;
            }
            else
            {
                colRepair.Visible = false;
                colTicket.Visible = true;
            }
            var ts = await _techSpecsService.GetByFilterAsync(x => x.Id == row.Id,
                x => x.TicketRequest);
            if(ts == null) return;
            spbTicketStatus.SelectedItemIndex = ((int)ts.TicketRequest.TicketStatus) - 1;
            _tsMapper.MapControl(ts, groupControl1, groupControl2, groupControl3);

            var reqByUser = HRMISEmployees.GetEmployeeById(ts.ReqById);
            var chief = HRMISEmployees.GetChief(reqByUser?.Office , reqByUser?.Division, ts.ReqById);

            txtRequestingOfficeChief.Text = HRMISEmployees.GetEmployeeById(chief?.ChiefId)?.Employee;
            txtRequestingOfficeChiefPos.Text = HRMISEmployees.GetEmployeeById(chief?.ChiefId)?.Position;
            txtRequestedByEmployeeName.Text = reqByUser?.Employee;
            txtRequestedByPos.Text = reqByUser?.Position;
            txtRequestedByOffice.Text = reqByUser?.Office;
            txtRequestedByDivision.Text = reqByUser?.Division;
        }

        private void LoadTechSpecs()
        {
            var ts = _techSpecsService.GetTSViewModels(IsTechSpecs);
            gcTechSpecs.DataSource = new BindingList<TechSpecsViewModel>(ts.ToList());
        }

        private async Task LoadSpecs()
        {
            var row = (TechSpecsViewModel)gridTechSpecs.GetFocusedRow();
            if (row == null) return;

            lblTSId.Text = row.Id.ToString();
            var ts = await _techSpecsService.GetByIdAsync(row.Id);

            var navigation = _serviceProvider.GetRequiredService<IControlNavigator<UCRequestedTechSpecs>>();
            navigation.NavigateTo(tabRequestedSpecs, act => act.InitUC(ts));
        }
        private void LoadEvaluationSheet()
        {
            var row = (TechSpecsViewModel)gridTechSpecs.GetFocusedRow();
            if (row == null) return;
            var navigation = _serviceProvider.GetRequiredService<IControlNavigator<UCEvaluationSheet>>();
            navigation.NavigateTo(tabEvaluation, act => act.InitForm(new ActionType { Id = row.Id, RequestType = RequestType.TechSpecs }));
        }

        private async void LoadStaff()
        {
            var row = (TechSpecsViewModel)gridTechSpecs.GetFocusedRow();
            var res = await _staffService.GetStaffModel(row.Id);

            var navigation = _serviceProvider.GetRequiredService<IControlNavigator<UCAssignedTo>>();
            navigation.NavigateTo(staffPanel, act => act.InitUC(res));
        }

        private void LoadActions()
        {
            var row = (TechSpecsViewModel)gridTechSpecs.GetFocusedRow();
            tabAction.Controls.Clear();
            if (row == null) return;

            var navigation = _serviceProvider.GetRequiredService<IControlNavigator<UCActions>>();
            navigation.NavigateTo(tabAction, act => act.setActions(new ActionType { Id = row.Id, RequestType = Models.Enums.RequestType.TechSpecs }));
        }

        private async void btnEdit_Click(object sender, System.EventArgs e)
        {
            var row = (TechSpecsViewModel)gridTechSpecs.GetFocusedRow();
            var ts = await _techSpecsService.GetByIdAsync(row.Id);
            var frm = _serviceProvider.GetRequiredService<frmAddEditTechSpecs>();
            await frm.InitForTSForm(ts);
            frm.ShowDialog();
            await LoadSpecs();
        }

        private void UCTechSpecs_Load(object sender, EventArgs e)
        {
            LoadAll();
            if(!IsTechSpecs)
                lblTechSpecs.Text = "Recommended Repair Specs";
            ApplyFilterText();
        }

        public void ApplyFilterText()
        {
            if (filterText != null) gridTechSpecs.ActiveFilterCriteria = new BinaryOperator("TicketId", filterText);
        }

        private async void btnVerifyPR_Click(object sender, EventArgs e)
        {
            var row = (TechSpecsViewModel)gridTechSpecs.GetFocusedRow();
            var res = await _prService.GetByFilterAsync(x => x.TechSpecsId == row.Id);

            if (res == null) await CreatePR(row);
            else await GotoPR(row);
        }

        private async Task GotoPR(TechSpecsViewModel row)
        {
            var res = await _techSpecsService.GetByIdAsync(row.Id);
            var pr = await _prService.GetByFilterAsync(x => x.TechSpecsId == row.Id);

            var mainForm = _serviceProvider.GetRequiredService<frmMain>();
            var navigation = _serviceProvider.GetRequiredService<IControlNavigator<UCRepair>>();
            navigation.NavigateTo(mainForm.mainPanel, act => act.filterText = pr.Id.ToString());
        }

        private async Task CreatePR(TechSpecsViewModel row)
        {
            if (MessageBox.Show("No PR Saved for this Tech Specs, Verify New PR?",
                                "Question", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel) return;

            var res = await _techSpecsService.GetByIdAsync(row.Id);
            var pr = await _prService.AddAsync(new PurchaseRequest()
            {
                ChiefId = res.ReqByChiefId,
                TechSpecsId = res.Id,
                ReqById = res.ReqById
            });

            var mainForm = _serviceProvider.GetRequiredService<frmMain>();
            var navigation = _serviceProvider.GetRequiredService<IControlNavigator<UCRepair>>();
            navigation.NavigateTo(mainForm.mainPanel, act => act.filterText = pr.Id.ToString());
        }

        private async void gridTechSpecs_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            var row = (TechSpecsViewModel)gridTechSpecs.GetFocusedRow();
            if(row == null) return; 
            await LoadDetails();
            await LoadSpecs();
            LoadEvaluationSheet();
            LoadActions();
            LoadStaff();
        }

        private void hplTicket_Click(object sender, EventArgs e)
        {
            var row = (TechSpecsViewModel)gridTechSpecs.GetFocusedRow();
            var mainForm = _serviceProvider.GetRequiredService<frmMain>();
            var navigation = _serviceProvider.GetRequiredService<IControlNavigator<UCTARequestDashboard>>();
            navigation.NavigateTo(mainForm.mainPanel, act => act.filterText = row.Id.ToString());
        }

        private async void btnPreview_Click(object sender, EventArgs e)
        {
            await PrintReport();
        }

        private async Task PrintReport()
        {
            var row = (TechSpecsViewModel)gridTechSpecs.GetFocusedRow();
            var data = await _techSpecsService.GetReportViewModel(row.Id);

            var rpt = new rptTechSpecs
            {
                DataSource = new List<TechSpecsReportViewModel> { data }
            };

            var frm = _serviceProvider.GetRequiredService<frmReportViewer>();
            frm.InitForm(rpt);
            frm.ShowDialog();
        }

        private void hplRepair_Click(object sender, EventArgs e)
        {
            var row = (TechSpecsViewModel)gridTechSpecs.GetFocusedRow();
            var mainForm = _serviceProvider.GetRequiredService<frmMain>();
            var navigation = _serviceProvider.GetRequiredService<IControlNavigator<UCRepair>>();
            navigation.NavigateTo(mainForm.mainPanel, act => act.filterText = row.RepairId.ToString());
        }

        private void slueEquipment_EditValueChanged(object sender, EventArgs e)
        {
            FilterGrid();
        }

        private void slueTaskOf_EditValueChanged(object sender, EventArgs e)
        {
            FilterGrid();
        }

        private void ceCompleted_CheckedChanged(object sender, EventArgs e)
        {
            FilterGrid();
        }

        private void btnFilterbyDate_Click(object sender, EventArgs e)
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
        private void FilterGrid()
        {
            gridTechSpecs.ActiveFilterCriteria = null;

            var dateFrom = deFrom.DateTime;
            var dateTo = deTo.DateTime;
            var equipment = (string)slueEquipment.EditValue;
            var isCompleted = ceCompleted.Checked;
            var row = slueTaskOf.EditValue;

            var criteria = gridTechSpecs.ActiveFilterCriteria;

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

            gridTechSpecs.ActiveFilterCriteria = criteria;
        }
    }
}
