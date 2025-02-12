using ICTProfilingV3.ActionsForms;
using ICTProfilingV3.BaseClasses;
using ICTProfilingV3.LookUpTables;
using ICTProfilingV3.ToolForms;
using Models.Entities;
using Models.Enums;
using Models.HRMISEntites;
using Models.Managers;
using Models.Managers.User;
using Models.Repository;
using Models.ViewModels;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ICTProfilingV3.DeliveriesForms
{
    public partial class frmAddEditDeliveries : BaseForm
    {
        private IUnitOfWork unitOfWork;
        private Models.Entities.Deliveries _deliveries;
        private bool IsSave = false;
        private SaveType SaveType;
        private EmployeesViewModel ofmisEmployee;

        public frmAddEditDeliveries()
        {
            InitializeComponent();
            SaveType = SaveType.Insert;
            unitOfWork = new UnitOfWork();
            LoadDropdowns();
            CreateTicket();
        }
        public frmAddEditDeliveries(Deliveries deliveries)
        {
            InitializeComponent();
            SaveType = SaveType.Update;
            _deliveries = deliveries;
            IsSave = true;
            unitOfWork = new UnitOfWork();
            LoadDropdowns();
            LoadEquipmentSpecs();
        }

        private void LoadDetails()
        {
            if (SaveType == SaveType.Insert) return;

            txtDate.DateTime = _deliveries.DateRequested ?? DateTime.Now;
            slueEmployee.EditValue = _deliveries.RequestedById;
            rdbtnGender.SelectedIndex = (int)_deliveries.Gender;
            txtContactNo.Text = _deliveries.ContactNo;
            txtPONo.Text = _deliveries.PONo;
            slueDeliveredBy.EditValue = _deliveries.DeliveredById;
            slueSupplierName.EditValue = _deliveries.SupplierId;
            txtDateofDelivery.DateTime = _deliveries.DeliveredDate ?? DateTime.Now;
            txtDeliveryReceipt.Text = _deliveries.ReceiptNo;
        }
        
        private void CreateTicket()
        {
            var ticket = new TicketRequest()
            {
                DateCreated = DateTime.Now,
                TicketStatus = TicketStatus.Accepted,
                RequestType = RequestType.Deliveries,
                CreatedBy = UserStore.UserId
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
            panelEquipmentSpecs.Controls.Add(new UCDeliveriesSpecs(_deliveries)
            {
                Dock = DockStyle.Fill
            });
        }

        private async void frmAddEditDeliveries_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            if (!IsSave) await DeleteDeliveries();
        }

        private async Task DeleteDeliveries()
        {
            unitOfWork.TicketRequestRepo.DeleteByEx(x => x.Id == _deliveries.Id);
            await unitOfWork.SaveChangesAsync();

            unitOfWork.DeliveriesSpecsRepo.DeleteRange(x => x.DeliveriesId == _deliveries.Id);
            await unitOfWork.SaveChangesAsync();

            unitOfWork.DeliveriesRepo.DeleteByEx(x => x.Id == _deliveries.Id);
            await unitOfWork.SaveChangesAsync();
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            IsSave = true;
            await SaveDeliveries();
            this.Close();

            if (SaveType == SaveType.Update) return;
            var actionType = new Models.Models.ActionType
            {
                Id = _deliveries.Id,
                RequestType = RequestType.Deliveries
            };

            var frm = new frmDocAction(actionType, SaveType.Insert, null, unitOfWork, null);
            frm.ShowDialog();
        }

        private async Task SaveDeliveries()
        {
            var deliveries = await unitOfWork.DeliveriesRepo.FindAsync(x => x.Id == _deliveries.Id);
            if (deliveries == null) return;
            deliveries.DateRequested = txtDate.DateTime;
            deliveries.RequestedById = (long)(slueEmployee.EditValue == null ? ofmisEmployee.Id : slueEmployee.EditValue);
            deliveries.Gender = (Gender)rdbtnGender.SelectedIndex;
            deliveries.ContactNo = txtContactNo.Text;
            deliveries.DeliveredById = (long)slueDeliveredBy.EditValue;
            deliveries.SupplierId = (int?)slueSupplierName.EditValue;
            deliveries.DeliveredDate = txtDateofDelivery.DateTime;  
            deliveries.PONo = txtPONo.Text;
            deliveries.ReceiptNo = txtDeliveryReceipt.Text;

            await unitOfWork.SaveChangesAsync();
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

        private void btnOFMIS_Click(object sender, EventArgs e)
        {
            var frm = new frmSelectOFMISEmployee();
            frm.ShowDialog();

            txtRequestedBy.Text = frm.OFMISEmployee.Employee;
            ofmisEmployee = frm.OFMISEmployee;
        }

        private void slueEmployee_EditValueChanged(object sender, EventArgs e)
        {
            var row = (EmployeesViewModel)slueEmployee.Properties.View.GetFocusedRow();
            if (row == null)
            {
                txtRequestedBy.Visible = false;
                return;
            }

            txtRequestedBy.Visible = true;
            txtRequestedBy.Text = row.Employee;
        }
    }
}