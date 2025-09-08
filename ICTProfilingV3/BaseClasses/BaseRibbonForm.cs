using DevExpress.XtraBars.Ribbon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICTProfilingV3.BaseClasses
{
    public class BaseRibbonForm : RibbonForm
    {
        public BaseRibbonForm()
        {
            SetFormIcon();
        }
        private void SetFormIcon()
        {
            this.IconOptions.Icon = Properties.Resources.episv2_logo;
        }
    }
}
