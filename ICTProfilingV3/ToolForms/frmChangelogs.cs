using DevExpress.XtraEditors;
using ICTProfilingV3.API.FilesApi;
using ICTProfilingV3.BaseClasses;
using ICTProfilingV3.DataTransferModels.ViewModels;
using Models.Repository;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ICTProfilingV3.ToolForms
{
    public partial class frmChangelogs : BaseForm
    {
        private readonly string version;
        private IUnitOfWork unitOfWork;
        private readonly HTTPNetworkFolder httpNetworkFolder;
        public frmChangelogs(string version)
        {
            InitializeComponent();
            this.version = version;
            httpNetworkFolder = new HTTPNetworkFolder();
            unitOfWork = new UnitOfWork();
            LoadDetails();
        }

        private async void LoadDetails()
        {
            var now = DateTime.Now;
            var firstDayOfLastMonth = new DateTime(now.Year, now.Month, 1).AddMonths(-1);

            btnClose.Enabled = false;
            lblVersion.Text = "Current Version: " + version;
            var changes = await Task.WhenAll(
                unitOfWork.ChangeLogsRepo.GetAll()
                .Where(x => x.DateCreated >= firstDayOfLastMonth)
                    .OrderByDescending(x => x.DateCreated)
                    .ToList()
                    .Select(async s => new ChangelogsViewModel
                    {
                        ChangeLogs = s,
                        Image = await httpNetworkFolder.DownloadFile(s.ImageName)
                    })
            );
            gcChangelogs.DataSource = changes.ToList();
            btnClose.Enabled = true;
            progressChanges.Visible = false;
        }

        private void btnClose_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void tvChangelogs_ItemClick(object sender, DevExpress.XtraGrid.Views.Tile.TileViewItemClickEventArgs e)
        {
            var row = (ChangelogsViewModel)tvChangelogs.GetFocusedRow();
            if (row == null || row.Image == null) return;

            XtraForm xtraForm = new XtraForm()
            {
                WindowState = FormWindowState.Maximized,
                Text = "Changelog Preview"
            };

            PictureEdit pictureEdit = new PictureEdit()
            {
                Dock = DockStyle.Fill,
                Image = row.Image
            };
            pictureEdit.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Squeeze;
            xtraForm.Controls.Add(pictureEdit);
            xtraForm.ShowDialog();
        }
    }
}