using Models.Enums;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entities
{
    public class RoleDesignation
    {
        public int Id { get; set; }
        public Designation Designation { get; set; }
        public string RoleId { get; set; }

        [ForeignKey("RoleId")]
        public Roles Roles { get; set; }

    }
}
