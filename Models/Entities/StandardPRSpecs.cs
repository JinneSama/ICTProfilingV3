using Models.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entities
{
    public class StandardPRSpecs
    {
        public int Id { get; set; }
        public int ItemNo { get; set; }
        public PRQuarter Quarter { get; set; }
        public string Description { get; set; }
        public string Purpose { get; set; }
        public Unit Unit { get; set; }
        public long? UnitCost { get; set; }
        public int EquipmentSpecsId { get; set; }

        [ForeignKey("EquipmentSpecsId")]
        public EquipmentSpecs EquipmentSpecs { get; set; }
        public virtual ICollection<PRStandardPRSpecs> PRStandardPRSpecs { get; set; }
        public virtual ICollection<StandardPRSpecsDetails> StandardPRSpecsDetails { get; set; }
        public StandardPRSpecs()
        {
            StandardPRSpecsDetails = new HashSet<StandardPRSpecsDetails>();
            PRStandardPRSpecs = new HashSet<PRStandardPRSpecs>();
        }
    }
}
