using Helpers.Interfaces;
using System;
using System.Windows.Forms;

namespace ICTProfilingV3.DashboardForms
{
    public partial class UCDashboard : DevExpress.XtraEditors.XtraUserControl, IDisposeUC
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
            DisposeUC(tabRequest);
            tabRequest.Controls.Add(new UCRequestDashboard()
            {
                Dock = DockStyle.Fill
            });
        }

        private void LoadRepairDashboard()
        {
            DisposeUC(tabRepairs);
            tabRepairs.Controls.Add(new UCRepairDashboard()
            {
                Dock = DockStyle.Fill
            });
        }

        private void LoadPGNDashboard()
        {
            DisposeUC(tabPGN);
            tabPGN.Controls.Add(new UCPGNDashboard()
            {
                Dock = DockStyle.Fill
            });
        }

        private void LoadM365Dashboard()
        {
            DisposeUC(tabM365);
            tabM365.Controls.Add(new UCM365Dashboard()
            {
                Dock = DockStyle.Fill
            });
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
    }
}
