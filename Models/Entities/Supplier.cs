using Newtonsoft.Json;
using System.Collections.Generic;
using System.Management.Instrumentation;

namespace Models.Entities
{
    public class Supplier
    {
        public int Id { get; set; }
        public string SupplierName { get; set; }
        public string Address { get; set; }
        public string ContactPerson { get; set; }
        public string TelNumber { get; set; }
        public string FaxNumber { get; set; }
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
