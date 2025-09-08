using ICTProfilingV3.BaseClasses;
using ICTProfilingV3.DataTransferModels.Models;
using ICTProfilingV3.Interfaces;
using Models.Entities;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace ICTProfilingV3.PPEInventoryForms
{
    public partial class frmInventoryParser : BaseForm
    {
        private readonly IParseInventory _parseInventory;
        private readonly IEquipmentService _equipmentService;

        private List<Specs> _specs;
        private Device _device;
        private string _ppeNo;

        private Model _model;
        public frmInventoryParser(IParseInventory parseInventory, IEquipmentService equipmentService)
        {
            _parseInventory = parseInventory;
            _equipmentService = equipmentService;
            InitializeComponent();
        }

        public void InitForm(Device device, string ppeNo)
        {
            _device = device;
            _specs = _device.Specs;
            _ppeNo = ppeNo;
            lblEquipment.Text = _device.DeviceType;
            lblBrand.Text = _device.Brand;
            lblModel.Text = _device.Model;
        }

        private void LoadData()
        {
            gcEquipmentDetails.DataSource = new BindingList<Specs>(_specs);
            SearchBrand();
        }

        private async Task SearchBrand()
        {
            var equipmentBrand = _equipmentService.BrandBaseService.GetAll().Where(x => x.BrandName == _device.Brand && x.EquipmentSpecs.Equipment.EquipmentName == _device.DeviceType)
                .Include(x => x.EquipmentSpecs)
                .Include(x => x.EquipmentSpecs.Equipment)
                .Include(x => x.EquipmentSpecs.Brands)
                .Include(x => x.EquipmentSpecs.Brands.Select(s => s.Models)).ToList();
            var equipmentSpecsFromBrand = equipmentBrand.Select(s => s.EquipmentSpecs);
            var equipmentSpecs = equipmentSpecsFromBrand.FirstOrDefault(x => x.Equipment.EquipmentName == _device.DeviceType);
            if (equipmentSpecs == null)
            {
                var equipmentNew = new Equipment
                {
                    EquipmentName = _device.DeviceType
                };
                await _equipmentService.AddAsync(equipmentNew);

                var equipmentSpecsNew = new EquipmentSpecs
                {
                    Equipment = equipmentNew
                };
                await _equipmentService.EquipmentSpecsBaseService.AddAsync(equipmentSpecsNew);

                var brandNew = new Brand
                {
                    BrandName = _device.Brand,
                    EquipmentSpecs = equipmentSpecsNew
                };
                await _equipmentService.BrandBaseService.AddAsync(brandNew);

                var modelNew = new Model
                {
                    ModelName = _device.Model,
                    Brand = brandNew
                };
                await _equipmentService.ModelBaseService.AddAsync(modelNew);

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
                await _equipmentService.BrandBaseService.AddAsync(brandNew);

                var modelNew = new Model
                {
                    ModelName = _device.Model,
                    Brand = brandNew
                };
                await _equipmentService.ModelBaseService.AddAsync(modelNew);
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
                await _equipmentService.ModelBaseService.AddAsync(modelNew);
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