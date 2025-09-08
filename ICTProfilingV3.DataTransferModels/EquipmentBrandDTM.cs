using Models.Entities;
using System.ComponentModel;

namespace ICTProfilingV3.DataTransferModels
{
    public class EquipmentBrandDTM
    {
        public EquipmentCategory EquipmentCategory { get; set; }
        public BindingList<BrandDTM> Brands { get; set; }
    }
}
