using DevExpress.XtraEditors;
using Models.Entities;
using Models.HRMISEntites;
using Models.Repository;
using Models.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using Models.Enums;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ICTProfilingV3.TechSpecsForms;
using ICTProfilingV3.DeliveriesForms;
using ICTProfilingV3.RepairForms;
using DevExpress.Data.Filtering;

namespace ICTProfilingV3.TicketRequestForms
{
    public partial class UCTARequestDashboard : DevExpress.XtraEditors.XtraUserControl
    {
        private readonly IUnitOfWork unitOfWork;
        public string filterText { get; set; }  
        public UCTARequestDashboard()
        {
            InitializeComponent();
            unitOfWork = new UnitOfWork();
            LoadTickets();
        }

        private void LoadTickets()
        {
            var res = unitOfWork.TicketRequestRepo.GetAll(x => x.Deliveries,
                x => x.TechSpecs,
                x => x.Repairs).ToList().Select(x => new TicketRequestViewModel
                {
                    TicketRequest = x
                });
            gcTARequests.DataSource = res.ToList();
        }

        private void btnNewRequest_Click(object sender, EventArgs e)
        {
            var frm = new frmTypeOfRequest();
            frm.ShowDialog();

            LoadTickets();
        }

        private void hplProcess_Click(object sender, EventArgs e)
        {
            var row = (TicketRequestViewModel)gridRequest.GetFocusedRow();
            if(row.TicketRequest.RequestType == RequestType.TechSpecs) NavigateToProcess(new UCTechSpecs() { 
                Dock = DockStyle.Fill, 
                filterText = row.TicketRequest.Id.ToString() });

            if (row.TicketRequest.RequestType == RequestType.Deliveries) NavigateToProcess(new UCDeliveries()
            {
                Dock = DockStyle.Fill,
                filterText = row.TicketRequest.Id.ToString()
            });

            if (row.TicketRequest.RequestType == RequestType.Repairs) NavigateToProcess(new UCRepair()
            {
                Dock = DockStyle.Fill,
                filterText = row.TicketRequest.Id.ToString()
            });
        }

        private void NavigateToProcess(Control uc)
        {
            var main = Application.OpenForms["frmMain"] as frmMain;
            main.mainPanel.Controls.Clear();

            main.mainPanel.Controls.Add(uc);
        }

        private void UCTARequestDashboard_Load(object sender, EventArgs e)
        {
            if (filterText != null) gridRequest.ActiveFilterCriteria = new BinaryOperator("TicketRequest.Id",filterText);
        }
    }
}
