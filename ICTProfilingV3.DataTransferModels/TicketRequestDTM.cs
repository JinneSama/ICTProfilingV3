using Models.Enums;
using System;

namespace ICTProfilingV3.DataTransferModels
{
    public class TicketRequestDTM
    {
        public int Id { get; set; }
        public DateTime? DateRequested { get; set; }
        public string Status { get; set; }
        public TicketStatus? EnumStatus { get; set; }
        public string AssignedTo { get; set; }
        public RequestType TypeOfRequest { get; set; }
        public string Equipments { get; set; }
        public string Office { get; set; }
        public string RequestedBy { get; set; }
        public string DeliveredBy { get; set; }
        public string Supplier { get; set; }
    }
}
