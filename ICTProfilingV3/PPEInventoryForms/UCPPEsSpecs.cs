using ICTProfilingV3.DataTransferModels.ViewModels;
using ICTProfilingV3.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Models.Entities;
using System;
using System.ComponentModel;
using System.Data;
using System.Linq;

namespace ICTProfilingV3.PPEInventoryForms
{
    public partial class UCPPEsSpecs : DevExpress.XtraEditors.XtraUserControl
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IPPEInventoryService _ppeInventoryService;
        private PPEs _ppe;
        public UCPPEsSpecs(IServiceProvider serviceProvider, IPPEInventoryService ppeInventoryService)
        {
            _serviceProvider = serviceProvider;
            _ppeInventoryService = ppeInventoryService;
            InitializeComponent();
        }

        public void InitUC(PPEs ppe, bool forViewing = true)
        {
            _ppe = ppe;
            LoadEquipmentSpecs();
            btnAddEquipment.Visible = !forViewing;
        }

        private void LoadEquipmentSpecs()
        {
            var res = _ppeInventoryService.PPESpecsBaseService.GetAll().Where(x => x.PPEsId == _ppe.Id).Select(x => new PPEsSpecsViewModel
            {
                Id = x.Id,
                ItemNo = x.ItemNo,
                Quantity = x.Quantity,
                Unit = x.Unit,
                Equipment = x.Model.Brand.EquipmentSpecs.Equipment.EquipmentName,
                Description = x.Model.Brand.EquipmentSpecs.Description,
                Brand = x.Model.Brand.BrandName,
                Model = x.Model.ModelName,
                UnitCost = x.UnitCost,
                TotalCost = x.TotalCost,
                PPEsSpecsDetails = x.PPEsSpecsDetails
            });
            gcEquipmentSpecs.DataSource = new BindingList<PPEsSpecsViewModel>(res.ToList());
        }

        private void btnAddEquipment_Click(object sender, EventArgs e)
        {
            var frm = _serviceProvider.GetRequiredService<frmAddEditPPEEquipment>();
            frm.ShowDialog();

            LoadEquipmentSpecs();
        }

        private void btnInfo_Click(object sender, EventArgs e)
        {
            var focusedRow = gridEquipmentSpecs.FocusedRowHandle;
            gridEquipmentSpecs.SetMasterRowExpanded(focusedRow, !gridEquipmentSpecs.GetMasterRowExpanded(focusedRow));
        }

        private async void btnEditData_Click(object sender, EventArgs e)
        {
            var row = (PPEsSpecsViewModel)gridEquipmentSpecs.GetFocusedRow();
            var ppeSpecs = await _ppeInventoryService.PPESpecsBaseService.GetByIdAsync(row.Id);
            if (ppeSpecs == null) return;
            var frm = _serviceProvider.GetRequiredService<frmAddEditPPEEquipment>();
            frm.InitForm(ppeSpecs);
            frm.ShowDialog();

            LoadEquipmentSpecs();
        }

        private async void btnAddSpecs_Click(object sender, EventArgs e)
        {
            var row = (PPEsSpecsViewModel)gridEquipmentSpecs.GetFocusedRow();
            var ppeSpecs = await _ppeInventoryService.PPESpecsBaseService.GetByIdAsync(row.Id);
            var frm = _serviceProvider.GetRequiredService<frmAddEditPPEsSpecsDetails>();
            frm.InitForm(ppeSpecs);
            frm.ShowDialog();

            LoadEquipmentSpecs();
        }
    }
}
