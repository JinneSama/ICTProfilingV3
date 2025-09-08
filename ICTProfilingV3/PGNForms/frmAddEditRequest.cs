using ICTProfilingV3.BaseClasses;
using ICTProfilingV3.Core.Common;
using ICTProfilingV3.DataTransferModels.ViewModels;
using ICTProfilingV3.Interfaces;
using ICTProfilingV3.Services.Employees;
using Microsoft.Extensions.DependencyInjection;
using Models.Entities;
using Models.Enums;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ICTProfilingV3.PGNForms
{
    public partial class frmAddEditRequest : BaseForm
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IICTUserManager _userManager;
        private readonly IPGNService _pgnService;
        private readonly UserStore _userStore;
        private PGNRequests _request;
        private bool _isSave = false;
        public frmAddEditRequest(IICTUserManager userManager, IServiceProvider serviceProvider, IPGNService pgnService, UserStore userStore)
        {
            _serviceProvider = serviceProvider;
            _userManager = userManager;
            _pgnService = pgnService;
            _userStore = userStore;
            InitializeComponent();
            
            LoadScanDocs();
        }

        public async Task InitForm(PGNRequestViewModel request = null)
        {
            if(request == null)
            {
                await CreateRequest();
                LoadDropdowns();
            }
            else
            {
                _isSave = true;
                _request = request.PGNRequest;
                LoadDetails(request);
                LoadDropdowns();
            }
        }

        private async Task CreateRequest()
        {
            var req = await _pgnService.PGNRequestsService.AddAsync(new PGNRequests { CreatedById = _userStore.UserId, DateCreated = DateTime.Now });
            _request = req;
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
            var navigation = _serviceProvider.GetRequiredService<IControlNavigator<UCPGNScanDocuments>>();
            navigation.NavigateTo(gcScanDocs, act => act.InitUC(_request));
        }

        private async void LoadDropdowns()
        {
            lueCommType.Properties.DataSource = Enum.GetValues(typeof(CommunicationType)).Cast<CommunicationType>().Select(x => new { CommType = x });
            var employees = HRMISEmployees.GetEmployees();
            slueSignatory.Properties.DataSource = employees;

            lblEPiSNo.Text = string.Join("-", "PGN", _request.Id);
            txtDate.DateTime = DateTime.Now;

            var user = await _userManager.FindUserAsync(_request.CreatedById);
            if (user == null) return;
            lblDateCreated.Text = _request.DateCreated.Value.ToShortDateString();
            lblCreatedby.Text = user.UserName;
            lblAttachedBy.Text = user.FullName;
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            _isSave = true;
            await Save();
            this.Close();
        }
        private async Task Save()
        {
            var res = await _pgnService.PGNRequestsService.GetByIdAsync(_request.Id);
            if (res == null) return;

            res.RequestDate = txtDate.DateTime;
            res.CommunicationType = (CommunicationType)lueCommType.EditValue;
            res.SignatoryId = (long?)slueSignatory.EditValue;
            res.Subject = txtSubject.Text;
            await _pgnService.PGNRequestsService.SaveChangesAsync();
        }

        private async void frmAddEditRequest_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            if (_isSave) return;
            await DeleteRequest();
        }

        private async Task DeleteRequest()
        {
            await _pgnService.PGNDocumentService.DeleteRangeAsync(x => x.PGNRequestId == _request.Id);
            await _pgnService.PGNRequestsService.DeleteAsync(_request.Id);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void slueSignatory_EditValueChanged(object sender, EventArgs e)
        {
            var row = (EmployeesViewModel)slueSignatory.Properties.View.GetFocusedRow();
            if (row == null) row = HRMISEmployees.GetEmployeeById(_request.SignatoryId);

            if (row == null) return;
            txtPosition.Text = row.Position;
            txtOffice.Text = row.Office + " " + row.Division;
            txtUsername.Text = row.Username;
        }
    }
}