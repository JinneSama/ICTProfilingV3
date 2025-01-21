using DevExpress.Utils.DirectXPaint.Svg;
using DevExpress.Utils.Filtering;
using ICTProfilingV3.DeliveriesForms;
using ICTProfilingV3.LookUpTables;
using Models.Entities;
using Models.Enums;
using Models.Repository;
using Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ICTProfilingV3.TechSpecsForms
{
    public partial class frmAddEditTechSpecsICTSpecs : DevExpress.XtraEditors.XtraForm
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly TechSpecsICTSpecsViewModel _specs;
        private readonly SaveType _saveType;

        public frmAddEditTechSpecsICTSpecs(TechSpecsICTSpecsViewModel specs, SaveType saveType)
        {
            InitializeComponent();
            unitOfWork = new UnitOfWork();
            _specs = specs;
            _saveType = saveType;
            LoadDropdowns();
            LoadDetails();
        }

        private async Task LoadItemNo()
        {
            if (_saveType == SaveType.Update) return;

            var ts = await unitOfWork.TechSpecsRepo.FindAsync(x => x.Id == _specs.TechSpecsId);
            if(ts == null) return;

            var itemNos = ts.TechSpecsICTSpecs?.OrderBy(x => x.ItemNo)?.LastOrDefault();
            var newItemNo = itemNos == null ? 0 : itemNos.ItemNo + 1;

            txtItemNo.Value = (decimal)newItemNo;
        }

        private void LoadDetails()
        {
            if (_saveType == SaveType.Insert) return;

            txtItemNo.Text = _specs.ItemNo.ToString();
            spinQuantity.Value = _specs.Quantity;
            slueEquipment.EditValue = _specs.EquipmentSpecsId;
            txtDescription.Text = _specs.Description;
            slueUnit.EditValue = _specs.Unit;
            spinUnitCost.EditValue = _specs.UnitCost;
            spintTotal.EditValue = _specs.TotalCost;
            txtPurpose.Text = _specs.Purpose;
        }

        private void LoadDropdowns()
        {
            slueUnit.Properties.DataSource = Enum.GetValues(typeof(Unit)).Cast<Unit>().Select(x => new
            {
                UnitType = x
            });
            var res = GetEquipmentDatasource();
            slueEquipment.Properties.DataSource = res;
        }

        private IEnumerable<EquipmentSpecsViewModel> GetEquipmentDatasource()
        {
            var res = unitOfWork.EquipmentSpecsRepo.GetAll(x => x.Equipment).Select(x => new EquipmentSpecsViewModel
            {
                Remarks = x.Remarks,
                Description = x.Description,
                Equipment = x.Equipment.EquipmentName,
                Id = x.Id
            });
            return res.ToList();
        }

        private void slueEquipment_EditValueChanged(object sender, System.EventArgs e)
        {
            var row = (EquipmentSpecsViewModel)slueEquipment.Properties.View.GetFocusedRow();
            if (row == null) row = GetEquipmentDatasource().Where(x => x.Id == _specs.EquipmentSpecsId).FirstOrDefault();

            txtDescription.Text = row.Description;
        }

        private void spinQuantity_EditValueChanged(object sender, EventArgs e)
        {
            spintTotal.Value = spinQuantity.Value * spinUnitCost.Value;
        }

        private void spinUnitCost_EditValueChanged(object sender, EventArgs e)
        {
            spintTotal.Value = spinQuantity.Value * spinUnitCost.Value;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (_saveType == SaveType.Insert) InsertSpecs();
            else UpdateSpecs();
            this.Close();
        }

        private async void UpdateSpecs()
        {
            var all = unitOfWork.TechSpecsICTSpecsRepo.GetAll();
            var specs = await unitOfWork.TechSpecsICTSpecsRepo.FindAsync(x => x.Id == _specs.Id);
            specs.ItemNo = (int)txtItemNo.Value;
            specs.Quantity = (int)spinQuantity.Value;
            specs.EquipmentSpecsId = (int)slueEquipment.EditValue;
            specs.Description = txtDescription.Text;
            specs.Unit = (Unit)slueUnit.EditValue;
            specs.UnitCost = (long)spinUnitCost.Value;
            specs.TotalCost = (long)spintTotal.Value;
            specs.Purpose = txtPurpose.Text;
            specs.TechSpecsId = _specs.TechSpecsId;

            unitOfWork.Save();
        }

        private void InsertSpecs()
        {
            var specs = new TechSpecsICTSpecs
            {
                ItemNo = (int)txtItemNo.Value,
                Quantity = (int)spinQuantity.Value,
                EquipmentSpecsId = (int)slueEquipment.EditValue,
                Description = txtDescription.Text,
                Unit = Unit.meter,
                UnitCost = (long)spinUnitCost.Value,
                TotalCost = (long)spintTotal.Value,
                Purpose = txtPurpose.Text,
                TechSpecsId = _specs.TechSpecsId
            };
            unitOfWork.TechSpecsICTSpecsRepo.Insert(specs);
            unitOfWork.Save();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void frmAddEditTechSpecsICTSpecs_Load(object sender, EventArgs e)
        {
            await LoadItemNo();
        }

        private void btnAddICTSpecs_Click(object sender, EventArgs e)
        {
            var frm = new frmEquipmentSpecs();
            frm.ShowDialog();
            LoadDropdowns();
        }

        private void btnAddEquipment_Click(object sender, EventArgs e)
        {
            var frm = new frmEquipment();
            frm.ShowDialog();
            LoadDropdowns();
        }
    }
}