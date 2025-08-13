using Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICTProfilingV3.DataTransferModels.ViewModels
{
    public class PRStandardPRSpecsViewModel
    {
        public int Id { get; set; }
        public string Equipment { get; set; }
        public int? ItemNo { get; set; }
        public int? Quantity { get; set; }
        public long? TotalCost { get; set; }
        public StandardPRSpecs StandardPRSpecs { get; set; }
        public IEnumerable<StandardPRSpecsDetails> StandardPRSpecsDetails { get; set; }
        public BindingList<StandardPRSpecsDetails> Specifications =>
            new BindingList<StandardPRSpecsDetails>(StandardPRSpecsDetails.ToList());
    }
}
