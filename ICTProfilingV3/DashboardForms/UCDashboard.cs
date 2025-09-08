using ICTProfilingV3.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows.Forms;

namespace ICTProfilingV3.DashboardForms
{
    public partial class UCDashboard : DevExpress.XtraEditors.XtraUserControl
    {
        private readonly IServiceProvider _serviceProvider;
        public UCDashboard(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            InitializeComponent();
            LoadRequestDashboard();
            LoadRepairDashboard();
            LoadPGNDashboard();
            LoadM365Dashboard();
        }

        private void LoadRequestDashboard()
        {
            var navigation = _serviceProvider.GetRequiredService<IControlNavigator<UCRequestDashboard>>();
            navigation.NavigateTo(tabRequest);
        }

        private void LoadRepairDashboard()
        {
            var navigation = _serviceProvider.GetRequiredService<IControlNavigator<UCRepairDashboard>>();
            navigation.NavigateTo(tabRequest);
        }

        private void LoadPGNDashboard()
        {
            tabPGN.Controls.Add(new UCPGNDashboard()
            {
                Dock = DockStyle.Fill
            });
        }

        private void LoadM365Dashboard()
        {
            tabM365.Controls.Add(new UCM365Dashboard()
            {
                Dock = DockStyle.Fill
            });
        }

    }
}
