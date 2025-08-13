using ICTProfilingV3.DataTransferModels.ViewModels;
using Models.Entities;
using System.Collections.Generic;

namespace ICTProfilingV3.DataTransferModels.ReportViewModel
{
    public class PGNReportViewModel
    {
        public string ReqNo { get; set; }
        public string Office { get; set; }
        public string PrintedBy { get; set; }
        public string DatePrinted { get; set; }
        public Users PreparedBy { get; set; }
        public Users NotedBy { get; set; }
        public IEnumerable<PGNAccountsViewModel> PGNAccounts { get; set; }
    }
}
