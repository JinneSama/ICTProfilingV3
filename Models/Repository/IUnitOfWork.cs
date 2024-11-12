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
        void Save();
        Task SaveChangesAsync();
    }
}
