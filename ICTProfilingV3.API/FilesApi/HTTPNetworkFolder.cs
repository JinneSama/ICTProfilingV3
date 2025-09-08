using ICTProfilingV3.API.ApiModels;
using ICTProfilingV3.Core.Enums;
using ICTProfilingV3.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ICTProfilingV3.API.FilesApi
{
    public class HTTPNetworkFolder : IHTTPNetworkFolder
    {
        private static readonly HttpClient httpClient = new HttpClient
        {
            BaseAddress = new Uri(ConfigurationManager.AppSettings["FolderURL"])
        };

        private static readonly HttpClient httpAuthClient = new HttpClient
        {
            BaseAddress = new Uri(ConfigurationManager.AppSettings["AuthURL"])
        };

        public async Task<List<string>> GetFiles()
        {
            var response = await httpClient.GetAsync("list");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var files = System.Text.Json.JsonSerializer.Deserialize<string[]>(json);

            return files.ToList();
        }

        public async Task UploadFile(object fileData, string fileName, FileType? fileType = null)
        {
            string jwtToken = await CheckAuthentication();

            using (var content = new MultipartFormDataContent())
            {
                StreamContent fileContent;

                if (fileData is Image img)
                {
                    var memoryStream = new MemoryStream();
                    img.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Jpeg);
                    memoryStream.Seek(0, SeekOrigin.Begin);

                    fileContent = new StreamContent(memoryStream);
                    fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/jpeg");
                }
                else if (fileData is byte[] bytes)
                {
                    var memoryStream = new MemoryStream(bytes);
                    fileContent = new StreamContent(memoryStream);

                    fileContent.Headers.ContentType =
                        new System.Net.Http.Headers.MediaTypeHeaderValue("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
                }
                else if (fileData is Stream stream)
                {
                    fileContent = new StreamContent(stream);
                    fileContent.Headers.ContentType =
                        new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");
                }
                else
                {
                    throw new ArgumentException("Unsupported file type. Use Image, byte[], or Stream.");
                }

                content.Add(fileContent, "file", fileName);

                httpClient.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", jwtToken);

                var url = (fileType == null) ? "upload/" : "uploadcomparison/";
                var response = await httpClient.PostAsync(url, content);
                response.EnsureSuccessStatusCode();
            }
        }


        public async Task<Image> DownloadFile(string fileName, FileType? fileType = null)
        {
            string jwtToken = await CheckAuthentication();
            if (string.IsNullOrEmpty(jwtToken)) return null;

            string filePathURL = string.Empty;
            if (fileType == null)
                filePathURL = "download/";
            else
                filePathURL = "downloadcomparison/";

            var request = new HttpRequestMessage(HttpMethod.Get, httpClient.BaseAddress + filePathURL + fileName);
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", jwtToken);

            var response = await httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode) return null;

            if (fileType == FileType.Excel)
            {
                byte[] fileBytes = await response.Content.ReadAsByteArrayAsync();

                using (var saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "Excel Files (*.xlsx)|*.xlsx";
                    saveFileDialog.FileName = fileName;

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        File.WriteAllBytes(saveFileDialog.FileName, fileBytes);
                        var result = MessageBox.Show(
                            "File downloaded successfully. Do you want to open it?",
                            "Open File",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question
                        );

                        if (result == DialogResult.Yes)
                        {
                            Process.Start(new ProcessStartInfo(saveFileDialog.FileName) { UseShellExecute = true });
                        }
                    }
                }

                return null;
            }
            else
            {
                using (var stream = await response.Content.ReadAsStreamAsync())
                {
                    return Image.FromStream(stream);
                }
            }
        }

        public async Task DeleteFile(string fileName)
        {
            string jwtToken = await CheckAuthentication();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);
            var response = await httpClient.DeleteAsync(fileName);

            if(response.StatusCode == System.Net.HttpStatusCode.NotFound) return;
            response.EnsureSuccessStatusCode();
        }

        public async Task UploadJsonFile(string filePath, string fileName)
        {
            string jwtToken = await CheckAuthentication();

            using (var content = new MultipartFormDataContent())
            {
                using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    var fileContent = new StreamContent(fileStream);
                    fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                    content.Add(fileContent, "file", fileName);

                    httpClient.DefaultRequestHeaders.Authorization =
                        new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", jwtToken);

                    var response = await httpClient.PostAsync("uploadJSON/", content);
                    response.EnsureSuccessStatusCode();
                }
            }
        }

        private async Task<string> CheckAuthentication()
        {
            string username = ConfigurationManager.AppSettings["Username"];
            string password = ConfigurationManager.AppSettings["Password"];

            string token = TokenCache.CheckCache();
            if (string.IsNullOrEmpty(token))
            {
                token = await AuthenticateUser(username, password, "");
                TokenCache.StoreCache(token);
            }
            return token;
        }

        public async Task<string> AuthenticateUser(string username, string password, string fileName)
        {
            var loginRequest = new LoginRequest
            {
                Username = username,
                Password = password,
                FileName = fileName
            };

            var content = new StringContent(
                Newtonsoft.Json.JsonConvert.SerializeObject(loginRequest),
                Encoding.UTF8,
                "application/json");

            var response = await httpAuthClient.PostAsync("login/", content);

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var tokenResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<TokenResponse>(jsonResponse);
                return tokenResponse.Token;
            }else return string.Empty;
        }
    }
}
