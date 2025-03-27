using DevExpress.Data.Filtering;
using DevExpress.XtraRichEdit.Import.OpenXml;
using Helpers.Interfaces;
using Helpers.NetworkFolder;
using Helpers.Utility;
using ICTMigration.ICTv2Models;
using ICTProfilingV3.ActionsForms;
using ICTProfilingV3.EvaluationForms;
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

namespace ICTProfilingV3.DeliveriesForms
{
    public partial class UCDeliveries : DevExpress.XtraEditors.XtraUserControl, IDisposeUC
    {
        private HTTPNetworkFolder networkFolder;
        private IUCManager<Control> _ucManager;

        public string filterText { get; set; }
        public UCDeliveries()
        {
            InitializeComponent();
            var main = Application.OpenForms["frmMain"] as frmMain;
            _ucManager = main._ucManager;
            networkFolder = new HTTPNetworkFolder();
        }

        private void LoadDropdown()
        {
            IUnitOfWork unitOfWork = new UnitOfWork();
            var staff = unitOfWork.UsersRepo.GetAll().ToList();
            var res = staff.OrderBy(o => o.FullName);

            slueTaskOf.Properties.DataSource = res.ToList();
        }

        private async Task LoadDeliveries()
        {
            IUnitOfWork unitOfWork = new UnitOfWork();
            var deliveries = await unitOfWork.DeliveriesRepo.FindAllAsync(x => x.TicketRequest.ITStaff != null,
                x => x.Supplier,
                x => x.TicketRequest,
                x => x.TicketRequest.ITStaff).OrderByDescending(x => x.DateRequested).ToListAsync();

            var delData = deliveries.Select(x => new DeliveriesViewModel
            {
                Id = x.Id,
                Status = x.TicketRequest.TicketStatus,
                TicketNo = x.TicketRequest.Id,
                Office = HRMISEmployees.GetEmployeeById(x.RequestedById)?.Office,
                Supplier = x.Supplier.SupplierName,
                DeliveryId = "EPiS-" + x.Id,
                PONo = x.PONo,
                Deliveries = x
            });
            gcDeliveries.DataSource = new BindingList<DeliveriesViewModel>(delData.ToList());
        }

        private async Task LoadStaff()
        {
            IUnitOfWork unitOfWork = new UnitOfWork();
            var row = (DeliveriesViewModel)gridDeliveries.GetFocusedRow();
            ITStaff staff = null;
            if(row != null) staff = await unitOfWork.ITStaffRepo.FindAsync(x => x.Id == row.Deliveries.TicketRequest.StaffId, x => x.Users);
            Image img = await networkFolder.DownloadFile(staff?.UserId + ".jpeg");
            var res = new StaffModel
            {
                Image = img,
                AssignedTo = row?.Status == TicketStatus.Accepted ? "Not Yet Assigned!" : (staff == null ? "N / A" : staff.Users.UserName),
                FullName = img == null ? (staff == null ? "N / A" : staff.Users.FullName) : "",
                PhotoVisible = img == null ? true : false,
                InitialsVisible = img == null ? false : true
            };
            staffPanel.Controls?.Cast<Control>()?.FirstOrDefault()?.Dispose();
            GC.Collect();
            staffPanel.Controls.Clear();
            staffPanel.Controls.Add(new UCAssignedTo(res)
            {
                Dock = DockStyle.Fill
            });
        }

        private void LoadDetails()
        {
            IUnitOfWork unitOfWork = new UnitOfWork();
            unitOfWork = new UnitOfWork();
            var row = (DeliveriesViewModel)gridDeliveries.GetFocusedRow();
            if (row == null) return;

            spbTicketStatus.SelectedItemIndex = ((int)row.Status) - 1;
            var deliveriesDetails = row.Deliveries;
            var requestingEmployee = HRMISEmployees.GetEmployeeById(deliveriesDetails.RequestedById);
            txtChief.Text = HRMISEmployees.GetEmployeeById(deliveriesDetails.ReqByChiefId)?.Employee;
            lblEpisNo.Text = deliveriesDetails.Id.ToString();
            txtOffice.Text = string.Join(" ", requestingEmployee?.Office, requestingEmployee?.Division);
            txtReqBy.Text = requestingEmployee?.Employee;
            txtTel.Text = deliveriesDetails.ContactNo;
            txtDeliveredBy.Text = HRMISEmployees.GetEmployeeById(deliveriesDetails.DeliveredById)?.Employee;
            txtSupplierName.Text = deliveriesDetails.Supplier?.SupplierName;
            txtSupplierAddress.Text = deliveriesDetails.Supplier?.Address;
            txtSupplierTelNo.Text = deliveriesDetails.Supplier?.TelNumber;
            dtDeliveredDate.DateTime = deliveriesDetails.DeliveredDate ?? DateTime.MinValue;
        }
        private void LoadEvaluationSheet()
        {
            var row = (DeliveriesViewModel)gridDeliveries.GetFocusedRow();
            tabEvaluation.Controls?.Cast<Control>()?.FirstOrDefault()?.Dispose();
            GC.Collect();
            tabEvaluation.Controls.Clear();
            tabEvaluation.Controls.Add(new UCEvaluationSheet(new ActionType { Id = row.Id, RequestType = RequestType.Deliveries })
            {
                Dock = DockStyle.Fill
            });
        }
        private async Task LoadEquipmentSpecs()
        {
            var row = (DeliveriesViewModel)gridDeliveries.GetFocusedRow();
            if (row == null) return;

            IUnitOfWork uow = new UnitOfWork();
            var deliveries = await uow.DeliveriesRepo.FindAsync(x => x.Id == row.Id , x => x.DeliveriesSpecs);

            if(deliveries == null) return;

            tabEquipmentSpecs.Controls?.Cast<Control>()?.FirstOrDefault()?.Dispose();
            GC.Collect();
            tabEquipmentSpecs.Controls.Clear();
            tabEquipmentSpecs.Controls.Add(new UCDeliveriesSpecs(deliveries)
            {
                Dock = DockStyle.Fill
            });
        }
        
        private void LoadActions()
        {
            var row = (DeliveriesViewModel)gridDeliveries.GetFocusedRow();
            tabAction.Controls.Clear();

            if (row == null) return;
            tabAction.Controls?.Cast<Control>()?.FirstOrDefault()?.Dispose();
            GC.Collect();
            tabAction.Controls.Add(new UCActions(new ActionType
            {
                Id = row.Id,
                RequestType = RequestType.Deliveries
            })
            {
                Dock = DockStyle.Fill
            });
        }

        private async void btnEdit_Click(object sender, System.EventArgs e)
        {
            var uow = new UnitOfWork();
            var rowHandle = gridDeliveries.FocusedRowHandle;
            var row = (DeliveriesViewModel)gridDeliveries.GetFocusedRow();
            var deliveries = await uow.DeliveriesRepo.FindAsync(x => x.Id == row.Id);
            var frm = new frmAddEditDeliveries(deliveries);
            frm.ShowDialog();

            await LoadDeliveries();
            await LoadEquipmentSpecs();
            LoadDetails();

            gridDeliveries.FocusedRowHandle = rowHandle;
        }

        private void btnPreview_Click(object sender, EventArgs e)
        {
            IUnitOfWork unitOfWork = new UnitOfWork();
            var item = (DeliveriesViewModel)gridDeliveries.GetFocusedRow();
            var ds = unitOfWork.DeliveriesRepo.FindAllAsync(x => x.Id == item.Id,
                x => x.Supplier,
                x => x.DeliveriesSpecs.Select(s => s.Model),
                x => x.DeliveriesSpecs.Select(s => s.Model.Brand),
                x => x.DeliveriesSpecs.Select(s => s.Model.Brand.EquipmentSpecs),
                x => x.DeliveriesSpecs.Select(s => s.Model.Brand.EquipmentSpecs.Equipment)).ToList();
            var rpt = new rptDeliveries
            {
                DataSource = ds,
                Office = txtOffice.Text
            };

            var frm = new frmReportViewer(rpt);
            frm.ShowDialog();
        }

        private async Task PrintComparisonReport()
        {
            var uow = new UnitOfWork();

            var item = (DeliveriesViewModel)gridDeliveries.GetFocusedRow();
            var deliveries = await uow.DeliveriesRepo.FindAsync(x => x.Id == item.Id,
                x => x.TicketRequest,
                x => x.Supplier,
                x => x.DeliveriesSpecs.Select(s => s.Model),
                x => x.DeliveriesSpecs.Select(s => s.Model.Brand),
                x => x.DeliveriesSpecs.Select(s => s.Model.Brand.EquipmentSpecs),
                x => x.DeliveriesSpecs.Select(s => s.Model.Brand.EquipmentSpecs.Equipment));

            var cr = await uow.ComparisonReportRepo.FindAsync(x => x.DeliveryId == deliveries.Id,
                x => x.PreparedByUser,
                x => x.NotedByUser,
                x => x.ReviewedByUser,
                x => x.ComparisonReportSpecs,
                x => x.ComparisonReportSpecs.Select(s => s.ComparisonReportSpecsDetails));

            var employee = HRMISEmployees.GetEmployeeById(deliveries.RequestedById);
            var inspectActions = deliveries?.Actions?.Where(x => x.SubActivityId == 1138 || x.SubActivityId == 1139).OrderBy(x => x.ActionDate);

            var comparisonModel = new ComparisonReportPrintViewModel
            {
                DateOfDelivery = deliveries.DeliveredDate,
                RequestingOffice = employee.Office + " " + employee.Division,
                Supplier = deliveries.Supplier.SupplierName,
                Amount = (double)deliveries.DeliveriesSpecs.Sum(x => (x.UnitCost * x.Quantity)),
                EpisNo = "EPiS-" + deliveries.TicketRequest.Id.ToString(),
                TechInspectedDate = (DateTime)inspectActions?.FirstOrDefault()?.ActionDate,
                ComparisonReportSpecs = cr?.ComparisonReportSpecs,
                PreparedBy = cr?.PreparedByUser,
                ReviewedBy = cr?.ReviewedByUser,
                NotedBy = cr?.NotedByUser,
                DatePrinted = DateTime.Now,
                PrintedBy = UserStore.Username
            };

            var rptCR = new rptComparisonReport
            {
                DataSource = new List<ComparisonReportPrintViewModel> { comparisonModel }
            };

            rptCR.CreateDocument();
            var frm = new frmReportViewer(rptCR);
            frm.ShowDialog();
        }

        private async void UCDeliveries_Load(object sender, EventArgs e)
        {
            await LoadDeliveries();
            LoadDropdown();
            ApplyFilterText();
        }

        public void ApplyFilterText()
        {
            if (filterText != null) gridDeliveries.ActiveFilterCriteria = new BinaryOperator("TicketNo", filterText);
        }

        private async void gridDeliveries_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            await LoadEquipmentSpecs();
            LoadActions();
            LoadDetails();
            LoadEvaluationSheet();
            await LoadStaff();
        }

        private void hplTicket_Click(object sender, EventArgs e)
        {
            var row = (DeliveriesViewModel)gridDeliveries.GetFocusedRow();

            _ucManager.ShowUCSystemDetails(hplTicket.Name, new UCTARequestDashboard()
            {
                Dock = DockStyle.Fill,
                filterText = row.Id.ToString()
            }, new string[] {"filterText"});
        }

        private async void btnCompReport_Click(object sender, EventArgs e)
        {
            IUnitOfWork unitOfWork = new UnitOfWork();
            var row = (DeliveriesViewModel)gridDeliveries.GetFocusedRow();
            var del = await unitOfWork.DeliveriesRepo.FindAsync(x => x.Id == row.Deliveries.Id,
                x => x.Supplier,
                x => x.TicketRequest,
                x => x.TicketRequest.ITStaff,
                x => x.TicketRequest.ITStaff.Users,
                x => x.DeliveriesSpecs.Select(s => s.Model),
                x => x.DeliveriesSpecs.Select(s => s.Model.Brand),
                x => x.DeliveriesSpecs.Select(s => s.Model.Brand.EquipmentSpecs),
                x => x.DeliveriesSpecs.Select(s => s.Model.Brand.EquipmentSpecs.Equipment));

            if (del == null) return;

            var inspectActions = del?.Actions?.Where(x => x.SubActivityId == 1138 || x.SubActivityId == 1139).OrderBy(x => x.ActionDate);
            if (inspectActions == null || inspectActions.Count() <= 0)
            {
                MessageBox.Show("Technical Inspection is not yet Started on this Delivery!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if(UserStore.UserId != del.TicketRequest.ITStaff.UserId)
            {
                await PrintComparisonReport();
                return;
            }

            var frm = new frmComparisonReport(del);    
            frm.ShowDialog();
        }

        private void FilterGrid()
        {
            gridDeliveries.ActiveFilterCriteria = null;
            var row = (Users)slueTaskOf.Properties.View.GetFocusedRow();
            var dateFrom = deFrom.DateTime;
            var dateTo = deTo.DateTime;

            var criteria = gridDeliveries.ActiveFilterCriteria;
            if (slueTaskOf.EditValue != null) criteria = GroupOperator.And(criteria, new BinaryOperator("Deliveries.TicketRequest.ITStaff.UserId", row.Id));
            if (deFrom.EditValue != null && deTo.EditValue != null)
            {
                var fromFilter = new BinaryOperator("Deliveries.DateRequested", dateFrom, BinaryOperatorType.GreaterOrEqual);
                var toFilter = new BinaryOperator("Deliveries.DateRequested", dateTo, BinaryOperatorType.LessOrEqual);
                criteria = GroupOperator.And(criteria, GroupOperator.And(fromFilter, toFilter));
            }
            gridDeliveries.ActiveFilterCriteria = criteria;
        }

        private void slueTaskOf_EditValueChanged(object sender, EventArgs e)
        {
            FilterGrid();
        }

        private void btnFilterbyDate_Click(object sender, EventArgs e)
        {
            FilterGrid();
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

        private async void btnFindings_Click(object sender, EventArgs e)
        {
            var row = (DeliveriesViewModel)gridDeliveries.GetFocusedRow();
            var frm = new frmInitialFindings(row);
            frm.ShowDialog();

            await LoadDeliveries();
        }
    }
}
