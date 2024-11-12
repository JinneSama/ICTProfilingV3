using DevExpress.XtraEditors;
using DevExpress.XtraReports.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ICTProfilingV3.ReportForms
{
    public partial class frmReportViewer : DevExpress.XtraEditors.XtraForm
    {
        public XtraReport xtraReport;
        public frmReportViewer()
        {
            InitializeComponent();
        }

        public frmReportViewer(XtraReport xtraReport)
        {
            InitializeComponent();
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