using Models.Entities;
using Models.HRMISEntites;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Models.ViewModels
{
    public class MOAccountsViewModel
    {
        public MOAccounts MOAccount { get; set; }
        public IEnumerable<MOAccountUsers> MOAccountUsers { get; set; }
        public BindingList<AccountUsers> AccountUser => new BindingList<AccountUsers>(MOAccountUsers.Select(x => new AccountUsers
        {
            MOAccountUser = x
        }).ToList());
    }

    public class AccountUsers
    {
        public MOAccountUsers MOAccountUser { get; set; }
        public string EPiSNo => "EPiS-M365-" + MOAccountUser.Id; 
        public EmployeesViewModel IssuedTo => HRMISEmployees.GetEmployeeById(MOAccountUser.IssuedTo);
        public EmployeesViewModel User => HRMISEmployees.GetEmployeeById(MOAccountUser.AccountUser);
    }
}
