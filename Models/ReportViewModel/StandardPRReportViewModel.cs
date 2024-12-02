using Models.Entities;
using System.Collections.Generic;

namespace Models.ReportViewModel
{
    public class StandardPRReportViewModel
    {
        public IEnumerable<StandardPREquipmentSupplies> StandardPREquipmentSupplies { get; set; }    
    }

    public class StandardPREquipmentSupplies
    {
        public string Quarter { get; set; }
        public string Title { get; set; }
        public IEnumerable<StandardPRSpecs> StandardPRSpecs { get; set; }
    }
}
