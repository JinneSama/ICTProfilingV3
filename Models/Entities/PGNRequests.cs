using Models.Enums;
using Newtonsoft.Json;
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
            RecordsRequestStatus = new HashSet<RecordsRequestStatus>();
        }
        public int Id { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? RequestDate { get; set; }
        public CommunicationType CommunicationType { get; set; }
        public long? SignatoryId { get; set; }
        public string Subject { get; set; }
        public TicketStatus? Status { get; set; }
        public string CreatedById { get; set; }
        [ForeignKey("CreatedById")]
        public Users CreatedByUser { get; set; }
        public virtual ICollection<Actions> Actions { get; set; }
        [JsonIgnore]
        public virtual ICollection<PGNAccounts> PGNAccounts { get; set; }
        [JsonIgnore]
        public virtual ICollection<PGNDocuments> PGNDocuments { get; set; }
        [JsonIgnore]
        public virtual ICollection<RecordsRequestStatus> RecordsRequestStatus { get; set; }
    }
}
