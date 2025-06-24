using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entities
{
    public class ChangeLogs
    {
        public int Id { get; set; }
        [MaxLength(128)]
        public string Version { get; set; }
        [MaxLength(2048)]
        public string Changelogs { get; set; }
        public DateTime? DateCreated { get; set; }
        [MaxLength(128)]
        public string ImageName { get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public Users Users { get; set; }
    }
}
