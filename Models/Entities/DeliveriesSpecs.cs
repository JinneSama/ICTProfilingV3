using Models.Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entities
{
    public class DeliveriesSpecs
    {
        public int Id { get; set; }
        public int? ItemNo { get; set; }
        public string Description { get; set; }
        public string Remarks { get; set; }
        public int? Quantity { get; set; }
        public Unit Unit { get; set; }
        public decimal? UnitCost { get; set; }
        public decimal? TotalCost { get; set; }
        public decimal? ProposedBudget { get; set; }
        public string Purpose { get; set; }
        public bool? IsActive { get; set; }
        public string SerialNo { get; set; }
        public int? ModelId { get; set; }

        [ForeignKey("ModelId")]
        public Model Model { get; set; }
        public int? DeliveriesId { get; set; }

        [ForeignKey("DeliveriesId")]
        public Deliveries Deliveries { get; set; }
        public virtual ICollection<DeliveriesSpecsDetails> DeliveriesSpecsDetails { get; set; }

        public DeliveriesSpecs()
        {
            DeliveriesSpecsDetails = new HashSet<DeliveriesSpecsDetails>();
        }

    }
}
