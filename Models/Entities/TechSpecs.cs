using Models.Enums;
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
        public string ContactNo { get; set; }
        public bool? RequestBasedApprovedPR { get; set; }
        public bool? RequestBasedApprovedAPP { get; set; }
        public bool? RequestBasedApprovedAIP { get; set; }
        public bool? RequestBasedApprovedPPMP { get; set; }
        public bool? RequestBasedRequestLetter { get; set; }
        public bool? RequestBasedForReplacement{ get; set; }
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
        public virtual ICollection<TechSpecsICTSpecs> TechSpecsICTSpecs { get; set; }
        public virtual ICollection<Actions> Actions { get; set; }
        public TechSpecs()
        {
            TechSpecsICTSpecs = new HashSet<TechSpecsICTSpecs>();
            Actions = new HashSet<Actions>();
        }
    }
}
