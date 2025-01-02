using System.Windows.Forms;

namespace ICTProfilingV3.DashboardForms
{
    public partial class UCDashboard : DevExpress.XtraEditors.XtraUserControl
    {
        public UCDashboard()
        {
            InitializeComponent();
            LoadRequestDashboard();
        }

        private void LoadRequestDashboard()
        {
            tabRequest.Controls.Clear();
            tabRequest.Controls.Add(new UCRequestDashboard()
            {
                Dock = DockStyle.Fill
            });
        }
    }
}
