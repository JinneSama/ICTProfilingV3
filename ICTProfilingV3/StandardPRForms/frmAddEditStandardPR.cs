using ICTProfilingV3.BaseClasses;
using Models.Entities;
using Models.Enums;
using Models.HRMISEntites;
using Models.Managers.User;
using Models.Repository;
using Models.ViewModels;
using System;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace ICTProfilingV3.StandardPRForms
{
    public partial class frmAddEditStandardPR : BaseForm
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PurchaseRequest _purchaseRequest;
        private long? ChiefId;
        private bool IsSave = false;
        public frmAddEditStandardPR()
        {
            InitializeComponent();
            _unitOfWork = new UnitOfWork();
            _purchaseRequest = CreatePR();
            LoadDropdowns();
            lblPRId.Text = _purchaseRequest.Id.ToString();
        }

        public frmAddEditStandardPR(PurchaseRequest pr)
        {
            InitializeComponent();
            IsSave = true;
            _unitOfWork = new UnitOfWork();
            _purchaseRequest = pr;
            LoadDropdowns();
            LoadDetails();
            lblPRId.Text = _purchaseRequest.Id.ToString();
        }

        private void LoadDropdowns()
        {
            slueEmployee.Properties.DataSource = HRMISEmployees.GetEmployees();
            lueQuarter.Properties.DataSource = Enum.GetValues(typeof(PRQuarter)).Cast<PRQuarter>().Select(x => new
            {
                Id = x,
                QuarterName = EnumHelper.GetEnumDescription(x)
            });
        }

        private async void LoadDetails()
        {
            await LoadStandardPRSpecs();
            txtDate.DateTime = (DateTime)(_purchaseRequest.DateCreated ?? DateTime.Now);
            if(_purchaseRequest.ReqById != null) slueEmployee.EditValue = _purchaseRequest.ReqById;
            txtPRNo.Text = _purchaseRequest.PRNo;
            lueQuarter.EditValue = _purchaseRequest.Quarter;
        }

        private async Task LoadStandardPRSpecs()
        {
            var pr = _unitOfWork.PRStandardPRSpecsRepo.FindAllAsync(x => x.PurchaseRequestId == _purchaseRequest.Id,
                x => x.StandardPRSpecs,
                x => x.StandardPRSpecs.StandardPRSpecsDetails,
                x => x.StandardPRSpecs.EquipmentSpecs,
                x => x.StandardPRSpecs.EquipmentSpecs.Equipment)
                .Select(x => new PRStandardPRSpecsViewModel
                {
                    Equipment = x.StandardPRSpecs.EquipmentSpecs.Equipment.EquipmentName,
                    StandardPRSpecs = x.StandardPRSpecs,
                    StandardPRSpecsDetails = x.StandardPRSpecs.StandardPRSpecsDetails,
                    ItemNo = x.ItemNo,
                    Quantity = x.Quantity,
                    TotalCost = x.TotalCost ?? x.StandardPRSpecs.UnitCost,
                    Id = x.Id
                });
            var dataPR = await pr.ToListAsync();
            gcPR.DataSource = new BindingList<PRStandardPRSpecsViewModel>(dataPR);  
        }

        private PurchaseRequest CreatePR()
        {
            var pr = new PurchaseRequest();
            _unitOfWork.PurchaseRequestRepo.Insert(pr);
            _unitOfWork.Save();
            return pr;
        }

        private async void btnAddSpecs_Click(object sender, System.EventArgs e)
        {
            PRQuarter quarter = (PRQuarter)lueQuarter.EditValue;
            var frm = new frmStandardPRList(_purchaseRequest.Id, quarter);
            frm.ShowDialog();

            await LoadStandardPRSpecs();
        }

        private async void frmAddEditStandardPR_Load(object sender, EventArgs e)
        {
            await LoadStandardPRSpecs();
        }

        private async void gridSpecs_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            var row = (PRStandardPRSpecsViewModel)gridSpecs.GetFocusedRow();
            await UpdateRow(row);
        }

        private async Task UpdateRow(PRStandardPRSpecsViewModel row)
        {
            var res = await _unitOfWork.PRStandardPRSpecsRepo.FindAsync(x => x.Id == row.Id);
            if (res == null) return;

            res.ItemNo = row.ItemNo;
            res.Quantity = row.Quantity;
            res.TotalCost = row.Quantity * row.StandardPRSpecs.UnitCost;

            await _unitOfWork.SaveChangesAsync();
            await LoadStandardPRSpecs();
        }

        private void btnExpandDetail_Click(object sender, EventArgs e)
        {
            var focusedRow = gridSpecs.FocusedRowHandle;
            gridSpecs.SetMasterRowExpanded(focusedRow, !gridSpecs.GetMasterRowExpanded(focusedRow));
        }

        private async void frmAddEditStandardPR_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            if (!IsSave) await DeletePR();
        }

        private async Task DeletePR()
        {
            _unitOfWork.PurchaseRequestRepo.DeleteByEx(x => x.Id == _purchaseRequest.Id);
            await _unitOfWork.SaveChangesAsync();
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            IsSave = true;
            await SavePR();
            this.Close();
        }

        private async Task SavePR()
        {
            var pr = await _unitOfWork.PurchaseRequestRepo.FindAsync(x => x.Id == _purchaseRequest.Id);
            pr.ReqById = (long?)slueEmployee.EditValue;
            pr.DateCreated = txtDate.DateTime;
            pr.PRNo = txtPRNo.Text;
            pr.Quarter = (PRQuarter)lueQuarter.EditValue;
            pr.ChiefId = ChiefId;

            await _unitOfWork.SaveChangesAsync();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void slueEmployee_EditValueChanged(object sender, EventArgs e)
        {
            var empId = (long?)slueEmployee.EditValue;
            var row = HRMISEmployees.GetEmployeeById(empId);
            if (row == null) return;

            txtRequestedByPos.Text = row.Position;
            txtRequestedByOffice.Text = row.Office;
            txtRequestedByDivision.Text = row.Division;
            ChiefId = HRMISEmployees.GetChief(row.Office, row.Division, empId).ChiefId;
        }
    }
}