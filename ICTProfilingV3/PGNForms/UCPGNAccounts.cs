using DevExpress.Charts.Native;
using Models.Entities;
using Models.Enums;
using Models.Repository;
using Models.ViewModels;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace ICTProfilingV3.PGNForms
{
    public partial class UCPGNAccounts : DevExpress.XtraEditors.XtraUserControl
    {
        private IUnitOfWork unitOfWork;
        public UCPGNAccounts()
        {
            InitializeComponent();
            unitOfWork = new UnitOfWork();
            LoadData();
        }

        private void LoadData()
        {
            var res = unitOfWork.PGNAccountsRepo.GetAll(x => x.PGNGroupOffices,
                x => x.PGNNonEmployee).ToList().Select(x => new PGNAccountsViewModel
                {
                    PGNAccount = x
                });
            gcPGN.DataSource = new BindingList<PGNAccountsViewModel>(res.ToList());
        }
        private void LoadMacAddress(PGNAccounts account)
        {
            groupMacAddress.Controls.Clear();
            groupMacAddress.Controls.Add(new UCMacAdresses(account)
            {
                Dock = DockStyle.Fill
            });
        }
        private void LoadDetails(PGNAccountsViewModel row)
        {
            txtName.Text = row.Name;
            txtPos.Text = row.Position;
            txtUsername.Text = row.PGNAccount.Username;
            txtType.Text = row.PGNAccount.UserType.ToString();
            txtOffice.Text = row.PGNAccount.PGNGroupOffices.OfficeAcr;
            txtStatus.Text = row.PGNAccount.Status.ToString();
            txtSignCount.Text = row.PGNAccount.SignInCount.ToString();
            txtTS.Text = Models.Enums.EnumHelper.GetEnumDescription(row.PGNAccount.TrafficSpeed);
            txtCategory.Text = row.PGNAccount.Designation.ToString();
            txtPassword.Text = row.PGNAccount.Password;
            txtRemarks.Text = row.PGNAccount.Remarks.ToString();
        }

        private void gridPGN_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            var row = (PGNAccountsViewModel)gridPGN.GetFocusedRow();
            if (row == null) return;

            LoadMacAddress(row.PGNAccount);
            LoadDetails(row);
        }

        private void btnCompReport_Click(object sender, System.EventArgs e)
        {
            var frm = new frmAddEditAccount();
            frm.ShowDialog();

            LoadData();
        }

        private void btnEdit_Click(object sender, System.EventArgs e)
        {
            var row = (PGNAccountsViewModel)gridPGN.GetFocusedRow();
            var frm = new frmAddEditAccount(row.PGNAccount);
            frm.ShowDialog();

            LoadData();
        }

        private void btnPreview_Click(object sender, System.EventArgs e)
        {

        }
    }
}
