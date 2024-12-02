namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
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
                        RequestType = c.Int(nullable: false),
                        ProgramId = c.Int(),
                        MainActId = c.Int(),
                        ActivityId = c.Int(),
                        SubActivityId = c.Int(),
                        DeliveriesId = c.Int(),
                        TechSpecsId = c.Int(),
                        RepairId = c.Int(),
                        PurchaseRequestId = c.Int(),
                        CustomerActionSheetId = c.Int(),
                        PGNRequestId = c.Int(),
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
                .ForeignKey("dbo.CustomerActionSheets", t => t.CustomerActionSheetId)
                .ForeignKey("dbo.Deliveries", t => t.DeliveriesId)
                .ForeignKey("dbo.PurchaseRequests", t => t.PurchaseRequestId)
                .ForeignKey("dbo.TechSpecs", t => t.TechSpecsId)
                .ForeignKey("dbo.Repairs", t => t.RepairId)
                .ForeignKey("dbo.PGNRequests", t => t.PGNRequestId)
                .ForeignKey("dbo.AspNetUsers", t => t.CreatedById)
                .ForeignKey("dbo.ActionsDropdowns", t => t.MainActId)
                .ForeignKey("dbo.ActionsDropdowns", t => t.ProgramId)
                .ForeignKey("dbo.ActionsDropdowns", t => t.SubActivityId)
                .Index(t => t.ProgramId)
                .Index(t => t.MainActId)
                .Index(t => t.ActivityId)
                .Index(t => t.SubActivityId)
                .Index(t => t.DeliveriesId)
                .Index(t => t.TechSpecsId)
                .Index(t => t.RepairId)
                .Index(t => t.PurchaseRequestId)
                .Index(t => t.CustomerActionSheetId)
                .Index(t => t.PGNRequestId)
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
                "dbo.CustomerActionSheets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ClientId = c.Long(),
                        DateCreated = c.DateTime(),
                        ClientName = c.String(),
                        Office = c.String(),
                        Gender = c.Int(nullable: false),
                        ContactNo = c.String(),
                        ClientRequest = c.String(),
                        ActionTaken = c.String(),
                        IsDeleted = c.Boolean(),
                        AssistedById = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.AssistedById)
                .Index(t => t.AssistedById);
            
            CreateTable(
                "dbo.ITStaffs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ImagePath = c.String(),
                        Section = c.Int(nullable: false),
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
                        IsDeleted = c.Boolean(),
                        TicketStatus = c.Int(nullable: false),
                        RequestType = c.Int(nullable: false),
                        StaffId = c.Int(),
                        IsRepairTechSpecs = c.Boolean(),
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
                        RequestedById = c.Long(nullable: false),
                        ReqByChiefId = c.Long(nullable: false),
                        DeliveredById = c.Long(nullable: false),
                        Gender = c.Int(nullable: false),
                        ContactNo = c.String(),
                        PONo = c.String(),
                        ReceiptNo = c.String(),
                        SupplierId = c.Int(),
                        IsDeleted = c.Boolean(),
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
                "dbo.StandardPRSpecs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ItemNo = c.Int(nullable: false),
                        Quarter = c.Int(nullable: false),
                        Description = c.String(),
                        Purpose = c.String(),
                        Unit = c.Int(nullable: false),
                        UnitCost = c.Long(),
                        EquipmentSpecsId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EquipmentSpecs", t => t.EquipmentSpecsId, cascadeDelete: true)
                .Index(t => t.EquipmentSpecsId);
            
            CreateTable(
                "dbo.PRStandardPRSpecs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ItemNo = c.Int(),
                        Quantity = c.Int(),
                        TotalCost = c.Long(),
                        PurchaseRequestId = c.Int(nullable: false),
                        StandardPRSpecsId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PurchaseRequests", t => t.PurchaseRequestId, cascadeDelete: true)
                .ForeignKey("dbo.StandardPRSpecs", t => t.StandardPRSpecsId, cascadeDelete: true)
                .Index(t => t.PurchaseRequestId)
                .Index(t => t.StandardPRSpecsId);
            
            CreateTable(
                "dbo.PurchaseRequests",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DateCreated = c.DateTime(),
                        ChiefId = c.Long(),
                        ReqById = c.Long(),
                        PRNo = c.String(),
                        Quarter = c.Int(nullable: false),
                        IsDeleted = c.Boolean(),
                        TechSpecsId = c.Int(),
                        CreatedById = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.CreatedById)
                .ForeignKey("dbo.TechSpecs", t => t.TechSpecsId)
                .Index(t => t.TechSpecsId)
                .Index(t => t.CreatedById);
            
            CreateTable(
                "dbo.TechSpecs",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        DateRequested = c.DateTime(),
                        DateAccepted = c.DateTime(),
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
                        IsDeleted = c.Boolean(),
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
                "dbo.Repairs",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        RequestedById = c.Long(nullable: false),
                        ReqByChiefId = c.Long(nullable: false),
                        DeliveredById = c.Long(nullable: false),
                        DateCreated = c.DateTime(),
                        DateDelivered = c.DateTime(),
                        Problems = c.String(),
                        Findings = c.String(),
                        Recommendations = c.String(),
                        PreparedById = c.String(maxLength: 128),
                        IsDeleted = c.Boolean(),
                        ReviewedById = c.String(maxLength: 128),
                        NotedById = c.String(maxLength: 128),
                        Gender = c.Int(nullable: false),
                        ContactNo = c.String(),
                        PPEsId = c.Int(),
                        PPESpecsId = c.Int(),
                        TechSpecsId = c.Int(),
                        Users_Id = c.String(maxLength: 128),
                        Users_Id1 = c.String(maxLength: 128),
                        Users_Id2 = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.NotedById)
                .ForeignKey("dbo.PPEsSpecs", t => t.PPESpecsId)
                .ForeignKey("dbo.PPEs", t => t.PPEsId)
                .ForeignKey("dbo.AspNetUsers", t => t.PreparedById)
                .ForeignKey("dbo.AspNetUsers", t => t.ReviewedById)
                .ForeignKey("dbo.TechSpecs", t => t.TechSpecsId)
                .ForeignKey("dbo.TicketRequests", t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Users_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Users_Id1)
                .ForeignKey("dbo.AspNetUsers", t => t.Users_Id2)
                .Index(t => t.Id)
                .Index(t => t.PreparedById)
                .Index(t => t.ReviewedById)
                .Index(t => t.NotedById)
                .Index(t => t.PPEsId)
                .Index(t => t.PPESpecsId)
                .Index(t => t.TechSpecsId)
                .Index(t => t.Users_Id)
                .Index(t => t.Users_Id1)
                .Index(t => t.Users_Id2);
            
            CreateTable(
                "dbo.PPEs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IssuedToId = c.Long(),
                        ChiefId = c.Long(),
                        Gender = c.Int(),
                        ContactNo = c.String(),
                        PropertyNo = c.String(),
                        SerialNo = c.String(),
                        DateCreated = c.DateTime(),
                        AquisitionDate = c.DateTime(),
                        Status = c.Int(),
                        Quantity = c.Int(nullable: false),
                        Unit = c.Int(nullable: false),
                        UnitValue = c.Long(),
                        TotalValue = c.Long(),
                        Remarks = c.String(),
                        IsDeleted = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PPEsSpecs",
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
                        PPEsId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Models", t => t.ModelId, cascadeDelete: true)
                .ForeignKey("dbo.PPEs", t => t.PPEsId)
                .Index(t => t.ModelId)
                .Index(t => t.PPEsId);
            
            CreateTable(
                "dbo.PPEsSpecsDetails",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        PPEsSpecsId = c.Int(nullable: false),
                        ItemNo = c.Int(nullable: false),
                        Specs = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PPEsSpecs", t => t.PPEsSpecsId, cascadeDelete: true)
                .Index(t => t.PPEsSpecsId);
            
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
                "dbo.StandardPRSpecsDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ItemNo = c.Int(nullable: false),
                        Specs = c.String(),
                        Description = c.String(),
                        StandardPRSpecsId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.StandardPRSpecs", t => t.StandardPRSpecsId, cascadeDelete: true)
                .Index(t => t.StandardPRSpecsId);
            
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
                "dbo.TicketRequestStatus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Status = c.Int(nullable: false),
                        DateStatusChanged = c.DateTime(),
                        ChangedByUserId = c.String(maxLength: 128),
                        TicketRequestId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ChangedByUserId)
                .ForeignKey("dbo.TicketRequests", t => t.TicketRequestId, cascadeDelete: true)
                .Index(t => t.ChangedByUserId)
                .Index(t => t.TicketRequestId);
            
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
                "dbo.PGNRequests",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DateCreated = c.DateTime(),
                        RequestDate = c.DateTime(),
                        CommunicationType = c.Int(nullable: false),
                        SignatoryId = c.Long(),
                        Subject = c.String(),
                        CreatedById = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.CreatedById)
                .Index(t => t.CreatedById);
            
            CreateTable(
                "dbo.PGNAccounts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        HRMISEmpId = c.Long(),
                        Username = c.String(),
                        UserType = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                        SignInCount = c.Int(),
                        TrafficSpeed = c.Int(nullable: false),
                        Designation = c.Int(nullable: false),
                        Remarks = c.String(),
                        Password = c.String(),
                        PGNGroupOfficesId = c.Int(),
                        PGNNonEmployeeId = c.Int(),
                        PGNRequestId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PGNGroupOffices", t => t.PGNGroupOfficesId)
                .ForeignKey("dbo.PGNNonEmployees", t => t.PGNNonEmployeeId)
                .ForeignKey("dbo.PGNRequests", t => t.PGNRequestId)
                .Index(t => t.PGNGroupOfficesId)
                .Index(t => t.PGNNonEmployeeId)
                .Index(t => t.PGNRequestId);
            
            CreateTable(
                "dbo.PGNMacAddresses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Connection = c.Int(nullable: false),
                        Device = c.Int(nullable: false),
                        MacAddress = c.String(),
                        PGNAccountId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PGNAccounts", t => t.PGNAccountId, cascadeDelete: true)
                .Index(t => t.PGNAccountId);
            
            CreateTable(
                "dbo.PGNGroupOffices",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OfficeAcr = c.String(),
                        Office = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PGNNonEmployees",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FullName = c.String(),
                        Position = c.String(),
                        Username = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PGNDocuments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DocOrder = c.Int(nullable: false),
                        FilePath = c.String(),
                        FileName = c.String(),
                        PGNRequestId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PGNRequests", t => t.PGNRequestId, cascadeDelete: true)
                .Index(t => t.PGNRequestId);
            
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
            DropForeignKey("dbo.Repairs", "Users_Id2", "dbo.AspNetUsers");
            DropForeignKey("dbo.Repairs", "Users_Id1", "dbo.AspNetUsers");
            DropForeignKey("dbo.Repairs", "Users_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.PGNDocuments", "PGNRequestId", "dbo.PGNRequests");
            DropForeignKey("dbo.PGNAccounts", "PGNRequestId", "dbo.PGNRequests");
            DropForeignKey("dbo.PGNAccounts", "PGNNonEmployeeId", "dbo.PGNNonEmployees");
            DropForeignKey("dbo.PGNAccounts", "PGNGroupOfficesId", "dbo.PGNGroupOffices");
            DropForeignKey("dbo.PGNMacAddresses", "PGNAccountId", "dbo.PGNAccounts");
            DropForeignKey("dbo.PGNRequests", "CreatedById", "dbo.AspNetUsers");
            DropForeignKey("dbo.Actions", "PGNRequestId", "dbo.PGNRequests");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ITStaffs", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.TicketRequestStatus", "TicketRequestId", "dbo.TicketRequests");
            DropForeignKey("dbo.TicketRequestStatus", "ChangedByUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.TicketRequests", "StaffId", "dbo.ITStaffs");
            DropForeignKey("dbo.Deliveries", "Id", "dbo.TicketRequests");
            DropForeignKey("dbo.Deliveries", "SupplierId", "dbo.Suppliers");
            DropForeignKey("dbo.DeliveriesSpecs", "ModelId", "dbo.Models");
            DropForeignKey("dbo.Models", "BrandId", "dbo.Brands");
            DropForeignKey("dbo.TechSpecsBasis", "EquipmentSpecsId", "dbo.EquipmentSpecs");
            DropForeignKey("dbo.StandardPRSpecsDetails", "StandardPRSpecsId", "dbo.StandardPRSpecs");
            DropForeignKey("dbo.PRStandardPRSpecs", "StandardPRSpecsId", "dbo.StandardPRSpecs");
            DropForeignKey("dbo.PurchaseRequests", "TechSpecsId", "dbo.TechSpecs");
            DropForeignKey("dbo.TechSpecs", "Id", "dbo.TicketRequests");
            DropForeignKey("dbo.TechSpecsICTSpecsDetails", "TechSpecsICTSpecsId", "dbo.TechSpecsICTSpecs");
            DropForeignKey("dbo.TechSpecsICTSpecs", "TechSpecsId", "dbo.TechSpecs");
            DropForeignKey("dbo.TechSpecsICTSpecs", "EquipmentSpecsId", "dbo.EquipmentSpecs");
            DropForeignKey("dbo.TechSpecs", "ReviewedById", "dbo.AspNetUsers");
            DropForeignKey("dbo.Repairs", "Id", "dbo.TicketRequests");
            DropForeignKey("dbo.Repairs", "TechSpecsId", "dbo.TechSpecs");
            DropForeignKey("dbo.Repairs", "ReviewedById", "dbo.AspNetUsers");
            DropForeignKey("dbo.Repairs", "PreparedById", "dbo.AspNetUsers");
            DropForeignKey("dbo.Repairs", "PPEsId", "dbo.PPEs");
            DropForeignKey("dbo.Repairs", "PPESpecsId", "dbo.PPEsSpecs");
            DropForeignKey("dbo.PPEsSpecsDetails", "PPEsSpecsId", "dbo.PPEsSpecs");
            DropForeignKey("dbo.PPEsSpecs", "PPEsId", "dbo.PPEs");
            DropForeignKey("dbo.PPEsSpecs", "ModelId", "dbo.Models");
            DropForeignKey("dbo.Repairs", "NotedById", "dbo.AspNetUsers");
            DropForeignKey("dbo.Actions", "RepairId", "dbo.Repairs");
            DropForeignKey("dbo.TechSpecs", "PreparedById", "dbo.AspNetUsers");
            DropForeignKey("dbo.TechSpecs", "NotedById", "dbo.AspNetUsers");
            DropForeignKey("dbo.Actions", "TechSpecsId", "dbo.TechSpecs");
            DropForeignKey("dbo.PRStandardPRSpecs", "PurchaseRequestId", "dbo.PurchaseRequests");
            DropForeignKey("dbo.PurchaseRequests", "CreatedById", "dbo.AspNetUsers");
            DropForeignKey("dbo.Actions", "PurchaseRequestId", "dbo.PurchaseRequests");
            DropForeignKey("dbo.StandardPRSpecs", "EquipmentSpecsId", "dbo.EquipmentSpecs");
            DropForeignKey("dbo.EquipmentSpecsDetails", "EquipmentSpecsId", "dbo.EquipmentSpecs");
            DropForeignKey("dbo.EquipmentSpecs", "EquipmentId", "dbo.Equipments");
            DropForeignKey("dbo.Brands", "EquipmenSpecsId", "dbo.EquipmentSpecs");
            DropForeignKey("dbo.DeliveriesSpecsDetails", "DeliveriesSpecsId", "dbo.DeliveriesSpecs");
            DropForeignKey("dbo.DeliveriesSpecs", "DeliveriesId", "dbo.Deliveries");
            DropForeignKey("dbo.Actions", "DeliveriesId", "dbo.Deliveries");
            DropForeignKey("dbo.TicketRequests", "CreatedBy", "dbo.AspNetUsers");
            DropForeignKey("dbo.CustomerActionSheets", "AssistedById", "dbo.AspNetUsers");
            DropForeignKey("dbo.Actions", "CustomerActionSheetId", "dbo.CustomerActionSheets");
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
            DropIndex("dbo.PGNDocuments", new[] { "PGNRequestId" });
            DropIndex("dbo.PGNMacAddresses", new[] { "PGNAccountId" });
            DropIndex("dbo.PGNAccounts", new[] { "PGNRequestId" });
            DropIndex("dbo.PGNAccounts", new[] { "PGNNonEmployeeId" });
            DropIndex("dbo.PGNAccounts", new[] { "PGNGroupOfficesId" });
            DropIndex("dbo.PGNRequests", new[] { "CreatedById" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.TicketRequestStatus", new[] { "TicketRequestId" });
            DropIndex("dbo.TicketRequestStatus", new[] { "ChangedByUserId" });
            DropIndex("dbo.TechSpecsBasis", new[] { "EquipmentSpecsId" });
            DropIndex("dbo.StandardPRSpecsDetails", new[] { "StandardPRSpecsId" });
            DropIndex("dbo.TechSpecsICTSpecsDetails", new[] { "TechSpecsICTSpecsId" });
            DropIndex("dbo.TechSpecsICTSpecs", new[] { "EquipmentSpecsId" });
            DropIndex("dbo.TechSpecsICTSpecs", new[] { "TechSpecsId" });
            DropIndex("dbo.PPEsSpecsDetails", new[] { "PPEsSpecsId" });
            DropIndex("dbo.PPEsSpecs", new[] { "PPEsId" });
            DropIndex("dbo.PPEsSpecs", new[] { "ModelId" });
            DropIndex("dbo.Repairs", new[] { "Users_Id2" });
            DropIndex("dbo.Repairs", new[] { "Users_Id1" });
            DropIndex("dbo.Repairs", new[] { "Users_Id" });
            DropIndex("dbo.Repairs", new[] { "TechSpecsId" });
            DropIndex("dbo.Repairs", new[] { "PPESpecsId" });
            DropIndex("dbo.Repairs", new[] { "PPEsId" });
            DropIndex("dbo.Repairs", new[] { "NotedById" });
            DropIndex("dbo.Repairs", new[] { "ReviewedById" });
            DropIndex("dbo.Repairs", new[] { "PreparedById" });
            DropIndex("dbo.Repairs", new[] { "Id" });
            DropIndex("dbo.TechSpecs", new[] { "Users_Id2" });
            DropIndex("dbo.TechSpecs", new[] { "Users_Id1" });
            DropIndex("dbo.TechSpecs", new[] { "Users_Id" });
            DropIndex("dbo.TechSpecs", new[] { "NotedById" });
            DropIndex("dbo.TechSpecs", new[] { "ReviewedById" });
            DropIndex("dbo.TechSpecs", new[] { "PreparedById" });
            DropIndex("dbo.TechSpecs", new[] { "Id" });
            DropIndex("dbo.PurchaseRequests", new[] { "CreatedById" });
            DropIndex("dbo.PurchaseRequests", new[] { "TechSpecsId" });
            DropIndex("dbo.PRStandardPRSpecs", new[] { "StandardPRSpecsId" });
            DropIndex("dbo.PRStandardPRSpecs", new[] { "PurchaseRequestId" });
            DropIndex("dbo.StandardPRSpecs", new[] { "EquipmentSpecsId" });
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
            DropIndex("dbo.CustomerActionSheets", new[] { "AssistedById" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Actions", new[] { "ActionsDropdowns_Id3" });
            DropIndex("dbo.Actions", new[] { "ActionsDropdowns_Id2" });
            DropIndex("dbo.Actions", new[] { "ActionsDropdowns_Id1" });
            DropIndex("dbo.Actions", new[] { "ActionsDropdowns_Id" });
            DropIndex("dbo.Actions", new[] { "CreatedById" });
            DropIndex("dbo.Actions", new[] { "PGNRequestId" });
            DropIndex("dbo.Actions", new[] { "CustomerActionSheetId" });
            DropIndex("dbo.Actions", new[] { "PurchaseRequestId" });
            DropIndex("dbo.Actions", new[] { "RepairId" });
            DropIndex("dbo.Actions", new[] { "TechSpecsId" });
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
            DropTable("dbo.PGNDocuments");
            DropTable("dbo.PGNNonEmployees");
            DropTable("dbo.PGNGroupOffices");
            DropTable("dbo.PGNMacAddresses");
            DropTable("dbo.PGNAccounts");
            DropTable("dbo.PGNRequests");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.TicketRequestStatus");
            DropTable("dbo.Suppliers");
            DropTable("dbo.TechSpecsBasis");
            DropTable("dbo.StandardPRSpecsDetails");
            DropTable("dbo.TechSpecsICTSpecsDetails");
            DropTable("dbo.TechSpecsICTSpecs");
            DropTable("dbo.PPEsSpecsDetails");
            DropTable("dbo.PPEsSpecs");
            DropTable("dbo.PPEs");
            DropTable("dbo.Repairs");
            DropTable("dbo.TechSpecs");
            DropTable("dbo.PurchaseRequests");
            DropTable("dbo.PRStandardPRSpecs");
            DropTable("dbo.StandardPRSpecs");
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
            DropTable("dbo.CustomerActionSheets");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.ActionsDropdowns");
            DropTable("dbo.Actions");
        }
    }
}
