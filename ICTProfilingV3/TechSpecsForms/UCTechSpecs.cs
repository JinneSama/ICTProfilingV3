using DevExpress.Data.Filtering;
using EntityManager.Managers.User;
using Helpers.NetworkFolder;
using ICTProfilingV3.ActionsForms;
using ICTProfilingV3.PurchaseRequestForms;
using ICTProfilingV3.RepairForms;
using ICTProfilingV3.ReportForms;
using ICTProfilingV3.TicketRequestForms;
using ICTProfilingV3.ToolForms;
using Models.Entities;
using Models.Enums;
using Models.HRMISEntites;
using Models.Managers.User;
using Models.Models;
using Models.ReportViewModel;
using Models.Repository;
using Models.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ICTProfilingV3.TechSpecsForms
{
    public partial class UCTechSpecs : DevExpress.XtraEditors.XtraUserControl
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IICTUserManager userManager;
        private HTTPNetworkFolder networkFolder;
        public string filterText { get; set; }
        public bool IsTechSpecs { get; set; }
        public UCTechSpecs()
        {
            InitializeComponent();
            networkFolder = new HTTPNetworkFolder();
            unitOfWork = new UnitOfWork();
            userManager = new ICTUserManager();
        }

        private void LoadAll()
        {
            LoadTechSpecs();
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
            if (!IsTechSpecs)
            {
                colRepair.Visible = true;
                colTicket.Visible = false;
            }
            else
            {
                colRepair.Visible = false;
                colTicket.Visible = true;
            }
            var ts = await unitOfWork.TechSpecsRepo.FindAsync(x => x.Id == row.Id);
            if(ts == null) return;
            spbTicketStatus.SelectedItemIndex = ((int)ts.TicketRequest.TicketStatus) - 1;
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

        private void LoadTechSpecs()
        {
            var res = unitOfWork.TechSpecsRepo.GetAll(x => x.TicketRequest, x => x.Repairs).ToList();
            if(IsTechSpecs) res = res.Where(x => x.TicketRequest.IsRepairTechSpecs != true).ToList();
            else res = res.Where(x => x.TicketRequest.IsRepairTechSpecs == true).ToList();
            var ts = res.Select(x => new TechSpecsViewModel
            {
                Id = x.Id,
                Status = x.TicketRequest.TicketStatus,
                DateAccepted = x.DateAccepted,
                DateRequested = x.DateRequested,
                Office = HRMISEmployees.GetEmployeeById(x.ReqById)?.Office,
                Division = HRMISEmployees.GetEmployeeById(x.ReqById)?.Division,
                TicketId = x.TicketRequest.Id,
                RepairId = x.Repairs.Count == 0 ? 0 : x.Repairs.FirstOrDefault().Id,
                TechSpecs = x
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
        private async void LoadStaff()
        {
            var row = (TechSpecsViewModel)gridTechSpecs.GetFocusedRow();
            var staff = await unitOfWork.ITStaffRepo.FindAsync(x => x.Id == row.TechSpecs.TicketRequest.StaffId, x => x.Users);
            Image img = await networkFolder.DownloadFile(staff.UserId + ".jpeg");
            var res = new StaffModel
            {
                Image = img,
                AssignedTo = row.Status == TicketStatus.Accepted ? "Not Yet Assigned!" : staff.Users.UserName,
                FullName = img == null ? (staff == null ? "N / A" : staff.Users.FullName) : "",
                PhotoVisible = img == null ? true : false,
                InitialsVisible = img == null ? false : true
            };

            staffPanel.Controls.Clear();
            staffPanel.Controls.Add(new UCAssignedTo(res)
            {
                Dock = DockStyle.Fill
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

        private void UCTechSpecs_Load(object sender, EventArgs e)
        {
            LoadAll();
            if (filterText != null) gridTechSpecs.ActiveFilterCriteria = new BinaryOperator("TicketId",filterText);
            if(!IsTechSpecs)
                lblTechSpecs.Text = "Recommended Repair Specs";
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
            var row = (TechSpecsViewModel)gridTechSpecs.GetFocusedRow();
            if(row == null) return; 
            await LoadDetails();
            await LoadSpecs();
            LoadActions();
            LoadStaff();
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

        private async void btnPreview_Click(object sender, EventArgs e)
        {
            await PrintReport();
        }

        private async Task PrintReport()
        {
            var row = (TechSpecsViewModel)gridTechSpecs.GetFocusedRow();
            var ts = await unitOfWork.TechSpecsRepo.FindAsync(x => x.Id == row.Id,
                x => x.Repairs,
                x => x.TechSpecsICTSpecs,
                x => x.TechSpecsICTSpecs.Select(s => s.EquipmentSpecs.Equipment));

            var chief = HRMISEmployees.GetEmployeeById(ts.ReqByChiefId);
            var staff = HRMISEmployees.GetEmployeeById(ts.ReqById);
            var data = new TechSpecsReportViewModel()
            {
                PrintedBy = UserStore.Username,
                DatePrinted = DateTime.UtcNow,
                Office = string.Join(" ", staff.Office, staff.Division),
                Chief = chief.Employee,
                ChiefPosition = chief.Position,
                Staff = staff.Employee,
                StaffPosition = staff.Position,
                TechSpecs = ts,
                PreparedBy = await userManager.FindUserAsync(ts.PreparedById),
                ReviewedBy = await userManager.FindUserAsync(ts.ReviewedById),
                NotedBy = await userManager.FindUserAsync(ts.NotedById),
                RepairId = ts.Repairs.Count == 0 ? 0 : ts.Repairs.FirstOrDefault().Id
            };

            var rpt = new rptTechSpecs
            {
                DataSource = new List<TechSpecsReportViewModel> { data }
            };

            var frm = new frmReportViewer(rpt);
            frm.ShowDialog();
        }

        private void hplRepair_Click(object sender, EventArgs e)
        {
            var row = (TechSpecsViewModel)gridTechSpecs.GetFocusedRow();
            var main = Application.OpenForms["frmMain"] as frmMain;
            main.mainPanel.Controls.Clear();

            main.mainPanel.Controls.Add(new UCRepair()
            {
                Dock = DockStyle.Fill,
                filterText = row.RepairId.ToString()
            });
        }
    }
}
