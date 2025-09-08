using ICTProfilingV3.DataTransferModels.ViewModels;

namespace ICTProfilingV3.DataTransferModels
{
    public interface IEmployeeProvider
    {
        EmployeesViewModel GetEmployeeById(long? id);
    }
}
