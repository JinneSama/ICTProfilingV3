using DevExpress.Data.Filtering;
using ICTProfilingV3.ActionsForms;
using ICTProfilingV3.Core.Common;
using ICTProfilingV3.DataTransferModels;
using ICTProfilingV3.DataTransferModels.Models;
using ICTProfilingV3.DataTransferModels.ViewModels;
using ICTProfilingV3.EvaluationForms;
using ICTProfilingV3.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Models.Enums;
using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ICTProfilingV3.CustomerActionSheetForms
{
    public partial class UCCAS : DevExpress.XtraEditors.XtraUserControl
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IControlMapper<CASDetailDTM> _controlMapper;
        private readonly ICASService _casService;
        private readonly IICTRoleManager _roleManager;
        private readonly UserStore _userStore;

        public string filterText { get; set; }
        public UCCAS(IServiceProvider serviceProvider, IControlMapper<CASDetailDTM> controlMapper, ICASService casService,
            IICTRoleManager roleManager, UserStore userStore)
        {
            _casService = casService;
            _controlMapper = controlMapper;
            _serviceProvider = serviceProvider;
            _roleManager = roleManager;
            _userStore = userStore;
            InitializeComponent();
            LoadCAS();
        }

        private void LoadCAS()
        {
            var cas = _casService.GetCASDTM();
            gcCAS.DataSource = new BindingList<CASDTM>(cas.ToList());
        }

        private void UCCAS_Load(object sender, System.EventArgs e)
        {
            ApplyFilterText();
        }

        public void ApplyFilterText()
        {
            if (filterText != null) gridCAS.ActiveFilterCriteria = new BinaryOperator("Id", filterText);
        }

        private async Task LoadDetails()
        {
            var row = (CASDTM)gridCAS.GetFocusedRow();
            var casDetail = await _casService.GetCASDetail(row.Id);
            _controlMapper.MapControl(casDetail, gcDetails);
        }

        private void LoadActions()
        {
            var row = (CASDTM)gridCAS.GetFocusedRow();
            tabAction.Controls.Clear();

            var uc = _serviceProvider.GetRequiredService<UCActions>();
            uc.setActions(new ActionType { Id = row.Id, RequestType = RequestType.CAS });
            uc.Dock = System.Windows.Forms.DockStyle.Fill;
            tabAction.Controls.Add(uc);
        }

        private void LoadEvaluationSheet()
        {
            var row = (CASDTM)gridCAS.GetFocusedRow();
            var navigation = _serviceProvider.GetRequiredService<IControlNavigator<UCEvaluationSheet>>();
            navigation.NavigateTo(tabEvaluation, act => act.InitForm(new ActionType { Id = row.Id, RequestType = RequestType.CAS }));
        }
        private async void btnEdit_Click(object sender, System.EventArgs e)
        {
            var row = (CASDTM)gridCAS.GetFocusedRow();
            var frm = _serviceProvider.GetRequiredService<frmAddEditCAS>();
            frm.InitForm(SaveType.Update, row);
            frm.ShowDialog();

            LoadCAS();
            await LoadDetails();
        }

        private void btnAdd_Click(object sender, System.EventArgs e)
        {
            var frm = _serviceProvider.GetRequiredService<frmAddEditCAS>();
            frm.InitForm(SaveType.Insert);
            frm.ShowDialog();

            LoadCAS();
        }

        private async void gridCAS_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            var row = (CASDTM)gridCAS.GetFocusedRow();
            if (row == null) return;

            lblCASNo.Text = row.Id.ToString();
            await LoadDetails();
            LoadActions();
            LoadEvaluationSheet();
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            var res = await _roleManager.HasDesignation(Designation.CASAdmin, _userStore.UserRole);
            if (!res)
            {
                MessageBox.Show("You don't have permission to delete CAS.", "Permission Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Delete this CAS?", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) == DialogResult.Cancel) return;

            var cas = (CASDTM)gridCAS.GetFocusedRow();
            await _casService.DeleteAsync(cas.Id);
            LoadCAS();
        }
    }
}
