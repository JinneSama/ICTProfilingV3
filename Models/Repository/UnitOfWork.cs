using EntityManager.Context;
using Models.Entities;
using System;
using System.Threading.Tasks;

namespace Models.Repository
{
    public class UnitOfWork : IUnitOfWork , IDisposable
    {
        private readonly ApplicationDbContext _context;
        private bool disposed;

        private IGenericRepository<Supplier> _SupplierRepo;
        public IGenericRepository<Supplier> SupplierRepo
        {
            get
            {
                if (_SupplierRepo == null)
                    _SupplierRepo = new GenericRepository<Supplier>(_context);
                return _SupplierRepo;
            }
            set => _SupplierRepo = value;
        }

        private IGenericRepository<Equipment> _EquipmentRepo;
        public IGenericRepository<Equipment> EquipmentRepo
        {
            get
            {
                if (_EquipmentRepo == null)
                    _EquipmentRepo = new GenericRepository<Equipment>(_context);
                return _EquipmentRepo;
            }
            set => _EquipmentRepo = value;
        }

        private IGenericRepository<EquipmentSpecs> _EquipmentSpecsRepo;
        public IGenericRepository<EquipmentSpecs> EquipmentSpecsRepo
        {
            get
            {
                if (_EquipmentSpecsRepo == null)
                    _EquipmentSpecsRepo = new GenericRepository<EquipmentSpecs>(_context);
                return _EquipmentSpecsRepo;
            }
            set => _EquipmentSpecsRepo = value;
        }

        private IGenericRepository<EquipmentSpecsDetails> _EquipmentSpecsDetailsRepo;
        public IGenericRepository<EquipmentSpecsDetails> EquipmentSpecsDetailsRepo
        {
            get
            {
                if (_EquipmentSpecsDetailsRepo == null)
                    _EquipmentSpecsDetailsRepo = new GenericRepository<EquipmentSpecsDetails>(_context);
                return _EquipmentSpecsDetailsRepo;
            }
            set => _EquipmentSpecsDetailsRepo = value;
        }

        private IGenericRepository<Brand> _BrandRepo;
        public IGenericRepository<Brand> BrandRepo
        {
            get
            {
                if (_BrandRepo == null)
                    _BrandRepo = new GenericRepository<Brand>(_context);
                return _BrandRepo;
            }
            set => _BrandRepo = value;
        }

        private IGenericRepository<Model> _ModelRepo;
        public IGenericRepository<Model> ModelRepo
        {
            get
            {
                if (_ModelRepo == null)
                    _ModelRepo = new GenericRepository<Model>(_context);
                return _ModelRepo;
            }
            set => _ModelRepo = value;
        }

        private IGenericRepository<Deliveries> _DeliveriesRepo;
        public IGenericRepository<Deliveries> DeliveriesRepo
        {
            get
            {
                if (_DeliveriesRepo == null)
                    _DeliveriesRepo = new GenericRepository<Deliveries>(_context);
                return _DeliveriesRepo;
            }
            set => _DeliveriesRepo = value;
        }

        private IGenericRepository<DeliveriesSpecs> _DeliveriesSpecsRepo;
        public IGenericRepository<DeliveriesSpecs> DeliveriesSpecsRepo
        {
            get
            {
                if (_DeliveriesSpecsRepo == null)
                    _DeliveriesSpecsRepo = new GenericRepository<DeliveriesSpecs>(_context);
                return _DeliveriesSpecsRepo;
            }
            set => _DeliveriesSpecsRepo = value;
        }

        private IGenericRepository<DeliveriesSpecsDetails> _DeliveriesSpecsDetailsRepo;
        public IGenericRepository<DeliveriesSpecsDetails> DeliveriesSpecsDetailsRepo
        {
            get
            {
                if (_DeliveriesSpecsDetailsRepo == null)
                    _DeliveriesSpecsDetailsRepo = new GenericRepository<DeliveriesSpecsDetails>(_context);
                return _DeliveriesSpecsDetailsRepo;
            }
            set => _DeliveriesSpecsDetailsRepo = value;
        }

        private IGenericRepository<TicketRequest> _TicketRequestRepo;
        public IGenericRepository<TicketRequest> TicketRequestRepo
        {
            get
            {
                if (_TicketRequestRepo == null)
                    _TicketRequestRepo = new GenericRepository<TicketRequest>(_context);
                return _TicketRequestRepo;
            }
            set => _TicketRequestRepo = value;
        }

        private IGenericRepository<ActionsDropdowns> _ActionsDropdownsRepo;
        public IGenericRepository<ActionsDropdowns> ActionsDropdownsRepo
        {
            get
            {
                if (_ActionsDropdownsRepo == null)
                    _ActionsDropdownsRepo = new GenericRepository<ActionsDropdowns>(_context);
                return _ActionsDropdownsRepo;
            }
            set => _ActionsDropdownsRepo = value;
        }

        private IGenericRepository<Actions> _ActionsRepo;
        public IGenericRepository<Actions> ActionsRepo
        {
            get
            {
                if (_ActionsRepo == null)
                    _ActionsRepo = new GenericRepository<Actions>(_context);
                return _ActionsRepo;
            }
            set => _ActionsRepo = value;
        }

        private IGenericRepository<ActionTaken> _ActionTakenRepo;
        public IGenericRepository<ActionTaken> ActionTakenRepo
        {
            get
            {
                if (_ActionTakenRepo == null)
                    _ActionTakenRepo = new GenericRepository<ActionTaken>(_context);
                return _ActionTakenRepo;
            }
            set => _ActionTakenRepo = value;
        }

        private IGenericRepository<Users> _UsersRepo;
        public IGenericRepository<Users> UsersRepo
        {
            get
            {
                if (_UsersRepo == null)
                    _UsersRepo = new GenericRepository<Users>(_context);
                return _UsersRepo;
            }
            set => _UsersRepo = value;
        }

        private IGenericRepository<TechSpecsBasis> _TechSpecsBasisRepo;
        public IGenericRepository<TechSpecsBasis> TechSpecsBasisRepo
        {
            get
            {
                if (_TechSpecsBasisRepo == null)
                    _TechSpecsBasisRepo = new GenericRepository<TechSpecsBasis>(_context);
                return _TechSpecsBasisRepo;
            }
            set => _TechSpecsBasisRepo = value;
        }

        private IGenericRepository<TechSpecs> _TechSpecsRepo;
        public IGenericRepository<TechSpecs> TechSpecsRepo
        {
            get
            {
                if (_TechSpecsRepo == null)
                    _TechSpecsRepo = new GenericRepository<TechSpecs>(_context);
                return _TechSpecsRepo;
            }
            set => _TechSpecsRepo = value;
        }

        private IGenericRepository<TechSpecsICTSpecs> _TechSpecsICTSpecsRepo;
        public IGenericRepository<TechSpecsICTSpecs> TechSpecsICTSpecsRepo
        {
            get
            {
                if (_TechSpecsICTSpecsRepo == null)
                    _TechSpecsICTSpecsRepo = new GenericRepository<TechSpecsICTSpecs>(_context);
                return _TechSpecsICTSpecsRepo;
            }
            set => _TechSpecsICTSpecsRepo = value;
        }

        private IGenericRepository<TechSpecsICTSpecsDetails> _TechSpecsICTSpecsDetailsRepo;
        public IGenericRepository<TechSpecsICTSpecsDetails> TechSpecsICTSpecsDetailsRepo
        {
            get
            {
                if (_TechSpecsICTSpecsDetailsRepo == null)
                    _TechSpecsICTSpecsDetailsRepo = new GenericRepository<TechSpecsICTSpecsDetails>(_context);
                return _TechSpecsICTSpecsDetailsRepo;
            }
            set => _TechSpecsICTSpecsDetailsRepo = value;
        }

        private IGenericRepository<PPEs> _PPesRepo;
        public IGenericRepository<PPEs> PPesRepo
        {
            get
            {
                if (_PPesRepo == null)
                    _PPesRepo = new GenericRepository<PPEs>(_context);
                return _PPesRepo;
            }
            set => _PPesRepo = value;
        }

        private IGenericRepository<PPEsSpecs> _PPEsSpecsRepo;
        public IGenericRepository<PPEsSpecs> PPEsSpecsRepo
        {
            get
            {
                if (_PPEsSpecsRepo == null)
                    _PPEsSpecsRepo = new GenericRepository<PPEsSpecs>(_context);
                return _PPEsSpecsRepo;
            }
            set => _PPEsSpecsRepo = value;
        }

        private IGenericRepository<PPEsSpecsDetails> _PPEsSpecsDetailsRepo;
        public IGenericRepository<PPEsSpecsDetails> PPEsSpecsDetailsRepo
        {
            get
            {
                if (_PPEsSpecsDetailsRepo == null)
                    _PPEsSpecsDetailsRepo = new GenericRepository<PPEsSpecsDetails>(_context);
                return _PPEsSpecsDetailsRepo;
            }
            set => _PPEsSpecsDetailsRepo = value;
        }

        private IGenericRepository<Repairs> _RepairsRepo;
        public IGenericRepository<Repairs> RepairsRepo
        {
            get
            {
                if (_RepairsRepo == null)
                    _RepairsRepo = new GenericRepository<Repairs>(_context);
                return _RepairsRepo;
            }
            set => _RepairsRepo = value;
        }

        private IGenericRepository<CustomerActionSheet> _CustomerActionSheetRepo;
        public IGenericRepository<CustomerActionSheet> CustomerActionSheetRepo
        {
            get
            {
                if (_CustomerActionSheetRepo == null)
                    _CustomerActionSheetRepo = new GenericRepository<CustomerActionSheet>(_context);
                return _CustomerActionSheetRepo;
            }
            set => _CustomerActionSheetRepo = value;
        }

        private IGenericRepository<PurchaseRequest> _PurchaseRequestRepo;
        public IGenericRepository<PurchaseRequest> PurchaseRequestRepo
        {
            get
            {
                if (_PurchaseRequestRepo == null)
                    _PurchaseRequestRepo = new GenericRepository<PurchaseRequest>(_context);
                return _PurchaseRequestRepo;
            }
            set => _PurchaseRequestRepo = value;
        }

        private IGenericRepository<StandardPRSpecs> _StandardPRSpecsRepo;
        public IGenericRepository<StandardPRSpecs> StandardPRSpecsRepo
        {
            get
            {
                if (_StandardPRSpecsRepo == null)
                    _StandardPRSpecsRepo = new GenericRepository<StandardPRSpecs>(_context);
                return _StandardPRSpecsRepo;
            }
            set => _StandardPRSpecsRepo = value;
        }

        private IGenericRepository<StandardPRSpecsDetails> _StandardPRSpecsDetailsRepo;
        public IGenericRepository<StandardPRSpecsDetails> StandardPRSpecsDetailsRepo
        {
            get
            {
                if (_StandardPRSpecsDetailsRepo == null)
                    _StandardPRSpecsDetailsRepo = new GenericRepository<StandardPRSpecsDetails>(_context);
                return _StandardPRSpecsDetailsRepo;
            }
            set => _StandardPRSpecsDetailsRepo = value;
        }

        private IGenericRepository<PRStandardPRSpecs> _PRStandardPRSpecsRepo;
        public IGenericRepository<PRStandardPRSpecs> PRStandardPRSpecsRepo
        {
            get
            {
                if (_PRStandardPRSpecsRepo == null)
                    _PRStandardPRSpecsRepo = new GenericRepository<PRStandardPRSpecs>(_context);
                return _PRStandardPRSpecsRepo;
            }
            set => _PRStandardPRSpecsRepo = value;
        }

        private IGenericRepository<ITStaff> _ITStaffRepo;
        public IGenericRepository<ITStaff> ITStaffRepo
        {
            get
            {
                if (_ITStaffRepo == null)
                    _ITStaffRepo = new GenericRepository<ITStaff>(_context);
                return _ITStaffRepo;
            }
            set => _ITStaffRepo = value;
        }

        private IGenericRepository<TicketRequestStatus> _TicketRequestStatusRepo;
        public IGenericRepository<TicketRequestStatus> TicketRequestStatusRepo
        {
            get
            {
                if (_TicketRequestStatusRepo == null)
                    _TicketRequestStatusRepo = new GenericRepository<TicketRequestStatus>(_context);
                return _TicketRequestStatusRepo;
            }
            set => _TicketRequestStatusRepo = value;
        }

        private IGenericRepository<PGNAccounts> _PGNAccountsRepo;
        public IGenericRepository<PGNAccounts> PGNAccountsRepo
        {
            get
            {
                if (_PGNAccountsRepo == null)
                    _PGNAccountsRepo = new GenericRepository<PGNAccounts>(_context);
                return _PGNAccountsRepo;
            }
            set => _PGNAccountsRepo = value;
        }

        private IGenericRepository<PGNDocuments> _PGNDocumentsRepo;
        public IGenericRepository<PGNDocuments> PGNDocumentsRepo
        {
            get
            {
                if (_PGNDocumentsRepo == null)
                    _PGNDocumentsRepo = new GenericRepository<PGNDocuments>(_context);
                return _PGNDocumentsRepo;
            }
            set => _PGNDocumentsRepo = value;
        }

        private IGenericRepository<PGNMacAddresses> _PGNMacAddressesRepo;
        public IGenericRepository<PGNMacAddresses> PGNMacAddressesRepo
        {
            get
            {
                if (_PGNMacAddressesRepo == null)
                    _PGNMacAddressesRepo = new GenericRepository<PGNMacAddresses>(_context);
                return _PGNMacAddressesRepo;
            }
            set => _PGNMacAddressesRepo = value;
        }

        private IGenericRepository<PGNNonEmployee> _PGNNonEmployeeRepo;
        public IGenericRepository<PGNNonEmployee> PGNNonEmployeeRepo
        {
            get
            {
                if (_PGNNonEmployeeRepo == null)
                    _PGNNonEmployeeRepo = new GenericRepository<PGNNonEmployee>(_context);
                return _PGNNonEmployeeRepo;
            }
            set => _PGNNonEmployeeRepo = value;
        }

        private IGenericRepository<PGNRequests> _PGNRequestsRepo;
        public IGenericRepository<PGNRequests> PGNRequestsRepo
        {
            get
            {
                if (_PGNRequestsRepo == null)
                    _PGNRequestsRepo = new GenericRepository<PGNRequests>(_context);
                return _PGNRequestsRepo;
            }
            set => _PGNRequestsRepo = value;
        }

        private IGenericRepository<PGNGroupOffices> _PGNGroupOfficesRepo;
        public IGenericRepository<PGNGroupOffices> PGNGroupOfficesRepo
        {
            get
            {
                if (_PGNGroupOfficesRepo == null)
                    _PGNGroupOfficesRepo = new GenericRepository<PGNGroupOffices>(_context);
                return _PGNGroupOfficesRepo;
            }
            set => _PGNGroupOfficesRepo = value;
        }

        private IGenericRepository<ComparisonReport> _ComparisonReportRepo;
        public IGenericRepository<ComparisonReport> ComparisonReportRepo
        {
            get
            {
                if (_ComparisonReportRepo == null)
                    _ComparisonReportRepo = new GenericRepository<ComparisonReport>(_context);
                return _ComparisonReportRepo;
            }
            set => _ComparisonReportRepo = value;
        }
        private IGenericRepository<ComparisonReportSpecs> _ComparisonReportSpecsRepo;
        public IGenericRepository<ComparisonReportSpecs> ComparisonReportSpecsRepo
        {
            get
            {
                if (_ComparisonReportSpecsRepo == null)
                    _ComparisonReportSpecsRepo = new GenericRepository<ComparisonReportSpecs>(_context);
                return _ComparisonReportSpecsRepo;
            }
            set => _ComparisonReportSpecsRepo = value;
        }
        private IGenericRepository<ComparisonReportSpecsDetails> _ComparisonReportSpecsDetailsRepo;
        public IGenericRepository<ComparisonReportSpecsDetails> ComparisonReportSpecsDetailsRepo
        {
            get
            {
                if (_ComparisonReportSpecsDetailsRepo == null)
                    _ComparisonReportSpecsDetailsRepo = new GenericRepository<ComparisonReportSpecsDetails>(_context);
                return _ComparisonReportSpecsDetailsRepo;
            }
            set => _ComparisonReportSpecsDetailsRepo = value;
        }

        private IGenericRepository<ChangeLogs> _ChangeLogsRepo;
        public IGenericRepository<ChangeLogs> ChangeLogsRepo
        {
            get
            {
                if (_ChangeLogsRepo == null)
                    _ChangeLogsRepo = new GenericRepository<ChangeLogs>(_context);
                return _ChangeLogsRepo;
            }
            set => _ChangeLogsRepo = value;
        }

        private IGenericRepository<MOAccounts> _MOAccountRepo;
        public IGenericRepository<MOAccounts> MOAccountRepo
        {
            get
            {
                if (_MOAccountRepo == null)
                    _MOAccountRepo = new GenericRepository<MOAccounts>(_context);
                return _MOAccountRepo;
            }
            set => _MOAccountRepo = value;
        }

        private IGenericRepository<MOAccountUsers> _MOAccountUserRepo;
        public IGenericRepository<MOAccountUsers> MOAccountUserRepo
        {
            get
            {
                if (_MOAccountUserRepo == null)
                    _MOAccountUserRepo = new GenericRepository<MOAccountUsers>(_context);
                return _MOAccountUserRepo;
            }
            set => _MOAccountUserRepo = value;
        }

        private IGenericRepository<ActionDocuments> _ActionDocumentsRepo;
        public IGenericRepository<ActionDocuments> ActionDocumentsRepo
        {
            get
            {
                if (_ActionDocumentsRepo == null)
                    _ActionDocumentsRepo = new GenericRepository<ActionDocuments>(_context);
                return _ActionDocumentsRepo;
            }
            set => _ActionDocumentsRepo = value;
        }

        private IGenericRepository<RecordsRequestStatus> _RecordsRequestStatusRepo;
        public IGenericRepository<RecordsRequestStatus> RecordsRequestStatus
        {
            get
            {
                if (_RecordsRequestStatusRepo == null)
                    _RecordsRequestStatusRepo = new GenericRepository<RecordsRequestStatus>(_context);
                return _RecordsRequestStatusRepo;
            }
            set => _RecordsRequestStatusRepo = value;
        }

        private IGenericRepository<EvaluationSheet> _EvaluationSheetRepo;
        public IGenericRepository<EvaluationSheet> EvaluationSheetRepo
        {
            get
            {
                if (_EvaluationSheetRepo == null)
                    _EvaluationSheetRepo = new GenericRepository<EvaluationSheet>(_context);
                return _EvaluationSheetRepo;
            }
            set => _EvaluationSheetRepo = value;
        }

        private IGenericRepository<EvaluationSheetDocument> _EvaluationSheetDocumentRepo;
        public IGenericRepository<EvaluationSheetDocument> EvaluationSheetDocumentRepo
        {
            get
            {
                if (_EvaluationSheetDocumentRepo == null)
                    _EvaluationSheetDocumentRepo = new GenericRepository<EvaluationSheetDocument>(_context);
                return _EvaluationSheetDocumentRepo;
            }
            set => _EvaluationSheetDocumentRepo = value;
        }

        private IGenericRepository<TechSpecsBasisDetails> _TechSpecsBasisDetailsRepo;
        public IGenericRepository<TechSpecsBasisDetails> TechSpecsBasisDetailsRepo
        {
            get
            {
                if (_TechSpecsBasisDetailsRepo == null)
                    _TechSpecsBasisDetailsRepo = new GenericRepository<TechSpecsBasisDetails>(_context);
                return _TechSpecsBasisDetailsRepo;
            }
            set => _TechSpecsBasisDetailsRepo = value;
        }

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

        public void ExecuteCommand(string command)
        {
            _context.Database.ExecuteSqlCommand(command);
        }
    }
}
