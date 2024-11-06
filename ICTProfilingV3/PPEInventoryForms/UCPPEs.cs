using Models.Enums;
using Models.HRMISEntites;
using Models.Repository;
using Models.ViewModels;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ICTProfilingV3.PPEInventoryForms
{
    public partial class UCPPEs : DevExpress.XtraEditors.XtraUserControl
    {
        private readonly IUnitOfWork _unitOfWork;
        public UCPPEs()
        {
            InitializeComponent();
            _unitOfWork = new UnitOfWork();
            LoadDropdowns();
        }

        private async Task LoadPPEs()
        {
            var ppe = await _unitOfWork.PPesRepo.GetAll().ToListAsync();
            
            var ppeModel = ppe.Select(x => new PPEsViewModel
            {
                Id = x.Id,
                PropertyNo = x.PropertyNo,
                IssuedTo = HRMISEmployees.GetEmployeeById(x.IssuedToId)?.Employee,
                DateCreated = x.DateCreated,
                Office = HRMISEmployees.GetEmployeeById(x.IssuedToId)?.Office,
                Status = x?.Status
            });
            gcPPEs.DataSource = ppeModel;
        }
        private async Task LoadEquipmentSpecs()
        {
            var row = (PPEsViewModel)gridPPEs.GetFocusedRow();
            var ppe = await _unitOfWork.PPesRepo.FindAsync(x => x.Id == row.Id);  
            gcEquipmentSpecs.Controls.Clear();
            gcEquipmentSpecs.Controls.Add(new UCPPEsSpecs(ppe, _unitOfWork)
            {
                Dock = DockStyle.Fill
            });
        }
        private void LoadDropdowns()
        {
            var employees = HRMISEmployees.GetEmployees();
            slueEmployee.Properties.DataSource = employees;

            cboUnit.Properties.DataSource = Enum.GetValues(typeof(Unit)).Cast<Unit>().Select(x => new
            {
                Unit = x
            });

            cboStatus.Properties.DataSource = Enum.GetValues(typeof(PPEStatus)).Cast<PPEStatus>().Select(x => new
            {
                Status = x
            });
        }

        private async Task LoadDetails()
        {
            var row = (PPEsViewModel)gridPPEs.GetFocusedRow();
            var ppe = await _unitOfWork.PPesRepo.FindAsync(x => x.Id == row.Id);
            slueEmployee.EditValue = (long?)ppe?.IssuedToId;
            txtContactNo.Text = ppe.ContactNo;
            rdbtnGender.SelectedIndex = (int)(ppe?.Gender ?? 0);
            txtPropertyNo.Text = ppe.PropertyNo;
            txtInvoiceDate.DateTime = ppe.AquisitionDate ?? DateTime.MinValue;
            cboStatus.EditValue = ppe.Status;
            txtRemarks.Text = ppe.Remarks;
            spinQty.Value = (decimal)ppe.Quantity;
            cboUnit.EditValue = (Unit)ppe.Unit;
            spinUnitCost.Value = (decimal?)ppe.UnitValue ?? 0;
            spintTotal.Value = (decimal?)ppe.TotalValue ?? 0;
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
        }

        private async void gridPPEs_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            await LoadDetails();
            await LoadEquipmentSpecs();
        }

        private async void btnEdit_Click(object sender, EventArgs e)
        {
            var row = (PPEsViewModel)gridPPEs.GetFocusedRow();
            var ppe = await _unitOfWork.PPesRepo.FindAsync(x => x.Id == row.Id);
            var frm = new frmAddEditPPEs(SaveType.Update , ppe);
            frm.ShowDialog();

            await LoadPPEs();
        }
    }
}
