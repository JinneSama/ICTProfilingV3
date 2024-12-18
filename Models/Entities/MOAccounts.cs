using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entities
{
    public class MOAccounts
    {
        public MOAccounts()
        {
            MOAccountUsers = new HashSet<MOAccountUsers>();
        }
        public int Id { get; set; }
        public string PrincipalName { get; set; }
        public string Password { get; set; }
        public DateTime? DateCreated { get; set; }
        public int? OfficeId { get; set; }
        [ForeignKey("OfficeId")]
        public PGNGroupOffices Office { get; set; } 
        public string CreatedById { get; set; }
        [ForeignKey("CreatedById")]
        public Users CreatedBy { get; set; }
        public virtual ICollection<MOAccountUsers> MOAccountUsers { get; set; }
    }
}
