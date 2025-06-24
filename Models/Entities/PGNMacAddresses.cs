using Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entities
{
    public class PGNMacAddresses
    {
        public int Id { get; set; }
        public PGNDeviceConnection Connection { get; set; }
        public PGNDevices Device { get; set; }
        [MaxLength(128)]
        public string MacAddress { get; set; }
        public int PGNAccountId { get; set; }
        [ForeignKey("PGNAccountId")]
        public PGNAccounts PGNAccounts { get; set; }
    }
}
