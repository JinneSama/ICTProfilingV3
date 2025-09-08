using ICTProfilingV3.DataTransferModels.ViewModels;
using Models.Entities;
using System;
using System.Linq;

namespace ICTProfilingV3.DataTransferModels.ReportViewModel
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
        public string Actions => string.Join(Environment.NewLine, Repair.Actions.Select(x => x.ActionTaken + "-" + x.Remarks));
    }
}
