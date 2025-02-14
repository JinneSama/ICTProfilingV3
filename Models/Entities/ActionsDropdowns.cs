﻿using Models.Enums;
using System.Collections.Generic;

namespace Models.Entities
{
    public class ActionsDropdowns
    {
        public ActionsDropdowns()
        {
            ProgramActions = new HashSet<Actions>();
            MainActActions = new HashSet<Actions>();
            ActivityActions = new HashSet<Actions>();
            SubActivityActions = new HashSet<Actions>();
        }
        public int? Id { get; set; }
        public ActionCategory ActionCategory { get; set; }
        public string Value { get; set; }
        public int ParentId { get; set; }
        public int Order { get; set; }
        public virtual ICollection<Actions> ProgramActions { get; set; }
        public virtual ICollection<Actions> MainActActions { get; set; }
        public virtual ICollection<Actions> ActivityActions { get; set; }
        public virtual ICollection<Actions> SubActivityActions { get; set; }
    }
}
