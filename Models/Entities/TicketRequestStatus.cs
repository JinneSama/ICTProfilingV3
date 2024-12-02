using Models.Enums;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entities
{
    public class TicketRequestStatus
    {
        public int Id { get; set; }
        public TicketStatus Status { get; set; }
        public DateTime? DateStatusChanged { get; set; }
        public string ChangedByUserId { get; set; }
        [ForeignKey("ChangedByUserId")]
        public Users ChangedByUser { get; set; }

        public int TicketRequestId { get; set; }
        [ForeignKey("TicketRequestId")]
        public TicketRequest TicketRequest { get; set; }    
    }
}
