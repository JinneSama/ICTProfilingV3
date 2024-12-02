using Models.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entities
{
    public class ITStaff
    {
        [Key]
        public int Id { get; set; }
        public string ImagePath { get; set; }
        public Sections Section { get; set; }
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public Users Users { get; set; }
        public virtual ICollection<TicketRequest> TicketRequests { get; set; }
        public ITStaff()
        {
            TicketRequests= new HashSet<TicketRequest>();
        }
    }
}
