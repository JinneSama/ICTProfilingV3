using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICTProfilingV3.DataTransferModels.ViewModels
{
    public class ActionsViewModel
    {
        public int Id { get; set; }
        public Actions Actions { get; set; }
        public DateTime? ActionDate { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedById { get; set; }
        public string SubActivity { get; set; }
        public string ActionTaken { get; set; }
        public string RoutedTo { get; set; }
        public string Remarks { get; set; }
        public bool? IsSend { get; set; }
        public bool? hasDocuments { get; set; }
    }
}
