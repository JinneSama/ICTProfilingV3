namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RepairNullablePPE : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Repairs", "PPEsId", "dbo.PPEs");
            DropIndex("dbo.Repairs", new[] { "PPEsId" });
            AlterColumn("dbo.Repairs", "PPEsId", c => c.Int());
            CreateIndex("dbo.Repairs", "PPEsId");
            AddForeignKey("dbo.Repairs", "PPEsId", "dbo.PPEs", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Repairs", "PPEsId", "dbo.PPEs");
            DropIndex("dbo.Repairs", new[] { "PPEsId" });
            AlterColumn("dbo.Repairs", "PPEsId", c => c.Int(nullable: false));
            CreateIndex("dbo.Repairs", "PPEsId");
            AddForeignKey("dbo.Repairs", "PPEsId", "dbo.PPEs", "Id", cascadeDelete: true);
        }
    }
}
