using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entities
{
    public class ChangeLogs
    {
        public int Id { get; set; }
        public string Version { get; set; }
        public string Changelogs { get; set; }
        public DateTime? DateCreated { get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public Users Users { get; set; }
    }
}
