using Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entities
{
    public class PGNRequests
    {
        public PGNRequests()
        {
            Actions = new HashSet<Actions>();
            PGNAccounts = new HashSet<PGNAccounts>();
            PGNDocuments = new HashSet<PGNDocuments>();
        }
        public int Id { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? RequestDate { get; set; }
        public CommunicationType CommunicationType { get; set; }
        public long? SignatoryId { get; set; }
        public string Subject { get; set; }
        public string CreatedById { get; set; }
        [ForeignKey("CreatedById")]
        public Users CreatedByUser { get; set; }
        public virtual ICollection<Actions> Actions { get; set; }
        public virtual ICollection<PGNAccounts> PGNAccounts { get; set; }
        public virtual ICollection<PGNDocuments> PGNDocuments { get; set; }    
    }
}
