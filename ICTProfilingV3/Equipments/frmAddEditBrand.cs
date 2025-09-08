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
    public partial class frmAddEditBrand : BaseForm
    {
        private readonly IEquipmentService _equipmentService;
        private SaveType _saveType;
        private BrandViewModel _brand;
        public frmAddEditBrand(IEquipmentService equipmentService)
        {
            _equipmentService = equipmentService;
            InitializeComponent();
        }

        public void InitForm(BrandViewModel brand = null)
        {
            if(brand == null)
            {
                _saveType = SaveType.Update;
                slueEquipment.EditValue = brand.EquipmentSpecsId;
                memoDesc.Text = brand.Description;
                txtBrand.Text = brand.BrandName;
                _brand = brand;
            }
            else
            {
                _saveType = SaveType.Insert;
            }
            LoadDropdown();
        }

        private void LoadDropdown()
        {
            var equipmentSpecs = _equipmentService.EquipmentSpecsBaseService.GetAll().Select(x => new EquipmentSpecsViewModel
            {
                Id = x.Id,
                Equipment = x.Equipment.EquipmentName,
                Description = x.Description
            });
            slueEquipment.Properties.DataSource = equipmentSpecs.ToList();
        }

        private async void btnAddBrand_Click(object sender, EventArgs e)
        {
            if (_saveType == SaveType.Insert) await InsertBrand();
            else await UpdateBrand();
        }

        private async Task UpdateBrand()
        {
            int equipId = (int)slueEquipment.EditValue;
            var equipSpecs = await _equipmentService.EquipmentSpecsBaseService.GetByIdAsync(equipId);

            var res = await _equipmentService.BrandBaseService.GetByIdAsync(_brand.BrandId);

            res.EquipmentSpecs = equipSpecs;
            res.BrandName = txtBrand.Text;
            await _equipmentService.BrandBaseService.SaveChangesAsync();

            this.Close();
        }

        private async Task InsertBrand()
        {
            int equipId = (int)slueEquipment.EditValue;
            var equipSpecs = await _equipmentService.EquipmentSpecsBaseService.GetByIdAsync(equipId);
            var brand = new Brand
            {
                BrandName = txtBrand.Text,
                EquipmentSpecs = equipSpecs
            };
            await _equipmentService.BrandBaseService.AddAsync(brand);

            this.Close();
        }

        private void slueEquipment_EditValueChanged(object sender, EventArgs e)
        {
            var row = (EquipmentSpecsViewModel)slueEquipment.Properties.View.GetFocusedRow();
            if (row == null) return;
            memoDesc.Text = row.Description;
        }
    }
}