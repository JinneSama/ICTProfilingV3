using ICTProfilingV3.BaseClasses;
using ICTProfilingV3.Core.Common;
using ICTProfilingV3.DataTransferModels.ReportViewModel;
using ICTProfilingV3.DataTransferModels.ViewModels;
using ICTProfilingV3.Interfaces;
using ICTProfilingV3.ReportForms;
using ICTProfilingV3.Services.ApiUsers;
using Models.Entities;
using Models.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ICTProfilingV3.PGNForms
{
    public partial class frmSelectNotee : BaseForm
    {
        private IICTUserManager usermanager;
        private PGNRequestViewModel request;
        private IUnitOfWork unitOfWork;
        private readonly UserStore _userStore;

        public frmSelectNotee(UserStore userStore)
        {
            _userStore = userStore;
            InitializeComponent();
            usermanager = new ICTUserManager();
            unitOfWork = new UnitOfWork();
            LoadDropdowns();
        }

        public void InitForm(PGNRequestViewModel request)
        {
            this.request = request;
        }

        private void LoadDropdowns()
        {
            var users = usermanager.GetUsers();
            slueNotedBy.Properties.DataSource = users;
        }

        private async void btnPreview_Click(object sender, EventArgs e)
        {
            var row = (Users)slueNotedBy.Properties.View.GetFocusedRow();
            txtPosition.Text = row.Position;

            var res = unitOfWork.PGNAccountsRepo.FindAllAsync(x => x.PGNRequestId == request.PGNRequest.Id, x => x.PGNGroupOffices,
                x => x.PGNNonEmployee).ToList().Select(x => new PGNAccountsViewModel
                {
                    PGNAccount = x
                }).ToList();

            var pgnReport = new PGNReportViewModel
            {
                ReqNo = request.ReqNo,
                Office = request.Employee.Office + " " + request.Employee.Division,
                PrintedBy = _userStore.Username,
                DatePrinted = DateTime.Now.ToShortDateString(),
                PreparedBy = await usermanager.FindUserAsync(_userStore.UserId),
                NotedBy = row,
                PGNAccounts = res
            };

            var rpt = new rptPGN
            {
                DataSource = new List<PGNReportViewModel>() { pgnReport }
            };

            var reportViewer = new frmReportViewer(rpt);
            reportViewer.ShowDialog();
        }

        private void slueNotedBy_EditValueChanged(object sender, EventArgs e)
        {
            var row = (Users)slueNotedBy.Properties.View.GetFocusedRow();
            txtPosition.Text = row.Position;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}