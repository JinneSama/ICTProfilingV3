using ICTProfilingV3.DataTransferModels.ViewModels;
using ICTProfilingV3.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Models.Entities;
using System;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Windows.Forms;

namespace ICTProfilingV3.PGNForms
{
    public partial class UCRequestAccount : DevExpress.XtraEditors.XtraUserControl
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IPGNService _pgnService;
        private PGNRequests _request;

        public UCRequestAccount(IServiceProvider serviceProvider, IPGNService pgnService)
        {
            _serviceProvider = serviceProvider;
            _pgnService = pgnService;
            InitializeComponent();
        }

        public void InitUC(PGNRequests request)
        {
            _request = request;
            LoadData();
        }

        private void LoadData()
        {
            var res = _pgnService.GetAll()
                .Where(x => x.PGNRequestId == _request.Id)
                .Include(x => x.PGNGroupOffices)
                .Include(x => x.PGNNonEmployee)
                .ToList()
                .Select(x => new PGNAccountsViewModel
                {
                    PGNAccount = x
                });
            gcAccount.DataSource = new BindingList<PGNAccountsViewModel>(res.ToList());
        }

        private async void btnDelete_Click(object sender, System.EventArgs e)
        {
            if (MessageBox.Show("Remove this Account from this Request?", "Confirmation", MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Exclamation) == DialogResult.Cancel) return;

            var row = (PGNAccountsViewModel)gridAccount.GetFocusedRow();
            var request = await _pgnService.PGNRequestsService.GetByIdAsync(_request.Id);
            request.PGNAccounts.Remove(row.PGNAccount);
            await _pgnService.PGNRequestsService.SaveChangesAsync();

            LoadData();
        }

        private void btnPGNAccount_Click(object sender, System.EventArgs e)
        {
            var frm = _serviceProvider.GetRequiredService<frmAddRequestAccount>();
            frm.InitForm(_request);
            frm.ShowDialog();

            LoadData();
        }
    }
}
