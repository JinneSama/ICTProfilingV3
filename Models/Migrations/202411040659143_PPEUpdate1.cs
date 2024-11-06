namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PPEUpdate1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PPEs", "IssuedToId", c => c.Long());
            DropColumn("dbo.PPEs", "IssedToId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PPEs", "IssedToId", c => c.Long());
            DropColumn("dbo.PPEs", "IssuedToId");
        }
    }
}
