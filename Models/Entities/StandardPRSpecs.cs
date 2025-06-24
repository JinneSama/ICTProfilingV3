using Models.Enums;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entities
{
    public class StandardPRSpecs
    {
        public int Id { get; set; }
        public int ItemNo { get; set; }
        public PRQuarter Quarter { get; set; }
        [MaxLength(1024)]
        public string Description { get; set; }
        [MaxLength(1024)]
        public string Purpose { get; set; }
        public Unit Unit { get; set; }
        public long? UnitCost { get; set; }
        public int? EquipmentSpecsId { get; set; }

        [ForeignKey("EquipmentSpecsId")]
        public EquipmentSpecs EquipmentSpecs { get; set; }
        [JsonIgnore]
        public virtual ICollection<PRStandardPRSpecs> PRStandardPRSpecs { get; set; }
        [JsonIgnore]
        public virtual ICollection<StandardPRSpecsDetails> StandardPRSpecsDetails { get; set; }
        public StandardPRSpecs()
        {
            StandardPRSpecsDetails = new HashSet<StandardPRSpecsDetails>();
            PRStandardPRSpecs = new HashSet<PRStandardPRSpecs>();
        }
    }
}
