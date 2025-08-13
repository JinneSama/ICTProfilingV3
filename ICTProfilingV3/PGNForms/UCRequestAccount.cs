using ICTProfilingV3.DataTransferModels.ViewModels;
using Models.Entities;
using Models.Repository;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace ICTProfilingV3.PGNForms
{
    public partial class UCRequestAccount : DevExpress.XtraEditors.XtraUserControl
    {
        private readonly PGNRequests request;
        private IUnitOfWork unitOfWork;

        public UCRequestAccount(PGNRequests request)
        {
            InitializeComponent();
            this.request = request;
            unitOfWork = new UnitOfWork();
            LoadData();
        }

        private void LoadData()
        {
            var res = unitOfWork.PGNAccountsRepo.FindAllAsync(x => x.PGNRequestId == request.Id ,x => x.PGNGroupOffices,
                x => x.PGNNonEmployee).ToList().Select(x => new PGNAccountsViewModel
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
            var _request = await unitOfWork.PGNRequestsRepo.FindAsync(x => x.Id == request.Id);
            _request.PGNAccounts.Remove(row.PGNAccount);
            unitOfWork.Save();

            LoadData();
        }

        private void btnPGNAccount_Click(object sender, System.EventArgs e)
        {
            var frm = new frmAddRequestAccount(request);
            frm.ShowDialog();

            LoadData();
        }
    }
}
