using Models.Entities;
using Models.HRMISEntites;

namespace Models.ViewModels
{
    public class PGNRequestViewModel
    {
        public PGNRequests PGNRequest { get; set; }
        public string ReqNo => string.Join("-", "PGN", PGNRequest.Id);
        public EmployeesViewModel Employee => PGNRequest.SignatoryId == null ? null : HRMISEmployees.GetEmployeeById(PGNRequest.SignatoryId);
    }
}
