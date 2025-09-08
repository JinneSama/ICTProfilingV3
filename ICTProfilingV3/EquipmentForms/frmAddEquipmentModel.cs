using ICTProfilingV3.BaseClasses;
using ICTProfilingV3.Interfaces;
using Models.Entities;
using Models.Enums;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ICTProfilingV3.EquipmentForms
{
    public partial class frmAddEquipmentModel : BaseForm
    {
        public readonly IEquipmentService _equipmentService;
        private EquipmentCategoryBrand _equipmentCategoryBrand;
        private Model _model;
        private SaveType _saveType;
        public frmAddEquipmentModel(IEquipmentService equipmentService)
        {
            _equipmentService = equipmentService;
            InitializeComponent();
        }

        public void InitForm(EquipmentCategoryBrand equipmentCategoryBrand, Model model = null)
        {
            if (model == null)
            {
                _saveType = SaveType.Insert;
                labelControl1.Text = "Add Model";
            }
            else
            {
                _saveType = SaveType.Update;
                labelControl1.Text = "Edit Model";
                txtModel.Text = model.ModelName;
                memoDescription.Text = model.Description;
            }
            _model = model;
            _equipmentCategoryBrand = equipmentCategoryBrand;
            LoadData();
        }

        private void LoadData()
        {
            txtBrand.Text = _equipmentCategoryBrand.EquipmentBrand.Name;
            txtEquipment.Text = _equipmentCategoryBrand.EquipmentCategory.Name;
        }
        
        private async void btnSave_Click(object sender, EventArgs e)
        {
            if (_saveType == SaveType.Insert)
                await InsertBrand();
            else 
                await UpdateBrand();

            this.Close();
        }

        private async Task UpdateBrand()
        {
            var brands = await _equipmentService.EquipmentCategoryBrandBaseService.GetByIdAsync(_equipmentCategoryBrand.Id);
            var exists = brands.Models.Select(x => x.ModelName.ToLower()).Contains(txtModel.Text.ToLower());
            if (exists)
            {
                MessageBox.Show("The selected Model already exists for this Equipment.", "Duplicate Model", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var model = await _equipmentService.ModelBaseService.GetByIdAsync(_model.Id);
            model.ModelName = txtModel.Text;
            await _equipmentService.ModelBaseService.SaveChangesAsync();
        }

        private async Task InsertBrand()
        {
            var brands = await _equipmentService.EquipmentCategoryBrandBaseService.GetByIdAsync(_equipmentCategoryBrand.Id);
            var exists = brands.Models.Select(x => x.ModelName.ToLower()).Contains(txtModel.Text.ToLower());
            if (exists)
            {
                MessageBox.Show("The selected Model already exists for this Equipment.", "Duplicate Model", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var model = new Model
            {
                EquipmentCategoryBrandId = _equipmentCategoryBrand.Id,
                ModelName = txtModel.Text,
                BrandId = 1
            };
            brands.Models.Add(model);
            await _equipmentService.EquipmentCategoryBrandBaseService.SaveChangesAsync();
            this.Close();
        }
    }
}