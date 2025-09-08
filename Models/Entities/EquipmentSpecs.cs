using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
            StandardPRSpecs = new HashSet<StandardPRSpecs>();
        }
        public int Id { get; set; }
        [MaxLength(1024)]
        public string Description { get; set; }
        [MaxLength(1024)]
        public string Remarks { get; set; }
        public int EquipmentId { get; set; }
        public int OldPK { get; set; }

        [ForeignKey("EquipmentId")]
        public Equipment Equipment { get; set; }
        [JsonIgnore]
        public virtual ICollection<Brand> Brands { get; set; }
        [JsonIgnore]
        public virtual ICollection<EquipmentSpecsDetails> EquipmentSpecsDetails { get; set; }
        [JsonIgnore]
        public virtual ICollection<TechSpecsBasis> TechSpecsBasis { get; set; }
        [JsonIgnore]
        public virtual ICollection<TechSpecsICTSpecs> TechSpecsICTSpecs { get; set; }
        [JsonIgnore]
        public virtual ICollection<StandardPRSpecs> StandardPRSpecs { get; set; }   
    }
}
