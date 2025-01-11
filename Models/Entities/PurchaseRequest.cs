using Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entities
{
    public class PurchaseRequest
    {
        public int Id { get; set; }
        public DateTime? DateCreated { get; set; }
        public long? ReqById { get; set; }
        public long? ChiefId { get; set; }  
        public string PRNo { get; set; }
        public PRQuarter? Quarter { get; set; }
        public bool? IsDeleted { get; set; }

        public int? TechSpecsId { get; set; }

        [ForeignKey("TechSpecsId")]
        public TechSpecs TechSpecs { get; set; }
        public string CreatedById { get; set; }

        [ForeignKey("CreatedById")]
        public Users CreatedByUser { get; set; }

        public virtual ICollection<PRStandardPRSpecs> PRStandardPRSpecs { get; set; }
        public virtual ICollection<Actions> Actions { get; set; }
        public PurchaseRequest()
        {
            PRStandardPRSpecs = new HashSet<PRStandardPRSpecs>();
            Actions = new HashSet<Actions>();   
        }
    }
}
