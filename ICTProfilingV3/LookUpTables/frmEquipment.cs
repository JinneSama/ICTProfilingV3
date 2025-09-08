using ICTProfilingV3.BaseClasses;
using ICTProfilingV3.Interfaces;
using Models.Entities;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ICTProfilingV3.LookUpTables
{
    public partial class frmEquipment : BaseForm
    {
        private readonly IEquipmentService _equipmentService;
        public frmEquipment(IEquipmentService equipmentService)
        {
            _equipmentService = equipmentService;
            InitializeComponent();
            LoadDropdowns();
            LoadEquipments();
        }

        private void LoadDropdowns()
        {
            var data = _equipmentService.EquEquipmentCategoryBaseService.GetAll().OrderBy(order => order.Name).ToList();
            bsEquipmentCategory.DataSource = data;
        }

        private void LoadEquipments()
        {
            var equipments = _equipmentService.GetAll().OrderBy(o => o.EquipmentName).ToList();
            gcEquipment.DataSource = new BindingList<Equipment>(equipments);
        }

        private async void btnDeleteEquipment_Click(object sender, System.EventArgs e)
        {
            var msgRes = MessageBox.Show("Delete Equipment?", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
            if (msgRes == DialogResult.Cancel) return;

            var equipment = (Equipment)gridEquipment.GetFocusedRow();
            var res = await _equipmentService.GetByIdAsync(equipment.Id);
            if (res == null) return;
            await _equipmentService.DeleteAsync(res.Id);

            LoadEquipments();
        }

        private async void gridEquipment_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            var row = (Equipment)gridEquipment.GetFocusedRow();
            var res = await _equipmentService.GetByIdAsync(row.Id);
            if (res == null) await InsertEquipment(row);
            else await UpdateEquipment(row);
        }

        private async Task InsertEquipment(Equipment row)
        {
            await _equipmentService.AddAsync(row);
        }

        private async Task UpdateEquipment(Equipment row)
        {
            var equipment = await _equipmentService.GetByIdAsync(row.Id);
            if (equipment == null) return;

            equipment.EquipmentCategoryId = row.EquipmentCategoryId;
            equipment.EquipmentName = row.EquipmentName;
            await _equipmentService.SaveChangesAsync();
        }
    }
}