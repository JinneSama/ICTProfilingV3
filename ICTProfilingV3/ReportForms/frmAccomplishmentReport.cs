using ICTProfilingV3.BaseClasses;
using ICTProfilingV3.Core.Common;
using ICTProfilingV3.DataTransferModels.ReportViewModel;
using ICTProfilingV3.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ICTProfilingV3.ReportForms
{
    public partial class frmAccomplishmentReport : BaseForm
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IDocActionsService _actionsService;
        private readonly IICTUserManager _userManager;
        private readonly UserStore _userStore;
        public frmAccomplishmentReport(IDocActionsService actionsService, IICTUserManager userManager, 
            IServiceProvider serviceProvider, UserStore userStore)
        {
            InitializeComponent();
            _userManager = userManager;
            _actionsService = actionsService;
            _serviceProvider = serviceProvider;
            _userStore = userStore;
        }

        private void LoadDropdowns()
        {
            var users = _userManager.GetUsers();
            slueStaff.Properties.DataSource = users;
            sluePreparedBy.Properties.DataSource = users;
            slueReviewedBy.Properties.DataSource = users;
            slueApprovedBy.Properties.DataSource = users;
        }

        private async void btnPreview_Click(object sender, EventArgs e)
        {
            await PreviewReport();
        }

        private async Task PreviewReport()
        {
            string StaffId = (string)slueStaff.EditValue;
            DateTime dateFrom = deDateFrom.DateTime;
            DateTime dateTo = deDateTo.DateTime;
            var actions = _actionsService.GetActionReport(StaffId, dateFrom, dateTo);
            var data = new AccomplishmentReportViewModel
            {
                AccomplishmentPeriod = dateFrom.ToShortDateString() + " - " + dateTo.ToShortDateString(),
                PrintedBy = _userStore.Username,
                Staff = await _userManager.FindUserAsync((string)slueStaff.EditValue),
                StartDate = dateFrom,
                EndDate = dateTo,
                PreparedBy = await _userManager.FindUserAsync((string)sluePreparedBy.EditValue),
                ReviewedBy = await _userManager.FindUserAsync((string)slueReviewedBy.EditValue),
                ApprovedBy = await _userManager.FindUserAsync((string)slueApprovedBy.EditValue),
                ActionReport = actions,
                AOPosition = txtReviewedByPos.Text,
                ApprovedPosition = txtApprovedByPos.Text
            };

            var rpt = new rptAccomplishment
            {
                DataSource = new List<AccomplishmentReportViewModel> { data }
            };

            var frm = _serviceProvider.GetRequiredService<frmReportViewer>();
            frm.InitForm(rpt);
            frm.ShowDialog();
        }

        private void frmAccomplishmentReport_Load(object sender, EventArgs e)
        {
            LoadDropdowns();
            sluePreparedBy.EditValue = _userStore.UserId;
            slueStaff.EditValue = _userStore.UserId;
        }

        private async void sluePreparedBy_EditValueChanged(object sender, EventArgs e)
        {
            var row = (Users)sluePreparedBy.Properties.View.GetFocusedRow();
            if (row == null)
            {
                var usr = await _userManager.FindUserAsync(_userStore.UserId);
                txtPreparedByPos.Text = usr.Position;
            }else txtPreparedByPos.Text = row.Position;
        }

        private void slueReviewedBy_EditValueChanged(object sender, EventArgs e)
        {
            var row = (Users)slueReviewedBy.Properties.View.GetFocusedRow();
            txtReviewedByPos.Text = row.Position;
        }

        private void slueApprovedBy_EditValueChanged(object sender, EventArgs e)
        {
            var row = (Users)slueApprovedBy.Properties.View.GetFocusedRow();
            txtApprovedByPos.Text = row.Position;
        }
    }
}