using ICTProfilingV3.API.FilesApi;
using ICTProfilingV3.BaseClasses;
using ICTProfilingV3.Core.Common;
using Models.Entities;
using Models.Enums;
using Models.Repository;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ICTProfilingV3.ToolForms
{
    public partial class frmAddEditChangeLogs : BaseForm
    {
        private IUnitOfWork unitOfWork;
        private SaveType saveType;
        private ChangeLogs changelogs;
        private readonly HTTPNetworkFolder httpNetworkFolder;
        private readonly UserStore _userStore;

        public frmAddEditChangeLogs(UserStore userStore)
        {
            _userStore = userStore;
            InitializeComponent();
            httpNetworkFolder = new HTTPNetworkFolder();
            unitOfWork = new UnitOfWork();
        }

        public void InitForm(ChangeLogs changelogs = null)
        {
            saveType = SaveType.Update;
            this.changelogs = changelogs;
            if (changelogs == null) LoadInsertDetails(); 
            else LoadDetails();
        }

        private void LoadInsertDetails()
        {
            var lastVersion = unitOfWork.ChangeLogsRepo.GetAll().ToList()?.LastOrDefault() ?? null;
            saveType = SaveType.Insert;
            string version;
            if (lastVersion == null) version = "1.0.0.1";
            else version = GetVersion(lastVersion.Version);

            txtVersion.Text = version;
        }

        private string GetVersion(string version)
        {
            string[] parts = version.Split('.');
            string lastPart = parts[parts.Length - 1];
            string mainVersion = string.Join(".", parts, 0, parts.Length - 1);
            return mainVersion + "." + (Convert.ToInt32(lastPart) + 1).ToString();
        }

        private async void LoadDetails()
        {
            saveType = SaveType.Insert;
            txtVersion.Text = changelogs.Version;
            memoChanges.Text = changelogs.Changelogs;
            var img = await httpNetworkFolder.DownloadFile(changelogs.ImageName);
            picImageInfo.Image = img;
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            if (saveType == SaveType.Insert) InsertChanges();
            else await UpdateChanges();

            this.Close();
        }

        private async void InsertChanges()
        {
            var changes = new ChangeLogs
            {
                Version = txtVersion.Text,
                DateCreated = DateTime.Now,
                UserId = _userStore.UserId,
                Changelogs = memoChanges.Text,
                ImageName = txtVersion.Text + ".jpeg",
            };
            unitOfWork.ChangeLogsRepo.Insert(changes);
            unitOfWork.Save();

            if (picImageInfo.Image == null) return;
            await httpNetworkFolder.UploadFile(picImageInfo.Image, txtVersion.Text + ".jpeg");
        }

        private async Task UpdateChanges()
        {
            var changes = await unitOfWork.ChangeLogsRepo.FindAsync(x => x.Id == changelogs.Id);
            if (changes == null) return;

            changes.Changelogs = memoChanges.Text;
            unitOfWork.Save();

            if (picImageInfo.Image == null) return;
            await httpNetworkFolder.UploadFile(picImageInfo.Image, txtVersion.Text + ".jpeg");
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}