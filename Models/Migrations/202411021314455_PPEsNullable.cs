namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PPEsNullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PPEs", "IssedToId", c => c.Long());
            AlterColumn("dbo.PPEs", "ChiefId", c => c.Long());
            AlterColumn("dbo.PPEs", "Gender", c => c.Int());
            AlterColumn("dbo.PPEs", "Status", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PPEs", "Status", c => c.Int(nullable: false));
            AlterColumn("dbo.PPEs", "Gender", c => c.Int(nullable: false));
            AlterColumn("dbo.PPEs", "ChiefId", c => c.Long(nullable: false));
            AlterColumn("dbo.PPEs", "IssedToId", c => c.Long(nullable: false));
        }
    }
}
