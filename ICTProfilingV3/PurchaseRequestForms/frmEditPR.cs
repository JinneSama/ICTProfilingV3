using Models.Entities;
using Models.HRMISEntites;
using Models.Repository;
using Models.ViewModels;
using System;
using System.Threading.Tasks;

namespace ICTProfilingV3.PurchaseRequestForms
{
    public partial class frmEditPR : DevExpress.XtraEditors.XtraForm
    {
        private readonly PurchaseRequest PR;
        private readonly IUnitOfWork unitOfWork;

        public frmEditPR(PurchaseRequest _PR, IUnitOfWork uow)
        {
            InitializeComponent();
            PR = _PR;
            unitOfWork = uow;
            LoadDropdowns();
        }

        private void LoadDetails()
        {
            txtDate.DateTime = (DateTime)PR.DateCreated;
            if(PR.DateCreated != null) txtDate.DateTime = (DateTime)PR.DateCreated;
            slueEmployee.EditValue = PR.ChiefId;
            txtPRNo.Text = PR.PRNo;
            lblPRNo.Text = PR.Id.ToString();
        }

        private void LoadDropdowns()
        {
            slueEmployee.Properties.DataSource = HRMISEmployees.GetChiefOfOffices();
        }

        private void slueEmployee_EditValueChanged(object sender, System.EventArgs e)
        {
            var row = (EmployeesViewModel)slueEmployee.Properties.View.GetFocusedRow();
            if (row == null) row = HRMISEmployees.GetEmployeeById(PR.ChiefId);

            txtRequestingOfficeChiefPos.Text = row.Position;
            txtRequestedByOffice.Text = row.Office;
            txtRequestedByDivision.Text = row.Division;
        }

        private void frmEditPR_Load(object sender, System.EventArgs e)
        {
            LoadDetails();
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            await Save();
            this.Close();
        }

        private async Task Save()
        {
            var pr = await unitOfWork.PurchaseRequestRepo.FindAsync(x => x.Id == PR.Id);
            pr.PRNo = txtPRNo.Text;
            pr.DateCreated = txtDate.DateTime;
            pr.ChiefId = (long?)slueEmployee.EditValue;

            await unitOfWork.SaveChangesAsync();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}