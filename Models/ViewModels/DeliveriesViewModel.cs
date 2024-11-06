using Models.Enums;

namespace Models.ViewModels
{
    public class DeliveriesViewModel
    {
        public int Id { get; set; }
        public TicketStatus Status { get; set; }
        public int TicketNo { get; set; }
        public string Office { get; set; }
        public string PONo { get; set; }
        public string Supplier { get; set; }
        public string DeliveryId { get; set; }
    }
}
