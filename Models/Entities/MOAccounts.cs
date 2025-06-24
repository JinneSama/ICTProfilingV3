using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [MaxLength(1024)]
        public string PrincipalName { get; set; }
        [MaxLength(1024)]
        public string Password { get; set; }
        public DateTime? DateCreated { get; set; }
        public int? OfficeId { get; set; }
        [ForeignKey("OfficeId")]
        public PGNGroupOffices Office { get; set; } 
        public string CreatedById { get; set; }
        [ForeignKey("CreatedById")]
        public Users CreatedBy { get; set; }
        [JsonIgnore]
        public virtual ICollection<MOAccountUsers> MOAccountUsers { get; set; }
    }
}
