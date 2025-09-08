using DevExpress.XtraEditors;
using ICTProfilingV3.BaseClasses;
using ICTProfilingV3.DeliveriesForms;
using ICTProfilingV3.RepairForms;
using ICTProfilingV3.TechSpecsForms;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace ICTProfilingV3.TicketRequestForms
{
    public partial class frmTypeOfRequest : BaseForm
    {
        private readonly IServiceProvider _serviceProvider;
        public frmTypeOfRequest(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            InitializeComponent();
        }

        private async void btnDeliveries_Click(object sender, EventArgs e)
        {
            var frm = _serviceProvider.GetRequiredService<frmAddEditDeliveries>();
            await frm.InitForm();
            frm.ShowDialog();
            this.Close();   
        }

        private async void btnTechSpecs_Click(object sender, EventArgs e)
        {
            var frm = _serviceProvider.GetRequiredService<frmAddEditTechSpecs>();
            await frm.InitForTSForm();
            frm.ShowDialog();
            this.Close();
        }

        private async void btnRepair_Click(object sender, EventArgs e)
        {
            var frm = _serviceProvider.GetRequiredService<frmAddEditRepair>();
            await frm.InitForm();
            frm.ShowDialog();
            this.Close();
        }
    }
}