using Models.Enums;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entities
{
    public class EvaluationSheet
    {
        public EvaluationSheet()
        {
            EvaluationSheetDocuments = new HashSet<EvaluationSheetDocument>();
        }
        public int Id { get; set; }
        public int ItemOrder { get; set; }
        public SheetService Service { get; set; }
        public SheetRating Rating { get; set; }
        public int? RatingValue { get; set; }
        public string Remarks { get; set; }
        public int? DeliveriesId { get; set; }
        [ForeignKey("DeliveriesId")]
        public Deliveries Deliveries { get; set; }
        public int? TechSpecsId { get; set; }
        [ForeignKey("TechSpecsId")]
        public TechSpecs TechSpecs { get; set; }
        public int? RepairId { get; set; }
        [ForeignKey("RepairId")]
        public Repairs Repairs { get; set; }
        public int? CustomerActionSheetId { get; set; }
        [ForeignKey("CustomerActionSheetId")]
        public CustomerActionSheet CustomerActionSheet { get; set; }
        public int? MOAccountUserId { get; set; }
        [ForeignKey("MOAccountUserId")]
        public MOAccountUsers MOAccountUsers { get; set; }
        public string CreatedById { get; set; }
        [ForeignKey("CreatedById")]
        public Users CreatedBy { get; set; }
        [JsonIgnore]
        public virtual ICollection<EvaluationSheetDocument> EvaluationSheetDocuments { get; set; }
    }
}
