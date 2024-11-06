using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entities
{
    public class Model
    {
        public int Id { get; set; }
        public string ModelName { get; set; }
        public int BrandId { get; set; }

        [ForeignKey("BrandId")]
        public Brand Brand { get; set; }
        public virtual ICollection<DeliveriesSpecs> DeliveriesSpecs { get; set; }
        public Model()
        {
            DeliveriesSpecs = new HashSet<DeliveriesSpecs>();
        }
    }
}
