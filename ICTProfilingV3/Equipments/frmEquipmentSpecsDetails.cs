using ICTProfilingV3.BaseClasses;
using Models.Entities;
using Models.Repository;
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace ICTProfilingV3.Equipments
{
    public partial class frmEquipmentSpecsDetails : BaseForm
    {
        private IUnitOfWork unitOfWork;
        private EquipmentSpecs _specs;
        public frmEquipmentSpecsDetails()
        {
            InitializeComponent();
        }

        public frmEquipmentSpecsDetails(EquipmentSpecs specs , IUnitOfWork uow)
        {
            InitializeComponent();
            unitOfWork = uow;
            _specs = specs;
            lblEquipment.Text = specs.Equipment.EquipmentName;
            lblDescription.Text = specs.Description;
            LoadSpecs();
        }

        private void LoadSpecs()
        {
            var data = unitOfWork.EquipmentSpecsDetailsRepo.FindAllAsync(x => x.EquipmentSpecsId == _specs.Id).OrderBy(o => o.ItemNo); 
            gcEquipmentDetails.DataSource = new BindingList<EquipmentSpecsDetails>(data.ToList());  
        }

        private async void gridEquipmentDetails_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            var row = (EquipmentSpecsDetails)gridEquipmentDetails.GetFocusedRow();
            var res = await unitOfWork.EquipmentSpecsDetailsRepo.FindAsync(x => x.Id == row.Id);
            if (res == null) InsertSpecs(row);
            else UpdateSpecs(row);
        }

        private async void UpdateSpecs(EquipmentSpecsDetails row)
        {
            var specs = await unitOfWork.EquipmentSpecsDetailsRepo.FindAsync(x => x.Id == row.Id);    
            specs.ItemNo = row.ItemNo;
            specs.DetailSpecs = row.DetailSpecs;
            specs.DetailDescription = row.DetailDescription;    
            unitOfWork.Save();
        }

        private void InsertSpecs(EquipmentSpecsDetails row)
        {
            var equipmentDetail = new EquipmentSpecsDetails
            {
                ItemNo = row.ItemNo,
                DetailSpecs = row.DetailSpecs,
                DetailDescription = row.DetailDescription,
                EquipmentSpecsId = _specs.Id
            };
            unitOfWork.EquipmentSpecsDetailsRepo.Insert(equipmentDetail);
            unitOfWork.Save();
            LoadSpecs();
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            var msgRes = MessageBox.Show("Delete this Specs?", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
            if (msgRes == DialogResult.Cancel) return;

            var equipment = (EquipmentSpecsDetails)gridEquipmentDetails.GetFocusedRow();
            var res = await unitOfWork.EquipmentSpecsDetailsRepo.FindAsync(x => x.Id == equipment.Id);
            if (res == null) return;
            unitOfWork.EquipmentSpecsDetailsRepo.Delete(equipment);
            unitOfWork.Save();

            LoadSpecs();
        }
    }
}