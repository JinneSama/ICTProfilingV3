using ICTProfilingV3.BaseClasses;
using ICTProfilingV3.DataTransferModels.ViewModels;
using ICTProfilingV3.Interfaces;
using ICTProfilingV3.Services.Employees;
using Microsoft.Extensions.DependencyInjection;
using Models.Entities;
using Models.Enums;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ICTProfilingV3.PGNForms
{
    public partial class frmAddEditAccount : BaseForm
    {
        private readonly IControlMapper<PGNAccounts> _accountMapper;
        private readonly IServiceProvider _serviceProvider;
        private readonly IPGNService _pgnService;
        private PGNAccounts _account;
        private EmployeesViewModel _employee;
        private bool _isSave = false;

        public frmAddEditAccount(IControlMapper<PGNAccounts> accountMapper, IServiceProvider serviceProvider, 
            IPGNService pgnService)
        {
            _accountMapper = accountMapper;
            _pgnService = pgnService;
            _serviceProvider = serviceProvider;
            InitializeComponent();
            LoadDropdowns();
        }

        public async Task InitForm(PGNAccounts account = null)
        {
            if (account == null)
            {
                await CreateAccount();
                return;
            }

            _isSave = true;
            _account = account;
            LoadMacAddresses();
            LoadDetails();
        }
        private void LoadDetails()
        {
            if (_account.HRMISEmpId == null) LoadNonEmployeeDetails();
            else LoadHRMISDetails();

            _accountMapper.MapControl(_account, groupControl1);
        }

        private void LoadHRMISDetails()
        {
            var res = HRMISEmployees.GetEmployeeById(_account.HRMISEmpId);
            if (res == null) return;
            txtEmployee.Text = res.Employee;
            txtPosition.Text = res.Position;
            rdbtnAccountType.SelectedIndex = 0;
            _employee = res;
        }

        private void LoadNonEmployeeDetails()
        {
            var res = _account.PGNNonEmployee;
            if (res == null) return;
            txtEmployee.Text = res.FullName;
            txtPosition.Text = res.Position;
            rdbtnAccountType.SelectedIndex = 1;
        }

        private void LoadDropdowns()
        {
            txtUserType.Properties.DataSource = Enum.GetValues(typeof(PGNUserType)).Cast<PGNUserType>().Select(x => new { Type = x });
            txtStatus.Properties.DataSource = Enum.GetValues(typeof(PGNStatus)).Cast<PGNStatus>().Select(x => new { Status = x });
            txtDesignation.Properties.DataSource = Enum.GetValues(typeof(PGNDesignations)).Cast<PGNDesignations>().Select(x => new { Category = x });

            txtTrafficSpeed.Properties.DataSource = Enum.GetValues(typeof(PGNTrafficSpeed)).Cast<PGNTrafficSpeed>().Select(x => new
            { 
                TS = x,
                TSDisplay = EnumHelper.GetEnumDescription(x)
            });

            var res = _pgnService.PGNGroupOfficeService.GetAll();
            txtPGNGroupOfficesId.Properties.DataSource = res.ToList();
        }

        private async Task CreateAccount()
        {
            var account = new PGNAccounts();
            var newAccount = await _pgnService.AddAsync(account);
            rdbtnAccountType.SelectedIndex = 1;
            _account = newAccount;
            LoadMacAddresses();
        }

        private void LoadMacAddresses()
        {
            var navigation = _serviceProvider.GetRequiredService<IControlNavigator<UCMacAdresses>>();
            navigation.NavigateTo(groupMacAddress, act => act.InitUC(_account));
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            _isSave = true;
            if (_employee == null) await SaveAsNonEmployee();
            else await SaveAsHRMISEmployee();
            this.Close();
        }

        private async Task SaveAsHRMISEmployee()
        {
            var acc = await _pgnService.GetByIdAsync(_account.Id);
            if (acc == null) return;

            acc.HRMISEmpId = _employee.Id;
            acc.Username = txtUsername.Text;
            acc.UserType = (PGNUserType)txtUserType.EditValue;
            acc.Status = (PGNStatus)txtStatus.EditValue;
            acc.SignInCount = (int?)seSignInCount.Value;
            acc.TrafficSpeed = (PGNTrafficSpeed)txtTrafficSpeed.EditValue;
            acc.Designation = (PGNDesignations)txtDesignation.EditValue;
            acc.PGNGroupOfficesId = (int)txtPGNGroupOfficesId.EditValue;
            acc.Remarks = txtRemarks.Text;
            acc.Password = txtPassword.Text;
            await _pgnService.SaveChangesAsync();
        }

        private async Task SaveAsNonEmployee()
        {
            var acc = await _pgnService.GetByIdAsync(_account.Id);
            if (acc == null) return;

            var nonEmployee = new PGNNonEmployee
            {
                FullName = txtEmployee.Text,
                Position = txtPosition.Text,
                Username = txtUsername.Text
            };
            await _pgnService.PGNNonEmployeeService.AddAsync(nonEmployee);
            
            acc.PGNNonEmployeeId = nonEmployee.Id; 
            acc.Username = txtUsername.Text;
            acc.UserType = (PGNUserType)txtUserType.EditValue;
            acc.Status = (PGNStatus)txtStatus.EditValue;
            acc.SignInCount = (int?)seSignInCount.Value;
            acc.TrafficSpeed = (PGNTrafficSpeed)txtTrafficSpeed.EditValue;
            acc.Designation = (PGNDesignations)txtDesignation.EditValue;
            acc.PGNGroupOfficesId = (int)txtPGNGroupOfficesId.EditValue;
            acc.Remarks = txtRemarks.Text;
            acc.Password = txtPassword.Text;
            await _pgnService.SaveChangesAsync();
        }

        private void btnAddOffice_Click(object sender, EventArgs e)
        {
            var frm = _serviceProvider.GetRequiredService<frmPGNOffices>();
            frm.ShowDialog();

            LoadDropdowns();
        }

        private void btnHRMIS_Click(object sender, EventArgs e)
        {
            var frm = new frmSelectFromHRMIS();
            frm.ShowDialog();

            if (frm.Employee == null) return;
            _employee = frm.Employee;
            PopulateFromHRMIS();
        }

        private void PopulateFromHRMIS()
        {
            txtUsername.Text = _employee.Username;
            txtEmployee.Text = _employee.Employee;
            txtPosition.Text = _employee.Position;

            rdbtnAccountType.SelectedIndex = 0;
        }

        private async void frmAddEditAccount_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_isSave) return;
            await _pgnService.PGNMacAddressService.DeleteRangeAsync(x => x.PGNAccountId == _account.Id);
            await _pgnService.DeleteAsync(_account.Id);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}