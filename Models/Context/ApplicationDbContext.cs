using System.Data.Entity;
using System.Security.Permissions;
using Models.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using Models.Migrations;

namespace EntityManager.Context
{
    public class ApplicationDbContext : IdentityDbContext<Users>
    {
        public ApplicationDbContext() : base("DefaultConnection")
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Deliveries> Deliveries { get; set; }   
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<TicketRequest> TicketRequests { get; set; }
        public DbSet<EquipmentSpecs> EquipmentSpecs { get; set; }
        public DbSet<EquipmentSpecsDetails> EquipmentSpecsDetails { get; set; }
        public DbSet<Equipment> Equipments { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Model> Models { get; set; }
        public DbSet<ActionsDropdowns> ActionsDropdowns { get; set; }
        public DbSet<Actions> Actions { get; set; }
        public DbSet<ActionTaken> ActionTaken { get; set; }
        public DbSet<TechSpecsBasis> TechSpecsBasis { get; set; }
        public DbSet<TechSpecs> TechSpecs { get; set; }
        public DbSet<TechSpecsICTSpecs> TechSpecsICTSpecs { get; set; }
        public DbSet<TechSpecsICTSpecsDetails> TechSpecsICTSpecsDetails { get; set; }
        public DbSet<PPEs> PPEs { get; set; }
        public DbSet<PPEsSpecs> PPEsSpecs { get; set; }
        public DbSet<PPEsSpecsDetails> PPEsSpecsDetails { get; set; }
        public DbSet<Repairs> Repairs { get; set; }
        public DbSet<CustomerActionSheet> CustomerActionSheets { get; set; }
        public DbSet<PurchaseRequest> PurchaseRequests { get; set; }
        public DbSet<StandardPRSpecs> StandardPRSpecs { get; set; }
        public DbSet<StandardPRSpecsDetails> standardPRSpecsDetails { get; set; }
        public DbSet<PRStandardPRSpecs> PRStandardPRSpecs { get; set; }
        public DbSet<PGNAccounts> PGNAccounts { get; set; }
        public DbSet<PGNDocuments> PGNDocuments { get; set; }
        public DbSet<PGNMacAddresses> PGNMacAddresses { get; set; }
        public DbSet<PGNNonEmployee> PGNNonEmployees { get; set; }
        public DbSet<PGNRequests> PGNRequests { get; set; }
        public DbSet<PGNGroupOffices> PGNGroupOffices { get; set; }
    }
}