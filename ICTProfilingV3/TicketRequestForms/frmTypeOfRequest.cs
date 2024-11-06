using DevExpress.XtraEditors;
using ICTProfilingV3.DeliveriesForms;
using ICTProfilingV3.RepairForms;
using ICTProfilingV3.TechSpecsForms;
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
    public partial class frmTypeOfRequest : DevExpress.XtraEditors.XtraForm
    {
        public frmTypeOfRequest()
        {
            InitializeComponent();
        }

        private void btnDeliveries_Click(object sender, EventArgs e)
        {
            var frm = new frmAddEditDeliveries();
            frm.ShowDialog();
            this.Close();   
        }

        private void btnTechSpecs_Click(object sender, EventArgs e)
        {
            var frm = new frmAddEditTechSpecs();
            frm.ShowDialog();
            this.Close();
        }

        private void btnRepair_Click(object sender, EventArgs e)
        {
            var frm = new frmAddEditRepair();
            frm.ShowDialog();
            this.Close();
        }
    }
}