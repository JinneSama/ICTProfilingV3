using Models.Models;
using Models.OFMISEntities;
using Models.Service;
using Models.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models.HRMISEntites
{
    public static class HRMISEmployees
    {
        private static IEnumerable<EmployeesViewModel> _employees;
        public static IEnumerable<ChiefOfOffices> ChiefOfOffices;
        private static HRMISService service;
        public static async Task InitContext()
        {
            service = new HRMISService();
            _employees = await InitEmployees();
            ChiefOfOffices = await GetOffices();
        }

        private static async Task<IEnumerable<ChiefOfOffices>> GetOffices()
        {
            var division = await service.GetDivision();
            var offices = await service.GetOffice();

            var res = offices.GroupJoin(
                division,
                A => A.Office,
                B => B.Office,
                (A, B) => new { A, B })
                .SelectMany(
                    x => x.B.DefaultIfEmpty(),
                    (x, B) => new ChiefOfOffices
                    {
                        ChiefId = B == null || B.ChiefId == null ? x.A.ChiefId : B.ChiefId.Value,
                        Office = x.A.Office,
                        Division = B?.Division
                    });

            return res;
        }
        private async static Task<IEnumerable<EmployeesViewModel>> InitEmployees()
        {
            var employees = await service.GetEmployees();
            return employees;
        }

        public static IEnumerable<EmployeesViewModel> GetChiefOfOffices()
        {
            return ChiefOfOffices.Select(x => GetEmployeeById(x.ChiefId)).ToList();
        }

        public static IEnumerable<EmployeesViewModel> GetEmployees()
        {
            return _employees;
        }

        public static EmployeesViewModel GetEmployeeById(long? Id)
        {
            var emp = _employees.FirstOrDefault(x => x.Id == Id);
            if (Id == null) return null;
            if (emp == null) emp = OFMISEmployees.GetEmployeeById((int)Id);
            return emp;
        }
        public static ChiefOfOffices GetChief(string Office, string Division, long? employeeId)
        {
            ChiefOfOffices chief = null;
            if (string.IsNullOrEmpty(Division) || string.IsNullOrWhiteSpace(Division)) chief = ChiefOfOffices.FirstOrDefault(x => x.Office == Office);
            else chief = ChiefOfOffices.FirstOrDefault(x => x.Division == Division);

            if(chief == null)
            {
                if (employeeId == null) return null;

                var empId = OFMISEmployees.GetEmployeeById((int)employeeId)?.Id;
                if(empId == null) return null;
                var resChief = OFMISEmployees.GetEmployeeById((int)empId);
                var res = new ChiefOfOffices()
                {
                    ChiefId = (long)resChief.Id,
                    Office = resChief.Office,
                    Division = resChief.Division,
                };
                chief = res;
            }
            return chief;
        }
    }

}
