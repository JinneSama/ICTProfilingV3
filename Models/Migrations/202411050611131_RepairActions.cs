namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RepairActions : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Actions", "RepairId", c => c.Int());
            AddColumn("dbo.Repairs", "PPESpecsId", c => c.Int());
            CreateIndex("dbo.Actions", "RepairId");
            CreateIndex("dbo.Repairs", "PPESpecsId");
            AddForeignKey("dbo.Actions", "RepairId", "dbo.Repairs", "Id");
            AddForeignKey("dbo.Repairs", "PPESpecsId", "dbo.PPEsSpecs", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Repairs", "PPESpecsId", "dbo.PPEsSpecs");
            DropForeignKey("dbo.Actions", "RepairId", "dbo.Repairs");
            DropIndex("dbo.Repairs", new[] { "PPESpecsId" });
            DropIndex("dbo.Actions", new[] { "RepairId" });
            DropColumn("dbo.Repairs", "PPESpecsId");
            DropColumn("dbo.Actions", "RepairId");
        }
    }
}
