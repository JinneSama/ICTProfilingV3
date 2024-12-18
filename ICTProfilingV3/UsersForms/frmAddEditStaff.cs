using EntityManager.Managers.User;
using Helpers.NetworkFolder;
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
    public partial class frmAddEditStaff : DevExpress.XtraEditors.XtraForm
    {
        private readonly IICTUserManager userManager;
        private readonly IUnitOfWork unitOfWork;
        private readonly StaffViewModel staffViewModel;
        private readonly SaveType saveType;

        private DocumentHandler documentHandler;
        public frmAddEditStaff()
        {
            InitializeComponent();
            userManager = new ICTUserManager();
            unitOfWork = new UnitOfWork();
            LoadDropdowns();
            documentHandler = new DocumentHandler(Properties.Settings.Default.StaffNetworkPath,
                Properties.Settings.Default.NetworkUsername,
                Properties.Settings.Default.NetworkPassword);
            saveType = SaveType.Insert;
        }

        public frmAddEditStaff(StaffViewModel staffViewModel)
        {
            InitializeComponent();
            userManager = new ICTUserManager();
            unitOfWork = new UnitOfWork();
            LoadDropdowns();
            documentHandler = new DocumentHandler(Properties.Settings.Default.StaffNetworkPath,
                Properties.Settings.Default.NetworkUsername,
                Properties.Settings.Default.NetworkPassword);
            this.staffViewModel = staffViewModel;
            LoadDetails();
            saveType = SaveType.Update;
        }

        private void LoadDetails()
        {
            peStaffImage.Image = documentHandler.GetImage(staffViewModel.Users.Id + ".jpeg");
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
            if (saveType == SaveType.Insert) SaveStaff();
            else await UpdateStaff();
            this.Close();
        }

        private async Task UpdateStaff()
        {
            var staff = await unitOfWork.ITStaffRepo.FindAsync(x => x.Id == staffViewModel.Staff.Id);
            if(staff == null) return;

            var image = peStaffImage.Image;
            staff.Section = (Sections)lueSection.EditValue;
            staff.UserId = (string)slueUser.EditValue;

            documentHandler.SaveImage(image, Path.Combine(Application.StartupPath, staff.UserId + ".jpeg"), staff.UserId + ".jpeg");
            unitOfWork.Save();
        }

        private void SaveStaff()
        {
            var image = peStaffImage.Image;
            var staff = new ITStaff
            {
                ImagePath = "",
                Section = (Sections)lueSection.EditValue,
                UserId = (string)slueUser.EditValue
            };
            documentHandler.SaveImage(image, Path.Combine(Application.StartupPath, staff.UserId + ".jpeg"), staff.UserId + ".jpeg");
            unitOfWork.ITStaffRepo.Insert(staff);
            unitOfWork.Save();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}