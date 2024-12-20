using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Helpers.NetworkFolder
{
    public class HTTPNetworkFolder
    {
        private static readonly HttpClient httpClient = new HttpClient
        {
            BaseAddress = new Uri("http://172.17.16.13:8888/api/files/")
        };

        public async Task<List<string>> GetFiles()
        {
            var response = await httpClient.GetAsync("list");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var files = JsonSerializer.Deserialize<string[]>(json);

            return files.ToList();
        }

        public async Task UploadFile(Image img, string fileName)
        {
            using (var content = new MultipartFormDataContent())
            {
                using (var memoryStream = new MemoryStream())
                {
                    img.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Jpeg);
                    memoryStream.Seek(0, SeekOrigin.Begin);
                    content.Add(new StreamContent(memoryStream), "file", fileName);

                    var response = await httpClient.PostAsync("upload/", content);
                    response.EnsureSuccessStatusCode();
                }
            }
        }

        public async Task<Image> DownloadFile(string fileName)
        {
            Image img = null;
            var response = await httpClient.GetAsync("download/" + fileName);
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound) return null;

            using (var stream = await response.Content.ReadAsStreamAsync())
            {
                img = Image.FromStream(stream);
            }
            return img;
        }

        public async Task DeleteFile(string fileName)
        {
            var response = await httpClient.DeleteAsync(fileName);
            response.EnsureSuccessStatusCode();
        }
    }
}
