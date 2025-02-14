﻿using Models.Enums;
using System;
using System.Collections.Generic;

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
        public string ContactNo { get; set; }
        public string PropertyNo { get; set; }
        public string SerialNo { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? AquisitionDate { get; set; }
        public PPEStatus? Status { get; set; }
        public int Quantity { get; set; }
        public Unit Unit { get; set; }
        public long? UnitValue { get; set; }
        public long? TotalValue { get; set; }
        public string Remarks { get; set; }
        public bool? IsDeleted { get; set; }
        public virtual ICollection<PPEsSpecs> PPEsSpecs { get; set; }
        public virtual ICollection<Repairs> Repairs { get; set; }
        public PPEs()
        {
            PPEsSpecs = new HashSet<PPEsSpecs>();
            Repairs= new HashSet<Repairs>();    
        }
    }
}
