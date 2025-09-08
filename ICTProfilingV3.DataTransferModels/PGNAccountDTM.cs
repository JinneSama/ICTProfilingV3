using Models.Entities;
using System.Collections.Generic;

namespace ICTProfilingV3.DataTransferModels
{
    public class PGNReportDTM
    {
        public IEnumerable<PGNAccountDTM> PGNAccounts { get; set; }
    }
    public class PGNAccountDTM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public string Username { get; set; }
        public string UserType { get; set; }
        public string OfficeAcr { get; set; }
        public string Status { get; set; }
        public int SignInCount { get; set; }
        public string TrafficSpeed { get; set; }
        public string Designation { get; set; }
        public string Password { get; set; }
        public string Remarks { get; set; }
        public IEnumerable<PGNMacAddresses> MacAddresses { get; set; }
    }
}
