namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddImagePathToITStaff : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ITStaffs", "ImagePath", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ITStaffs", "ImagePath");
        }
    }
}
