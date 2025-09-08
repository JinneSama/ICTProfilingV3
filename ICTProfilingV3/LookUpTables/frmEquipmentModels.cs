using DevExpress.XtraEditors;
using ICTProfilingV3.BaseClasses;
using ICTProfilingV3.DataTransferModels.ViewModels;
using ICTProfilingV3.Equipments;
using ICTProfilingV3.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Models.Entities;
using System;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ICTProfilingV3.LookUpTables
{
    public partial class frmEquipmentModels : BaseForm
    {
        private readonly IEquipmentService _equipmentService;
        private readonly IServiceProvider _serviceProvider;
        public frmEquipmentModels(IEquipmentService equipmentService, IServiceProvider serviceProvider)
        {
            _equipmentService = equipmentService;
            _serviceProvider = serviceProvider;
            InitializeComponent();
            LoadDropdowns();
            LoadModels();
        }

        private void LoadDropdowns()
        {
            var equipments = _equipmentService.EquipmentCategoryBrandBaseService.GetAll()
                .Include(x => x.EquipmentBrand)
                .Include(x => x.EquipmentCategory)
                .OrderBy(o => o.EquipmentCategory.Name)
                .ToList();

            slueEquipmentBrand.DataSource = new BindingList<EquipmentCategoryBrand>(equipments);
        }
        private void LoadModels()
        {
            var data = _equipmentService.ModelBaseService.GetAll().Select(x => new BrandModelViewModel
            {
                EquipmentCategoryBrandId = x.EquipmentCategoryBrandId,
                EquipmentBrandId = x.EquipmentCategoryBrand.EquipmentBrandId,
                Equipment = x.Brand.EquipmentSpecs.Equipment.EquipmentName,
                Description = x.Brand.EquipmentSpecs.Description,
                Brand = x.Brand.BrandName,
                Model = x.ModelName,
                EquipmentSpecsId = x.Brand.EquipmenSpecsId,
                BrandId = x.Brand.Id,
                ModelId = x.Id
            });
            gcModel.DataSource = new BindingList<BrandModelViewModel>(data.ToList());
        }

        private async void btnDeleteEquip_Click(object sender, System.EventArgs e)
        {
            var msgRes = MessageBox.Show("Delete Model?", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
            if (msgRes == DialogResult.Cancel) return;

            var model = (BrandModelViewModel)gridModel.GetFocusedRow();
            var res = await _equipmentService.ModelBaseService.GetByIdAsync(model.ModelId);
            if (res == null) return;
            await _equipmentService.ModelBaseService.DeleteAsync(res.Id);

            LoadModels();
        }

        private void btnEdit_Click(object sender, System.EventArgs e)
        {
            var res = (BrandModelViewModel)gridModel.GetFocusedRow();
            var frm = _serviceProvider.GetRequiredService<frmAddEditModel>();
            frm.InitForm(res);
            frm.ShowDialog();

            LoadModels();
        }

        private void btnAddModel_Click(object sender, System.EventArgs e)
        {
            var frm = _serviceProvider.GetRequiredService<frmAddEditBrand>(); ;
            frm.ShowDialog();

            LoadModels();
        }

        private async void gridModel_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            var row = (BrandModelViewModel)gridModel.GetFocusedRow();
            if (row == null) return;
            var res = await _equipmentService.ModelBaseService.GetByIdAsync(row.ModelId);
            if (res == null) await InsertEquipment(row);
            else await UpdateEquipment(row);
        }
        private async Task InsertEquipment(BrandModelViewModel row)
        {
            //await _equipmentService.ModelBaseService.AddAsync(row);
        }

        private async Task UpdateEquipment(BrandModelViewModel row)
        {
            var equipment = await _equipmentService.ModelBaseService.GetByIdAsync(row.ModelId);
            if (equipment == null) return;

            equipment.EquipmentCategoryBrandId = row.EquipmentCategoryBrandId;
            await _equipmentService.ModelBaseService.SaveChangesAsync();
        }
    }
}