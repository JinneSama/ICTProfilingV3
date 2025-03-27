using Newtonsoft.Json;
using System.Collections.Generic;

namespace Models.Entities
{
    public class PGNGroupOffices
    {
        public PGNGroupOffices()
        {
            PGNAccounts = new HashSet<PGNAccounts>();
            MOAccounts = new HashSet<MOAccounts>(); 
        }
        public int Id { get; set; }
        public string OfficeAcr { get; set; }
        public string Office { get; set; }
        [JsonIgnore]
        public virtual ICollection<PGNAccounts> PGNAccounts { get; set; }
        [JsonIgnore]
        public virtual ICollection<MOAccounts> MOAccounts { get; set; }
    }
}
