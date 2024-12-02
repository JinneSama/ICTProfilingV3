using DevExpress.Utils.Html.Internal;
using Models.Entities;
using Models.Enums;
using Models.Repository;
using System;
using System.ComponentModel;
using System.Linq;

namespace ICTProfilingV3.PGNForms
{
    public partial class UCMacAdresses : DevExpress.XtraEditors.XtraUserControl
    {
        private IUnitOfWork unitOfWork;
        private readonly PGNAccounts account;
        public UCMacAdresses(PGNAccounts _account)
        {
            InitializeComponent();
            account = _account;
            unitOfWork = new UnitOfWork();
            LoadData();
            LoadDropdowns();
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
            var res = unitOfWork.PGNMacAddressesRepo.FindAllAsync(x => x.PGNAccountId == account.Id);
            gcMacAddress.DataSource = new BindingList<PGNMacAddresses>(res.ToList());
        }

        private async void gridMacAddress_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            var row = (PGNMacAddresses)gridMacAddress.GetFocusedRow();
            var res = await unitOfWork.PGNMacAddressesRepo.FindAsync(x => x.Id == row.Id);
            if (res == null) InsertMacAddress(row);
            else UpdateMacAddress(row,res);
        }

        private void InsertMacAddress(PGNMacAddresses row)
        {
            row.PGNAccountId = account.Id;
            unitOfWork.PGNMacAddressesRepo.Insert(row);
            unitOfWork.Save();
        }

        private void UpdateMacAddress(PGNMacAddresses row, PGNMacAddresses res)
        {
            res.MacAddress = row.MacAddress;
            res.Device = row.Device;
            res.Connection = row.Connection;
            unitOfWork.Save();
        }
    }
}
