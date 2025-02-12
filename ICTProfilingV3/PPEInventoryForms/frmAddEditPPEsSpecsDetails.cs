using ICTProfilingV3.BaseClasses;
using Models.Entities;
using Models.Repository;
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace ICTProfilingV3.PPEInventoryForms
{
    public partial class frmAddEditPPEsSpecsDetails : BaseForm
    {
        private IUnitOfWork unitOfWork;
        private PPEsSpecs _specs;
        public frmAddEditPPEsSpecsDetails(PPEsSpecs specs)
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
            var data = unitOfWork.PPEsSpecsDetailsRepo.FindAllAsync(x => x.PPEsSpecsId == _specs.Id);
            gcEquipmentDetails.DataSource = new BindingList<PPEsSpecsDetails>(data.ToList());
        }

        private async void UpdateSpecs(PPEsSpecsDetails row)
        {
            var specs = await unitOfWork.PPEsSpecsDetailsRepo.FindAsync(x => x.Id == row.Id);
            specs.ItemNo = row.ItemNo;
            specs.Specs = row.Specs;
            specs.Description = row.Description;
            unitOfWork.Save();
        }

        private void InsertSpecs(PPEsSpecsDetails row)
        {
            var equipmentDetail = new PPEsSpecsDetails
            {
                ItemNo = row.ItemNo,
                Specs = row.Specs,
                Description = row.Description,
                PPEsSpecsId = _specs.Id
            };
            unitOfWork.PPEsSpecsDetailsRepo.Insert(equipmentDetail);
            unitOfWork.Save();
            LoadSpecs();
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            var msgRes = MessageBox.Show("Delete this Specs?", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
            if (msgRes == DialogResult.Cancel) return;

            var equipment = (PPEsSpecsDetails)gridEquipmentDetails.GetFocusedRow();
            var res = await unitOfWork.PPEsSpecsDetailsRepo.FindAsync(x => x.Id == equipment.Id);
            if (res == null) return;
            unitOfWork.PPEsSpecsDetailsRepo.Delete(equipment);
            unitOfWork.Save();

            LoadSpecs();
        }

        private async void gridEquipmentDetails_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            var row = (PPEsSpecsDetails)gridEquipmentDetails.GetFocusedRow();
            var res = await unitOfWork.PPEsSpecsDetailsRepo.FindAsync(x => x.Id == row.Id);
            if (res == null) InsertSpecs(row);
            else UpdateSpecs(row);
        }
    }
}