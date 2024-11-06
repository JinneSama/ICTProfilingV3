namespace ICTProfilingV3
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ribbon = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.btnDashboard = new DevExpress.XtraBars.BarButtonItem();
            this.btnTARequest = new DevExpress.XtraBars.BarButtonItem();
            this.btnTechSpecs = new DevExpress.XtraBars.BarButtonItem();
            this.btnDeliveries = new DevExpress.XtraBars.BarButtonItem();
            this.btnRepair = new DevExpress.XtraBars.BarButtonItem();
            this.btnCAS = new DevExpress.XtraBars.BarButtonItem();
            this.btnVPR = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem6 = new DevExpress.XtraBars.BarButtonItem();
            this.btnSuppliers = new DevExpress.XtraBars.BarButtonItem();
            this.btnEquipment = new DevExpress.XtraBars.BarButtonItem();
            this.btnBrand = new DevExpress.XtraBars.BarButtonItem();
            this.btnModels = new DevExpress.XtraBars.BarButtonItem();
            this.btnEquipmentSpecs = new DevExpress.XtraBars.BarButtonItem();
            this.btnUsers = new DevExpress.XtraBars.BarButtonItem();
            this.btnUserLevels = new DevExpress.XtraBars.BarButtonItem();
            this.btnPPE = new DevExpress.XtraBars.BarButtonItem();
            this.btnActionTree = new DevExpress.XtraBars.BarButtonItem();
            this.btnActionList = new DevExpress.XtraBars.BarButtonItem();
            this.btnStaff = new DevExpress.XtraBars.BarButtonItem();
            this.btnTechSpecsBasis = new DevExpress.XtraBars.BarButtonItem();
            this.btnStandardPR = new DevExpress.XtraBars.BarButtonItem();
            this.btnPRQuarter = new DevExpress.XtraBars.BarButtonItem();
            this.btnReport = new DevExpress.XtraBars.BarButtonItem();
            this.btnSPR = new DevExpress.XtraBars.BarButtonItem();
            this.btnRepairSpecs = new DevExpress.XtraBars.BarButtonItem();
            this.ribbonPage1 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroup1 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGroup2 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGroup3 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGroup12 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGroup4 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGroup7 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPage2 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroup5 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGroup6 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGroup9 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGroup10 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGroup8 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPage3 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroup11 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonStatusBar = new DevExpress.XtraBars.Ribbon.RibbonStatusBar();
            this.mainPanel = new DevExpress.XtraEditors.PanelControl();
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mainPanel)).BeginInit();
            this.SuspendLayout();
            // 
            // ribbon
            // 
            this.ribbon.ExpandCollapseItem.Id = 0;
            this.ribbon.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbon.ExpandCollapseItem,
            this.ribbon.SearchEditItem,
            this.btnDashboard,
            this.btnTARequest,
            this.btnTechSpecs,
            this.btnDeliveries,
            this.btnRepair,
            this.btnCAS,
            this.btnVPR,
            this.barButtonItem6,
            this.btnSuppliers,
            this.btnEquipment,
            this.btnBrand,
            this.btnModels,
            this.btnEquipmentSpecs,
            this.btnUsers,
            this.btnUserLevels,
            this.btnPPE,
            this.btnActionTree,
            this.btnActionList,
            this.btnStaff,
            this.btnTechSpecsBasis,
            this.btnStandardPR,
            this.btnPRQuarter,
            this.btnReport,
            this.btnSPR,
            this.btnRepairSpecs});
            this.ribbon.Location = new System.Drawing.Point(0, 0);
            this.ribbon.MaxItemId = 28;
            this.ribbon.Name = "ribbon";
            this.ribbon.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ribbonPage1,
            this.ribbonPage2,
            this.ribbonPage3});
            this.ribbon.Size = new System.Drawing.Size(1036, 154);
            this.ribbon.StatusBar = this.ribbonStatusBar;
            // 
            // btnDashboard
            // 
            this.btnDashboard.Caption = "Dashboard";
            this.btnDashboard.Id = 1;
            this.btnDashboard.ImageOptions.SvgImage = global::ICTProfilingV3.Properties.Resources.chart;
            this.btnDashboard.Name = "btnDashboard";
            // 
            // btnTARequest
            // 
            this.btnTARequest.Caption = "TA Request";
            this.btnTARequest.Id = 2;
            this.btnTARequest.ImageOptions.Image = global::ICTProfilingV3.Properties.Resources.assignto_16x16;
            this.btnTARequest.ImageOptions.LargeImage = global::ICTProfilingV3.Properties.Resources.assignto_32x32;
            this.btnTARequest.Name = "btnTARequest";
            this.btnTARequest.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnTARequest_ItemClick);
            // 
            // btnTechSpecs
            // 
            this.btnTechSpecs.Caption = "TechSpecs";
            this.btnTechSpecs.Id = 3;
            this.btnTechSpecs.ImageOptions.Image = global::ICTProfilingV3.Properties.Resources.paste_16x16;
            this.btnTechSpecs.ImageOptions.LargeImage = global::ICTProfilingV3.Properties.Resources.paste_32x32;
            this.btnTechSpecs.Name = "btnTechSpecs";
            this.btnTechSpecs.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnTechSpecs_ItemClick);
            // 
            // btnDeliveries
            // 
            this.btnDeliveries.Caption = "Deliveries";
            this.btnDeliveries.Id = 4;
            this.btnDeliveries.ImageOptions.Image = global::ICTProfilingV3.Properties.Resources.boorderitem_16x16;
            this.btnDeliveries.ImageOptions.LargeImage = global::ICTProfilingV3.Properties.Resources.boorderitem_32x32;
            this.btnDeliveries.Name = "btnDeliveries";
            this.btnDeliveries.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnDeliveries_ItemClick);
            // 
            // btnRepair
            // 
            this.btnRepair.Caption = "Repair";
            this.btnRepair.Id = 5;
            this.btnRepair.ImageOptions.Image = global::ICTProfilingV3.Properties.Resources.ide_16x161;
            this.btnRepair.ImageOptions.LargeImage = global::ICTProfilingV3.Properties.Resources.ide_32x322;
            this.btnRepair.Name = "btnRepair";
            this.btnRepair.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnRepair_ItemClick);
            // 
            // btnCAS
            // 
            this.btnCAS.Caption = "CAS";
            this.btnCAS.Id = 6;
            this.btnCAS.ImageOptions.Image = global::ICTProfilingV3.Properties.Resources.bodetails_16x16;
            this.btnCAS.ImageOptions.LargeImage = global::ICTProfilingV3.Properties.Resources.bodetails_32x32;
            this.btnCAS.Name = "btnCAS";
            // 
            // btnVPR
            // 
            this.btnVPR.Caption = "Verify PR";
            this.btnVPR.Id = 7;
            this.btnVPR.ImageOptions.Image = global::ICTProfilingV3.Properties.Resources.bosale_16x16;
            this.btnVPR.ImageOptions.LargeImage = global::ICTProfilingV3.Properties.Resources.bosale_32x32;
            this.btnVPR.Name = "btnVPR";
            // 
            // barButtonItem6
            // 
            this.barButtonItem6.Caption = "User Tasks";
            this.barButtonItem6.Id = 8;
            this.barButtonItem6.ImageOptions.Image = global::ICTProfilingV3.Properties.Resources.botask_16x16;
            this.barButtonItem6.ImageOptions.LargeImage = global::ICTProfilingV3.Properties.Resources.botask_32x32;
            this.barButtonItem6.Name = "barButtonItem6";
            // 
            // btnSuppliers
            // 
            this.btnSuppliers.Caption = "Suppliers";
            this.btnSuppliers.Id = 9;
            this.btnSuppliers.ImageOptions.Image = global::ICTProfilingV3.Properties.Resources.boreport2_16x16;
            this.btnSuppliers.ImageOptions.LargeImage = global::ICTProfilingV3.Properties.Resources.boreport2_32x32;
            this.btnSuppliers.Name = "btnSuppliers";
            this.btnSuppliers.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnSuppliers_ItemClick);
            // 
            // btnEquipment
            // 
            this.btnEquipment.Caption = "Equipments";
            this.btnEquipment.Id = 10;
            this.btnEquipment.ImageOptions.Image = global::ICTProfilingV3.Properties.Resources.customization_16x16;
            this.btnEquipment.ImageOptions.LargeImage = global::ICTProfilingV3.Properties.Resources.customization_32x32;
            this.btnEquipment.Name = "btnEquipment";
            this.btnEquipment.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnEquipment_ItemClick);
            // 
            // btnBrand
            // 
            this.btnBrand.Caption = "Equipment Brands";
            this.btnBrand.Id = 11;
            this.btnBrand.ImageOptions.Image = global::ICTProfilingV3.Properties.Resources.brand_16x161;
            this.btnBrand.ImageOptions.LargeImage = global::ICTProfilingV3.Properties.Resources.b_32x32;
            this.btnBrand.Name = "btnBrand";
            this.btnBrand.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnBrand_ItemClick);
            // 
            // btnModels
            // 
            this.btnModels.Caption = "Equipment Models";
            this.btnModels.Id = 12;
            this.btnModels.ImageOptions.Image = global::ICTProfilingV3.Properties.Resources.tag_16x16;
            this.btnModels.ImageOptions.LargeImage = global::ICTProfilingV3.Properties.Resources.tag_32x32;
            this.btnModels.Name = "btnModels";
            this.btnModels.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnModels_ItemClick);
            // 
            // btnEquipmentSpecs
            // 
            this.btnEquipmentSpecs.Caption = "Equipment Specifications";
            this.btnEquipmentSpecs.Id = 13;
            this.btnEquipmentSpecs.ImageOptions.Image = global::ICTProfilingV3.Properties.Resources.pielabelsdatalabels2_16x16;
            this.btnEquipmentSpecs.ImageOptions.LargeImage = global::ICTProfilingV3.Properties.Resources.pielabelsdatalabels2_32x32;
            this.btnEquipmentSpecs.Name = "btnEquipmentSpecs";
            this.btnEquipmentSpecs.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnEquipmentSpecs_ItemClick);
            // 
            // btnUsers
            // 
            this.btnUsers.Caption = "Users";
            this.btnUsers.Id = 14;
            this.btnUsers.ImageOptions.Image = global::ICTProfilingV3.Properties.Resources.user_16x16;
            this.btnUsers.ImageOptions.LargeImage = global::ICTProfilingV3.Properties.Resources.user_32x32;
            this.btnUsers.Name = "btnUsers";
            this.btnUsers.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnUsers_ItemClick);
            // 
            // btnUserLevels
            // 
            this.btnUserLevels.Caption = "User Levels";
            this.btnUserLevels.Id = 15;
            this.btnUserLevels.ImageOptions.Image = global::ICTProfilingV3.Properties.Resources.bouser_16x16;
            this.btnUserLevels.ImageOptions.LargeImage = global::ICTProfilingV3.Properties.Resources.bouser_32x32;
            this.btnUserLevels.Name = "btnUserLevels";
            this.btnUserLevels.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnUserLevels_ItemClick);
            // 
            // btnPPE
            // 
            this.btnPPE.Caption = "PPE Profiles";
            this.btnPPE.Id = 16;
            this.btnPPE.ImageOptions.Image = global::ICTProfilingV3.Properties.Resources.projectdirectory_16x16;
            this.btnPPE.ImageOptions.LargeImage = global::ICTProfilingV3.Properties.Resources.projectdirectory_32x32;
            this.btnPPE.Name = "btnPPE";
            this.btnPPE.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnPPE_ItemClick);
            // 
            // btnActionTree
            // 
            this.btnActionTree.Caption = "Action Tree";
            this.btnActionTree.Id = 18;
            this.btnActionTree.ImageOptions.Image = global::ICTProfilingV3.Properties.Resources.treeview_16x161;
            this.btnActionTree.ImageOptions.LargeImage = global::ICTProfilingV3.Properties.Resources.treeview_32x321;
            this.btnActionTree.Name = "btnActionTree";
            this.btnActionTree.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnActionTree_ItemClick);
            // 
            // btnActionList
            // 
            this.btnActionList.Caption = "Action List";
            this.btnActionList.Id = 19;
            this.btnActionList.ImageOptions.Image = global::ICTProfilingV3.Properties.Resources.listbullets_righttoleft_16x16;
            this.btnActionList.ImageOptions.LargeImage = global::ICTProfilingV3.Properties.Resources.listbullets_righttoleft_32x32;
            this.btnActionList.Name = "btnActionList";
            this.btnActionList.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnActionList_ItemClick);
            // 
            // btnStaff
            // 
            this.btnStaff.Caption = "IT Staff";
            this.btnStaff.Id = 20;
            this.btnStaff.ImageOptions.Image = global::ICTProfilingV3.Properties.Resources.female_16x16;
            this.btnStaff.ImageOptions.LargeImage = global::ICTProfilingV3.Properties.Resources.female_32x32;
            this.btnStaff.Name = "btnStaff";
            this.btnStaff.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnStaff_ItemClick);
            // 
            // btnTechSpecsBasis
            // 
            this.btnTechSpecsBasis.Caption = "Tech Specs Basis";
            this.btnTechSpecsBasis.Id = 21;
            this.btnTechSpecsBasis.ImageOptions.Image = global::ICTProfilingV3.Properties.Resources.financial_16x16;
            this.btnTechSpecsBasis.ImageOptions.LargeImage = global::ICTProfilingV3.Properties.Resources.financial_32x32;
            this.btnTechSpecsBasis.Name = "btnTechSpecsBasis";
            this.btnTechSpecsBasis.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnTechSpecsBasis_ItemClick);
            // 
            // btnStandardPR
            // 
            this.btnStandardPR.Caption = "Recommended ICT Specs";
            this.btnStandardPR.Id = 22;
            this.btnStandardPR.ImageOptions.Image = global::ICTProfilingV3.Properties.Resources.gotofooter_16x16;
            this.btnStandardPR.ImageOptions.LargeImage = global::ICTProfilingV3.Properties.Resources.gotofooter_32x32;
            this.btnStandardPR.Name = "btnStandardPR";
            // 
            // btnPRQuarter
            // 
            this.btnPRQuarter.Caption = "Quarter";
            this.btnPRQuarter.Id = 23;
            this.btnPRQuarter.ImageOptions.Image = global::ICTProfilingV3.Properties.Resources.listnumbers_16x16;
            this.btnPRQuarter.ImageOptions.LargeImage = global::ICTProfilingV3.Properties.Resources.listnumbers_32x32;
            this.btnPRQuarter.Name = "btnPRQuarter";
            // 
            // btnReport
            // 
            this.btnReport.Caption = "Accomplishment Report";
            this.btnReport.Id = 25;
            this.btnReport.ImageOptions.Image = global::ICTProfilingV3.Properties.Resources.report_16x16;
            this.btnReport.ImageOptions.LargeImage = global::ICTProfilingV3.Properties.Resources.report_32x32;
            this.btnReport.Name = "btnReport";
            // 
            // btnSPR
            // 
            this.btnSPR.Caption = "Standard PR";
            this.btnSPR.Id = 26;
            this.btnSPR.ImageOptions.Image = global::ICTProfilingV3.Properties.Resources.costanalysis_16x16;
            this.btnSPR.ImageOptions.LargeImage = global::ICTProfilingV3.Properties.Resources.costanalysis_32x32;
            this.btnSPR.Name = "btnSPR";
            // 
            // btnRepairSpecs
            // 
            this.btnRepairSpecs.Caption = "Repair Specs";
            this.btnRepairSpecs.Id = 27;
            this.btnRepairSpecs.ImageOptions.Image = global::ICTProfilingV3.Properties.Resources.customization_16x161;
            this.btnRepairSpecs.ImageOptions.LargeImage = global::ICTProfilingV3.Properties.Resources.customization_32x321;
            this.btnRepairSpecs.Name = "btnRepairSpecs";
            // 
            // ribbonPage1
            // 
            this.ribbonPage1.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroup1,
            this.ribbonPageGroup2,
            this.ribbonPageGroup3,
            this.ribbonPageGroup12,
            this.ribbonPageGroup4,
            this.ribbonPageGroup7});
            this.ribbonPage1.ImageOptions.Image = global::ICTProfilingV3.Properties.Resources.home_16x16;
            this.ribbonPage1.Name = "ribbonPage1";
            this.ribbonPage1.Text = "Home";
            // 
            // ribbonPageGroup1
            // 
            this.ribbonPageGroup1.ItemLinks.Add(this.btnDashboard);
            this.ribbonPageGroup1.ItemLinks.Add(this.barButtonItem6);
            this.ribbonPageGroup1.Name = "ribbonPageGroup1";
            // 
            // ribbonPageGroup2
            // 
            this.ribbonPageGroup2.ItemLinks.Add(this.btnTARequest);
            this.ribbonPageGroup2.Name = "ribbonPageGroup2";
            this.ribbonPageGroup2.Text = "Ticket";
            // 
            // ribbonPageGroup3
            // 
            this.ribbonPageGroup3.ItemLinks.Add(this.btnTechSpecs, true);
            this.ribbonPageGroup3.ItemLinks.Add(this.btnDeliveries);
            this.ribbonPageGroup3.ItemLinks.Add(this.btnRepair);
            this.ribbonPageGroup3.Name = "ribbonPageGroup3";
            this.ribbonPageGroup3.Text = "Transactions";
            // 
            // ribbonPageGroup12
            // 
            this.ribbonPageGroup12.ItemLinks.Add(this.btnRepairSpecs);
            this.ribbonPageGroup12.Name = "ribbonPageGroup12";
            // 
            // ribbonPageGroup4
            // 
            this.ribbonPageGroup4.ItemLinks.Add(this.btnCAS);
            this.ribbonPageGroup4.ItemLinks.Add(this.btnVPR);
            this.ribbonPageGroup4.ItemLinks.Add(this.btnSPR);
            this.ribbonPageGroup4.Name = "ribbonPageGroup4";
            // 
            // ribbonPageGroup7
            // 
            this.ribbonPageGroup7.ItemLinks.Add(this.btnPPE);
            this.ribbonPageGroup7.Name = "ribbonPageGroup7";
            this.ribbonPageGroup7.Text = "Inventory";
            // 
            // ribbonPage2
            // 
            this.ribbonPage2.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroup5,
            this.ribbonPageGroup6,
            this.ribbonPageGroup9,
            this.ribbonPageGroup10,
            this.ribbonPageGroup8});
            this.ribbonPage2.ImageOptions.Image = global::ICTProfilingV3.Properties.Resources.inserttablerowsabove_16x16;
            this.ribbonPage2.Name = "ribbonPage2";
            this.ribbonPage2.Text = "Look Up Tables";
            // 
            // ribbonPageGroup5
            // 
            this.ribbonPageGroup5.ItemLinks.Add(this.btnUsers);
            this.ribbonPageGroup5.ItemLinks.Add(this.btnUserLevels);
            this.ribbonPageGroup5.ItemLinks.Add(this.btnStaff);
            this.ribbonPageGroup5.Name = "ribbonPageGroup5";
            this.ribbonPageGroup5.Text = "User Management";
            // 
            // ribbonPageGroup6
            // 
            this.ribbonPageGroup6.ItemLinks.Add(this.btnSuppliers);
            this.ribbonPageGroup6.ItemLinks.Add(this.btnEquipment);
            this.ribbonPageGroup6.ItemLinks.Add(this.btnEquipmentSpecs);
            this.ribbonPageGroup6.ItemLinks.Add(this.btnBrand);
            this.ribbonPageGroup6.ItemLinks.Add(this.btnModels);
            this.ribbonPageGroup6.Name = "ribbonPageGroup6";
            this.ribbonPageGroup6.Text = "Equipment";
            // 
            // ribbonPageGroup9
            // 
            this.ribbonPageGroup9.ItemLinks.Add(this.btnTechSpecsBasis);
            this.ribbonPageGroup9.Name = "ribbonPageGroup9";
            this.ribbonPageGroup9.Text = "TS Specifications";
            // 
            // ribbonPageGroup10
            // 
            this.ribbonPageGroup10.ItemLinks.Add(this.btnPRQuarter);
            this.ribbonPageGroup10.ItemLinks.Add(this.btnStandardPR);
            this.ribbonPageGroup10.Name = "ribbonPageGroup10";
            this.ribbonPageGroup10.Text = "Purchase Request";
            // 
            // ribbonPageGroup8
            // 
            this.ribbonPageGroup8.ItemLinks.Add(this.btnActionTree);
            this.ribbonPageGroup8.ItemLinks.Add(this.btnActionList);
            this.ribbonPageGroup8.Name = "ribbonPageGroup8";
            this.ribbonPageGroup8.Text = "Actions";
            // 
            // ribbonPage3
            // 
            this.ribbonPage3.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroup11});
            this.ribbonPage3.ImageOptions.Image = global::ICTProfilingV3.Properties.Resources.report_16x161;
            this.ribbonPage3.Name = "ribbonPage3";
            this.ribbonPage3.Text = "Report";
            // 
            // ribbonPageGroup11
            // 
            this.ribbonPageGroup11.ItemLinks.Add(this.btnReport);
            this.ribbonPageGroup11.Name = "ribbonPageGroup11";
            // 
            // ribbonStatusBar
            // 
            this.ribbonStatusBar.Location = new System.Drawing.Point(0, 629);
            this.ribbonStatusBar.Name = "ribbonStatusBar";
            this.ribbonStatusBar.Ribbon = this.ribbon;
            this.ribbonStatusBar.Size = new System.Drawing.Size(1036, 21);
            // 
            // mainPanel
            // 
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanel.Location = new System.Drawing.Point(0, 154);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(1036, 475);
            this.mainPanel.TabIndex = 2;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1036, 650);
            this.Controls.Add(this.mainPanel);
            this.Controls.Add(this.ribbonStatusBar);
            this.Controls.Add(this.ribbon);
            this.Name = "frmMain";
            this.Ribbon = this.ribbon;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.StatusBar = this.ribbonStatusBar;
            this.Text = "ICT Profiling V3";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mainPanel)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.Ribbon.RibbonControl ribbon;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage1;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup1;
        private DevExpress.XtraBars.Ribbon.RibbonStatusBar ribbonStatusBar;
        private DevExpress.XtraBars.BarButtonItem btnDashboard;
        private DevExpress.XtraBars.BarButtonItem btnTARequest;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup2;
        private DevExpress.XtraBars.BarButtonItem btnTechSpecs;
        private DevExpress.XtraBars.BarButtonItem btnDeliveries;
        private DevExpress.XtraBars.BarButtonItem btnRepair;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup3;
        private DevExpress.XtraBars.BarButtonItem btnCAS;
        private DevExpress.XtraBars.BarButtonItem btnVPR;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup4;
        private DevExpress.XtraEditors.PanelControl mainPanel;
        private DevExpress.XtraBars.BarButtonItem barButtonItem6;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage2;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup5;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup6;
        private DevExpress.XtraBars.BarButtonItem btnSuppliers;
        private DevExpress.XtraBars.BarButtonItem btnEquipment;
        private DevExpress.XtraBars.BarButtonItem btnBrand;
        private DevExpress.XtraBars.BarButtonItem btnModels;
        private DevExpress.XtraBars.BarButtonItem btnEquipmentSpecs;
        private DevExpress.XtraBars.BarButtonItem btnUsers;
        private DevExpress.XtraBars.BarButtonItem btnUserLevels;
        private DevExpress.XtraBars.BarButtonItem btnPPE;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup7;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup8;
        private DevExpress.XtraBars.BarButtonItem btnActionTree;
        private DevExpress.XtraBars.BarButtonItem btnActionList;
        private DevExpress.XtraBars.BarButtonItem btnStaff;
        private DevExpress.XtraBars.BarButtonItem btnTechSpecsBasis;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup9;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup10;
        private DevExpress.XtraBars.BarButtonItem btnStandardPR;
        private DevExpress.XtraBars.BarButtonItem btnPRQuarter;
        private DevExpress.XtraBars.BarButtonItem btnReport;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage3;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup11;
        private DevExpress.XtraBars.BarButtonItem btnSPR;
        private DevExpress.XtraBars.BarButtonItem btnRepairSpecs;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup12;
    }
}