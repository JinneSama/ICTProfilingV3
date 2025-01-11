using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Camera;
using Helpers.NetworkFolder;
using Helpers.Scanner;
using Helpers.Security;
using Models.Entities;
using Models.Managers.User;
using Models.Repository;
using System;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ICTProfilingV3.ActionsForms
{
    public partial class frmActionDocuments : DevExpress.XtraEditors.XtraForm
    {
        private IUnitOfWork unitOfWork;
        private HTTPNetworkFolder networkFolder;
        private readonly Actions action;

        public frmActionDocuments(Actions action)
        {
            InitializeComponent();
            unitOfWork = new UnitOfWork();  
            networkFolder = new HTTPNetworkFolder();
            this.action = action;
            LoadData();
        }
        public frmActionDocuments()
        {
            InitializeComponent();
            unitOfWork = new UnitOfWork();
            networkFolder = new HTTPNetworkFolder();
            LoadData();
        }

        private void LoadData()
        {
            int? actionId = action?.Id ?? null;
            var data = unitOfWork.ActionDocumentsRepo.FindAllAsync(x => x.ActionId == actionId).OrderBy(o => o.DocOrder).ToList();
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

            Image img = await networkFolder.DownloadFile(row.DocumentName);
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

        private async void btnDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (!CheckUser()) return;
            var msgRes = MessageBox.Show("Delete this Document?", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
            if (msgRes == DialogResult.Cancel) return;

            var row = (ActionDocuments)gridDocs.GetFocusedRow();
            if (row == null) return;

            await networkFolder.DeleteFile(row.DocumentName);
            unitOfWork.ActionDocumentsRepo.DeleteByEx(x => x.Id == row.Id);
            unitOfWork.Save();

            int? actionId = action?.Id ?? null;
            var res = unitOfWork.ActionDocumentsRepo.FindAllAsync(x => x.ActionId == actionId);

            int order = 1;
            foreach (var doc in res)
            {
                doc.DocOrder = order;
                order++;
            }

            unitOfWork.Save();
            LoadData();
        }

        private async Task SaveImage(Image image)
        {
            int? actionId = action?.Id ?? null;

            int DocOrder = 1;
            var docs = unitOfWork.ActionDocumentsRepo.FindAllAsync(x => x.ActionId == actionId).ToList().OrderBy(o => o.DocOrder);
            if (docs.LastOrDefault() != null) DocOrder = docs.LastOrDefault().DocOrder + 1;

            var actionDocs = new ActionDocuments
            {
                ActionId = actionId,
                DocOrder = DocOrder
            };

            unitOfWork.ActionDocumentsRepo.Insert(actionDocs);
            unitOfWork.Save();

            var actionDocsRes = await unitOfWork.ActionDocumentsRepo.FindAsync(x => x.Id == actionDocs.Id);

            var securityStamp = Guid.NewGuid().ToString();
            var documentName = Cryptography.Encrypt("Action_Document_" + actionDocsRes.Id , securityStamp);

            if (actionDocsRes != null) 
            {
                actionDocsRes.SecurityStamp = securityStamp;
                actionDocsRes.DocumentName = documentName + ".jpeg";
                unitOfWork.Save();
            }

            await networkFolder.UploadFile(image, actionDocsRes.DocumentName);
        }

        private async void gridDocs_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            var row = (ActionDocuments)gridDocs.GetFocusedRow();
            if(row == null) return;

            Image image = await networkFolder.DownloadFile(row.DocumentName);
            picDocImage.Image = image;
        }

        private bool CheckUser()
        {
            if (action.CreatedById != UserStore.UserId)
            {
                MessageBox.Show("This Option is not Available!");
                return false;
            }
            return true;
        }
    }
}