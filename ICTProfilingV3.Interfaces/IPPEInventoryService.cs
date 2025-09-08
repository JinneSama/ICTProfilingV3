using Models.Entities;

namespace ICTProfilingV3.Interfaces
{
    public interface IPPEInventoryService : IBaseDataService<PPEs, int>
    {
        IBaseDataService<PPEsSpecs, int> PPESpecsBaseService { get; set; }
        IBaseDataService<PPEsSpecsDetails, long> PPESpecsDetailsBaseService { get; set; }
    }
}
