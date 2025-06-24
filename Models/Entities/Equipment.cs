using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Models.Entities
{
    public class Equipment
    {
        public Equipment()
        {
            EquipmentSpecs = new HashSet<EquipmentSpecs>();
        }
        public int Id { get; set; }
        [MaxLength(512)]
        public string EquipmentName { get; set; }
        public int OldPK { get; set; }
        [JsonIgnore]
        public virtual ICollection<EquipmentSpecs> EquipmentSpecs { get; set; }
    }
}
