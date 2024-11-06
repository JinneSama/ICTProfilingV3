using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;

namespace Models.Entities
{
    public class Roles : IdentityRole
    {
        public string Description { get; set; }
        public virtual ICollection<RoleDesignation> RoleDesignations { get; set; } = new List<RoleDesignation>();
        public Roles()
        {
            RoleDesignations = new HashSet<RoleDesignation>();
        }
    }
}
