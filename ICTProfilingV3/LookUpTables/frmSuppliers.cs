using ICTProfilingV3.BaseClasses;
using ICTProfilingV3.Interfaces;
using Models.Entities;
using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ICTProfilingV3.LookUpTables
{
    public partial class frmSuppliers : BaseForm
    {
        private readonly ILookUpService _lookUpService;
        public frmSuppliers(ILookUpService lookUpService)
        {
            _lookUpService = lookUpService;
            InitializeComponent();
            LoadSuppliers();
        }
        
        private void LoadSuppliers()
        {
            var data = _lookUpService.SupplierDataService.GetAll();
            gcSuppliers.DataSource = new BindingList<Supplier>(data.ToList());
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            var msgRes = MessageBox.Show("Delete Supplier?" , "Confirmation" , MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
            if (msgRes == DialogResult.Cancel) return;
            
            var row = (Supplier)gridSuppliers.GetFocusedRow();
            await _lookUpService.SupplierDataService.DeleteAsync(row.Id);

            LoadSuppliers();
        }

        private async void gridSuppliers_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            var row = (Supplier)gridSuppliers.GetFocusedRow();
            var res = await _lookUpService.SupplierDataService.GetByIdAsync(row.Id);
            if (res == null) await InsertSupplier(row);
            else await UpdateSupplier(row);
        }

        private async Task InsertSupplier(Supplier supplier)
        {
            await _lookUpService.SupplierDataService.AddAsync(supplier);
            LoadSuppliers();
        }

        private async Task UpdateSupplier(Supplier supplier)
        {
            var res = await _lookUpService.SupplierDataService.GetByIdAsync(supplier.Id);
            res.SupplierName = supplier.SupplierName;
            res.Address = supplier.Address;
            res.PhoneNumber = supplier.PhoneNumber;
            res.TelNumber = supplier.TelNumber;
            res.ContactPerson = supplier.ContactPerson;
            await _lookUpService.SupplierDataService.SaveChangesAsync();
        }
    }
}