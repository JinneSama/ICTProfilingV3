using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entities
{
    public class EquipmentSpecs
    {
        public EquipmentSpecs()
        {
            Brands = new HashSet<Brand>();
            EquipmentSpecsDetails = new HashSet<EquipmentSpecsDetails>();
            TechSpecsBasis = new HashSet<TechSpecsBasis>();
            TechSpecsICTSpecs = new HashSet<TechSpecsICTSpecs>();
        }
        public int Id { get; set; }
        public string Description { get; set; }
        public string Remarks { get; set; }
        public int EquipmentId { get; set; }

        [ForeignKey("EquipmentId")]
        public Equipment Equipment { get; set; }
        public virtual ICollection<Brand> Brands { get; set; }
        public virtual ICollection<EquipmentSpecsDetails> EquipmentSpecsDetails { get; set; }
        public virtual ICollection<TechSpecsBasis> TechSpecsBasis { get; set; }
        public virtual ICollection<TechSpecsICTSpecs> TechSpecsICTSpecs { get; set; }
    }
}
