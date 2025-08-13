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

        private void btnDeliveries_Click(object sender, EventArgs e)
        {
            var frm = _serviceProvider.GetRequiredService<frmAddEditDeliveries>();
            frm.InitForm();
            frm.ShowDialog();
            this.Close();   
        }

        private void btnTechSpecs_Click(object sender, EventArgs e)
        {
            var frm = _serviceProvider.GetRequiredService<frmAddEditTechSpecs>();
            frm.InitForTSForm();
            frm.ShowDialog();
            this.Close();
        }

        private void btnRepair_Click(object sender, EventArgs e)
        {
            var frm = _serviceProvider.GetRequiredService<frmAddEditRepair>();
            frm.InitForm();
            frm.ShowDialog();
            this.Close();
        }
    }
}