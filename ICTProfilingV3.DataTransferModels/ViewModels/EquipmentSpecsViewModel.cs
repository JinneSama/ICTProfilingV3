using Models.Entities;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Linq;

namespace ICTProfilingV3.DataTransferModels.ViewModels
{
    public class EquipmentSpecsViewModel
    {
        public int Id { get; set; }
        public int? EquipmentId { get; set; }
        public string Equipment { get; set; }
        public string Description { get; set; }
        public string Remarks { get; set; }
        public EquipmentSpecs EquipmentSpecs { get; set; }
        public IEnumerable<EquipmentSpecsDetails> EquipmentSpecsDetails { get; set; } = new List<EquipmentSpecsDetails>();  
        public BindingList<EquipmentSpecsDetails> Specifications
        {
            get
            {
                if (EquipmentSpecsDetails == null) return null;
                return new BindingList<EquipmentSpecsDetails>(EquipmentSpecsDetails.ToList());
            }
        }
    }
}
