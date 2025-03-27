using Models.Entities;
using Models.Repository;
using Models.ViewModels;
using System;
using System.ComponentModel;
using System.Data;
using System.Linq;

namespace ICTProfilingV3.PPEInventoryForms
{
    public partial class UCPPEsSpecs : DevExpress.XtraEditors.XtraUserControl
    {
        private readonly PPEs _ppe;
        private IUnitOfWork unitOfWork;
        public UCPPEsSpecs(PPEs ppe)
        {
            InitializeComponent();
            unitOfWork = new UnitOfWork();
            _ppe = ppe;
            LoadEquipmentSpecs();
        }

        private void LoadEquipmentSpecs()
        {
            var uow = new UnitOfWork();
            var res = uow.PPEsSpecsRepo.FindAllAsync(x => x.PPEsId == _ppe.Id).Select(x => new PPEsSpecsViewModel
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
            var frm = new frmAddEditPPEEquipment(_ppe);
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
            var ppeSpecs = await unitOfWork.PPEsSpecsRepo.FindAsync(x => x.Id == row.Id,
                x => x.Model,
                x => x.Model.Brand,
                x => x.Model.Brand.EquipmentSpecs,
                x => x.Model.Brand.EquipmentSpecs.Equipment);
            if (ppeSpecs == null) return;
            var frm = new frmAddEditPPEEquipment(ppeSpecs);
            frm.ShowDialog();

            LoadEquipmentSpecs();
        }

        private async void btnAddSpecs_Click(object sender, EventArgs e)
        {
            var row = (PPEsSpecsViewModel)gridEquipmentSpecs.GetFocusedRow();
            var ppeSpecs = await unitOfWork.PPEsSpecsRepo.FindAsync(x => x.Id == row.Id,
                x => x.Model.Brand,
                x => x.Model.Brand.EquipmentSpecs,
                x => x.Model.Brand.EquipmentSpecs.Equipment);
            var frm = new frmAddEditPPEsSpecsDetails(ppeSpecs);
            frm.ShowDialog();

            LoadEquipmentSpecs();
        }
    }
}
