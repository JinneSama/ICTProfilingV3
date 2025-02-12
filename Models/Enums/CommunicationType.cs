using System.ComponentModel;

namespace Models.Enums
{
    public enum CommunicationType
    {
        [Description("PGN Account")]
        PGN = 0,
        [Description("Request for TA")]
        TA = 1
    }
}
