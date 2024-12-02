using Models.Entities;
using Models.Repository;
using Models.ViewModels;
using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace ICTProfilingV3.PGNForms
{
    public partial class frmAddRequestAccount : DevExpress.XtraEditors.XtraForm
    {
        private IUnitOfWork unitOfWork;
        private readonly PGNRequests request;

        public frmAddRequestAccount(PGNRequests request)
        {
            InitializeComponent();
            unitOfWork = new UnitOfWork();
            this.request = request;
            LoadData();
        }

        private void LoadData()
        {
            var res = unitOfWork.PGNAccountsRepo.FindAllAsync(x => x.PGNRequestId == null, x => x.PGNGroupOffices,
                x => x.PGNNonEmployee).ToList().Select(x => new PGNAccountsViewModel
                {
                    PGNAccount = x
                });
            gcAccount.DataSource = new BindingList<PGNAccountsViewModel>(res.ToList());
        }
        private async void btnProceed_Click(object sender, System.EventArgs e)
        {
            await AddToRequest();

            this.Close();
        }

        private async Task AddToRequest()
        {
            var row = (PGNAccountsViewModel)gridAccount.GetFocusedRow();
            var _request = await unitOfWork.PGNRequestsRepo.FindAsync(x => x.Id == request.Id);
            if (_request == null) return;

            _request.PGNAccounts.Add(row.PGNAccount);
            await unitOfWork.SaveChangesAsync();
        }
    }
}