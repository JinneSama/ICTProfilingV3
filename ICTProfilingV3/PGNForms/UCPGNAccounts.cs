using DevExpress.Charts.Native;
using DevExpress.ClipboardSource.SpreadsheetML;
using ICTProfilingV3.DataTransferModels;
using ICTProfilingV3.DataTransferModels.ReportViewModel;
using ICTProfilingV3.DataTransferModels.ViewModels;
using ICTProfilingV3.ReportForms;
using Models.Entities;
using Models.Enums;
using Models.Repository;
using System.Collections.Generic;
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
            txtName.Text = row?.Name;
            txtPos.Text = row?.Position;
            txtUsername.Text = row?.PGNAccount?.Username;
            txtType.Text = row?.PGNAccount?.UserType?.ToString();
            txtOffice.Text = row?.PGNAccount?.PGNGroupOffices?.OfficeAcr;
            txtStatus.Text = row?.PGNAccount?.Status?.ToString();
            txtSignCount.Text = row?.PGNAccount?.SignInCount?.ToString();
            txtTS.Text = Models.Enums.EnumHelper.GetEnumDescription(row.PGNAccount.TrafficSpeed);
            txtCategory.Text = row?.PGNAccount?.Designation.ToString();
            txtPassword.Text = row?.PGNAccount?.Password;
            txtRemarks.Text = row?.PGNAccount?.Remarks?.ToString();
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
            if(row == null) return;
            var frm = new frmAddEditAccount(row.PGNAccount);
            frm.ShowDialog();

            LoadData();
        }

        private void btnPreview_Click(object sender, System.EventArgs e)
        {
            var res = unitOfWork.PGNAccountsRepo.GetAll(x => x.PGNGroupOffices,
                x => x.PGNNonEmployee).Where(x => x.Username.StartsWith("sp.")).ToList().Select(x => new PGNAccountsViewModel
                {
                    PGNAccount = x
                }).ToList();

            var data = res.Select(x => new PGNAccountDTM
            {
                Id = x.PGNAccount.Id,
                Name = x.Name,
                Position = x.Position,
                Username = x.PGNAccount.Username,
                UserType = x.PGNAccount.UserType?.ToString(),
                OfficeAcr = x.PGNAccount.PGNGroupOffices?.OfficeAcr,
                Status = x.PGNAccount.Status?.ToString(),
                SignInCount = x.PGNAccount.SignInCount ?? 0,
                TrafficSpeed = Models.Enums.EnumHelper.GetEnumDescription(x.PGNAccount.TrafficSpeed),
                Designation = x.PGNAccount.Designation.ToString(),
                Password = x.PGNAccount.Password,
                Remarks = x.PGNAccount.Remarks,
                MacAddresses = unitOfWork.PGNMacAddressesRepo.FindAllAsync(m => m.PGNAccountId == x.PGNAccount.Id).ToList()
            }).ToList();

            var report = new PGNReportDTM
            {
                PGNAccounts = data
            };

            var rpt = new rptPGNUsers
            {
                DataSource = new List<PGNReportDTM> { report }
            };

            var frm = new frmReportViewer(rpt);
            frm.ShowDialog();
        }
    }
}
