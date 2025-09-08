using DevExpress.XtraEditors.Camera;
using ICTProfilingV3.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace ICTProfilingV3.Services.Base
{
    public class DocumentService<T, TKey> : BaseDataService<T, TKey>, IDocumentService<T, TKey> where T : class
    {
        private readonly IScanDocument _scanDocument;
        private readonly IHTTPNetworkFolder _networkFolder;
        private readonly IEncryptFile _encryptFile;
        public DocumentService(IRepository<TKey, T> baseRepo, IHTTPNetworkFolder networkFolder, 
            IScanDocument scanDocument) : base(baseRepo)
        {
            _networkFolder = networkFolder;
            _scanDocument = scanDocument;
        }

        public virtual async Task DeleteImage(string docName, TKey Id, TKey parentId)
        {
            await _networkFolder.DeleteFile(docName);
        }

        public virtual async Task<Image> DownloadFile(string docName)
        {
            return await _networkFolder.DownloadFile(docName);
        }

        public virtual async Task<IEnumerable<EncryptionData>> ScanFile(string docNamePrefix, TKey parentId)
        {
            List<EncryptionData> data = new List<EncryptionData>();
            var scannedDocs = _scanDocument.ScanImages();
            if (scannedDocs.Count <= 0 || scannedDocs == null) return null;

            int docOrder = 1;
            foreach (var scannedDoc in scannedDocs)
            {
                var encryptionData = _encryptFile.EncryptFile($"{docNamePrefix}-{parentId}-{docOrder}");
                var fileName = encryptionData.filename + ".jpeg";
                await _networkFolder.UploadFile(scannedDoc, fileName);
                data.Add(encryptionData);
                docOrder++;
            }
            return data;
        }
        
        public virtual async Task<EncryptionData?> TakeImage(string docName, TKey parentId)
        {
            TakePictureDialog dialog = new TakePictureDialog();
            dialog.ResolutionMode = ResolutionMode.Maximum;
            if (dialog.ShowDialog() == DialogResult.Cancel) return null;

            var image = dialog.Image;
            var encryptionData = _encryptFile.EncryptFile(docName);
            var fileName = encryptionData.filename + ".jpeg";
            await _networkFolder.UploadFile(image, fileName);
            return encryptionData;
        }

        public virtual async Task<EncryptionData?> UploadFile(string docName, TKey parentId)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif;*.tiff|All Files|*.*";
            openFileDialog.Title = "Select an Image";
            if (openFileDialog.ShowDialog() == DialogResult.Cancel) return null;

            string selectedFilePath = openFileDialog.FileName;
            Image image = Image.FromFile(selectedFilePath);
            var encryptionData = _encryptFile.EncryptFile(docName);
            var fileName = encryptionData.filename + ".jpeg";
            await _networkFolder.UploadFile(image, fileName);
            return encryptionData;
        }

        public async Task ReOrderDocument<TPKey, TOrder>(TKey parentId, Expression<Func<T, TPKey>> parentProperty, Expression<Func<T, TOrder>> orderProperty)
        {
            var docs = base.GetAll().Where(Expression.Lambda<Func<T,bool>>(
                Expression.Equal(parentProperty.Body, 
                Expression.Constant(parentId, typeof(TPKey))), 
                parentProperty.Parameters)
            );

            int order = 1;
            foreach (var doc in docs)
            {
                typeof(T).GetProperty(orderProperty.Name)?.SetValue(doc, order);
                order++;
            }
            await base.SaveChangesAsync();
        }

        public virtual async Task<EncryptionData?> UploadImage(Image image, string docName, TKey parentId)
        {
            var fileName = docName;
            await _networkFolder.UploadFile(image, fileName);
            return null;
        }
    }
}
