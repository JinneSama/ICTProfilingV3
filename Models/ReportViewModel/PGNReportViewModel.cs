using Models.Entities;
using Models.ViewModels;
using System.Collections.Generic;

namespace Models.ReportViewModel
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
