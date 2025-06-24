using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entities
{
    public class ComparisonReportSpecsDetails
    {
        public int Id { get; set; }
        public int ItemOrder { get; set; }
        [MaxLength(2048)]
        public string Type { get; set; }
        [MaxLength(2048)]
        public string PR { get; set; }
        [MaxLength(2048)]
        public string Quotation { get; set; }
        [MaxLength(2048)]
        public string PO { get; set; }
        [MaxLength(2048)]
        public string ActualDelivery { get; set; }
        public bool? IsDiscrepancy { get; set; }
        public int? ComparisonReportSpecsId { get; set; }
        [ForeignKey("ComparisonReportSpecsId")]
        public ComparisonReportSpecs ComparisonReportSpecs { get; set; }
    }
}
