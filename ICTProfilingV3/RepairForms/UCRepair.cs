using DevExpress.Data.Filtering;
using DevExpress.PivotGrid.ServerMode;
using DevExpress.XtraCharts.Native;
using DevExpress.XtraEditors;
using EntityManager.Managers;
using EntityManager.Managers.User;
using Helpers.Interfaces;
using Helpers.NetworkFolder;
using Helpers.Utility;
using ICTProfilingV3.ActionsForms;
using ICTProfilingV3.EvaluationForms;
using ICTProfilingV3.PPEInventoryForms;
using ICTProfilingV3.ReportForms;
using ICTProfilingV3.TechSpecsForms;
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
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ICTProfilingV3.RepairForms
{
    public partial class UCRepair : DevExpress.XtraEditors.XtraUserControl , IDisposeUC
    {
        private readonly IICTUserManager _userManager;
        private readonly IUCManager<Control> _ucManager;
        private HTTPNetworkFolder networkFolder;
        public string filterText { get; set; }
        public UCRepair()
        {
            InitializeComponent();
            var main = Application.OpenForms["frmMain"] as frmMain;
            _ucManager = main._ucManager;
            _userManager = new ICTUserManager();
            networkFolder = new HTTPNetworkFolder();
            LoadRepair();
        }

        private void LoadRepair()
        {
            IUnitOfWork unitOfWork = new UnitOfWork();
            var res = unitOfWork.RepairsRepo.FindAllAsync(x => x.TicketRequest.StaffId != null,
                x => x.TicketRequest,
                x => x.PPEs).OrderByDescending(x => x.DateCreated).ToList();
            var repair = res.Select(x => new RepairViewModel
            {
                Id = x.Id,
                Status = x.TicketRequest.TicketStatus,
                DateCreated = x.DateCreated,
                PropertyNo = x.PPEs?.PropertyNo,
                IssuedTo = HRMISEmployees.GetEmployeeById(x.RequestedById)?.Employee,
                Office = HRMISEmployees.GetEmployeeById(x.RequestedById)?.Office,
                RepairId = "EPiS-" + x.Id,
                Repair = x
            });
            gcRepair.DataSource = repair; 
        }

        private void LoadActions()
        {
            var row = (RepairViewModel)gridRepair.GetFocusedRow();
            tabAction.Controls.Clear();
            if (row == null) return;

            var ucAction = new UCActions(new ActionType
            {
                Id = row.Id,
                RequestType = RequestType.Repairs
            })
            {
                Dock = DockStyle.Fill
            };
            if(row.Repair.PPEs.Status == PPEStatus.Condemned) ucAction.btnAddAction.Enabled = false;
            tabAction.Controls.Add(ucAction);
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
            if (repair == null) return;

            txtEquipment.Text = repair.PPEs.PPEsSpecs.FirstOrDefault()?.Model.Brand?.EquipmentSpecs?.Equipment?.EquipmentName;
            txtBrand.Text = repair.PPEs.PPEsSpecs.FirstOrDefault()?.Model?.Brand?.BrandName;
            txtModel.Text = repair.PPEs.PPEsSpecs.FirstOrDefault()?.Model?.ModelName;
            txtDescription.Text = repair.PPEs.PPEsSpecs.FirstOrDefault()?.Description;
            txtSerialNo.Text = repair.PPEs.SerialNo;

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
            var prepared = repair?.PreparedById == null ? null : await userManager.FindUserAsync(repair.PreparedById);
            txtPreparedBy.Text = prepared?.FullName;
            var assesed = repair?.ReviewedById == null ? null : await userManager.FindUserAsync(repair.ReviewedById);
            txtAssessedBy.Text = assesed?.FullName;
            var noted = repair?.NotedById == null ? null : await userManager.FindUserAsync(repair.NotedById);
            txtNotedBy.Text = noted?.FullName;
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
            tabEvaluation.Controls.Clear();
            tabEvaluation.Controls.Add(new UCEvaluationSheet(new ActionType { Id = row.Id, RequestType = RequestType.Repairs })
            {
                Dock = System.Windows.Forms.DockStyle.Fill
            });
        }

        private async Task LoadEquipmentDetails(PPEs ppe, PPEsSpecs ppeSpecs)
        {
            IUnitOfWork unitOfWork = new UnitOfWork();
            LoadPPEEquipment(ppe);
            if (ppe == null) return;
            txtPropertyNo.Text = ppe.PropertyNo;   
            txtIssuedTo.Text = HRMISEmployees.GetEmployeeById(ppe.IssuedToId)?.Employee;
            txtStatus.Text = ppe.Status.ToString();

            if (ppeSpecs == null) return;
            var specs = await unitOfWork.PPEsSpecsRepo.FindAsync(x => x.Id == ppeSpecs.Id,
                x => x.Model.Brand,
                x => x.Model.Brand.EquipmentSpecs,
                x => x.Model.Brand.EquipmentSpecs.Equipment);

            if(specs == null) return;
            txtEquipment.Text = specs.Model.Brand.EquipmentSpecs.Equipment.EquipmentName;
            txtDescription.Text = specs.Model.Brand.EquipmentSpecs.Description;
            txtBrand.Text = specs.Model.Brand.BrandName;
            txtModel.Text = specs.Model.ModelName;
            txtSerialNo.Text = specs.SerialNo;
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
            var frm = new frmAddEditRepair(row.Id);
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
        private async void btnSignatories_Click(object sender, EventArgs e)
        {
            var row = (RepairViewModel)gridRepair.GetFocusedRow();
            var frm = new frmEditSignatories(row.Id);
            frm.ShowDialog();

            await LoadRepairDetails();
        }

        private async void btnFindings_Click(object sender, EventArgs e)
        {
            var row = (RepairViewModel)gridRepair.GetFocusedRow();
            var frm = new frmEditFindings(row.Id);
            frm.ShowDialog();

            await LoadRepairDetails();
        }

        private void btnLedger_Click(object sender, EventArgs e)
        {

        }

        private async void btnTR_Click(object sender, EventArgs e)
        {
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
                PrintedBy = UserStore.Username,
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
            _ucManager.ShowUCSystemDetails(hplTicket.Name, new UCTARequestDashboard()
            {
                Dock = DockStyle.Fill,
                filterText = row.Id.ToString()
            },new string[] { "filterText" });
        }

        private void hplPPE_Click(object sender, EventArgs e)
        {
            var row = (RepairViewModel)gridRepair.GetFocusedRow();
            _ucManager.ShowUCSystemDetails(hplPPE.Name, new UCPPEs()
            {
                Dock = DockStyle.Fill,
                filterText = row.PropertyNo.ToString()
            }, new string[] { "filterText" });
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

            var frm = new frmAddEditTechSpecs(repair);
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
            _ucManager.ShowUCSystemDetails(btnTechSpecs.Name, new UCTechSpecs()
            {
                IsTechSpecs = false,
                Dock = DockStyle.Fill,
                filterText = repair.TechSpecsId.ToString()
            }, new string[] { "filterText" , "IsTechSpecs" });
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
    }
}
