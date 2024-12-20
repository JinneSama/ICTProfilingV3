using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using ICTProfilingV3.DeliveriesForms;
using ICTProfilingV3.ReportForms;
using ICTProfilingV3.TicketRequestForms;
using Models.Entities;
using Models.HRMISEntites;
using Models.Managers.User;
using Models.ReportViewModel;
using Models.Repository;
using Models.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace ICTProfilingV3.MOForms
{
    public partial class UCMOAccounts : DevExpress.XtraEditors.XtraUserControl
    {
        private IUnitOfWork unitOfWork;
        public UCMOAccounts()
        {
            InitializeComponent();
            unitOfWork = new UnitOfWork();
            LoadData();
        }

        private void LoadData()
        {
            var accounts = unitOfWork.MOAccountRepo.GetAll(x => x.Office,
                x => x.MOAccountUsers,
                x => x.MOAccountUsers.Select(s => s.PPE)).ToList().Select(x => new MOAccountsViewModel
                {
                    MOAccount = x,
                    MOAccountUsers = new BindingList<MOAccountUsers>(x.MOAccountUsers.ToList())
                });
            gcMO.DataSource = accounts.ToList();
        }

        private void btnAddPN_Click(object sender, EventArgs e)
        {
            var frm = new frmAddEditMOAccount();
            frm.ShowDialog();

            LoadData();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Delete this Account? Users of this account will be deleted as well", 
                "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel) return;

            var row = (MOAccountsViewModel)gridMO.GetFocusedRow();
            unitOfWork.MOAccountUserRepo.DeleteRange(x => x.MOAccountId == row.MOAccount.Id);
            unitOfWork.MOAccountRepo.DeleteByEx(x => x.Id == row.MOAccount.Id);
            unitOfWork.Save();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            var row = (MOAccountsViewModel)gridMO.GetFocusedRow();
            var frm = new frmAddEditMOAccount(row.MOAccount);
            frm.ShowDialog();

            LoadData();
        }

        private void btnAddAccount_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Create new User for this Account? this will create a User Request" ,
                "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel) return;

            var row = (MOAccountsViewModel)gridMO.GetFocusedRow();
            var frm = new frmAddEditAccountUsers(row.MOAccount);
            frm.ShowDialog();

            LoadData();
        }

        private void btnInfo_Click(object sender, EventArgs e)
        {
            var focusedRow = gridMO.FocusedRowHandle;
            gridMO.SetMasterRowExpanded(focusedRow, !gridMO.GetMasterRowExpanded(focusedRow));
        }

        private void btnUserRequest_Click(object sender, EventArgs e)
        {
            var main = Application.OpenForms["frmMain"] as frmMain;
            main.mainPanel.Controls.Clear();

            main.mainPanel.Controls.Add(new UCMOAccountUserRequests()
            {
                Dock = DockStyle.Fill
            });
        }

        private void hplRedirect_Click(object sender, EventArgs e)
        {
            var masterRowHandle = gridMO.FocusedRowHandle;
            var rowHandle = (gcMO.FocusedView as ColumnView).FocusedRowHandle;
            var row = gridMO.GetDetailView(masterRowHandle, 0) as GridView;
            var detailRow = (AccountUsers)row.GetRow(rowHandle);

            var main = Application.OpenForms["frmMain"] as frmMain;
            main.mainPanel.Controls.Clear();

            main.mainPanel.Controls.Add(new UCMOAccountUserRequests()
            {
                Dock = DockStyle.Fill,
                filterText = detailRow.MOAccountUser.Id.ToString()
            });
        }

        private void btnPreview_Click(object sender, EventArgs e)
        {
            var accounts = unitOfWork.MOAccountRepo.GetAll(x => x.Office,
                x => x.MOAccountUsers,
                x => x.MOAccountUsers.Select(s => s.PPE)).ToList().Select(x => new MOAccountsViewModel
                {
                    MOAccount = x,
                    MOAccountUsers = new BindingList<MOAccountUsers>(x.MOAccountUsers.ToList())
                });

            var rptAccounts = new MOAccountReportViewModel
            {
                MOAccountsViewModel = accounts,
                DatePrinted = DateTime.UtcNow,
                PrintedBy = UserStore.Username
            };

            var rptM365 = new rptM365
            {
                DataSource = new List<MOAccountReportViewModel> { rptAccounts }
            };

            rptM365.CreateDocument();
            var frm = new frmReportViewer(rptM365);
            frm.ShowDialog();
        }
    }
}
