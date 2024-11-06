namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TSDeliveriesUpdate1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Deliveries", "RequestedById", c => c.Long(nullable: false));
            AddColumn("dbo.Deliveries", "DeliveredById", c => c.Long(nullable: false));
            AddColumn("dbo.Deliveries", "Gender", c => c.Int(nullable: false));
            AddColumn("dbo.Deliveries", "ContactNo", c => c.String());
            DropColumn("dbo.Deliveries", "ReqById");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Deliveries", "ReqById", c => c.Long(nullable: false));
            DropColumn("dbo.Deliveries", "ContactNo");
            DropColumn("dbo.Deliveries", "Gender");
            DropColumn("dbo.Deliveries", "DeliveredById");
            DropColumn("dbo.Deliveries", "RequestedById");
        }
    }
}
