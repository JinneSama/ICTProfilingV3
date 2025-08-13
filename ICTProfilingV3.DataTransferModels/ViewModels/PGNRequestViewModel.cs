using Models.Entities;

namespace ICTProfilingV3.DataTransferModels.ViewModels
{
    public class PGNRequestViewModel
    {
        public PGNRequests PGNRequest { get; set; }
        public string ReqNo => string.Join("-", "PGN", PGNRequest.Id);
        public EmployeesViewModel Employee => PGNRequest.SignatoryId == null ? null : EmployeeProviderAccessor.Provider?.GetEmployeeById(PGNRequest.SignatoryId);
    }
}
