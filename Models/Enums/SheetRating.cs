using System.ComponentModel;

namespace Models.Enums
{
    public enum SheetRating
    {
        [Description("O")]
        _5 = 0,
        [Description("VS")]
        _4 = 1,
        [Description("V")]
        _3 = 2,
        [Description("US")]
        _2 = 3,
        [Description("P")]
        _1 = 4  
    }
}
