using ICTProfilingV3.ActionsForms;
using ICTProfilingV3.BaseClasses;
using ICTProfilingV3.Core.Common;
using ICTProfilingV3.DataTransferModels.Models;
using ICTProfilingV3.DataTransferModels.ViewModels;
using ICTProfilingV3.Interfaces;
using ICTProfilingV3.LookUpTables;
using ICTProfilingV3.Services.Employees;
using ICTProfilingV3.ToolForms;
using Microsoft.Extensions.DependencyInjection;
using Models.Entities;
using Models.Enums;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ICTProfilingV3.DeliveriesForms
{
    public partial class frmAddEditDeliveries : BaseForm
    {
        private Deliveries _deliveries;
        private bool _isSave = false;
        private SaveType _saveType;
        private EmployeesViewModel _ofmisEmployee;

        private readonly UserStore _userStore;
        private readonly IServiceProvider _serviceProvider;
        private readonly IDeliveriesService _deliveriesService;
        private readonly IControlMapper<Deliveries> _deliveriesMapper;
        private readonly ILookUpService _lookUpService;

        public frmAddEditDeliveries(UserStore userStore, IServiceProvider serviceProvider, IDeliveriesService deliveriesService, 
            IControlMapper<Deliveries> deliveriesMapper, ILookUpService lookUpService)
        {
            _userStore = userStore;
            _serviceProvider = serviceProvider;
            _deliveriesService = deliveriesService;
            _deliveriesMapper = deliveriesMapper;
            _lookUpService = lookUpService;
            InitializeComponent();

            LoadDropdowns();
        }

        public async Task InitForm(Deliveries deliveries = null)
        {
            if (deliveries == null)
            {
                _saveType = SaveType.Insert;
                await CreateTicket();
            }
            else
            {
                _saveType = SaveType.Update;
                _deliveries = deliveries;
                LoadEquipmentSpecs();
            }
        }

        private void LoadDetails()
        {
            if (_saveType == SaveType.Insert) return;

            _deliveriesMapper.MapControl(_deliveries, groupControl1, groupControl2);
            //txtDateRequested.DateTime = _deliveries.DateRequested ?? DateTime.Now;
            //slueRequestedById.EditValue = _deliveries.RequestedById;
            //rdbtnGender.SelectedIndex = (int)_deliveries.Gender;
            //txtContactNo.Text = _deliveries.ContactNo;
            //txtPONo.Text = _deliveries.PONo;
            //slueDeliveredById.EditValue = _deliveries.DeliveredById;
            //slueSupplierId.EditValue = _deliveries.SupplierId;
            //txtDeliveredDate.DateTime = _deliveries.DeliveredDate ?? DateTime.Now;
            //txtReceiptNo.Text = _deliveries.ReceiptNo;
        }
        
        private async Task CreateTicket()
        {
            var deliveries = await _deliveriesService.AddAsync(new Deliveries());
            _deliveries = deliveries;
            LoadEquipmentSpecs();
        }

        private void LoadDropdowns()
        {
            var suppliers = _lookUpService .SupplierDataService.GetAll();
            slueSupplierId.Properties.DataSource = suppliers.ToList();
            var employees = HRMISEmployees.GetEmployees();
            slueRequestedById.Properties.DataSource = employees;
            slueDeliveredById.Properties.DataSource = employees;
        }

        private void LoadEquipmentSpecs()
        {
            var navigation = _serviceProvider.GetRequiredService<IControlNavigator<UCDeliveriesSpecs>>();
            navigation.NavigateTo(panelEquipmentSpecs, act => act.InitUC(_deliveries.Id, false));
        }

        private async void frmAddEditDeliveries_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            if (!_isSave && _saveType == SaveType.Insert) 
                await DeleteDeliveries();
        }

        private async Task DeleteDeliveries()
        {
            await _deliveriesService.DeleteAsync(_deliveries.Id);
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            _isSave = true;
            await SaveDeliveries();
            this.Close();

            if (_saveType == SaveType.Update) return;
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
            //var deliveries = await unitOfWork.DeliveriesRepo.FindAsync(x => x.Id == _deliveries.Id);
            var deliveries = await _deliveriesService.GetByIdAsync(_deliveries.Id);

            if (deliveries == null) return;
            _deliveriesMapper.MapToEntity(deliveries, groupControl1, groupControl2);
            await _deliveriesService.SaveChangesAsync();
            
            //deliveries.DateRequested = txtDateRequested.DateTime;
            //deliveries.RequestedById = (long)(slueRequestedById.EditValue == null ? _ofmisEmployee.Id : slueRequestedById.EditValue);
            //deliveries.Gender = (Gender)rdbtnGender.SelectedIndex;
            //deliveries.ContactNo = txtContactNo.Text;
            //deliveries.DeliveredById = (long)slueDeliveredById.EditValue;
            //deliveries.SupplierId = (int?)slueSupplierId.EditValue;
            //deliveries.DeliveredDate = txtDeliveredDate.DateTime;  
            //deliveries.PONo = txtPONo.Text;
            //deliveries.ReceiptNo = txtReceiptNo.Text;
            //unitOfWork.DeliveriesRepo.Update(deliveries);

            //await unitOfWork.SaveChangesAsync();
        }

        private void btnAddSupplier_Click(object sender, EventArgs e)
        {
            var frm = _serviceProvider.GetRequiredService<frmSuppliers>();
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
            var frm = _serviceProvider.GetRequiredService<frmSelectOFMISEmployee>();
            frm.ShowDialog();
            if (frm.OFMISEmployee == null)
                return;
            txtRequestedBy.Text = frm.OFMISEmployee.Employee;
            _ofmisEmployee = frm.OFMISEmployee;
        }

        private void slueEmployee_EditValueChanged(object sender, EventArgs e)
        {
            var row = (EmployeesViewModel)slueRequestedById.Properties.View.GetFocusedRow();
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