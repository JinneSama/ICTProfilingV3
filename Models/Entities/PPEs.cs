using Models.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models.Entities
{
    public class PPEs
    {
        public int Id { get; set; }

        //----HRIS Data----
        public long? IssuedToId { get; set; }
        public long? ChiefId { get; set; }
        //----HRIS Data----
        public Gender? Gender { get; set; }
        [MaxLength(128)]
        public string ContactNo { get; set; }
        [MaxLength(128)]
        public string PropertyNo { get; set; }
        [MaxLength(128)]
        public string SerialNo { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? AquisitionDate { get; set; }
        public PPEStatus? Status { get; set; }
        public int Quantity { get; set; }
        public Unit Unit { get; set; }
        public long? UnitValue { get; set; }
        public long? TotalValue { get; set; }
        [MaxLength(4096)]
        public string Remarks { get; set; }
        public bool? IsDeleted { get; set; }
        //public bool? IsParsed { get; set; }
        public int? OldPk { get; set; }
        [JsonIgnore]
        public virtual ICollection<PPEsSpecs> PPEsSpecs { get; set; }
        [JsonIgnore]
        public virtual ICollection<Repairs> Repairs { get; set; }
        [JsonIgnore]
        public virtual ICollection<MOAccountUsers> MOAccountUsers { get; set; }
        public PPEs()
        {
            PPEsSpecs = new HashSet<PPEsSpecs>();
            Repairs= new HashSet<Repairs>();    
            MOAccountUsers = new HashSet<MOAccountUsers>();
        }
    }
}
