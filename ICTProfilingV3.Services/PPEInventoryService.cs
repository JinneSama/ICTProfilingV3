using ICTProfilingV3.Interfaces;
using ICTProfilingV3.Services.Base;
using Models.Entities;
using System.Threading.Tasks;

namespace ICTProfilingV3.Services
{
    public class PPEInventoryService : BaseDataService<PPEs, int>, IPPEInventoryService
    {
        public PPEInventoryService(IRepository<int, PPEs> baseRepo,
            IBaseDataService<PPEsSpecs, int> ppeSpecsBaseService,
            IBaseDataService<PPEsSpecsDetails, long> ppeSpecsDetailsBaseService) : base(baseRepo)
        {
            PPESpecsBaseService = ppeSpecsBaseService;
            PPESpecsDetailsBaseService = ppeSpecsDetailsBaseService;
        }

        public IBaseDataService<PPEsSpecs, int> PPESpecsBaseService { get; set; }
        public IBaseDataService<PPEsSpecsDetails, long> PPESpecsDetailsBaseService { get; set; }

        public override async Task DeleteAsync(int id)
        {
            await PPESpecsBaseService.DeleteRangeAsync(x => x.PPEsId == id);
            await base.DeleteAsync(id);
        }
    }
}
