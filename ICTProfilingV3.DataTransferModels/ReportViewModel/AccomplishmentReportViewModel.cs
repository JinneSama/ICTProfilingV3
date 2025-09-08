using Models.Entities;
using System;
using System.Collections;
using System.Collections.Generic;

namespace ICTProfilingV3.DataTransferModels.ReportViewModel
{
    public class AccomplishmentReportViewModel
    {
        public string PrintedBy { get; set; }
        public Users Staff { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Users PreparedBy { get; set; }
        public Users ReviewedBy { get; set; }
        public Users ApprovedBy { get; set; }
        public string AccomplishmentPeriod { get; set; }
        public string AOPosition { get; set; }
        public string ApprovedPosition { get; set; }
        public IEnumerable<ActionReport> ActionReport { get; set; }
    }

    public class ActionReport
    {
        public Action Action { get; set; }
        public int SubActivityId { get; set; }
        public string SubActivity { get; set; } 
        public string MainActivity { get; set; }
        public int Count { get; set; }
    }
}
