using DevExpress.Data.Filtering;
using ICTProfilingV3.ActionsForms;
using ICTProfilingV3.Core.Common;
using ICTProfilingV3.DataTransferModels.Models;
using ICTProfilingV3.DataTransferModels.ViewModels;
using ICTProfilingV3.Interfaces;
using ICTProfilingV3.Services.Employees;
using ICTProfilingV3.TechSpecsForms;
using Microsoft.Extensions.DependencyInjection;
using Models.Enums;
using System;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ICTProfilingV3.PurchaseRequestForms
{
    public partial class UCPR : DevExpress.XtraEditors.XtraUserControl
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IPurchaseReqService _purchaseReqService;
        private readonly ITechSpecsService _techSpecsService;
        private readonly IICTRoleManager _roleManager;
        private readonly UserStore _userStore;

        public string filterText { get; set; }
        public UCPR(IUCManager ucManager, IServiceProvider serviceProvider, IPurchaseReqService purchaseReqService,
            IICTRoleManager roleManager, UserStore userStore, ITechSpecsService techSpecsService)
        {
            _serviceProvider = serviceProvider;
            _purchaseReqService = purchaseReqService;
            _roleManager = roleManager;
            _userStore = userStore;
            _techSpecsService = techSpecsService;
            InitializeComponent();
            LoadPR();
        }

        private void LoadPR()
        {
            var pr = _purchaseReqService.GetAll()
                .Include(x => x.CreatedByUser)
                .Include(x => x.TechSpecs)
                .OrderByDescending(x => x.DateCreated)
                .ToList()
                .Select(x => new PRViewModel
                {
                    PurchaseRequest = x,
                    Office = HRMISEmployees.GetEmployeeById(x.ChiefId)?.Office,
                    CreatedBy = x.CreatedByUser?.UserName
                });
            gcPR.DataSource = new BindingList<PRViewModel>(pr.ToList());
        }

        private void LoadPRDetails()
        {
            var row = (PRViewModel)gridPR.GetFocusedRow();
            if (row == null) return;
            var chief = HRMISEmployees.GetEmployeeById(row.PurchaseRequest.ChiefId);

            txtPRNo.Text = row.PurchaseRequest.PRNo;
            txtDate.DateTime = (DateTime)(row.PurchaseRequest.DateCreated ?? DateTime.MinValue);
            lblEpisNo.Text = row.PurchaseRequest.Id.ToString();
            lblPRNo.Text = row.PurchaseRequest.PRNo?.ToString();

            txtRequestingOfficeChief.Text = chief?.Employee ?? "";
            txtRequestingOfficeChiefPos.Text = chief?.Office ?? "";
            txtRequestedByOffice.Text = chief?.Office ?? "";
            txtRequestedByDivision.Text = chief?.Division ?? "";
        }

        private async void LoadSpecs()
        {
            var row = (PRViewModel)gridPR.GetFocusedRow();
            if (row == null) return;

            if(row.PurchaseRequest.TechSpecsId == null)
            {
                LoadOFMISPR(row.PurchaseRequest.PRNo);
                return;
            }
            else await LoadTSSpecs(row);
        }

        private void LoadOFMISPR(string pRNo)
        {
            var navigation = _serviceProvider.GetRequiredService<IControlNavigator<UCOFMISPR>>();
            navigation.NavigateTo(panelSpecs, act => act.InitUC(pRNo));
        }

        private async Task LoadTSSpecs(PRViewModel row)
        {
            var ts = await _techSpecsService.GetByIdAsync(row.PurchaseRequest.TechSpecs.Id);
            
            var navigation = _serviceProvider.GetRequiredService<IControlNavigator<UCRequestedTechSpecs>>();
            navigation.NavigateTo(panelSpecs, act => act.InitUC(ts));
        }
        private void LoadActions()
        {
            var row = (PRViewModel)gridPR.GetFocusedRow();
            if (row == null) return;
            var navigation = _serviceProvider.GetRequiredService<IControlNavigator<UCActions>>();
            navigation.NavigateTo(tabAction, act => act.setActions(
                new ActionType 
                { 
                    Id = row.PurchaseRequest.Id, 
                    RequestType = Models.Enums.RequestType.PR }
                )
            );
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            var row = (PRViewModel)gridPR.GetFocusedRow();

            var frm = _serviceProvider.GetRequiredService<frmEditPR>();
            frm.InitForm(row.PurchaseRequest);
            frm.ShowDialog();

            LoadPR();
        }

        private async void gridPR_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            LoadPRDetails();
            LoadSpecs();
            LoadActions();
            await LoadFDTSDetails();
        }

        private void UCPR_Load(object sender, EventArgs e)
        {
            if(filterText != null) gridPR.ActiveFilterCriteria = new BinaryOperator("PurchaseRequest.Id",filterText);
        }

        private void hplTechSpecs_Click(object sender, EventArgs e)
        {
            var pr = (PRViewModel)gridPR.GetFocusedRow();

            var mainForm = _serviceProvider.GetRequiredService<frmMain>();
            var navigation = _serviceProvider.GetRequiredService<IControlNavigator<UCTechSpecs>>();
            navigation.NavigateTo(mainForm.mainPanel, act => act.filterText = pr.PurchaseRequest.TechSpecsId.ToString());
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            var res = await _roleManager.HasDesignation(Designation.PRAdmin, _userStore.UserRole);
            if (!res)
            {
                MessageBox.Show("You don't have permission to delete Purchase Requests.", "Permission Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Delete this Request?", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) == DialogResult.Cancel) return;

            var pr = (PRViewModel)gridPR.GetFocusedRow();
            await _purchaseReqService.DeleteAsync(pr.PurchaseRequest.Id);
        }

        private async Task LoadFDTSDetails()
        {
            var row = (PRViewModel)gridPR.GetFocusedRow();
            if (row == null) return;

            var data = await FDTSData.GetData(row.PurchaseRequest.PRNo);
            if (data == null) return;

            txtDate.Text = data.Date.Value.ToShortDateString();
            txtPRDesc.Text = data.PRDescription;
            txtTotalAmount.Value = data.TotalAmount.Value;
            txtPurpose.Text = data.Purpose;
            txtBudgetPR.Text = data.BudgetPRNo;

            gcAction.DataSource = data.DocumentActions;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var row = (PRViewModel)gridPR.GetFocusedRow();

            var frm = _serviceProvider.GetRequiredService<frmEditPR>();
            frm.InitForm(row.PurchaseRequest);
            frm.ShowDialog();

            LoadPR();
        }
    }
}
