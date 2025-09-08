using ICTProfilingV3.Interfaces;
using Models.Entities;
using Models.Enums;
using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace ICTProfilingV3.PGNForms
{
    public partial class UCMacAdresses : DevExpress.XtraEditors.XtraUserControl
    {
        private readonly IPGNService _pgnService;
        private PGNAccounts _account;
        public UCMacAdresses(IPGNService pgnService)
        {
            _pgnService = pgnService;
            InitializeComponent();
            LoadDropdowns();
        }

        public void InitUC(PGNAccounts account)
        {
            _account = account;
            LoadData();
        }

        private void LoadDropdowns()
        {
            bsConnection.DataSource = Enum.GetValues(typeof(PGNDeviceConnection)).Cast<PGNDeviceConnection>().Select(x => new
            {
                Connection = x
            }); 

            bsDevice.DataSource = Enum.GetValues(typeof(PGNDevices)).Cast<PGNDevices>().Select(x => new
            {
                Device = x
            });
        }

        private void LoadData()
        {
            var res = _pgnService.PGNMacAddressService.GetAll().Where(x => x.PGNAccountId == _account.Id).ToList();
            gcMacAddress.DataSource = new BindingList<PGNMacAddresses>(res);
        }

        private async void gridMacAddress_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            var row = (PGNMacAddresses)gridMacAddress.GetFocusedRow();
            var res = await _pgnService.PGNMacAddressService.GetByIdAsync(row.Id);
            if (res == null) await InsertMacAddress(row);
            else UpdateMacAddress(row,res);
        }

        private async Task InsertMacAddress(PGNMacAddresses row)
        {
            row.PGNAccountId = _account.Id;
            await _pgnService.PGNMacAddressService.AddAsync(row);
        }

        private void UpdateMacAddress(PGNMacAddresses row, PGNMacAddresses res)
        {
            res.MacAddress = row.MacAddress;
            res.Device = row.Device;
            res.Connection = row.Connection;
            _pgnService.PGNMacAddressService.SaveChangesAsync();
        }
    }
}
