using Models.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entities
{
    public class Deliveries
    {
        [Key, ForeignKey("TicketRequest")]
        public int Id { get; set; }
        public DateTime? DeliveredDate { get; set; }
        public DateTime? DateRequested { get; set; }

        //----HRIS Data----
        public long RequestedById { get; set; }
        public long ReqByChiefId { get; set; }
        public long DeliveredById { get; set; }
        //----HRIS Data----

        public Gender Gender { get; set; }
        public string ContactNo { get; set; }
        public string PONo { get; set; }
        public string ReceiptNo { get; set; }
        public int? SupplierId { get; set; }
        public bool? IsDeleted { get; set; }
        public long? FindingsRequestedById { get; set; }
        public DateTime? POServed { get; set; }
        public string FindingsStatus { get; set; }
        public string FindingsActionTaken { get; set; }
        public int OldPK { get; set; }

        [ForeignKey("SupplierId")]
        public Supplier Supplier { get; set; }

        public TicketRequest TicketRequest { get; set; }
        [JsonIgnore]
        public virtual ICollection<DeliveriesSpecs> DeliveriesSpecs { get; set; }
        public virtual ICollection<Actions> Actions { get; set; }
        [JsonIgnore]
        public virtual ICollection<ComparisonReport> ComparisonReports { get; set; }
        [JsonIgnore]
        public virtual ICollection<EvaluationSheet> EvaluationSheets { get; set; }
        public Deliveries()
        {
            DeliveriesSpecs = new HashSet<DeliveriesSpecs>();
            Actions = new HashSet<Actions>();
            ComparisonReports = new HashSet<ComparisonReport>();
            EvaluationSheets = new HashSet<EvaluationSheet>();
        }
    }
}
