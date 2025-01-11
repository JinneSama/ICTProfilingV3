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
    
    public partial class TicketRequest
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TicketRequest()
        {
            this.Deliveries = new HashSet<Delivery>();
            this.TechSpecs = new HashSet<TechSpec>();
            this.TicketActions = new HashSet<TicketAction>();
            this.TicketReqDetails = new HashSet<TicketReqDetail>();
            this.TicketReqICTSpecs = new HashSet<TicketReqICTSpec>();
        }
    
        public int Id { get; set; }
        public Nullable<int> ReqOfficeId { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }
        public Nullable<System.DateTime> DateRequested { get; set; }
        public string ControlNo { get; set; }
        public Nullable<int> TypeId { get; set; }
        public string Category { get; set; }
        public string ActedBy { get; set; }
        public Nullable<System.DateTime> DateActed { get; set; }
        public string StatusLatest { get; set; }
        public Nullable<bool> Cancelled { get; set; }
        public Nullable<int> PPENo { get; set; }
        public string ContactNo { get; set; }
        public string ReqByName { get; set; }
        public string ReqByPos { get; set; }
        public string Gender { get; set; }
        public Nullable<long> ItemDelById { get; set; }
        public string ItemDelByName { get; set; }
        public string ItemDelByPos { get; set; }
        public Nullable<System.DateTime> DateDelivered { get; set; }
        public Nullable<int> ICTSpecsId { get; set; }
        public string ReqProblem { get; set; }
        public Nullable<int> BrandId { get; set; }
        public Nullable<int> ModelId { get; set; }
        public string Accessories { get; set; }
        public string Password { get; set; }
        public string SerialNo { get; set; }
        public string ItemsRecBy { get; set; }
        public string EPISNo { get; set; }
        public Nullable<int> SupplierId { get; set; }
        public string DelReceiptNo { get; set; }
        public Nullable<bool> RequestBasedApprovedPR { get; set; }
        public Nullable<bool> RequestBasedApprovedAPP { get; set; }
        public Nullable<bool> RequestBasedApprovedAIP { get; set; }
        public Nullable<bool> RequestBasedApprovedPPMP { get; set; }
        public Nullable<bool> RequestBasedRequestLetter { get; set; }
        public string RequestBasedOn { get; set; }
        public Nullable<long> ReqByIdNo { get; set; }
        public Nullable<long> ReqByChiefIdNo { get; set; }
        public string ReqByChiefName { get; set; }
        public string ReqByChiefPos { get; set; }
        public string ReqByOfficeId { get; set; }
        public string ReqByDivision { get; set; }
        public Nullable<bool> IsDivision { get; set; }
        public string ReqOffAcr { get; set; }
        public Nullable<int> PONo { get; set; }
        public string PONoString { get; set; }
        public Nullable<bool> RequestBasedForReplacement { get; set; }
        public Nullable<bool> OverdueTrigger { get; set; }
        public Nullable<System.DateTime> OverdueStartDate { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Delivery> Deliveries { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TechSpec> TechSpecs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TicketAction> TicketActions { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TicketReqDetail> TicketReqDetails { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TicketReqICTSpec> TicketReqICTSpecs { get; set; }
        public virtual TicketType TicketType { get; set; }
    }
}
