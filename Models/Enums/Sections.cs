using System.ComponentModel;

namespace Models.Enums
{
    public enum Sections
    {
        [Description("Repair Section")]
        Repair = 0,
        [Description("Network Section")]
        Network = 1,
        [Description("Information Systems Section")]
        IS = 2,
        [Description("Records Section")]
        Records = 3
    }
}
