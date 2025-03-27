using ICTProfilingV3.BaseClasses;
using ICTProfilingV3.LookUpTables;
using Models.Entities;
using Models.Repository;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ICTProfilingV3.TechSpecsForms
{
    public partial class frmAddEditTSICTSpecsDetails : BaseForm
    {
        private IUnitOfWork unitOfWork;
        private TechSpecsICTSpecs _specs;
        public frmAddEditTSICTSpecsDetails(TechSpecsICTSpecs specs)
        {
            InitializeComponent(); 
            unitOfWork = new UnitOfWork();
            _specs = specs;
            lblEquipment.Text = specs?.EquipmentSpecs?.Equipment?.EquipmentName;
            lblDescription.Text = specs?.Description;
            LoadSpecs();
        }

        private void LoadSpecs()
        {
            var data = unitOfWork.TechSpecsICTSpecsDetailsRepo.FindAllAsync(x => x.TechSpecsICTSpecsId == _specs.Id).OrderBy(o => o.ItemNo);
            gcEquipmentDetails.DataSource = new BindingList<TechSpecsICTSpecsDetails>(data.ToList());
        }

        private async void UpdateSpecs(TechSpecsICTSpecsDetails row)
        {
            var specs = await unitOfWork.TechSpecsICTSpecsDetailsRepo.FindAsync(x => x.Id == row.Id);
            specs.ItemNo = row.ItemNo;
            specs.Specs = row.Specs;
            specs.Description = row.Description;
            unitOfWork.TechSpecsICTSpecsDetailsRepo.Update(specs);
            unitOfWork.Save();
        }

        private void InsertSpecs(TechSpecsICTSpecsDetails row)
        {
            var equipmentDetail = new TechSpecsICTSpecsDetails
            {
                ItemNo = row.ItemNo,
                Specs = row.Specs,
                Description = row.Description,
                TechSpecsICTSpecsId = _specs.Id
            };
            unitOfWork.TechSpecsICTSpecsDetailsRepo.Insert(equipmentDetail);
            unitOfWork.Save();
            LoadSpecs();
        }

        private async void gridEquipmentDetails_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            var row = (TechSpecsICTSpecsDetails)gridEquipmentDetails.GetFocusedRow();
            var res = await unitOfWork.TechSpecsICTSpecsDetailsRepo.FindAsync(x => x.Id == row.Id);
            if (res == null) InsertSpecs(row);
            else UpdateSpecs(row);
        }

        private async void btnDelete_Click(object sender, System.EventArgs e)
        {
            var msgRes = MessageBox.Show("Delete this Specs?", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
            if (msgRes == DialogResult.Cancel) return;

            var equipment = (TechSpecsICTSpecsDetails)gridEquipmentDetails.GetFocusedRow();
            var res = await unitOfWork.TechSpecsICTSpecsDetailsRepo.FindAsync(x => x.Id == equipment.Id);
            if (res == null) return;
            unitOfWork.TechSpecsICTSpecsDetailsRepo.Delete(equipment);
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
            uow.TechSpecsICTSpecsDetailsRepo.DeleteRange(x => x.TechSpecsICTSpecsId == _specs.Id);
            await uow.SaveChangesAsync();

            foreach (var spec in specsDetails)
            {
                var equipmentDetail = new TechSpecsICTSpecsDetails
                {
                    ItemNo = spec.ItemNo,
                    Specs = spec.DetailSpecs,
                    Description = spec.DetailDescription,
                    TechSpecsICTSpecsId = _specs.Id
                };
                uow.TechSpecsICTSpecsDetailsRepo.Insert(equipmentDetail);
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
                var equipmentDetail = new TechSpecsICTSpecsDetails
                {
                    ItemNo = spec.ItemNo,
                    Specs = spec.Specs,
                    Description = spec.Description,
                    TechSpecsICTSpecsId = _specs.Id
                };
                uow.TechSpecsICTSpecsDetailsRepo.Insert(equipmentDetail);
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