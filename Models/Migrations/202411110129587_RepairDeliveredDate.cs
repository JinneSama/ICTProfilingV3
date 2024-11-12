namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RepairDeliveredDate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Repairs", "DateDelivered", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Repairs", "DateDelivered");
        }
    }
}
