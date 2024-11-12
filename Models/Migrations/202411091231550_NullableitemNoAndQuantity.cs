namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NullableitemNoAndQuantity : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PRStandardPRSpecs", "ItemNo", c => c.Int());
            AlterColumn("dbo.PRStandardPRSpecs", "Quantity", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PRStandardPRSpecs", "Quantity", c => c.Int(nullable: false));
            AlterColumn("dbo.PRStandardPRSpecs", "ItemNo", c => c.Int(nullable: false));
        }
    }
}
