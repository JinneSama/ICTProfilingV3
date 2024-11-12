namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDeletedToProcesses : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CustomerActionSheets", "IsDeleted", c => c.Boolean());
            AddColumn("dbo.TicketRequests", "IsDeleted", c => c.Boolean());
            AddColumn("dbo.Deliveries", "IsDeleted", c => c.Boolean());
            AddColumn("dbo.PurchaseRequests", "IsDeleted", c => c.Boolean());
            AddColumn("dbo.TechSpecs", "IsDeleted", c => c.Boolean());
            AddColumn("dbo.Repairs", "IsDeleted", c => c.Boolean());
            AddColumn("dbo.PPEs", "IsDeleted", c => c.Boolean());
            DropColumn("dbo.TicketRequests", "Deleted");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TicketRequests", "Deleted", c => c.Boolean(nullable: false));
            DropColumn("dbo.PPEs", "IsDeleted");
            DropColumn("dbo.Repairs", "IsDeleted");
            DropColumn("dbo.TechSpecs", "IsDeleted");
            DropColumn("dbo.PurchaseRequests", "IsDeleted");
            DropColumn("dbo.Deliveries", "IsDeleted");
            DropColumn("dbo.TicketRequests", "IsDeleted");
            DropColumn("dbo.CustomerActionSheets", "IsDeleted");
        }
    }
}
