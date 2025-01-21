using Models.Service;
using Models.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models.OFMISEntities
{
    public class OFMISEmployees
    {
        private static IEnumerable<EmployeesViewModel> employees;
        private static OFMISService service;
        public static async Task InitEmployees()
        {
            service = new OFMISService();
            var emp = await service.GetEmployees();
            employees = emp;
        }
        public static EmployeesViewModel GetEmployeeById(int Id)
        {
            var employee = employees.FirstOrDefault(x => x.Id == Id);
            if (employee == null) return null;
            return employee;
        }

        public static EmployeesViewModel GetChief(int employeeId)
        {
            var employee = employees.FirstOrDefault(x => x.Id == employeeId);
            var chief = employees.FirstOrDefault(x => x.Id == employee.ChiefId);
            return chief;
        }

        public static IEnumerable<EmployeesViewModel> GetAllEmployees()
        {
            return employees;
        }
    }
}
