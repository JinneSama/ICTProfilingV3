using DevExpress.XtraEditors;
using Models.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ICTProfilingV3.TicketRequestForms
{
    public partial class UCTARequestDashboard : DevExpress.XtraEditors.XtraUserControl
    {
        IUnitOfWork unitOfWork;
        public UCTARequestDashboard()
        {
            InitializeComponent();
            unitOfWork = new UnitOfWork();
            LoadTickets();
        }

        private void LoadTickets()
        {
            var res = unitOfWork.TicketRequestRepo.GetAll();
            gcTARequests.DataSource = res.ToList();
        }

        private void btnNewRequest_Click(object sender, EventArgs e)
        {
            var frm = new frmTypeOfRequest();
            frm.ShowDialog();
        }
    }
}
