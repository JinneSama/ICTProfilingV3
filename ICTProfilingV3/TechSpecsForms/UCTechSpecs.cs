using DevExpress.Data.Filtering;
using DevExpress.Xpo.DB;
using EntityManager.Managers;
using EntityManager.Managers.User;
using ICTProfilingV3.ActionsForms;
using ICTProfilingV3.PurchaseRequestForms;
using ICTProfilingV3.TicketRequestForms;
using Models.Entities;
using Models.Enums;
using Models.HRMISEntites;
using Models.Managers.User;
using Models.Models;
using Models.Repository;
using Models.ViewModels;
using System;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ICTProfilingV3.TechSpecsForms
{
    public partial class UCTechSpecs : DevExpress.XtraEditors.XtraUserControl
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IICTUserManager userManager;
        public string filterText { get; set; }
        public UCTechSpecs()
        {
            InitializeComponent();
            unitOfWork = new UnitOfWork();
            userManager = new ICTUserManager();
        }

        private async Task LoadAll()
        {
            await LoadTechSpecs();
            LoadDropdowns();
        }
        private void LoadDropdowns()
        {
            var users = userManager.GetUsers();
            sluePreparedBy.Properties.DataSource = users;
            slueReviewedBy.Properties.DataSource = users;
            slueNotedBy.Properties.DataSource = users;
        }


        private async Task LoadDetails()
        {
            var row = (TechSpecsViewModel)gridTechSpecs.GetFocusedRow();
            if (row == null) return;
            var ts = await unitOfWork.TechSpecsRepo.FindAsync(x => x.Id == row.Id);
            txtDate.DateTime = ts.DateRequested ?? DateTime.MinValue;
            rdbtnGender.SelectedIndex = (int)ts.ReqByGender;
            txtContactNo.Text = ts.ContactNo;
            checkEditApprovedPR.Checked = ts.RequestBasedApprovedPR ?? false;
            checkEditApprovedAPP.Checked = ts.RequestBasedApprovedAPP ?? false;
            checkEditApprovedAIP.Checked = ts.RequestBasedApprovedAIP ?? false;
            checkEditApprovedPPMP.Checked = ts.RequestBasedApprovedPPMP ?? false;
            checkEditRequestLetter.Checked = ts.RequestBasedRequestLetter ?? false;
            checkEditForReplacement.Checked = ts.RequestBasedForReplacement ?? false;
            sluePreparedBy.EditValue = (string)ts.PreparedById;
            slueReviewedBy.EditValue = (string)ts.ReviewedById;
            slueNotedBy.EditValue = (string)ts.NotedById;

            var reqByUser = HRMISEmployees.GetEmployeeById(ts.ReqById);
            var chief = HRMISEmployees.GetChief(reqByUser?.Office , reqByUser?.Division);

            txtRequestingOfficeChief.Text = HRMISEmployees.GetEmployeeById(chief?.ChiefId)?.Employee;
            txtRequestingOfficeChiefPos.Text = HRMISEmployees.GetEmployeeById(chief?.ChiefId)?.Position;
            txtRequestedByEmployeeName.Text = reqByUser?.Employee;
            txtRequestedByPos.Text = reqByUser?.Position;
            txtRequestedByOffice.Text = reqByUser?.Office;
            txtRequestedByDivision.Text = reqByUser?.Division;
        }

        private async Task LoadTechSpecs()
        {
            var res = await unitOfWork.TechSpecsRepo.GetAll(x => x.TicketRequest).ToListAsync();
            var ts = res.Select(x => new TechSpecsViewModel
            {
                Id = x.Id,
                Status = x.TicketRequest.TicketStatus,
                DateAccepted = x.DateAccepted,
                DateRequested = x.DateRequested,
                Office = HRMISEmployees.GetEmployeeById(x.ReqById)?.Office,
                Division = HRMISEmployees.GetEmployeeById(x.ReqById)?.Division,
                TicketId = x.TicketRequest.Id
            });
            gcTechSpecs.DataSource = new BindingList<TechSpecsViewModel>(ts.ToList());
        }

        private async Task LoadSpecs()
        {
            var row = (TechSpecsViewModel)gridTechSpecs.GetFocusedRow();
            tabRequestedSpecs.Controls.Clear();
            if (row == null) return;

            lblTSId.Text = row.Id.ToString();
            var ts = await unitOfWork.TechSpecsRepo.FindAsync(x => x.Id == row.Id);
            tabRequestedSpecs.Controls.Add(new UCRequestedTechSpecs(ts)
            {
                Dock = System.Windows.Forms.DockStyle.Fill
            });
        }

        private void LoadActions()
        {
            var row = (TechSpecsViewModel)gridTechSpecs.GetFocusedRow();
            tabAction.Controls.Clear();
            if (row == null) return;

            tabAction.Controls.Add(new UCActions(new ActionType { Id = row.Id, RequestType = Models.Enums.RequestType.TechSpecs})
            {
                Dock = System.Windows.Forms.DockStyle.Fill
            });
        }

        private async void btnEdit_Click(object sender, System.EventArgs e)
        {
            var row = (TechSpecsViewModel)gridTechSpecs.GetFocusedRow();
            var ts = await unitOfWork.TechSpecsRepo.FindAsync(x => x.Id == row.Id);
            var frm = new frmAddEditTechSpecs(ts , unitOfWork);
            frm.ShowDialog();
        }

        private async void UCTechSpecs_Load(object sender, EventArgs e)
        {
            await LoadAll();
            if (filterText != null) gridTechSpecs.ActiveFilterCriteria = new BinaryOperator("TicketId",filterText);
        }

        private async void btnVerifyPR_Click(object sender, EventArgs e)
        {
            var row = (TechSpecsViewModel)gridTechSpecs.GetFocusedRow();
            var res = await unitOfWork.PurchaseRequestRepo.FindAsync(x => x.TechSpecsId == row.Id);

            if (res == null) await CreatePR(row);
            else await GotoPR(row);
        }

        private async Task GotoPR(TechSpecsViewModel row)
        {
            var res = await unitOfWork.TechSpecsRepo.FindAsync(x => x.Id == row.Id);
            var main = Application.OpenForms["frmMain"] as frmMain;
            main.mainPanel.Controls.Clear();

            var pr = await unitOfWork.PurchaseRequestRepo.FindAsync(x => x.TechSpecsId == row.Id);

            main.mainPanel.Controls.Add(new UCPR()
            {
                Dock = DockStyle.Fill,
                filterText = pr.Id.ToString()
            });
        }

        private async Task CreatePR(TechSpecsViewModel row)
        {
            if (MessageBox.Show("No PR Saved for this Tech Specs, Verify New PR?",
                                "Question", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel) return;

            var res = await unitOfWork.TechSpecsRepo.FindAsync(x => x.Id == row.Id);

            var pr = new PurchaseRequest
            {
                ChiefId = res.ReqByChiefId,
                TechSpecsId = res.Id,
                ReqById = res.ReqById,
                CreatedById = UserStore.UserId,
                DateCreated = DateTime.UtcNow
            };

            unitOfWork.PurchaseRequestRepo.Insert(pr);
            await unitOfWork.SaveChangesAsync();

            var main = Application.OpenForms["frmMain"] as frmMain;
            main.mainPanel.Controls.Clear();

            main.mainPanel.Controls.Add(new UCPR()
            {
                Dock = DockStyle.Fill,
                filterText = pr.Id.ToString()
            });
        }

        private async void gridTechSpecs_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            await LoadDetails();
            await LoadSpecs();
            LoadActions();
        }

        private void hplTicket_Click(object sender, EventArgs e)
        {
            var row = (TechSpecsViewModel)gridTechSpecs.GetFocusedRow();
            var main = Application.OpenForms["frmMain"] as frmMain;
            main.mainPanel.Controls.Clear();

            main.mainPanel.Controls.Add(new UCTARequestDashboard()
            {
                Dock = DockStyle.Fill,
                filterText = row.Id.ToString()
            });
        }

    }
}
