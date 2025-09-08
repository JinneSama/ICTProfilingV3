using System;

namespace ICTProfilingV3.DataTransferModels.ServiceModels.DTOModels
{
    public class DocumentActionsDto
    {
        public DateTime? ActionDate { get; set; }
        public string ActionBy { get; set; }
        public string From { get; set; }
        public string ActionTaken { get; set; }
        public string DocType { get; set; }
        public string To { get; set; }
        public string Remarks { get; set; }
        public string Note { get; set; }
        public string Aging { get; set; }
    }
}
