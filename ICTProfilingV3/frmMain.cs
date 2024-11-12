using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors.Mask;
using EntityManager.Managers.User;
using ICTProfilingV3.ActionsForms;
using ICTProfilingV3.CustomerActionSheetForms;
using ICTProfilingV3.DashboardForms;
using ICTProfilingV3.DeliveriesForms;
using ICTProfilingV3.LoginForms;
using ICTProfilingV3.LookUpTables;
using ICTProfilingV3.PPEInventoryForms;
using ICTProfilingV3.PurchaseRequestForms;
using ICTProfilingV3.RepairForms;
using ICTProfilingV3.StandardPRForms;
using ICTProfilingV3.TechSpecsForms;
using ICTProfilingV3.TicketRequestForms;
using ICTProfilingV3.UsersForms;
using Models.HRMISEntites;

namespace ICTProfilingV3
{
    public partial class frmMain : RibbonForm
    {
        public frmMain()
        {
            InitializeComponent();
            var frm = new frmLogin();
            frm.ShowDialog();
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
                Dock = DockStyle.Fill
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

        private void btnLogout_SelectedChanged(object sender, BackstageViewItemEventArgs e)
        {

        }

        private void btnRoutedActions_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            mainPanel.Controls.Clear();
            mainPanel.Controls.Add(new UCRoutedActions()
            {
                Dock = DockStyle.Fill
            });
        }
    }
}