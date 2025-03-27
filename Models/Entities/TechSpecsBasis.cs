using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entities
{
    public class TechSpecsBasis
    {
        public TechSpecsBasis()
        {
            TechSpecsBasisDetails = new HashSet<TechSpecsBasisDetails>();
        }
        public int Id { get; set; }
        public double PriceRange { get; set; }
        public DateTime? PriceDate { get; set; }
        public string URLBasis { get; set; }
        public string Remarks { get; set; }
        public bool? Available { get; set; }
        public int EquipmentSpecsId { get; set; }

        [ForeignKey("EquipmentSpecsId")]
        public EquipmentSpecs EquipmentSpecs { get; set; }
        [JsonIgnore]
        public virtual ICollection<TechSpecsBasisDetails> TechSpecsBasisDetails { get; set; }
    }
}
