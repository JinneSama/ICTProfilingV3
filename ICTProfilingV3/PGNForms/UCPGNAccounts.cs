using ICTProfilingV3.Core.Common;
using ICTProfilingV3.DataTransferModels;
using ICTProfilingV3.DataTransferModels.ViewModels;
using ICTProfilingV3.Interfaces;
using ICTProfilingV3.ReportForms;
using Microsoft.Extensions.DependencyInjection;
using Models.Entities;
using Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Windows.Forms;

namespace ICTProfilingV3.PGNForms
{
    public partial class UCPGNAccounts : DevExpress.XtraEditors.XtraUserControl
    {

        private readonly IICTRoleManager _roleManager;
        private readonly IControlMapper<PGNAccounts> _pgnAccountMapper;
        private readonly IControlMapper<PGNAccountsViewModel> _pgnAccountVMMapper;
        private readonly IServiceProvider _serviceProvider;
        private readonly IPGNService _pgnService;
        private readonly UserStore _userStore;
        public UCPGNAccounts(IServiceProvider serviceProvider, IPGNService pgnService, IControlMapper<PGNAccounts> pgnAccountMapper, 
            IControlMapper<PGNAccountsViewModel> pgnAccountVMMapper, IICTRoleManager roleManager, UserStore userStore)
        {
            _serviceProvider = serviceProvider;
            _pgnService = pgnService;
            _pgnAccountMapper = pgnAccountMapper;
            _pgnAccountVMMapper = pgnAccountVMMapper;
            _roleManager = roleManager;
            _userStore = userStore;
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            var res = _pgnService.GetPGNAccViewModel();
            gcPGN.DataSource = new BindingList<PGNAccountsViewModel>(res.ToList());
        }
        private void LoadMacAddress(PGNAccounts account)
        {
            var navigation = _serviceProvider.GetRequiredService<IControlNavigator<UCMacAdresses>>();
            navigation.NavigateTo(groupMacAddress, act => act.InitUC(account));
        }
        private void LoadDetails(PGNAccountsViewModel row)
        {
            _pgnAccountMapper.MapControl(row.PGNAccount, groupControl1);
            _pgnAccountVMMapper.MapControl(row, groupControl1); 
            txtOffice.Text = row?.PGNAccount?.PGNGroupOffices?.OfficeAcr;
        }

        private void gridPGN_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            var row = (PGNAccountsViewModel)gridPGN.GetFocusedRow();
            if (row == null) return;

            LoadMacAddress(row.PGNAccount);
            LoadDetails(row);
        }

        private async void btnCompReport_Click(object sender, System.EventArgs e)
        {
            var frm = _serviceProvider.GetRequiredService<frmAddEditAccount>();
            await frm.InitForm();
            frm.ShowDialog();

            LoadData();
        }

        private async void btnEdit_Click(object sender, System.EventArgs e)
        {
            var row = (PGNAccountsViewModel)gridPGN.GetFocusedRow();
            if(row == null) return;
            var frm = _serviceProvider.GetRequiredService<frmAddEditAccount>();
            await frm.InitForm(row.PGNAccount);
            frm.ShowDialog();

            LoadData();
        }

        private void btnPreview_Click(object sender, System.EventArgs e)
        {
            var res = _pgnService.GetAll()
                .Include(x => x.PGNGroupOffices)
                .Include(x => x.PGNNonEmployee)
                .Where(x => x.Username.StartsWith("sp."))
                .ToList()
                .Select(x => new PGNAccountsViewModel
                {
                    PGNAccount = x
                }).ToList();

            var data = res.Select(x => new PGNAccountDTM
            {
                Id = x.PGNAccount.Id,
                Name = x.Name,
                Position = x.Position,
                Username = x.PGNAccount.Username,
                UserType = x.PGNAccount.UserType?.ToString(),
                OfficeAcr = x.PGNAccount.PGNGroupOffices?.OfficeAcr,
                Status = x.PGNAccount.Status?.ToString(),
                SignInCount = x.PGNAccount.SignInCount ?? 0,
                TrafficSpeed = Models.Enums.EnumHelper.GetEnumDescription(x.PGNAccount.TrafficSpeed),
                Designation = x.PGNAccount.Designation.ToString(),
                Password = x.PGNAccount.Password,
                Remarks = x.PGNAccount.Remarks,
                MacAddresses = _pgnService.PGNMacAddressService.GetAll().Where(m => m.PGNAccountId == x.PGNAccount.Id).ToList()
            }).ToList();

            var report = new PGNReportDTM
            {
                PGNAccounts = data
            };

            var rpt = new rptPGNUsers
            {
                DataSource = new List<PGNReportDTM> { report }
            };

            var frm = _serviceProvider.GetRequiredService<frmReportViewer>();
            frm.InitForm(rpt);
            frm.ShowDialog();
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            var res = await _roleManager.HasDesignation(Designation.PGNAdmin, _userStore.UserRole);
            if (!res)
            {
                MessageBox.Show("You don't have permission to delete PGN Accounts.", "Permission Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if(MessageBox.Show("Delete this Account?", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) == DialogResult.Cancel) return;
            var row = (PGNAccountsViewModel)gridPGN.GetFocusedRow();
            await _pgnService.PGNMacAddressService.DeleteRangeAsync(x => x.PGNAccountId == row.PGNAccount.Id);
            if (row.IsNonEmployee)
            {
                int id = row.PGNAccount.PGNNonEmployeeId ?? 0;
                await _pgnService.PGNNonEmployeeService.DeleteAsync(id);
            }
            await _pgnService.DeleteAsync(row.PGNAccount.Id);
            LoadData();
        }
    }
}
