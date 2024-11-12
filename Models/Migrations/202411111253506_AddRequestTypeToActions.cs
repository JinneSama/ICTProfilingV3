namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRequestTypeToActions : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Actions", "RequestType", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Actions", "RequestType");
        }
    }
}
