using DevExpress.XtraEditors;
using Models.Entities;
using Models.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ICTProfilingV3.LookUpTables
{
    public partial class frmSuppliers : DevExpress.XtraEditors.XtraForm
    {
        private IUnitOfWork unitOfWork;
        public frmSuppliers()
        {
            InitializeComponent();
            unitOfWork = new UnitOfWork();
            LoadSuppliers();
        }
        
        private void LoadSuppliers()
        {
            var data = unitOfWork.SupplierRepo.GetAll();
            gcSuppliers.DataSource = new BindingList<Supplier>(data.ToList());
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var msgRes = MessageBox.Show("Delete Supplier?" , "Confirmation" , MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
            if (msgRes == DialogResult.Cancel) return;
            
            var row = (Supplier)gridSuppliers.GetFocusedRow();
            unitOfWork.SupplierRepo.Delete(row);
            unitOfWork.Save();

            LoadSuppliers();
        }

        private async void gridSuppliers_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            var row = (Supplier)gridSuppliers.GetFocusedRow();
            var res = await unitOfWork.SupplierRepo.FindAsync(x => x.Id == row.Id);
            if (res == null) InsertSupplier(row);
            else UpdateSupplier(row);
        }

        private void InsertSupplier(Supplier supplier)
        {
            unitOfWork.SupplierRepo.Insert(supplier);
            unitOfWork.Save();  
        }

        private async void UpdateSupplier(Supplier supplier)
        {
            var res = await unitOfWork.SupplierRepo.FindAsync(x => x.Id == supplier.Id);
            res.SupplierName = supplier.SupplierName;
            res.Address = supplier.Address;
            res.PhoneNumber = supplier.PhoneNumber;
            res.TelNumber = supplier.TelNumber;
            res.ContactPerson = supplier.ContactPerson;
            unitOfWork.Save();
        }
    }
}