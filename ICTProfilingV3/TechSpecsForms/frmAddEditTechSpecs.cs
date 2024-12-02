using Models.Entities;
using Models.Enums;
using Models.HRMISEntites;
using Models.Managers;
using Models.Managers.User;
using Models.Repository;
using Models.ViewModels;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ICTProfilingV3.TechSpecsForms
{
    public partial class frmAddEditTechSpecs : DevExpress.XtraEditors.XtraForm, ITicketStatus
    {
        private readonly IUnitOfWork unitOfWork;
        private TechSpecs _techSpecs;
        private bool IsSave = false; 
        private SaveType SaveType;
        public TechSpecs RepairTechSpecs { get; set; }
        public frmAddEditTechSpecs()
        {
            InitializeComponent();
            unitOfWork = new UnitOfWork();
            LoadDropdowns();
            CreateTicket();
        }
        public frmAddEditTechSpecs(Repairs repair)
        {
            InitializeComponent();
            unitOfWork = new UnitOfWork();
            LoadDropdowns();
            CreateRepairTicket(repair);
        }

        public frmAddEditTechSpecs(TechSpecs ts, IUnitOfWork uow)
        {
            InitializeComponent();
            unitOfWork = uow;
            _techSpecs = ts;
            SaveType = SaveType.Update;
            LoadDropdowns();
            LoadTechSpecs();
        }

        private void LoadDropdowns()
        {
            var res = HRMISEmployees.GetEmployees();
            slueEmployee.Properties.DataSource = res;

            var users = unitOfWork.UsersRepo.GetAll().ToList();
            sluePreparedBy.Properties.DataSource = users;
            slueReviewedBy.Properties.DataSource = users;
            slueNotedBy.Properties.DataSource = users;
        }

        private void LoadDetails()
        {
            if (!(SaveType == SaveType.Update)) return;

            txtDate.DateTime = _techSpecs.DateRequested ?? DateTime.MinValue;
            rdbtnGender.SelectedIndex = (int)_techSpecs.ReqByGender;
            txtContactNo.Text = _techSpecs.ContactNo;
            checkEditApprovedPR.Checked = _techSpecs.RequestBasedApprovedPR ?? false;
            checkEditApprovedAPP.Checked = _techSpecs.RequestBasedApprovedAPP ?? false;
            checkEditApprovedAIP.Checked = _techSpecs.RequestBasedApprovedAIP ?? false;
            checkEditApprovedPPMP.Checked = _techSpecs.RequestBasedApprovedPPMP ?? false;
            checkEditRequestLetter.Checked = _techSpecs.RequestBasedRequestLetter ?? false;
            checkEditForReplacement.Checked = _techSpecs.RequestBasedForReplacement ?? false;
            sluePreparedBy.EditValue = (string)_techSpecs.PreparedById;
            slueReviewedBy.EditValue = (string)_techSpecs.ReviewedById;
            slueNotedBy.EditValue = (string)_techSpecs.NotedById;
            slueEmployee.EditValue = _techSpecs.ReqById;
        }
        private void CreateRepairTicket(Repairs repair)
        {
            var ticket = new TicketRequest()
            {
                DateCreated = DateTime.UtcNow,
                TicketStatus = TicketStatus.Accepted,
                RequestType = RequestType.TechSpecs,
                IsRepairTechSpecs = true
            };
            unitOfWork.TicketRequestRepo.Insert(ticket);
            unitOfWork.Save();

            var techSpecs = new TechSpecs()
            {
                Id = ticket.Id
            };

            unitOfWork.TechSpecsRepo.Insert(techSpecs);
            unitOfWork.Save();

            _techSpecs = techSpecs;
            lblRepair.Visible = true;
            lblRepairNo.Visible = true;
            lblRepairNo.Text = repair.Id.ToString();
            LoadTechSpecs();
        }

        private void CreateTicket()
        {
            var ticket = new TicketRequest()
            {
                DateCreated = DateTime.UtcNow,
                TicketStatus = TicketStatus.Accepted,
                RequestType = RequestType.TechSpecs
            };
            unitOfWork.TicketRequestRepo.Insert(ticket);
            unitOfWork.Save();

            var techSpecs = new TechSpecs()
            {
                Id = ticket.Id
            };
            unitOfWork.TechSpecsRepo.Insert(techSpecs);
            unitOfWork.Save();

            _techSpecs = techSpecs;
            LoadTechSpecs();
        }

        private void LoadTechSpecs()
        {
            lblRequestNo.Text = _techSpecs.Id.ToString();
            LoadTechSpecsICTSpecs();
        }

        private void LoadTechSpecsICTSpecs()
        {
            groupRequestedSpecs.Controls.Clear();
            groupRequestedSpecs.Controls.Add(new UCRequestedTechSpecs(_techSpecs)
            {
                Dock = System.Windows.Forms.DockStyle.Fill
            });
        }

        private async void frmAddEditTechSpecs_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            if (!IsSave) await DeleteTechSpecs();
        }

        private async Task DeleteTechSpecs()
        {
            unitOfWork.TicketRequestRepo.DeleteByEx(x => x.Id == _techSpecs.Id);
            unitOfWork.TechSpecsRepo.DeleteByEx(x => x.Id == _techSpecs.Id);
            await unitOfWork.SaveChangesAsync();
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            IsSave = true;
            RepairTechSpecs = _techSpecs;
            await SaveTechSpecs();
            this.Close();
        }

        private async Task SaveTechSpecs()
        {
            var clickedEmployee = (EmployeesViewModel)slueEmployee.Properties.View.GetFocusedRow();
            if (clickedEmployee == null) clickedEmployee = HRMISEmployees.GetEmployeeById(_techSpecs.ReqById);

            var ts = await unitOfWork.TechSpecsRepo.FindAsync(x => x.Id == _techSpecs.Id);
            ts.DateAccepted = DateTime.UtcNow;
            ts.DateRequested = txtDate.DateTime;
            ts.ReqById = (long)slueEmployee.EditValue;
            ts.ReqByChiefId = HRMISEmployees.GetChief(clickedEmployee.Office , clickedEmployee.Division).ChiefId;
            ts.ReqByGender = (Gender)rdbtnGender.SelectedIndex;
            ts.ContactNo = txtContactNo.Text;
            ts.RequestBasedApprovedPR = checkEditApprovedPR.Checked;
            ts.RequestBasedApprovedAPP = checkEditApprovedAPP.Checked;
            ts.RequestBasedApprovedAIP = checkEditApprovedAIP.Checked;
            ts.RequestBasedApprovedPPMP = checkEditApprovedPPMP.Checked;
            ts.RequestBasedRequestLetter = checkEditRequestLetter.Checked;
            ts.RequestBasedForReplacement = checkEditForReplacement.Checked;
            ts.PreparedById = (string)sluePreparedBy.EditValue;
            ts.ReviewedById = (string)slueReviewedBy.EditValue;
            ts.NotedById = (string)slueNotedBy.EditValue;

            await unitOfWork.SaveChangesAsync();
            await ModifyStatus(TicketStatus.Accepted, ts.Id);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void slueEmployee_EditValueChanged(object sender, EventArgs e)
        {
            var clickedEmployee = (EmployeesViewModel)slueEmployee.Properties.View.GetFocusedRow();
            if (clickedEmployee == null) clickedEmployee = HRMISEmployees.GetEmployeeById(_techSpecs.ReqById);
            var chief = HRMISEmployees.GetChief(clickedEmployee.Office, clickedEmployee.Division);
            var ChiefDetails = HRMISEmployees.GetEmployeeById(chief.ChiefId);
            txtRequestingOfficeChief.Text = ChiefDetails.Employee;
            txtRequestingOfficeChiefPos.Text = ChiefDetails.Position;

            txtRequestedByPos.Text = clickedEmployee.Position;
            txtRequestedByOffice.Text = clickedEmployee.Office;
            txtRequestedByDivision.Text = clickedEmployee.Division;
        }

        private void frmAddEditTechSpecs_Load(object sender, EventArgs e)
        {
            if (SaveType == SaveType.Update) LoadDetails();
        }

        public async Task ModifyStatus(TicketStatus status, int ticketId)
        {
            var ticketStatus = new TicketRequestStatus
            {
                Status = status,
                DateStatusChanged = DateTime.UtcNow,
                ChangedByUserId = UserStore.UserId,
                TicketRequestId = ticketId
            };
            unitOfWork.TicketRequestStatusRepo.Insert(ticketStatus);
            await unitOfWork.SaveChangesAsync();
        }
    }
}