using DevExpress.Data.Filtering;
using DevExpress.PivotGrid.ServerMode;
using DevExpress.XtraCharts.Native;
using DevExpress.XtraEditors;
using EntityManager.Managers.User;
using ICTProfilingV3.ActionsForms;
using ICTProfilingV3.PPEInventoryForms;
using ICTProfilingV3.ReportForms;
using ICTProfilingV3.TechSpecsForms;
using ICTProfilingV3.TicketRequestForms;
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
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ICTProfilingV3.RepairForms
{
    public partial class UCRepair : DevExpress.XtraEditors.XtraUserControl
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IICTUserManager _userManager;
        public string filterText { get; set; }
        public UCRepair()
        {
            InitializeComponent();
            _unitOfWork = new UnitOfWork();
            _userManager = new ICTUserManager();
            LoadRepair();
        }

        private void LoadRepair()
        {
            var res = _unitOfWork.RepairsRepo.GetAll(x => x.TicketRequest,
                x => x.PPEs).ToList();
            var repair = res.Select(x => new RepairViewModel
            {
                Id = x.Id,
                Status = x.TicketRequest.TicketStatus,
                DateCreated = x.DateCreated,
                PropertyNo = x.PPEs?.PropertyNo,
                IssuedTo = HRMISEmployees.GetEmployeeById(x.RequestedById)?.Employee,
                Office = HRMISEmployees.GetEmployeeById(x.RequestedById)?.Office,
                RepairId = "EPiS-" + x.Id
            });
            gcRepair.DataSource = repair; 
        }

        private void LoadActions()
        {
            var row = (RepairViewModel)gridRepair.GetFocusedRow();
            tabAction.Controls.Clear();
            if (row == null) return;
            tabAction.Controls.Add(new UCActions(new ActionType
            {
                Id = row.Id,
                RequestType = RequestType.Repairs,
            })
            {
                Dock = DockStyle.Fill
            });
        }
        private async Task LoadRepairDetails()
        {
            ClearAllFields();
            var row = (RepairViewModel)gridRepair.GetFocusedRow();
            if(row == null) return;
            var repair = await _unitOfWork.RepairsRepo.FindAsync(x => x.Id == row.Id,
                x => x.PPEsSpecs);
            if (repair == null) return;

            var employee = HRMISEmployees.GetEmployeeById(repair.RequestedById);
            var chief = HRMISEmployees.GetChief(employee?.Office, employee?.Division);

            lblRepairNo.Text = repair.Id.ToString();
            txtOffice.Text = string.Join(" ", employee?.Office , employee?.Division);
            txtChief.Text = HRMISEmployees.GetEmployeeById(chief?.ChiefId)?.Employee;
            txtReqBy.Text = employee?.Employee;
            txtContactNo.Text = repair.ContactNo;
            txtDeliveredBy.Text = HRMISEmployees.GetEmployeeById(repair.DeliveredById)?.Employee;

            txtFindings.Text = repair.Findings;
            txtRecommendation.Text = repair.Recommendations;
            txtRequestProblem.Text = repair.Problems;
            var prepared = await _userManager.FindUserAsync(repair.PreparedById);
            txtPreparedBy.Text = prepared?.FullName;
            var assesed = await _userManager.FindUserAsync(repair.ReviewedById);
            txtAssessedBy.Text = assesed?.FullName;
            var noted = await _userManager.FindUserAsync(repair.NotedById);
            txtNotedBy.Text = noted?.FullName;

            await LoadEquipmentDetails(repair.PPEs , repair.PPEsSpecs);
        }

        private async Task LoadEquipmentDetails(PPEs ppe, PPEsSpecs ppeSpecs)
        {
            LoadPPEEquipment(ppe);
            if (ppe == null) return;
            txtPropertyNo.Text = ppe.PropertyNo;   
            txtIssuedTo.Text = HRMISEmployees.GetEmployeeById(ppe.IssuedToId)?.Employee;
            txtStatus.Text = ppe.Status.ToString();

            if (ppeSpecs == null) return;
            var specs = await _unitOfWork.PPEsSpecsRepo.FindAsync(x => x.Id == ppeSpecs.Id,
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
            tabEquipmentSpecs.Controls.Add(new UCAddPPEEquipment(specs, _unitOfWork, false)
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
            var frm = new frmAddEditRepair(_unitOfWork, row.Id);
            frm.ShowDialog();

            await LoadRepairDetails();
        }

        private void UCRepair_Load(object sender, EventArgs e)
        {
            if (filterText != null) gridRepair.ActiveFilterCriteria = new BinaryOperator("Id",filterText);
        }
        private async void btnSignatories_Click(object sender, EventArgs e)
        {
            var row = (RepairViewModel)gridRepair.GetFocusedRow();
            var frm = new frmEditSignatories(_unitOfWork, row.Id);
            frm.ShowDialog();

            await LoadRepairDetails();
        }

        private async void btnFindings_Click(object sender, EventArgs e)
        {
            var row = (RepairViewModel)gridRepair.GetFocusedRow();
            var frm = new frmEditFindings(_unitOfWork, row.Id);
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
            var row = (RepairViewModel)gridRepair.GetFocusedRow();
            var repair = await _unitOfWork.RepairsRepo.FindAsync(x => x.Id == row.Id,
                x => x.PPEs,
                x => x.PPEsSpecs.Model.Brand.EquipmentSpecs.Equipment);

            var data = new RepairTRViewModel
            {
                PrintedBy = UserStore.Username,
                DatePrinted = DateTime.UtcNow,
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
        }

        private void hplTicket_Click(object sender, EventArgs e)
        {
            var row = (RepairViewModel)gridRepair.GetFocusedRow();
            var main = Application.OpenForms["frmMain"] as frmMain;
            main.mainPanel.Controls.Clear();

            main.mainPanel.Controls.Add(new UCTARequestDashboard()
            {
                Dock = DockStyle.Fill,
                filterText = row.Id.ToString()
            });
        }

        private void hplPPE_Click(object sender, EventArgs e)
        {
            var row = (RepairViewModel)gridRepair.GetFocusedRow();
            var main = Application.OpenForms["frmMain"] as frmMain;
            main.mainPanel.Controls.Clear();

            main.mainPanel.Controls.Add(new UCPPEs()
            {
                Dock = DockStyle.Fill,
                filterText = row.PropertyNo.ToString()
            });
        }

        private async void btnTechSpecs_Click(object sender, EventArgs e)
        {
            var row = (RepairViewModel)gridRepair.GetFocusedRow();
            var repair = await _unitOfWork.RepairsRepo.FindAsync(x => x.Id == row.Id);
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
            _unitOfWork.Save();
        }

        private void NavigateToRepairSpecs(Repairs repair)
        {
            var main = Application.OpenForms["frmMain"] as frmMain;
            main.mainPanel.Controls.Clear();

            main.mainPanel.Controls.Add(new UCTechSpecs()
            {
                IsTechSpecs = false,
                Dock = DockStyle.Fill,
                filterText = repair.TechSpecsId.ToString()
            });
        }
    }
}
