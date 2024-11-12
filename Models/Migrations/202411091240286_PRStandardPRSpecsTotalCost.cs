namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PRStandardPRSpecsTotalCost : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PRStandardPRSpecs", "TotalCost", c => c.Long());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PRStandardPRSpecs", "TotalCost");
        }
    }
}
