using ICTProfilingV3.DataTransferModels.ViewModels;
using Models.Entities;
using System.Collections.Generic;

namespace ICTProfilingV3.Interfaces
{
    public interface IPGNService : IBaseDataService<PGNAccounts, int>
    {
        IBaseDataService<PGNMacAddresses, int> PGNMacAddressService { get; set; }
        IBaseDataService<PGNGroupOffices, int> PGNGroupOfficeService { get; set; }
        IBaseDataService<PGNNonEmployee, int> PGNNonEmployeeService { get; set; }
        IBaseDataService<PGNRequests, int> PGNRequestsService { get; set; }
        IDocumentService<PGNDocuments, int> PGNDocumentService { get; set; }
        IEnumerable<PGNAccountsViewModel> GetPGNAccViewModel();
    }
}
