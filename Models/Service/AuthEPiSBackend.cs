using System.Configuration;
using System.Net.Http;
using System;
using System.Text;
using System.Threading.Tasks;
using Models.Service.AuthModels;

namespace Models.Service
{
    public class AuthEPiSBackend
    {
        private static readonly HttpClient httpAuthClient = new HttpClient
        {
            BaseAddress = new Uri(ConfigurationManager.AppSettings["AuthURL"])
        };

        public async Task<string> CheckAuthentication()
        {
            string username = ConfigurationManager.AppSettings["Username"];
            string password = ConfigurationManager.AppSettings["Password"];

            string token = TokenCache.CheckCache();
            if (string.IsNullOrEmpty(token))
            {
                token = await AuthenticateUser(username, password, "");
                TokenCache.StoreCache(token);
            };
            return token;
        }

        private async Task<string> AuthenticateUser(string username, string password, string fileName)
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
            }
            else return string.Empty;
        }
    }
}
