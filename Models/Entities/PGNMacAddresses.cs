using Models.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entities
{
    public class PGNMacAddresses
    {
        public int Id { get; set; }
        public PGNDeviceConnection Connection { get; set; }
        public PGNDevices Device { get; set; }
        public string MacAddress { get; set; }
        public int PGNAccountId { get; set; }
        [ForeignKey("PGNAccountId")]
        public PGNAccounts PGNAccounts { get; set; }
    }
}
