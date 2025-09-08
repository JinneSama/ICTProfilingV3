using Models.Enums;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [MaxLength(1024)]
        public string Description { get; set; }
        [MaxLength(1024)]
        public string Purpose { get; set; }

        public int TechSpecsId { get; set; }

        [ForeignKey("TechSpecsId")]
        public TechSpecs TechSpecs { get; set; }
        public int? EquipmentSpecsId { get; set; }

        [ForeignKey("EquipmentSpecsId")]
        public EquipmentSpecs EquipmentSpecs { get; set; }

        public int TechSpecsICTSpecsDetailsId { get; set; }
        [JsonIgnore]
        public virtual ICollection<TechSpecsICTSpecsDetails> TechSpecsICTSpecsDetails { get; set; }
        public TechSpecsICTSpecs()
        {
            TechSpecsICTSpecsDetails = new HashSet<TechSpecsICTSpecsDetails>();
        }
    }
}
