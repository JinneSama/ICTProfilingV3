using Models.Entities;

namespace ICTProfilingV3.Interfaces
{
    public interface ILookUpService
    {
        IBaseDataService<Supplier, int> SupplierDataService { get; set; }
    }
}
