using DevExpress.XtraGrid.Views.Grid;
using ICTProfilingV3.BaseClasses;
using ICTProfilingV3.DataTransferModels;
using ICTProfilingV3.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Models.Entities;
using System;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ICTProfilingV3.EquipmentForms
{
    public partial class frmEquipmentBrand : BaseForm
    {
        private readonly IEquipmentService _equipmentService;
        private readonly IServiceProvider _serviceProvider;
        public frmEquipmentBrand(IEquipmentService equipmentService, IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _equipmentService = equipmentService;
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            var categories = _equipmentService.EquEquipmentCategoryBaseService
                .GetAll()
                .Include(c => c.EquipmentCategoryBrands)
                .Include(c => c.EquipmentCategoryBrands.Select(s => s.EquipmentBrand))
                .Include(c => c.EquipmentCategoryBrands.Select(b => b.Models))
                .ToList();

            var data = categories.Select(x => new EquipmentBrandDTM
            {
                EquipmentCategory = x,
                Brands = new BindingList<BrandDTM>(x.EquipmentCategoryBrands.Select(
                    b => new BrandDTM
                    {
                        EquipmentCategoryBrandId = b.Id,
                        Brand = b.EquipmentBrand,
                        Models = new BindingList<Model>(b.Models.ToList())
                    })
                .ToList())
            }).ToList();

            gcEquipmentBrand.DataSource = new BindingList<EquipmentBrandDTM>(data);
        }

        private void btnShowBrands_Click(object sender, System.EventArgs e)
        {
            var focusedRow = gridEquipment.FocusedRowHandle;
            gridEquipment.SetMasterRowExpanded(focusedRow, !gridEquipment.GetMasterRowExpanded(focusedRow));
        }

        private void btnAddBrand_Click(object sender, System.EventArgs e)
        {
            var row = (EquipmentBrandDTM)gridEquipment.GetFocusedRow();
            var frm = _serviceProvider.GetRequiredService<frmAddEquipmentBrand>();
            frm.InitForm(row.EquipmentCategory);
            frm.ShowDialog();

            LoadData();
        }

        private async void btnDeleteBrand_Click(object sender, System.EventArgs e)
        {
            if (MessageBox.Show("Delete this Brand?","Confirmation", 
                MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.Cancel) 
                return;

            var masterRowHandle = gridEquipment.FocusedRowHandle;
            GridView detailView = gridEquipment.GetDetailView(masterRowHandle, 0) as GridView;
            var row = (BrandDTM)detailView.GetFocusedRow();

            await _equipmentService.ModelBaseService.DeleteRangeAsync(x => x.EquipmentCategoryBrandId == row.EquipmentCategoryBrandId);
            await _equipmentService.EquipmentCategoryBrandBaseService.DeleteAsync(row.EquipmentCategoryBrandId);
            LoadData();
        }

        private async void btnDeleteEquipment_Click(object sender, System.EventArgs e)
        {
            if (MessageBox.Show("Delete this Equipment?", "Confirmation",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.Cancel)
                return;

            var row = (EquipmentBrandDTM)gridEquipment.GetFocusedRow();
            await _equipmentService.ModelBaseService.DeleteRangeAsync(x => x.EquipmentCategoryBrand.EquipmentCategoryId == row.EquipmentCategory.Id);
            await _equipmentService.EquipmentCategoryBrandBaseService.DeleteRangeAsync(x => x.EquipmentCategoryId == row.EquipmentCategory.Id);
            await _equipmentService.EquEquipmentCategoryBaseService.DeleteAsync(row.EquipmentCategory.Id);
            LoadData();
        }

        private void btnEditEquipment_Click(object sender, System.EventArgs e)
        {
            var row = (EquipmentBrandDTM)gridEquipment.GetFocusedRow();
            var frm = _serviceProvider.GetRequiredService<frmAddEquipmentCategory>();
            frm.InitForm(row.EquipmentCategory);
            frm.ShowDialog();
            LoadData();
        }

        private async void btnAddModel_Click(object sender, EventArgs e)
        {
            var masterRowHandle = gridEquipment.FocusedRowHandle;
            GridView detailView = gridEquipment.GetDetailView(masterRowHandle, 0) as GridView;

            if (detailView != null)
            {
                var row = (BrandDTM)detailView.GetFocusedRow();
                var equipmentBrandCategory = await _equipmentService.EquipmentCategoryBrandBaseService.GetByIdAsync(row.EquipmentCategoryBrandId);
                
                var frm = _serviceProvider.GetRequiredService<frmAddEquipmentModel>();
                frm.InitForm(equipmentBrandCategory);
                frm.ShowDialog();

                LoadData();
            }
        }

        private void btnShowModels_Click(object sender, EventArgs e)
        {
            var masterRowHandle = gridEquipment.FocusedRowHandle;
            GridView detailView = gridEquipment.GetDetailView(masterRowHandle, 0) as GridView;
            var focusedRow = detailView.FocusedRowHandle;
            var expanded = !detailView.GetMasterRowExpandedEx(focusedRow, 0);
            detailView.SetMasterRowExpanded(focusedRow, expanded);
        }

        private async void btnEditModel_ClickAsync(object sender, EventArgs e)
        {
            var masterRowHandle = gridEquipment.FocusedRowHandle;
            GridView detailView = gridEquipment.GetDetailView(masterRowHandle, 0) as GridView;

            var secondDetailRowHandle = detailView.FocusedRowHandle;
            GridView thirdDetailView = detailView.GetDetailView(secondDetailRowHandle, 0) as GridView;

            var rowModel = (Model)thirdDetailView.GetFocusedRow();
            if (detailView != null)
            {
                var row = (BrandDTM)detailView.GetFocusedRow();
                var equipmentBrandCategory = await _equipmentService.EquipmentCategoryBrandBaseService.GetByIdAsync(row.EquipmentCategoryBrandId);

                var frm = _serviceProvider.GetRequiredService<frmAddEquipmentModel>();
                frm.InitForm(equipmentBrandCategory, rowModel);
                frm.ShowDialog();

                LoadData();
            }
        }

        private async void btnDeleteModel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Delete this Model?", "Confirmation",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.Cancel)
                return;

            var masterRowHandle = gridEquipment.FocusedRowHandle;
            GridView detailView = gridEquipment.GetDetailView(masterRowHandle, 0) as GridView;

            var secondDetailRowHandle = detailView.FocusedRowHandle;
            GridView thirdDetailView = detailView.GetDetailView(secondDetailRowHandle, 0) as GridView;

            var row = (Model)thirdDetailView.GetFocusedRow();
            await _equipmentService.ModelBaseService.DeleteAsync(row.Id);
            LoadData();
        }

        private void btnBrandEdit_Click(object sender, EventArgs e)
        {
            var masterRowHandle = gridEquipment.FocusedRowHandle;
            GridView detailView = gridEquipment.GetDetailView(masterRowHandle, 0) as GridView;

            var rowBrand = (BrandDTM)detailView.GetFocusedRow();
            var row = (EquipmentBrandDTM)gridEquipment.GetFocusedRow();
            var frm = _serviceProvider.GetRequiredService<frmAddEquipmentBrand>();
            frm.InitForm(row.EquipmentCategory, rowBrand.EquipmentCategoryBrandId);
            frm.ShowDialog();

            LoadData();
        }

        private void btnAddEquipmentCategory_Click(object sender, EventArgs e)
        {
            var row = (EquipmentBrandDTM)gridEquipment.GetFocusedRow();
            var frm = _serviceProvider.GetRequiredService<frmAddEquipmentCategory>();
            frm.InitForm();
            frm.ShowDialog();
            LoadData();
        }
    }
}