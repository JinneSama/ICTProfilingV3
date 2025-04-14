using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using Helpers.Interfaces;
using Helpers.Utility;
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
    public partial class UCMOAccounts : DevExpress.XtraEditors.XtraUserControl, IDisposeUC
    {
        private readonly IUCManager<Control> _ucManager;
        public UCMOAccounts()
        {
            InitializeComponent();
            var main = Application.OpenForms["frmMain"] as frmMain;
            _ucManager = main._ucManager;
            LoadData();
        }

        private void LoadData()
        {
            IUnitOfWork unitOfWork = new UnitOfWork();
            var accounts = unitOfWork.MOAccountRepo.GetAll(x => x.Office,
                x => x.MOAccountUsers,
                x => x.MOAccountUsers.Select(s => s.PPE)).ToList().Select(x => new MOAccountsViewModel
                {
                    MOAccount = x,
                    MOAccountUsers = new BindingList<MOAccountUsers>(x.MOAccountUsers.ToList())
                });
            gcLogs.DataSource = accounts.ToList();
        }

        private void btnAddPN_Click(object sender, EventArgs e)
        {
            var frm = new frmAddEditMOAccount();
            frm.ShowDialog();

            LoadData();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            IUnitOfWork unitOfWork = new UnitOfWork();
            if (MessageBox.Show("Delete this Account? Users of this account will be deleted as well", 
                "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel) return;

            var row = (MOAccountsViewModel)gridLogs.GetFocusedRow();
            unitOfWork.MOAccountUserRepo.DeleteRange(x => x.MOAccountId == row.MOAccount.Id);
            unitOfWork.MOAccountRepo.DeleteByEx(x => x.Id == row.MOAccount.Id);
            unitOfWork.Save();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            var row = (MOAccountsViewModel)gridLogs.GetFocusedRow();
            var frm = new frmAddEditMOAccount(row.MOAccount);
            frm.ShowDialog();

            LoadData();
        }

        private void btnAddAccount_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Create new User for this Account? this will create a User Request" ,
                "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel) return;

            var row = (MOAccountsViewModel)gridLogs.GetFocusedRow();
            var frm = new frmAddEditAccountUsers(row.MOAccount);
            frm.ShowDialog();

            LoadData();
        }

        private void btnInfo_Click(object sender, EventArgs e)
        {
            var focusedRow = gridLogs.FocusedRowHandle;
            gridLogs.SetMasterRowExpanded(focusedRow, !gridLogs.GetMasterRowExpanded(focusedRow));
        }

        private void btnUserRequest_Click(object sender, EventArgs e)
        {
            _ucManager.ShowUCSystemDetails(btnUserRequest.Name, new UCMOAccountUserRequests()
            {
                Dock = DockStyle.Fill
            },null);
        }

        private void hplRedirect_Click(object sender, EventArgs e)
        {
            var masterRowHandle = gridLogs.FocusedRowHandle;
            var rowHandle = (gcLogs.FocusedView as ColumnView).FocusedRowHandle;
            var row = gridLogs.GetDetailView(masterRowHandle, 0) as GridView;
            var detailRow = (AccountUsers)row.GetRow(rowHandle);

            _ucManager.ShowUCSystemDetails(hplRedirect.Name, new UCMOAccountUserRequests()
            {
                Dock = DockStyle.Fill,
                filterText = detailRow.MOAccountUser.Id.ToString()
            }, new string[] {"filterText"});
        }

        private void btnPreview_Click(object sender, EventArgs e)
        {
            IUnitOfWork unitOfWork = new UnitOfWork();
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
                DatePrinted = DateTime.Now,
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

        private void btnExpandAll_Click(object sender, EventArgs e)
        {
            for (var i = 0; i < gridLogs.RowCount; i++)
            {
                var focusedRow = gridLogs.GetRowHandle(i);
                gridLogs.SetMasterRowExpanded(focusedRow, !gridLogs.GetMasterRowExpanded(focusedRow));
            }
        }

        public void DisposeUC(Control parent)
        {
            foreach (Control ctrl in parent.Controls)
            {
                ctrl.Dispose();
                GC.Collect();
            }
            parent.Controls.Clear();
        }
    }
}
