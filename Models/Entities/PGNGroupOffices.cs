using System.Collections.Generic;

namespace Models.Entities
{
    public class PGNGroupOffices
    {
        public PGNGroupOffices()
        {
            PGNAccounts = new HashSet<PGNAccounts>();
        }
        public int Id { get; set; }
        public string OfficeAcr { get; set; }
        public string Office { get; set; }
        public virtual ICollection<PGNAccounts> PGNAccounts { get; set; }
    }
}
