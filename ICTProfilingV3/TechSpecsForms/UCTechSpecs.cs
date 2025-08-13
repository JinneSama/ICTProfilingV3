using DevExpress.Data.Filtering;
using Helpers.Interfaces;
using ICTProfilingV3.ActionsForms;
using ICTProfilingV3.API.FilesApi;
using ICTProfilingV3.Core.Common;
using ICTProfilingV3.DataTransferModels.Models;
using ICTProfilingV3.DataTransferModels.ReportViewModel;
using ICTProfilingV3.DataTransferModels.ViewModels;
using ICTProfilingV3.EvaluationForms;
using ICTProfilingV3.Interfaces;
using ICTProfilingV3.RepairForms;
using ICTProfilingV3.ReportForms;
using ICTProfilingV3.Services;
using ICTProfilingV3.Services.Employees;
using ICTProfilingV3.TicketRequestForms;
using ICTProfilingV3.ToolForms;
using Microsoft.Extensions.DependencyInjection;
using Models.Entities;
using Models.Enums;
using Models.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ICTProfilingV3.TechSpecsForms
{
    public partial class UCTechSpecs : DevExpress.XtraEditors.XtraUserControl, IDisposeUC
    {
        private readonly IICTUserManager _userManager;
        private readonly IUCManager _ucManager;
        private readonly IServiceProvider _serviceProvider;
        private readonly IStaffService _staffService;
        private readonly IEquipmentService _equipmentService;
        private readonly UserStore _userStore;
        private HTTPNetworkFolder networkFolder;
        public string filterText { get; set; }
        public bool IsTechSpecs { get; set; }
        public UCTechSpecs(IUCManager ucManager, IServiceProvider serviceProvider, UserStore userStore,
            IICTUserManager userManager, IStaffService staffService, IEquipmentService equipmentService)
        {
            InitializeComponent();
            _userStore = userStore;
            _staffService = staffService;
            _equipmentService = equipmentService;
            _ucManager = ucManager;
            _serviceProvider = serviceProvider;
            networkFolder = new HTTPNetworkFolder();
            _userManager = userManager;
            _userStore = userStore;

            LoadFilterDropdowns();
        }
        private void LoadFilterDropdowns()
        {
            var users = _staffService.GetAllStaff().
                Select(x => x.Users).ToList();
            slueTaskOf.Properties.DataSource = users;

            slueEquipment.Properties.DataSource = _equipmentService.GetAllEquipment().ToList();
            FilterGrid();
        }
        private void LoadAll()
        {
            LoadTechSpecs();
            LoadDropdowns();
        }
        private void LoadDropdowns()
        {
            var users = _userManager.GetUsers();
            sluePreparedBy.Properties.DataSource = users;
            slueReviewedBy.Properties.DataSource = users;
            slueNotedBy.Properties.DataSource = users;
        }


        private async Task LoadDetails()
        {
            IUnitOfWork unitOfWork = new UnitOfWork();
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
            var ts = await unitOfWork.TechSpecsRepo.FindAsync(x => x.Id == row.Id,
                x => x.TicketRequest);
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
            var chief = HRMISEmployees.GetChief(reqByUser?.Office , reqByUser?.Division, ts.ReqById);

            txtRequestingOfficeChief.Text = HRMISEmployees.GetEmployeeById(chief?.ChiefId)?.Employee;
            txtRequestingOfficeChiefPos.Text = HRMISEmployees.GetEmployeeById(chief?.ChiefId)?.Position;
            txtRequestedByEmployeeName.Text = reqByUser?.Employee;
            txtRequestedByPos.Text = reqByUser?.Position;
            txtRequestedByOffice.Text = reqByUser?.Office;
            txtRequestedByDivision.Text = reqByUser?.Division;
        }

        private void LoadTechSpecs()
        {
            IUnitOfWork unitOfWork = new UnitOfWork();
            var res = unitOfWork.TechSpecsRepo.FindAllAsync(x => x.TicketRequest.StaffId != null,
                x => x.TicketRequest, x => x.Repairs,
                x => x.TicketRequest.ITStaff.Users,
                x => x.TechSpecsICTSpecs.Select(s => s.EquipmentSpecs.Equipment))
                .OrderByDescending(x => x.DateRequested)
                .ToList();

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
                TechSpecs = x,
                AssignedTo = x.TicketRequest.ITStaff.Users.FullName,
                Equipment = x.TechSpecsICTSpecs.Count == 0 ? "N/A" : string.Join(", ", x.TechSpecsICTSpecs?.Select(s => s?.EquipmentSpecs?.Equipment?.EquipmentName ?? ""))
            });
            gcTechSpecs.DataSource = new BindingList<TechSpecsViewModel>(ts.ToList());
        }

        private async Task LoadSpecs()
        {
            var row = (TechSpecsViewModel)gridTechSpecs.GetFocusedRow();
            tabRequestedSpecs.Controls.Clear();
            if (row == null) return;

            lblTSId.Text = row.Id.ToString();
            var uow = new UnitOfWork();
            var ts = await uow.TechSpecsRepo.FindAsync(x => x.Id == row.Id);
            tabRequestedSpecs.Controls.Add(new UCRequestedTechSpecs(ts)
            {
                Dock = System.Windows.Forms.DockStyle.Fill
            });
        }
        private void LoadEvaluationSheet()
        {
            var row = (TechSpecsViewModel)gridTechSpecs.GetFocusedRow();
            if (row == null) return;
            var navigation = _serviceProvider.GetRequiredService<IControlNavigator<UCEvaluationSheet>>();
            navigation.NavigateTo(tabEvaluation, act => act.InitForm(new ActionType { Id = row.Id, RequestType = RequestType.TechSpecs }));
        }

        private async void LoadStaff()
        {
            IUnitOfWork unitOfWork = new UnitOfWork();
            var row = (TechSpecsViewModel)gridTechSpecs.GetFocusedRow();
            var staff = await unitOfWork.ITStaffRepo.FindAsync(x => x.Id == row.TechSpecs.TicketRequest.StaffId, x => x.Users);
            Image img = await networkFolder.DownloadFile(staff?.UserId + ".jpeg");
            var res = new StaffModel
            {
                Image = img,
                AssignedTo = row.Status == TicketStatus.Accepted ? "Not Yet Assigned!" : (staff == null ? "N / A" : staff.Users.UserName),
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

            var uc = _serviceProvider.GetRequiredService<UCActions>();
            uc.setActions(new ActionType { Id = row.Id, RequestType = Models.Enums.RequestType.TechSpecs });
            uc.Dock = System.Windows.Forms.DockStyle.Fill;
            tabAction.Controls.Add(uc);
        }

        private async void btnEdit_Click(object sender, System.EventArgs e)
        {
            IUnitOfWork unitOfWork = new UnitOfWork();
            var row = (TechSpecsViewModel)gridTechSpecs.GetFocusedRow();
            var ts = await unitOfWork.TechSpecsRepo.FindAsync(x => x.Id == row.Id);
            var frm = _serviceProvider.GetRequiredService<frmAddEditTechSpecs>();
            frm.InitForTSForm(ts);
            frm.ShowDialog();
        }

        private void UCTechSpecs_Load(object sender, EventArgs e)
        {
            LoadAll();
            if(!IsTechSpecs)
                lblTechSpecs.Text = "Recommended Repair Specs";
            ApplyFilterText();
        }

        public void ApplyFilterText()
        {
            if (filterText != null) gridTechSpecs.ActiveFilterCriteria = new BinaryOperator("TicketId", filterText);
        }

        private async void btnVerifyPR_Click(object sender, EventArgs e)
        {
            IUnitOfWork unitOfWork = new UnitOfWork();
            var row = (TechSpecsViewModel)gridTechSpecs.GetFocusedRow();
            var res = await unitOfWork.PurchaseRequestRepo.FindAsync(x => x.TechSpecsId == row.Id);

            if (res == null) await CreatePR(row);
            else await GotoPR(row);
        }

        private async Task GotoPR(TechSpecsViewModel row)
        {
            IUnitOfWork unitOfWork = new UnitOfWork();
            var res = await unitOfWork.TechSpecsRepo.FindAsync(x => x.Id == row.Id);
            var uow = new UnitOfWork();
            var pr = await uow.PurchaseRequestRepo.FindAsync(x => x.TechSpecsId == row.Id);

            var mainForm = _serviceProvider.GetRequiredService<frmMain>();
            var navigation = _serviceProvider.GetRequiredService<IControlNavigator<UCRepair>>();
            navigation.NavigateTo(mainForm.mainPanel, act => act.filterText = pr.Id.ToString());
        }

        private async Task CreatePR(TechSpecsViewModel row)
        {
            IUnitOfWork unitOfWork = new UnitOfWork();
            if (MessageBox.Show("No PR Saved for this Tech Specs, Verify New PR?",
                                "Question", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel) return;

            var res = await unitOfWork.TechSpecsRepo.FindAsync(x => x.Id == row.Id);

            var pr = new PurchaseRequest
            {
                ChiefId = res.ReqByChiefId,
                TechSpecsId = res.Id,
                ReqById = res.ReqById,
                CreatedById = _userStore.UserId,
                DateCreated = DateTime.Now
            };

            unitOfWork.PurchaseRequestRepo.Insert(pr);
            await unitOfWork.SaveChangesAsync();

            var mainForm = _serviceProvider.GetRequiredService<frmMain>();
            var navigation = _serviceProvider.GetRequiredService<IControlNavigator<UCRepair>>();
            navigation.NavigateTo(mainForm.mainPanel, act => act.filterText = pr.Id.ToString());
        }

        private async void gridTechSpecs_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            var row = (TechSpecsViewModel)gridTechSpecs.GetFocusedRow();
            if(row == null) return; 
            await LoadDetails();
            await LoadSpecs();
            LoadEvaluationSheet();
            LoadActions();
            LoadStaff();
        }

        private void hplTicket_Click(object sender, EventArgs e)
        {
            var row = (TechSpecsViewModel)gridTechSpecs.GetFocusedRow();
            var mainForm = _serviceProvider.GetRequiredService<frmMain>();
            var navigation = _serviceProvider.GetRequiredService<IControlNavigator<UCTARequestDashboard>>();
            navigation.NavigateTo(mainForm.mainPanel, act => act.filterText = row.Id.ToString());
        }

        private async void btnPreview_Click(object sender, EventArgs e)
        {
            await PrintReport();
        }

        private async Task PrintReport()
        {
            IUnitOfWork unitOfWork = new UnitOfWork();
            var row = (TechSpecsViewModel)gridTechSpecs.GetFocusedRow();
            var ts = await unitOfWork.TechSpecsRepo.FindAsync(x => x.Id == row.Id,
                x => x.Repairs,
                x => x.TechSpecsICTSpecs,
                x => x.TechSpecsICTSpecs.Select(s => s.TechSpecsICTSpecsDetails),
                x => x.TechSpecsICTSpecs.Select(s => s.EquipmentSpecs),
                x => x.TechSpecsICTSpecs.Select(s => s.EquipmentSpecs.Equipment));

            var chief = HRMISEmployees.GetEmployeeById(ts.ReqByChiefId);
            var staff = HRMISEmployees.GetEmployeeById(ts.ReqById);
            var data = new TechSpecsReportViewModel()
            {
                PrintedBy = _userStore.Username,
                DatePrinted = DateTime.Now,
                Office = string.Join(" ", staff?.Office, staff?.Division),
                Chief = chief.Employee,
                ChiefPosition = chief?.Position,
                Staff = staff?.Employee,
                StaffPosition = staff?.Position,
                TechSpecs = ts,
                PreparedBy = await _userManager.FindUserAsync(ts.PreparedById),
                ReviewedBy = await _userManager.FindUserAsync(ts.ReviewedById),
                NotedBy = await _userManager.FindUserAsync(ts.NotedById),
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
            var mainForm = _serviceProvider.GetRequiredService<frmMain>();
            var navigation = _serviceProvider.GetRequiredService<IControlNavigator<UCRepair>>();
            navigation.NavigateTo(mainForm.mainPanel, act => act.filterText = row.RepairId.ToString());
        }

        public void DisposeUC(Control parent)
        {
            foreach (Control ctrl in parent.Controls)
            {
                ctrl.Dispose();
                GC.Collect();
            }
            parent.Controls.Clear();
        }

        private void slueEquipment_EditValueChanged(object sender, EventArgs e)
        {
            FilterGrid();
        }

        private void slueTaskOf_EditValueChanged(object sender, EventArgs e)
        {
            FilterGrid();
        }

        private void ceCompleted_CheckedChanged(object sender, EventArgs e)
        {
            FilterGrid();
        }

        private void btnFilterbyDate_Click(object sender, EventArgs e)
        {
            FilterGrid();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            deFrom.EditValue = null;
            deTo.EditValue = null;
            slueEquipment.EditValue = null;
            slueTaskOf.EditValue = null;
            ceCompleted.Checked = false;

            FilterGrid();
        }
        private void FilterGrid()
        {
            gridTechSpecs.ActiveFilterCriteria = null;

            var dateFrom = deFrom.DateTime;
            var dateTo = deTo.DateTime;
            var equipment = (string)slueEquipment.EditValue;
            var isCompleted = ceCompleted.Checked;
            var row = slueTaskOf.EditValue;

            var criteria = gridTechSpecs.ActiveFilterCriteria;

            if (isCompleted)
                foreach (var status in Enum.GetValues(typeof(TicketStatus)).Cast<TicketStatus>())
                    criteria = GroupOperator.Or(criteria, new BinaryOperator("Status", status));
            else
                foreach (var status in Enum.GetValues(typeof(TicketStatus)).Cast<TicketStatus>())
                {
                    if (status == TicketStatus.Completed) continue;
                    criteria = GroupOperator.Or(criteria, new BinaryOperator("Status", status));
                }

            if (slueTaskOf.EditValue != null)
                criteria = GroupOperator.And(criteria, new BinaryOperator("AssignedTo", row));
            if (slueEquipment.EditValue != null)
                criteria = GroupOperator.And(criteria, new FunctionOperator(FunctionOperatorType.Contains, new OperandProperty("Equipment"), new OperandValue(equipment)));

            if (deFrom.EditValue != null && deTo.EditValue != null)
            {
                var fromFilter = new BinaryOperator("DateCreated", dateFrom, BinaryOperatorType.GreaterOrEqual);
                var toFilter = new BinaryOperator("DateCreated", dateTo, BinaryOperatorType.LessOrEqual);
                criteria = GroupOperator.And(criteria, GroupOperator.And(fromFilter, toFilter));
            }

            gridTechSpecs.ActiveFilterCriteria = criteria;
        }
    }
}
