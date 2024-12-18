using System.ComponentModel;

namespace Models.Enums
{
    public enum RequestType
    {
        [Description("Technical Specification")]
        TechSpecs = 0,
        [Description("Deliveries")]
        Deliveries = 1,
        [Description("Repair")]
        Repairs = 2,
        PR = 3,
        CAS = 4,
        PGN = 5,
        M365 = 6
    }
}
