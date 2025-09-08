using ICTProfilingV3.BaseClasses;
using ICTProfilingV3.Core.Common;
using ICTProfilingV3.DataTransferModels.ViewModels;
using ICTProfilingV3.Interfaces;
using ICTProfilingV3.PPEInventoryForms;
using ICTProfilingV3.Services.Employees;
using Microsoft.Extensions.DependencyInjection;
using Models.Entities;
using Models.Enums;
using System;
using System.Linq;
using System.Threading.Tasks;
namespace ICTProfilingV3.MOForms
{
    public partial class frmAddEditAccountUsers : BaseForm
    {
        private readonly IPPEInventoryService _ppeInventoryService;
        private readonly IMOService _moService;
        private readonly IServiceProvider _serviceProvider;
        private readonly UserStore _userStore;

        private SaveType _saveType;
        private MOAccountUsers _moAccountUser;
        private MOAccounts _mOAccount;

        public frmAddEditAccountUsers(UserStore userStore, IServiceProvider serviceProvider,
            IPPEInventoryService ppeInventoryService, IMOService mOService)
        {
            _userStore = userStore;
            _serviceProvider = serviceProvider;
            _ppeInventoryService = ppeInventoryService;
            _moService = mOService;
            InitializeComponent();
            LoadDropdowns();
        }

        public void InitForm(MOAccounts mOAccount = null, MOAccountUsers moAccountUser = null)
        {
            if(mOAccount == null)
            {
                _saveType = SaveType.Update;
                _moAccountUser = moAccountUser;
                LoadDetails();
            }
            else
            {
                _saveType = SaveType.Insert;
                _mOAccount = mOAccount;
            }
        }

        private void LoadDetails()
        {
            deInstallationDate.DateTime = (DateTime)_moAccountUser.DateCreated;
            deProcuredDate.DateTime = (DateTime)_moAccountUser.ProcuredDate;
            spinDeviceNo.Value = _moAccountUser.DeviceNo;
            sluePropertyNo.EditValue = _moAccountUser.PPEId;
            txtRemarks.Text = _moAccountUser.Remarks;
            slueIssuedTo.EditValue = _moAccountUser.IssuedTo;
            slueUser.EditValue = _moAccountUser.AccountUser;
            txtDescription.Text = _moAccountUser.Description;
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
            var ppe = _ppeInventoryService.GetAll().ToList();

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

            LoadPPEs();
        }

        private void btnCancel_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        private async void btnSave_Click(object sender, System.EventArgs e)
        {
            if (_saveType == SaveType.Insert) InsertUser();
            else await UpdateUser();

            this.Close();
        }

        private void InsertUser()
        {
            var user = new MOAccountUsers
            {
                DateCreated = DateTime.Now,
                DateOfInstallation = deInstallationDate.DateTime,
                ProcuredDate = deProcuredDate.DateTime,
                DeviceNo = (int)spinDeviceNo.Value,
                PPEId = (int?)sluePropertyNo.EditValue,
                Remarks = txtRemarks.Text,
                IssuedTo = (long)slueIssuedTo.EditValue,
                AccountUser = (long)slueUser.EditValue,
                Description = txtDescription.Text,
                MOAccountId = _mOAccount.Id,
                CreatedById = _userStore.UserId
            };
            _moService.MOAccountUserBaseService.AddAsync(user);
        }

        private async Task UpdateUser()
        {
            var user = await _moService.MOAccountUserBaseService.GetByIdAsync(_moAccountUser.Id);
            if (user == null) return;

          
            user.DateOfInstallation = deInstallationDate.DateTime;
            user.ProcuredDate = deProcuredDate.DateTime;
            user.DeviceNo = (int)spinDeviceNo.Value;
            user.PPEId = (int?)sluePropertyNo.EditValue;
            user.Remarks = txtRemarks.Text;
            user.IssuedTo = (long)slueIssuedTo.EditValue;
            user.AccountUser = (long)slueUser.EditValue;
            user.Description = txtDescription.Text;
            _moService.MOAccountUserBaseService.SaveChangesAsync();
        }
    }
}