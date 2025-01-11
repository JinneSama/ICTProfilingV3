using Models.HRMISEntites;
using Models.Models;
using Models.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace Models.OFMISEntities
{
    public class OFMISEmployees
    {
        private static OFMISEntities ofmisEntities;
        private static IQueryable<Employee> employees;
        public static void InitEmployees()
        {
            ofmisEntities = new OFMISEntities();
            var emp = ofmisEntities.Employees;
            employees = emp;
        }
        public static EmployeesViewModel GetEmployeeById(int Id)
        {
            var employee = employees.FirstOrDefault(x => x.Id == Id);
            if (employee == null) return null;
            EmployeesViewModel employeeModel = new EmployeesViewModel
            {
                Id = employee.Id,
                Employee = employee.LastName + " " + employee.FirstName + " " + employee.MiddleName + " " + employee.ExtName,
                Position = employee.Position,
                Office = employee.OffcAcr
            };
            return employeeModel;
        }

        private static EmployeesViewModel GetChief(int employeeId)
        {
            var employee = employees.FirstOrDefault(x => x.Id == employeeId);
            var chief = employees.FirstOrDefault(x => x.Id == employee.Office.EmployeeId);
            EmployeesViewModel employeeModel = new EmployeesViewModel
            {
                Id = chief.Office.EmployeeId,
                Employee = chief.LastName + " " + chief.FirstName + " " + chief.MiddleName + " " + chief.ExtName,
                Position = chief.Position,
                Office = chief.OffcAcr
            };
            return employeeModel;
        }

        public static IEnumerable<EmployeesViewModel> GetAllEmployees()
        {
            var emps = employees.Select(x => new EmployeesViewModel
            {
                Id = x.Id,
                Employee = x.LastName + " " + x.FirstName + " " + x.MiddleName + " " + x.ExtName,
                Position = x.Position,
                Office = x.OffcAcr
            });
            return emps.ToList();
        }
    }
}
