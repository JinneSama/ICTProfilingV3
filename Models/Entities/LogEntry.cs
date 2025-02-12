using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entities
{
    public class LogEntry
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string MacAddress { get; set; }
        public string PCName { get; set; }
        public string TableName { get; set; }
        public string ActionType { get; set; }
        public string OldValues { get; set; }
        public string NewValues { get; set; }
        public string CreatedById { get; set; }
        [ForeignKey("CreatedById")]
        public Users CreatedByUser { get; set; }
    }
}
