using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entities
{
    public class Users : IdentityUser
    {
        public Users()
        {
            ITStaffs = new HashSet<ITStaff>();
            Actions = new HashSet<Actions>();
            CreatedActions = new HashSet<Actions>();
            TechSpecsPreaparedBy = new HashSet<TechSpecs>();
            TechSpecsReviewedBy = new HashSet<TechSpecs>();
            TechSpecsNotedBy = new HashSet<TechSpecs>();
            CustomerActionSheet = new HashSet<CustomerActionSheet>();
            RepairsPreparedBy = new HashSet<Repairs>();
            RepairsReviewedBy = new HashSet<Repairs>();
            RepairsNotedBy = new HashSet<Repairs>();
            PurchaseRequests = new HashSet<PurchaseRequest>();
        }
        public string FullName { get; set; }
        public string Position { get; set; }
        public virtual ICollection<ITStaff> ITStaffs { get; set; }

        [InverseProperty("CreatedBy")]
        public ICollection<Actions> CreatedActions { get; set; }
        public ICollection<Actions> Actions { get; set; }
        public ICollection<TechSpecs> TechSpecsPreaparedBy { get; set; }
        public ICollection<TechSpecs> TechSpecsReviewedBy { get; set; }
        public ICollection<TechSpecs> TechSpecsNotedBy { get; set; }
        public ICollection<CustomerActionSheet> CustomerActionSheet { get; set; }
        public ICollection<Repairs> RepairsPreparedBy { get; set; }
        public ICollection<Repairs> RepairsReviewedBy { get; set; }
        public ICollection<Repairs> RepairsNotedBy { get; set; }
        public ICollection<PurchaseRequest> PurchaseRequests { get; set; }
    }
}
