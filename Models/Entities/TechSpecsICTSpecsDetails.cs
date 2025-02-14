﻿using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entities
{
    public class TechSpecsICTSpecsDetails
    {
        public int Id { get; set; }
        public int ItemNo { get; set; }
        public string Specs { get; set; }
        public string Description { get; set; }
        public bool? IsActive { get; set; }
        public int TechSpecsICTSpecsId { get; set; }

        [ForeignKey("TechSpecsICTSpecsId")]
        public TechSpecsICTSpecs TechSpecsICTSpecs { get; set; }
    }
}
