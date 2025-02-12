using System.Configuration;
using System.Net.Http;
using System;
using Models.Service.DTOModels;
using System.Threading.Tasks;

namespace Models.Service
{
    public class FDTSService
    {
        private static readonly HttpClient httpClient = new HttpClient
        {
            BaseAddress = new Uri(ConfigurationManager.AppSettings["FDTSURL"])
        };

        private readonly AuthEPiSBackend AuthEPiSBackend;

        public FDTSService()
        {
            AuthEPiSBackend = new AuthEPiSBackend();
        }
        public async Task<FDTSPRDetailsDto> GetDetails(string controlNo)
        {
            string jwtToken = await AuthEPiSBackend.CheckAuthentication();
            if (string.IsNullOrEmpty(jwtToken)) return null;
            var request = new HttpRequestMessage(HttpMethod.Get, httpClient.BaseAddress + "prdetails/" + controlNo);
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", jwtToken);
            var response = await httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode) return null;

            var json = await response.Content.ReadAsStringAsync();
            var fdtsDetails = Newtonsoft.Json.JsonConvert.DeserializeObject<FDTSPRDetailsDto>(json);
            return fdtsDetails;
        }
    }
}
