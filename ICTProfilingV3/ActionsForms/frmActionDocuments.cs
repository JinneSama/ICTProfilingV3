using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Camera;
using Helpers.Scanner;
using ICTProfilingV3.API.FilesApi;
using ICTProfilingV3.BaseClasses;
using ICTProfilingV3.Core.Common;
using ICTProfilingV3.Interfaces;
using Models.Entities;
using Models.Repository;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ICTProfilingV3.ActionsForms
{
    public partial class frmActionDocuments : BaseForm
    {
        private readonly IDocActionsService _docActService;
        private readonly HTTPNetworkFolder _networkFolder;
        private readonly UserStore _userStore;
        private Actions _action;

        public frmActionDocuments(HTTPNetworkFolder networkFolder, IDocActionsService docActionsService, UserStore userStore)
        {
            InitializeComponent();
            _docActService = docActionsService;
            _userStore = userStore;
            LoadData();
        }
        public void SetAction(Actions action)
        {
            _action = action;
        }

        private void LoadData()
        {
            int? actionId = _action?.Id ?? null;
            var data = _docActService.GetActionDocuments(actionId);
            gcScanDocs.DataSource = data;
        }

        private void picDocImage_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
                ImageMenu.ShowPopup(MousePosition);
        }

        private async void btnScan_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (!CheckUser()) return;
            var scannedDocs = ScanDocument.ScanImages();
            if (scannedDocs == null) return;

            foreach (var scannedDoc in scannedDocs)
            {
                await SaveImage(scannedDoc);
            }

            LoadData();
        }
        #region CameraCapture
        private async void btnFile_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (!CheckUser()) return;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif;*.tiff|All Files|*.*";
            openFileDialog.Title = "Select an Image";
            if (openFileDialog.ShowDialog() == DialogResult.Cancel) return;

            string selectedFilePath = openFileDialog.FileName;
            Image img = Image.FromFile(selectedFilePath);
            picDocImage.Image = img;

            await SaveImage(img);
            LoadData();
        }

        private async void btnCamera_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (!CheckUser()) return;
            TakePictureDialog dialog = new TakePictureDialog();
            dialog.ResolutionMode = ResolutionMode.Maximum;
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.Cancel) return;

            Image image = dialog.Image;
            picDocImage.Image = image;

            await SaveImage(image);
            LoadData();
        }

        private async void btnPreview_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var row = (ActionDocuments)gridDocs.GetFocusedRow();
            if (row == null) return;

            Image img = await _networkFolder.DownloadFile(row.DocumentName);
            XtraForm xtraForm = new XtraForm()
            {
                WindowState = FormWindowState.Maximized,
                Text = "Document Preview"
            };

            PictureEdit pictureEdit = new PictureEdit()
            {
                Dock = DockStyle.Fill,
                Image = img
            };
            pictureEdit.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Squeeze;
            xtraForm.Controls.Add(pictureEdit);
            xtraForm.ShowDialog();
        }
        #endregion
        private async void btnDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (!CheckUser()) return;
            var msgRes = MessageBox.Show("Delete this Document?", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
            if (msgRes == DialogResult.Cancel) return;

            var row = (ActionDocuments)gridDocs.GetFocusedRow();
            if (row == null) return;
            await _networkFolder.DeleteFile(row.DocumentName);
            _docActService.DeleteDocument(row.Id);

            _docActService.ReorderDocument(_action?.Id ?? null);
            LoadData();
        }

        private async Task SaveImage(Image image)
        {
            int? actionId = _action?.Id ?? null;
            string docName = await _docActService.AddActionDocument(actionId);
            await _networkFolder.UploadFile(image, docName);
        }

        private async void gridDocs_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            var row = (ActionDocuments)gridDocs.GetFocusedRow();
            if(row == null) return;

            Image image = await _networkFolder.DownloadFile(row.DocumentName);
            picDocImage.Image = image;
        }

        private bool CheckUser()
        {
            if (_action.CreatedById != _userStore.UserId)
            {
                MessageBox.Show("This Option is not Available!");
                return false;
            }
            return true;
        }
    }
}