using System.IO;
using System.Net;
using System.Drawing;

namespace Helpers.NetworkFolder
{
    public class DocumentHandler
    {
        private readonly string networkPath;
        private readonly string username;
        private readonly string password;
        static NetworkCredential credentials;

        public DocumentHandler(string networkPath, string username, string password)
        {
            this.networkPath = networkPath;
            this.username = username;
            this.password = password;
            credentials = new NetworkCredential(username, password);
        }

        public void SaveImage(Image image, string sourcePath , string fileName)
        {
            image.Save(sourcePath);
            var destPath = Path.Combine(this.networkPath, fileName);
            FileUpload(sourcePath, destPath);
            File.Delete(sourcePath);
        }

        public void FileUpload(string imgPath, string destPath)
        {
            using (new ConnectToFolder(networkPath, credentials))
            {;
                File.Copy(imgPath, destPath,true);
            }
        }
        public Image GetImage(string fileName)
        {
            using (new ConnectToFolder(networkPath, credentials))
            {
                byte[] imgbyte = DownloadFileByte(Path.Combine(networkPath, fileName));
                if (imgbyte == null) return null;
                return Image.FromStream(new MemoryStream(imgbyte));
            }
        }
        public byte[] DownloadFileByte(string DownloadURL)
        {
            byte[] fileBytes = null;
            using (new ConnectToFolder(networkPath, credentials))
            {
                fileBytes = File.ReadAllBytes(DownloadURL);
            }
            return fileBytes;
        }

        public void DeleteImage(string fileName)
        {
            using (new ConnectToFolder(networkPath, credentials))
            {
                var dest = Path.Combine(networkPath ,fileName);
                File.Delete(dest);
            }
        }
    }
}
