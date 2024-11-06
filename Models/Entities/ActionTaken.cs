using System;

namespace Models.Entities
{
    public class ActionTaken
    {
        public int Id { get; set; }
        public string Action { get; set; }
        public DateTime? DateAdded { get; set; }
    }
}
