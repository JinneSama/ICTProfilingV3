using DevExpress.Data.Linq.Helpers;
using DevExpress.XtraEditors;
using ICTProfilingV3.PPEInventoryForms;
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

namespace ICTProfilingV3.RepairForms
{
    public partial class UCAddPPEEquipment : DevExpress.XtraEditors.XtraUserControl
    {
        private readonly PPEs _ppe;
        private readonly PPEsSpecs ppeSpecs;
        private IUnitOfWork unitOfWork;
        private readonly bool showMark;

        public UCAddPPEEquipment(PPEs ppe, IUnitOfWork _unitOfWork, bool showMark)
        {
            InitializeComponent();
            unitOfWork = new UnitOfWork();
            this.showMark = !showMark;
            _ppe = ppe;
            if (_ppe == null) return;
            LoadEquipmentSpecs();
        }

        public UCAddPPEEquipment(PPEsSpecs ppeSpecs, IUnitOfWork _unitOfWork, bool showMark)
        {
            InitializeComponent();
            this.ppeSpecs = ppeSpecs;
            unitOfWork = new UnitOfWork();
            this.showMark = showMark;
            if (ppeSpecs == null) return;
            LoadEquipmentSpecs();
        }

        private void LoadEquipmentSpecs()
        {
            gridMark.Visible = !showMark;
            IEnumerable<PPEsSpecs> res;
            if (showMark) res = unitOfWork.PPEsSpecsRepo.FindAllAsync(x => x.PPEsId == _ppe.Id,
                x => x.Model,
                x => x.Model.Brand,
                x => x.Model.Brand.EquipmentSpecs,
                x => x.Model.Brand.EquipmentSpecs.Equipment).ToList();
            else res = unitOfWork.PPEsSpecsRepo.FindAllAsync(x => x.Id == ppeSpecs.Id,
                x => x.Model,
                x => x.Model.Brand,
                x => x.Model.Brand.EquipmentSpecs,
                x => x.Model.Brand.EquipmentSpecs.Equipment).ToList();

            var data = res.Select(x => new PPEsSpecsViewModel
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

            gcEquipmentSpecs.DataSource = new BindingList<PPEsSpecsViewModel>(data.ToList());
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
    }
}
