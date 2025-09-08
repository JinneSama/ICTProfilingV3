using ICTProfilingV3.BaseClasses;
using ICTProfilingV3.Interfaces;
using Models.Entities;
using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ICTProfilingV3.PPEInventoryForms
{
    public partial class frmAddEditPPEsSpecsDetails : BaseForm
    {
        private readonly IPPEInventoryService _ppeService;
        private PPEsSpecs _specs;
        public frmAddEditPPEsSpecsDetails(IPPEInventoryService ppeService)
        {
            _ppeService = ppeService;
            InitializeComponent();
        }

        public void InitForm(PPEsSpecs specs)
        {
            _specs = specs;
            lblEquipment.Text = specs.Model.Brand.EquipmentSpecs.Equipment.EquipmentName;
            lblDescription.Text = specs.Model.Brand.EquipmentSpecs.Description;
            LoadSpecs();
        }
        private void LoadSpecs()
        {
            var data = _ppeService.PPESpecsDetailsBaseService.GetAll().Where(x => x.PPEsSpecsId == _specs.Id);
            gcEquipmentDetails.DataSource = new BindingList<PPEsSpecsDetails>(data.ToList());
        }

        private async Task UpdateSpecs(PPEsSpecsDetails row)
        {
            var specs = await _ppeService.PPESpecsDetailsBaseService.GetByIdAsync(row.Id);
            specs.ItemNo = row.ItemNo;
            specs.Specs = row.Specs;
            specs.Description = row.Description;
            await _ppeService.PPESpecsDetailsBaseService.SaveChangesAsync();
        }

        private async Task InsertSpecs(PPEsSpecsDetails row)
        {
            var equipmentDetail = new PPEsSpecsDetails
            {
                ItemNo = row.ItemNo,
                Specs = row.Specs,
                Description = row.Description,
                PPEsSpecsId = _specs.Id
            };
            await _ppeService.PPESpecsDetailsBaseService.AddAsync(equipmentDetail);
            LoadSpecs();
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            var msgRes = MessageBox.Show("Delete this Specs?", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
            if (msgRes == DialogResult.Cancel) return;

            var equipment = (PPEsSpecsDetails)gridEquipmentDetails.GetFocusedRow();
            var res = await _ppeService.PPESpecsDetailsBaseService.GetByIdAsync(equipment.Id);
            if (res == null) return;
            await _ppeService.PPESpecsDetailsBaseService.DeleteAsync(equipment.Id);

            LoadSpecs();
        }

        private async void gridEquipmentDetails_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            var row = (PPEsSpecsDetails)gridEquipmentDetails.GetFocusedRow();
            var res = await _ppeService.PPESpecsDetailsBaseService.GetByIdAsync(row.Id);
            if (res == null) await InsertSpecs(row);
            else await UpdateSpecs(row);
        }
    }
}