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
    
    public partial class tblFWTUActivity
    {
        public int Id { get; set; }
        public Nullable<int> FWTUEquipmentId { get; set; }
        public string Title { get; set; }
        public string Activity { get; set; }
        public string Observation { get; set; }
    
        public virtual tblFWTUEquipmentInstallation tblFWTUEquipmentInstallation { get; set; }
    }
}
