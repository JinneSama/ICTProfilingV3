using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entities
{
    public class StandardPRSpecsDetails
    {
        public int Id { get; set; }
        public int ItemNo { get; set; }
        public string Specs { get; set; }
        public string Description { get; set; }
        public int? StandardPRSpecsId { get; set; }

        [ForeignKey("StandardPRSpecsId")]
        public StandardPRSpecs StandardPRSpecs { get; set; }
    }
}
