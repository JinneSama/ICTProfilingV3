using ICTProfilingV3.DataTransferModels.ViewModels;
using Models.Entities;
using System.Collections.Generic;
using System.Linq;

namespace ICTProfilingV3.Interfaces
{
    public interface IEquipmentService : IBaseDataService<Equipment, int>
    {
        IEnumerable<EquipmentSpecsViewModel> GetEquipmentVM();
        IBaseDataService<EquipmentSpecs, int> EquipmentSpecsBaseService { get; set; }
        IBaseDataService<EquipmentSpecsDetails, int> EquipmentSpecsDetailsBaseService { get; set; }
        IBaseDataService<EquipmentCategory, int> EquEquipmentCategoryBaseService { get; set; }
        IBaseDataService<Brand, int> BrandBaseService { get; set; }
        IBaseDataService<Model, int> ModelBaseService { get; set; }
        IBaseDataService<EquipmentBrand, int> EquipmentBrandBaseService { get; set; }
        IBaseDataService<EquipmentCategoryBrand, int> EquipmentCategoryBrandBaseService { get; set; }
    }
}
