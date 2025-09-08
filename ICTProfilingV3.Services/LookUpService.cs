using ICTProfilingV3.Interfaces;
using Models.Entities;

namespace ICTProfilingV3.Services
{
    public class LookUpService : ILookUpService
    {
        public IBaseDataService<Supplier, int> SupplierDataService { get; set; }
        public LookUpService(IBaseDataService<Supplier, int> supplierBaseService)
        {
            SupplierDataService = supplierBaseService;
        }
    }
}
