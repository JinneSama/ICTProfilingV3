using DevExpress.Data.Filtering;
using ICTProfilingV3.DataTransferModels.ViewModels;
using ICTProfilingV3.Interfaces;
using ICTProfilingV3.Services.Employees;
using Microsoft.Extensions.DependencyInjection;
using Models.Enums;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace ICTProfilingV3.PPEInventoryForms
{
    public partial class UCPPEs : DevExpress.XtraEditors.XtraUserControl
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IPPEInventoryService _inventoryService;
        private readonly IParseInventory _inventoryParser;
        public string filterText { get; set; }
        public UCPPEs(IServiceProvider serviceProvider, IPPEInventoryService inventoryService, IParseInventory inventoryParser)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
            _inventoryService = inventoryService;
            _inventoryParser = inventoryParser;
            LoadDropdowns();
        }

        private async Task LoadPPEs()
        {
            var ppe = await _inventoryService.GetAll().Include(i => i.Repairs).ToListAsync();
            
            var ppeModel = ppe.Select(x => new PPEsViewModel
            {
                Id = x.Id,
                PropertyNo = x.PropertyNo,
                IssuedTo = HRMISEmployees.GetEmployeeById(x.IssuedToId)?.Employee,
                DateCreated = x.AquisitionDate,
                Office = HRMISEmployees.GetEmployeeById(x.IssuedToId)?.Office,
                Status = x?.Status,
                IsResigned = HRMISEmployees.GetEmployeeById(x.IssuedToId)?.IsResigned ?? false,
                RepairCount = x.Repairs.Count
            });

            gcPPEs.DataSource = ppeModel;
        }

        private async Task LoadHistory()
        {
            var row = (PPEsViewModel)gridPPEs.GetFocusedRow();
            gcHistory.Controls.Clear();
            if (row == null) return;
            var ppe = await _inventoryService.GetByIdAsync(row.Id);

            var nav = _serviceProvider.GetRequiredService<IControlNavigator<UCRepairHistory>>();
            nav.NavigateTo(gcHistory, act => act.SetPPE(ppe));
        }
        private async Task LoadEquipmentSpecs()
        {
            var row = (PPEsViewModel)gridPPEs.GetFocusedRow();
            gcEquipmentSpecs.Controls.Clear();
            if (row == null) return;

            var ppe = await _inventoryService.GetByIdAsync(row.Id);

            var navigation = _serviceProvider.GetRequiredService<IControlNavigator<UCPPEsSpecs>>();
            navigation.NavigateTo(gcEquipmentSpecs, act => act.InitUC(ppe));
        }
        private void LoadDropdowns()
        {
            cboStatus.Properties.DataSource = Enum.GetValues(typeof(PPEStatus)).Cast<PPEStatus>().Select(x => new
            {
                Status = x
            });
        }

        private async Task LoadDetails()
        {
            var row = (PPEsViewModel)gridPPEs.GetFocusedRow();
            if (row == null) return;
            lblPropertyNo.Text = row.PropertyNo;
            var ppe = await _inventoryService.GetByIdAsync(row.Id);
            txtEmployee.Text = HRMISEmployees.GetEmployeeById(ppe?.IssuedToId)?.Employee ?? "";
            txtContactNo.Text = ppe.ContactNo;
            rdbtnGender.SelectedIndex = (int)(ppe?.Gender ?? 0);
            txtPropertyNo.Text = ppe.PropertyNo;
            txtInvoiceDate.DateTime = ppe.AquisitionDate ?? DateTime.MinValue;
            cboStatus.EditValue = ppe.Status;
            txtRemarks.Text = ppe.Remarks;
        }

        private async void btnAdd_Click(object sender, System.EventArgs e)
        {
            var frm = _serviceProvider.GetRequiredService<frmAddEditPPEs>();
            await frm.InitForm(SaveType.Insert, null);
            frm.ShowDialog();

            await LoadPPEs();
            await LoadDetails();
        }

        private async void UCPPEs_Load(object sender, System.EventArgs e)
        {
            await LoadPPEs();
            ApplyFilterText();
        }

        public void ApplyFilterText()
        {
            if (filterText != null) gridPPEs.ActiveFilterCriteria = new BinaryOperator("PropertyNo", filterText);
        }

        private async void btnEdit_Click(object sender, EventArgs e)
        {
            var row = (PPEsViewModel)gridPPEs.GetFocusedRow();
            var ppe = await _inventoryService.GetByIdAsync(row.Id);
            var frm = _serviceProvider.GetRequiredService<frmAddEditPPEs>();
            await frm.InitForm(SaveType.Update, ppe);
            frm.ShowDialog();

            await LoadPPEs();
            await LoadDetails();
        }

        private async void gridPPEs_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            await LoadDetails();
            await LoadEquipmentSpecs();
            await LoadHistory();
        }

        private async void simpleButton1_Click(object sender, EventArgs e)
        {
            var row = (PPEsViewModel)gridPPEs.GetFocusedRow();
            var ppe = await _inventoryService.GetByIdAsync(row.Id);
            var res = await _inventoryParser.Parse(row.PropertyNo, ppe.Remarks);

            int handle = gridPPEs.FocusedRowHandle;
            var frm = _serviceProvider.GetRequiredService<frmInventoryParser>();
            frm.InitForm(res, row.PropertyNo);
            frm.ShowDialog();

            await LoadPPEs();
            gridPPEs.FocusedRowHandle = handle;
        }
    }
}
