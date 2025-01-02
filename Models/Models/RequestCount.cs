using Models.Enums;

namespace Models.Models
{
    public class RequestCount
    {
        public TicketStatus TicketStatus { get; set; }
        public RequestType RequestType { get; set; }
        public string Office { get; set; }
        public int? Request { get; set; }
        public int? Quantity { get; set; }
        public int? Item { get; set; }
        public int? Female { get; set; }
        public int? Male { get; set; }
    }
}
