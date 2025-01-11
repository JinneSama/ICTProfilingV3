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
    
    public partial class PurchaseOrder
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PurchaseOrder()
        {
            this.EquipmentProfiles = new HashSet<EquipmentProfile>();
        }
    
        public int Id { get; set; }
        public Nullable<int> PurchaseNumber { get; set; }
        public Nullable<int> OfficeId { get; set; }
        public Nullable<int> EmployeeId { get; set; }
        public Nullable<int> EquipmentTypeId { get; set; }
        public string Description { get; set; }
        public Nullable<System.DateTime> DateDelivered { get; set; }
        public Nullable<System.DateTime> PurchaseDate { get; set; }
        public Nullable<int> SupplierId { get; set; }
        public string Remarks { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EquipmentProfile> EquipmentProfiles { get; set; }
        public virtual Office Office { get; set; }
        public virtual Supplier Supplier { get; set; }
    }
}
