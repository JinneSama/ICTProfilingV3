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
    
    public partial class UserRolesInAction
    {
        public int Id { get; set; }
        public string RoleId { get; set; }
        public string Action { get; set; }
    
        public virtual UserRole UserRole { get; set; }
    }
}
