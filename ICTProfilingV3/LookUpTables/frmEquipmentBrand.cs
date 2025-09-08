using ICTProfilingV3.BaseClasses;
using ICTProfilingV3.DataTransferModels.ViewModels;
using ICTProfilingV3.Equipments;
using ICTProfilingV3.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace ICTProfilingV3.LookUpTables
{
    public partial class frmEquipmentBrand : BaseForm
    {
        private readonly IEquipmentService _equipmentService;
        private readonly IServiceProvider _serviceProvider;
        public frmEquipmentBrand(IServiceProvider serviceProvider, IEquipmentService equipmentService)
        {
            _serviceProvider = serviceProvider;
            _equipmentService = equipmentService;
            InitializeComponent();
            LoadDropdowns();
            LoadBrand();
        }

        private void LoadDropdowns()
        {
            var dropdown = _equipmentService.EquEquipmentCategoryBaseService.GetAll();
            lueEquipmentCategory.DataSource = new BindingList<Models.Entities.EquipmentCategory>(dropdown.ToList());
        }

        private void LoadBrand()
        {
            var res = _equipmentService.BrandBaseService.GetAll()
                .Select(x => new BrandViewModel
            {
                BrandId = x.Id,
                EquipmentCategoryId = x.EquipmentSpecs.Equipment.EquipmentCategoryId,
                EquipmentCategory = x.EquipmentSpecs.Equipment.EquipmentCategory.Name,
                BrandName = x.BrandName,
                Equipment = x.EquipmentSpecs.Equipment.EquipmentName,
                Description = x.EquipmentSpecs.Description,
                EquipmentSpecsId = x.EquipmenSpecsId
            });
            gcBrand.DataSource = new BindingList<BrandViewModel>(res.ToList());
        }

        private void btnAddBrand_Click(object sender, EventArgs e)
        {
            var frm = _serviceProvider.GetRequiredService<frmAddEditBrand>();
            frm.ShowDialog();

            LoadBrand();
        }

        private async void btnDeleteEquip_Click(object sender, EventArgs e)
        {
            var msgRes = MessageBox.Show("Delete Brand?", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
            if (msgRes == DialogResult.Cancel) return;

            var brand = (BrandViewModel)gridBrand.GetFocusedRow();
            var res = await _equipmentService.BrandBaseService.GetByIdAsync(brand.BrandId);
            if (res == null) return;
            await _equipmentService.BrandBaseService.DeleteAsync(res.Id);

            LoadBrand();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            var row = (BrandViewModel)gridBrand.GetFocusedRow();
            var frm = _serviceProvider.GetRequiredService<frmAddEditBrand>();
            frm.InitForm(row);
            frm.ShowDialog();

            LoadBrand();
        }
    }
}