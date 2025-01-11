//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ICTMigration.ICTv2Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class TechSpecsBasi
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TechSpecsBasi()
        {
            this.TechSpecsBasisDetails = new HashSet<TechSpecsBasisDetail>();
        }
    
        public int Id { get; set; }
        public Nullable<int> ICTSpecsId { get; set; }
        public Nullable<decimal> PriceRange { get; set; }
        public Nullable<bool> Available { get; set; }
        public string Basis { get; set; }
        public string Remarks { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }
        public Nullable<System.DateTime> PriceDate { get; set; }
    
        public virtual TicketICTSpec TicketICTSpec { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TechSpecsBasisDetail> TechSpecsBasisDetails { get; set; }
    }
}
