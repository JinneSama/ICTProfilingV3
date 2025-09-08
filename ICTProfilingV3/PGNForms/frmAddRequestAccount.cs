using ICTProfilingV3.BaseClasses;
using ICTProfilingV3.DataTransferModels.ViewModels;
using ICTProfilingV3.Interfaces;
using Models.Entities;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace ICTProfilingV3.PGNForms
{
    public partial class frmAddRequestAccount : BaseForm
    {
        private readonly IPGNService _pgnService;
        private PGNRequests _request;

        public frmAddRequestAccount(IPGNService pgnService)
        {
            InitializeComponent();
        }

        public void InitForm(PGNRequests request)
        {
            _request = request;
            LoadData();
        }

        private void LoadData()
        {
            var res = _pgnService.GetAll().Where(x => x.PGNRequestId == null)
                .Include(x => x.PGNGroupOffices)
                .Include(x => x.PGNNonEmployee)
                .ToList()
                .Select(x => new PGNAccountsViewModel
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
            var requests = await _pgnService.PGNRequestsService.GetByIdAsync(_request.Id);
            if (requests == null) return;

            requests.PGNAccounts.Add(row.PGNAccount);
            await _pgnService.PGNRequestsService.SaveChangesAsync();
        }
    }
}