using DevExpress.XtraEditors;
using DevExpress.XtraReports.UI;
using ICTProfilingV3.BaseClasses;
using System;

namespace ICTProfilingV3.ReportForms
{
    public partial class frmReportViewer : BaseForm
    {
        public XtraReport xtraReport;
        public frmReportViewer()
        {
            InitializeComponent();
        }

        public void InitForm(XtraReport xtraReport)
        {
            this.xtraReport = xtraReport;
        }

        private void frmReportViewer_Load(object sender, EventArgs e)
        {
            xtraReport.CreateDocument();
            documentViewer1.DocumentSource = xtraReport;
            documentViewer1.Refresh();
        }
    }
}