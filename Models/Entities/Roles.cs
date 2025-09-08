using Microsoft.AspNet.Identity.EntityFramework;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models.Entities
{
    public class Roles : IdentityRole
    {
        [MaxLength(1024)]
        public string Description { get; set; }
        [JsonIgnore]
        public virtual ICollection<RoleDesignation> RoleDesignations { get; set; } = new List<RoleDesignation>();
        public Roles()
        {
            RoleDesignations = new HashSet<RoleDesignation>();
        }
    }
}
