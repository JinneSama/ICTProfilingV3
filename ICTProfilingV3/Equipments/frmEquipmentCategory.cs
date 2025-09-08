using ICTProfilingV3.BaseClasses;
using ICTProfilingV3.Interfaces;
using Models.Entities;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ICTProfilingV3.Equipments
{
    public partial class frmEquipmentCategory : BaseForm
    {
        private readonly IEquipmentService _equipmentService;
        public frmEquipmentCategory(IEquipmentService equipmentService)
        {
            _equipmentService = equipmentService;
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            var data = _equipmentService.EquEquipmentCategoryBaseService.GetAll().OrderBy(order => order.Name).ToList();
            gcEquipment.DataSource = new BindingList<EquipmentCategory>(data);
        }

        private async void gridEquipment_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            var row = (EquipmentCategory)gridEquipment.GetFocusedRow();
            if (row == null) return;
            var res = await _equipmentService.EquEquipmentCategoryBaseService.GetByIdAsync(row.Id);
            if (res == null) await InsertEquipment(row);
            else await UpdateEquipment(row);
        }

        private async Task InsertEquipment(EquipmentCategory row)
        {
            await _equipmentService.EquEquipmentCategoryBaseService.AddAsync(row);
        }

        private async Task UpdateEquipment(EquipmentCategory row)
        {
            var equipment = await _equipmentService.EquEquipmentCategoryBaseService.GetByIdAsync(row.Id);
            if (equipment == null) return;

            equipment.Name = row.Name;
            await _equipmentService.SaveChangesAsync();
        }

        private async void btnDeleteEquipment_Click(object sender, System.EventArgs e)
        {
            var msgRes = MessageBox.Show("Delete Equipment?", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
            if (msgRes == DialogResult.Cancel) return;

            var equipment = (EquipmentCategory)gridEquipment.GetFocusedRow();
            var res = await _equipmentService.EquEquipmentCategoryBaseService.GetByIdAsync(equipment.Id);
            if (res == null) return;
            await _equipmentService.EquEquipmentCategoryBaseService.DeleteAsync(res.Id);

            LoadData();
        }
    }
}