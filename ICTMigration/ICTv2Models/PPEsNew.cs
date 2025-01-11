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
    
    public partial class PPEsNew
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PPEsNew()
        {
            this.PPEsNewSpecs = new HashSet<PPEsNewSpec>();
            this.Repairs = new HashSet<Repair>();
        }
    
        public int Id { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }
        public Nullable<long> PropertyNo { get; set; }
        public Nullable<long> IssuedToId { get; set; }
        public string IssuedTo { get; set; }
        public string IssuedToPos { get; set; }
        public string ContactNo { get; set; }
        public string Gender { get; set; }
        public Nullable<long> ChiefId { get; set; }
        public string ChiefName { get; set; }
        public string ChiefPos { get; set; }
        public string OfficeId { get; set; }
        public string Division { get; set; }
        public Nullable<bool> IsDivision { get; set; }
        public string OffAcr { get; set; }
        public Nullable<int> ICTSpecsId { get; set; }
        public string Description { get; set; }
        public string Remarks { get; set; }
        public Nullable<int> BrandId { get; set; }
        public Nullable<int> ModelId { get; set; }
        public Nullable<decimal> Quantity { get; set; }
        public string Unit { get; set; }
        public Nullable<decimal> UnitCost { get; set; }
        public Nullable<decimal> TotalCost { get; set; }
        public Nullable<System.DateTime> InvoiceDate { get; set; }
        public string SerialNo { get; set; }
        public string Status { get; set; }
        public Nullable<bool> Deleted { get; set; }
        public string OfficeInstalled { get; set; }
        public string PropertyNoString { get; set; }
    
        public virtual TicketEquipmentBrand TicketEquipmentBrand { get; set; }
        public virtual TicketEquipmentModel TicketEquipmentModel { get; set; }
        public virtual TicketICTSpec TicketICTSpec { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PPEsNewSpec> PPEsNewSpecs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Repair> Repairs { get; set; }
    }
}
