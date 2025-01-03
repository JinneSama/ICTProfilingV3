using System.Windows.Forms;

namespace ICTProfilingV3.DashboardForms
{
    public partial class UCDashboard : DevExpress.XtraEditors.XtraUserControl
    {
        public UCDashboard()
        {
            InitializeComponent();
            LoadRequestDashboard();
            LoadRepairDashboard();
            LoadPGNDashboard();
            LoadM365Dashboard();
        }

        private void LoadRequestDashboard()
        {
            tabRequest.Controls.Clear();
            tabRequest.Controls.Add(new UCRequestDashboard()
            {
                Dock = DockStyle.Fill
            });
        }

        private void LoadRepairDashboard()
        {
            tabRepairs.Controls.Clear();
            tabRepairs.Controls.Add(new UCRepairDashboard()
            {
                Dock = DockStyle.Fill
            });
        }

        private void LoadPGNDashboard()
        {
            tabPGN.Controls.Clear();
            tabPGN.Controls.Add(new UCPGNDashboard()
            {
                Dock = DockStyle.Fill
            });
        }

        private void LoadM365Dashboard()
        {
            tabM365.Controls.Clear();
            tabM365.Controls.Add(new UCM365Dashboard()
            {
                Dock = DockStyle.Fill
            });
        }
    }
}
