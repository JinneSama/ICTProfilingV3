using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Helpers.NetworkFolder
{
    public class HTTPNetworkFolder
    {
        private readonly HttpClient httpClient;
        public HTTPNetworkFolder()
        {
            httpClient = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:8085/api/files/")
            };
        }

        public async Task<List<string>> GetFiles()
        {
            var response = await httpClient.GetAsync("list");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var files = JsonSerializer.Deserialize<string[]>(json);

            return files.ToList();
        }

        public async Task UploadFile(string filePath, string fileName)
        {
            using (var content = new MultipartFormDataContent())
            {
                var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                content.Add(new StreamContent(fileStream), "file", fileName);

                var response = await httpClient.PostAsync("upload/", content);
                response.EnsureSuccessStatusCode();
            }
        }

        public async Task DownloadFile(string fileName, string savePath)
        {
            var response = await httpClient.GetAsync("download/" + fileName);
            response.EnsureSuccessStatusCode();

            using (var stream = await response.Content.ReadAsStreamAsync())
            {
                using (var fileStream = new FileStream(savePath, FileMode.Create, FileAccess.Write))
                {
                    await stream.CopyToAsync(fileStream);
                }
            }
        }

        public async Task DeleteFile(string fileName)
        {
            var response = await httpClient.DeleteAsync(fileName);
            response.EnsureSuccessStatusCode();
        }
    }
}
