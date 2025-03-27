using Models.Entities;
using Models.Repository;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace ICTProfilingV3.LookUpTables
{
    public partial class frmTechSpecsBasisDetails : DevExpress.XtraEditors.XtraForm
    {
        private IUnitOfWork unitOfWork;
        private TechSpecsBasis _specs;
        public frmTechSpecsBasisDetails(TechSpecsBasis specs)
        {
            InitializeComponent();
            unitOfWork = new UnitOfWork();
            _specs = specs;

            lblEquipment.Text = specs.EquipmentSpecs.Equipment.EquipmentName;
            lblDescription.Text = specs.EquipmentSpecs.Description;
            LoadSpecs();
        }
        private void LoadSpecs()
        {
            var data = unitOfWork.TechSpecsBasisDetailsRepo.FindAllAsync(x => x.TechSpecsBasisId == _specs.Id).OrderBy(o => o.ItemNo);
            gcEquipmentDetails.DataSource = new BindingList<TechSpecsBasisDetails>(data.ToList());
        }

        private async void btnDelete_Click(object sender, System.EventArgs e)
        {
            var msgRes = MessageBox.Show("Delete this Specs?", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
            if (msgRes == DialogResult.Cancel) return;

            var equipment = (TechSpecsBasisDetails)gridEquipmentDetails.GetFocusedRow();
            var res = await unitOfWork.TechSpecsBasisDetailsRepo.FindAsync(x => x.Id == equipment.Id);
            if (res == null) return;
            unitOfWork.TechSpecsBasisDetailsRepo.Delete(equipment);
            unitOfWork.Save();

            LoadSpecs();
        }

        private async void gridEquipmentDetails_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            var row = (TechSpecsBasisDetails)gridEquipmentDetails.GetFocusedRow();
            var res = await unitOfWork.TechSpecsBasisDetailsRepo.FindAsync(x => x.Id == row.Id);
            if (res == null) InsertSpecs(row);
            else UpdateSpecs(row);
        }
        private async void UpdateSpecs(TechSpecsBasisDetails row)
        {
            var specs = await unitOfWork.TechSpecsBasisDetailsRepo.FindAsync(x => x.Id == row.Id);
            specs.ItemNo = row.ItemNo;
            specs.Specs = row.Specs;
            specs.Description = row.Description;
            unitOfWork.TechSpecsBasisDetailsRepo.Update(specs);
            unitOfWork.Save();
            LoadSpecs();
        }

        private void InsertSpecs(TechSpecsBasisDetails row)
        {
            var equipmentDetail = new TechSpecsBasisDetails
            {
                ItemNo = row.ItemNo,
                Specs = row.Specs,
                Description = row.Description,
                TechSpecsBasisId = _specs.Id
            };
            unitOfWork.TechSpecsBasisDetailsRepo.Insert(equipmentDetail);
            unitOfWork.Save();
            LoadSpecs();
        }
    }
}