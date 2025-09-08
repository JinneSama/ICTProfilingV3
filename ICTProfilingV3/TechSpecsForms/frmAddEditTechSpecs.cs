using ICTProfilingV3.ActionsForms;
using ICTProfilingV3.BaseClasses;
using ICTProfilingV3.Core.Common;
using ICTProfilingV3.DataTransferModels.Models;
using ICTProfilingV3.DataTransferModels.ViewModels;
using ICTProfilingV3.Interfaces;
using ICTProfilingV3.Services.Employees;
using Microsoft.Extensions.DependencyInjection;
using Models.Entities;
using Models.Enums;
using System;
using System.Threading.Tasks;

namespace ICTProfilingV3.TechSpecsForms
{
    public partial class frmAddEditTechSpecs : BaseForm
    {
        private readonly IICTUserManager _userManager;
        private readonly ITechSpecsService _tsService;
        private readonly IControlMapper<TechSpecs> _tsMapper;
        private readonly UserStore _userStore;
        private readonly IServiceProvider _serviceProvider;

        private TechSpecs _techSpecs;
        private bool _isSave = false; 
        private SaveType _saveType;
        public TechSpecs _repairTechSpecs { get; set; }
        public frmAddEditTechSpecs(ITechSpecsService tsService, UserStore userStore, IServiceProvider serviceProvider, IControlMapper<TechSpecs> tsMapper,
            IICTUserManager userManager)
        {
            _serviceProvider = serviceProvider;
            _tsMapper = tsMapper;
            _userStore = userStore;
            _tsService = tsService;
            _userManager = userManager;

            InitializeComponent();
            LoadDropdowns();
        }

        public async Task InitForRepairForm(Repairs repair = null)
        {
            await CreateRepairTicket(repair);
        }

        public async Task InitForTSForm(TechSpecs ts = null)
        {
            if (ts == null)
                await CreateTicket();
            else
            {
                _techSpecs = ts;
                _saveType = SaveType.Update;
                LoadTechSpecs();
            }
        }

        private void LoadDropdowns()
        {
            var res = HRMISEmployees.GetEmployees();
            slueReqById.Properties.DataSource = res;

            var users = _userManager.GetUsers();
            sluePreparedById.Properties.DataSource = users;
            slueReviewedById.Properties.DataSource = users;
            slueNotedById.Properties.DataSource = users;
        }

        private void LoadDetails()
        {
            if (!(_saveType == SaveType.Update)) return;

            _tsMapper.MapControl(_techSpecs, groupControl1, groupControl2, groupControl3);
        }
        private async Task CreateRepairTicket(Repairs repair)
        {
            var ts = await _tsService.AddRepairTechSpecsAsync(new TechSpecs());

            _techSpecs = ts;
            lblRepair.Visible = true;
            lblRepairNo.Visible = true;
            lblRepairNo.Text = repair.Id.ToString();
            LoadTechSpecs();
        }

        private async Task CreateTicket()
        {
            var ts = await _tsService.AddAsync(new TechSpecs());

            _techSpecs = ts;
            LoadTechSpecs();
        }

        private void LoadTechSpecs()
        {
            lblRequestNo.Text = _techSpecs.Id.ToString();
            LoadTechSpecsICTSpecs();
        }

        private void LoadTechSpecsICTSpecs()
        {
            var navigation = _serviceProvider.GetRequiredService<IControlNavigator<UCRequestedTechSpecs>>();
            navigation.NavigateTo(groupRequestedSpecs, act => act.InitUC(_techSpecs, false));
        }

        private async void frmAddEditTechSpecs_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            if (_isSave && _saveType == SaveType.Insert) await DeleteTechSpecs();
        }

        private async Task DeleteTechSpecs()
        {
            await _tsService.DeleteAsync(_techSpecs.Id);
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            _isSave = true;
            _repairTechSpecs = _techSpecs;
            await SaveTechSpecs();
            Close();

            if (_saveType == SaveType.Update) return;
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
            var clickedEmployee = (EmployeesViewModel)slueReqById.Properties.View.GetFocusedRow();
            if (clickedEmployee == null) clickedEmployee = HRMISEmployees.GetEmployeeById(_techSpecs.ReqById);

            var ts = await _tsService.GetByIdAsync(_techSpecs.Id);
            ts.ReqByChiefId = (long)HRMISEmployees.GetChief(clickedEmployee.Office, clickedEmployee.Division, (long)slueReqById.EditValue).ChiefId;
            _tsMapper.MapToEntity(ts, groupControl1, groupControl2, groupControl3);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void slueEmployee_EditValueChanged(object sender, EventArgs e)
        {
            var clickedEmployee = (EmployeesViewModel)slueReqById.Properties.View.GetFocusedRow();
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
            if (_saveType == SaveType.Update) LoadDetails();
        }
    }
}