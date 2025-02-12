using System;
using System.Drawing;
using System.Windows.Forms;

namespace ICTProfilingV3.BaseClasses
{
    public class BaseForm : DevExpress.XtraEditors.XtraForm
    {
        public BaseForm()
        {
            SetFormIcon();
        }

        private void SetFormIcon()
        {

            this.IconOptions.Icon = Properties.Resources.episv2_logo;
        }
    }
}
