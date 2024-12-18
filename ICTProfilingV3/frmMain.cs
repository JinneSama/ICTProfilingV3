using System;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using DevExpress.Xpo;
using DevExpress.XtraBars.Ribbon;
using Helpers.NetworkFolder;
using Helpers.Update;
using ICTProfilingV3.ActionsForms;
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
using static System.Net.WebRequestMethods;
namespace ICTProfilingV3
{
    public partial class frmMain : RibbonForm
    {
        public frmMain()
        {
            InitializeComponent();
            var frm = new frmLogin(this);
            frm.ShowDialog();

            if (!UpdateThread.IsBusy)
                UpdateThread.RunWorkerAsync();
        }

        public void SetUser(string name, string position)
        {
            lblEmployee.Caption = name;
            lblPosition.Caption = position;
        }

        private void btnTARequest_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            mainPanel.Controls.Clear();
            mainPanel.Controls.Add(new UCTARequestDashboard()
            {
                Dock = DockStyle.Fill
            });
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
            mainPanel.Controls.Clear();
            mainPanel.Controls.Add(new UCDeliveries()
            {
                Dock = DockStyle.Fill
            });
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
            mainPanel.Controls.Clear();
            mainPanel.Controls.Add(new UCTechSpecs()
            {
                Dock = DockStyle.Fill,
                IsTechSpecs = true
            });
        }

        private void btnPPE_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            mainPanel.Controls.Clear();
            mainPanel.Controls.Add(new UCPPEs()
            {
                Dock = DockStyle.Fill
            });
        }

        private void btnRepair_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            mainPanel.Controls.Clear();
            mainPanel.Controls.Add(new UCRepair()
            {
                Dock = DockStyle.Fill
            });
        }

        private void btnCAS_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            mainPanel.Controls.Clear();
            mainPanel.Controls.Add(new UCCAS()
            {
                Dock = DockStyle.Fill
            });
        }

        private void btnStandardPR_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var frm = new frmStandardPRList();
            frm.ShowDialog();
        }

        private void btnVPR_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            mainPanel.Controls.Clear();
            mainPanel.Controls.Add(new UCPR()
            {
                Dock = DockStyle.Fill
            });
        }

        private void btnRoutedActions_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            mainPanel.Controls.Clear();
            mainPanel.Controls.Add(new UCRoutedActions()
            {
                Dock = DockStyle.Fill
            });
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
            mainPanel.Controls.Clear();
            var frm = new frmLogin(this);
            frm.ShowDialog();
        }

        private void btnRepairSpecs_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            mainPanel.Controls.Clear();
            mainPanel.Controls.Add(new UCTechSpecs()
            {
                IsTechSpecs = false,
                Dock = DockStyle.Fill
            });
        }

        private void btnPGNAccounts_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            mainPanel.Controls.Clear();
            mainPanel.Controls.Add(new UCPGNAccounts()
            {
                Dock = DockStyle.Fill
            });
        }

        private void btnPGNOffices_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var frm = new frmPGNOffices();
            frm.ShowDialog();
        }

        private void btnRequests_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            mainPanel.Controls.Clear();
            mainPanel.Controls.Add(new UCPGNRequests()
            {
                Dock = DockStyle.Fill
            });
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
            mainPanel.Controls.Clear();
            mainPanel.Controls.Add(new UCQueue()
            {
                Dock = DockStyle.Fill
            });
        }

        private void btnUserTasks_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            mainPanel.Controls.Clear();
            mainPanel.Controls.Add(new UCUserTasks()
            {
                Dock = DockStyle.Fill
            });
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
            mainPanel.Controls.Clear();
            mainPanel.Controls.Add(new UCMOAccounts()
            {
                Dock = DockStyle.Fill
            });
        }

        private void btnDashboard_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
        }
    }
}