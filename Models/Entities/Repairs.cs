using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Enums;

namespace Models.Entities
{
    public class Repairs
    {
        [Key, ForeignKey("TicketRequest")]
        public int Id { get; set; }
        //----HRIS Data----
        public long? RequestedById { get; set; }
        public long? ReqByChiefId { get; set; }
        public long? DeliveredById { get; set; }
        //----HRIS Data----

        public DateTime? DateCreated { get; set; }
        public DateTime? DateDelivered { get; set; }
        public string Problems { get; set; }
        public string Findings { get; set; }
        public string Recommendations { get; set; }
        public string PreparedById { get; set; }
        public bool? IsDeleted { get; set; }

        [ForeignKey("PreparedById")]
        private Users PreparedByUsers { get; set; }
        public string ReviewedById { get; set; }

        [ForeignKey("ReviewedById")]
        private Users ReviewedByUsers { get; set; }
        public string NotedById { get; set; }

        [ForeignKey("NotedById")]
        private Users NotedByUsers { get; set; }
        public Gender Gender { get; set; }
        public string ContactNo { get; set; }
        public TicketRequest TicketRequest { get; set; }
        public int? PPEsId { get; set; }

        [ForeignKey("PPEsId")]
        public PPEs PPEs { get; set; }
        public int? PPESpecsId { get; set; }

        [ForeignKey("PPESpecsId")]
        public PPEsSpecs PPEsSpecs { get; set; }

        public int? TechSpecsId { get; set; }

        [ForeignKey("TechSpecsId")]
        public TechSpecs TechSpecs { get; set; }
        public virtual ICollection<Actions> Actions { get; set; }

        public Repairs()
        {
            Actions = new HashSet<Actions>();
        }
    }
}
