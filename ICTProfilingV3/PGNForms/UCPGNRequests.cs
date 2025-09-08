using DevExpress.Data.Filtering;
using ICTProfilingV3.ActionsForms;
using ICTProfilingV3.DataTransferModels.Models;
using ICTProfilingV3.DataTransferModels.ViewModels;
using ICTProfilingV3.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Models.Enums;
using System;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Windows.Forms;

namespace ICTProfilingV3.PGNForms
{
    public partial class UCPGNRequests : DevExpress.XtraEditors.XtraUserControl
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IPGNService _pgnService;
        public string filterText { get; set; }
        public UCPGNRequests(IServiceProvider serviceProvider, IPGNService pgnService)
        {
            _serviceProvider = serviceProvider;
            _pgnService = pgnService;
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            var req = _pgnService.PGNRequestsService.GetAll()
                .Include(x => x.CreatedByUser)
                .OrderByDescending(x => x.DateCreated)
                .ToList()
                .Select(x => new PGNRequestViewModel
                {
                    PGNRequest = x
                });

            gcPGNRequest.DataSource = new BindingList<PGNRequestViewModel>(req.ToList());
        }

        private void LoadDetails()
        {
            var row = (PGNRequestViewModel)gridPGNRequest.GetFocusedRow();
            if (row == null) return;

            if(row.PGNRequest?.RequestDate != null) txtDate.DateTime = (DateTime)(row.PGNRequest?.RequestDate);
            txtCommType.Text = row.PGNRequest?.CommunicationType.ToString();
            txtSignatory.Text = row.Employee?.Employee;
            txtPosition.Text = row.Employee?.Position;
            txtOffice.Text = row.Employee?.Office + " " + row.Employee?.Division;
            txtUsername.Text = row.Employee?.Username;
            txtSubject.Text = row.PGNRequest?.Subject;
            txtCreatedBy.Text = row.PGNRequest?.CreatedByUser?.FullName;
            lblEpisNo.Text = row.ReqNo;

            spbTicketStatus.SelectedItemIndex = ((int)(row.PGNRequest?.Status ?? TicketStatus.Assigned)) - 1;
        }

        private void LoadActions()
        {
            var row = (PGNRequestViewModel)gridPGNRequest.GetFocusedRow();

            if (row == null) return;
            var navigation = _serviceProvider.GetRequiredService<IControlNavigator<UCActions>>();
            navigation.NavigateTo(tabAction, act => act.setActions(new ActionType
            {
                Id = row.PGNRequest.Id,
                RequestType = RequestType.PGN
            }));
        }

        private void LoadRequestAccounts()
        {
            var row = (PGNRequestViewModel)gridPGNRequest.GetFocusedRow();

            if (row == null) return;
            var navigation = _serviceProvider.GetRequiredService<IControlNavigator<UCRequestAccount>>();
            navigation.NavigateTo(tabAccounts, act => act.InitUC(row.PGNRequest));
        }

        private async void btnNewRequest_Click(object sender, System.EventArgs e)
        {
            var frm = _serviceProvider.GetRequiredService<frmAddEditRequest>();
            await frm.InitForm();
            frm.ShowDialog();

            LoadData();
        }

        private void gridPGNRequest_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            LoadActions();
            LoadDetails();
            LoadScanDocs();
            LoadRequestAccounts();
        }
        private void LoadScanDocs()
        {
            var row = (PGNRequestViewModel)gridPGNRequest.GetFocusedRow();
            gcScanDocs.Controls.Clear();
            if(row == null) return;

            var navigation = _serviceProvider.GetRequiredService<IControlNavigator<UCPGNScanDocuments>>();
            navigation.NavigateTo(gcScanDocs, act => act.InitUC(row.PGNRequest));
        }

        private void UCPGNRequests_Load(object sender, System.EventArgs e)
        {
            if (filterText != null) gridPGNRequest.ActiveFilterCriteria = new BinaryOperator("PGNRequest.Id", filterText);
        }

        private async void btnEditRequest_Click(object sender, EventArgs e)
        {
            var row = (PGNRequestViewModel)gridPGNRequest.GetFocusedRow();
            if (row == null) return;

            var frm = _serviceProvider.GetRequiredService<frmAddEditRequest>();
            await frm.InitForm(row);
            frm.ShowDialog();

            LoadData();
        }

        private void btnPreview_Click(object sender, EventArgs e)
        {
            var row = (PGNRequestViewModel)gridPGNRequest.GetFocusedRow();
            if (row == null) return;

            var frm = _serviceProvider.GetRequiredService<frmSelectNotee>();
            frm.InitForm(row);
            frm.ShowDialog();
        }
    }
}
