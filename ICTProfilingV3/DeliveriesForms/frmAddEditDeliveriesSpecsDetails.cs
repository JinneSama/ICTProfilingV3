using Models.Entities;
using Models.Repository;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace ICTProfilingV3.DeliveriesForms
{
    public partial class frmAddEditDeliveriesSpecsDetails : DevExpress.XtraEditors.XtraForm
    {
        private IUnitOfWork unitOfWork;
        private DeliveriesSpecs _specs;
        public frmAddEditDeliveriesSpecsDetails()
        {
            InitializeComponent();
        }
        public frmAddEditDeliveriesSpecsDetails(DeliveriesSpecs specs , IUnitOfWork uow)
        {
            InitializeComponent();
            unitOfWork = uow;
            _specs = specs;
            lblEquipment.Text = specs.Model.Brand.EquipmentSpecs.Equipment.EquipmentName;
            lblDescription.Text = specs.Model.Brand.EquipmentSpecs.Description;
            LoadSpecs();
        }
        private void LoadSpecs()
        {
            var data = unitOfWork.DeliveriesSpecsDetailsRepo.FindAllAsync(x => x.DeliveriesSpecsId == _specs.Id);
            gcEquipmentDetails.DataSource = new BindingList<DeliveriesSpecsDetails>(data.ToList());
        }

        private async void UpdateSpecs(DeliveriesSpecsDetails row)
        {
            var specs = await unitOfWork.DeliveriesSpecsDetailsRepo.FindAsync(x => x.Id == row.Id);
            specs.ItemNo = row.ItemNo;
            specs.Specs = row.Specs;
            specs.Description = row.Description;
            unitOfWork.Save();
        }

        private void InsertSpecs(DeliveriesSpecsDetails row)
        {
            var equipmentDetail = new DeliveriesSpecsDetails
            {
                ItemNo = row.ItemNo,
                Specs = row.Specs,
                Description = row.Description,
                DeliveriesSpecsId = _specs.Id
            };
            unitOfWork.DeliveriesSpecsDetailsRepo.Insert(equipmentDetail);
            unitOfWork.Save();
            LoadSpecs();
        }

        private async void gridEquipmentDetails_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            var row = (DeliveriesSpecsDetails)gridEquipmentDetails.GetFocusedRow();
            var res = await unitOfWork.DeliveriesSpecsDetailsRepo.FindAsync(x => x.Id == row.Id);
            if (res == null) InsertSpecs(row);
            else UpdateSpecs(row);
        }

        private async void btnDelete_Click(object sender, System.EventArgs e)
        {
            var msgRes = MessageBox.Show("Delete this Specs?", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
            if (msgRes == DialogResult.Cancel) return;

            var equipment = (DeliveriesSpecsDetails)gridEquipmentDetails.GetFocusedRow();
            var res = await unitOfWork.DeliveriesSpecsDetailsRepo.FindAsync(x => x.Id == equipment.Id);
            if (res == null) return;
            unitOfWork.DeliveriesSpecsDetailsRepo.Delete(equipment);
            unitOfWork.Save();

            LoadSpecs();
        }
    }
}