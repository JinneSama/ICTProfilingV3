using Models.Enums;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entities
{
    public class ComparisonReportSpecs
    {
        public ComparisonReportSpecs()
        {
            ComparisonReportSpecsDetails = new HashSet<ComparisonReportSpecsDetails>();
        }
        public int Id { get; set; }
        public int? ItemNo { get; set; }
        public int? Quantity { get; set; }
        public Unit Unit { get; set; }
        public string Type { get; set; }
        public string PR { get; set; }
        public string Quotation { get; set; }
        public string PO { get; set; }
        public string ActualDelivery { get; set; }
        public string Remarks { get; set; }
        public bool? IsDiscrepancy { get; set; }
        public int? ComparisonReportId { get; set; }
        [ForeignKey("ComparisonReportId")]
        public ComparisonReport ComparisonReport { get; set; }
        [JsonIgnore]
        public ICollection<ComparisonReportSpecsDetails> ComparisonReportSpecsDetails { get; set; }
    }
}
