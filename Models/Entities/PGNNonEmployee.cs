using System.Collections.Generic;

namespace Models.Entities
{
    public class PGNNonEmployee
    {
        public PGNNonEmployee()
        {
            PGNAccounts = new HashSet<PGNAccounts>();
        }
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Position { get; set; }
        public string Username { get; set; }
        public virtual ICollection<PGNAccounts> PGNAccounts { get; set; }
    }
}
