using Models.Enums;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entities
{
    public class CustomerActionSheet
    {
        public int Id { get; set; }
        public long? ClientId { get; set; }
        public DateTime? DateCreated { get; set; }
        public string ClientName { get; set; }
        public string Office { get; set; }
        public Gender Gender { get; set; }
        public string ContactNo { get; set; }
        public string ClientRequest { get; set; }
        public string ActionTaken { get; set; }
        public TicketStatus? Status { get; set; }
        public bool? IsDeleted { get; set; }
        public int? OldPk { get; set; }
        public string CreatedById { get; set; }

        [ForeignKey("CreatedById")]
        public Users CreatedBy { get; set; }
        public string AssistedById { get; set; }

        [ForeignKey("AssistedById")]
        public Users AssistedBy { get; set; }
        public virtual ICollection<Actions> Actions { get; set; }
        [JsonIgnore]
        public virtual ICollection<RecordsRequestStatus> RecordsRequestStatus { get; set; }
        [JsonIgnore]
        public virtual ICollection<EvaluationSheet> EvaluationSheets { get; set; }

        public CustomerActionSheet()
        {
            Actions = new HashSet<Actions>();
            RecordsRequestStatus = new HashSet<RecordsRequestStatus>();
            EvaluationSheets = new HashSet<EvaluationSheet>();
        }
    }
}
