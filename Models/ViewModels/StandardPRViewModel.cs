using Models.Entities;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Models.ViewModels
{
    public class StandardPRViewModel
    {
        public string Equipment { get; set; }
        public StandardPRSpecs StandardPRSpecs { get; set; }
        public IEnumerable<StandardPRSpecsDetails> StandardPRSpecsDetails { get; set; }  
        public BindingList<StandardPRSpecsDetails> Specifications =>
            new BindingList<StandardPRSpecsDetails>(StandardPRSpecsDetails.ToList());

        public bool Mark { get; set; }

        public string Type
        {
            get
            {
                string type;
                if (StandardPRSpecs.UnitCost <= 50000) type = "IT SUPPLIES";
                else type = "IT EQUIPMENT";
                return type;
            }
        }
    }
}
