using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entities
{
    public class EquipmentSpecsDetails
    {
        public int Id { get; set; }
        public int ItemNo { get; set; }
        public string DetailSpecs { get; set; }
        public string DetailDescription { get; set; }
        public int EquipmentSpecsId { get; set; }

        [ForeignKey("EquipmentSpecsId")]
        public EquipmentSpecs EquipmentSpecs { get; set; }
    }
}
