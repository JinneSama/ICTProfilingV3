using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entities
{
    public class PPEsSpecsDetails
    {
        public long Id { get; set; }
        public int PPEsSpecsId { get; set; }
        public int ItemNo { get; set; }
        public string Specs { get; set; }
        public string Description { get; set; }

        [ForeignKey("PPEsSpecsId")]
        public PPEsSpecs PPEsSpecs { get; set; }
    }
}
