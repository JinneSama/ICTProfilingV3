using System.ComponentModel;

namespace Models.Enums
{
    public enum TicketStatus
    {
        [Description("Accepted")]
        Accepted = 0,
        [Description("Assigned")]
        Assigned = 1,
        [Description("On Process")]
        OnProcess = 2,
        [Description("For Release")]
        ForRelease = 3,
        [Description("Completed")]
        Completed = 4
    }
}
