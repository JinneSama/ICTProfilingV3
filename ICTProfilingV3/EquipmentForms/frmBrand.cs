using ICTProfilingV3.BaseClasses;
using ICTProfilingV3.Interfaces;
using Models.Entities;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ICTProfilingV3.EquipmentForms
{
    public partial class frmBrand : BaseForm
    {
        private readonly IEquipmentService _equipmentService;
        public frmBrand(IEquipmentService equipmentService)
        {
            _equipmentService = equipmentService;
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            var data = _equipmentService.EquipmentBrandBaseService.GetAll().OrderBy(order => order.Name).ToList();
            gcBrand.DataSource = new BindingList<EquipmentBrand>(data);
        }

        private async void gridBrand_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            var row = (EquipmentBrand)gridBrand.GetFocusedRow();
            if (row == null) return;
            var res = await _equipmentService.EquipmentBrandBaseService.GetByIdAsync(row.Id);
            if (res == null) await InsertEquipment(row);
            else await UpdateEquipment(row);
        }
        private async Task InsertEquipment(EquipmentBrand row)
        {
            await _equipmentService.EquipmentBrandBaseService.AddAsync(row);
        }

        private async Task UpdateEquipment(EquipmentBrand row)
        {
            var equipment = await _equipmentService.EquipmentBrandBaseService.GetByIdAsync(row.Id);
            if (equipment == null) return;

            equipment.Name = row.Name;
            await _equipmentService.EquipmentBrandBaseService.SaveChangesAsync();
        }

        private async void btnDeleteEquipment_Click(object sender, System.EventArgs e)
        {
            var msgRes = MessageBox.Show("Delete Equipment?", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
            if (msgRes == DialogResult.Cancel) return;

            var equipment = (EquipmentCategory)gridBrand.GetFocusedRow();
            var res = await _equipmentService.EquipmentBrandBaseService.GetByIdAsync(equipment.Id);
            if (res == null) return;
            await _equipmentService.EquipmentBrandBaseService.DeleteAsync(res.Id);

            LoadData();
        }
    }
}