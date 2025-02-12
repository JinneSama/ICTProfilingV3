﻿using DevExpress.XtraEditors;
using ICTProfilingV3.BaseClasses;
using ICTProfilingV3.LookUpTables;
using Models.Entities;
using Models.Enums;
using Models.Repository;
using Models.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ICTProfilingV3.DeliveriesForms
{
    public partial class frmAddEquipment : BaseForm
    {
        private IUnitOfWork unitOfWork;
        private SaveType saveType;
        private readonly Deliveries _deliveries;
        private DeliveriesSpecs _deliveriesSpecs;

        public frmAddEquipment(Deliveries deliveries) 
        {
            InitializeComponent();
            saveType = SaveType.Insert;
            unitOfWork = new UnitOfWork();
            _deliveries = deliveries;
            LoadDropdowns();
            LoadItemNo();
        }
        public frmAddEquipment(DeliveriesSpecs deliveriesSpecs)
        {
            InitializeComponent();
            _deliveriesSpecs = deliveriesSpecs;
            saveType = SaveType.Update;
            unitOfWork = new UnitOfWork();
            LoadDropdowns();
            LoadEquipmentSpecs();
        }

        private void LoadItemNo()
        {
            var itemNos = _deliveries.DeliveriesSpecs.OrderBy(x => x.ItemNo).LastOrDefault();
            var newItemNo = itemNos == null ? 0:  itemNos.ItemNo + 1;

            spinItemNo.Value = (decimal)newItemNo;
        }

        private void LoadEquipmentSpecs()
        {
            spinQty.Value = (decimal)_deliveriesSpecs.Quantity;
            cboUnit.EditValue = _deliveriesSpecs.Unit;
            spinUnitCost.Value = (decimal)_deliveriesSpecs.UnitCost;
            spintTotal.Value = (decimal)_deliveriesSpecs.TotalCost;
            spinItemNo.Value = (decimal)_deliveriesSpecs.ItemNo;
            txtSerialNo.Text = _deliveriesSpecs.SerialNo;

            slueEquipment.EditValue = _deliveriesSpecs.Model.Brand.EquipmenSpecsId;
            txtDescription.Text = _deliveriesSpecs.Description;

            var brand = unitOfWork.BrandRepo.FindAllAsync(x => x.EquipmenSpecsId == _deliveriesSpecs.Model.Brand.EquipmenSpecsId);
            slueBrand.Properties.DataSource = brand.ToList();

            slueBrand.EditValue = _deliveriesSpecs.Model.BrandId;

            var model = unitOfWork.ModelRepo.FindAllAsync(x => x.BrandId == _deliveriesSpecs.Model.BrandId);
            slueModel.Properties.DataSource = model.ToList();

            slueModel.EditValue = _deliveriesSpecs.ModelId;
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
        }

        private void slueEquipment_EditValueChanged(object sender, EventArgs e)
        {
            var res = (EquipmentSpecsViewModel)slueEquipment.Properties.View.GetFocusedRow();
            if (res == null) return;

            txtDescription.Text = res.Description;

            var brand = unitOfWork.BrandRepo.FindAllAsync(x => x.EquipmenSpecsId == res.Id).ToList();
            slueBrand.Properties.DataSource = brand;
        }

        private void slueBrand_EditValueChanged(object sender, EventArgs e)
        {
            var res = (Brand)slueBrand.Properties.View.GetFocusedRow();
            if (res == null) return;

            var model = unitOfWork.ModelRepo.FindAllAsync(x => x.BrandId == res.Id);
            slueModel.Properties.DataSource = model.ToList();
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            if (saveType == SaveType.Insert) await InsertEquipment();
            else await UpdateEquipment();
        }
        private async Task InsertEquipment()
        {
            var del = await unitOfWork.DeliveriesRepo.FindAsync(x => x.Id == _deliveries.Id);
            var equipment = new DeliveriesSpecs
            {
                Deliveries = del,
                Quantity = (int)spinQty.Value,
                Unit = (Unit)cboUnit.EditValue,
                ModelId = (int?)slueModel.EditValue,
                UnitCost = (long)spinUnitCost.Value,
                TotalCost = (long)spintTotal.Value,
                SerialNo = txtSerialNo.Text,
                ItemNo = (int)spinItemNo.Value,
                Description = txtDescription.Text
            };
            unitOfWork.DeliveriesSpecsRepo.Insert(equipment);
            await unitOfWork.SaveChangesAsync();

            var res = (EquipmentSpecsViewModel)slueEquipment.Properties.View.GetFocusedRow();
            var specs = await unitOfWork.EquipmentSpecsRepo.FindAsync(x => x.Id == res.Id);
            if (specs.EquipmentSpecsDetails.Count > 0)
            {
                if (MessageBox.Show("There are Existing Specs for this Equipment, Copy the Specs?",
                    "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) == DialogResult.OK) InsertSpecs(specs.EquipmentSpecsDetails , equipment);
            }
            this.Close();
        }

        private void InsertSpecs(IEnumerable<EquipmentSpecsDetails> specs , DeliveriesSpecs deliveriesSpecs)
        {
            foreach (var spec in specs) {
                var delSpecsDetails = new DeliveriesSpecsDetails()
                {
                    ItemNo = spec.ItemNo,
                    Specs = spec.DetailSpecs,
                    Description = spec.DetailDescription,
                    DeliveriesSpecs = deliveriesSpecs
                };
                unitOfWork.DeliveriesSpecsDetailsRepo.Insert(delSpecsDetails);
            }
            unitOfWork.Save();
        }

        private async Task UpdateEquipment()
        {
            var delSpecs = await unitOfWork.DeliveriesSpecsRepo.FindAsync(x => x.Id == _deliveriesSpecs.Id);
            delSpecs.Quantity = (int)spinQty.Value;
            delSpecs.Unit = (Unit)cboUnit.EditValue;
            delSpecs.ModelId = (int)slueModel.EditValue;
            delSpecs.UnitCost = (long)spinUnitCost.Value;
            delSpecs.TotalCost = (long)spintTotal.Value;
            delSpecs.SerialNo = txtSerialNo.Text;
            delSpecs.ItemNo = (int)spinItemNo.Value;
            delSpecs.Description = txtDescription.Text;

            await unitOfWork.SaveChangesAsync();
            this.Close();
        }

        private void RefreshBrand()
        {
            var res = (EquipmentSpecsViewModel)slueEquipment.Properties.View.GetFocusedRow();

            if (res == null) return;    

            var brand = unitOfWork.BrandRepo.FindAllAsync(x => x.EquipmenSpecsId == res.Id).ToList();
            slueBrand.Properties.DataSource = brand;
        }

        private void RefreshModel()
        {
            var res = (Brand)slueBrand.Properties.View.GetFocusedRow();
            if (res == null) return;

            var model = unitOfWork.ModelRepo.FindAllAsync(x => x.BrandId == res.Id);
            slueModel.Properties.DataSource = model.ToList();
        }

        private void btnAddEquipment_Click(object sender, EventArgs e)
        {
            var frm = new frmEquipment();
            frm.ShowDialog();
            LoadDropdowns();
        }

        private void btnAddICTSpecs_Click(object sender, EventArgs e)
        {
            var frm = new frmEquipmentSpecs();
            frm.ShowDialog();
            LoadDropdowns();
        }

        private void btnAddBrand_Click(object sender, EventArgs e)
        {
            var frm = new frmEquipmentBrand();
            frm.ShowDialog();
            RefreshBrand();
        }

        private void btnAddModel_Click(object sender, EventArgs e)
        {
            var frm = new frmEquipmentModels();
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