using DevExpress.Data.Filtering;
using Helpers.Interfaces;
using Helpers.Inventory;
using ICTProfilingV3.DataTransferModels.ViewModels;
using ICTProfilingV3.Interfaces;
using ICTProfilingV3.Services.Employees;
using Microsoft.Extensions.DependencyInjection;
using Models.Enums;
using Models.Repository;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ICTProfilingV3.PPEInventoryForms
{
    public partial class UCPPEs : DevExpress.XtraEditors.XtraUserControl
    {
        private readonly IServiceProvider _serviceProvider;
        public string filterText { get; set; }
        public UCPPEs(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
            LoadDropdowns();
        }

        private async Task LoadPPEs()
        {
            IUnitOfWork unitOfWork = new UnitOfWork();
            var ppe = await unitOfWork.PPesRepo.GetAll().Include(i => i.Repairs).ToListAsync();
            
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
            IUnitOfWork unitOfWork = new UnitOfWork();
            var row = (PPEsViewModel)gridPPEs.GetFocusedRow();
            gcHistory.Controls.Clear();
            if (row == null) return;
            var ppe = await unitOfWork.PPesRepo.FindAsync(x => x.Id == row.Id);

            var mainForm = _serviceProvider.GetRequiredService<frmMain>();
            var ucRepairHistory = _serviceProvider.GetRequiredService<UCRepairHistory>();
            ucRepairHistory.Dock = DockStyle.Fill;
            ucRepairHistory.SetPPE(ppe);
            gcHistory.Controls.Add(ucRepairHistory);
        }
        private async Task LoadEquipmentSpecs()
        {
            IUnitOfWork unitOfWork = new UnitOfWork();
            var row = (PPEsViewModel)gridPPEs.GetFocusedRow();
            gcEquipmentSpecs.Controls.Clear();
            if (row == null) return;

            var uow = new UnitOfWork();
            var ppe = await uow.PPesRepo.FindAsync(x => x.Id == row.Id);
            gcEquipmentSpecs.Controls.Add(new UCPPEsSpecs(ppe)
            {
                Dock = DockStyle.Fill
            });
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
            var uow = new UnitOfWork();
            var row = (PPEsViewModel)gridPPEs.GetFocusedRow();
            if (row == null) return;
            lblPropertyNo.Text = row.PropertyNo;
            var ppe = await uow.PPesRepo.FindAsync(x => x.Id == row.Id);
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
            var frm = new frmAddEditPPEs(SaveType.Insert , null);
            frm.ShowDialog();

            await LoadPPEs();
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
            IUnitOfWork unitOfWork = new UnitOfWork();
            var row = (PPEsViewModel)gridPPEs.GetFocusedRow();
            var ppe = await unitOfWork.PPesRepo.FindAsync(x => x.Id == row.Id);
            var frm = new frmAddEditPPEs(SaveType.Update , ppe);
            frm.ShowDialog();

            await LoadPPEs();
        }

        private async void gridPPEs_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            await LoadDetails();
            await LoadEquipmentSpecs();
            await LoadHistory();
        }

        private async void simpleButton1_Click(object sender, EventArgs e)
        {
            IUnitOfWork unitOfWork = new UnitOfWork();
            var row = (PPEsViewModel)gridPPEs.GetFocusedRow();
            var ppe = await unitOfWork.PPesRepo.FindAsync(x => x.Id == row.Id);
            IParseInventory parseInventory = new ParseInventory();
            var res = await parseInventory.Parse(row.PropertyNo, ppe.Remarks);

            int handle = gridPPEs.FocusedRowHandle;
            var frm = new frmInventoryParser(res, row.PropertyNo);
            frm.ShowDialog();

            await LoadPPEs();
            gridPPEs.FocusedRowHandle = handle;
        }
    }
}
