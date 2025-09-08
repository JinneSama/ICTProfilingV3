using Models.Entities;
using Models.Enums;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace ICTProfilingV3.DataTransferModels.ViewModels
{
    public class TechSpecsICTSpecsViewModel
    {
        public int Id { get; set; }
        public int ItemNo { get; set; }
        public int? Quantity { get; set; }
        public Unit? Unit { get; set; }
        public string Equipment { get; set; }
        public int? EquipmentSpecsId { get; set; }
        public string Description { get; set; }
        public decimal? UnitCost { get; set; }
        public decimal? TotalCost { get; set; }
        public string Purpose { get; set; }
        public int TechSpecsId { get; set; }
        public IEnumerable<TechSpecsICTSpecsDetails> TechSpecsICTSpecsDetails { get; set; }
        public BindingList<TechSpecsICTSpecsDetails> Specifications
        {
            get
            {
                return new BindingList<TechSpecsICTSpecsDetails>(TechSpecsICTSpecsDetails.OrderBy(o => o.ItemNo).ToList());
            }    
        }
    }
}
