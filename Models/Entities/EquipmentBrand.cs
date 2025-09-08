using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entities
{
    public sealed class EquipmentBrand
    {
        public EquipmentBrand()
        {
            EquipmentCategoryBrands = new HashSet<EquipmentCategoryBrand>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<EquipmentCategoryBrand> EquipmentCategoryBrands { get; set; }
    }
}
