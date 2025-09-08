using ICTProfilingV3.BaseClasses;
using ICTProfilingV3.DataTransferModels.ViewModels;
using ICTProfilingV3.Interfaces;
using Models.Entities;
using Models.Enums;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ICTProfilingV3.Equipments
{
    public partial class frmAddEditModel : BaseForm
    {
        private readonly IEquipmentService _equipmentService;
        private SaveType _saveType;
        private BrandModelViewModel _model;
        public frmAddEditModel(IEquipmentService equipmentService)
        {
            _equipmentService = equipmentService;
            InitializeComponent();
        }

        public void InitForm(BrandModelViewModel model = null)
        {
            LoadDropdown();
            if (model == null)
            {
                _saveType = SaveType.Insert;
                return;
            }
            else
            {
                _model = model;
                _saveType = SaveType.Update;
                slueEquipment.EditValue = model.EquipmentSpecsId;
                LoadSelectedModel();
            }
        }

        private void LoadDropdown()
        {
            var res = _equipmentService.EquipmentSpecsBaseService.GetAll().Select(x => new EquipmentSpecsViewModel
            {
                Description = x.Description,
                Equipment = x.Equipment.EquipmentName,
                Id= x.Id
            });
            slueEquipment.Properties.DataSource = res.ToList();
        }

        private void slueEquipment_EditValueChanged(object sender, EventArgs e)
        {
            var res = (EquipmentSpecsViewModel)slueEquipment.Properties.View.GetFocusedRow();
            if(res == null) return;

            memoDesc.Text = res.Description;

            var brand = _equipmentService.BrandBaseService.GetAll().Where(x => x.EquipmenSpecsId == res.Id);
            lueBrand.Properties.DataSource = brand.ToList();
        }

        private async Task LoadSelectedModel()
        {
            var brand = _equipmentService.BrandBaseService.GetAll().Where(x => x.EquipmenSpecsId == _model.EquipmentSpecsId);
            lueBrand.Properties.DataSource = brand.ToList();
            var model = await _equipmentService.ModelBaseService.GetByIdAsync(_model.ModelId);

            memoDesc.Text = _model.Description; 
            txtModel.Text = model.ModelName;
            lueBrand.EditValue = _model.BrandId;
        }

        private async void btnSave_ClickAsync(object sender, EventArgs e)
        {
            if (_saveType == SaveType.Insert) await InsertModel();
            else await UpdateModel();
        }

        private async Task UpdateModel()
        {
            int brandId = (int)lueBrand.EditValue;
            var model = await _equipmentService.ModelBaseService.GetByIdAsync(_model.ModelId);
            var brand = await _equipmentService.BrandBaseService.GetByIdAsync(brandId);

            model.ModelName = txtModel.Text;
            model.Brand = brand;
            await _equipmentService.ModelBaseService.SaveChangesAsync();

            this.Close();
        }

        private async Task InsertModel()
        {
            int brandId = (int)lueBrand.EditValue;
            var brand = await _equipmentService.BrandBaseService.GetByIdAsync(brandId);
            var model = new Model
            {
                ModelName = txtModel.Text,
                Brand = brand
            };
            await _equipmentService.ModelBaseService.AddAsync(model);

            this.Close();
        }
    }
}