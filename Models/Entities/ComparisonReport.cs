using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entities
{
    public class ComparisonReport
    {
        public ComparisonReport()
        {
            ComparisonReportSpecs = new HashSet<ComparisonReportSpecs>();
        }
        public int Id { get; set; }
        public int? DeliveryId { get; set; }
        [ForeignKey("DeliveryId")]
        public Deliveries Deliveries { get; set; }
        public string PreparedById { get; set; }
        [ForeignKey("PreparedById")]
        public Users PreparedByUser { get; set; }
        public string ReviewedById { get; set; }
        [ForeignKey("ReviewedById")]
        public Users ReviewedByUser { get; set; }
        public string NotedById { get; set; }
        [ForeignKey("NotedById")]
        public Users NotedByUser { get; set; }
        public ICollection<ComparisonReportSpecs> ComparisonReportSpecs { get; set; }
    }
}
