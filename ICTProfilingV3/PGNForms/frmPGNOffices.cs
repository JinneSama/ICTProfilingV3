using ICTProfilingV3.BaseClasses;
using ICTProfilingV3.Interfaces;
using Models.Entities;
using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ICTProfilingV3.PGNForms
{
    public partial class frmPGNOffices : BaseForm
    {
        private readonly IPGNService _pgnService;
        public frmPGNOffices(IPGNService pgnService)
        {
            _pgnService = pgnService;
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            var res = _pgnService.PGNGroupOfficeService.GetAll();
            gcOffice.DataSource = new BindingList<PGNGroupOffices>(res.ToList());
        }

        private async void gridOffice_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            var row = (PGNGroupOffices)gridOffice.GetFocusedRow();
            var res = await _pgnService.PGNGroupOfficeService.GetByIdAsync(row.Id);
            if (res == null) await InsertOffice(row);
            else await UpdateOffice(row, res);
        }

        private async Task InsertOffice(PGNGroupOffices row)
        {
            await _pgnService.PGNGroupOfficeService.AddAsync(row);
        }

        private async Task UpdateOffice(PGNGroupOffices row, PGNGroupOffices res)
        {
            res.Office = row.Office;
            res.OfficeAcr = row.OfficeAcr;
            await _pgnService.PGNGroupOfficeService.SaveChangesAsync();
        }

        private async void btnDeleteEquipment_Click(object sender, EventArgs e)
        {
            var msgRes = MessageBox.Show("Delete Office/Group?", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
            if (msgRes == DialogResult.Cancel) return;

            var row = (PGNGroupOffices)gridOffice.GetFocusedRow();
            await _pgnService.PGNGroupOfficeService.DeleteAsync(row.Id);

            LoadData();
        }
    }
}