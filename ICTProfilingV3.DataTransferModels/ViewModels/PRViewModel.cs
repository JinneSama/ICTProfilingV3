using Models.Entities;

namespace ICTProfilingV3.DataTransferModels.ViewModels
{
    public class PRViewModel
    {
        public PurchaseRequest PurchaseRequest { get; set; }
        public string Office { get; set; }
        public string CreatedBy { get; set; }
    }
}
