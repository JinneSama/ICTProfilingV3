using ICTProfilingV3.DataTransferModels.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ICTProfilingV3.Services.Employees
{
    public class OFMISEmployees
    {
        private static IEnumerable<EmployeesViewModel> employees;
        private static OFMISService _service;
        public static async Task InitEmployees(OFMISService service)
        {
            _service = service;
            var emp = await _service.GetEmployees();
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
