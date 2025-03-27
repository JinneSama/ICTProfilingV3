using DevExpress.XtraEditors.Controls;
using ICTProfilingV3.BaseClasses;
using Models.Entities;
using Models.Enums;
using Models.HRMISEntites;
using Models.Managers.User;
using Models.Repository;
using Models.ViewModels;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ICTProfilingV3.PGNForms
{
    public partial class frmAddEditRequest : BaseForm
    {
        private IUnitOfWork unitOfWork;
        private PGNRequests request;
        private bool IsSave = false;
        public frmAddEditRequest()
        {
            InitializeComponent();
            unitOfWork = new UnitOfWork();
            CreateRequest();
            LoadDropdowns();
            LoadScanDocs();
        }
        public frmAddEditRequest(PGNRequestViewModel request)
        {
            InitializeComponent();
            IsSave = true;
            unitOfWork = new UnitOfWork();
            this.request = request.PGNRequest;
            LoadDropdowns();
            LoadDetails(request);
            LoadScanDocs();
        }

        private void CreateRequest()
        {
            var req = new PGNRequests { CreatedById = UserStore.UserId , DateCreated = DateTime.Now };
            unitOfWork.PGNRequestsRepo.Insert(req); 
            unitOfWork.Save();
            request = req;
        }

        private void LoadDetails(PGNRequestViewModel request)
        {
            if(request.PGNRequest.RequestDate != null) txtDate.DateTime = (DateTime)request.PGNRequest.RequestDate;
            lueCommType.EditValue = request.PGNRequest.CommunicationType;
            txtSubject.Text = request.PGNRequest.Subject;
            slueSignatory.EditValue = request.PGNRequest.SignatoryId;
        }

        private void LoadScanDocs()
        {
            gcScanDocs.Controls.Clear();
            gcScanDocs.Controls.Add(new UCPGNScanDocuments(request)
            {
                Dock = System.Windows.Forms.DockStyle.Fill
            });
        }

        private async void LoadDropdowns()
        {
            lueCommType.Properties.DataSource = Enum.GetValues(typeof(CommunicationType)).Cast<CommunicationType>().Select(x => new { CommType = x });
            var employees = HRMISEmployees.GetEmployees();
            slueSignatory.Properties.DataSource = employees;

            lblEPiSNo.Text = string.Join("-", "PGN", request.Id);
            txtDate.DateTime = DateTime.Now;

            var user = await unitOfWork.UsersRepo.FindAsync(x => x.Id == request.CreatedById);
            if (user == null) return;
            lblDateCreated.Text = request.DateCreated.Value.ToShortDateString();
            lblCreatedby.Text = user.UserName;
            lblAttachedBy.Text = user.FullName;
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            IsSave = true;
            await Save();
            this.Close();
        }
        private async Task Save()
        {
            var res = await unitOfWork.PGNRequestsRepo.FindAsync(x => x.Id == request.Id);
            if (res == null) return;

            res.RequestDate = txtDate.DateTime;
            res.CommunicationType = (CommunicationType)lueCommType.EditValue;
            res.SignatoryId = (long?)slueSignatory.EditValue;
            res.Subject = txtSubject.Text;
            unitOfWork.PGNRequestsRepo.Update(res);
            await unitOfWork.SaveChangesAsync();
        }

        private async void frmAddEditRequest_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            if (IsSave) return;
            await DeleteRequest();
        }

        private async Task DeleteRequest()
        {
            unitOfWork.PGNDocumentsRepo.DeleteRange(x => x.PGNRequestId == request.Id);
            unitOfWork.PGNRequestsRepo.DeleteByEx(x => x.Id == request.Id);
            await unitOfWork.SaveChangesAsync();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void slueSignatory_EditValueChanged(object sender, EventArgs e)
        {
            var row = (EmployeesViewModel)slueSignatory.Properties.View.GetFocusedRow();
            if (row == null) row = HRMISEmployees.GetEmployeeById(request.SignatoryId);

            if (row == null) return;
            txtPosition.Text = row.Position;
            txtOffice.Text = row.Office + " " + row.Division;
            txtUsername.Text = row.Username;
        }
    }
}