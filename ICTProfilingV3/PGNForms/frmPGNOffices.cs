using ICTProfilingV3.BaseClasses;
using Models.Entities;
using Models.Repository;
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace ICTProfilingV3.PGNForms
{
    public partial class frmPGNOffices : BaseForm
    {
        private IUnitOfWork unitOfWork;
        public frmPGNOffices()
        {
            InitializeComponent();
            unitOfWork = new UnitOfWork();
            LoadData();
        }

        private void LoadData()
        {
            var res = unitOfWork.PGNGroupOfficesRepo.GetAll();
            gcOffice.DataSource = new BindingList<PGNGroupOffices>(res.ToList());
        }

        private async void gridOffice_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            var row = (PGNGroupOffices)gridOffice.GetFocusedRow();
            var res = await unitOfWork.PGNGroupOfficesRepo.FindAsync(x => x.Id == row.Id);
            if (res == null) InsertOffice(row);
            else UpdateOffice(row, res);
        }

        private void InsertOffice(PGNGroupOffices row)
        {
            unitOfWork.PGNGroupOfficesRepo.Insert(row);
            unitOfWork.Save();
        }

        private void UpdateOffice(PGNGroupOffices row, PGNGroupOffices res)
        {
            res.Office = row.Office;
            res.OfficeAcr = row.OfficeAcr;
            unitOfWork.PGNGroupOfficesRepo.Update(res);
            unitOfWork.Save();
        }

        private void btnDeleteEquipment_Click(object sender, EventArgs e)
        {
            var msgRes = MessageBox.Show("Delete Office/Group?", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
            if (msgRes == DialogResult.Cancel) return;

            var row = (PGNGroupOffices)gridOffice.GetFocusedRow();
            unitOfWork.PGNGroupOfficesRepo.DeleteByEx(x => x.Id == row.Id);
            unitOfWork.Save();

            LoadData();
        }
    }
}