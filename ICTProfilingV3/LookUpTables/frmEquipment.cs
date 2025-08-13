using ICTProfilingV3.BaseClasses;
using Models.Entities;
using Models.Repository;
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace ICTProfilingV3.LookUpTables
{
    public partial class frmEquipment : BaseForm
    {
        private IUnitOfWork unitOfWork;
        public frmEquipment()
        {
            InitializeComponent();
            unitOfWork = new UnitOfWork();
            LoadEquipments();
        }

        private void LoadEquipments()
        {
            var equipments = unitOfWork.EquipmentRepo.GetAll().OrderBy(o => o.EquipmentName).ToList();
            gcEquipment.DataSource = new BindingList<Equipment>(equipments);
        }

        private async void btnDeleteEquipment_Click(object sender, System.EventArgs e)
        {
            var msgRes = MessageBox.Show("Delete Equipment?", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
            if (msgRes == DialogResult.Cancel) return;

            var equipment = (Equipment)gridEquipment.GetFocusedRow();
            var res = await unitOfWork.EquipmentRepo.FindAsync(x => x.Id == equipment.Id);
            if (res == null) return;
            unitOfWork.EquipmentRepo.Delete(equipment);
            unitOfWork.Save();

            LoadEquipments();
        }

        private async void gridEquipment_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            var row = (Equipment)gridEquipment.GetFocusedRow();
            var res = await unitOfWork.EquipmentRepo.FindAsync(x => x.Id == row.Id);
            if (res == null) InsertEquipment(row);
            else UpdateEquipment(row);
        }

        private void InsertEquipment(Equipment row)
        {
            unitOfWork.EquipmentRepo.Insert(row);
            unitOfWork.Save();
        }

        private async void UpdateEquipment(Equipment row)
        {
            var equipment = await unitOfWork.EquipmentRepo.FindAsync(x => x.Id == row.Id);
            if (equipment == null) return;

            equipment.EquipmentName = row.EquipmentName;
            //unitOfWork.EquipmentRepo.Update(equipment);
            unitOfWork.Save();
        }
    }
}