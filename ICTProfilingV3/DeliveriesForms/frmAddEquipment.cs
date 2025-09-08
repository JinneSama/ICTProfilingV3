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

namespace ICTProfilingV3.DeliveriesForms
{
    public partial class frmAddEquipment : BaseForm
    {
        private SaveType _saveType;
        private Deliveries _deliveries;
        private DeliveriesSpecs _deliveriesSpecs;
        private readonly IServiceProvider _serviceProvider;
        private readonly IEquipmentService _equipmentService;
        private readonly IDeliveriesService _deliveriesService;
        public frmAddEquipment(IEquipmentService equipmentService, IServiceProvider serviceProvider,
            IDeliveriesService deliveriesService) 
        {
            _serviceProvider = serviceProvider;
            _equipmentService = equipmentService;
            _deliveriesService = deliveriesService;
            InitializeComponent();
        }

        public void InitForm(Deliveries deliveries)
        {
            _saveType = SaveType.Insert;
            _deliveries = deliveries;
            LoadDropdowns();
            LoadItemNo();
        }

        public void InitForm(DeliveriesSpecs deliveriesSpecs)
        {
            _saveType = SaveType.Update;
            _deliveriesSpecs = deliveriesSpecs;
            LoadDropdowns();
            LoadEquipmentSpecs();
        }

        private void LoadItemNo()
        {
            var itemNos = _deliveries.DeliveriesSpecs.OrderBy(x => x.ItemNo).LastOrDefault();
            var newItemNo = itemNos == null ? 0:  itemNos.ItemNo + 1;

            seItemNo.Value = (decimal)newItemNo;
        }

        private void LoadEquipmentSpecs()
        {
            spinQty.Value = (decimal)_deliveriesSpecs.Quantity;
            cboUnit.EditValue = _deliveriesSpecs.Unit;
            spinUnitCost.Value = (decimal)_deliveriesSpecs.UnitCost;
            spintTotal.Value = (decimal)_deliveriesSpecs.TotalCost;
            seItemNo.Value = (decimal)_deliveriesSpecs.ItemNo;
            txtSerialNo.Text = _deliveriesSpecs.SerialNo;

            slueEquipment.EditValue = _deliveriesSpecs?.Model?.Brand?.EquipmenSpecsId;
            txtDescription.Text = _deliveriesSpecs.Description;
            if(!(_deliveriesSpecs?.Model?.Brand?.EquipmenSpecsId == null))
            {
                var brand = _equipmentService.BrandBaseService.GetAll().Where(x => x.EquipmenSpecsId == _deliveriesSpecs.Model.Brand.EquipmenSpecsId);
                slueBrand.Properties.DataSource = brand.ToList();
                slueBrand.EditValue = _deliveriesSpecs?.Model?.BrandId;
            }

            if (!(_deliveriesSpecs?.Model?.BrandId == null))
            {
                var model = _equipmentService.ModelBaseService.GetAll().Where(x => x.BrandId == _deliveriesSpecs.Model.BrandId);
                slueModel.Properties.DataSource = model.ToList();

                slueModel.EditValue = _deliveriesSpecs?.ModelId;
            }
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
            cboUnit.Properties.DataSource = Enum.GetValues(typeof(Unit)).Cast<Unit>().Select(x => new
            {
                Unit = x
            });
        }

        private void slueEquipment_EditValueChanged(object sender, EventArgs e)
        {
            var res = (EquipmentSpecsViewModel)slueEquipment.Properties.View.GetFocusedRow();
            if (res == null) return;

            txtDescription.Text = res.Description;

            var brand = _equipmentService.BrandBaseService.GetAll().Where(x => x.EquipmenSpecsId == res.Id).ToList();
            slueBrand.Properties.DataSource = brand;
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
            var equipment = new DeliveriesSpecs
            {
                DeliveriesId = _deliveries.Id,
                Quantity = (int)spinQty.Value,
                Unit = (Unit)cboUnit.EditValue,
                ModelId = (int?)slueModel.EditValue,
                UnitCost = (long)spinUnitCost.Value,
                TotalCost = (long)spintTotal.Value,
                SerialNo = txtSerialNo.Text,
                ItemNo = (int)seItemNo.Value,
                Description = txtDescription.Text
            };
            await _deliveriesService.DeliveriesSpecsBaseService.AddAsync(equipment);

            var res = (EquipmentSpecsViewModel)slueEquipment.Properties.View.GetFocusedRow();
            var specs = await _equipmentService.EquipmentSpecsBaseService.GetByIdAsync(res.Id);
            if (specs.EquipmentSpecsDetails.Count > 0)
            {
                if (MessageBox.Show("There are Existing Specs for this Equipment, Copy the Specs?",
                    "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) == DialogResult.OK) 
                    await InsertSpecs(specs.EquipmentSpecsDetails, equipment);
            }
            this.Close();
        }

        private async Task InsertSpecs(IEnumerable<EquipmentSpecsDetails> specs , DeliveriesSpecs deliveriesSpecs)
        {
            foreach (var spec in specs) {
                var delSpecsDetails = new DeliveriesSpecsDetails()
                {
                    ItemNo = spec.ItemNo,
                    Specs = spec.DetailSpecs,
                    Description = spec.DetailDescription,
                    DeliveriesSpecs = deliveriesSpecs
                };
                await _deliveriesService.DeliveriesSpecsDetailsBaseService.AddAsync(delSpecsDetails);
            }
        }

        private async Task UpdateEquipment()
        {
            var delSpecs = await _deliveriesService.DeliveriesSpecsBaseService.GetByIdAsync(_deliveriesSpecs.Id);
            delSpecs.Quantity = (int)spinQty.Value;
            delSpecs.Unit = (Unit)cboUnit.EditValue;
            delSpecs.ModelId = (int)slueModel.EditValue;
            delSpecs.UnitCost = (long)spinUnitCost.Value;
            delSpecs.TotalCost = (long)spintTotal.Value;
            delSpecs.SerialNo = txtSerialNo.Text;
            delSpecs.ItemNo = (int)seItemNo.Value;
            delSpecs.Description = txtDescription.Text;

            await _deliveriesService.DeliveriesSpecsBaseService.SaveChangesAsync();
            this.Close();
        }

        private void RefreshBrand()
        {
            var res = (EquipmentSpecsViewModel)slueEquipment.Properties.View.GetFocusedRow();

            if (res == null) return;    

            var brand = _equipmentService.BrandBaseService.GetAll().Where(x => x.EquipmenSpecsId == res.Id).ToList();
            slueBrand.Properties.DataSource = brand;
        }

        private void RefreshModel()
        {
            var res = (Brand)slueBrand.Properties.View.GetFocusedRow();
            if (res == null) return;

            var model = _equipmentService.ModelBaseService.GetAll().Where(x => x.BrandId == res.Id);
            slueModel.Properties.DataSource = model.ToList();
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
            RefreshBrand();
        }

        private void btnAddModel_Click(object sender, EventArgs e)
        {
            var frm = _serviceProvider.GetRequiredService<frmEquipmentModels>();
            frm.ShowDialog();
            RefreshModel();
        }

        private void spinUnitCost_EditValueChanged(object sender, EventArgs e)
        {
            CalcTotalValue();
        }

        private void spinQty_EditValueChanged(object sender, EventArgs e)
        {
            CalcTotalValue();
        }

        private void CalcTotalValue()
        {
            spintTotal.Value = spinUnitCost.Value * spinQty.Value;
        }
    }
}