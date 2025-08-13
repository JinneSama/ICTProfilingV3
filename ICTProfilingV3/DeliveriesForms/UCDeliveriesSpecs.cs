using ICTProfilingV3.DataTransferModels.ViewModels;
using Models.Entities;
using Models.Repository;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace ICTProfilingV3.DeliveriesForms
{
    public partial class UCDeliveriesSpecs : DevExpress.XtraEditors.XtraUserControl
    {
        private readonly Deliveries _deliveries;

        public UCDeliveriesSpecs(Deliveries deliveries, bool forViewing = true)
        {
            InitializeComponent();
            _deliveries = deliveries;
            SetHiddenButtons(!forViewing);
        }

        private void SetHiddenButtons(bool forViewing)
        {
            colDelete.Visible = forViewing;
            colEdit.Visible = forViewing;
            btnAddEquipment.Visible = forViewing;
            colAddSpecs.Visible = forViewing;
        }


        private void LoadEquipmentSpecs()
        {
            IUnitOfWork unitOfWork = new UnitOfWork();
            var res = unitOfWork.DeliveriesSpecsRepo.FindAllAsync(x => x.DeliveriesId == _deliveries.Id).Select(x => new DeliveriesSpecsViewModel
            {
                Id = x.Id,
                ItemNo = (int)x.ItemNo,
                Quantity = (int)x.Quantity,
                Unit = x.Unit,
                Equipment = x.Model.Brand.EquipmentSpecs.Equipment.EquipmentName,
                Description = x.Description,
                Brand = x.Model.Brand.BrandName,
                Model = x.Model.ModelName,
                UnitCost = (long)x.UnitCost,
                TotalCost = (long)x.TotalCost,
                DeliveriesSpecsDetails = x.DeliveriesSpecsDetails.OrderBy(o => o.ItemNo).ToList()
            });
            gcEquipmentSpecs.DataSource = new BindingList<DeliveriesSpecsViewModel>(res.ToList());
        }

        private void btnAddEquipment_Click(object sender, System.EventArgs e)
        {
            var frm = new frmAddEquipment(_deliveries);
            frm.ShowDialog();

            LoadEquipmentSpecs();
        }

        private void btnInfo_Click(object sender, System.EventArgs e)
        {
            var focusedRow = gridEquipmentSpecs.FocusedRowHandle;
            gridEquipmentSpecs.SetMasterRowExpanded(focusedRow, !gridEquipmentSpecs.GetMasterRowExpanded(focusedRow));
        }

        private async void btnEditData_Click(object sender, System.EventArgs e)
        {
            IUnitOfWork unitOfWork = new UnitOfWork();
            var row = (DeliveriesSpecsViewModel)gridEquipmentSpecs.GetFocusedRow();
            var delSpecs = await unitOfWork.DeliveriesSpecsRepo.FindAsync(x => x.Id == row.Id,
                x => x.Model ,
                x => x.Model.Brand,
                x => x.Model.Brand.EquipmentSpecs.Equipment,
                x => x.Model.Brand.EquipmentSpecs);
            var frm = new frmAddEquipment(delSpecs);
            frm.ShowDialog();

            LoadEquipmentSpecs();
        }

        private async void btnAddSpecs_Click(object sender, System.EventArgs e)
        {
            IUnitOfWork unitOfWork = new UnitOfWork();
            var row = (DeliveriesSpecsViewModel)gridEquipmentSpecs.GetFocusedRow();
            var delSpecs = await unitOfWork.DeliveriesSpecsRepo.FindAsync(x => x.Id == row.Id,
                x => x.Model.Brand,
                x => x.Model.Brand.EquipmentSpecs,
                x => x.Model.Brand.EquipmentSpecs.Equipment);
            var frm = new frmAddEditDeliveriesSpecsDetails(delSpecs);
            frm.ShowDialog();

            LoadEquipmentSpecs();
        }

        private void UCDeliveriesSpecs_Load(object sender, System.EventArgs e)
        {
            LoadEquipmentSpecs();
        }

        private void btnDelete_Click(object sender, System.EventArgs e)
        {
            IUnitOfWork unitOfWork = new UnitOfWork();
            
            if (MessageBox.Show("Delete this Specs?", "Confirmation", MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Exclamation) == DialogResult.Cancel) return;

            var row = (DeliveriesSpecsViewModel)gridEquipmentSpecs.GetFocusedRow();
            unitOfWork.DeliveriesSpecsRepo.DeleteByEx(x => x.Id == row.Id);
            unitOfWork.Save();
        }
    }
}
