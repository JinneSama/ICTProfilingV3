using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models.Entities
{
    public class PGNNonEmployee
    {
        public PGNNonEmployee()
        {
            PGNAccounts = new HashSet<PGNAccounts>();
        }
        public int Id { get; set; }
        [MaxLength(128)]
        public string FullName { get; set; }
        [MaxLength(128)]
        public string Position { get; set; }
        [MaxLength(128)]
        public string Username { get; set; }
        [JsonIgnore]
        public virtual ICollection<PGNAccounts> PGNAccounts { get; set; }
    }
}
