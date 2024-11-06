using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entities
{
    public class DeliveriesSpecsDetails
    {
        public long Id { get; set; }
        public int DeliveriesSpecsId { get; set; }
        public int ItemNo { get; set; }
        public string Specs { get; set; }
        public string Description { get; set; }

        [ForeignKey("DeliveriesSpecsId")]
        public DeliveriesSpecs DeliveriesSpecs { get; set; }    
    }
}
