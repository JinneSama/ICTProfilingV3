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
    
    public partial class TicketReqICTSpecsDetail
    {
        public int Id { get; set; }
        public Nullable<int> ReqICTSpecdId { get; set; }
        public Nullable<int> ItemNo { get; set; }
        public string EquipmentSpecs { get; set; }
        public string EquipmentDescrip { get; set; }
        public Nullable<bool> IsActive { get; set; }
    
        public virtual TicketReqICTSpec TicketReqICTSpec { get; set; }
    }
}
