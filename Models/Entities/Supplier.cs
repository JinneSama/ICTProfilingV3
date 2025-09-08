using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Management.Instrumentation;

namespace Models.Entities
{
    public class Supplier
    {
        public int Id { get; set; }
        [MaxLength(1024)]
        public string SupplierName { get; set; }
        [MaxLength(1024)]
        public string Address { get; set; }
        [MaxLength(1024)]
        public string ContactPerson { get; set; }
        [MaxLength(128)]
        public string TelNumber { get; set; }
        [MaxLength(128)]
        public string FaxNumber { get; set; }
        [MaxLength(128)]
        public string PhoneNumber { get; set; }
        public bool? Status { get; set; }
        public bool? IsDeleted { get; set; }
        public int OldPK { get; set; }
        [JsonIgnore]
        public virtual ICollection<Deliveries> Deliveries { get; set; }
        public Supplier()
        {
            Deliveries = new HashSet<Deliveries>();
        }
    }
}
