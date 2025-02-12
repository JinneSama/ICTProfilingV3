using System.ComponentModel;

namespace Models.Enums
{
    public enum SheetService
    {
        [Description("The person in charge was quick to respond when asked for assistance")]
        _Service1 = 0,
        [Description("Principles/processes pertinent to requested when asked for assistance")]
        _Service2 = 1,
        [Description("The scope for the requested assistance was discussed and agreed upon")]
        _Service3 = 2,
        [Description("The person in charge exhibits courtesy and professionalism")]
        _Service4 = 3,
        [Description("Concern on the subject of assistance addressed/provided")]
        _Service5 = 4,
    }
}
