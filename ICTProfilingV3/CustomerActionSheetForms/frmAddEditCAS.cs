using ICTProfilingV3.BaseClasses;
using ICTProfilingV3.DataTransferModels;
using ICTProfilingV3.DataTransferModels.ViewModels;
using ICTProfilingV3.Interfaces;
using ICTProfilingV3.Services.Employees;
using Models.Enums;
using System;
using System.Threading.Tasks;

namespace ICTProfilingV3.CustomerActionSheetForms
{
    public partial class frmAddEditCAS : BaseForm
    {
        private CASDTM _casDTM;
        private SaveType _saveType;

        private readonly IControlMapper<CASDetailDTM> _controlMapper;
        private readonly IICTUserManager _userManager;
        private readonly ICASService _casService;

        public frmAddEditCAS(IICTUserManager userManager, IControlMapper<CASDetailDTM> controlMapper,
            ICASService casService)
        {
            _userManager = userManager;
            _controlMapper = controlMapper;
            _casService = casService;
            InitializeComponent();
            LoadDropdowns();
        }

        public async void InitForm(SaveType saveType, CASDTM cas = null)
        {
            LoadDropdowns();
            _saveType = saveType;
            if (saveType == SaveType.Update && cas != null)
            {
                _casDTM = cas;
                var casDetails = await _casService.GetCASDetail(_casDTM.Id);
                _controlMapper.MapControl(casDetails, gcDetails);
            }
            else
            {
                _casDTM = new CASDTM();
            }
        }

        private void LoadDropdowns()
        {
            slueEmployee.Properties.DataSource = HRMISEmployees.GetEmployees();
            slueAssistedBy.Properties.DataSource = _userManager.GetUsers();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            if (_saveType == SaveType.Insert) await Insert();
            else await UpdateCAS();
            this.Close();
        }

        private async Task UpdateCAS()
        {
            var casDTM = new CASDetailDTM();
            _controlMapper.MapToEntity(casDTM, gcDetails);
            await _casService.UpdateCAS(casDTM, _casDTM.Id);
        }

        private async Task Insert()
        {
            var casDTM = new CASDetailDTM();
            _controlMapper.MapToEntity(casDTM, gcDetails);
            await _casService.AddCAS(casDTM);
        }

        private void slueEmployee_EditValueChanged(object sender, EventArgs e)
        {
            var emp = (EmployeesViewModel)slueEmployee.Properties.View.GetFocusedRow();

            if (emp == null) emp = HRMISEmployees.GetEmployeeById(_casDTM.CustomerActionSheet.ClientId);
            if (emp == null) return;
            txtClientName.Text = emp.Employee;
            txtOffice.Text = emp.Office;
        }
    }
}