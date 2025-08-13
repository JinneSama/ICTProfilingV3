using ICTProfilingV3.DataTransferModels.ServiceModels.DTOModels;
using ICTProfilingV3.DataTransferModels.ViewModels;
using ICTProfilingV3.Mapper;
using ICTProfilingV3.Mapper.Configurations;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace ICTProfilingV3.Services.Employees
{
    public class HRMISService
    {
        private readonly MapperInitializer _mapper;
        private static readonly HttpClient httpClient = new HttpClient
        {
            BaseAddress = new Uri(ConfigurationManager.AppSettings["HRMISURL"])
        };

        private readonly AuthEPiSBackend AuthEPiSBackend;
        public HRMISService(MapperInitializer mapper)
        {
            _mapper = mapper;
            AuthEPiSBackend = new AuthEPiSBackend();
        }

        public async Task<IEnumerable<EmployeesViewModel>> GetEmployees()
        {
            var token = await AuthEPiSBackend.CheckAuthentication();
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            var response = await httpClient.GetAsync("employees");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var employees = JsonSerializer.Deserialize<List<HRMISEmployeesDto>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            var empViewModel = employees.Select(x => _mapper.MapperConfig.MapTo<HRMISEmployeesDto,EmployeesViewModel>(x)).ToList();
            return empViewModel;
        }

        public async Task<IEnumerable<HRMISOfficeDto>> GetOffice()
        {
            var token = await AuthEPiSBackend.CheckAuthentication();
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            var response = await httpClient.GetAsync("office");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var office = JsonSerializer.Deserialize<List<HRMISOfficeDto>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            return office;
        }

        public async Task<IEnumerable<HRMISDivisionDto>> GetDivision()
        {
            var token = await AuthEPiSBackend.CheckAuthentication();
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            var response = await httpClient.GetAsync("division");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var division = JsonSerializer.Deserialize<List<HRMISDivisionDto>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            return division;
        }
    }
}
