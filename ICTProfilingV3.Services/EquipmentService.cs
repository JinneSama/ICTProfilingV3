using ICTProfilingV3.DataTransferModels.ViewModels;
using ICTProfilingV3.Interfaces;
using ICTProfilingV3.Services.Base;
using Models.Entities;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ICTProfilingV3.Services
{
    public class EquipmentService : BaseDataService<Equipment, int>, IEquipmentService
    {
        private readonly IRepository<int, EquipmentSpecs> _equipmentSpecsRepo;
        public EquipmentService(IRepository<int, Equipment> baseRepo, IRepository<int, EquipmentSpecs> equipmentSpecsRepo,
            IBaseDataService<EquipmentSpecs, int> equipmentSpecsBaseService, IBaseDataService<EquipmentSpecsDetails, int> equipmentSpecsDetailsBaseService,
            IBaseDataService<Brand, int> brandBaseService, IBaseDataService<Model, int> modelBaseService, IBaseDataService<EquipmentCategory, int> equEquipmentCategoryBaseService, 
            IBaseDataService<EquipmentBrand, int> equipmentBrandBaseService, IBaseDataService<EquipmentCategoryBrand, int> equipmentCategoryBrandBaseService) : base(baseRepo)
        {
            EquipmentSpecsBaseService = equipmentSpecsBaseService;
            _equipmentSpecsRepo = equipmentSpecsRepo;
            EquipmentSpecsDetailsBaseService = equipmentSpecsDetailsBaseService;
            BrandBaseService = brandBaseService;
            ModelBaseService = modelBaseService;
            EquEquipmentCategoryBaseService = equEquipmentCategoryBaseService;
            EquipmentBrandBaseService = equipmentBrandBaseService;
            EquipmentCategoryBrandBaseService = equipmentCategoryBrandBaseService;
        }

        public IBaseDataService<EquipmentSpecs, int> EquipmentSpecsBaseService { get; set; }
        public IBaseDataService<EquipmentSpecsDetails, int> EquipmentSpecsDetailsBaseService { get; set; }
        public IBaseDataService<Brand, int> BrandBaseService { get; set; }
        public IBaseDataService<Model, int> ModelBaseService { get; set; }
        public IBaseDataService<EquipmentCategory, int> EquEquipmentCategoryBaseService { get; set; }
        public IBaseDataService<EquipmentBrand, int> EquipmentBrandBaseService { get; set; }
        public IBaseDataService<EquipmentCategoryBrand, int> EquipmentCategoryBrandBaseService { get; set; }

        public IEnumerable<EquipmentSpecsViewModel> GetEquipmentVM()
        {
            var res = _equipmentSpecsRepo.GetAll()
                .Include(x => x.Equipment)
                .Select(x => new EquipmentSpecsViewModel
                {
                    Remarks = x.Remarks,
                    Description = x.Description,
                    Equipment = x.Equipment.EquipmentName,
                    Id = x.Id
                });
            return res.ToList();
        }
    }
}
