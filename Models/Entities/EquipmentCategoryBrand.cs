using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entities
{
    public class EquipmentCategoryBrand
    {
        public EquipmentCategoryBrand()
        {
            Models = new HashSet<Model>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int EquipmentBrandId { get; set; }
        public int EquipmentCategoryId { get; set; }
        [ForeignKey("EquipmentBrandId")]
        public EquipmentBrand EquipmentBrand { get; set; }
        [ForeignKey("EquipmentCategoryId")]
        public EquipmentCategory EquipmentCategory { get; set; }
        public ICollection<Model> Models { get; set; }
    }
}
