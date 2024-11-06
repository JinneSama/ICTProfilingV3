using EntityManager.Managers;
using EntityManager.Managers.User;
using ICTProfilingV3.ActionsForms;
using Models.Entities;
using Models.Enums;
using Models.HRMISEntites;
using Models.Models;
using Models.Repository;
using Models.ViewModels;
using System;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace ICTProfilingV3.TechSpecsForms
{
    public partial class UCTechSpecs : DevExpress.XtraEditors.XtraUserControl
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IICTUserManager userManager;
        public UCTechSpecs()
        {
            InitializeComponent();
            unitOfWork = new UnitOfWork();
            userManager = new ICTUserManager();
        }

        private async Task LoadAll()
        {
            await LoadTechSpecs();
            LoadDropdowns();
        }
        private void LoadDropdowns()
        {
            var users = userManager.GetUsers();
            sluePreparedBy.Properties.DataSource = users;
            slueReviewedBy.Properties.DataSource = users;
            slueNotedBy.Properties.DataSource = users;
        }


        private async Task LoadDetails()
        {
            var row = (TechSpecsViewModel)gridTechSpecs.GetFocusedRow();
            var ts = await unitOfWork.TechSpecsRepo.FindAsync(x => x.Id == row.Id);
            txtDate.DateTime = ts.DateRequested ?? DateTime.MinValue;
            rdbtnGender.SelectedIndex = (int)ts.ReqByGender;
            txtContactNo.Text = ts.ContactNo;
            checkEditApprovedPR.Checked = ts.RequestBasedApprovedPR ?? false;
            checkEditApprovedAPP.Checked = ts.RequestBasedApprovedAPP ?? false;
            checkEditApprovedAIP.Checked = ts.RequestBasedApprovedAIP ?? false;
            checkEditApprovedPPMP.Checked = ts.RequestBasedApprovedPPMP ?? false;
            checkEditRequestLetter.Checked = ts.RequestBasedRequestLetter ?? false;
            checkEditForReplacement.Checked = ts.RequestBasedForReplacement ?? false;
            sluePreparedBy.EditValue = (string)ts.PreparedById;
            slueReviewedBy.EditValue = (string)ts.ReviewedById;
            slueNotedBy.EditValue = (string)ts.NotedById;

            var reqByUser = HRMISEmployees.GetEmployeeById(ts.ReqById);
            var chief = HRMISEmployees.GetChief(reqByUser?.Office , reqByUser?.Division);

            txtRequestingOfficeChief.Text = HRMISEmployees.GetEmployeeById(chief?.ChiefId)?.Employee;
            txtRequestingOfficeChiefPos.Text = HRMISEmployees.GetEmployeeById(chief?.ChiefId)?.Position;
            txtRequestedByEmployeeName.Text = reqByUser?.Employee;
            txtRequestedByPos.Text = reqByUser?.Position;
            txtRequestedByOffice.Text = reqByUser?.Office;
            txtRequestedByDivision.Text = reqByUser?.Division;
        }

        private async Task LoadTechSpecs()
        {
            var res = await unitOfWork.TechSpecsRepo.GetAll(x => x.TicketRequest).ToListAsync();
            var ts = res.Select(x => new TechSpecsViewModel
            {
                Id = x.Id,
                Status = x.TicketRequest.TicketStatus,
                DateAccepted = x.DateAccepted,
                DateRequested = x.DateRequested,
                Office = HRMISEmployees.GetEmployeeById(x.ReqById)?.Office,
                Division = HRMISEmployees.GetEmployeeById(x.ReqById)?.Division,
                TicketId = x.TicketRequest.Id
            });
            gcTechSpecs.DataSource = new BindingList<TechSpecsViewModel>(ts.ToList());
        }

        private async void gridTechSpecs_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            await LoadDetails();
            await LoadSpecs();
            LoadActions();
        }

        private async Task LoadSpecs()
        {
            var row = (TechSpecsViewModel)gridTechSpecs.GetFocusedRow();
            lblTSId.Text = row.Id.ToString();
            var ts = await unitOfWork.TechSpecsRepo.FindAsync(x => x.Id == row.Id);
            tabRequestedSpecs.Controls.Clear();
            tabRequestedSpecs.Controls.Add(new UCRequestedTechSpecs(ts)
            {
                Dock = System.Windows.Forms.DockStyle.Fill
            });
        }

        private void LoadActions()
        {
            var row = (TechSpecsViewModel)gridTechSpecs.GetFocusedRow();
            tabAction.Controls.Clear();
            tabAction.Controls.Add(new UCActions(new ActionType { Id = row.Id, RequestType = Models.Enums.RequestType.TechSpecs})
            {
                Dock = System.Windows.Forms.DockStyle.Fill
            });
        }

        private async void btnEdit_Click(object sender, System.EventArgs e)
        {
            var row = (TechSpecsViewModel)gridTechSpecs.GetFocusedRow();
            var ts = await unitOfWork.TechSpecsRepo.FindAsync(x => x.Id == row.Id);
            var frm = new frmAddEditTechSpecs(ts , unitOfWork);
            frm.ShowDialog();
        }

        private async void UCTechSpecs_Load(object sender, EventArgs e)
        {
            await LoadAll();
        }
    }
}
