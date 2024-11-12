namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PRAddReqById1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PurchaseRequests", "ReqById", c => c.Long());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PurchaseRequests", "ReqById");
        }
    }
}
