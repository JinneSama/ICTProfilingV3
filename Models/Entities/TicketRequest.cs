using Models.Enums;
using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entities
{
    public class TicketRequest
    {
        [Key]
        public int Id { get; set; }
        public DateTime? DateCreated { get; set; }
        public string CreatedBy { get; set; }
        public bool? IsDeleted { get; set; }

        [ForeignKey("CreatedBy")]
        public Users CreatedByUser { get; set; }
        public TicketStatus TicketStatus { get; set; }
        public RequestType RequestType { get; set; }
        public int? StaffId { get; set; }

        [ForeignKey("StaffId")]
        public ITStaff ITStaff { get; set; }
        public Deliveries Deliveries { get; set; }
        public TechSpecs TechSpecs { get; set; }
        public Repairs Repairs { get; set; }
    }
}
