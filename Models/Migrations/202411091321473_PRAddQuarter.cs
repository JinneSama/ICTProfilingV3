namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PRAddQuarter : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PurchaseRequests", "Quarter", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PurchaseRequests", "Quarter");
        }
    }
}
