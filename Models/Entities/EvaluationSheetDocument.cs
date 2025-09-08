using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entities
{
    public class EvaluationSheetDocument
    {
        public int Id { get; set; }
        public int DocOrder { get; set; }
        [MaxLength(128)]
        public string DocumentName { get; set; }
        [MaxLength(128)]
        public string Description { get; set; }
        [MaxLength(128)]
        public string SecurityStamp { get; set; }
        public int EvaluationSheetId { get; set; }
        [ForeignKey("EvaluationSheetId")]
        public EvaluationSheet EvaluationSheet { get; set; }
    }
}
