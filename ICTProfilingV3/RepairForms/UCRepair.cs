using DevExpress.Data.Filtering;
using DevExpress.XtraEditors;
using Helpers.Interfaces;
using ICTProfilingV3.ActionsForms;
using ICTProfilingV3.API.FilesApi;
using ICTProfilingV3.Core.Common;
using ICTProfilingV3.DataTransferModels.Models;
using ICTProfilingV3.DataTransferModels.ReportViewModel;
using ICTProfilingV3.DataTransferModels.ViewModels;
using ICTProfilingV3.EvaluationForms;
using ICTProfilingV3.Interfaces;
using ICTProfilingV3.PPEInventoryForms;
using ICTProfilingV3.ReportForms;
using ICTProfilingV3.Services.ApiUsers;
using ICTProfilingV3.Services.Employees;
using ICTProfilingV3.TechSpecsForms;
using ICTProfilingV3.TicketRequestForms;
using ICTProfilingV3.ToolForms;
using Microsoft.Extensions.DependencyInjection;
using Models.Entities;
using Models.Enums;
using Models.Repository;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ICTProfilingV3.RepairForms
{
    public partial class UCRepair : XtraUserControl , IDisposeUC
    {
        private readonly IICTUserManager _userManager;
        private readonly IUCManager _ucManager;
        private readonly IServiceProvider _serviceProvider;
        private readonly IStaffService _staffService;
        private readonly IEquipmentService _equipmentService;
        private readonly UserStore _userStore;

        private HTTPNetworkFolder networkFolder;
        public string filterText { get; set; }
        public UCRepair(IUCManager ucManager, IServiceProvider serviceProvider, UserStore userStore, IICTUserManager userManager, 
            IStaffService staffService, IEquipmentService equipmentService)
        {
            InitializeComponent();
            _ucManager = ucManager;
            _serviceProvider = serviceProvider;
            _userStore = userStore;
            _userManager = userManager;
            _staffService = staffService;
            _equipmentService = equipmentService;
            networkFolder = new HTTPNetworkFolder();
            LoadRepair();
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

        private void LoadRepair()
        {
            IUnitOfWork unitOfWork = new UnitOfWork();
            var res = unitOfWork.RepairsRepo.FindAllAsync(x => x.TicketRequest.StaffId != null,
                x => x.TicketRequest,
                x => x.PPEs,
                x => x.PPEs.PPEsSpecs.Select(s => s.Model.Brand.EquipmentSpecs.Equipment))
                .OrderByDescending(x => x.DateCreated).ToList();
            var repair = res.Select(x => new RepairViewModel
            {
                Id = x.Id,
                Status = x.TicketRequest.TicketStatus,
                DateCreated = x.DateCreated,
                PropertyNo = x.PPEs?.PropertyNo,
                IssuedTo = HRMISEmployees.GetEmployeeById(x.RequestedById)?.Employee,
                Office = HRMISEmployees.GetEmployeeById(x.RequestedById)?.Office,
                RepairId = "EPiS-" + x.Id,
                Repair = x,
                AssignedTo = x.TicketRequest.StaffId,
                Equipment = x.PPEs?.PPEsSpecs?.FirstOrDefault()?.Model?.Brand?.EquipmentSpecs?.Equipment?.EquipmentName ?? ""
            });
            gcRepair.DataSource = repair; 
        }

        private void LoadActions()
        {
            var row = (RepairViewModel)gridRepair.GetFocusedRow();
            tabAction.Controls.Clear();
            if (row == null) return;

            var uc = _serviceProvider.GetRequiredService<UCActions>();
            uc.setActions(new ActionType
            {
                Id = row.Id,
                RequestType = RequestType.Repairs
            });
            uc.Dock = System.Windows.Forms.DockStyle.Fill;
            tabAction.Controls.Add(uc);

            if(row.Repair.PPEs.Status == PPEStatus.Condemned) uc.btnAddAction.Enabled = false;
            tabAction.Controls.Add(uc);
        }
        private async void LoadStaff()
        {
            IUnitOfWork unitOfWork = new UnitOfWork();
            var row = (RepairViewModel)gridRepair.GetFocusedRow();
            ITStaff staff = null;
            if(row != null) staff = await unitOfWork.ITStaffRepo.FindAsync(x => x.Id == row.Repair.TicketRequest.StaffId, x => x.Users);
            Image img = await networkFolder.DownloadFile(staff?.UserId + ".jpeg");
            var res = new StaffModel
            {
                Image = img,
                AssignedTo = row?.Status == TicketStatus.Accepted ? "Not Yet Assigned!" : (staff == null ? "N / A" : staff.Users.UserName),
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
        private async Task LoadRepairDetails()
        {
            IUnitOfWork unitOfWork = new UnitOfWork();
            ClearAllFields();
            var row = (RepairViewModel)gridRepair.GetFocusedRow();
            if(row == null) return;
            var repair = await unitOfWork.RepairsRepo.FindAsync(x => x.Id == row.Id,
                x => x.TicketRequest,
                x => x.PPEs.PPEsSpecs,
                x => x.PPEs.PPEsSpecs.Select(s => s.Model),
                x => x.PPEs.PPEsSpecs.Select(s => s.Model.Brand),
                x => x.PPEs.PPEsSpecs.Select(s => s.Model.Brand.EquipmentSpecs),
                x => x.PPEs.PPEsSpecs.Select(s => s.Model.Brand.EquipmentSpecs.Equipment));

            spbTicketStatus.SelectedItemIndex = ((int)repair.TicketRequest.TicketStatus) - 1;
            var employee = HRMISEmployees.GetEmployeeById(repair.RequestedById);
            var chief = HRMISEmployees.GetChief(employee?.Office, employee?.Division, repair.RequestedById);

            lblRepairNo.Text = repair.Id.ToString();
            txtOffice.Text = string.Join(" ", employee?.Office , employee?.Division);
            txtChief.Text = HRMISEmployees.GetEmployeeById(chief?.ChiefId)?.Employee;
            txtReqBy.Text = employee?.Employee;
            txtContactNo.Text = repair.ContactNo;
            txtDeliveredBy.Text = HRMISEmployees.GetEmployeeById(repair.DeliveredById)?.Employee;

            txtFindings.Text = repair.Findings;
            txtRecommendation.Text = repair.Recommendations;
            txtRequestProblem.Text = repair.Problems;

            var userManager = new ICTUserManager();
            if(repair.PPEs.Status == PPEStatus.Condemned)
            {
                SetButtons(true);
                MessageBox.Show("This Equipment is Condemned!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                SetButtons(false);
            }
            await LoadEquipmentDetails(repair.PPEs , repair.PPEsSpecs);
        }
        private void LoadEvaluationSheet()
        {
            var row = (RepairViewModel)gridRepair.GetFocusedRow();
            if(row == null) return;

            var navigation = _serviceProvider.GetRequiredService<IControlNavigator<UCEvaluationSheet>>();
            navigation.NavigateTo(tabEvaluation, act => act.InitForm(new ActionType { Id = row.Id, RequestType = RequestType.Repairs }));
        }

        private async Task LoadEquipmentDetails(PPEs ppe, PPEsSpecs ppeSpecs)
        {
            IUnitOfWork unitOfWork = new UnitOfWork();
            LoadPPEEquipment(ppe);

            if (ppeSpecs == null) return;
            var specs = await unitOfWork.PPEsSpecsRepo.FindAsync(x => x.Id == ppeSpecs.Id,
                x => x.Model.Brand,
                x => x.Model.Brand.EquipmentSpecs,
                x => x.Model.Brand.EquipmentSpecs.Equipment);
        }

        private void LoadPPEEquipment(PPEs specs)
        {
            tabEquipmentSpecs.Controls.Clear();
            tabEquipmentSpecs.Controls.Add(new UCAddPPEEquipment(specs, false)
            {
                Dock = DockStyle.Fill
            });
        }
        private void ClearAllFields()
        {
            foreach (var item in this.Controls)
            {
                var ctrl = (Control)item;
                if (ctrl.HasChildren){
                    foreach (var child in ctrl.Controls)
                    {
                        if (child.GetType() == typeof(TextEdit))
                            ((TextEdit)child).Text = string.Empty;

                        if (child.GetType() == typeof(MemoEdit))
                            ((MemoEdit)child).Text = string.Empty;
                    }
                }
            }
        }

        private async void btnEdit_Click(object sender, System.EventArgs e)
        {
            var row = (RepairViewModel)gridRepair.GetFocusedRow();
            var frm = _serviceProvider.GetRequiredService<frmAddEditRepairNew>();
            frm.InitForm(row.Id);
            frm.ShowDialog();

            await LoadRepairDetails();
        }

        private void UCRepair_Load(object sender, EventArgs e)
        {
            ApplyFilterText();
        }

        public void ApplyFilterText()
        {
            if (filterText != null) gridRepair.ActiveFilterCriteria = new BinaryOperator("Id", filterText);
        }

        private void btnLedger_Click(object sender, EventArgs e)
        {

        }

        private async void btnTR_Click(object sender, EventArgs e)
        {
            var row = (RepairViewModel)gridRepair.GetFocusedRow();
            var frm = _serviceProvider.GetRequiredService<frmEditSignatories>();
            frm.InitForm(row.Id);
            frm.ShowDialog();

            await PrintTR();
        }

        private async Task PrintTR()
        {
            IUnitOfWork unitOfWork = new UnitOfWork();
            var row = (RepairViewModel)gridRepair.GetFocusedRow();
            var repair = await unitOfWork.RepairsRepo.FindAsync(x => x.Id == row.Id,
                x => x.PPEs,
                x => x.PPEs.PPEsSpecs,
                x => x.PPEs.PPEsSpecs.Select(s => s.Model),
                x => x.PPEs.PPEsSpecs.Select(s => s.Model.Brand),
                x => x.PPEs.PPEsSpecs.Select(s => s.Model.Brand.EquipmentSpecs),
                x => x.PPEs.PPEsSpecs.Select(s => s.Model.Brand.EquipmentSpecs.Equipment)
                );
            var data = new RepairTRViewModel
            {
                PrintedBy = _userStore.Username,
                DatePrinted = DateTime.Now,
                RequestBy = HRMISEmployees.GetEmployeeById(repair.RequestedById),
                DeliveredBy = HRMISEmployees.GetEmployeeById(repair.DeliveredById),
                IssuedTo = HRMISEmployees.GetEmployeeById(repair.PPEs.IssuedToId),
                Repair = repair,
                ReceivedBy = await _userManager.FindUserAsync(repair.PreparedById),
                AssesedBy = await _userManager.FindUserAsync(repair.ReviewedById),
                NotedBy = await _userManager.FindUserAsync(repair.NotedById)
            };

            var rpt = new rptRepairTR
            {
                DataSource = new List<RepairTRViewModel> { data }
            };

            var frm = new frmReportViewer(rpt);
            frm.ShowDialog();
        }

        private async void gridRepair_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            await LoadRepairDetails();
            LoadActions();
            LoadEvaluationSheet();
            LoadStaff();
        }

        private void hplTicket_Click(object sender, EventArgs e)
        {
            var row = (RepairViewModel)gridRepair.GetFocusedRow(); 
            
            var mainForm = _serviceProvider.GetRequiredService<frmMain>();
            var navigation = _serviceProvider.GetRequiredService<IControlNavigator<UCTARequestDashboard>>();
            navigation.NavigateTo(mainForm.mainPanel, act => act.filterText = row.Id.ToString());
        }

        private void hplPPE_Click(object sender, EventArgs e)
        {
            var row = (RepairViewModel)gridRepair.GetFocusedRow();

            var mainForm = _serviceProvider.GetRequiredService<frmMain>();
            var navigation = _serviceProvider.GetRequiredService<IControlNavigator<UCPPEs>>();
            navigation.NavigateTo(mainForm.mainPanel, act => act.filterText = row.PropertyNo.ToString());
        }

        private async void btnTechSpecs_Click(object sender, EventArgs e)
        {
            IUnitOfWork unitOfWork = new UnitOfWork();
            var row = (RepairViewModel)gridRepair.GetFocusedRow();
            var repair = await unitOfWork.RepairsRepo.FindAsync(x => x.Id == row.Id);
            if (repair.TechSpecsId == null)
            {
                if (MessageBox.Show("This Repair has no Rcommended Specs, Proceeding will Generate Recommended Specs to this Repair",
                    "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) == DialogResult.Cancel) return;
            }
            else
            {
                NavigateToRepairSpecs(repair);
                return;
            }

            var frm = _serviceProvider.GetRequiredService<frmAddEditTechSpecs>();
            frm.InitForRepairForm(repair);
            frm.ShowDialog();

            var ts = frm.RepairTechSpecs;
            if (ts == null) return;
            repair.TechSpecsId = ts.Id;
            unitOfWork.Save();
        }

        private void SetButtons(bool condemned)
        {
            var controls = pnlButtons.Controls;
            foreach (Control c in controls)
            {
                if (c is SimpleButton)
                    c.Enabled = !condemned;
            }
        }
        private void NavigateToRepairSpecs(Repairs repair)
        {
            var mainForm = _serviceProvider.GetRequiredService<frmMain>();
            var navigation = _serviceProvider.GetRequiredService<IControlNavigator<UCRepair>>();
            navigation.NavigateTo(mainForm.mainPanel, act => act.filterText = repair.TechSpecsId.ToString());
        }

        private async void btnCondemned_Click(object sender, EventArgs e)
        {
            IUnitOfWork unitOfWork = new UnitOfWork();
            if (MessageBox.Show("Condemn this Equipment?","Confimation",MessageBoxButtons.OKCancel,MessageBoxIcon.Question) == DialogResult.Cancel) return;

            var row = (RepairViewModel)gridRepair.GetFocusedRow();
            var ppe = await unitOfWork.PPesRepo.FindAsync(x => x.Id == row.Repair.PPEsId);
            if(ppe == null) return;

            ppe.Status = PPEStatus.Condemned;

            await unitOfWork.SaveChangesAsync();
            LoadRepair();
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

        private void checkEdit1_CheckedChanged(object sender, EventArgs e)
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

        private void btnFilterbyDate_Click(object sender, EventArgs e)
        {
            FilterGrid();
        }

        private void FilterGrid()
        {
            gridRepair.ActiveFilterCriteria = null;

            var dateFrom = deFrom.DateTime;
            var dateTo = deTo.DateTime;
            var equipment = (string)slueEquipment.EditValue;
            var isCompleted = ceCompleted.Checked;
            var row = slueTaskOf.EditValue;

            var criteria = gridRepair.ActiveFilterCriteria;

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

            gridRepair.ActiveFilterCriteria = criteria;
        }
    }
}
