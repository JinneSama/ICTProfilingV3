using Models.Enums;
using System;
using System.Collections.Generic;

namespace ICTProfilingV3.DataTransferModels
{
    public class ActionDTM
    {
        public string ActionTaken { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public DateTime? ActionDate { get; set; }
        public string Remarks { get; set; }
        public bool? IsSend { get; set; }
        public string CreatedBy { get; set; }
        public int? Program { get; set; }
        public int? MainActivity { get; set; }
        public int? Activity { get; set; }
        public int? SubActivity { get; set; }
        public string RoutedTo { get; set; }
        public RequestType RequestType { get; set; }
        public List<UsersDTM> RoutedUsersObject { get; set; }
    }
}
