using Models.Entities;
using Models.Repository;
using Models.ViewModels;
using System.Data.Entity;
using System.Linq;
using System.Windows.Forms;

namespace ICTProfilingV3.TechSpecsForms
{
    public partial class UCRequestedTechSpecs : DevExpress.XtraEditors.XtraUserControl
    {
        private readonly TechSpecs _techSpecs;

        public UCRequestedTechSpecs(TechSpecs techSpecs)
        {
            InitializeComponent();
            _techSpecs = techSpecs;
            LoadTSEquipments();
        }

        private async void LoadTSEquipments()
        {
            IUnitOfWork unitOfWork = new UnitOfWork();
            var res = unitOfWork.TechSpecsICTSpecsRepo.FindAllAsync(x => x.TechSpecsId == _techSpecs.Id,
                x => x.EquipmentSpecs,
                x => x.EquipmentSpecs.Equipment).OrderBy(o => o.ItemNo);
            var TSSpecsViewModel = res.Select(x => new TechSpecsICTSpecsViewModel
            {
                Id = x.Id,
                ItemNo = x.ItemNo,
                Quantity = x.Quantity,
                Unit = x.Unit,
                Equipment = x.EquipmentSpecs.Equipment.EquipmentName,
                EquipmentSpecsId = x.EquipmentSpecsId,
                Description = x.Description,
                UnitCost = x.UnitCost,
                TotalCost = x.TotalCost,
                Purpose = x.Purpose,
                TechSpecsICTSpecsDetails = x.TechSpecsICTSpecsDetails,
                TechSpecsId = x.TechSpecsId
            });
            gcICTSpecs.DataSource = await TSSpecsViewModel.ToListAsync();
        }

        private void btnAddTS_Click(object sender, System.EventArgs e)
        {
            var row = new TechSpecsICTSpecsViewModel { TechSpecsId = _techSpecs.Id };
            var frm = new frmAddEditTechSpecsICTSpecs(row , Models.Enums.SaveType.Insert);
            frm.ShowDialog();
            
            LoadTSEquipments();
        }

        private void btnInfo_Click(object sender, System.EventArgs e)
        {
            var focusedRow = gridICTSpecs.FocusedRowHandle;
            gridICTSpecs.SetMasterRowExpanded(focusedRow, !gridICTSpecs.GetMasterRowExpanded(focusedRow));
        }

        private void btnEditData_Click(object sender, System.EventArgs e)
        {
            var row = (TechSpecsICTSpecsViewModel)gridICTSpecs.GetFocusedRow();
            var frm = new frmAddEditTechSpecsICTSpecs(row, Models.Enums.SaveType.Update);
            frm.ShowDialog();

            LoadTSEquipments();
        }

        private async void btnAddSpecs_Click(object sender, System.EventArgs e)
        {
            IUnitOfWork unitOfWork = new UnitOfWork();
            var row = (TechSpecsICTSpecsViewModel)gridICTSpecs.GetFocusedRow();
            var ictSpecs = await unitOfWork.TechSpecsICTSpecsRepo.FindAsync(x => x.Id == row.Id,
                x => x.EquipmentSpecs.Equipment);

            var frm = new frmAddEditTSICTSpecsDetails(ictSpecs);
            frm.ShowDialog();

            LoadTSEquipments();
        }

        private void btnDelete_Click(object sender, System.EventArgs e)
        {
            IUnitOfWork unitOfWork = new UnitOfWork();
            if (MessageBox.Show("Delete this Specs?", "Confirmation", MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Exclamation) == DialogResult.Cancel) return;

            var row = (TechSpecsICTSpecsViewModel)gridICTSpecs.GetFocusedRow();
            unitOfWork.TechSpecsICTSpecsRepo.DeleteByEx(x => x.Id == row.Id);
            unitOfWork.Save();
        }
    }
}
