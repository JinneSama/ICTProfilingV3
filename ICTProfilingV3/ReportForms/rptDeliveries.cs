using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;

namespace ICTProfilingV3.ReportForms
{
    public partial class rptDeliveries : DevExpress.XtraReports.UI.XtraReport
    {
        public string Office { get; set; }
        public rptDeliveries()
        {
            InitializeComponent();
        }

        private void rptDeliveries_AfterPrint(object sender, EventArgs e)
        {
        }

        private void lblOffice_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            (sender as XRLabel).Text = Office;
        }
    }
}
