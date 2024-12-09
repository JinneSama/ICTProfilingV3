using Models.Entities;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Models.ViewModels
{
    public class ComparisonReportViewModel
    {
        public int CRId { get; set; }
        public ComparisonReportSpecs CRSpecs { get; set; }
        public IEnumerable<ComparisonReportSpecsDetails> CRSpecsDetails { get; set; }
        public BindingList<ComparisonReportSpecsDetails> Specifications
        {
            get
            {
                if (CRSpecsDetails == null) return null;
                return new BindingList<ComparisonReportSpecsDetails>(CRSpecsDetails.OrderBy(x => x.ItemOrder).ToList());
            }
        }
    }
}
