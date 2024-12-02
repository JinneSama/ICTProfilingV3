using Models.Entities;
using Models.ViewModels;
using System;
using System.Linq;

namespace Models.ReportViewModel
{
    public class RepairTRViewModel
    {
        public string PrintedBy { get; set; }
        public DateTime DatePrinted { get; set; }
        public EmployeesViewModel RequestBy { get; set; }
        public EmployeesViewModel DeliveredBy { get; set; }
        public EmployeesViewModel IssuedTo { get; set; }    
        public Repairs Repair { get; set; }
        public Users ReceivedBy { get; set; }
        public Users AssesedBy { get; set; }
        public Users NotedBy { get; set; }
        public string Actions => string.Join(Environment.NewLine, Repair.Actions.Select(x => x.Remarks));
    }
}
