using DevExpress.XtraEditors;
using ICTProfilingV3.BaseClasses;
using Models.Entities;
using Models.Enums;
using Models.Repository;
using Models.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ICTProfilingV3.Equipments
{
    public partial class frmAddEditModel : BaseForm
    {
        private IUnitOfWork unitOfWork;
        private SaveType saveType;
        private BrandModelViewModel _model;
        public frmAddEditModel()
        {
            InitializeComponent();
            unitOfWork = new UnitOfWork();
            saveType = SaveType.Insert;
            LoadDropdown();
        }
        public frmAddEditModel(BrandModelViewModel model)
        {
            InitializeComponent();
            _model = model;
            unitOfWork = new UnitOfWork();
            saveType = SaveType.Update;
            LoadDropdown();

            slueEquipment.EditValue = model.EquipmentSpecsId;
            LoadSelectedModel();
        }

        private void LoadDropdown()
        {
            var res = unitOfWork.EquipmentSpecsRepo.GetAll().Select(x => new EquipmentSpecsViewModel
            {
                Description = x.Description,
                Equipment = x.Equipment.EquipmentName,
                Id= x.Id
            });
            slueEquipment.Properties.DataSource = res.ToList();
        }

        private async void slueEquipment_EditValueChanged(object sender, EventArgs e)
        {
            var res = (EquipmentSpecsViewModel)slueEquipment.Properties.View.GetFocusedRow();
            if(res == null) return;

            memoDesc.Text = res.Description;

            var brand = unitOfWork.BrandRepo.FindAllAsync(x => x.EquipmenSpecsId == res.Id);
            lueBrand.Properties.DataSource = brand.ToList();

            var res2 = await unitOfWork.ModelRepo.FindAsync(x => x.Id == 1);
        }

        private async void LoadSelectedModel()
        {
            var brand = unitOfWork.BrandRepo.FindAllAsync(x => x.EquipmenSpecsId == _model.EquipmentSpecsId);
            lueBrand.Properties.DataSource = await brand.ToListAsync();
            var model = (Model)await unitOfWork.ModelRepo.FindAsync(x => x.Id == _model.ModelId);

            memoDesc.Text = _model.Description; 
            txtModel.Text = model.ModelName;
            lueBrand.EditValue = _model.BrandId;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (saveType == SaveType.Insert) InsertModel();
            else UpdateModel();
        }

        private async void UpdateModel()
        {
            int brandId = (int)lueBrand.EditValue;
            var model = await unitOfWork.ModelRepo.FindAsync(x => x.Id == _model.ModelId);
            var brand = (Brand)(await unitOfWork.BrandRepo.FindAsync(x => x.Id == brandId));

            model.ModelName = txtModel.Text;
            model.Brand = brand;
            unitOfWork.ModelRepo.Update(model);
            unitOfWork.Save();

            this.Close();
        }

        private async void InsertModel()
        {
            int brandId = (int)lueBrand.EditValue;
            var brand = (Brand)(await unitOfWork.BrandRepo.FindAsync(x => x.Id == brandId));
            var model = new Model
            {
                ModelName = txtModel.Text,
                Brand = brand
            };
            unitOfWork.ModelRepo.Insert(model);
            unitOfWork.Save();

            this.Close();
        }
    }
}