using ICTProfilingV3.BaseClasses;
using ICTProfilingV3.ToolForms;
using Models.HRMISEntites;
using Models.Repository;
using Models.ViewModels;
using System.Linq;
using System.Threading.Tasks;

namespace ICTProfilingV3.DeliveriesForms
{
    public partial class frmInitialFindings : BaseForm
    {
        private readonly DeliveriesViewModel deliveries;
        private EmployeesViewModel ofmisEmployee;
        public frmInitialFindings(DeliveriesViewModel deliveries)
        {
            InitializeComponent();
            this.deliveries = deliveries;
            LoadDropdowns();
            LoadDetails();
        }
        private void LoadDropdowns()
        {
            var uow = new UnitOfWork();
            slueSupplierName.Properties.DataSource = uow.SupplierRepo.GetAll().ToList();
            var employees = HRMISEmployees.GetEmployees();
            slueEmployee.Properties.DataSource = employees;
        }
        private void LoadDetails()
        {
            if(deliveries.Deliveries.FindingsRequestedById == null) slueEmployee.EditValue = deliveries.Deliveries.ReqByChiefId;
            else slueEmployee.EditValue = deliveries.Deliveries.FindingsRequestedById;

            txtPONo.Text = deliveries.Deliveries.PONo;
            if (deliveries.Deliveries.POServed == null) dePOServed.EditValue = null;
            else dePOServed.DateTime = (System.DateTime)deliveries.Deliveries.POServed;

            deDeliveryDate.DateTime = (System.DateTime)deliveries.Deliveries.DeliveredDate;
            slueSupplierName.EditValue = deliveries.Deliveries.SupplierId;
            memoStatus.Text = deliveries.Deliveries.FindingsStatus;
            memoActionTaken.Text = deliveries.Deliveries.FindingsActionTaken;
            lblEpisNo.Text = deliveries.Id.ToString();
        }

        private void btnOFMIS_Click(object sender, System.EventArgs e)
        {
            var frm = new frmSelectOFMISEmployee();
            frm.ShowDialog();

            if (frm.OFMISEmployee == null) return;
            txtRequestedBy.Text = frm.OFMISEmployee.Employee;
            ofmisEmployee = frm.OFMISEmployee;
        }

        private void slueEmployee_EditValueChanged(object sender, System.EventArgs e)
        {
            var row = (EmployeesViewModel)slueEmployee.Properties.View.GetFocusedRow();
            if (row == null)
            {
                txtRequestedBy.Visible = false;
                return;
            }

            txtRequestedBy.Visible = true;
            txtRequestedBy.Text = row.Employee;
        }

        private async void btnSave_Click(object sender, System.EventArgs e)
        {
            await UpdateDeliveries();
        }

        private async Task UpdateDeliveries()
        {
            var uow = new UnitOfWork();
            var del = await uow.DeliveriesRepo.FindAsync(x => x.Id == deliveries.Deliveries.Id);

            del.FindingsRequestedById = (long)(slueEmployee.EditValue == null ? ofmisEmployee.Id : slueEmployee.EditValue);
            
            if(dePOServed.EditValue == null) del.POServed = null;
            else del.POServed = dePOServed.DateTime;

            del.FindingsStatus = memoStatus.Text;  
            del.FindingsActionTaken = memoActionTaken.Text;

            await uow.SaveChangesAsync();
        }

        private void btnPreview_Click(object sender, System.EventArgs e)
        {

        }
    }
}