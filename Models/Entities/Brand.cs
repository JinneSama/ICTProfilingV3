using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entities
{
    public class Brand
    {
        public Brand()
        {
            Models = new HashSet<Model>();
        }
        public int Id { get; set; }
        public string BrandName { get; set; }
        public int EquipmenSpecsId { get; set; }
        public int OldPK { get; set; }

        [ForeignKey("EquipmenSpecsId")]
        public EquipmentSpecs EquipmentSpecs { get; set; }
        [JsonIgnore]
        public virtual ICollection<Model> Models { get; set; }
    }
}
