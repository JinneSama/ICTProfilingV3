using Models.Models;
using Models.ViewModels;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Models.HRMISEntites
{
    public static class HRMISEmployees
    {
        private static HRMISv2Entities _context;
        private static IEnumerable<EmployeesViewModel> _employees;
        public static IQueryable<ChiefOfOffices> ChiefOfOffices;
        public static async void InitContext()
        {
            _context = new HRMISv2Entities();
            _employees = await InitEmployees();
            ChiefOfOffices = GetOffices();
        }

        private static IQueryable<ChiefOfOffices> GetOffices()
        {
            var offices = _context.Offices;
            var division = _context.Divisions;

            var res = offices.GroupJoin(
                division,
                A => A.fldOfficeID,
                B => B.fldOfficeID,
                (A, B) => new { A, B }).SelectMany(x => x.B.DefaultIfEmpty(),
                (x, B) => new ChiefOfOffices
                {
                    ChiefId = (long)(B.fldEmpID == null ? x.A.fldEmpID : B.fldEmpID),
                    Office = x.A.fldOfficeID,
                    Division = B.fldDivision
                });
            return res;
        }
        private async static Task<IEnumerable<EmployeesViewModel>> InitEmployees()
        {
            var employees = _context.Employees.Where(x => x.fldDeleted == false)
                .Select(x => new EmployeesViewModel
                {
                    Id = x.fldEmpID,
                    Employee = x.fldLastname + ", " + x.fldFirstname + " " + x.fldMIddleName + " " + x.fldNameExt,
                    Office = x.flddetailed == true ? x.flddetailedTo : x.fldOfficeID,
                    Division = x.flddetailed == true ? x.flddetailedToDivision : x.fldDivision,
                    Position = x.fldPosition,
                    Username = x.fldUsername
                });
            return await employees.ToListAsync();
        }

        public static IEnumerable<EmployeesViewModel> GetChiefOfOffices()
        {
            return ChiefOfOffices.ToList().Select(x => GetEmployeeById(x.ChiefId)).ToList();
        }

        public static IEnumerable<EmployeesViewModel> GetEmployees()
        {
            return _employees;
        }

        public static EmployeesViewModel GetEmployeeById(long? Id)
        {
            return _employees.FirstOrDefault(x => x.Id == Id);
        }
        public static ChiefOfOffices GetChief(string Office, string Division)
        {
            if (string.IsNullOrEmpty(Division) || string.IsNullOrWhiteSpace(Division)) return ChiefOfOffices.FirstOrDefault(x => x.Office == Office);
            else return ChiefOfOffices.FirstOrDefault(x => x.Division.Contains(Division));
        }
    }

}
