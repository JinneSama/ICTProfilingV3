using Models.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Models.ReportViewModel
{
    public class MOAccountReportViewModel
    {
        public DateTime? DatePrinted { get; set; }
        public string PrintedBy { get; set; }
        public IEnumerable<MOAccountsViewModel> MOAccountsViewModel { get; set; }
    }
}
