using DevExpress.XtraEditors;
using ICTProfilingV3.BaseClasses;
using ICTProfilingV3.DataTransferModels.ViewModels;
using ICTProfilingV3.Interfaces;
using ICTProfilingV3.LookUpTables;
using Microsoft.Extensions.DependencyInjection;
using Models.Entities;
using Models.Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ICTProfilingV3.PPEInventoryForms
{
    public partial class frmAddEditPPEEquipment : BaseForm
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IEquipmentService _equipmentService;
        private readonly IPPEInventoryService _ppeInventoryService;
        private readonly IControlMapper<PPEsSpecs> _ppeSpecsMapper;
        private SaveType _saveType;
        private PPEs _ppe;
        private PPEsSpecs _ppeSpecs;
        public frmAddEditPPEEquipment(IServiceProvider serviceProvider, IControlMapper<PPEsSpecs> ppeSpecsMapper, 
            IEquipmentService equipmentService, IPPEInventoryService ppeInventoryService)
        {
            _serviceProvider = serviceProvider;
            _ppeSpecsMapper = ppeSpecsMapper;
            _ppeInventoryService = ppeInventoryService;
            _equipmentService = equipmentService;
            InitializeComponent();
        }

        public void InitForm(PPEs ppe)
        {
            _ppe = ppe;
            _saveType = SaveType.Insert;
            LoadDropdowns();
        }

        public void InitForm(PPEsSpecs ppesSpecs)
        {
            _ppeSpecs = ppesSpecs;
            _saveType = SaveType.Update;
            LoadDropdowns();
            LoadEquipmentSpecs();
        }
        private void LoadEquipmentSpecs()
        {
            _ppeSpecsMapper.MapControl(_ppeSpecs, this);
            //spinQuantity.Value = _ppeSpecs.Quantity;
            //lueUnit.EditValue = _ppeSpecs.Unit;
            //spinUnitCost.Value = _ppeSpecs.UnitCost;
            //spinTotalCost.Value = _ppeSpecs.TotalCost;
            //spinItemNo.Value = _ppeSpecs.ItemNo;
            //txtSerialNo.Text = _ppeSpecs.SerialNo;

            slueEquipment.EditValue = _ppeSpecs.Model.Brand.EquipmenSpecsId;
            txtDescription.Text = _ppeSpecs.Description;

            var brand = _equipmentService.BrandBaseService.GetAll().Where(x => x.EquipmenSpecsId == _ppeSpecs.Model.Brand.EquipmenSpecsId);
            slueBrand.Properties.DataSource = brand.ToList();

            slueBrand.EditValue = _ppeSpecs.Model.BrandId;

            var model = _equipmentService.ModelBaseService.GetAll().Where(x => x.BrandId == _ppeSpecs.Model.BrandId);
            slueModel.Properties.DataSource = model.ToList();

            slueModel.EditValue = _ppeSpecs.ModelId;
        }

        private void LoadDropdowns()
        {
            var res = _equipmentService.EquipmentSpecsBaseService.GetAll().Select(x => new EquipmentSpecsViewModel
            {
                Description = x.Description,
                Equipment = x.Equipment.EquipmentName,
                Id = x.Id
            });
            slueEquipment.Properties.DataSource = res.ToList();
            lueUnit.Properties.DataSource = Enum.GetValues(typeof(Unit)).Cast<Unit>().Select(x => new
            {
                Unit = x
            });
        }

        private void slueEquipment_EditValueChanged(object sender, EventArgs e)
        {
            var res = (EquipmentSpecsViewModel)slueEquipment.Properties.View.GetFocusedRow();
            if (res == null) return;

            txtDescription.Text = res.Description;

            var brand = _equipmentService.BrandBaseService.GetAll().Where(x => x.EquipmenSpecsId == res.Id);
            slueBrand.Properties.DataSource = brand.ToList();
        }

        private void slueBrand_EditValueChanged(object sender, EventArgs e)
        {
            var res = (Brand)slueBrand.Properties.View.GetFocusedRow();
            if (res == null) return;

            var model = _equipmentService.ModelBaseService.GetAll().Where(x => x.BrandId == res.Id);
            slueModel.Properties.DataSource = model.ToList();
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            if (_saveType == SaveType.Insert) await InsertEquipment();
            else await UpdateEquipment();
        }
        private async Task InsertEquipment()
        {
            //var equipment = new PPEsSpecs
            //{
            //    PPEsId = _ppe.Id,
            //    Quantity = (int)spinQuantity.Value,
            //    UnitCost = (long)spinUnitCost.Value,
            //    TotalCost = (long)spinTotalCost.Value,
            //    SerialNo = txtSerialNo.Text,
            //    ItemNo = (int)spinItemNo.Value,
            //    Description = txtDescription.Text
            //};

            var equipment = new PPEsSpecs { PPEsId = _ppe.Id };  
            _ppeSpecsMapper.MapToEntity(equipment, this);
            equipment.Unit = (Unit)lueUnit.EditValue;
            equipment.Model = slueModel.Properties.View.GetFocusedRow() as Model;

            await _ppeInventoryService.PPESpecsBaseService.AddAsync(equipment);

            var res = (EquipmentSpecsViewModel)slueEquipment.Properties.View.GetFocusedRow();
            var specs = await _equipmentService.EquipmentSpecsBaseService.GetByIdAsync(res.Id);
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
                await _ppeInventoryService.PPESpecsDetailsBaseService.AddAsync(delSpecsDetails);
            }
        }

        private async Task UpdateEquipment()
        {
            var ppeSpecs = await _ppeInventoryService.PPESpecsBaseService.GetByIdAsync(_ppeSpecs.Id);
            _ppeSpecsMapper.MapToEntity(ppeSpecs, this);
            //ppeSpecs.Quantity = (int)spinQuantity.Value;
            //ppeSpecs.Unit = (Unit)lueUnit.EditValue;
            //ppeSpecs.ModelId = (int)slueModel.EditValue;
            //ppeSpecs.UnitCost = (long)spinUnitCost.Value;
            //ppeSpecs.TotalCost = (long)spinTotalCost.Value;
            //ppeSpecs.SerialNo = txtSerialNo.Text;
            //ppeSpecs.ItemNo = (int)spinItemNo.Value;
            //ppeSpecs.Description = txtDescription.Text;

            await _ppeInventoryService.PPESpecsBaseService.SaveChangesAsync();
            this.Close();
        }

        private void btnAddEquipment_Click(object sender, EventArgs e)
        {
            var frm = _serviceProvider.GetRequiredService<frmEquipment>();
            frm.ShowDialog();
            LoadDropdowns();
        }

        private void btnAddICTSpecs_Click(object sender, EventArgs e)
        {
            var frm = _serviceProvider.GetRequiredService<frmEquipmentSpecs>();
            frm.ShowDialog();
            LoadDropdowns();
        }

        private void btnAddBrand_Click(object sender, EventArgs e)
        {
            var frm = _serviceProvider.GetRequiredService<frmEquipmentBrand>();
            frm.ShowDialog();
            LoadDropdowns();
        }

        private void btnAddModel_Click(object sender, EventArgs e)
        {
            var frm = _serviceProvider.GetRequiredService<frmEquipmentModels>();
            frm.ShowDialog();
            LoadDropdowns();
        }
        private void CalcTotalValue()
        {
            spinTotalCost.Value = spinUnitCost.Value * spinQuantity.Value;
        }

        private void spinQty_EditValueChanged(object sender, EventArgs e)
        {
            CalcTotalValue();
        }

        private void spinUnitCost_EditValueChanged(object sender, EventArgs e)
        {
            CalcTotalValue();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}