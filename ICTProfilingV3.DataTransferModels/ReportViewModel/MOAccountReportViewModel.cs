using ICTProfilingV3.DataTransferModels.ViewModels;
using System;
using System.Collections.Generic;

namespace ICTProfilingV3.DataTransferModels.ReportViewModel
{
    public class MOAccountReportViewModel
    {
        public DateTime? DatePrinted { get; set; }
        public string PrintedBy { get; set; }
        public IEnumerable<MOAccountsViewModel> MOAccountsViewModel { get; set; }
    }
}
