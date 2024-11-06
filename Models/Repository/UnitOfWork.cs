using EntityManager.Context;
using Models.Entities;
using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Runtime.Remoting.Contexts;
using System.Threading.Tasks;

namespace Models.Repository
{
    public class UnitOfWork : IUnitOfWork , IDisposable
    {
        private readonly ApplicationDbContext _context;
        private bool disposed;

        public IGenericRepository<Supplier> SupplierRepo => new GenericRepository<Supplier>(_context);
        public IGenericRepository<Equipment> EquipmentRepo => new GenericRepository<Equipment>(_context);
        public IGenericRepository<EquipmentSpecs> EquipmentSpecsRepo => new GenericRepository<EquipmentSpecs>(_context);
        public IGenericRepository<EquipmentSpecsDetails> EquipmentSpecsDetailsRepo => new GenericRepository<EquipmentSpecsDetails>(_context);
        public IGenericRepository<Brand> BrandRepo => new GenericRepository<Brand>(_context);
        public IGenericRepository<Model> ModelRepo => new GenericRepository<Model>(_context);
        public IGenericRepository<Deliveries> DeliveriesRepo => new GenericRepository<Deliveries>(_context);
        public IGenericRepository<DeliveriesSpecs> DeliveriesSpecsRepo => new GenericRepository<DeliveriesSpecs>(_context);
        public IGenericRepository<DeliveriesSpecsDetails> DeliveriesSpecsDetailsRepo => new GenericRepository<DeliveriesSpecsDetails>(_context);
        public IGenericRepository<TicketRequest> TicketRequestRepo => new GenericRepository<TicketRequest>(_context);
        public IGenericRepository<ActionsDropdowns> ActionsDropdownsRepo => new GenericRepository<ActionsDropdowns>(_context);  
        public IGenericRepository<Actions> ActionsRepo => new GenericRepository<Actions>(_context);
        public IGenericRepository<ActionTaken> ActionTakenRepo => new GenericRepository<ActionTaken>(_context);
        public IGenericRepository<Users> UsersRepo => new GenericRepository<Users>(_context);
        public IGenericRepository<TechSpecsBasis> TechSpecsBasisRepo => new GenericRepository<TechSpecsBasis>(_context);
        public IGenericRepository<TechSpecs> TechSpecsRepo => new GenericRepository<TechSpecs>(_context);
        public IGenericRepository<TechSpecsICTSpecs> TechSpecsICTSpecsRepo => new GenericRepository<TechSpecsICTSpecs>(_context);
        public IGenericRepository<TechSpecsICTSpecsDetails> TechSpecsICTSpecsDetailsRepo => new GenericRepository<TechSpecsICTSpecsDetails>(_context);
        public IGenericRepository<PPEs> PPesRepo => new GenericRepository<PPEs>(_context);
        public IGenericRepository<PPEsSpecs> PPEsSpecsRepo => new GenericRepository<PPEsSpecs>(_context);
        public IGenericRepository<PPEsSpecsDetails> PPEsSpecsDetailsRepo => new GenericRepository<PPEsSpecsDetails>(_context);
        public IGenericRepository<Repairs> RepairsRepo => new GenericRepository<Repairs>(_context);

        public UnitOfWork()
        {
            _context = ApplicationDbContext.Create();
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
                if (disposing)
                    _context.Dispose();
            disposed = true;
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
