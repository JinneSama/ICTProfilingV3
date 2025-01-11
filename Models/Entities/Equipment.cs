using System.Collections.Generic;

namespace Models.Entities
{
    public class Equipment
    {
        public Equipment()
        {
            EquipmentSpecs = new HashSet<EquipmentSpecs>();
        }
        public int Id { get; set; }
        public string EquipmentName { get; set; }
        public int OldPK { get; set; }
        public virtual ICollection<EquipmentSpecs> EquipmentSpecs { get; set; }
    }
}
