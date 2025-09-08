using ICTProfilingV3.Core.Enums;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;

namespace ICTProfilingV3.Interfaces
{
    public interface IHTTPNetworkFolder
    {
        Task<List<string>> GetFiles();
        Task UploadFile(object fileData, string fileName, FileType? fileType = null);
        Task<Image> DownloadFile(string fileName, FileType? fileType = null);
        Task DeleteFile(string fileName);
        Task UploadJsonFile(string filePath, string fileName);
        Task<string> AuthenticateUser(string username, string password, string fileName);
    }
}
