using ICTProfilingV3.BaseClasses;
using ICTProfilingV3.Interfaces;
using Models.Entities;
using Models.Enums;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ICTProfilingV3.MOForms
{
    public partial class frmAddEditMOAccount : BaseForm
    {
        private readonly IPGNService _pgnService;
        private readonly IMOService _moService;
        private SaveType _saveType;
        private MOAccounts _account;

        public frmAddEditMOAccount(IMOService moService)
        {
            _moService = moService;
            InitializeComponent();
        }

        public void InitForm(MOAccounts account = null)
        {
            LoadDropdowns();
            if (account == null) _saveType = SaveType.Insert;
            else
            {
                _saveType = SaveType.Update;
                _account = account;
                LoadDetails();
            }
        }

        private void LoadDetails()
        {
            txtPrincipalName.Text = _account.PrincipalName;
            txtPassword.Text = _account.Password;
            deDateCreated.DateTime = (DateTime)_account.DateCreated;
            lueOffice.EditValue = _account.OfficeId;
        }

        private void LoadDropdowns()
        {
            var res = _pgnService.PGNGroupOfficeService.GetAll();
            lueOffice.Properties.DataSource = res.ToList();
        }

        private async void btnSave_Click(object sender, System.EventArgs e)
        {
            if (_saveType == SaveType.Insert) await InsertAccount();
            else await UpdateAccount();

            this.Close();
        }

        private async Task UpdateAccount()
        {
            var moAccount = await _moService.GetByIdAsync(_account.Id);
            moAccount.PrincipalName = txtPrincipalName.Text;
            moAccount.Password = txtPassword.Text;
            moAccount.DateCreated = deDateCreated.DateTime;
            moAccount.OfficeId = (int?)lueOffice.EditValue;
            await _moService.SaveChangesAsync();
        }

        private async Task InsertAccount()
        {
            var account = new MOAccounts
            {
                PrincipalName = txtPrincipalName.Text,
                Password = txtPassword.Text,
                DateCreated = deDateCreated.DateTime,
                OfficeId = (int?)lueOffice.EditValue
            };
            await _moService.AddAsync(account);
        }
    }
}