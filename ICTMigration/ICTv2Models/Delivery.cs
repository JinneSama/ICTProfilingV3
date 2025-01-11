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
    
    public partial class Delivery
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Delivery()
        {
            this.DeliveriesICTSpecs = new HashSet<DeliveriesICTSpec>();
        }
    
        public int Id { get; set; }
        public string DeliveryId { get; set; }
        public Nullable<int> SupplierId { get; set; }
        public Nullable<int> OfficeId { get; set; }
        public Nullable<System.DateTime> DeliveredDate { get; set; }
        public byte[] Files { get; set; }
        public Nullable<System.DateTime> DateRequested { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }
        public Nullable<long> ReqById { get; set; }
        public string ReqByName { get; set; }
        public string ReqByPos { get; set; }
        public string ContactNo { get; set; }
        public string Gender { get; set; }
        public Nullable<long> ItemDelById { get; set; }
        public string ItemDelByName { get; set; }
        public string ItemDelByPos { get; set; }
        public string DelReceiptNo { get; set; }
        public Nullable<long> ReqByChiefId { get; set; }
        public string ReqByChiefName { get; set; }
        public string ReqByChiefPos { get; set; }
        public string ReqByOfficeId { get; set; }
        public string ReqByDivision { get; set; }
        public Nullable<bool> IsDivision { get; set; }
        public Nullable<int> RequestId { get; set; }
        public string ReqOffAcr { get; set; }
        public Nullable<int> PONo { get; set; }
        public string PONoString { get; set; }
        public Nullable<int> CRPRId { get; set; }
        public string CRRemarks { get; set; }
        public string CRPreparedId { get; set; }
        public string CRReviewedId { get; set; }
        public string CRNotedId { get; set; }
    
        public virtual Office Office { get; set; }
        public virtual Supplier Supplier { get; set; }
        public virtual TicketRequest TicketRequest { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DeliveriesICTSpec> DeliveriesICTSpecs { get; set; }
        public virtual PurchaseReq PurchaseReq { get; set; }
    }
}
