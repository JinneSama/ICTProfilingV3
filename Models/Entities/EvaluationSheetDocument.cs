using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entities
{
    public class EvaluationSheetDocument
    {
        public int Id { get; set; }
        public int DocOrder { get; set; }
        public string DocumentName { get; set; }
        public string Description { get; set; }
        public string SecurityStamp { get; set; }
        public int EvaluationSheetId { get; set; }
        [ForeignKey("EvaluationSheetId")]
        public EvaluationSheet EvaluationSheet { get; set; }
    }
}
