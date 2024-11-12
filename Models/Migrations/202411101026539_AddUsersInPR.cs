namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUsersInPR : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PurchaseRequests", "CreatedById", c => c.String(maxLength: 128));
            CreateIndex("dbo.PurchaseRequests", "CreatedById");
            AddForeignKey("dbo.PurchaseRequests", "CreatedById", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PurchaseRequests", "CreatedById", "dbo.AspNetUsers");
            DropIndex("dbo.PurchaseRequests", new[] { "CreatedById" });
            DropColumn("dbo.PurchaseRequests", "CreatedById");
        }
    }
}
