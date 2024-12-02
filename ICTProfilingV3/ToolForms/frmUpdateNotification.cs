using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ICTProfilingV3.ToolForms
{
    public partial class frmUpdateNotification : DevExpress.XtraEditors.XtraForm
    {
        public frmUpdateNotification()
        {
            InitializeComponent();
        }

        private void btnLater_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnNow_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }
    }
}