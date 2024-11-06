using ICTProfilingV3.LookUpTables;
using ICTProfilingV3.TicketRequestForms;
using Models.Entities;
using Models.Enums;
using Models.HRMISEntites;
using Models.Repository;
using Models.ViewModels;
using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ICTProfilingV3.DeliveriesForms
{
    public partial class frmAddEditDeliveries : DevExpress.XtraEditors.XtraForm
    {
        private IUnitOfWork unitOfWork;
        private Models.Entities.Deliveries _deliveries;
        private bool IsSave = false;
        private SaveType SaveType;

        public frmAddEditDeliveries()
        {
            InitializeComponent();
            SaveType = SaveType.Insert;
            unitOfWork = new UnitOfWork();
            LoadDropdowns();
            CreateTicket();
        }
        public frmAddEditDeliveries(Deliveries deliveries , IUnitOfWork uow)
        {
            InitializeComponent();
            SaveType = SaveType.Update;
            _deliveries = deliveries;
            IsSave = true;
            unitOfWork = uow;
            LoadDropdowns();
            LoadEquipmentSpecs();
        }

        private void LoadDetails()
        {
            if (SaveType == SaveType.Insert) return;

            txtDate.DateTime = _deliveries.DateRequested ?? DateTime.UtcNow;
            slueEmployee.EditValue = _deliveries.RequestedById;
            rdbtnGender.SelectedIndex = (int)_deliveries.Gender;
            txtContactNo.Text = _deliveries.ContactNo;
            txtPONo.Text = _deliveries.PONo;
            slueDeliveredBy.EditValue = _deliveries.DeliveredById;
            slueSupplierName.EditValue = _deliveries.SupplierId;
            txtDateofDelivery.DateTime = _deliveries.DeliveredDate ?? DateTime.UtcNow;
            txtDeliveryReceipt.Text = _deliveries.ReceiptNo;
        }
        
        private void CreateTicket()
        {
            var ticket = new TicketRequest()
            {
                DateCreated = DateTime.UtcNow,
                TicketStatus = TicketStatus.Accepted
            };
            unitOfWork.TicketRequestRepo.Insert(ticket);
            unitOfWork.Save();

            var deliveries = new Deliveries()
            {
                Id = ticket.Id
            };
            unitOfWork.DeliveriesRepo.Insert(deliveries);
            unitOfWork.Save();

            _deliveries = deliveries;
            LoadEquipmentSpecs();
        }

        private void LoadDropdowns()
        {
            slueSupplierName.Properties.DataSource = unitOfWork.SupplierRepo.GetAll().ToList();
            var employees = HRMISEmployees.GetEmployees();
            slueEmployee.Properties.DataSource = employees;
            slueDeliveredBy.Properties.DataSource = employees;
        }

        private void LoadEquipmentSpecs()
        {
            panelEquipmentSpecs.Controls.Clear();
            panelEquipmentSpecs.Controls.Add(new UCDeliveriesSpecs(_deliveries , unitOfWork)
            {
                Dock = DockStyle.Fill
            });
        }

        private void frmAddEditDeliveries_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            if (!IsSave) DeleteDeliveries();
        }

        private void DeleteDeliveries()
        {
            unitOfWork.DeliveriesSpecsRepo.DeleteRange(x => x.DeliveriesId == _deliveries.Id);
            unitOfWork.Save();

            unitOfWork.DeliveriesRepo.Delete(_deliveries);
            unitOfWork.Save();
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            IsSave = true;
            await SaveDeliveries();
            this.Close();
        }

        private async Task SaveDeliveries()
        {
            var deliveries = await unitOfWork.DeliveriesRepo.FindAsync(x => x.Id == _deliveries.Id);
            deliveries.DateRequested = txtDate.DateTime;
            deliveries.RequestedById = (long)slueEmployee.EditValue;
            deliveries.Gender = (Gender)rdbtnGender.SelectedIndex;
            deliveries.ContactNo = txtContactNo.Text;
            deliveries.DeliveredById = (long)slueDeliveredBy.EditValue;
            deliveries.SupplierId = (int?)slueSupplierName.EditValue;
            deliveries.DeliveredDate = txtDateofDelivery.DateTime;  
            deliveries.PONo = txtPONo.Text;
            deliveries.ReceiptNo = txtDeliveryReceipt.Text;

            unitOfWork.Save();
        }

        private void btnAddSupplier_Click(object sender, EventArgs e)
        {
            var frm = new frmSuppliers();
            frm.ShowDialog();

            LoadDropdowns();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmAddEditDeliveries_Load(object sender, EventArgs e)
        {
            LoadDetails();
        }
    }
}