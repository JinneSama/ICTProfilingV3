using Models.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace Models.Entities
{
    public class RecordsRequestStatus
    {
        public int Id { get; set; }
        public TicketStatus Status { get; set; }
        public DateTime? DateStatusChanged { get; set; }
        public string ChangedByUserId { get; set; }
        [ForeignKey("ChangedByUserId")]
        public Users ChangedByUser { get; set; }

        public int? PRId { get; set; }
        [ForeignKey("PRId")]
        public PurchaseRequest PurchaseRequest { get; set; }
        public int? CASId { get; set; }
        [ForeignKey("CASId")]
        public CustomerActionSheet CustomerActionSheet { get; set; }
        public int? MOId { get; set; }
        [ForeignKey("MOId")]
        public MOAccountUsers MOAccountUsers { get; set; }
        public int? PGNId { get; set; }
        [ForeignKey("PGNId")]
        public PGNRequests PGNRequests { get; set; }
    }
}
