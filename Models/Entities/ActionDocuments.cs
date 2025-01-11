using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entities
{
    public class ActionDocuments
    {
        public int Id { get; set; }
        public int DocOrder { get; set; }
        public string DocumentName { get; set; }
        public string Description { get; set; }
        public string SecurityStamp { get; set; }
        public int? ActionId { get; set; }
        [ForeignKey("ActionId")]
        public Actions Action { get; set; } 
    }
}
