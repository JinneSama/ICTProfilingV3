using Helpers.Update;
using System;
using System.ComponentModel;
using System.Threading;
using System.Windows.Forms;

namespace ICTProfilingV3.ToolForms
{
    public partial class frmUpdater : DevExpress.XtraEditors.XtraForm
    {
        public string NewVersion { get; set; }
        public frmUpdater()
        {
            InitializeComponent();

            if (!backgroundWorker1.IsBusy)
                backgroundWorker1.RunWorkerAsync();
        }

        private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            var updateNow = true;
            while (updateNow)
            {
                Thread.Sleep(1000);
                if (UpdateHelpers.InstallUpdateSyncWithInfo())
                {
                    Invoke(new Action(() =>
                    {
                        updateNow = false;
                        UpdateHelpers.applicationDeployment.UpdateCompleted += (se, ev) =>
                        {
                            MessageBox.Show($@"Application Updated to Version: {NewVersion}, the Application will now Restart, Press OK", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            Application.Restart();
                        };
                        UpdateHelpers.applicationDeployment.UpdateProgressChanged += (se, ev) =>
                        {
                            progressUpdate.Position = ev.ProgressPercentage;
                        };
                        UpdateHelpers.applicationDeployment.UpdateAsync();
                    }));
                }
                else
                {
                    updateNow = false;
                }
            }
        }
    }
}