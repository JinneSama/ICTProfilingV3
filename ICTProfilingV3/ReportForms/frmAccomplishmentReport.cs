using ICTProfilingV3.BaseClasses;
using ICTProfilingV3.Core.Common;
using ICTProfilingV3.DataTransferModels.ReportViewModel;
using ICTProfilingV3.Interfaces;
using ICTProfilingV3.Services.ApiUsers;
using Models.Entities;
using Models.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ICTProfilingV3.ReportForms
{
    public partial class frmAccomplishmentReport : BaseForm
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IICTUserManager userManager;
        private readonly UserStore _userStore;
        public frmAccomplishmentReport(UserStore userStore)
        {
            InitializeComponent();
            unitOfWork = new UnitOfWork();
            userManager = new ICTUserManager();
            _userStore = userStore;
        }

        private void LoadDropdowns()
        {
            var users = userManager.GetUsers();
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

            var actions = unitOfWork.ActionsRepo.FindAllAsync(x => x.CreatedById == StaffId &&
                (x.ActionDate >= dateFrom && x.ActionDate <= dateTo) && x.SubActivityId != null)
                .GroupBy(g => g.SubActivityId)
                .Select(s => new ActionReport
                {
                    MainActivity = s.FirstOrDefault().MainActDropdowns.Value,
                    SubActivity = s.FirstOrDefault().SubActivityDropdowns.Value,
                    Count = s.Count(),
                    SubActivityId = (int)s.Key
                }).ToList();

            var data = new AccomplishmentReportViewModel
            {
                AccomplishmentPeriod = dateFrom.ToShortDateString() + " - " + dateTo.ToShortDateString(),
                PrintedBy = _userStore.Username,
                Staff = await userManager.FindUserAsync((string)slueStaff.EditValue),
                StartDate = dateFrom,
                EndDate = dateTo,
                PreparedBy = await userManager.FindUserAsync((string)sluePreparedBy.EditValue),
                ReviewedBy = await userManager.FindUserAsync((string)slueReviewedBy.EditValue),
                ApprovedBy = await userManager.FindUserAsync((string)slueApprovedBy.EditValue),
                ActionReport = actions,
                AOPosition = txtReviewedByPos.Text,
                ApprovedPosition = txtApprovedByPos.Text
            };

            var rpt = new rptAccomplishment
            {
                DataSource = new List<AccomplishmentReportViewModel> { data }
            };

            var frm = new frmReportViewer(rpt);
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
                var usr = await unitOfWork.UsersRepo.FindAsync(x => x.Id == _userStore.UserId);
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