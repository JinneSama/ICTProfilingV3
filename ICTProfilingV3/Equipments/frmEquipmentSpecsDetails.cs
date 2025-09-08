using ICTProfilingV3.BaseClasses;
using ICTProfilingV3.Interfaces;
using Models.Entities;
using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ICTProfilingV3.Equipments
{
    public partial class frmEquipmentSpecsDetails : BaseForm
    {
        private readonly IEquipmentService _equipmentService;
        private EquipmentSpecs _specs;
        public frmEquipmentSpecsDetails(IEquipmentService equipmentService)
        {
            _equipmentService = equipmentService;
            InitializeComponent();
        }

        public void InitForm(EquipmentSpecs specs)
        {
            _specs = specs;
            lblEquipment.Text = specs.Equipment.EquipmentName;
            lblDescription.Text = specs.Description;
            LoadSpecs();
        }

        private void LoadSpecs()
        {
            var data = _equipmentService.EquipmentSpecsDetailsBaseService.GetAll().Where(x => x.EquipmentSpecsId == _specs.Id).OrderBy(o => o.ItemNo); 
            gcEquipmentDetails.DataSource = new BindingList<EquipmentSpecsDetails>(data.ToList());  
        }

        private async void gridEquipmentDetails_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            var row = (EquipmentSpecsDetails)gridEquipmentDetails.GetFocusedRow();
            var res = await _equipmentService.EquipmentSpecsDetailsBaseService.GetByIdAsync(row.Id);
            if (res == null) await InsertSpecs(row);
            else await UpdateSpecs(row);
        }

        private async Task UpdateSpecs(EquipmentSpecsDetails row)
        {
            var specs = await _equipmentService.EquipmentSpecsDetailsBaseService.GetByIdAsync(row.Id);    
            specs.ItemNo = row.ItemNo;
            specs.DetailSpecs = row.DetailSpecs;
            specs.DetailDescription = row.DetailDescription;
            await _equipmentService.EquipmentSpecsDetailsBaseService.SaveChangesAsync();
        }

        private async Task InsertSpecs(EquipmentSpecsDetails row)
        {
            var equipmentDetail = new EquipmentSpecsDetails
            {
                ItemNo = row.ItemNo,
                DetailSpecs = row.DetailSpecs,
                DetailDescription = row.DetailDescription,
                EquipmentSpecsId = _specs.Id
            };
            await _equipmentService.EquipmentSpecsDetailsBaseService.AddAsync(equipmentDetail);
            LoadSpecs();
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            var msgRes = MessageBox.Show("Delete this Specs?", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
            if (msgRes == DialogResult.Cancel) return;

            var equipment = (EquipmentSpecsDetails)gridEquipmentDetails.GetFocusedRow();
            var res = await _equipmentService.EquipmentSpecsDetailsBaseService.GetByIdAsync(equipment.Id);
            if (res == null) return;
            await _equipmentService.EquipmentSpecsDetailsBaseService.DeleteAsync(equipment.Id);

            LoadSpecs();
        }
    }
}