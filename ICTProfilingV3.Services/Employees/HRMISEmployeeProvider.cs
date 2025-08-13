using ICTProfilingV3.DataTransferModels.ViewModels;
using ICTProfilingV3.DataTransferModels;

namespace ICTProfilingV3.Services.Employees
{
    public class HRMISEmployeeProvider : IEmployeeProvider
    {
        public EmployeesViewModel GetEmployeeById(long? id)
        {
            return HRMISEmployees.GetEmployeeById(id);
        }
    }
}
