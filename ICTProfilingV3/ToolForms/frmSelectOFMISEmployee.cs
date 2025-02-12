using ICTProfilingV3.BaseClasses;
using Models.Models;
using Models.OFMISEntities;
using Models.ViewModels;

namespace ICTProfilingV3.ToolForms
{
    public partial class frmSelectOFMISEmployee : BaseForm
    {
        public EmployeesViewModel OFMISEmployee { get; set; }
        public frmSelectOFMISEmployee()
        {
            InitializeComponent();
            LoadEmployees();
        }

        public void LoadEmployees()
        {
            var employees = OFMISEmployees.GetAllEmployees();
            slueEmployee.Properties.DataSource = employees; 
        }

        private void slueEmployee_EditValueChanged(object sender, System.EventArgs e)
        {
            var row = (EmployeesViewModel)slueEmployee.Properties.View.GetFocusedRow();
            if (row == null) return;

            txtOffice.Text = row.Office.ToString();
            txtPosition.Text = row.Position.ToString();

            OFMISEmployee = row;
        }

        private void btnCancel_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, System.EventArgs e)
        {
            var row = (EmployeesViewModel)slueEmployee.Properties.View.GetFocusedRow();
            if (row == null) return;

            txtOffice.Text = row.Office.ToString();
            txtPosition.Text = row.Position.ToString();

            OFMISEmployee = row;

            this.Close();
        }
    }
}