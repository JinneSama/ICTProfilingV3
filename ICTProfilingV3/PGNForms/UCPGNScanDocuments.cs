using DevExpress.Data.ODataLinq.Helpers;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using Helpers.Scanner;
using ICTProfilingV3.Interfaces;
using Models.Entities;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ICTProfilingV3.PGNForms
{
    public partial class UCPGNScanDocuments : DevExpress.XtraEditors.XtraUserControl
    {
        private readonly IPGNService _pgnService;
        private PGNRequests _request;

        public UCPGNScanDocuments(IPGNService pgnService)
        {
            _pgnService = pgnService;
            InitializeComponent();
            LoadData();
        }

        public void InitUC(PGNRequests request)
        {
            _request = request;
        }

        private void LoadData()
        {
            if (_request == null) return;
            var docs = _pgnService.PGNDocumentService.GetAll().Where(x => x.PGNRequestId == _request.Id);
            gcScanDocs.DataSource = docs.ToList();
        }

        private void pictureEdit1_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
                ImageMenu.ShowPopup(MousePosition);
        }

        private async void btnDelete_ItemClick(object sender, ItemClickEventArgs e)
        {
            var msgRes = MessageBox.Show("Delete this Document?", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
            if (msgRes == DialogResult.Cancel) return;

            var row = (PGNDocuments)gridDocs.GetFocusedRow();
            if (row == null) return;

            await _pgnService.PGNDocumentService.DeleteImage(row.FileName, row.Id, _request.Id);
            LoadData();
        }

        private async void btnPreview_ItemClick(object sender, ItemClickEventArgs e)
        {
            splashScreenDownload.ShowWaitForm();
            var row = (PGNDocuments)gridDocs.GetFocusedRow();
            if (row == null) return;

            Image img = await _pgnService.PGNDocumentService.DownloadFile(row.FileName);
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
            splashScreenDownload.CloseWaitForm();
            xtraForm.ShowDialog();
        }

        private async void btnScan_ItemClick(object sender, ItemClickEventArgs e)
        {
            var scannedDocs = ScanDocument.ScanImages();
            if (scannedDocs == null) return;

            await _pgnService.PGNDocumentService.ScanFile("PGN", _request.Id);
            LoadData();
            splashScreenUpload.CloseWaitForm();
        }

        private async void gridDocs_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            var row = (PGNDocuments)gridDocs.GetFocusedRow();
            if (row == null) return;
            picDocImage.Image = null;
            progressDownload.Visible = true;

            picDocImage.Image = await _pgnService.PGNDocumentService.DownloadFile(row.FileName);
            progressDownload.Visible = false;               
        }
    }
}
