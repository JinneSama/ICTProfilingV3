using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entities
{
    public class LogEntry
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        [MaxLength(128)]
        public string MacAddress { get; set; }
        [MaxLength(128)]
        public string PCName { get; set; }
        [MaxLength(128)]
        public string TableName { get; set; }
        [MaxLength(128)]
        public string ActionType { get; set; }
        [MaxLength(128)]
        public string OldValues { get; set; }
        [MaxLength(128)]
        public string NewValues { get; set; }
        public string CreatedById { get; set; }
        [ForeignKey("CreatedById")]
        public Users CreatedByUser { get; set; }
    }
}
