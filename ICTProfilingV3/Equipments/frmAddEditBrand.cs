using ICTProfilingV3.BaseClasses;
using Models.Entities;
using Models.Enums;
using Models.Repository;
using Models.ViewModels;
using System;
using System.Data;
using System.Linq;

namespace ICTProfilingV3.Equipments
{
    public partial class frmAddEditBrand : BaseForm
    {
        private SaveType saveType;
        private IUnitOfWork unitOfWork;
        private BrandViewModel _brand;
        public frmAddEditBrand()
        {
            InitializeComponent();
            saveType = SaveType.Insert;
            unitOfWork = new UnitOfWork();
            LoadDropdown();
        }
        public frmAddEditBrand(BrandViewModel brand)
        {
            InitializeComponent();
            saveType = SaveType.Update;
            unitOfWork = new UnitOfWork();
            LoadDropdown();

            slueEquipment.EditValue = brand.EquipmentSpecsId;
            memoDesc.Text = brand.Description;
            txtBrand.Text = brand.BrandName;
            _brand = brand;
        }

        private void LoadDropdown()
        {
            var equipmentSpecs = unitOfWork.EquipmentSpecsRepo.GetAll().Select(x => new EquipmentSpecsViewModel
            {
                Id = x.Id,
                Equipment = x.Equipment.EquipmentName,
                Description = x.Description
            });
            slueEquipment.Properties.DataSource = equipmentSpecs.ToList();
        }

        private void btnAddBrand_Click(object sender, EventArgs e)
        {
            if (saveType == SaveType.Insert) InsertBrand();
            else UpdateBrand();
        }

        private async void UpdateBrand()
        {
            int equipId = (int)slueEquipment.EditValue;
            var equipSpecs = await unitOfWork.EquipmentSpecsRepo.FindAsync(x => x.Id == equipId);

            var res = await unitOfWork.BrandRepo.FindAsync(x => x.Id == _brand.BrandId);

            res.EquipmentSpecs = equipSpecs;
            res.BrandName = txtBrand.Text;
            unitOfWork.Save();

            this.Close();
        }

        private async void InsertBrand()
        {
            int equipId = (int)slueEquipment.EditValue;
            var equipSpecs = await unitOfWork.EquipmentSpecsRepo.FindAsync(x => x.Id == equipId);
            var brand = new Brand
            {
                BrandName = txtBrand.Text,
                EquipmentSpecs = equipSpecs
            };
            unitOfWork.BrandRepo.Insert(brand);
            unitOfWork.Save();

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