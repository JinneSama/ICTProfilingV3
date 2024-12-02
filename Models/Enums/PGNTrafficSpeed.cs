using System.ComponentModel;

namespace Models.Enums
{
    public enum PGNTrafficSpeed
    {
        [Description("1Mbps")]
        _1Mbps = 0,
        [Description("3Mbps")]
        _3Mbps = 1,
        [Description("5Mbps")]
        _5Mbps = 2,
        [Description("8Mbps")]
        _8Mbps = 3,
        [Description("10Mbps")]
        _10Mbps = 4,
        [Description("High Guarantee User")]
        HighGuaranteeUser = 5
    }
}
