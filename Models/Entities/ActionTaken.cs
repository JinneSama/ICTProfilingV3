using System;
using System.ComponentModel.DataAnnotations;

namespace Models.Entities
{
    public class ActionTaken
    {
        public int Id { get; set; }
        [MaxLength(128)]
        public string Action { get; set; }
        public DateTime? DateAdded { get; set; }
    }
}
