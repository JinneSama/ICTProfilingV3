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
    
    public partial class SoftwareProfile
    {
        public int Id { get; set; }
        public Nullable<int> fldSoftwareId { get; set; }
        public string fldLicense { get; set; }
        public Nullable<int> fldPPEProfileId { get; set; }
        public string fldUserAccount { get; set; }
        public string fldPassword { get; set; }
        public Nullable<System.DateTime> fldDateCreated { get; set; }
    }
}
