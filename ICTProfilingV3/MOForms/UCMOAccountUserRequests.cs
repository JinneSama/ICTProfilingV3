using DevExpress.Data.Filtering;
using DevExpress.XtraRichEdit.Model;
using ICTProfilingV3.ActionsForms;
using ICTProfilingV3.DataTransferModels.Models;
using ICTProfilingV3.EvaluationForms;
using ICTProfilingV3.Interfaces;
using ICTProfilingV3.Services.Employees;
using Microsoft.Extensions.DependencyInjection;
using Models.Entities;
using Models.Enums;
using System;
using System.Data.Entity;
using System.Linq;

namespace ICTProfilingV3.MOForms
{
    public partial class UCMOAccountUserRequests : DevExpress.XtraEditors.XtraUserControl
    {
        private readonly IMOService _moService;
        private readonly IServiceProvider _serviceProvider;
        public string filterText { get; set; }
        public UCMOAccountUserRequests(IServiceProvider serviceProvider, IMOService moService)
        {
            _serviceProvider = serviceProvider;
            _moService = moService;
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            var users = _moService.MOAccountUserBaseService.GetAll()
                .Include(x => x.MOAccount)
                .Include(x => x.MOAccount.Office)
                .Include(x => x.PPE);
            gcMOAccountUsers.DataSource = users.ToList();
        }

        private void LoadDetails()
        {
            var row = (MOAccountUsers)gridMOAccountUsers.GetFocusedRow();

            txtDateOfInstallation.Text = row?.DateOfInstallation.Value.ToShortDateString();
            txtProcuredDate.Text = row?.ProcuredDate.Value.ToShortDateString();
            txtDeviceNo.Text = row?.DeviceNo.ToString();
            txtPropertyNo.Text = row?.PPE?.PropertyNo;
            txtDescription.Text = row?.Description;
            txtRemarks.Text = row?.Remarks;
            
            var IssuedTo = HRMISEmployees.GetEmployeeById(row.IssuedTo);
            var User = HRMISEmployees.GetEmployeeById(row.AccountUser);

            txtIssuedTo.Text = IssuedTo.Employee;
            txtIssuedToOffice.Text = IssuedTo.Office + " " + IssuedTo.Division;
            txtUser.Text = User.Employee;
            txtUserOffice.Text = User.Office + " " + User.Division;
        }

        private void btnEdit_Click(object sender, System.EventArgs e)
        {
            var row = (MOAccountUsers)gridMOAccountUsers.GetFocusedRow();
            
            var frm = _serviceProvider.GetRequiredService<frmAddEditAccountUsers>();
            frm.InitForm(null, row);
            frm.ShowDialog();
            LoadData();
        }
        private void LoadActions()
        {
            var row = (MOAccountUsers)gridMOAccountUsers.GetFocusedRow();
            var navigation = _serviceProvider.GetRequiredService<IControlNavigator<UCActions>>();
            navigation.NavigateTo(tabAction, act => act.setActions(new ActionType { Id = row.Id, RequestType = RequestType.M365 }));
        }
        private void LoadEvaluationSheet()
        {
            var row = (MOAccountUsers)gridMOAccountUsers.GetFocusedRow();
            var navigation = _serviceProvider.GetRequiredService<IControlNavigator<UCEvaluationSheet>>();
            navigation.NavigateTo(tabEvaluation, act => act.InitForm(new ActionType { Id = row.Id, RequestType = RequestType.M365 }));
        }
        private void gridMOAccountUsers_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            var row = (MOAccountUsers)gridMOAccountUsers.GetFocusedRow();
            if (row == null) return;

            lblEpisNo.Text = "M365-" + row.Id.ToString();
            LoadDetails();
            LoadActions();
            LoadEvaluationSheet();
        }

        private void UCMOAccountUserRequests_Load(object sender, System.EventArgs e)
        { 
            ApplyFilterText();
        }

        public void ApplyFilterText()
        {
            if (filterText != null) gridMOAccountUsers.ActiveFilterCriteria = new BinaryOperator("Id", filterText);
        }
    }
}
