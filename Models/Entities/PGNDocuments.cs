using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entities
{
    public class PGNDocuments
    {
        public int Id { get; set; }
        public int DocOrder { get; set; }
        public string FilePath { get; set; }
        public string FileName { get; set; }
        public string SecurityStamp { get; set; }
        public int PGNRequestId { get; set; }
        [ForeignKey("PGNRequestId")]
        public virtual PGNRequests PGNRequest { get; set; } 
    }
}
