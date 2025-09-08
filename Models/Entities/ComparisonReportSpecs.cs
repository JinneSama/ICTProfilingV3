using Models.Enums;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [MaxLength(2048)]
        public string Remarks { get; set; }
        public bool? IsDiscrepancy { get; set; }
        public int? ComparisonReportId { get; set; }
        [ForeignKey("ComparisonReportId")]
        public ComparisonReport ComparisonReport { get; set; }
        [JsonIgnore]
        public ICollection<ComparisonReportSpecsDetails> ComparisonReportSpecsDetails { get; set; }
    }
}
