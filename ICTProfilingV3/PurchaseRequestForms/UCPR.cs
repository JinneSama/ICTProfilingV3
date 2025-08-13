using DevExpress.Charts.Native;
using DevExpress.Data.Filtering;
using DevExpress.XtraEditors;
using Helpers.Interfaces;
using Helpers.Utility;
using ICTProfilingV3.ActionsForms;
using ICTProfilingV3.DataTransferModels.Models;
using ICTProfilingV3.DataTransferModels.ViewModels;
using ICTProfilingV3.Equipments;
using ICTProfilingV3.Interfaces;
using ICTProfilingV3.RepairForms;
using ICTProfilingV3.Services.Employees;
using ICTProfilingV3.StandardPRForms;
using ICTProfilingV3.TechSpecsForms;
using Microsoft.Extensions.DependencyInjection;
using Models.Models;
using Models.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ICTProfilingV3.PurchaseRequestForms
{
    public partial class UCPR : DevExpress.XtraEditors.XtraUserControl
    {
        private readonly IUCManager _ucManager;
        private readonly IServiceProvider _serviceProvider;
        public string filterText { get; set; }
        public UCPR(IUCManager ucManager, IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
            _ucManager = ucManager;
            LoadPR();
        }

        private void LoadPR()
        {
            IUnitOfWork unitOfWork = new UnitOfWork();
            var pr = unitOfWork.PurchaseRequestRepo.GetAll(x => x.CreatedByUser,
                x => x.TechSpecs).OrderByDescending(x => x.DateCreated).ToList()
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

            if(!string.IsNullOrWhiteSpace(row.PurchaseRequest.PRNo))
            {
                LoadOFMISPR(row.PurchaseRequest.PRNo);
                return;
            }
            if (row.PurchaseRequest.TechSpecsId == null) LoadPRSpecs(row);
            else await LoadTSSpecs(row);
        }

        private void LoadOFMISPR(string pRNo)
        {
            panelSpecs.Controls.Clear();
            panelSpecs.Controls.Add(new UCOFMISPR(pRNo)
            {
                Dock = DockStyle.Fill
            });
        }

        private async Task LoadTSSpecs(PRViewModel row)
        {
            IUnitOfWork unitOfWork = new UnitOfWork();
            var ts = await unitOfWork.TechSpecsRepo.FindAsync(x => x.Id == row.PurchaseRequest.TechSpecs.Id);
            panelSpecs.Controls.Clear();
            panelSpecs.Controls.Add(new UCRequestedTechSpecs(ts)
            {
                Dock = DockStyle.Fill
            });
        }
        private void LoadActions()
        {
            var row = (PRViewModel)gridPR.GetFocusedRow();
            if (row == null) return;
            tabAction.Controls.Clear();
            var uc = _serviceProvider.GetRequiredService<UCActions>();
            uc.setActions(new ActionType { Id = row.PurchaseRequest.Id, RequestType = Models.Enums.RequestType.PR });
            uc.Dock = System.Windows.Forms.DockStyle.Fill;
            tabAction.Controls.Add(uc);
        }

        private void LoadPRSpecs(PRViewModel row)
        {
            panelSpecs.Controls.Clear();
            panelSpecs.Controls.Add(new UCStandardPR(row.PurchaseRequest)
            {
                Dock = DockStyle.Fill
            });
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            var row = (PRViewModel)gridPR.GetFocusedRow();
            if (row.PurchaseRequest.TechSpecs == null)
            {
                var frm = new frmAddEditStandardPR( row.PurchaseRequest);
                frm.ShowDialog();
            }
            else
            {
                var frm = new frmEditPR(row.PurchaseRequest);
                frm.ShowDialog();
            }

            LoadPR();
        }

        private void btnAddStandardPR_Click(object sender, EventArgs e)
        {
            var frm = new frmAddEditStandardPR();
            frm.ShowDialog();

            LoadPR();
        }

        private void gridPR_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            LoadPRDetails();
            LoadSpecs();
            LoadActions();
            LoadFDTSData();
        }

        private void LoadFDTSData()
        {
            var row = (PRViewModel)gridPR.GetFocusedRow();
            if (row == null) return;

            fpPanelFDTS.Controls.Clear();
            fpPanelFDTS.Controls.Add(new UCFDTS(row.PurchaseRequest.PRNo)
            {
                Dock = DockStyle.Fill
            });
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

        private void btnFDTS_Click(object sender, EventArgs e)
        {
            if (fpFDTS.IsPopupOpen) fpFDTS.HidePopup();
            else fpFDTS.ShowPopup();
        }
    }
}
