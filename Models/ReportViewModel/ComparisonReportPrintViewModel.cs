using Models.Entities;
using System.Collections.Generic;
using System;

namespace Models.ReportViewModel
{
    public class ComparisonReportPrintViewModel
    {
        public DateTime? DateOfDelivery { get; set; }
        public string RequestingOffice { get; set; }
        public string Supplier { get; set; }
        public double Amount { get; set; }
        public string EpisNo { get; set; }
        public DateTime? TechInspectedDate { get; set; }
        public Users PreparedBy { get; set; }
        public Users ReviewedBy { get; set; }
        public Users NotedBy { get; set; }
        public IEnumerable<ComparisonReportSpecs> ComparisonReportSpecs { get; set; }
        public string PrintedBy { get; set; }
        public DateTime? DatePrinted { get; set; }
    }
}
