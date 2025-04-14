using Models.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Models.Repository
{
    public interface IUnitOfWork
    {
        IGenericRepository<Users> UsersRepo { get; }
        IGenericRepository<Supplier> SupplierRepo { get; }
        IGenericRepository<Equipment> EquipmentRepo { get; }
        IGenericRepository<EquipmentSpecs> EquipmentSpecsRepo { get; }
        IGenericRepository<EquipmentSpecsDetails> EquipmentSpecsDetailsRepo { get; }
        IGenericRepository<Brand> BrandRepo { get; }
        IGenericRepository<Model> ModelRepo { get; }
        IGenericRepository<Deliveries> DeliveriesRepo { get; }
        IGenericRepository<DeliveriesSpecs> DeliveriesSpecsRepo { get; }
        IGenericRepository<DeliveriesSpecsDetails> DeliveriesSpecsDetailsRepo { get; }
        IGenericRepository<TicketRequest> TicketRequestRepo { get; }
        IGenericRepository<ActionsDropdowns> ActionsDropdownsRepo { get; }
        IGenericRepository<Actions> ActionsRepo { get; }
        IGenericRepository<ActionTaken> ActionTakenRepo { get; }
        IGenericRepository<TechSpecsBasis> TechSpecsBasisRepo { get; }
        IGenericRepository<TechSpecs> TechSpecsRepo { get; }
        IGenericRepository<TechSpecsICTSpecs> TechSpecsICTSpecsRepo { get; }
        IGenericRepository<TechSpecsICTSpecsDetails> TechSpecsICTSpecsDetailsRepo { get; }
        IGenericRepository<PPEs> PPesRepo { get; }
        IGenericRepository<PPEsSpecs> PPEsSpecsRepo { get; }
        IGenericRepository<PPEsSpecsDetails> PPEsSpecsDetailsRepo { get; }
        IGenericRepository<Repairs> RepairsRepo { get; }
        IGenericRepository<CustomerActionSheet> CustomerActionSheetRepo { get; }
        IGenericRepository<PurchaseRequest> PurchaseRequestRepo { get; }
        IGenericRepository<StandardPRSpecs> StandardPRSpecsRepo { get; }
        IGenericRepository<StandardPRSpecsDetails> StandardPRSpecsDetailsRepo { get; }
        IGenericRepository<PRStandardPRSpecs> PRStandardPRSpecsRepo { get; }
        IGenericRepository<ITStaff> ITStaffRepo { get; }
        IGenericRepository<TicketRequestStatus> TicketRequestStatusRepo { get; }
        IGenericRepository<PGNAccounts> PGNAccountsRepo { get; }
        IGenericRepository<PGNDocuments> PGNDocumentsRepo { get; }
        IGenericRepository<PGNMacAddresses> PGNMacAddressesRepo { get; }
        IGenericRepository<PGNNonEmployee> PGNNonEmployeeRepo { get; }
        IGenericRepository<PGNRequests> PGNRequestsRepo { get; }
        IGenericRepository<PGNGroupOffices> PGNGroupOfficesRepo { get; }
        IGenericRepository<ComparisonReport> ComparisonReportRepo { get; }
        IGenericRepository<ComparisonReportSpecs> ComparisonReportSpecsRepo { get; }
        IGenericRepository<ComparisonReportSpecsDetails> ComparisonReportSpecsDetailsRepo { get; }
        IGenericRepository<ChangeLogs> ChangeLogsRepo { get; }
        IGenericRepository<MOAccounts> MOAccountRepo { get; }
        IGenericRepository<MOAccountUsers> MOAccountUserRepo { get; }
        IGenericRepository<ActionDocuments> ActionDocumentsRepo { get; }
        IGenericRepository<RecordsRequestStatus> RecordsRequestStatus { get; }
        IGenericRepository<EvaluationSheet> EvaluationSheetRepo { get; }
        IGenericRepository<EvaluationSheetDocument> EvaluationSheetDocumentRepo { get; }
        IGenericRepository<TechSpecsBasisDetails> TechSpecsBasisDetailsRepo { get; }
        IGenericRepository<LogEntry> LogEntriesRepo { get; }
        void Save();
        void ExecuteCommand(string command);
        Task SaveChangesAsync();
    }
}
