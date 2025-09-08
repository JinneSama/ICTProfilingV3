using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using ICTProfilingV3.Core.Common;
using ICTProfilingV3.DataTransferModels.ReportViewModel;
using ICTProfilingV3.DataTransferModels.ViewModels;
using ICTProfilingV3.Interfaces;
using ICTProfilingV3.ReportForms;
using Microsoft.Extensions.DependencyInjection;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Windows.Forms;

namespace ICTProfilingV3.MOForms
{
    public partial class UCMOAccounts : DevExpress.XtraEditors.XtraUserControl
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IMOService _moService;
        private readonly UserStore _userStore;
        public UCMOAccounts(IServiceProvider serviceProvider, UserStore userStore,
            IMOService moService)
        {
            _userStore = userStore;
            _serviceProvider = serviceProvider;
            _moService = moService;
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            var accounts = _moService.GetAll()
                .Include(x => x.Office)
                .Include(x => x.MOAccountUsers)
                .Include(x => x.MOAccountUsers.Select(s => s.PPE))
                .ToList()
                .Select(x => new MOAccountsViewModel
                {
                    MOAccount = x,
                    MOAccountUsers = new BindingList<MOAccountUsers>(x.MOAccountUsers.ToList())
                });
            gcLogs.DataSource = accounts.ToList();
        }

        private void btnAddPN_Click(object sender, EventArgs e)
        {
            var frm = _serviceProvider.GetRequiredService<frmAddEditMOAccount>();
            frm.InitForm();
            frm.ShowDialog();

            LoadData();
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Delete this Account? Users of this account will be deleted as well", 
                "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel) return;

            var row = (MOAccountsViewModel)gridLogs.GetFocusedRow();
            await _moService.MOAccountUserBaseService.DeleteRangeAsync(x => x.MOAccountId == row.MOAccount.Id);
            await _moService.DeleteRangeAsync(x => x.Id == row.MOAccount.Id);
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            var row = (MOAccountsViewModel)gridLogs.GetFocusedRow();
            var frm = _serviceProvider.GetRequiredService<frmAddEditMOAccount>();
            frm.InitForm(row.MOAccount);
            frm.ShowDialog();

            LoadData();
        }

        private void btnAddAccount_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Create new User for this Account? this will create a User Request" ,
                "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel) return;

            var row = (MOAccountsViewModel)gridLogs.GetFocusedRow();
            var frm = _serviceProvider.GetRequiredService<frmAddEditAccountUsers>();
            frm.InitForm(row.MOAccount, null);
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
            var mainForm = _serviceProvider.GetRequiredService<frmMain>();
            var navigation = _serviceProvider.GetRequiredService<IControlNavigator<UCMOAccountUserRequests>>();
            navigation.NavigateTo(mainForm.mainPanel);
        }

        private void hplRedirect_Click(object sender, EventArgs e)
        {
            var masterRowHandle = gridLogs.FocusedRowHandle;
            var rowHandle = (gcLogs.FocusedView as ColumnView).FocusedRowHandle;
            var row = gridLogs.GetDetailView(masterRowHandle, 0) as GridView;
            var detailRow = (AccountUsers)row.GetRow(rowHandle);

            var mainForm = _serviceProvider.GetRequiredService<frmMain>();
            var navigation = _serviceProvider.GetRequiredService<IControlNavigator<UCMOAccountUserRequests>>();
            navigation.NavigateTo(mainForm.mainPanel, act => act.filterText = detailRow.MOAccountUser.Id.ToString());
        }

        private void btnPreview_Click(object sender, EventArgs e)
        {
            var accounts = _moService.GetAll()
                .Include(x => x.Office)
                .Include(x => x.MOAccountUsers)
                .Include(x => x.MOAccountUsers.Select(s => s.PPE))
                .ToList()
                .Select(x => new MOAccountsViewModel
                {
                    MOAccount = x,
                    MOAccountUsers = new BindingList<MOAccountUsers>(x.MOAccountUsers.ToList())
                });

            var rptAccounts = new MOAccountReportViewModel
            {
                MOAccountsViewModel = accounts,
                DatePrinted = DateTime.Now,
                PrintedBy = _userStore.Username
            };

            var rptM365 = new rptM365
            {
                DataSource = new List<MOAccountReportViewModel> { rptAccounts }
            };

            rptM365.CreateDocument();
            var frm = _serviceProvider.GetRequiredService<frmReportViewer>();
            frm.InitForm(rptM365);
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
    }
}
