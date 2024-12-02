using Models.HRMISEntites;
using Models.ViewModels;
using System.Linq;

namespace ICTProfilingV3.PGNForms
{
    public partial class frmSelectFromHRMIS : DevExpress.XtraEditors.XtraForm
    {
        public EmployeesViewModel Employee { get; set; }
        public frmSelectFromHRMIS()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            var res = HRMISEmployees.GetEmployees();
            gcAccs.DataSource = res.ToList();
        }

        private void btnSelect_Click(object sender, System.EventArgs e)
        {
            var row = (EmployeesViewModel)gvAccs.GetFocusedRow();
            Employee = row;
            this.Close();
        }
    }
}