namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TechSpecsActions : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Actions", "TechSpecsId", c => c.Int());
            CreateIndex("dbo.Actions", "TechSpecsId");
            AddForeignKey("dbo.Actions", "TechSpecsId", "dbo.TechSpecs", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Actions", "TechSpecsId", "dbo.TechSpecs");
            DropIndex("dbo.Actions", new[] { "TechSpecsId" });
            DropColumn("dbo.Actions", "TechSpecsId");
        }
    }
}
