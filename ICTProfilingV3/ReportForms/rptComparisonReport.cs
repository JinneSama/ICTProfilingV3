using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;

namespace ICTProfilingV3.ReportForms
{
    public partial class rptComparisonReport : DevExpress.XtraReports.UI.XtraReport
    {
        public rptComparisonReport()
        {
            InitializeComponent();
        }

        private void rptComparisonReport_AfterPrint(object sender, EventArgs e)
        {
        }

        private void rptComparisonReport_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }
    }
}
