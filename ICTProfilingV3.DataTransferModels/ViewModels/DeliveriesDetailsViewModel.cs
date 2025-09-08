using System;

namespace ICTProfilingV3.DataTransferModels.ViewModels
{
    public class DeliveriesDetailsViewModel
    {
        public string Chief { get; set; }
        public string EpisNo { get; set; }
        public string Office { get; set; }
        public string ReqBy { get; set; }
        public string Tel { get; set; }
        public string DeliveredBy { get; set; }
        public string SupplierName { get; set; }
        public string SupplierAddress { get; set; }
        public string SupplierTelNo { get; set; }
        public DateTime? DeliveredDate { get; set; }
    }
}
