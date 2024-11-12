using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entities
{
    public class PRStandardPRSpecs
    {
        public int Id { get; set; }
        public int? ItemNo { get; set; }
        public int? Quantity { get; set; }
        public long? TotalCost { get; set; }
        public int PurchaseRequestId { get; set; }

        [ForeignKey("PurchaseRequestId")]
        public PurchaseRequest PurchaseRequest { get; set; }

        public int StandardPRSpecsId { get; set; }

        [ForeignKey("StandardPRSpecsId")]
        public StandardPRSpecs StandardPRSpecs { get; set; }
    }
}
