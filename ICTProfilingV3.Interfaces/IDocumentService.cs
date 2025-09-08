using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;

namespace ICTProfilingV3.Interfaces
{
    public interface IDocumentService<T, TKey> : IBaseDataService<T, TKey> where T : class
    {
        Task<EncryptionData?> UploadFile(string docName, TKey parentId);
        Task<IEnumerable<EncryptionData>> ScanFile(string docNamePrefix, TKey parentId);
        Task<EncryptionData?> TakeImage(string docName, TKey parentId);
        Task<EncryptionData?> UploadImage(Image image, string docName, TKey parentId);
        Task<Image> DownloadFile(string docName);
        Task DeleteImage(string docName, TKey Id, TKey parentId);
    }
}
