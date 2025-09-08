using ICTProfilingV3.BaseClasses;
using ICTProfilingV3.Core.Common;
using ICTProfilingV3.Interfaces;
using Models.Entities;
using Models.Enums;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ICTProfilingV3.ToolForms
{
    public partial class frmAddEditChangeLogs : BaseForm
    {
        private IChangeLogService _changeLogService;
        private readonly UserStore _userStore;

        private SaveType _saveType;
        private ChangeLogs _changelogs;

        public frmAddEditChangeLogs(UserStore userStore, IChangeLogService changeLogService)
        {
            _userStore = userStore;
            _changeLogService = changeLogService;
            InitializeComponent();
        }

        public void InitForm(ChangeLogs changelogs = null)
        {
            _saveType = SaveType.Update;
            _changelogs = changelogs;
            if (changelogs == null) LoadInsertDetails(); 
            else LoadDetails();
        }

        private void LoadInsertDetails()
        {
            var lastVersion = _changeLogService.GetAll().ToList()?.LastOrDefault() ?? null;
            _saveType = SaveType.Insert;
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
            _saveType = SaveType.Insert;
            txtVersion.Text = _changelogs.Version;
            memoChanges.Text = _changelogs.Changelogs;
            var img = await _changeLogService.DownloadFile(_changelogs.ImageName);
            picImageInfo.Image = img;
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            if (_saveType == SaveType.Insert) await InsertChanges();
            else await UpdateChanges();

            this.Close();
        }

        private async Task InsertChanges()
        {
            var changes = new ChangeLogs
            {
                Version = txtVersion.Text,
                DateCreated = DateTime.Now,
                UserId = _userStore.UserId,
                Changelogs = memoChanges.Text,
                ImageName = txtVersion.Text + ".jpeg",
            };
            var res = await _changeLogService.AddAsync(changes);

            if (picImageInfo.Image == null) return;
            await _changeLogService.UploadImage(picImageInfo.Image, res.ImageName , res.Id);
        }

        private async Task UpdateChanges()
        {
            var changes = await _changeLogService.GetByIdAsync(_changelogs.Id);
            if (changes == null) return;

            changes.Changelogs = memoChanges.Text;
            await _changeLogService.SaveChangesAsync();

            if (picImageInfo.Image == null) return;
            await _changeLogService.UploadImage(picImageInfo.Image, changes.ImageName, changes.Id);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}