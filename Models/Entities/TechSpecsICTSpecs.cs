using Models.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace Models.Entities
{
    public class TechSpecsICTSpecs
    {
        public int Id { get; set; }
        public int ItemNo { get; set; }
        public int? Quantity { get; set; }
        public Unit? Unit { get; set; }
        public decimal? UnitCost { get; set; }
        public decimal? TotalCost { get; set; }
        public string Description { get; set; }
        public string Purpose { get; set; }

        public int TechSpecsId { get; set; }

        [ForeignKey("TechSpecsId")]
        public TechSpecs TechSpecs { get; set; }
        public int? EquipmentSpecsId { get; set; }

        [ForeignKey("EquipmentSpecsId")]
        public EquipmentSpecs EquipmentSpecs { get; set; }

        public int TechSpecsICTSpecsDetailsId { get; set; }
        public virtual ICollection<TechSpecsICTSpecsDetails> TechSpecsICTSpecsDetails { get; set; }
        public TechSpecsICTSpecs()
        {
            TechSpecsICTSpecsDetails = new HashSet<TechSpecsICTSpecsDetails>();
        }
    }
}
