using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entities
{
    public class Model
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(1024)]
        public string ModelName { get; set; }
        public int? BrandId { get; set; }
        public int OldPK { get; set; }

        public int? EquipmentCategoryBrandId { get; set; }
        [ForeignKey("EquipmentCategoryBrandId")]
        public EquipmentCategoryBrand EquipmentCategoryBrand { get; set; }

        public string Description { get; set; }
        [ForeignKey("BrandId")]
        public Brand Brand { get; set; }
        [JsonIgnore]
        public virtual ICollection<DeliveriesSpecs> DeliveriesSpecs { get; set; }
        public Model()
        {
            DeliveriesSpecs = new HashSet<DeliveriesSpecs>();
        }
    }
}
