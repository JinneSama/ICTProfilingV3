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
    
    public partial class PGNRequestAction
    {
        public int Id { get; set; }
        public Nullable<int> RequestId { get; set; }
        public Nullable<System.DateTime> ActionDate { get; set; }
        public string ActionBy { get; set; }
        public Nullable<int> ActionId { get; set; }
        public string RoutedTo { get; set; }
        public string Remarks { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }
        public Nullable<int> ActivityId { get; set; }
    }
}
