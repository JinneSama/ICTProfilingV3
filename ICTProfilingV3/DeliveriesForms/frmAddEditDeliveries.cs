using ICTProfilingV3.ActionsForms;
using ICTProfilingV3.BaseClasses;
using ICTProfilingV3.Core.Common;
using ICTProfilingV3.DataTransferModels.Models;
using ICTProfilingV3.DataTransferModels.ViewModels;
using ICTProfilingV3.LookUpTables;
using ICTProfilingV3.Services.Employees;
using ICTProfilingV3.ToolForms;
using Microsoft.Extensions.DependencyInjection;
using Models.Entities;
using Models.Enums;
using Models.Repository;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ICTProfilingV3.DeliveriesForms
{
    public partial class frmAddEditDeliveries : BaseForm
    {
        private IUnitOfWork unitOfWork;
        private Deliveries _deliveries;
        private bool IsSave = false;
        private SaveType SaveType;
        private EmployeesViewModel ofmisEmployee;

        private readonly UserStore _userStore;
        private readonly IServiceProvider _serviceProvider;

        public frmAddEditDeliveries(UserStore userStore, IServiceProvider serviceProvider)
        {
            _userStore = userStore;
            _serviceProvider = serviceProvider;
            InitializeComponent();
            unitOfWork = new UnitOfWork();
            LoadDropdowns();
        }

        public void InitForm(Deliveries deliveries = null)
        {
            if (deliveries == null)
            {
                SaveType = SaveType.Insert;
                CreateTicket();
            }
            else
            {
                SaveType = SaveType.Update;
                _deliveries = deliveries;
                LoadEquipmentSpecs();
            }
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
                CreatedBy = _userStore.UserId
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
            panelEquipmentSpecs.Controls.Add(new UCDeliveriesSpecs(_deliveries, false)
            {
                Dock = DockStyle.Fill
            });
        }

        private async void frmAddEditDeliveries_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            if (!IsSave && SaveType == SaveType.Insert) 
                await DeleteDeliveries();
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
            var actionType = new ActionType
            {
                Id = _deliveries.Id,
                RequestType = RequestType.Deliveries
            };

            var frm = _serviceProvider.GetRequiredService<frmDocAction>();
            frm.SetActionBehavior(actionType, SaveType.Insert, null, null);
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
            unitOfWork.DeliveriesRepo.Update(deliveries);

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
            if (frm.OFMISEmployee == null)
                return;
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