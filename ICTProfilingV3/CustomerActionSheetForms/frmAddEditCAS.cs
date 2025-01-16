using DevExpress.XtraEditors;
using EntityManager.Managers.User;
using Models.Entities;
using Models.Enums;
using Models.HRMISEntites;
using Models.Repository;
using Models.ViewModels;
using System;
using System.Threading.Tasks;

namespace ICTProfilingV3.CustomerActionSheetForms
{
    public partial class frmAddEditCAS : DevExpress.XtraEditors.XtraForm
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly CASViewModel cas;
        private readonly IICTUserManager userManager;
        private readonly SaveType saveType;

        public frmAddEditCAS()
        {
            InitializeComponent();
            saveType = SaveType.Insert;
            this.unitOfWork = new UnitOfWork();
            this.userManager = new ICTUserManager();
            LoadDropdowns();
        }
        public frmAddEditCAS(CASViewModel cas)
        {
            InitializeComponent();
            saveType = SaveType.Update;
            this.unitOfWork = new UnitOfWork();
            this.cas = cas;
            this.userManager = new ICTUserManager();
            LoadDropdowns();
            LoadDetails();
        }

        private void LoadDetails()
        {
            txtDate.DateTime = cas.DateCreated;
            txtName.Text = cas.CustomerActionSheet.ClientName;
            slueEmployee.EditValue = cas.CustomerActionSheet.ClientId;
            txtOffice.Text = cas.Office;
            txtContactNo.Text = cas.CustomerActionSheet.ContactNo;
            rdbtnGender.SelectedIndex = (int)cas.CustomerActionSheet.Gender;
            txtClientRequest.Text = cas.CustomerActionSheet.ClientRequest;
            txtActionTaken.Text = cas.CustomerActionSheet.ActionTaken;
            slueAssistedBy.Text = cas.CustomerActionSheet.AssistedById;
        }

        private void LoadDropdowns()
        {
            slueEmployee.Properties.DataSource = HRMISEmployees.GetEmployees();
            slueAssistedBy.Properties.DataSource = userManager.GetUsers();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            if (saveType == SaveType.Insert) await Insert();
            else await UpdateCAS();
            this.Close();
        }

        private async Task UpdateCAS()
        {
            var selectedEmployee = string.IsNullOrWhiteSpace(txtName.Text) ? slueEmployee.EditValue : null;

            var casUpdate = await unitOfWork.CustomerActionSheetRepo.FindAsync(x => x.Id == cas.Id);
            casUpdate.DateCreated = txtDate.DateTime;
            casUpdate.ClientName = txtName.Text;
            casUpdate.ClientId = (long?)selectedEmployee;
            casUpdate.Office = txtOffice.Text;
            casUpdate.ContactNo = txtContactNo.Text;
            casUpdate.Gender = (Gender)rdbtnGender.SelectedIndex;
            casUpdate.ClientRequest = txtClientRequest.Text;
            casUpdate.ActionTaken = txtActionTaken.Text;
            casUpdate.AssistedById = (string)slueAssistedBy.EditValue;

            await unitOfWork.SaveChangesAsync();
        }

        private async Task Insert()
        {
            var selectedEmployee = string.IsNullOrWhiteSpace(txtName.Text) ? slueEmployee.EditValue : null;
            var cas = new CustomerActionSheet
            {
                DateCreated = txtDate.DateTime,
                ClientName = txtName.Text,
                ClientId = (long?)selectedEmployee,
                Office = txtOffice.Text,
                ContactNo = txtContactNo.Text,
                Gender = (Gender)rdbtnGender.SelectedIndex,
                ClientRequest = txtClientRequest.Text,
                ActionTaken = txtActionTaken.Text,
                AssistedById = (string)slueAssistedBy.EditValue
            };
            unitOfWork.CustomerActionSheetRepo.Insert(cas);
            await unitOfWork.SaveChangesAsync();
        }

        private void slueEmployee_EditValueChanged(object sender, EventArgs e)
        {
            var emp = (EmployeesViewModel)slueEmployee.Properties.View.GetFocusedRow();
            
            if(emp == null) emp = HRMISEmployees.GetEmployeeById(cas.CustomerActionSheet.ClientId);
            if (emp == null) return;
            txtName.Text = emp.Employee;
            txtOffice.Text = emp.Office;
        }
    }
}