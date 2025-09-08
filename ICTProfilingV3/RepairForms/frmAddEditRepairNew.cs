using ICTProfilingV3.BaseClasses;
using ICTProfilingV3.Interfaces;
using ICTProfilingV3.PPEInventoryForms;
using ICTProfilingV3.Services.Employees;
using Microsoft.Extensions.DependencyInjection;
using Models.Entities;
using System;
using System.Threading.Tasks;

namespace ICTProfilingV3.RepairForms
{
    public partial class frmAddEditRepairNew : BaseForm
    {
        private readonly IRepairService _repairService;
        private readonly IControlMapper<Repairs> _repairCMapper;
        private readonly IServiceProvider _serviceProvider;
        private int _repairId;
        public frmAddEditRepairNew(IRepairService repairService, IControlMapper<Repairs> repairCMapper, IServiceProvider serviceProvider)
        {
            _repairService = repairService;
            _repairCMapper = repairCMapper;
            _serviceProvider = serviceProvider;
            InitializeComponent();
            LoadDropdowns();
        }

        private void LoadDropdowns()
        {
            sluePPEsId.Properties.DataSource = _repairService.GetRepairPPE();
            var employees = HRMISEmployees.GetEmployees();
            slueRequestedById.Properties.DataSource = employees;
            slueDeliveredById.Properties.DataSource = employees;
        }
        public async void InitForm(int? repairId = null)
        {
            _repairId = repairId.Value;
            var data = await _repairService.GetByIdAsync(repairId.Value);
            _repairCMapper.MapControl(data, gcDetails, gcProblems, gcReqDetails);
        }
        private async void sluePropertyNo_EditValueChanged(object sender, System.EventArgs e)
        {
            await LoadPPESpecs();
        }

        private async Task LoadPPESpecs()
        {
            var ppeId = Convert.ToInt32(sluePPEsId.EditValue);
            var ppe = await _repairService.GetPPE(ppeId);

            var navigation = _serviceProvider.GetRequiredService<IControlNavigator<UCPPEsSpecs>>();
            navigation.NavigateTo(gcEquipmentSpecs, act => act.InitUC(ppe, forViewing: true));

            var employee = HRMISEmployees.GetEmployeeById(ppe.IssuedToId);
            txtIssuedTo.Text = employee?.Employee;
            txtOffAcr.Text = employee?.Office;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private async void btnSave_ClickAsync(object sender, EventArgs e)
        {
            var data = await _repairService.GetByIdAsync(_repairId);
            _repairCMapper.MapToEntity(data, gcDetails, gcProblems, gcReqDetails);
            await _repairService.SaveChangesAsync();
            Close();
        }
    }
}