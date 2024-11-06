using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entities
{
    public class Actions
    {
        public Actions()
        {
            RoutedUsers = new HashSet<Users>();
        }
        public int Id { get; set; }
        public string ActionTaken { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? ActionDate { get; set; }
        public string Remarks { get; set; }
        public bool? IsSend { get; set; }
        public int? ProgramId { get; set; }

        [ForeignKey("ProgramId")]
        public ActionsDropdowns ProgramDropdowns { get; set; }
        public int? MainActId { get; set; }

        [ForeignKey("MainActId")]
        public ActionsDropdowns MainActDropdowns { get; set; }
        public int? ActivityId { get; set; }

        [ForeignKey("ActivityId")]
        public ActionsDropdowns ActivityDropdowns { get; set; }
        public int? SubActivityId { get; set; }

        [ForeignKey("SubActivityId")]
        public ActionsDropdowns SubActivityDropdowns { get; set; }

        public int? DeliveriesId { get; set; }
        [ForeignKey("DeliveriesId")]
        public Deliveries Deliveries { get; set; }
        public int? TechSpecsId { get; set; }
        [ForeignKey("TechSpecsId")]
        public TechSpecs TechSpecs { get; set; }
        public int? RepairId { get; set; }
        [ForeignKey("RepairId")]
        public Repairs Repairs { get; set; }

        public string CreatedById { get; set; }

        [ForeignKey("CreatedById")]
        [InverseProperty("CreatedActions")]
        public virtual Users CreatedBy { get; set; }
        public virtual ICollection<Users> RoutedUsers { get; set; }
    }
}
