using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entities
{
    public class MOAccountUsers
    {
        public MOAccountUsers()
        {
            Actions = new HashSet<Actions>();
        }
        public int Id { get; set; }
        public int DeviceNo { get; set; }
        public DateTime? DateCreated { get; set; }
        //HRIS Data
        public long IssuedTo { get; set; }
        public long AccountUser { get; set; }
        //HRIS Data
        public DateTime? DateOfInstallation { get; set; }
        public DateTime? ProcuredDate { get; set; }
        public string Remarks { get; set; }
        public string Description { get; set; }

        public int PPEId { get; set; }
        [ForeignKey("PPEId")]
        public PPEs PPE { get; set; }
        public int MOAccountId { get; set; }
        [ForeignKey("MOAccountId")]
        public MOAccounts MOAccount { get; set; }
        public string CreatedById { get; set; }
        [ForeignKey("CreatedById")]
        public Users CreatedBy { get; set; }
        public virtual ICollection<Actions> Actions { get; set; }
    }
}
