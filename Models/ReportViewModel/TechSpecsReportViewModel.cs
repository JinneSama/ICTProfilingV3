using Models.Entities;
using System;

namespace Models.ReportViewModel
{
    public class TechSpecsReportViewModel
    {
        public string PrintedBy { get; set; }
        public DateTime DatePrinted { get; set; }
        public string Office { get; set; }
        public string Chief { get; set; }
        public string ChiefPosition { get; set; }
        public string Staff { get; set; }
        public string StaffPosition { get; set; }
        public int RepairId { get; set; }
        public TechSpecs TechSpecs { get; set; }
        public Users PreparedBy { get; set; }    
        public Users ReviewedBy { get; set; }
        public Users NotedBy { get; set; }
    }
}
