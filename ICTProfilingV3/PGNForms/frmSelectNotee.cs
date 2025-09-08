using ICTProfilingV3.BaseClasses;
using ICTProfilingV3.Core.Common;
using ICTProfilingV3.DataTransferModels.ReportViewModel;
using ICTProfilingV3.DataTransferModels.ViewModels;
using ICTProfilingV3.Interfaces;
using ICTProfilingV3.ReportForms;
using Microsoft.Extensions.DependencyInjection;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ICTProfilingV3.PGNForms
{
    public partial class frmSelectNotee : BaseForm
    {
        private readonly IPGNService _pgnService;
        private readonly IICTUserManager _userManager;
        private readonly IServiceProvider _serviceProvider;
        private readonly UserStore _userStore;

        private PGNRequestViewModel _request;

        public frmSelectNotee(UserStore userStore, IICTUserManager userManager, IServiceProvider serviceProvider, IPGNService pgnService)
        {
            _userStore = userStore;
            _userManager = userManager;
            _serviceProvider = serviceProvider;
            _pgnService = pgnService;

            InitializeComponent();
            LoadDropdowns();
        }

        public void InitForm(PGNRequestViewModel request)
        {
            _request = request;
        }

        private void LoadDropdowns()
        {
            var users = _userManager.GetUsers();
            slueNotedBy.Properties.DataSource = users;
        }

        private async void btnPreview_Click(object sender, EventArgs e)
        {
            var row = (Users)slueNotedBy.Properties.View.GetFocusedRow();
            txtPosition.Text = row.Position;

            var res = _pgnService.GetAll().Where(x => x.PGNRequestId == _request.PGNRequest.Id)
                .Include(x => x.PGNGroupOffices)
                .Include(x => x.PGNNonEmployee)
                .ToList()
                .Select(x => new PGNAccountsViewModel
                {
                    PGNAccount = x
                })
                .ToList();

            var pgnReport = new PGNReportViewModel
            {
                ReqNo = _request.ReqNo,
                Office = _request.Employee.Office + " " + _request.Employee.Division,
                PrintedBy = _userStore.Username,
                DatePrinted = DateTime.Now.ToShortDateString(),
                PreparedBy = await _userManager.FindUserAsync(_userStore.UserId),
                NotedBy = row,
                PGNAccounts = res
            };

            var rpt = new rptPGN
            {
                DataSource = new List<PGNReportViewModel>() { pgnReport }
            };

            var reportViewer = _serviceProvider.GetRequiredService<frmReportViewer>();
            reportViewer.InitForm(rpt);
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