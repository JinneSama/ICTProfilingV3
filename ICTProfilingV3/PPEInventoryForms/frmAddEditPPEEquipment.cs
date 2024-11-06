using DevExpress.XtraEditors;
using ICTProfilingV3.DeliveriesForms;
using ICTProfilingV3.LookUpTables;
using Models.Entities;
using Models.Enums;
using Models.Repository;
using Models.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ICTProfilingV3.PPEInventoryForms
{
    public partial class frmAddEditPPEEquipment : DevExpress.XtraEditors.XtraForm
    {
        private IUnitOfWork unitOfWork;
        private SaveType saveType;
        private readonly PPEs _PPE;
        private PPEsSpecs _PPEsSpecs;
        public frmAddEditPPEEquipment(PPEs PPE, IUnitOfWork _unitOfWork)
        {
            InitializeComponent();
            saveType = SaveType.Insert;
            unitOfWork = _unitOfWork;
            LoadDropdowns();
            _PPE = PPE;
        }

        public frmAddEditPPEEquipment(PPEsSpecs ppesSpecs, IUnitOfWork _unitOfWork)
        {
            InitializeComponent();
            _PPEsSpecs = ppesSpecs;
            saveType = SaveType.Update;
            unitOfWork = _unitOfWork;
            LoadDropdowns();
            LoadEquipmentSpecs();
        }
        private void LoadEquipmentSpecs()
        {
            spinQty.Value = _PPEsSpecs.Quantity;
            cboUnit.EditValue = _PPEsSpecs.Unit;
            spinUnitCost.Value = _PPEsSpecs.UnitCost;
            spintTotal.Value = _PPEsSpecs.TotalCost;
            spinItemNo.Value = _PPEsSpecs.ItemNo;
            txtSerialNo.Text = _PPEsSpecs.SerialNo;

            slueEquipment.EditValue = _PPEsSpecs.Model.Brand.EquipmenSpecsId;
            txtDescription.Text = _PPEsSpecs.Description;

            var brand = unitOfWork.BrandRepo.FindAllAsync(x => x.EquipmenSpecsId == _PPEsSpecs.Model.Brand.EquipmenSpecsId);
            slueBrand.Properties.DataSource = brand.ToList();

            slueBrand.EditValue = _PPEsSpecs.Model.BrandId;

            var model = unitOfWork.ModelRepo.FindAllAsync(x => x.BrandId == _PPEsSpecs.Model.BrandId);
            slueModel.Properties.DataSource = model.ToList();

            slueModel.EditValue = _PPEsSpecs.ModelId;
        }

        private void LoadDropdowns()
        {
            var res = unitOfWork.EquipmentSpecsRepo.GetAll().Select(x => new EquipmentSpecsViewModel
            {
                Description = x.Description,
                Equipment = x.Equipment.EquipmentName,
                Id = x.Id
            });
            slueEquipment.Properties.DataSource = res.ToList();
            cboUnit.Properties.DataSource = Enum.GetValues(typeof(Unit)).Cast<Unit>().Select(x => new
            {
                Unit = x
            });
        }

        private void slueEquipment_EditValueChanged(object sender, EventArgs e)
        {
            var res = (EquipmentSpecsViewModel)slueEquipment.Properties.View.GetFocusedRow();
            if (res == null) return;

            txtDescription.Text = res.Description;

            var brand = unitOfWork.BrandRepo.FindAllAsync(x => x.EquipmenSpecsId == res.Id);
            slueBrand.Properties.DataSource = brand.ToList();
        }

        private void slueBrand_EditValueChanged(object sender, EventArgs e)
        {
            var res = (Brand)slueBrand.Properties.View.GetFocusedRow();
            if (res == null) return;

            var model = unitOfWork.ModelRepo.FindAllAsync(x => x.BrandId == res.Id);
            slueModel.Properties.DataSource = model.ToList();
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            if (saveType == SaveType.Insert) await InsertEquipment();
            else await UpdateEquipment();
        }
        private async Task InsertEquipment()
        {
            var equipment = new PPEsSpecs
            {
                PPEs = _PPE,
                Quantity = (int)spinQty.Value,
                Unit = (Unit)cboUnit.EditValue,
                Model = slueModel.Properties.View.GetFocusedRow() as Model,
                UnitCost = (long)spinUnitCost.Value,
                TotalCost = (long)spintTotal.Value,
                SerialNo = txtSerialNo.Text,
                ItemNo = (int)spinItemNo.Value,
                Description = txtDescription.Text
            };
            unitOfWork.PPEsSpecsRepo.Insert(equipment);
            await unitOfWork.SaveChangesAsync();

            var res = (EquipmentSpecsViewModel)slueEquipment.Properties.View.GetFocusedRow();
            var specs = await unitOfWork.EquipmentSpecsRepo.FindAsync(x => x.Id == res.Id);
            if (specs.EquipmentSpecsDetails.Count > 0)
            {
                if (MessageBox.Show("There are Existing Specs for this Equipment, Copy the Specs?",
                    "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) == DialogResult.OK) await InsertSpecs(specs.EquipmentSpecsDetails, equipment);
            }
            this.Close();
        }

        private async Task InsertSpecs(IEnumerable<EquipmentSpecsDetails> specs, PPEsSpecs ppesSpecs)
        {
            foreach (var spec in specs)
            {
                var delSpecsDetails = new PPEsSpecsDetails()
                {
                    ItemNo = spec.ItemNo,
                    Specs = spec.DetailSpecs,
                    Description = spec.DetailDescription,
                    PPEsSpecs = ppesSpecs
                };
                unitOfWork.PPEsSpecsDetailsRepo.Insert(delSpecsDetails);
            }
            await unitOfWork.SaveChangesAsync();
        }

        private async Task UpdateEquipment()
        {
            var ppeSpecs = await unitOfWork.PPEsSpecsRepo.FindAsync(x => x.Id == _PPEsSpecs.Id);
            ppeSpecs.Quantity = (int)spinQty.Value;
            ppeSpecs.Unit = (Unit)cboUnit.EditValue;
            ppeSpecs.Model = slueModel.Properties.View.GetFocusedRow() as Model;
            ppeSpecs.UnitCost = (long)spinUnitCost.Value;
            ppeSpecs.TotalCost = (long)spintTotal.Value;
            ppeSpecs.SerialNo = txtSerialNo.Text;
            ppeSpecs.ItemNo = (int)spinItemNo.Value;
            ppeSpecs.Description = txtDescription.Text;

            await unitOfWork.SaveChangesAsync();
            this.Close();
        }

        private void btnAddEquipment_Click(object sender, EventArgs e)
        {
            var frm = new frmEquipment();
            frm.ShowDialog();
            LoadDropdowns();
        }

        private void btnAddICTSpecs_Click(object sender, EventArgs e)
        {
            var frm = new frmEquipmentSpecs();
            frm.ShowDialog();
            LoadDropdowns();
        }

        private void btnAddBrand_Click(object sender, EventArgs e)
        {
            var frm = new frmEquipmentBrand();
            frm.ShowDialog();
            LoadDropdowns();
        }

        private void btnAddModel_Click(object sender, EventArgs e)
        {
            var frm = new frmEquipmentModels();
            frm.ShowDialog();
            LoadDropdowns();
        }
        private void CalcTotalValue()
        {
            spintTotal.Value = spinUnitCost.Value * spinQty.Value;
        }

        private void spinQty_EditValueChanged(object sender, EventArgs e)
        {
            CalcTotalValue();
        }

        private void spinUnitCost_EditValueChanged(object sender, EventArgs e)
        {
            CalcTotalValue();
        }
    }
}