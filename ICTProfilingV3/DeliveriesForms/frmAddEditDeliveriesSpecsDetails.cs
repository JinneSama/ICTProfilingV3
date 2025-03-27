using ICTMigration.ICTv2Models;
using ICTProfilingV3.BaseClasses;
using ICTProfilingV3.LookUpTables;
using Models.Entities;
using Models.Repository;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ICTProfilingV3.DeliveriesForms
{
    public partial class frmAddEditDeliveriesSpecsDetails : BaseForm
    {
        private IUnitOfWork unitOfWork;
        private DeliveriesSpecs _specs;
        public frmAddEditDeliveriesSpecsDetails()
        {
            InitializeComponent();
        }
        public frmAddEditDeliveriesSpecsDetails(DeliveriesSpecs specs)
        {
            InitializeComponent();
            unitOfWork = new UnitOfWork();
            _specs = specs;
            lblEquipment.Text = specs.Model.Brand.EquipmentSpecs.Equipment.EquipmentName;
            lblDescription.Text = specs.Model.Brand.EquipmentSpecs.Description;
            LoadSpecs();
        }
        private void LoadSpecs()
        {
            var data = unitOfWork.DeliveriesSpecsDetailsRepo.FindAllAsync(x => x.DeliveriesSpecsId == _specs.Id).OrderBy(o => o.ItemNo);
            gcEquipmentDetails.DataSource = new BindingList<DeliveriesSpecsDetails>(data.ToList());
        }

        private async void UpdateSpecs(DeliveriesSpecsDetails row)
        {
            var specs = await unitOfWork.DeliveriesSpecsDetailsRepo.FindAsync(x => x.Id == row.Id);
            specs.ItemNo = row.ItemNo;
            specs.Specs = row.Specs;
            specs.Description = row.Description;
            unitOfWork.DeliveriesSpecsDetailsRepo.Update(specs);
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

        private async void btnCopySpecs_Click(object sender, System.EventArgs e)
        {
            var msgRes = MessageBox.Show("This will Overwrite Existing Specs?", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
            if (msgRes == DialogResult.Cancel) return;

            var frm = new frmEquipmentSpecs()
            {
                Copy = true
            };
            frm.ShowDialog();
            await OverwriteSpecs(frm.SpecsDetails);
        }

        private async Task OverwriteSpecs(IEnumerable<EquipmentSpecsDetails> specsDetails)
        {
            if (!specsDetails.Any() || specsDetails == null) return;

            var uow = new UnitOfWork();
            uow.DeliveriesSpecsDetailsRepo.DeleteRange(x => x.DeliveriesSpecsId == _specs.Id);
            await uow.SaveChangesAsync();

            foreach (var spec in specsDetails)
            {
                var equipmentDetail = new DeliveriesSpecsDetails
                {
                    ItemNo = spec.ItemNo,
                    Specs = spec.DetailSpecs,
                    Description = spec.DetailDescription,
                    DeliveriesSpecsId = _specs.Id
                };
                uow.DeliveriesSpecsDetailsRepo.Insert(equipmentDetail);
            }
            await uow.SaveChangesAsync();
            LoadSpecs();
        }

        private async Task OverwriteSpecsFromTSBasis(IEnumerable<TechSpecsBasisDetails> specsDetails)
        {
            if (!specsDetails.Any() || specsDetails == null) return;
            var uow = new UnitOfWork();
            uow.TechSpecsICTSpecsDetailsRepo.DeleteRange(x => x.TechSpecsICTSpecsId == _specs.Id);
            await uow.SaveChangesAsync();

            foreach (var spec in specsDetails)
            {
                var equipmentDetail = new DeliveriesSpecsDetails
                {
                    ItemNo = spec.ItemNo,
                    Specs = spec.Specs,
                    Description = spec.Description,
                    DeliveriesSpecsId = _specs.Id
                };
                uow.DeliveriesSpecsDetailsRepo.Insert(equipmentDetail);
            }
            await uow.SaveChangesAsync();
            LoadSpecs();
        }

        private async void btnCopyTSBasis_Click(object sender, System.EventArgs e)
        {
            var msgRes = MessageBox.Show("This will Overwrite Existing Specs?", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
            if (msgRes == DialogResult.Cancel) return;

            var frm = new frmTechSpecsBasis()
            {
                Copy = true
            };
            frm.ShowDialog();
            await OverwriteSpecsFromTSBasis(frm.SpecsDetails);
        }
    }
}