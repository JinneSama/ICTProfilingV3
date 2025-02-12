using ICTProfilingV3.BaseClasses;
using System;
using System.Windows.Forms;

namespace ICTProfilingV3.ToolForms
{
    public partial class frmUpdateNotification : BaseForm
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