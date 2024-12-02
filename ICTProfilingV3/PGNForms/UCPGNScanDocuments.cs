using DevExpress.Data.ODataLinq.Helpers;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using Helpers.NetworkFolder;
using Helpers.Scanner;
using Models.Entities;
using Models.Repository;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace ICTProfilingV3.PGNForms
{
    public partial class UCPGNScanDocuments : DevExpress.XtraEditors.XtraUserControl
    {
        private IUnitOfWork unitOfWork;
        private readonly PGNRequests request;
        private DocumentHandler documentHandler;

        public UCPGNScanDocuments(PGNRequests request)
        {
            InitializeComponent();
            unitOfWork = new UnitOfWork();
            this.request = request;
            documentHandler = new DocumentHandler(Properties.Settings.Default.NetworkPath,
                Properties.Settings.Default.NetworkUsername,
                Properties.Settings.Default.NetworkPassword);

            LoadData();
        }

        private void LoadData()
        {
            var docs = unitOfWork.PGNDocumentsRepo.FindAllAsync(x => x.PGNRequestId == request.Id);
            gcScanDocs.DataSource = docs.ToList();
        }

        private void pictureEdit1_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
                ImageMenu.ShowPopup(MousePosition);
        }

        private void btnDelete_ItemClick(object sender, ItemClickEventArgs e)
        {
            var msgRes = MessageBox.Show("Delete this Document?", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
            if (msgRes == DialogResult.Cancel) return;

            var row = (PGNDocuments)gridDocs.GetFocusedRow();
            if (row == null) return;

            documentHandler.DeleteImage(row.FileName);
            unitOfWork.PGNDocumentsRepo.DeleteByEx(x => x.Id == row.Id);
            unitOfWork.Save();

            var res = unitOfWork.PGNDocumentsRepo.FindAllAsync(x => x.PGNRequestId == request.Id);

            int order = 1;
            foreach (var doc in res)
            {
                doc.DocOrder = order;
                order++;
            }

            unitOfWork.Save();
            LoadData();
        }

        private void btnPreview_ItemClick(object sender, ItemClickEventArgs e)
        {
            var row = (PGNDocuments)gridDocs.GetFocusedRow();
            if (row == null) return;

            Image img = documentHandler.GetImage(row.FileName);
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

        private void btnScan_ItemClick(object sender, ItemClickEventArgs e)
        {
            var scannedDocs = ScanDocument.ScanImages();
            if (scannedDocs == null) return;

            int DocOrder = 1;
            var docs = unitOfWork.PGNDocumentsRepo.FindAllAsync(x => x.PGNRequestId == request.Id).ToList().OrderBy(o => o.DocOrder);
            if (docs.LastOrDefault() != null) DocOrder = docs.LastOrDefault().DocOrder + 1;
            
            foreach (Image image in scannedDocs)
            {
                var doc = new PGNDocuments
                {
                    FileName = string.Join("-","PGN" , request.Id , DocOrder, ".jpeg"),
                    DocOrder = DocOrder,
                    PGNRequestId = request.Id
                };
                documentHandler.SaveImage(image , Path.Combine(Application.StartupPath, doc.FileName) , doc.FileName);
                unitOfWork.PGNDocumentsRepo.Insert(doc);
                DocOrder += 1;
            }
            unitOfWork.Save();
            LoadData();
        }

        private void gridDocs_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            var row = (PGNDocuments)gridDocs.GetFocusedRow();
            if (row == null) return;

            Image img = documentHandler.GetImage(row.FileName);
            picDocImage.Image = img;
        }
    }
}
