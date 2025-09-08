using System.Collections.Generic;

namespace ICTProfilingV3.DataTransferModels.Models
{
    public class QuarterlyReport
    {
        public string Process { get; set; }
        public string Quarter { get; set; }
        public string PrintedBy { get; set; }
        public string DatePrinted { get; set; }
        public int RequestedByMale { get; set; }
        public int RequestedByFemale { get; set; }
        public int ItemsByMale { get; set; }
        public int ItemsByFemale { get; set; }
        public IEnumerable<EvaluationRating> EvaluationRating { get; set; }
    }
}
