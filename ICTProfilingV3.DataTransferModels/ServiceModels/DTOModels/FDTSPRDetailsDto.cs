using System.Collections.Generic;
using System;

namespace ICTProfilingV3.DataTransferModels.ServiceModels.DTOModels
{
    public class FDTSPRDetailsDto
    {
        public DateTime? Date { get; set; }
        public string ControlNo { get; set; }
        public string PRDescription { get; set; }
        public decimal? TotalAmount { get; set; }
        public string Purpose { get; set; }
        public string BudgetPRNo { get; set; }
        public IEnumerable<PRDetailsDto> PRDetails { get; set; }
        public IEnumerable<DocumentActionsDto> DocumentActions { get; set; }
    }
}
