using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entities
{
    public class TechSpecsBasisDetails
    {
        public long Id { get; set; }
        public int ItemNo { get; set; }
        public string Specs { get; set; }
        public string Description { get; set; }
        public int TechSpecsBasisId { get; set; }

        [ForeignKey("TechSpecsBasisId")]
        public TechSpecsBasis DeliveriesSpecs { get; set; }
    }
}
