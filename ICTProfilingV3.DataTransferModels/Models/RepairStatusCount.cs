using Models.Enums;
using System;

namespace ICTProfilingV3.DataTransferModels.Models
{
    public class RepairStatusCount
    {
        public DateTime? DateCreated { get; set; }
        public string RepairNo { get; set; }
        public string Brand { get; set; }
        public PPEStatus Status { get; set; }
    }
}
