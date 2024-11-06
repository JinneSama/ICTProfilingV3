namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TSDeliveriesUpdate : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Deliveries", "OfficeId");
            DropColumn("dbo.Deliveries", "ReqOffAcr");
            DropColumn("dbo.TechSpecs", "OfficeId");
            DropColumn("dbo.TechSpecs", "ReqOffAcr");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TechSpecs", "ReqOffAcr", c => c.String());
            AddColumn("dbo.TechSpecs", "OfficeId", c => c.Long(nullable: false));
            AddColumn("dbo.Deliveries", "ReqOffAcr", c => c.String());
            AddColumn("dbo.Deliveries", "OfficeId", c => c.Long(nullable: false));
        }
    }
}
