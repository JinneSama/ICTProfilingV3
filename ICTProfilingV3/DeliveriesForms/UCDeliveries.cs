using DevExpress.Data.Filtering;
using ICTProfilingV3.ActionsForms;
using ICTProfilingV3.API.FilesApi;
using ICTProfilingV3.Core.Common;
using ICTProfilingV3.DataTransferModels.Models;
using ICTProfilingV3.DataTransferModels.ReportViewModel;
using ICTProfilingV3.DataTransferModels.ViewModels;
using ICTProfilingV3.EvaluationForms;
using ICTProfilingV3.Interfaces;
using ICTProfilingV3.ReportForms;
using ICTProfilingV3.Services.Employees;
using ICTProfilingV3.TicketRequestForms;
using ICTProfilingV3.ToolForms;
using Microsoft.Extensions.DependencyInjection;
using Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ICTProfilingV3.DeliveriesForms
{
    public partial class UCDeliveries : DevExpress.XtraEditors.XtraUserControl
    {
        private readonly HTTPNetworkFolder networkFolder;
        private readonly UserStore _userStore;

        private readonly IUCManager _ucManager;
        private readonly IServiceProvider _serviceProvider;
        private readonly IStaffService _staffService;
        private readonly IEquipmentService _equipmentService;
        private readonly IICTUserManager _userManager;
        private readonly IDeliveriesService _deliveriesService;
        private readonly IComparisonReportService _comparisonReportService;
        private readonly IControlMapper<DeliveriesDetailsViewModel> _delDetailsMapper;

        public string filterText { get; set; }
        public UCDeliveries(IUCManager ucManager, IServiceProvider serviceProvider, UserStore userStore,
            IStaffService staffService, IEquipmentService equipmentService, IICTUserManager userManager,
            IDeliveriesService deliveriesService, IComparisonReportService comparisonReportService, IControlMapper<DeliveriesDetailsViewModel> delDetailsMapper)
        {
            _ucManager = ucManager;
            _serviceProvider = serviceProvider;
            networkFolder = new HTTPNetworkFolder();
            _userStore = userStore;
            _staffService = staffService;
            _userManager = userManager;
            _equipmentService = equipmentService;
            _deliveriesService = deliveriesService;
            _comparisonReportService = comparisonReportService;
            _delDetailsMapper = delDetailsMapper;

            InitializeComponent();
            LoadFilterDropdowns();
        }

        private void LoadDropdown()
        {
            var staff = _userManager.GetUsers().ToList();
            var res = staff.OrderBy(o => o.FullName);
        }
        private void LoadFilterDropdowns()
        {
            var users = _staffService.GetAll().
                Select(x => x.Users).ToList();
            slueTaskOf.Properties.DataSource = users;

            slueEquipment.Properties.DataSource = _equipmentService.GetAll().ToList();
            FilterGrid();
        }
        private async Task LoadDeliveries()
        {
            var delData = await _deliveriesService.GetDeliveriesViewModels();
            gcDeliveries.DataSource = new BindingList<DeliveriesViewModel>(delData.ToList());
        }

        private async Task LoadStaff()
        {
            var row = (DeliveriesViewModel)gridDeliveries.GetFocusedRow();
            var res = await _staffService.GetStaffModel(row.Id);

            var navigation = _serviceProvider.GetRequiredService<IControlNavigator<UCAssignedTo>>();
            navigation.NavigateTo(staffPanel, act => act.InitUC(res));
        }

        private void LoadDetails()
        {
            var row = (DeliveriesViewModel)gridDeliveries.GetFocusedRow();
            if (row == null) return;

            spbTicketStatus.SelectedItemIndex = ((int)row.Status) - 1;
            var detailsModel = _deliveriesService.GetDeliveriesDetailViewModels(row);
            _delDetailsMapper.MapControl(detailsModel, groupControl1, groupControl3, pnlButtons);
        }
        private void LoadEvaluationSheet()
        {
            var row = (DeliveriesViewModel)gridDeliveries.GetFocusedRow();
            if (row == null) return;
            var navigation = _serviceProvider.GetRequiredService<IControlNavigator<UCEvaluationSheet>>();
            navigation.NavigateTo(tabEvaluation, act => act.InitForm(new ActionType { Id = row.Id, RequestType = RequestType.Deliveries }));
        }
        private void LoadEquipmentSpecs()
        {
            var row = (DeliveriesViewModel)gridDeliveries.GetFocusedRow();
            if (row == null) return;

            var navigation = _serviceProvider.GetRequiredService<IControlNavigator<UCDeliveriesSpecs>>();
            navigation.NavigateTo(tabEquipmentSpecs, act => act.InitUC(row.Id));
        }
        
        private void LoadActions()
        {
            var row = (DeliveriesViewModel)gridDeliveries.GetFocusedRow();
            tabAction.Controls.Clear();

            if (row == null) return;

            var navigation = _serviceProvider.GetRequiredService<IControlNavigator<UCActions>>();
            navigation.NavigateTo(tabAction, act => act.setActions(new ActionType
            {
                Id = row.Id,
                RequestType = RequestType.Deliveries
            }));
        }

        private async void btnEdit_Click(object sender, System.EventArgs e)
        {
            var rowHandle = gridDeliveries.FocusedRowHandle;
            var row = (DeliveriesViewModel)gridDeliveries.GetFocusedRow();
            var deliveries = await _deliveriesService.GetByIdAsync(row.Id);
            var frm = _serviceProvider.GetRequiredService<frmAddEditDeliveries>();
            await frm.InitForm(deliveries);
            frm.ShowDialog();

            await LoadDeliveries();
            //await LoadEquipmentSpecs();
            LoadDetails();

            gridDeliveries.FocusedRowHandle = rowHandle;
        }

        private void btnPreview_Click(object sender, EventArgs e)
        {
            var item = (DeliveriesViewModel)gridDeliveries.GetFocusedRow();
            var ds = _deliveriesService.GetAll().Where(x => x.Id == item.Id)
                .Include(x => x.Supplier)
                .Include(x => x.DeliveriesSpecs.Select(s => s.Model))
                .Include(x => x.DeliveriesSpecs.Select(s => s.Model.Brand))
                .Include(x => x.DeliveriesSpecs.Select(s => s.Model.Brand.EquipmentSpecs))
                .Include(x => x.DeliveriesSpecs.Select(s => s.Model.Brand.EquipmentSpecs.Equipment))
                .ToList();

            var rpt = new rptDeliveries
            {
                DataSource = ds,
                Office = txtOffice.Text
            };

            var frm = _serviceProvider.GetRequiredService<frmReportViewer>();
            frm.InitForm(rpt);
            frm.ShowDialog();
        }

        private async Task PrintComparisonReport()
        {
            var item = (DeliveriesViewModel)gridDeliveries.GetFocusedRow();
            var comparisonModel = await _comparisonReportService.GetComparisonReportPrintModel(item.Id);

            var rptCR = new rptComparisonReport
            {
                DataSource = new List<ComparisonReportPrintViewModel> { comparisonModel }
            };

            rptCR.CreateDocument();
            var frm = _serviceProvider.GetRequiredService<frmReportViewer>();
            frm.InitForm(rptCR);
            frm.ShowDialog();
        }

        private async void UCDeliveries_Load(object sender, EventArgs e)
        {
            await LoadDeliveries();
            LoadDropdown();
            ApplyFilterText();
        }

        public void ApplyFilterText()
        {
            if (filterText != null) gridDeliveries.ActiveFilterCriteria = new BinaryOperator("TicketNo", filterText);
        }

        private async void gridDeliveries_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            await LoadAllDetails();
        }

        private async Task LoadAllDetails()
        {
            LoadEquipmentSpecs();
            LoadActions();
            LoadDetails();
            LoadEvaluationSheet();
            await LoadStaff();
        }

        private void hplTicket_Click(object sender, EventArgs e)
        {
            var row = (DeliveriesViewModel)gridDeliveries.GetFocusedRow();
            var mainForm = _serviceProvider.GetRequiredService<frmMain>();
            var navigation = _serviceProvider.GetRequiredService<IControlNavigator<UCTARequestDashboard>>();
            navigation.NavigateTo(mainForm.mainPanel, act => act.filterText = row.Id.ToString());
        }

        private async void btnCompReport_Click(object sender, EventArgs e)
        {
            var row = (DeliveriesViewModel)gridDeliveries.GetFocusedRow();
            var del = await _deliveriesService.GetByFilterAsync(x => x.Id == row.Deliveries.Id,
                x => x.Supplier,
                x => x.TicketRequest,
                x => x.TicketRequest.ITStaff,
                x => x.TicketRequest.ITStaff.Users,
                x => x.DeliveriesSpecs.Select(s => s.Model),
                x => x.DeliveriesSpecs.Select(s => s.Model.Brand),
                x => x.DeliveriesSpecs.Select(s => s.Model.Brand.EquipmentSpecs),
                x => x.DeliveriesSpecs.Select(s => s.Model.Brand.EquipmentSpecs.Equipment));

            if (del == null) return;

            var inspectActions = del?.Actions?.Where(x => x.SubActivityId == 1138 || x.SubActivityId == 1139).OrderBy(x => x.ActionDate);
            if (inspectActions == null || inspectActions.Count() <= 0)
            {
                MessageBox.Show("Technical Inspection is not yet Started on this Delivery!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if(_userStore.UserId != del.TicketRequest.ITStaff.UserId)
            {
                await PrintComparisonReport();
                return;
            }

            var frm = _serviceProvider.GetRequiredService<frmComparisonReport>();
            frm.InitForm(del);
            frm.ShowDialog();
        }

        private async void btnFindings_Click(object sender, EventArgs e)
        {
            //var row = (DeliveriesViewModel)gridDeliveries.GetFocusedRow();
            //var frm = new frmInitialFindings(row);
            //frm.ShowDialog();

            //await LoadDeliveries();
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
        private void FilterGrid()
        {
            gridDeliveries.ActiveFilterCriteria = null;

            var dateFrom = deFrom.DateTime;
            var dateTo = deTo.DateTime;
            var equipment = (string)slueEquipment.EditValue;
            var isCompleted = ceCompleted.Checked;
            var row = slueTaskOf.EditValue;

            var criteria = gridDeliveries.ActiveFilterCriteria;

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

            gridDeliveries.ActiveFilterCriteria = criteria;
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

        private void btnComparisonExcel_Click(object sender, EventArgs e)
        {
            var row = (DeliveriesViewModel)gridDeliveries.GetFocusedRow();
            var frm = _serviceProvider.GetRequiredService<frmComparisonReportMenu>();
            frm.InitForm(row.Id);
            frm.ShowDialog();
        }
    }
}
