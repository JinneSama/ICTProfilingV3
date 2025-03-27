using System;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using DevExpress.Xpo;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using EntityManager.Managers.Role;
using EntityManager.Managers.User;
using Helpers.Interfaces;
using Helpers.Update;
using Helpers.Utility;
using ICTProfilingV3.ActionsForms;
using ICTProfilingV3.BaseClasses;
using ICTProfilingV3.CustomerActionSheetForms;
using ICTProfilingV3.DashboardForms;
using ICTProfilingV3.DeliveriesForms;
using ICTProfilingV3.LoginForms;
using ICTProfilingV3.LookUpTables;
using ICTProfilingV3.MOForms;
using ICTProfilingV3.PGNForms;
using ICTProfilingV3.PPEInventoryForms;
using ICTProfilingV3.PurchaseRequestForms;
using ICTProfilingV3.RepairForms;
using ICTProfilingV3.ReportForms;
using ICTProfilingV3.StandardPRForms;
using ICTProfilingV3.TechSpecsForms;
using ICTProfilingV3.TicketRequestForms;
using ICTProfilingV3.ToolForms;
using ICTProfilingV3.UsersForms;
using Models.Enums;
using Models.Managers.User;
namespace ICTProfilingV3
{
    public partial class frmMain : BaseRibbonForm , IDisposeUC
    {
        private IICTUserManager userManager;
        private IICTRoleManager roleManager;
        public readonly IUCManager<Control> _ucManager;
        private string Version = "";
        public frmMain()
        {
            InitializeComponent();
            userManager = new ICTUserManager();
            roleManager = new ICTRoleManager();
            _ucManager = new UCManager<Control>(mainPanel);
            ForceUserUpdate();

            if (!UpdateThread.IsBusy)
                UpdateThread.RunWorkerAsync();
        }

        private void ForceUserUpdate()
        {
            if (System.Deployment.Application.ApplicationDeployment.IsNetworkDeployed)
            {
                System.Deployment.Application.ApplicationDeployment cd = System.Deployment.Application.ApplicationDeployment.CurrentDeployment;
                string version = cd.CurrentVersion.ToString();
                if (UpdateHelpers.InstallUpdateSyncWithInfo())
                {
                    MessageBox.Show($@"This Version of EPiSv3 is Outdated, the Application will now Automatically Update", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    
                    Properties.Settings.Default.LastVersion = version;
                    Properties.Settings.Default.Save();

                    var frm = new frmUpdater() { NewVersion = version };
                    frm.ShowDialog();
                }
            }
        }

        public async void setRoleDesignations()
        {
            var user = await userManager.FindUserAsync(UserStore.UserId);
            if (user.Roles == null) return;
            var role = await roleManager.GetRoleDesignations(user.Roles.FirstOrDefault().RoleId);
            if(role == null) return;    

            foreach (RibbonPage page in ribbon.Pages)
            {
                if(page.Tag != null)
                    if(role.Select(s => s.Designation).ToList().Contains(ConvertTagToEnum(page.Tag.ToString()))) page.Visible = true;
                    else page.Visible = false;
                foreach(RibbonPageGroup group in page.Groups)
                {
                    if (group.Tag != null)
                        if (role.Select(s => s.Designation).ToList().Contains(ConvertTagToEnum(group.Tag.ToString()))) group.Visible = true;
                        else group.Visible = false;
                    foreach (BarButtonItemLink button in group.ItemLinks)
                    {
                        var btn = button.Item;
                        if (btn.Tag != null)
                            if (role.Select(s => s.Designation).ToList().Contains(ConvertTagToEnum(btn.Tag.ToString()))) btn.Visibility = BarItemVisibility.Always;
                            else btn.Visibility = BarItemVisibility.Never;
                    }
                }
            }
        }
        public Designation ConvertTagToEnum(string tag)
        {
            if (Enum.TryParse(tag, out Designation enumValue))
                return enumValue;
            else
                return Designation.Unknown;
        }

        public void SetUser(string name, string position)
        {
            lblEmployee.Caption = name;
            lblPosition.Caption = position;
        }

        private void btnTARequest_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //DisposeUC(mainPanel);
            //mainPanel.Controls.Add(new UCTARequestDashboard()
            //{
            //    Dock = DockStyle.Fill
            //});
            _ucManager.ShowUCSystemDetails(e.Item.Name, new UCTARequestDashboard { Dock = DockStyle.Fill }, null);
        }

        private void btnSuppliers_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var frm = new frmSuppliers();
            frm.ShowDialog();
        }

        private void btnEquipment_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var frm = new frmEquipment();
            frm.ShowDialog();
        }

        private void btnEquipmentSpecs_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var frm = new frmEquipmentSpecs();
            frm.ShowDialog();
        }

        private void btnBrand_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var frm = new frmEquipmentBrand();
            frm.ShowDialog();   
        }

        private void btnModels_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var frm = new frmEquipmentModels();
            frm.ShowDialog();
        }

        private void btnDeliveries_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _ucManager.ShowUCSystemDetails(e.Item.Name, new UCDeliveries { Dock = DockStyle.Fill }, null);
        }

        private void btnUsers_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var frm = new frmUsers();
            frm.ShowDialog();
        }

        private void btnUserLevels_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var frm = new frmUserRoles();
            frm.ShowDialog();
        }

        private void btnActionTree_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var frm = new frmActionTree();
            frm.ShowDialog();
        }

        private void btnActionList_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var frm = new frmActionList();
            frm.ShowDialog();
        }

        private void btnStaff_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var frm = new frmStaff();
            frm.ShowDialog();
        }

        private void btnTechSpecsBasis_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var frm = new frmTechSpecsBasis();
            frm.ShowDialog();
        }

        private void btnTechSpecs_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _ucManager.ShowUCSystemDetails(e.Item.Name, new UCTechSpecs()
            {
                Dock = DockStyle.Fill,
                IsTechSpecs = true
            }, new string[] { "IsTechSpecs" });
        }

        private void btnPPE_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _ucManager.ShowUCSystemDetails(e.Item.Name, new UCPPEs()
            {
                Dock = DockStyle.Fill
            }, null);
        }

        private void btnRepair_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _ucManager.ShowUCSystemDetails(e.Item.Name, new UCRepair()
            {
                Dock = DockStyle.Fill
            }, null);
        }

        private void btnCAS_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _ucManager.ShowUCSystemDetails(e.Item.Name, new UCCAS()
            {
                Dock = DockStyle.Fill
            }, null);
        }

        private void btnStandardPR_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var frm = new frmStandardPRList();
            frm.ShowDialog();
        }

        private void btnVPR_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _ucManager.ShowUCSystemDetails(e.Item.Name, new UCPR()
            {
                Dock = DockStyle.Fill
            }, null);
        }

        private void btnRoutedActions_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _ucManager.ShowUCSystemDetails(e.Item.Name, new UCRoutedActions()
            {
                Dock = DockStyle.Fill
            }, null);
        }

        private void btnReport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var frm = new frmAccomplishmentReport();
            frm.ShowDialog();
        }

        private void btnLogout_ItemPressed(object sender, BackstageViewItemEventArgs e)
        {
            backstageViewControl1.Close();
            lblEmployee.Caption = string.Empty;
            lblPosition.Caption = string.Empty;
            DisposeUC(mainPanel);
            var frm = new frmLogin(this);
            frm.ShowDialog();
        }

        private void btnRepairSpecs_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _ucManager.ShowUCSystemDetails(e.Item.Name, new UCTechSpecs()
            {
                IsTechSpecs = false,
                Dock = DockStyle.Fill
            }, new string[] { "IsTechSpecs" });
        }

        private void btnPGNAccounts_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _ucManager.ShowUCSystemDetails(e.Item.Name, new UCPGNAccounts()
            {
                Dock = DockStyle.Fill
            }, null);
        }

        private void btnPGNOffices_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var frm = new frmPGNOffices();
            frm.ShowDialog();
        }

        private void btnRequests_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _ucManager.ShowUCSystemDetails(e.Item.Name, new UCPGNRequests()
            {
                Dock = DockStyle.Fill
            }, null);
        }

        private void UpdateThread_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            var updateNow = true;
            while (updateNow)
            {
                Thread.Sleep(1000);
                if (UpdateHelpers.InstallUpdateSyncWithInfo())
                    Invoke(new Action(() =>
                    {
                        updateNow = false;
                        lblUpdate.Caption = @"EPiSv3: Update available(the system is updating)";
                        UpdateHelpers.applicationDeployment.UpdateCompleted += (se, ev) =>
                        {
                            Properties.Settings.Default.LastVersion = Version;
                            Properties.Settings.Default.Save();

                            new frmUpdateNotification().ShowDialog(this);
                        };
                        UpdateHelpers.applicationDeployment.UpdateProgressChanged += (se, ev) =>
                        {
                            lblUpdate.Caption =
                                $@"EPiSv3: Update available(the system is updating) {ev.ProgressPercentage}%";
                        };
                        UpdateHelpers.applicationDeployment.UpdateAsync();
                    }));
            }
        }

        private void btnQueue_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _ucManager.ShowUCSystemDetails(e.Item.Name, new UCUserTasks()
            {
                Dock = DockStyle.Fill,
                FromQueue = true
            }, new string[] {"FromQueue"});
        }

        private void btnUserTasks_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _ucManager.ShowUCSystemDetails(e.Item.Name, new UCUserTasks()
            {
                Dock = DockStyle.Fill
            }, null);
        }

        private void btnChanges_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var frm = new frmViewChangeLogs();
            frm.ShowDialog();
        }

        private void btnChangelogs_ItemPressed(object sender, BackstageViewItemEventArgs e)
        {
            if (System.Deployment.Application.ApplicationDeployment.IsNetworkDeployed)
            {
                System.Deployment.Application.ApplicationDeployment cd = System.Deployment.Application.ApplicationDeployment.CurrentDeployment;
                var version = cd.CurrentVersion.ToString();

                var frm = new frmChangelogs(version);
                frm.ShowDialog();
            }
        }

        private void btnMOAccounts_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _ucManager.ShowUCSystemDetails(e.Item.Name, new UCMOAccounts()
            {
                Dock = DockStyle.Fill
            }, null);
        }

        private void btnDashboard_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _ucManager.ShowUCSystemDetails(e.Item.Name, new UCDashboard()
            {
                Dock = DockStyle.Fill
            }, null);
        }

        public void DisposeUC(Control parent)
        {
            foreach (Control ctrl in parent.Controls)
            {
                ctrl.Dispose();
                GC.Collect();
            }
            parent.Controls.Clear();
        }

        private void btnQuarterlyReport_ItemClick(object sender, ItemClickEventArgs e)
        {
            var frm = new frmQuarterlyReport();
            frm.ShowDialog();
        }

        private void btnNavigateBack_ItemClick(object sender, ItemClickEventArgs e)
        {
            _ucManager.NavigateBack();
        }

        private void btnNavigateForward_ItemClick(object sender, ItemClickEventArgs e)
        {
            _ucManager.NavigateForward();
        }

        private void btnRefreshControl_ItemClick(object sender, ItemClickEventArgs e)
        {
            _ucManager.RefreshControl();
        }

        private async void frmMain_Load(object sender, EventArgs e)
        {
            var frm = new frmLogin(this);

            if (UserStore.ArugmentCredentialsDto == null) frm.ShowDialog(this);
            else await frm.Login(UserStore.ArugmentCredentialsDto.Username, UserStore.ArugmentCredentialsDto.Password);
        }
    }
}