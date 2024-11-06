namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Actions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ActionTaken = c.String(),
                        DateCreated = c.DateTime(),
                        ActionDate = c.DateTime(),
                        Remarks = c.String(),
                        IsSend = c.Boolean(),
                        ProgramId = c.Int(),
                        MainActId = c.Int(),
                        ActivityId = c.Int(),
                        SubActivityId = c.Int(),
                        DeliveriesId = c.Int(),
                        CreatedById = c.String(maxLength: 128),
                        ActionsDropdowns_Id = c.Int(),
                        ActionsDropdowns_Id1 = c.Int(),
                        ActionsDropdowns_Id2 = c.Int(),
                        ActionsDropdowns_Id3 = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ActionsDropdowns", t => t.ActionsDropdowns_Id)
                .ForeignKey("dbo.ActionsDropdowns", t => t.ActionsDropdowns_Id1)
                .ForeignKey("dbo.ActionsDropdowns", t => t.ActionsDropdowns_Id2)
                .ForeignKey("dbo.ActionsDropdowns", t => t.ActionsDropdowns_Id3)
                .ForeignKey("dbo.ActionsDropdowns", t => t.ActivityId)
                .ForeignKey("dbo.Deliveries", t => t.DeliveriesId)
                .ForeignKey("dbo.AspNetUsers", t => t.CreatedById)
                .ForeignKey("dbo.ActionsDropdowns", t => t.MainActId)
                .ForeignKey("dbo.ActionsDropdowns", t => t.ProgramId)
                .ForeignKey("dbo.ActionsDropdowns", t => t.SubActivityId)
                .Index(t => t.ProgramId)
                .Index(t => t.MainActId)
                .Index(t => t.ActivityId)
                .Index(t => t.SubActivityId)
                .Index(t => t.DeliveriesId)
                .Index(t => t.CreatedById)
                .Index(t => t.ActionsDropdowns_Id)
                .Index(t => t.ActionsDropdowns_Id1)
                .Index(t => t.ActionsDropdowns_Id2)
                .Index(t => t.ActionsDropdowns_Id3);
            
            CreateTable(
                "dbo.ActionsDropdowns",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ActionCategory = c.Int(nullable: false),
                        Value = c.String(),
                        ParentId = c.Int(nullable: false),
                        Order = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FullName = c.String(),
                        Position = c.String(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.ITStaffs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.TicketRequests",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DateCreated = c.DateTime(),
                        CreatedBy = c.String(maxLength: 128),
                        TicketStatus = c.Int(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                        RequestType = c.Int(nullable: false),
                        StaffId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.CreatedBy)
                .ForeignKey("dbo.ITStaffs", t => t.StaffId)
                .Index(t => t.CreatedBy)
                .Index(t => t.StaffId);
            
            CreateTable(
                "dbo.Deliveries",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        DeliveredDate = c.DateTime(),
                        DateRequested = c.DateTime(),
                        OfficeId = c.Long(nullable: false),
                        ReqOffAcr = c.String(),
                        ReqById = c.Long(nullable: false),
                        ReqByChiefId = c.Long(nullable: false),
                        PONo = c.String(),
                        ReceiptNo = c.String(),
                        SupplierId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Suppliers", t => t.SupplierId)
                .ForeignKey("dbo.TicketRequests", t => t.Id)
                .Index(t => t.Id)
                .Index(t => t.SupplierId);
            
            CreateTable(
                "dbo.DeliveriesSpecs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ItemNo = c.Int(nullable: false),
                        Description = c.String(),
                        Remarks = c.String(),
                        Quantity = c.Int(nullable: false),
                        Unit = c.Int(nullable: false),
                        UnitCost = c.Long(nullable: false),
                        TotalCost = c.Long(nullable: false),
                        ProposedBudget = c.Long(nullable: false),
                        Purpose = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        SerialNo = c.String(),
                        ModelId = c.Int(nullable: false),
                        DeliveriesId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Deliveries", t => t.DeliveriesId)
                .ForeignKey("dbo.Models", t => t.ModelId, cascadeDelete: true)
                .Index(t => t.ModelId)
                .Index(t => t.DeliveriesId);
            
            CreateTable(
                "dbo.DeliveriesSpecsDetails",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        DeliveriesSpecsId = c.Int(nullable: false),
                        ItemNo = c.Int(nullable: false),
                        Specs = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DeliveriesSpecs", t => t.DeliveriesSpecsId, cascadeDelete: true)
                .Index(t => t.DeliveriesSpecsId);
            
            CreateTable(
                "dbo.Models",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ModelName = c.String(),
                        BrandId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Brands", t => t.BrandId, cascadeDelete: true)
                .Index(t => t.BrandId);
            
            CreateTable(
                "dbo.Brands",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BrandName = c.String(),
                        EquipmenSpecsId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EquipmentSpecs", t => t.EquipmenSpecsId, cascadeDelete: true)
                .Index(t => t.EquipmenSpecsId);
            
            CreateTable(
                "dbo.EquipmentSpecs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        Remarks = c.String(),
                        EquipmentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Equipments", t => t.EquipmentId, cascadeDelete: true)
                .Index(t => t.EquipmentId);
            
            CreateTable(
                "dbo.Equipments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EquipmentName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EquipmentSpecsDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ItemNo = c.Int(nullable: false),
                        DetailSpecs = c.String(),
                        DetailDescription = c.String(),
                        EquipmentSpecsId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EquipmentSpecs", t => t.EquipmentSpecsId, cascadeDelete: true)
                .Index(t => t.EquipmentSpecsId);
            
            CreateTable(
                "dbo.TechSpecsBasis",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PriceRange = c.Double(nullable: false),
                        PriceDate = c.DateTime(),
                        URLBasis = c.String(),
                        Remarks = c.String(),
                        Available = c.Boolean(),
                        EquipmentSpecsId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EquipmentSpecs", t => t.EquipmentSpecsId, cascadeDelete: true)
                .Index(t => t.EquipmentSpecsId);
            
            CreateTable(
                "dbo.TechSpecsICTSpecs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ItemNo = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        Unit = c.Int(nullable: false),
                        UnitCost = c.Long(nullable: false),
                        TotalCost = c.Long(nullable: false),
                        Description = c.String(),
                        Purpose = c.String(),
                        TechSpecsId = c.Int(nullable: false),
                        EquipmentSpecsId = c.Int(nullable: false),
                        TechSpecsICTSpecsDetailsId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EquipmentSpecs", t => t.EquipmentSpecsId, cascadeDelete: true)
                .ForeignKey("dbo.TechSpecs", t => t.TechSpecsId, cascadeDelete: true)
                .Index(t => t.TechSpecsId)
                .Index(t => t.EquipmentSpecsId);
            
            CreateTable(
                "dbo.TechSpecs",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        DateRequested = c.DateTime(),
                        DateAccepted = c.DateTime(),
                        OfficeId = c.Long(nullable: false),
                        ReqOffAcr = c.String(),
                        ReqById = c.Long(nullable: false),
                        ReqByChiefId = c.Long(nullable: false),
                        ReqByGender = c.Int(nullable: false),
                        ContactNo = c.String(),
                        RequestBasedApprovedPR = c.Boolean(),
                        RequestBasedApprovedAPP = c.Boolean(),
                        RequestBasedApprovedAIP = c.Boolean(),
                        RequestBasedApprovedPPMP = c.Boolean(),
                        RequestBasedRequestLetter = c.Boolean(),
                        RequestBasedForReplacement = c.Boolean(),
                        PreparedById = c.String(maxLength: 128),
                        ReviewedById = c.String(maxLength: 128),
                        NotedById = c.String(maxLength: 128),
                        Users_Id = c.String(maxLength: 128),
                        Users_Id1 = c.String(maxLength: 128),
                        Users_Id2 = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.NotedById)
                .ForeignKey("dbo.AspNetUsers", t => t.PreparedById)
                .ForeignKey("dbo.AspNetUsers", t => t.ReviewedById)
                .ForeignKey("dbo.TicketRequests", t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Users_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Users_Id1)
                .ForeignKey("dbo.AspNetUsers", t => t.Users_Id2)
                .Index(t => t.Id)
                .Index(t => t.PreparedById)
                .Index(t => t.ReviewedById)
                .Index(t => t.NotedById)
                .Index(t => t.Users_Id)
                .Index(t => t.Users_Id1)
                .Index(t => t.Users_Id2);
            
            CreateTable(
                "dbo.TechSpecsICTSpecsDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ItemNo = c.Int(nullable: false),
                        Specs = c.String(),
                        Description = c.String(),
                        IsActive = c.Boolean(),
                        TechSpecsICTSpecsId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TechSpecsICTSpecs", t => t.TechSpecsICTSpecsId, cascadeDelete: true)
                .Index(t => t.TechSpecsICTSpecsId);
            
            CreateTable(
                "dbo.Suppliers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SupplierName = c.String(),
                        Address = c.String(),
                        ContactPerson = c.String(),
                        TelNumber = c.String(),
                        FaxNumber = c.String(),
                        PhoneNumber = c.String(),
                        Status = c.Boolean(),
                        IsDeleted = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.ActionTakens",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Action = c.String(),
                        DateAdded = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                        Description = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.RoleDesignations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Designation = c.Int(nullable: false),
                        RoleId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.UsersActions",
                c => new
                    {
                        Users_Id = c.String(nullable: false, maxLength: 128),
                        Actions_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Users_Id, t.Actions_Id })
                .ForeignKey("dbo.AspNetUsers", t => t.Users_Id, cascadeDelete: true)
                .ForeignKey("dbo.Actions", t => t.Actions_Id, cascadeDelete: true)
                .Index(t => t.Users_Id)
                .Index(t => t.Actions_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RoleDesignations", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Actions", "SubActivityId", "dbo.ActionsDropdowns");
            DropForeignKey("dbo.Actions", "ProgramId", "dbo.ActionsDropdowns");
            DropForeignKey("dbo.Actions", "MainActId", "dbo.ActionsDropdowns");
            DropForeignKey("dbo.Actions", "CreatedById", "dbo.AspNetUsers");
            DropForeignKey("dbo.TechSpecs", "Users_Id2", "dbo.AspNetUsers");
            DropForeignKey("dbo.TechSpecs", "Users_Id1", "dbo.AspNetUsers");
            DropForeignKey("dbo.TechSpecs", "Users_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ITStaffs", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.TicketRequests", "StaffId", "dbo.ITStaffs");
            DropForeignKey("dbo.Deliveries", "Id", "dbo.TicketRequests");
            DropForeignKey("dbo.Deliveries", "SupplierId", "dbo.Suppliers");
            DropForeignKey("dbo.DeliveriesSpecs", "ModelId", "dbo.Models");
            DropForeignKey("dbo.Models", "BrandId", "dbo.Brands");
            DropForeignKey("dbo.TechSpecsICTSpecsDetails", "TechSpecsICTSpecsId", "dbo.TechSpecsICTSpecs");
            DropForeignKey("dbo.TechSpecs", "Id", "dbo.TicketRequests");
            DropForeignKey("dbo.TechSpecsICTSpecs", "TechSpecsId", "dbo.TechSpecs");
            DropForeignKey("dbo.TechSpecs", "ReviewedById", "dbo.AspNetUsers");
            DropForeignKey("dbo.TechSpecs", "PreparedById", "dbo.AspNetUsers");
            DropForeignKey("dbo.TechSpecs", "NotedById", "dbo.AspNetUsers");
            DropForeignKey("dbo.TechSpecsICTSpecs", "EquipmentSpecsId", "dbo.EquipmentSpecs");
            DropForeignKey("dbo.TechSpecsBasis", "EquipmentSpecsId", "dbo.EquipmentSpecs");
            DropForeignKey("dbo.EquipmentSpecsDetails", "EquipmentSpecsId", "dbo.EquipmentSpecs");
            DropForeignKey("dbo.EquipmentSpecs", "EquipmentId", "dbo.Equipments");
            DropForeignKey("dbo.Brands", "EquipmenSpecsId", "dbo.EquipmentSpecs");
            DropForeignKey("dbo.DeliveriesSpecsDetails", "DeliveriesSpecsId", "dbo.DeliveriesSpecs");
            DropForeignKey("dbo.DeliveriesSpecs", "DeliveriesId", "dbo.Deliveries");
            DropForeignKey("dbo.Actions", "DeliveriesId", "dbo.Deliveries");
            DropForeignKey("dbo.TicketRequests", "CreatedBy", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.UsersActions", "Actions_Id", "dbo.Actions");
            DropForeignKey("dbo.UsersActions", "Users_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Actions", "ActivityId", "dbo.ActionsDropdowns");
            DropForeignKey("dbo.Actions", "ActionsDropdowns_Id3", "dbo.ActionsDropdowns");
            DropForeignKey("dbo.Actions", "ActionsDropdowns_Id2", "dbo.ActionsDropdowns");
            DropForeignKey("dbo.Actions", "ActionsDropdowns_Id1", "dbo.ActionsDropdowns");
            DropForeignKey("dbo.Actions", "ActionsDropdowns_Id", "dbo.ActionsDropdowns");
            DropIndex("dbo.UsersActions", new[] { "Actions_Id" });
            DropIndex("dbo.UsersActions", new[] { "Users_Id" });
            DropIndex("dbo.RoleDesignations", new[] { "RoleId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.TechSpecsICTSpecsDetails", new[] { "TechSpecsICTSpecsId" });
            DropIndex("dbo.TechSpecs", new[] { "Users_Id2" });
            DropIndex("dbo.TechSpecs", new[] { "Users_Id1" });
            DropIndex("dbo.TechSpecs", new[] { "Users_Id" });
            DropIndex("dbo.TechSpecs", new[] { "NotedById" });
            DropIndex("dbo.TechSpecs", new[] { "ReviewedById" });
            DropIndex("dbo.TechSpecs", new[] { "PreparedById" });
            DropIndex("dbo.TechSpecs", new[] { "Id" });
            DropIndex("dbo.TechSpecsICTSpecs", new[] { "EquipmentSpecsId" });
            DropIndex("dbo.TechSpecsICTSpecs", new[] { "TechSpecsId" });
            DropIndex("dbo.TechSpecsBasis", new[] { "EquipmentSpecsId" });
            DropIndex("dbo.EquipmentSpecsDetails", new[] { "EquipmentSpecsId" });
            DropIndex("dbo.EquipmentSpecs", new[] { "EquipmentId" });
            DropIndex("dbo.Brands", new[] { "EquipmenSpecsId" });
            DropIndex("dbo.Models", new[] { "BrandId" });
            DropIndex("dbo.DeliveriesSpecsDetails", new[] { "DeliveriesSpecsId" });
            DropIndex("dbo.DeliveriesSpecs", new[] { "DeliveriesId" });
            DropIndex("dbo.DeliveriesSpecs", new[] { "ModelId" });
            DropIndex("dbo.Deliveries", new[] { "SupplierId" });
            DropIndex("dbo.Deliveries", new[] { "Id" });
            DropIndex("dbo.TicketRequests", new[] { "StaffId" });
            DropIndex("dbo.TicketRequests", new[] { "CreatedBy" });
            DropIndex("dbo.ITStaffs", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Actions", new[] { "ActionsDropdowns_Id3" });
            DropIndex("dbo.Actions", new[] { "ActionsDropdowns_Id2" });
            DropIndex("dbo.Actions", new[] { "ActionsDropdowns_Id1" });
            DropIndex("dbo.Actions", new[] { "ActionsDropdowns_Id" });
            DropIndex("dbo.Actions", new[] { "CreatedById" });
            DropIndex("dbo.Actions", new[] { "DeliveriesId" });
            DropIndex("dbo.Actions", new[] { "SubActivityId" });
            DropIndex("dbo.Actions", new[] { "ActivityId" });
            DropIndex("dbo.Actions", new[] { "MainActId" });
            DropIndex("dbo.Actions", new[] { "ProgramId" });
            DropTable("dbo.UsersActions");
            DropTable("dbo.RoleDesignations");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.ActionTakens");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.Suppliers");
            DropTable("dbo.TechSpecsICTSpecsDetails");
            DropTable("dbo.TechSpecs");
            DropTable("dbo.TechSpecsICTSpecs");
            DropTable("dbo.TechSpecsBasis");
            DropTable("dbo.EquipmentSpecsDetails");
            DropTable("dbo.Equipments");
            DropTable("dbo.EquipmentSpecs");
            DropTable("dbo.Brands");
            DropTable("dbo.Models");
            DropTable("dbo.DeliveriesSpecsDetails");
            DropTable("dbo.DeliveriesSpecs");
            DropTable("dbo.Deliveries");
            DropTable("dbo.TicketRequests");
            DropTable("dbo.ITStaffs");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.ActionsDropdowns");
            DropTable("dbo.Actions");
        }
    }
}
