using DevExpress.XtraEditors;
using Models.Entities;
using Models.Repository;
using Models.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ICTProfilingV3.StandardPRForms
{
    public partial class UCStandardPR : DevExpress.XtraEditors.XtraUserControl
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PurchaseRequest _purchaseRequest;
        public UCStandardPR(IUnitOfWork uow, PurchaseRequest pr)
        {
            InitializeComponent();
            _unitOfWork = uow;
            _purchaseRequest = pr;
        }
        private async Task LoadStandardPRSpecs()
        {
            var pr = _unitOfWork.PRStandardPRSpecsRepo.FindAllAsync(x => x.PurchaseRequestId == _purchaseRequest.Id,
                x => x.StandardPRSpecs,
                x => x.StandardPRSpecs.StandardPRSpecsDetails,
                x => x.StandardPRSpecs.EquipmentSpecs,
                x => x.StandardPRSpecs.EquipmentSpecs.Equipment)
                .Select(x => new PRStandardPRSpecsViewModel
                {
                    Equipment = x.StandardPRSpecs.EquipmentSpecs.Equipment.EquipmentName,
                    StandardPRSpecs = x.StandardPRSpecs,
                    StandardPRSpecsDetails = x.StandardPRSpecs.StandardPRSpecsDetails,
                    ItemNo = x.ItemNo,
                    Quantity = x.Quantity,
                    TotalCost = x.TotalCost ?? x.StandardPRSpecs.UnitCost,
                    Id = x.Id
                });
            var dataPR = await pr.ToListAsync();
            gcPR.DataSource = new BindingList<PRStandardPRSpecsViewModel>(dataPR);
        }

        private async void UCStandardPR_Load(object sender, EventArgs e)
        {
            await LoadStandardPRSpecs();
        }

        private void btnExpandDetail_Click(object sender, EventArgs e)
        {
            var focusedRow = gridSpecs.FocusedRowHandle;
            gridSpecs.SetMasterRowExpanded(focusedRow, !gridSpecs.GetMasterRowExpanded(focusedRow));
        }
    }
}
