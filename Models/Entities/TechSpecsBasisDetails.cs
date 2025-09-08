using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entities
{
    public class TechSpecsBasisDetails
    {
        public long Id { get; set; }
        public int ItemNo { get; set; }
        [MaxLength(1024)]
        public string Specs { get; set; }
        [MaxLength(1024)]
        public string Description { get; set; }
        public int TechSpecsBasisId { get; set; }

        [ForeignKey("TechSpecsBasisId")]
        public TechSpecsBasis DeliveriesSpecs { get; set; }
    }
}
