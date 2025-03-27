using Models.Enums;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entities
{
    public class PGNAccounts
    {
        public PGNAccounts()
        {
            MacAddresses = new HashSet<PGNMacAddresses>();
        }
        public int Id { get; set; }
        public long? HRMISEmpId { get; set; }
        public string Username { get; set; }
        public PGNUserType? UserType { get; set; }
        public PGNStatus? Status { get; set; }
        public string IPAddress { get; set; }
        public int? SignInCount { get; set; }
        public PGNTrafficSpeed? TrafficSpeed { get; set; }
        public PGNDesignations? Designation { get; set; }
        public string Remarks { get; set; }
        public string Password { get; set; }
        public int? PGNGroupOfficesId { get; set; }
        [ForeignKey("PGNGroupOfficesId")]
        public PGNGroupOffices PGNGroupOffices { get; set; }
        public int? PGNNonEmployeeId { get; set; }
        [ForeignKey("PGNNonEmployeeId")]
        public PGNNonEmployee PGNNonEmployee { get; set; }
        public int? PGNRequestId { get; set; }
        [ForeignKey("PGNRequestId")]
        public PGNRequests PGNRequests { get; set; }
        [JsonIgnore]
        public virtual ICollection<PGNMacAddresses> MacAddresses { get; set; }
    }
}
