using Models.Enums;
using Models.Models;
using System;

namespace Models.ViewModels
{
    public class TechSpecsViewModel
    {
        public int Id { get; set; }
        public TicketStatus Status { get; set; }
        public DateTime? DateRequested { get; set; }
        public DateTime? DateAccepted { get; set; }
        public string Office { get; set; }
        public string Division { get; set; }
        public int TicketId { get; set; }
    }
}
