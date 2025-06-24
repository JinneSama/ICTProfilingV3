using Models.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entities
{
    public class Actions
    {
        public Actions()
        {
            RoutedUsers = new HashSet<Users>();
            ActionDocuments = new HashSet<ActionDocuments>();
        }
        public int Id { get; set; }
        [MaxLength(512)]
        public string ActionTaken { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? ActionDate { get; set; }
        [MaxLength(1024)]
        public string Remarks { get; set; }
        public bool? IsSend { get; set; }
        public RequestType RequestType { get; set; }
        public int? ProgramId { get; set; }

        [ForeignKey("ProgramId")]
        public ActionsDropdowns ProgramDropdowns { get; set; }
        public int? MainActId { get; set; }

        [ForeignKey("MainActId")]
        public ActionsDropdowns MainActDropdowns { get; set; }
        public int? ActivityId { get; set; }

        [ForeignKey("ActivityId")]
        public ActionsDropdowns ActivityDropdowns { get; set; }
        public int? SubActivityId { get; set; }

        [ForeignKey("SubActivityId")]
        public ActionsDropdowns SubActivityDropdowns { get; set; }

        public int? DeliveriesId { get; set; }
        [ForeignKey("DeliveriesId")]
        public Deliveries Deliveries { get; set; }
        public int? TechSpecsId { get; set; }
        [ForeignKey("TechSpecsId")]
        public TechSpecs TechSpecs { get; set; }
        public int? RepairId { get; set; }
        [ForeignKey("RepairId")]
        public Repairs Repairs { get; set; }
        public int? PurchaseRequestId { get; set; }
        [ForeignKey("PurchaseRequestId")]
        public PurchaseRequest PurchaseRequest { get; set; }
        public int? CustomerActionSheetId { get; set; }
        [ForeignKey("CustomerActionSheetId")]
        public CustomerActionSheet CustomerActionSheet { get; set; }
        public int? PGNRequestId { get; set; }
        [ForeignKey("PGNRequestId")]
        public PGNRequests PGNRequests { get; set; }
        public int? MOAccountUserId { get; set; }
        [ForeignKey("MOAccountUserId")]
        public MOAccountUsers MOAccountUsers { get; set; }

        public string CreatedById { get; set; }

        [ForeignKey("CreatedById")]
        [InverseProperty("CreatedActions")]
        public virtual Users CreatedBy { get; set; }
        [JsonIgnore]
        public virtual ICollection<Users> RoutedUsers { get; set; }
        [JsonIgnore]
        public virtual ICollection<ActionDocuments> ActionDocuments { get; set; }   
    }
}
