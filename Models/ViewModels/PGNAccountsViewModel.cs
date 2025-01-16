using Models.Entities;
using Models.HRMISEntites;

namespace Models.ViewModels
{
    public class PGNAccountsViewModel
    {
        private EmployeesViewModel Employee()
        {
            return HRMISEmployees.GetEmployeeById(EmployeeId);
        }
        public PGNAccounts PGNAccount { get; set; }
        public bool IsNonEmployee => PGNAccount?.PGNNonEmployeeId == null ? false : true;
        public long? EmployeeId
        {
            get
            {
                if(PGNAccount.PGNNonEmployeeId == null) return PGNAccount?.HRMISEmpId;
                return (long?)PGNAccount?.PGNNonEmployeeId;
            }
        }
        public string Position
        {
            get
            {
                if (PGNAccount.PGNNonEmployeeId == null) return Employee()?.Position;
                return PGNAccount.PGNNonEmployee.Position;
            }
        }
        public string Name
        {
            get
            {
                if (PGNAccount.PGNNonEmployeeId == null) return Employee()?.Employee;
                return PGNAccount.PGNNonEmployee.FullName;
            }
        }
    }
}
