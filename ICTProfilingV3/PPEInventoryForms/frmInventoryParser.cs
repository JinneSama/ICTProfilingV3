using Helpers.Interfaces;
using Helpers.Inventory;
using Models.Entities;
using Models.Repository;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace ICTProfilingV3.PPEInventoryForms
{
    public partial class frmInventoryParser : DevExpress.XtraEditors.XtraForm
    {
        private Device _device;
        private string _ppeNo;
        private readonly IParseInventory _parseInventory;
        private List<Specs> _specs;

        private EquipmentSpecs _equipmentSpecs;
        private Model _model;
        private Brand _brand;
        public frmInventoryParser(Device device, string ppeNo)
        {
            InitializeComponent();
            _device = device;
            _parseInventory = new ParseInventory();
            _specs = _device.Specs;
            _ppeNo = ppeNo;

            lblEquipment.Text = _device.DeviceType;
            lblBrand.Text = _device.Brand;
            lblModel.Text = _device.Model;
        }

        private void LoadData()
        {
            IUnitOfWork unitOfWork = new UnitOfWork();
            gcEquipmentDetails.DataSource = new BindingList<Specs>(_specs);
            SearchBrand();
        }

        private void SearchBrand()
        {
            IUnitOfWork unitOfWork = new UnitOfWork();
            var equipmentBrand = unitOfWork.BrandRepo.FindAllAsync(x => x.BrandName == _device.Brand && x.EquipmentSpecs.Equipment.EquipmentName == _device.DeviceType,
                x => x.EquipmentSpecs,
                x => x.EquipmentSpecs.Equipment,
                x => x.EquipmentSpecs.Brands,
                x => x.EquipmentSpecs.Brands.Select(s => s.Models)).ToList();
            var equipmentSpecsFromBrand = equipmentBrand.Select(s => s.EquipmentSpecs);
            var equipmentSpecs = equipmentSpecsFromBrand.FirstOrDefault(x => x.Equipment.EquipmentName == _device.DeviceType);
            if (equipmentSpecs == null)
            {
                var equipmentNew = new Equipment
                {
                    EquipmentName = _device.DeviceType
                };
                unitOfWork.EquipmentRepo.Insert(equipmentNew);

                var equipmentSpecsNew = new EquipmentSpecs
                {
                    Equipment = equipmentNew
                };
                unitOfWork.EquipmentSpecsRepo.Insert(equipmentSpecsNew);

                var brandNew = new Brand
                {
                    BrandName = _device.Brand,
                    EquipmentSpecs = equipmentSpecsNew
                };
                unitOfWork.BrandRepo.Insert(brandNew);

                var modelNew = new Model
                {
                    ModelName = _device.Model,
                    Brand = brandNew
                };
                unitOfWork.ModelRepo.Insert(modelNew);
                unitOfWork.Save();

                _model = modelNew;
                return;
            }

            var brand = equipmentSpecs.Brands.FirstOrDefault(x => x.BrandName == _device.Brand);
            if (brand == null)
            {
                var brandNew = new Brand
                {
                    BrandName = _device.Brand,
                    EquipmentSpecs = equipmentSpecs
                };
                unitOfWork.BrandRepo.Insert(brandNew);

                var modelNew = new Model
                {
                    ModelName = _device.Model,
                    Brand = brandNew
                };
                unitOfWork.ModelRepo.Insert(modelNew);
                unitOfWork.Save();
                _model = modelNew;
                return;
            }

            var model = brand.Models.FirstOrDefault(x => x.ModelName == _device.Model);
            if (model == null)
            {
                var modelNew = new Model
                {
                    ModelName = _device.Model,
                    Brand = brand
                };
                unitOfWork.ModelRepo.Insert(modelNew);
                unitOfWork.Save();
                _model = modelNew;
                return;
            }
            _model = model;
        }

        private async void btnSave_Click(object sender, System.EventArgs e)
        {
            List<Specs> specs = new List<Specs>();
            for (int i = 0; i <= gridEquipmentDetails.RowCount; i++)
            {
                var row = (Specs)gridEquipmentDetails.GetRow(i);
                if (row == null) continue;
                specs.Add(row);
            }

            _device.Specs = specs;
            await _parseInventory.GeneratePPESpecs(_device, _ppeNo,_model);
            this.Close();
        }

        private void btnDelete_Click(object sender, System.EventArgs e)
        {
            gridEquipmentDetails.DeleteRow(gridEquipmentDetails.FocusedRowHandle);
        }

        private void frmInventoryParser_Load(object sender, System.EventArgs e)
        {
            LoadData();
        }
    }
}