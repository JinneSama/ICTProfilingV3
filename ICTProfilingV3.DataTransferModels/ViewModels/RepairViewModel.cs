using Models.Entities;
using Models.Enums;
using System;

namespace ICTProfilingV3.DataTransferModels.ViewModels
{
    public class RepairViewModel
    {
        public int Id { get; set; }
        public TicketStatus Status { get; set; }
        public DateTime? DateCreated { get; set; }
        public string PropertyNo { get; set; }
        public string IssuedTo { get; set; }
        public string Office { get; set; }
        public string RepairId { get; set; }
        public int? AssignedTo { get; set; }
        public string Equipment { get; set; }
        public Repairs Repair { get; set; }
    }
}
