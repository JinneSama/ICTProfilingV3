using DevExpress.Charts.Native;
using DevExpress.Data.Filtering;
using DevExpress.XtraEditors;
using ICTProfilingV3.ActionsForms;
using ICTProfilingV3.Equipments;
using ICTProfilingV3.StandardPRForms;
using ICTProfilingV3.TechSpecsForms;
using Models.HRMISEntites;
using Models.Models;
using Models.Repository;
using Models.ViewModels;
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
        private readonly IUnitOfWork unitOfWork;
        public string filterText { get; set; }
        public UCPR()
        {
            InitializeComponent();
            unitOfWork = new UnitOfWork();
            LoadPR();
        }

        private void LoadPR()
        {
            var pr = unitOfWork.PurchaseRequestRepo.GetAll(x => x.CreatedByUser,
                x => x.TechSpecs).ToList()
                .Select(x => new PRViewModel
                {
                    PurchaseRequest = x,
                    Office = HRMISEmployees.GetEmployeeById(x.ReqById)?.Office,
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
            if (row.PurchaseRequest.TechSpecsId == null) LoadPRSpecs(row);
            else await LoadTSSpecs(row);
        }

        private async Task LoadTSSpecs(PRViewModel row)
        {
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
            tabAction.Controls.Add(new UCActions(new ActionType { Id = row.PurchaseRequest.Id, RequestType = Models.Enums.RequestType.PR })
            {
                Dock = System.Windows.Forms.DockStyle.Fill
            });
        }

        private void LoadPRSpecs(PRViewModel row)
        {
            panelSpecs.Controls.Clear();
            panelSpecs.Controls.Add(new UCStandardPR(unitOfWork, row.PurchaseRequest)
            {
                Dock = DockStyle.Fill
            });
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            var row = (PRViewModel)gridPR.GetFocusedRow();
            if (row.PurchaseRequest.TechSpecs == null)
            {
                var frm = new frmAddEditStandardPR(unitOfWork, row.PurchaseRequest);
                frm.ShowDialog();
            }
            else
            {
                var frm = new frmEditPR(row.PurchaseRequest,unitOfWork);
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
        }

        private void UCPR_Load(object sender, EventArgs e)
        {
            if(filterText != null) gridPR.ActiveFilterCriteria = new BinaryOperator("PurchaseRequest.Id",filterText);
        }

        private void hplTechSpecs_Click(object sender, EventArgs e)
        {
            var pr = (PRViewModel)gridPR.GetFocusedRow();
            var main = Application.OpenForms["frmMain"] as frmMain;
            main.mainPanel.Controls.Clear();

            main.mainPanel.Controls.Add(new UCTechSpecs()
            {
                IsTechSpecs = true,
                Dock = DockStyle.Fill,
                filterText = pr.PurchaseRequest.TechSpecsId.ToString()
            });
        }
    }
}
