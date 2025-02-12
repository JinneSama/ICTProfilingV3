using Models.Repository;
using System.Linq;
using System;
using Models.ViewModels;
using Models.Enums;
using Models.Entities;
using System.Threading.Tasks;
using ICTProfilingV3.BaseClasses;

namespace ICTProfilingV3.StandardPRForms
{
    public partial class frmAddEditStandardPRSpecs : BaseForm
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly SaveType saveType;
        private readonly StandardPRSpecs specs;
        public frmAddEditStandardPRSpecs()
        {
            InitializeComponent();
            saveType = SaveType.Insert;
            unitOfWork = new UnitOfWork();
            LoadDropdowns();
        }

        public frmAddEditStandardPRSpecs(StandardPRSpecs _specs)
        {
            InitializeComponent();
            saveType = SaveType.Update;
            specs = _specs;
            unitOfWork = new UnitOfWork();
            LoadDropdowns();
        }

        private void LoadDetails()
        {
            spinItemNo.Value = specs.ItemNo;
            lueQuarter.EditValue = specs.Quarter;
            slueEquipment.EditValue = specs.EquipmentSpecsId;
            txtDescription.Text = specs.Description;
            cboUnit.EditValue = specs.Unit;
            spinUnitCost.EditValue = specs.UnitCost;    
            txtPurpose.Text = specs.Purpose;
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

            lueQuarter.Properties.DataSource = Enum.GetValues(typeof(PRQuarter)).Cast<PRQuarter>().Select(x => new
            {
                Id = x,
                QuarterName = EnumHelper.GetEnumDescription(x)
            });
        }

        private void slueEquipment_EditValueChanged(object sender, EventArgs e)
        {
            var res = (EquipmentSpecsViewModel)slueEquipment.Properties.View.GetFocusedRow();
            if (res == null) return;

            txtDescription.Text = res.Description;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            if (saveType == SaveType.Insert) await InsertSpecs();
            else await UpdateSpecs();
            this.Close();
        }

        private async Task InsertSpecs()
        {
            var sprSpecs = new StandardPRSpecs
            {
                ItemNo = (int)spinItemNo.Value,
                Quarter = (PRQuarter)lueQuarter.EditValue,
                Description = txtDescription.Text,
                Purpose = txtPurpose.Text,
                Unit = (Unit)cboUnit.EditValue,
                UnitCost = (long?)spinUnitCost.Value,
                EquipmentSpecsId = (int)slueEquipment.EditValue
            };
            unitOfWork.StandardPRSpecsRepo.Insert(sprSpecs);
            await unitOfWork.SaveChangesAsync();
        }

        private async Task UpdateSpecs()
        {
            var sprSpecs = await unitOfWork.StandardPRSpecsRepo.FindAsync(x => x.Id == specs.Id);
            if (sprSpecs == null) return;

            sprSpecs.ItemNo = (int)spinItemNo.Value;
            sprSpecs.Quarter = (PRQuarter)lueQuarter.EditValue;
            sprSpecs.Description = txtDescription.Text;
            sprSpecs.Purpose = txtPurpose.Text;
            sprSpecs.Unit = (Unit)cboUnit.EditValue;
            sprSpecs.UnitCost = (long?)spinUnitCost.Value;
            sprSpecs.EquipmentSpecsId = (int)slueEquipment.EditValue;

            await unitOfWork.SaveChangesAsync();
        }

        private void frmAddEditStandardPRSpecs_Load(object sender, EventArgs e)
        {
            if(saveType == SaveType.Update) LoadDetails();
        }
    }
}