using EntityManager.Managers.User;
using Helpers.Interfaces;
using Helpers.NetworkFolder;
using Helpers.Security;
using ICTProfilingV3.BaseClasses;
using Models.Entities;
using Models.Enums;
using Models.Repository;
using Models.ViewModels;
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ICTProfilingV3.UsersForms
{
    public partial class frmAddEditStaff : BaseForm
    {
        private readonly IICTUserManager userManager;
        private readonly IUnitOfWork unitOfWork;
        private readonly StaffViewModel staffViewModel;
        private readonly SaveType saveType;

        private HTTPNetworkFolder networkFolder;
        public frmAddEditStaff()
        {
            InitializeComponent();
            userManager = new ICTUserManager();
            unitOfWork = new UnitOfWork();
            networkFolder = new HTTPNetworkFolder();
            LoadDropdowns();
            saveType = SaveType.Insert;
        }

        public frmAddEditStaff(StaffViewModel staffViewModel)
        {
            InitializeComponent();
            userManager = new ICTUserManager();
            unitOfWork = new UnitOfWork();
            LoadDropdowns();
            networkFolder = new HTTPNetworkFolder();
            this.staffViewModel = staffViewModel;
            LoadDetails();
            saveType = SaveType.Update;
        }

        private async void LoadDetails()
        {
            peStaffImage.Image = await networkFolder.DownloadFile(staffViewModel.Users.Id + ".jpeg");
            slueUser.EditValue = staffViewModel.Users.Id;
            lueSection.EditValue = staffViewModel.Staff.Section;
        }

        private void LoadDropdowns()
        {
            slueUser.Properties.DataSource = userManager.GetUsers();
            lueSection.Properties.DataSource = Enum.GetValues(typeof(Sections)).Cast<Sections>().Select(x => new
            {
                Id = x,
                Section = EnumHelper.GetEnumDescription(x)
            });
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            splashScreenUpload.ShowWaitForm();
            if (saveType == SaveType.Insert) await SaveStaff();
            else await UpdateStaff();
            splashScreenUpload.CloseWaitForm();
            this.Close();
        }

        private async Task UpdateStaff()
        {
            var staff = await unitOfWork.ITStaffRepo.FindAsync(x => x.Id == staffViewModel.Staff.Id);
            if(staff == null) return;

            var image = peStaffImage.Image;
            staff.Section = (Sections)lueSection.EditValue;
            staff.UserId = (string)slueUser.EditValue;

            if(image != null) await networkFolder.UploadFile(image, staff.UserId + ".jpeg");
            await unitOfWork.SaveChangesAsync();
        }

        private async Task SaveStaff()
        {
            var image = peStaffImage.Image;
            var staff = new ITStaff
            {
                ImagePath = "",
                Section = (Sections)lueSection.EditValue,
                UserId = (string)slueUser.EditValue
            };

            if (image != null) await networkFolder.UploadFile(image, staff.UserId + ".jpeg");

            unitOfWork.ITStaffRepo.Insert(staff);
            await unitOfWork.SaveChangesAsync();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}