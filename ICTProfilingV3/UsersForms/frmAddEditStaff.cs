using ICTProfilingV3.API.FilesApi;
using ICTProfilingV3.BaseClasses;
using ICTProfilingV3.DataTransferModels.ViewModels;
using ICTProfilingV3.Interfaces;
using Models.Entities;
using Models.Enums;
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ICTProfilingV3.UsersForms
{
    public partial class frmAddEditStaff : BaseForm
    {
        private readonly IICTUserManager _userManager;
        private readonly IStaffService _staffService;
        private readonly HTTPNetworkFolder _networkFolder;
        private StaffViewModel _staffViewModel;
        private SaveType _saveType = SaveType.Insert;

        public frmAddEditStaff(IICTUserManager userManager, HTTPNetworkFolder networkFolder, IStaffService staffService)
        {
            _userManager = userManager;
            _networkFolder = networkFolder;
            _staffService = staffService;
            InitializeComponent();
            LoadDropdowns();
        }

        public void InitForm(StaffViewModel staffModel)
        {
            SaveType saveType = SaveType.Update;
            _staffViewModel = staffModel;
        }

        private async void LoadDetails()
        {
            peStaffImage.Image = await _networkFolder.DownloadFile(_staffViewModel.Users.Id + ".jpeg");
            slueUser.EditValue = _staffViewModel.Users.Id;
            lueSection.EditValue = _staffViewModel.Staff.Section;
        }

        private void LoadDropdowns()
        {
            slueUser.Properties.DataSource = _userManager.GetUsers();
            lueSection.Properties.DataSource = Enum.GetValues(typeof(Sections)).Cast<Sections>().Select(x => new
            {
                Id = x,
                Section = EnumHelper.GetEnumDescription(x)
            });
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            splashScreenUpload.ShowWaitForm();
            if (_saveType == SaveType.Insert) await SaveStaff();
            else await UpdateStaff();
            splashScreenUpload.CloseWaitForm();
            this.Close();
        }

        private async Task UpdateStaff()
        {
            var staff = await _staffService.GetByIdAsync(_staffViewModel.Staff.Id);
            if(staff == null) return;

            var image = peStaffImage.Image;
            staff.Section = (Sections)lueSection.EditValue;
            staff.UserId = (string)slueUser.EditValue;

            if(image != null) await _networkFolder.UploadFile(image, staff.UserId + ".jpeg");
            await _staffService.SaveChangesAsync();
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

            if (image != null) await _networkFolder.UploadFile(image, staff.UserId + ".jpeg");
            await _staffService.AddAsync(staff);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}