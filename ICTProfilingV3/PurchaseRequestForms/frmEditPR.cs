using ICTProfilingV3.BaseClasses;
using ICTProfilingV3.DataTransferModels.ViewModels;
using ICTProfilingV3.Interfaces;
using ICTProfilingV3.Services.Employees;
using Models.Entities;
using Models.Enums;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ICTProfilingV3.PurchaseRequestForms
{
    public partial class frmEditPR : BaseForm
    {
        private readonly IPurchaseReqService _purchaseReqService;
        private PurchaseRequest _purchaseReq;
        private SaveType _saveType;

        public frmEditPR(IPurchaseReqService purchaseReqService)
        {
            _purchaseReqService = purchaseReqService;
            InitializeComponent();
            LoadDropdowns();
        }

        public void InitForm(PurchaseRequest purchaseRequest = null)
        {
            if(_purchaseReq == null)
            {
                var lastId = _purchaseReqService.GetAll().OrderByDescending(x => x.Id).FirstOrDefault()?.Id ?? 0;
                lblPRNo.Text = (lastId + 1).ToString("D5");
                lblModifyType.Text = "Add PR";
                _purchaseReq = new PurchaseRequest();
                _saveType = SaveType.Insert;
            }
            else
            {
                _purchaseReq = purchaseRequest;
                _saveType = SaveType.Update;
                LoadDetails();
            }
        }
        private void LoadDetails()
        {
            txtDate.DateTime = (DateTime)_purchaseReq.DateCreated;
            if(_purchaseReq.DateCreated != null) txtDate.DateTime = (DateTime)_purchaseReq.DateCreated;
            slueEmployee.EditValue = _purchaseReq.ChiefId;
            txtPRNo.Text = _purchaseReq.PRNo;
            lblPRNo.Text = _purchaseReq.Id.ToString();
        }

        private void LoadDropdowns()
        {
            slueEmployee.Properties.DataSource = HRMISEmployees.GetChiefOfOffices();
        }

        private void slueEmployee_EditValueChanged(object sender, System.EventArgs e)
        {
            var row = (EmployeesViewModel)slueEmployee.Properties.View.GetFocusedRow();
            if (row == null) row = HRMISEmployees.GetEmployeeById(_purchaseReq.ChiefId);

            txtRequestingOfficeChiefPos.Text = row.Position;
            txtRequestedByOffice.Text = row.Office;
            txtRequestedByDivision.Text = row.Division;
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            if (_saveType == SaveType.Update) await UpdatePR();
            else await Insert();
            this.Close();
        }

        private async Task Insert()
        {
            _purchaseReq.PRNo = txtPRNo.Text;
            _purchaseReq.DateCreated = txtDate.DateTime;
            _purchaseReq.ChiefId = (long?)slueEmployee.EditValue;
            await _purchaseReqService.AddAsync(_purchaseReq);
        }

        private async Task UpdatePR()
        {
            var pr = await _purchaseReqService.GetByIdAsync(_purchaseReq.Id);
            pr.PRNo = txtPRNo.Text;
            pr.DateCreated = txtDate.DateTime;
            pr.ChiefId = (long?)slueEmployee.EditValue;
            await _purchaseReqService.SaveChangesAsync();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}