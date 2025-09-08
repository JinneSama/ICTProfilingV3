using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entities
{
    public sealed class ComparisonReportFiles
    {
        public int Id { get; set; }
        public int Version { get; set; }
        public string FileName { get; set; }
        public int MyProperty { get; set; }
        public int DeliveriesId { get; set; }
        [ForeignKey("DeliveriesId")]
        public Deliveries Deliveries { get; set; }
    }
}
