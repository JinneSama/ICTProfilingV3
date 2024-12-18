﻿using Models.Entities;
using Models.Enums;
using Models.Managers.User;
using Models.Repository;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ICTProfilingV3.ToolForms
{
    public partial class frmAddEditChangeLogs : DevExpress.XtraEditors.XtraForm
    {
        private IUnitOfWork unitOfWork;
        private SaveType saveType;
        private readonly ChangeLogs changelogs;

        public frmAddEditChangeLogs()
        {
            InitializeComponent();
            unitOfWork = new UnitOfWork();
            saveType = SaveType.Insert;
            LoadInsertDetails();
        }

        public frmAddEditChangeLogs(ChangeLogs changelogs)
        {
            InitializeComponent();
            unitOfWork = new UnitOfWork();
            saveType = SaveType.Update;
            this.changelogs = changelogs;
            LoadDetails();
        }

        private void LoadInsertDetails()
        {
            var lastVersion = unitOfWork.ChangeLogsRepo.GetAll().ToList()?.LastOrDefault() ?? null;

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

        private void LoadDetails()
        {
            txtVersion.Text = changelogs.Version;
            memoChanges.Text = changelogs.Changelogs;
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            if (saveType == SaveType.Insert) InsertChanges();
            else await UpdateChanges();

            this.Close();
        }

        private void InsertChanges()
        {
            var changes = new ChangeLogs
            {
                Version = txtVersion.Text,
                DateCreated = DateTime.UtcNow,
                UserId = UserStore.UserId,
                Changelogs = memoChanges.Text

            };
            unitOfWork.ChangeLogsRepo.Insert(changes);
            unitOfWork.Save();
        }

        private async Task UpdateChanges()
        {
            var changes = await unitOfWork.ChangeLogsRepo.FindAsync(x => x.Id == changelogs.Id);
            if (changes == null) return;

            changes.Changelogs = memoChanges.Text;
            unitOfWork.Save();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}