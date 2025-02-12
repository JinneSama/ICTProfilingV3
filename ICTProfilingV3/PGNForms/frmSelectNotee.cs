using EntityManager.Managers.User;
using ICTProfilingV3.BaseClasses;
using ICTProfilingV3.ReportForms;
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
using System.Security.Principal;

namespace ICTProfilingV3.PGNForms
{
    public partial class frmSelectNotee : BaseForm
    {
        private IICTUserManager usermanager;
        private readonly PGNRequestViewModel request;
        private IUnitOfWork unitOfWork;

        public frmSelectNotee(PGNRequestViewModel request)
        {
            InitializeComponent();
            usermanager = new ICTUserManager();
            unitOfWork = new UnitOfWork();
            LoadDropdowns();
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
                PrintedBy = UserStore.Username,
                DatePrinted = DateTime.Now.ToShortDateString(),
                PreparedBy = await usermanager.FindUserAsync(UserStore.UserId),
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