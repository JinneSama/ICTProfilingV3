using Models.Enums;

namespace ICTProfilingV3.DataTransferModels.ViewModels
{
    public class ClientRequestViewModel
    {
        public string DateCreated { get; set; }
        public string ControlNo { get; set; }
        public RequestType RequestType { get; set; }
        public string Description { get; set; }
        public string AssignedTo { get; set; }
        public string Remarks { get; set; }
        public TicketStatus Status { get; set; }
    }
}
