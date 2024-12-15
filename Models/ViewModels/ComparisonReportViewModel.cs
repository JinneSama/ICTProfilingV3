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
        private IEnumerable<ComparisonReportSpecsDetails> _CRSpecsDetails;
        public IEnumerable<ComparisonReportSpecsDetails> CRSpecsDetails 
        {
            get
            {
                if (_CRSpecsDetails == null)
                    return new BindingList<ComparisonReportSpecsDetails>();

                return new BindingList<ComparisonReportSpecsDetails>(_CRSpecsDetails.OrderBy(x => x.ItemOrder).ToList());
            }
            set
            {
                _CRSpecsDetails = value;
            }
        }
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
