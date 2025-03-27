using ICTProfilingV3.BaseClasses;
using Models.Entities;
using Models.Enums;
using Models.Repository;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ICTProfilingV3.MOForms
{
    public partial class frmAddEditMOAccount : BaseForm
    {
        private IUnitOfWork unitOfWork;
        private SaveType saveType;
        private readonly MOAccounts account;

        public frmAddEditMOAccount()
        {
            InitializeComponent();
            saveType = SaveType.Insert;
            unitOfWork = new UnitOfWork();
            LoadDropdowns();
        }

        public frmAddEditMOAccount(MOAccounts account)
        {
            InitializeComponent();
            saveType = SaveType.Update;
            unitOfWork = new UnitOfWork();
            this.account = account;
            LoadDropdowns();
            LoadDetails();
        }

        private void LoadDetails()
        {
            txtPrincipalName.Text = account.PrincipalName;
            txtPassword.Text = account.Password;
            deDateCreated.DateTime = (DateTime)account.DateCreated;
            lueOffice.EditValue = account.OfficeId;
        }

        private void LoadDropdowns()
        {
            var res = unitOfWork.PGNGroupOfficesRepo.GetAll();
            lueOffice.Properties.DataSource = res.ToList();
        }

        private async void btnSave_Click(object sender, System.EventArgs e)
        {
            if (saveType == SaveType.Insert) InsertAccount();
            else await UpdateAccount();

            this.Close();
        }

        private async Task UpdateAccount()
        {
            var moAccount = await unitOfWork.MOAccountRepo.FindAsync(x => x.Id == account.Id);
            moAccount.PrincipalName = txtPrincipalName.Text;
            moAccount.Password = txtPassword.Text;
            moAccount.DateCreated = deDateCreated.DateTime;
            moAccount.OfficeId = (int?)lueOffice.EditValue;
            unitOfWork.MOAccountRepo.Update(moAccount);
            unitOfWork.Save();
        }

        private void InsertAccount()
        {
            var account = new MOAccounts
            {
                PrincipalName = txtPrincipalName.Text,
                Password = txtPassword.Text,
                DateCreated = deDateCreated.DateTime,
                OfficeId = (int?)lueOffice.EditValue
            };
            unitOfWork.MOAccountRepo.Insert(account);
            unitOfWork.Save();
        }
    }
}