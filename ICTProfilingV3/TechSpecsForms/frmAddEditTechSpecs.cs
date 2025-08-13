using ICTProfilingV3.ActionsForms;
using ICTProfilingV3.BaseClasses;
using ICTProfilingV3.Core.Common;
using ICTProfilingV3.DataTransferModels.Models;
using ICTProfilingV3.DataTransferModels.ViewModels;
using ICTProfilingV3.DeliveriesForms;
using ICTProfilingV3.Services.Employees;
using Microsoft.Extensions.DependencyInjection;
using Models.Entities;
using Models.Enums;
using Models.Repository;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ICTProfilingV3.TechSpecsForms
{
    public partial class frmAddEditTechSpecs : BaseForm
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly UserStore _userStore;
        private readonly IServiceProvider _serviceProvider;

        private TechSpecs _techSpecs;
        private bool IsSave = false; 
        private SaveType SaveType;
        public TechSpecs RepairTechSpecs { get; set; }
        public frmAddEditTechSpecs(UserStore userStore, IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _userStore = userStore;
            InitializeComponent();
            unitOfWork = new UnitOfWork();
            LoadDropdowns();
        }

        public void InitForRepairForm(Repairs repair = null)
        {
            CreateRepairTicket(repair);
        }

        public void InitForTSForm(TechSpecs ts = null)
        {
            if (ts == null)
                CreateTicket();
            else
            {
                _techSpecs = ts;
                SaveType = SaveType.Update;
                LoadTechSpecs();
            }
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
                DateCreated = DateTime.Now,
                TicketStatus = TicketStatus.Accepted,
                RequestType = RequestType.TechSpecs,
                IsRepairTechSpecs = true,
                CreatedBy = _userStore.UserId
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
                DateCreated = DateTime.Now,
                TicketStatus = TicketStatus.Accepted,
                RequestType = RequestType.TechSpecs,
                CreatedBy = _userStore.UserId
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
            groupRequestedSpecs.Controls.Add(new UCRequestedTechSpecs(_techSpecs, false)
            {
                Dock = System.Windows.Forms.DockStyle.Fill
            });
        }

        private async void frmAddEditTechSpecs_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            if (!IsSave && SaveType == SaveType.Insert) await DeleteTechSpecs();
        }

        private async Task DeleteTechSpecs()
        {
            unitOfWork.TicketRequestRepo.DeleteByEx(x => x.Id == _techSpecs.Id);
            await unitOfWork.SaveChangesAsync();
            unitOfWork.TechSpecsRepo.DeleteByEx(x => x.Id == _techSpecs.Id);
            await unitOfWork.SaveChangesAsync();
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            IsSave = true;
            RepairTechSpecs = _techSpecs;
            await SaveTechSpecs();
            this.Close();
            if (SaveType == SaveType.Update) return;
            var actionType = new ActionType
            {
                Id = _techSpecs.Id,
                RequestType = RequestType.TechSpecs
            };

            var frm = _serviceProvider.GetRequiredService<frmDocAction>();
            frm.SetActionBehavior(actionType, SaveType.Insert, null, null);
            frm.ShowDialog();
        }

        private async Task SaveTechSpecs()
        {
            var clickedEmployee = (EmployeesViewModel)slueEmployee.Properties.View.GetFocusedRow();
            if (clickedEmployee == null) clickedEmployee = HRMISEmployees.GetEmployeeById(_techSpecs.ReqById);

            var ts = await unitOfWork.TechSpecsRepo.FindAsync(x => x.Id == _techSpecs.Id);
            ts.DateAccepted = DateTime.Now;
            ts.DateRequested = txtDate.DateTime;
            ts.ReqById = (long)slueEmployee.EditValue;
            ts.ReqByChiefId = (long)HRMISEmployees.GetChief(clickedEmployee.Office , clickedEmployee.Division, (long)slueEmployee.EditValue).ChiefId;
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
            unitOfWork.TechSpecsRepo.Update(ts);
            await unitOfWork.SaveChangesAsync();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void slueEmployee_EditValueChanged(object sender, EventArgs e)
        {
            var clickedEmployee = (EmployeesViewModel)slueEmployee.Properties.View.GetFocusedRow();
            if (clickedEmployee == null) clickedEmployee = HRMISEmployees.GetEmployeeById(_techSpecs.ReqById);
            var chief = HRMISEmployees.GetChief(clickedEmployee?.Office, clickedEmployee?.Division, _techSpecs.ReqById);
            var ChiefDetails = HRMISEmployees.GetEmployeeById(chief?.ChiefId);
            txtRequestingOfficeChief.Text = ChiefDetails?.Employee;
            txtRequestingOfficeChiefPos.Text = ChiefDetails?.Position;

            txtRequestedByPos.Text = clickedEmployee?.Position;
            txtRequestedByOffice.Text = clickedEmployee?.Office;
            txtRequestedByDivision.Text = clickedEmployee?.Division;
        }

        private void frmAddEditTechSpecs_Load(object sender, EventArgs e)
        {
            if (SaveType == SaveType.Update) LoadDetails();
        }
    }
}