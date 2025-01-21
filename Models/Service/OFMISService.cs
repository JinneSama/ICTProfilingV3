using System.Configuration;
using System.Net.Http;
using System;
using Models.Service.DTOModels;
using Models.ViewModels;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using System.Linq;
using Models.MappingConfigurations;

namespace Models.Service
{
    public class OFMISService
    {
        private static readonly HttpClient httpClient = new HttpClient
        {
            BaseAddress = new Uri(ConfigurationManager.AppSettings["OFMISURL"])
        };

        private readonly AuthEPiSBackend AuthEPiSBackend;
        public OFMISService()
        {
            AuthEPiSBackend = new AuthEPiSBackend();
        }

        public async Task<IEnumerable<EmployeesViewModel>> GetEmployees()
        {
            var token = await AuthEPiSBackend.CheckAuthentication();
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            var response = await httpClient.GetAsync("employees");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var employees = System.Text.Json.JsonSerializer.Deserialize<List<OFMISEmployeesDto>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            var empViewModel = employees.Select(x => MappingConfig.MapperConfig.MapTo<OFMISEmployeesDto, EmployeesViewModel>(x)).ToList();
            return empViewModel;
        }

        public async Task<OFMISUsersDto> GetUser(string username)
        {
            string jwtToken = await AuthEPiSBackend.CheckAuthentication();
            if (string.IsNullOrEmpty(jwtToken)) return null;
            var request = new HttpRequestMessage(HttpMethod.Get, httpClient.BaseAddress + "user/" + username);
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", jwtToken);
            var response = await httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var user = System.Text.Json.JsonSerializer.Deserialize<OFMISUsersDto>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            return user;
        }
    }
}
