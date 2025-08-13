using Models.Entities;
using Models.Enums;
using Models.Models;
using System;

namespace ICTProfilingV3.DataTransferModels.ViewModels
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
        public int? RepairId { get; set; }
        public TechSpecs TechSpecs { get; set; }
        public string AssignedTo { get; set; }
        public string Equipment { get; set; }
    }
}
