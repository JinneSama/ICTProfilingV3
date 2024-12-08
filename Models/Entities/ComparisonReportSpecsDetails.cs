using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entities
{
    public class ComparisonReportSpecsDetails
    {
        public int Id { get; set; }
        public int ItemOrder { get; set; }
        public string Type { get; set; }
        public string PR { get; set; }
        public string Quotation { get; set; }
        public string PO { get; set; }
        public string ActualDelivery { get; set; }
        public bool? IsDiscrepancy { get; set; }
        public int? ComparisonReportSpecsId { get; set; }
        [ForeignKey("ComparisonReportSpecsId")]
        public ComparisonReportSpecs ComparisonReportSpecs { get; set; }
    }
}
