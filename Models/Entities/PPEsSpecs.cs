using Models.Enums;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entities
{
    public class PPEsSpecs
    {
        public int Id { get; set; }
        public int ItemNo { get; set; }
        public string Description { get; set; }
        public string Remarks { get; set; }
        public int Quantity { get; set; }
        public Unit Unit { get; set; }
        public long UnitCost { get; set; }
        public long TotalCost { get; set; }
        public long ProposedBudget { get; set; }
        public string Purpose { get; set; }
        public bool IsActive { get; set; }
        public string SerialNo { get; set; }
        public int ModelId { get; set; }

        [ForeignKey("ModelId")]
        public Model Model { get; set; }
        public int? PPEsId { get; set; }

        [ForeignKey("PPEsId")]
        public PPEs PPEs { get; set; }
        [JsonIgnore]
        public virtual ICollection<PPEsSpecsDetails> PPEsSpecsDetails { get; set; }
        [JsonIgnore]
        public virtual ICollection<Repairs> Repairs { get; set; }

        public PPEsSpecs()
        {
            PPEsSpecsDetails = new HashSet<PPEsSpecsDetails>();
            Repairs = new HashSet<Repairs>();
        }
    }
}
