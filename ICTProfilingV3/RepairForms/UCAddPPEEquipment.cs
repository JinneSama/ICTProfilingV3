using DevExpress.XtraEditors;
using ICTProfilingV3.PPEInventoryForms;
using Models.Entities;
using Models.Repository;
using Models.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ICTProfilingV3.RepairForms
{
    public partial class UCAddPPEEquipment : DevExpress.XtraEditors.XtraUserControl
    {
        private readonly PPEs _ppe;
        private IUnitOfWork unitOfWork;
        public UCAddPPEEquipment(PPEs ppe, IUnitOfWork _unitOfWork)
        {
            InitializeComponent();
            unitOfWork = _unitOfWork;
            _ppe = ppe;
            LoadEquipmentSpecs();
        }
        private void LoadEquipmentSpecs()
        {
            var res = unitOfWork.PPEsSpecsRepo.FindAllAsync(x => x.PPEsId == _ppe.Id).Select(x => new PPEsSpecsViewModel
            {
                Id = x.Id,
                ItemNo = x.ItemNo,
                Quantity = x.Quantity,
                Unit = x.Unit,
                Equipment = x.Model.Brand.EquipmentSpecs.Equipment.EquipmentName,
                Description = x.Model.Brand.EquipmentSpecs.Description,
                Brand = x.Model.Brand.BrandName,
                Model = x.Model.ModelName,
                UnitCost = x.UnitCost,
                TotalCost = x.TotalCost,
                PPEsSpecsDetails = x.PPEsSpecsDetails
            });
            gcEquipmentSpecs.DataSource = new BindingList<PPEsSpecsViewModel>(res.ToList());
        }

        private void btnInfo_Click(object sender, EventArgs e)
        {
            var focusedRow = gridEquipmentSpecs.FocusedRowHandle;
            gridEquipmentSpecs.SetMasterRowExpanded(focusedRow, !gridEquipmentSpecs.GetMasterRowExpanded(focusedRow));
        }

        public Task<List<PPEsSpecsViewModel>> RetrieveSelectedEquipment()
        {
            List<PPEsSpecsViewModel> markedPPEsSpecs = new List<PPEsSpecsViewModel>();
            for(int i = 0; i < gridEquipmentSpecs.RowCount; i++)
            {
                var equipment = (PPEsSpecsViewModel)gridEquipmentSpecs.GetRow(i);
                if (equipment.Mark.Value)
                    markedPPEsSpecs.Add(equipment);
            }
            return Task.FromResult(markedPPEsSpecs);
        }
        private void gridEquipmentSpecs_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {

        }
    }
}
