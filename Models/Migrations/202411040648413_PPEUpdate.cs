namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PPEUpdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PPEs", "Quantity", c => c.Int(nullable: false));
            AddColumn("dbo.PPEs", "Unit", c => c.Int(nullable: false));
            AddColumn("dbo.PPEs", "UnitValue", c => c.Long());
            AddColumn("dbo.PPEs", "TotalValue", c => c.Long());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PPEs", "TotalValue");
            DropColumn("dbo.PPEs", "UnitValue");
            DropColumn("dbo.PPEs", "Unit");
            DropColumn("dbo.PPEs", "Quantity");
        }
    }
}
