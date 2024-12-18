using DevExpress.Utils.Drawing;
using DevExpress.XtraRichEdit.Model.History;
using ICTProfilingV3.PPEInventoryForms;
using Models.Entities;
using Models.Enums;
using Models.HRMISEntites;
using Models.Managers.User;
using Models.Repository;
using Models.ViewModels;
using System;
using System.Linq;
using System.Threading.Tasks;
namespace ICTProfilingV3.MOForms
{
    public partial class frmAddEditAccountUsers : DevExpress.XtraEditors.XtraForm
    {
        private IUnitOfWork unitOfWork;
        private SaveType saveType;
        private readonly MOAccountUsers moAccountUser;
        private readonly MOAccounts mOAccount;

        public frmAddEditAccountUsers(MOAccounts mOAccount)
        {
            InitializeComponent();
            saveType = SaveType.Insert;
            unitOfWork = new UnitOfWork();
            LoadDropdowns();
            this.mOAccount = mOAccount;
        }

        public frmAddEditAccountUsers(MOAccountUsers moAccountUser)
        {
            InitializeComponent();
            this.moAccountUser = moAccountUser;
            saveType = SaveType.Update;
            unitOfWork = new UnitOfWork();
            LoadDropdowns();
            LoadDetails();
        }

        private void LoadDetails()
        {
            deInstallationDate.DateTime = (DateTime)moAccountUser.DateCreated;
            deProcuredDate.DateTime = (DateTime)moAccountUser.ProcuredDate;
            spinDeviceNo.Value = moAccountUser.DeviceNo;
            sluePropertyNo.EditValue = moAccountUser.PPEId;
            txtRemarks.Text = moAccountUser.Remarks;
            slueIssuedTo.EditValue = moAccountUser.IssuedTo;
            slueUser.EditValue = moAccountUser.AccountUser;
        }

        private void LoadDropdowns()
        {
            var employees = HRMISEmployees.GetEmployees();
            slueIssuedTo.Properties.DataSource = employees;
            slueUser.Properties.DataSource = employees;
            LoadPPEs();
        }
        private void LoadPPEs()
        {
            var ppe = unitOfWork.PPesRepo.GetAll().ToList();

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

        private void btnAddPPE_Click(object sender, System.EventArgs e)
        {
            var frm = new frmAddEditPPEs(SaveType.Insert, null);
            frm.ShowDialog();

            LoadPPEs();
        }

        private void btnCancel_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        private async void btnSave_Click(object sender, System.EventArgs e)
        {
            if (saveType == SaveType.Insert) InsertUser();
            else await UpdateUser();

            this.Close();
        }

        private void InsertUser()
        {
            var user = new MOAccountUsers
            {
                DateCreated = DateTime.UtcNow,
                DateOfInstallation = deInstallationDate.DateTime,
                ProcuredDate = deProcuredDate.DateTime,
                DeviceNo = (int)spinDeviceNo.Value,
                PPEId = (int)sluePropertyNo.EditValue,
                Remarks = txtRemarks.Text,
                IssuedTo = (long)slueIssuedTo.EditValue,
                AccountUser = (long)slueUser.EditValue,
                Description = txtDescription.Text,
                MOAccountId = mOAccount.Id,
                CreatedById = UserStore.UserId
            };
            unitOfWork.MOAccountUserRepo.Insert(user);
            unitOfWork.Save();
        }

        private async Task UpdateUser()
        {
            var user = await unitOfWork.MOAccountUserRepo.FindAsync(x => x.Id == moAccountUser.Id);
            if (user == null) return;

            user.DateCreated = DateTime.UtcNow;
            user.DateOfInstallation = deInstallationDate.DateTime;
            user.ProcuredDate = deProcuredDate.DateTime;
            user.DeviceNo = (int)spinDeviceNo.Value;
            user.PPEId = (int)sluePropertyNo.EditValue;
            user.Remarks = txtRemarks.Text;
            user.IssuedTo = (long)slueIssuedTo.EditValue;
            user.AccountUser = (long)slueUser.EditValue;
            user.Description = txtDescription.Text;

            unitOfWork.Save();
        }
    }
}