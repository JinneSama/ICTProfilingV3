using ICTProfilingV3.DataTransferModels.ViewModels;

namespace ICTProfilingV3.DataTransferModels
{
    public class EmployeeInfoDTM
    {
        public EmployeesViewModel Requestor { get; set; }
        public EmployeesViewModel Chief { get; set; }
        public EmployeesViewModel DeliveredBy { get; set; }
    }
}
