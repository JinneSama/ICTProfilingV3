using Models.HRMISEntites;
using Models.Models;

namespace Models.ViewModels
{
    public class EmployeesViewModel
    {
        public long? Id { get; set; }
        public string Employee { get; set; }
        public string Position { get; set; }
        public string Office { get; set; }
        public string Division { get; set; }
        public ChiefOfOffices ChiefOfOffices => HRMISEmployees.GetChief(Office, Division);
    }
}
