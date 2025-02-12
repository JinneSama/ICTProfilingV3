using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Helpers.NetworkFolder.NetworkModel;
using Newtonsoft.Json;

namespace Helpers.NetworkFolder
{
    public class HTTPNetworkFolder
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

            public async Task UploadFile(Image img, string fileName)
            {
                string jwtToken = await CheckAuthentication();
                using (var content = new MultipartFormDataContent())
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        img.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Jpeg);
                        memoryStream.Seek(0, SeekOrigin.Begin);

                        var imageContent = new StreamContent(memoryStream);
                        imageContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/jpeg");
                        content.Add(imageContent, "file", fileName);

                        httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", jwtToken);

                        var response = await httpClient.PostAsync("upload/", content); 
                        response.EnsureSuccessStatusCode();
                    }
                }
            }

            public async Task<Image> DownloadFile(string fileName)
            {
                string jwtToken = await CheckAuthentication();
                if (string.IsNullOrEmpty(jwtToken)) return null;
                var request = new HttpRequestMessage(HttpMethod.Get, httpClient.BaseAddress + "download/" + fileName);
                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", jwtToken);
                var response = await httpClient.SendAsync(request);

                Image img = null;
                if (response.IsSuccessStatusCode)
                {
                    using (var stream = await response.Content.ReadAsStreamAsync())
                    {
                        img = Image.FromStream(stream);
                    }
                }
                else return null;

                return img;
            }

        public async Task DeleteFile(string fileName)
        {
            string jwtToken = await CheckAuthentication();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);
            var response = await httpClient.DeleteAsync(fileName);

            if(response.StatusCode == System.Net.HttpStatusCode.NotFound) return;
            response.EnsureSuccessStatusCode();
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
