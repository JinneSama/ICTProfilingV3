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
    
    public partial class PGNMacAdderess
    {
        public int Id { get; set; }
        public Nullable<int> EmpId { get; set; }
        public string DeviceName { get; set; }
        public string DeviceMac { get; set; }
    
        public virtual PGNAccount PGNAccount { get; set; }
    }
}
