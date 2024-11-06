namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateRepairDate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Repairs", "DateCreated", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Repairs", "DateCreated");
        }
    }
}
