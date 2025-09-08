using Models.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entities
{
    public class TechSpecs
    {
        [Key, ForeignKey("TicketRequest")]
        public int Id { get; set; }
        public DateTime? DateRequested { get; set; }
        public DateTime? DateAccepted { get; set; }

        //----HRIS Data----
        public long ReqById { get; set; }
        public long ReqByChiefId { get; set; }
        //----HRIS Data----
        public Gender ReqByGender { get; set; }
        [MaxLength(128)]
        public string ContactNo { get; set; }
        public bool? RequestBasedApprovedPR { get; set; }
        public bool? RequestBasedApprovedAPP { get; set; }
        public bool? RequestBasedApprovedAIP { get; set; }
        public bool? RequestBasedApprovedPPMP { get; set; }
        public bool? RequestBasedRequestLetter { get; set; }
        public bool? RequestBasedForReplacement{ get; set; }
        public bool? IsDeleted { get; set; }
        public TicketRequest TicketRequest { get; set; }
        public string PreparedById { get; set; }

        [ForeignKey("PreparedById")]
        private Users PreparedByUsers { get; set; }
        public string ReviewedById { get; set; }

        [ForeignKey("ReviewedById")]
        private Users ReviewedByUsers { get; set; }
        public string NotedById { get; set; }

        [ForeignKey("NotedById")]
        private Users NotedByUsers { get; set; }
        [JsonIgnore]
        public virtual ICollection<TechSpecsICTSpecs> TechSpecsICTSpecs { get; set; }
        public virtual ICollection<Actions> Actions { get; set; }
        [JsonIgnore]
        public virtual ICollection<Repairs> Repairs { get; set; }
        [JsonIgnore]
        public virtual ICollection<EvaluationSheet> EvaluationSheets { get; set; }
        public TechSpecs()
        {
            TechSpecsICTSpecs = new HashSet<TechSpecsICTSpecs>();
            Actions = new HashSet<Actions>();
            Repairs = new HashSet<Repairs>();
            EvaluationSheets = new HashSet<EvaluationSheet>();
        }
    }
}
