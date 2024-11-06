using DevExpress.XtraLayout;
using ICTProfilingV3.ActionsForms;
using Models.Entities;
using Models.Enums;
using Models.HRMISEntites;
using Models.Models;
using Models.Repository;
using Models.ViewModels;
using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ICTProfilingV3.DeliveriesForms
{
    public partial class UCDeliveries : DevExpress.XtraEditors.XtraUserControl
    {
        private IUnitOfWork _unitOfWork;
        public UCDeliveries()
        {
            InitializeComponent();
            _unitOfWork = new UnitOfWork();
            LoadDeliveries();
        }

        private Task LoadDeliveries()
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
                PONo = x.PONo
            });
            gcDeliveries.DataSource = new BindingList<DeliveriesViewModel>(deliveries.ToList());
            return Task.CompletedTask;
        }

        private async void gridDeliveries_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            await LoadEquipmentSpecs();
            LoadActions();
            await LoadDetails();
        }

        private async Task LoadDetails()
        {
            var row = (DeliveriesViewModel)gridDeliveries.GetFocusedRow();

            var deliveriesDetails = await _unitOfWork.DeliveriesRepo.FindAsync(x => x.Id == row.Id , x => x.Supplier);
            var requestingEmployee = HRMISEmployees.GetEmployees().FirstOrDefault(x => x.Id == deliveriesDetails.RequestedById);
            txtChief.Text = HRMISEmployees.GetEmployeeById(requestingEmployee.ChiefOfOffices.ChiefId).Employee;
            lblEpisNo.Text = deliveriesDetails.Id.ToString();
            txtOffice.Text = string.Join("-", requestingEmployee.Office, requestingEmployee.Division);
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
            var deliveries = await _unitOfWork.DeliveriesRepo.FindAsync(x => x.Id == row.Id , x => x.DeliveriesSpecs);
            tabEquipmentSpecs.Controls.Clear();
            tabEquipmentSpecs.Controls.Add(new UCDeliveriesSpecs(deliveries, _unitOfWork)
            {
                Dock = DockStyle.Fill
            });
        }
        
        private void LoadActions()
        {
            var row = (DeliveriesViewModel)gridDeliveries.GetFocusedRow();
            tabAction.Controls.Clear();
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
            var row = (DeliveriesViewModel)gridDeliveries.GetFocusedRow();
            var deliveries = await _unitOfWork.DeliveriesRepo.FindAsync(x => x.Id == row.Id);
            var frm = new frmAddEditDeliveries(deliveries, _unitOfWork);
            frm.ShowDialog();

            await LoadDeliveries();
            await LoadDetails();
            await LoadEquipmentSpecs();
        }

        private void btnPreview_Click(object sender, EventArgs e)
        {

        }
    }
}
