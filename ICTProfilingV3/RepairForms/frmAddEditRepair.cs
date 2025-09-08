using ICTProfilingV3.ActionsForms;
using ICTProfilingV3.BaseClasses;
using ICTProfilingV3.Core.Common;
using ICTProfilingV3.DataTransferModels.Models;
using ICTProfilingV3.DataTransferModels.ViewModels;
using ICTProfilingV3.Interfaces;
using ICTProfilingV3.PPEInventoryForms;
using ICTProfilingV3.Services.Employees;
using Microsoft.Extensions.DependencyInjection;
using Models.Entities;
using Models.Enums;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ICTProfilingV3.RepairForms
{
    public partial class frmAddEditRepair : BaseForm
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IRepairService _repairService;
        private readonly IPPEInventoryService _ppeInventoryService;
        private readonly IProcessService _processService;
        private readonly UserStore _userStore;

        private SaveType _saveType;
        private Repairs _repair;
        private int _repairId;
        private bool _isSave = false;

        public frmAddEditRepair(UserStore userStore, IServiceProvider serviceProvider, IRepairService repairService, 
            IPPEInventoryService ppeInventoryService, IProcessService processService)
        {
            _userStore = userStore;
            _serviceProvider = serviceProvider;
            _ppeInventoryService = ppeInventoryService;
            _repairService = repairService;
            _processService = processService;

            InitializeComponent();
            LoadDropdowns();
        }

        public async Task InitForm(int? repair = null)
        {
            if(repair == null)
            {
                _saveType = SaveType.Insert;
                await CreateTicket();
            }
            else
            {
                _saveType = SaveType.Update;
                _isSave = true;
                _repairId = repair.Value;
            }
        }

        private async Task LoadDetails()
        {
            var repair = await _repairService.GetByIdAsync(_repairId);    
            _repair = repair;

            if(repair.DateCreated != null) txtDate.DateTime = (DateTime)repair.DateCreated;
            slueEmployee.EditValue = repair.RequestedById;
            rdbtnGender.SelectedIndex = (int)repair.Gender;
            txtContactNo.Text = repair.ContactNo;
            if (repair.DateDelivered != null) txtDateofDelivery.DateTime = (DateTime)repair.DateDelivered;
            slueDeliveredBy.EditValue = repair.DeliveredById;
            sluePropertyNo.EditValue = repair.PPEsId;
            txtRequestProblem.Text = repair.Problems;
        }

        private async Task CreateTicket()
        {
            var repair = await _repairService.AddAsync(new Repairs());
            _repair = repair;
        }

        private void LoadDropdowns()
        {
            var employees = HRMISEmployees.GetEmployees();
            slueEmployee.Properties.DataSource = employees;
            slueDeliveredBy.Properties.DataSource = employees;

            txtDate.DateTime = DateTime.Now;
            txtDateofDelivery.DateTime = DateTime.Now;
        }
        private async Task LoadPPEs()
        {
            var ppe = await _ppeInventoryService.GetAll().ToListAsync();
            var ppeModel = ppe.Select(x => new PPEsViewModel
            {
                Id = x.Id,
                PropertyNo = x.PropertyNo,
                IssuedTo = HRMISEmployees.GetEmployeeById(x.IssuedToId)?.Employee,
                DateCreated = x.DateCreated,
                Office = HRMISEmployees.GetEmployeeById(x.IssuedToId)?.Office,
                Status = x?.Status
            }).ToList();

            sluePropertyNo.Properties.DataSource = ppeModel;
        }

        private async void btnAddPPE_Click(object sender, System.EventArgs e)
        {
            var frm = _serviceProvider.GetRequiredService<frmAddEditPPEs>();
            await frm.InitForm(SaveType.Insert, null);
            frm.ShowDialog();

            await LoadPPEs();
        }

        private async void sluePropertyNo_EditValueChanged(object sender, EventArgs e)
        {
            await LoadEquipmentSpecs();
        }

        private async Task LoadEquipmentSpecs()
        {
            var row = (PPEsViewModel)sluePropertyNo.Properties.View.GetFocusedRow();

            int repId;
            if (row == null) repId = (int)_repair.PPEsId;
            else repId = row.Id;

            var ppe = await _ppeInventoryService.GetByIdAsync(repId);

            var navigation = _serviceProvider.GetRequiredService<IControlNavigator<UCPPEsSpecs>>();
            navigation.NavigateTo(gcEquipmentSpecs, act => act.InitUC(ppe, forViewing: true));

            txtIssuedTo.Text = HRMISEmployees.GetEmployeeById(ppe.IssuedToId)?.Employee;
            txtOffAcr.Text = HRMISEmployees.GetEmployeeById(ppe.IssuedToId)?.Office;
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            await Save();
            _isSave = true;
            this.Close();
            if (_saveType == SaveType.Update) return;
            var actionType = new ActionType
            {
                Id = _repair.Id,
                RequestType = RequestType.Repairs
            };

            var frm = _serviceProvider.GetRequiredService<frmDocAction>();
            frm.SetActionBehavior(actionType, SaveType.Insert, null, null);
            frm.ShowDialog();
        }
        private async Task Save()
        {
            var reqEmployee = HRMISEmployees.GetEmployeeById((long?)slueEmployee.EditValue);
            var repair = await _repairService.GetByIdAsync(_repair.Id);
            repair.RequestedById = (long)slueEmployee.EditValue;
            repair.ReqByChiefId = HRMISEmployees.GetChief(reqEmployee.Office, reqEmployee.Division, (long?)slueEmployee.EditValue).ChiefId;
            repair.DeliveredById = (long)slueDeliveredBy.EditValue;
            repair.Problems = txtRequestProblem.Text;
            repair.Gender = (Gender)rdbtnGender.SelectedIndex;
            repair.ContactNo = txtContactNo.Text;
            repair.PPEsId = (int?)sluePropertyNo.EditValue;
            repair.DateCreated = txtDate.DateTime;
            repair.DateDelivered = txtDateofDelivery.DateTime;
            await _repairService.SaveChangesAsync();
            await ModifyTicketStatusStatus(TicketStatus.Accepted, repair.Id);
        }

        private async void frmAddEditRepair_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!_isSave) await DeleteRepair();
        }

        private async Task DeleteRepair()
        {
            await _repairService.DeleteAsync(_repair.Id);
        }

        private async void frmAddEditRepair_Load(object sender, EventArgs e)
        {
            await LoadPPEs();
            if (_saveType == SaveType.Update) await LoadDetails();
        }

        private void groupControl3_Paint(object sender, PaintEventArgs e)
        {

        }

        public async Task ModifyTicketStatusStatus(TicketStatus status, int Id)
        {
            await _processService.AddProcessLog(Id, RequestType.Repairs, status);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}