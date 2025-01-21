using System;

namespace ICTProfilingV3.DebugTools
{
    public partial class frmVersionSetter : DevExpress.XtraEditors.XtraForm
    {
        public frmVersionSetter()
        {
            InitializeComponent();
            txtBrand.Text = Properties.Settings.Default.LastVersion;
        }

        private void btnAddBrand_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.LastVersion = txtBrand.Text;
            Properties.Settings.Default.Save();

            this.Close();
        }
    }
}