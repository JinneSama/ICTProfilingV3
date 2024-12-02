using Models.Entities;
using Models.Enums;
using Models.HRMISEntites;
using Models.Repository;
using Models.ViewModels;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace ICTProfilingV3.PGNForms
{
    public partial class frmAddEditAccount : DevExpress.XtraEditors.XtraForm
    {
        private IUnitOfWork unitOfWork;
        private PGNAccounts account;
        private EmployeesViewModel Employee;
        private bool IsSave = false;
        private SaveType SaveType;

        public frmAddEditAccount()
        {
            InitializeComponent();
            unitOfWork = new UnitOfWork();
            SaveType = SaveType.Insert;
            LoadDropdowns();
            CreateAccount();
        }

        public frmAddEditAccount(PGNAccounts account)
        {
            InitializeComponent();
            IsSave = true;
            this.account = account;
            SaveType = SaveType.Update;
            unitOfWork = new UnitOfWork();
            LoadDropdowns();
            LoadMacAddresses();
            LoadDetails();
        }

        private void LoadDetails()
        {
            if (account.HRMISEmpId == null) LoadNonEmployeeDetails();
            else LoadHRMISDetails();

            txtUsername.Text = account.Username;
            txtType.EditValue = account.UserType;
            txtOffice.EditValue = account.PGNGroupOfficesId;
            txtStatus.EditValue = account.Status;
            txtSignCount.Value = (decimal)account.SignInCount;
            txtTS.EditValue = account.TrafficSpeed;
            txtCategory.EditValue = account.Designation;
            txtPassword.Text = account.Password;
            txtRemarks.Text = account.Remarks;
        }

        private void LoadHRMISDetails()
        {
            var res = HRMISEmployees.GetEmployeeById(account.HRMISEmpId);
            if (res == null) return;
            txtName.Text = res.Employee;
            txtPos.Text = res.Position;
            rdbtnAccountType.SelectedIndex = 0;
            Employee = res;
        }

        private void LoadNonEmployeeDetails()
        {
            var res = account.PGNNonEmployee;
            if (res == null) return;
            txtName.Text = res.FullName;
            txtPos.Text = res.Position;
            rdbtnAccountType.SelectedIndex = 1;
        }

        private void LoadDropdowns()
        {
            txtType.Properties.DataSource = Enum.GetValues(typeof(PGNUserType)).Cast<PGNUserType>().Select(x => new { Type = x });
            txtStatus.Properties.DataSource = Enum.GetValues(typeof(PGNStatus)).Cast<PGNStatus>().Select(x => new { Status = x });
            txtCategory.Properties.DataSource = Enum.GetValues(typeof(PGNDesignations)).Cast<PGNDesignations>().Select(x => new { Category = x });

            txtTS.Properties.DataSource = Enum.GetValues(typeof(PGNTrafficSpeed)).Cast<PGNTrafficSpeed>().Select(x => new
            { 
                TS = x,
                TSDisplay = EnumHelper.GetEnumDescription(x)
            });

            var res = unitOfWork.PGNGroupOfficesRepo.GetAll();
            txtOffice.Properties.DataSource = res.ToList();
        }

        private void CreateAccount()
        {
            var _account = new PGNAccounts();
            unitOfWork.PGNAccountsRepo.Insert(_account);
            unitOfWork.Save();
            rdbtnAccountType.SelectedIndex = 1;
            account = _account;
            LoadMacAddresses();
        }

        private void LoadMacAddresses()
        {
            groupMacAddress.Controls.Clear();
            groupMacAddress.Controls.Add(new UCMacAdresses(account)
            {
                Dock = DockStyle.Fill
            });
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            IsSave = true;
            if (Employee == null) await SaveAsNonEmployee();
            else await SaveAsHRMISEmployee();
            this.Close();
        }

        private async Task SaveAsHRMISEmployee()
        {
            var acc = await unitOfWork.PGNAccountsRepo.FindAsync(x => x.Id == account.Id);
            if (acc == null) return;

            acc.HRMISEmpId = Employee.Id;
            acc.Username = txtUsername.Text;
            acc.UserType = (PGNUserType)txtType.EditValue;
            acc.Status = (PGNStatus)txtStatus.EditValue;
            acc.SignInCount = (int?)txtSignCount.Value;
            acc.TrafficSpeed = (PGNTrafficSpeed)txtTS.EditValue;
            acc.Designation = (PGNDesignations)txtCategory.EditValue;
            acc.PGNGroupOfficesId = (int)txtOffice.EditValue;
            acc.Remarks = txtRemarks.Text;
            acc.Password = txtPassword.Text;
            await unitOfWork.SaveChangesAsync();
        }

        private async Task SaveAsNonEmployee()
        {
            var acc = await unitOfWork.PGNAccountsRepo.FindAsync(x => x.Id == account.Id);
            if (acc == null) return;

            var nonEmployee = new PGNNonEmployee
            {
                FullName = txtName.Text,
                Position = txtPos.Text,
                Username = txtUsername.Text
            };
            unitOfWork.PGNNonEmployeeRepo.Insert(nonEmployee);
            
            acc.PGNNonEmployeeId = nonEmployee.Id; 
            acc.Username = txtUsername.Text;
            acc.UserType = (PGNUserType)txtType.EditValue;
            acc.Status = (PGNStatus)txtStatus.EditValue;
            acc.SignInCount = (int?)txtSignCount.Value;
            acc.TrafficSpeed = (PGNTrafficSpeed)txtTS.EditValue;
            acc.Designation = (PGNDesignations)txtCategory.EditValue;
            acc.PGNGroupOfficesId = (int)txtOffice.EditValue;
            acc.Remarks = txtRemarks.Text;
            acc.Password = txtPassword.Text;
            await unitOfWork.SaveChangesAsync();
        }

        private void btnAddOffice_Click(object sender, EventArgs e)
        {
            var frm = new frmPGNOffices();
            frm.ShowDialog();

            LoadDropdowns();
        }

        private void btnHRMIS_Click(object sender, EventArgs e)
        {
            var frm = new frmSelectFromHRMIS();
            frm.ShowDialog();

            if (frm.Employee == null) return;
            Employee = frm.Employee;
            PopulateFromHRMIS();
        }

        private void PopulateFromHRMIS()
        {
            txtUsername.Text = Employee.Username;
            txtName.Text = Employee.Employee;
            txtPos.Text = Employee.Position;

            rdbtnAccountType.SelectedIndex = 0;
        }

        private void frmAddEditAccount_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (IsSave) return;
            unitOfWork.PGNMacAddressesRepo.DeleteRange(x => x.PGNAccountId == account.Id);
            unitOfWork.PGNAccountsRepo.DeleteByEx(x => x.Id ==  account.Id);
            unitOfWork.Save();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}