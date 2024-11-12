using Models.Entities;
using Models.Enums;
using Models.HRMISEntites;
using Models.Repository;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ICTProfilingV3.PPEInventoryForms
{
    public partial class frmAddEditPPEs : DevExpress.XtraEditors.XtraForm
    {
        private readonly IUnitOfWork unitOfWork;
        private PPEs _PPEs;
        private readonly SaveType _SaveType;
        private bool IsSave = false;

        public frmAddEditPPEs(SaveType saveType , PPEs ppe)
        {
            InitializeComponent();
            unitOfWork = new UnitOfWork();
            LoadDropdowns();
            _SaveType = saveType;

            if (saveType == SaveType.Insert) CreatePPE();
            else
            {
                _PPEs = ppe;
                IsSave = true;
            }
            LoadDetails();
        }

        private void CreatePPE()
        {
            var newPPE = new PPEs();
            unitOfWork.PPesRepo.Insert(newPPE);
            unitOfWork.Save();

            _PPEs = newPPE;

            LoadEquipmentSpecs();
        }

        private void LoadDetails()
        {
            if (_SaveType == SaveType.Insert) return;
            
            slueEmployee.EditValue = _PPEs.IssuedToId;
            txtContactNo.Text = _PPEs.ContactNo;
            rdbtnGender.SelectedIndex = (int)(_PPEs?.Gender ?? 0);
            txtPropertyNo.Text = _PPEs.PropertyNo;
            spinQty.Value = _PPEs.Quantity;
            spinUnitCost.Value = (decimal)(_PPEs.UnitValue ?? 0);
            spintTotal.Value = (decimal)(_PPEs.TotalValue ?? 0);
            cboUnit.EditValue = _PPEs.Unit;
            txtInvoiceDate.DateTime = _PPEs?.DateCreated ?? DateTime.UtcNow;
            cboStatus.EditValue = _PPEs.Status;
            txtRemarks.Text = _PPEs.Remarks;    
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

        private void LoadEquipmentSpecs()
        {
            gcEquipmentSpecs.Controls.Clear();
            gcEquipmentSpecs.Controls.Add(new UCPPEsSpecs(_PPEs, unitOfWork)
            {
                Dock = DockStyle.Fill
            });
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            var editPPE = await unitOfWork.PPesRepo.FindAsync(x => x.Id == _PPEs.Id);
            editPPE.IssuedToId = (long?)slueEmployee.EditValue;
            editPPE.ContactNo = txtContactNo.Text;
            editPPE.Gender = (Gender)rdbtnGender.SelectedIndex;
            editPPE.PropertyNo = txtPropertyNo.Text;
            editPPE.AquisitionDate = txtInvoiceDate.DateTime;
            editPPE.Status = (PPEStatus)cboStatus.EditValue;
            editPPE.Remarks = txtRemarks.Text;
            editPPE.Quantity = (int)spinQty.Value;
            editPPE.Unit = (Unit)cboUnit.EditValue;
            editPPE.UnitValue = (long?)spinUnitCost.Value;
            editPPE.TotalValue = (long?)spintTotal.Value;
            editPPE.DateCreated = DateTime.UtcNow;

            unitOfWork.Save();
            IsSave = true;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void frmAddEditPPEs_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!IsSave) await DeletePPE();
        }

        private async Task DeletePPE()
        {
            unitOfWork.PPEsSpecsRepo.DeleteRange(x => x.PPEsId == _PPEs.Id);
            await unitOfWork.PPesRepo.DeleteByEx(x => x.Id == _PPEs.Id);
            await unitOfWork.SaveChangesAsync();
        }
    }
}