using DevExpress.Data.Filtering;
using Helpers.NetworkFolder;
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
            LoadDeliveries();
        }

        private void LoadDeliveries()
        {
            var deliveries = _unitOfWork.DeliveriesRepo.GetAll(x => x.Supplier,
                x => x.TicketRequest).ToList().Select(x => new DeliveriesViewModel
            {
                Id = x.Id,
                Status = x.TicketRequest.TicketStatus,
                TicketNo = x.TicketRequest.Id,
                Office = HRMISEmployees.GetEmployees().FirstOrDefault(h => h.Id == x.RequestedById).Office,
                Supplier = x.Supplier.SupplierName,
                DeliveryId = "EPiS-" + x.Id,
                PONo = x.PONo,
                Deliveries = x
            });
            gcDeliveries.DataSource = new BindingList<DeliveriesViewModel>(deliveries.ToList());
        }

        private async void LoadStaff()
        {
            var row = (DeliveriesViewModel)gridDeliveries.GetFocusedRow();
            var staff = await _unitOfWork.ITStaffRepo.FindAsync(x => x.Id == row.Deliveries.TicketRequest.StaffId, x => x.Users);
            Image img = await networkFolder.DownloadFile(staff?.UserId + ".jpeg");
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

        private void LoadDetails()
        {
            _unitOfWork = new UnitOfWork();
            var row = (DeliveriesViewModel)gridDeliveries.GetFocusedRow();
            if (row == null) return;

            spbTicketStatus.SelectedItemIndex = ((int)row.Status) - 1;
            var deliveriesDetails = row.Deliveries;
            var requestingEmployee = HRMISEmployees.GetEmployees().FirstOrDefault(x => x.Id == deliveriesDetails.RequestedById);
            txtChief.Text = HRMISEmployees.GetEmployeeById(requestingEmployee.ChiefOfOffices.ChiefId).Employee;
            lblEpisNo.Text = deliveriesDetails.Id.ToString();
            txtOffice.Text = string.Join(" ", requestingEmployee.Office, requestingEmployee.Division);
            txtReqBy.Text = requestingEmployee.Employee;
            txtTel.Text = deliveriesDetails.ContactNo;
            txtDeliveredBy.Text = HRMISEmployees.GetEmployeeById(deliveriesDetails.DeliveredById).Employee;
            txtSupplierName.Text = deliveriesDetails.Supplier?.SupplierName;
            txtSupplierAddress.Text = deliveriesDetails.Supplier?.Address;
            txtSupplierTelNo.Text = deliveriesDetails.Supplier?.TelNumber;
            dtDeliveredDate.DateTime = deliveriesDetails.DeliveredDate ?? DateTime.MinValue;
        }

        private async Task LoadEquipmentSpecs()
        {
            var row = (DeliveriesViewModel)gridDeliveries.GetFocusedRow();
            tabEquipmentSpecs.Controls.Clear();

            if (row == null) return;

            var deliveries = await _unitOfWork.DeliveriesRepo.FindAsync(x => x.Id == row.Id , x => x.DeliveriesSpecs);
            tabEquipmentSpecs.Controls.Add(new UCDeliveriesSpecs(deliveries, _unitOfWork)
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

            LoadDeliveries();
            LoadDetails();
            await LoadEquipmentSpecs();

            gridDeliveries.FocusedRowHandle = rowHandle;
        }

        private void btnPreview_Click(object sender, EventArgs e)
        {
            var item = (DeliveriesViewModel)gridDeliveries.GetFocusedRow();
            var ds = _unitOfWork.DeliveriesRepo.FindAllAsync(x => x.Id == item.Id,
                x => x.DeliveriesSpecs.Select(s => s.Model),
                x => x.DeliveriesSpecs.Select(s => s.Model.Brand),
                x => x.DeliveriesSpecs.Select(s => s.Model.Brand.EquipmentSpecs),
                x => x.DeliveriesSpecs.Select(s => s.Model.Brand.EquipmentSpecs.Equipment)).ToList();
            var rpt = new rptDeliveries
            {
                DataSource = ds
            };

            var frm = new frmReportViewer(rpt);
            frm.ShowDialog();
        }

        private void UCDeliveries_Load(object sender, EventArgs e)
        {
            if (filterText != null) gridDeliveries.ActiveFilterCriteria = new BinaryOperator("TicketNo", filterText);
        }

        private async void gridDeliveries_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            await LoadEquipmentSpecs();
            LoadActions();
            LoadDetails();
            LoadStaff();
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
            var frm = new frmComparisonReport(del);    
            frm.ShowDialog();
        }
    }
}
