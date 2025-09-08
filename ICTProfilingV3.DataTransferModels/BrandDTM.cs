using Models.Entities;
using System.ComponentModel;

namespace ICTProfilingV3.DataTransferModels
{
    public class BrandDTM
    {
        public int EquipmentCategoryBrandId { get; set; }
        public EquipmentBrand Brand { get; set; }
        public BindingList<Model> Models { get; set; }
    }
}
