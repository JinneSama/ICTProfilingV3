using ICTProfilingV3.DataTransferModels.ViewModels;
using ICTProfilingV3.Interfaces;
using ICTProfilingV3.Services.Base;
using Models.Entities;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ICTProfilingV3.Services
{
    public class PGNService : BaseDataService<PGNAccounts, int>, IPGNService
    {
        public PGNService(IRepository<int, PGNAccounts> baseRepo, 
            IBaseDataService<PGNMacAddresses, int> pGNMacAddressService,
            IBaseDataService<PGNGroupOffices, int> pGNGroupOfficeService,
            IBaseDataService<PGNRequests, int> pGNRequestsService,
            IBaseDataService<PGNNonEmployee, int> pGNNonEmployeeService,
            PGNDocumentsService pGNDocumentService) : base(baseRepo)
        {
            PGNMacAddressService = pGNMacAddressService;
            PGNGroupOfficeService = pGNGroupOfficeService;
            PGNRequestsService = pGNRequestsService;
            PGNDocumentService = pGNDocumentService;
            PGNNonEmployeeService = pGNNonEmployeeService;
        }

        public IBaseDataService<PGNMacAddresses, int> PGNMacAddressService { get; set; }
        public IBaseDataService<PGNGroupOffices, int> PGNGroupOfficeService { get; set; }
        public IBaseDataService<PGNNonEmployee, int> PGNNonEmployeeService { get; set; }
        public IBaseDataService<PGNRequests, int> PGNRequestsService { get; set; }
        public IDocumentService<PGNDocuments, int> PGNDocumentService { get; set; }

        public IEnumerable<PGNAccountsViewModel> GetPGNAccViewModel()
        {
            var accounts = base.GetAll()
                .Include(x => x.PGNGroupOffices)
                .Include(x => x.PGNNonEmployee)
                .ToList();

            return accounts.Select(x => new PGNAccountsViewModel { PGNAccount = x }).ToList();
        }
    }
}
