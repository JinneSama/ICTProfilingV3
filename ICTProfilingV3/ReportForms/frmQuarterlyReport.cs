using ICTProfilingV3.BaseClasses;
using Models.Enums;
using System;
using System.Data;
using System.Linq;

namespace ICTProfilingV3.ReportForms
{
    public partial class frmQuarterlyReport : BaseForm
    {
        public frmQuarterlyReport()
        {
            InitializeComponent();
            LoadDropdowns();
        }
        private void LoadDropdowns()
        {
            lueQuarter.Properties.DataSource = Enum.GetValues(typeof(PRQuarter)).Cast<PRQuarter>().Select(x => new
            {
                Id = x,
                QuarterName = EnumHelper.GetEnumDescription(x)
            });

            lueYear.Properties.DataSource = Enum.GetValues(typeof(Year)).Cast<Year>().Select(x => new
            {
                Id = x,
                YearName = EnumHelper.GetEnumDescription(x)
            });
            lueYear.EditValue = Enum.GetValues(typeof(Year)).Cast<Year>().Max();

            lueProcess.Properties.DataSource = Enum.GetValues(typeof(RequestType)).Cast<RequestType>().Select(x => new
            {
                Id = x,
                ProcessName = EnumHelper.GetEnumDescription(x)
            });
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}