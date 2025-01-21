using DevExpress.Data.Filtering;
using DevExpress.XtraRichEdit.Import.OpenXml;
using Helpers.NetworkFolder;
using ICTMigration.ICTv2Models;
using ICTProfilingV3.ActionsForms;
using ICTProfilingV3.ReportForms;
using ICTProfilingV3.TicketRequestForms;
using ICTProfilingV3.ToolForms;
using Models.Entities;
using Models.Enums;
using Models.HRMISEntites;
using Models.Models;
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
    public partial class UCDeliveries : DevExpress.XtraEditors.XtraUserControl
    {
        private IUnitOfWork _unitOfWork;
        private HTTPNetworkFolder networkFolder;

        public string filterText { get; set; }
        public UCDeliveries()
        {
            InitializeComponent();
            networkFolder = new HTTPNetworkFolder();
            _unitOfWork = new UnitOfWork();
        }

        private void LoadDropdown()
        {
            var staff = _unitOfWork.UsersRepo.GetAll().ToList();
            var res = staff.OrderBy(o => o.FullName);

            slueTaskOf.Properties.DataSource = res.ToList();
        }

        private async Task LoadDeliveries()
        {
            var deliveries = await _unitOfWork.DeliveriesRepo.FindAllAsync(x => x.TicketRequest.ITStaff != null,
                x => x.Supplier,
                x => x.TicketRequest,
                x => x.TicketRequest.ITStaff,
                x => x.Actions).OrderByDescending(x => x.DateRequested).ToListAsync();

            var delData = deliveries.Select(x => new DeliveriesViewModel
            {
                Id = x.Id,
                Status = x.TicketRequest.TicketStatus,
                TicketNo = x.TicketRequest.Id,
                Office = HRMISEmployees.GetEmployeeById(x.RequestedById)?.Office,
                Supplier = x.Supplier.SupplierName,
                DeliveryId = "EPiS-" + x.Id,
                PONo = x.PONo,
                Deliveries = x,
                RecordedBy = x?.Actions?.OrderBy(o => o.ActionDate)?.FirstOrDefault()?.CreatedById
            });
            gcDeliveries.DataSource = new BindingList<DeliveriesViewModel>(delData.ToList());
        }

        private async Task LoadStaff()
        {
            var row = (DeliveriesViewModel)gridDeliveries.GetFocusedRow();
            ITStaff staff = null;
            if(row != null) staff = await _unitOfWork.ITStaffRepo.FindAsync(x => x.Id == row.Deliveries.TicketRequest.StaffId, x => x.Users);
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

        private void LoadDetails()
        {
            _unitOfWork = new UnitOfWork();
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

        private async Task LoadEquipmentSpecs()
        {
            var row = (DeliveriesViewModel)gridDeliveries.GetFocusedRow();
            if (row == null) return;

            IUnitOfWork uow = new UnitOfWork();
            var deliveries = await uow.DeliveriesRepo.FindAsync(x => x.Id == row.Id , x => x.DeliveriesSpecs);

            if(deliveries == null) return;

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
            var rowHandle = gridDeliveries.FocusedRowHandle;
            var row = (DeliveriesViewModel)gridDeliveries.GetFocusedRow();
            var deliveries = await _unitOfWork.DeliveriesRepo.FindAsync(x => x.Id == row.Id);
            var frm = new frmAddEditDeliveries(deliveries);
            frm.ShowDialog();

            await LoadDeliveries();
            LoadDetails();
            await LoadEquipmentSpecs();

            gridDeliveries.FocusedRowHandle = rowHandle;
        }

        private void btnPreview_Click(object sender, EventArgs e)
        {
            var item = (DeliveriesViewModel)gridDeliveries.GetFocusedRow();
            var ds = _unitOfWork.DeliveriesRepo.FindAllAsync(x => x.Id == item.Id,
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

        private async void UCDeliveries_Load(object sender, EventArgs e)
        {
            await LoadDeliveries();
            LoadDropdown();
            if (filterText != null) gridDeliveries.ActiveFilterCriteria = new BinaryOperator("TicketNo", filterText);
        }

        private async void gridDeliveries_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            await LoadEquipmentSpecs();
            LoadActions();
            LoadDetails();
            await LoadStaff();
        }

        private void hplTicket_Click(object sender, EventArgs e)
        {
            var row = (DeliveriesViewModel)gridDeliveries.GetFocusedRow();
            var main = Application.OpenForms["frmMain"] as frmMain;
            main.mainPanel.Controls.Clear();

            main.mainPanel.Controls.Add(new UCTARequestDashboard()
            {
                Dock = DockStyle.Fill,
                filterText = row.Id.ToString()
            });
        }

        private async void btnCompReport_Click(object sender, EventArgs e)
        {
            var row = (DeliveriesViewModel)gridDeliveries.GetFocusedRow();
            var del = await _unitOfWork.DeliveriesRepo.FindAsync(x => x.Id == row.Deliveries.Id,
                x => x.Supplier,
                x => x.TicketRequest,
                x => x.DeliveriesSpecs.Select(s => s.Model),
                x => x.DeliveriesSpecs.Select(s => s.Model.Brand),
                x => x.DeliveriesSpecs.Select(s => s.Model.Brand.EquipmentSpecs),
                x => x.DeliveriesSpecs.Select(s => s.Model.Brand.EquipmentSpecs.Equipment));

            if (del == null) return;

            var inspectActions = del?.Actions?.Where(x => x.SubActivityId == 1138).OrderBy(x => x.ActionDate);
            if (inspectActions == null || inspectActions.Count() <= 0)
            {
                MessageBox.Show("Technical Inspection is not yet Started on this Delivery!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
    }
}
