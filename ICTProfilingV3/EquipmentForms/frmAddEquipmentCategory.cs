using ICTProfilingV3.BaseClasses;
using ICTProfilingV3.Interfaces;
using Models.Entities;
using Models.Enums;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ICTProfilingV3.EquipmentForms
{
    public partial class frmAddEquipmentCategory : BaseForm
    {
        private readonly IEquipmentService _equipmentService;
        private EquipmentCategory _equipmentCategory;
        private SaveType _saveType;
        public frmAddEquipmentCategory(IEquipmentService equipmentService)
        {
            _equipmentService = equipmentService;
            InitializeComponent();
        }

        public void InitForm(EquipmentCategory equipmentCategory = null)
        {
            _equipmentCategory = equipmentCategory;
            if (equipmentCategory == null)
            {
                _saveType = SaveType.Insert;
                labelControl1.Text = "Add Equipment";
            }
            else
            {
                _saveType = SaveType.Update;
                labelControl1.Text = "Edit Equipment";
                LoadData();
            }
        }

        private void LoadData()
        {
            txtEquipment.Text = _equipmentCategory.Name;
            memoDescription.Text = _equipmentCategory.Description;
        }
        private async void btnSave_Click(object sender, System.EventArgs e)
        {
            var checkExist = _equipmentService.EquEquipmentCategoryBaseService
                .GetAll()
                .FirstOrDefault(c => c.Name.ToLower() == txtEquipment.Text.ToLower());
            if (checkExist != null)
            {
                MessageBox.Show("Equipment with the same name already exists.", "Duplicate Entry", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (_saveType == SaveType.Insert)
                await InsertEquipment();
            else
                await UpdateEquipment();

            this.Close();
        }

        private async Task InsertEquipment()
        {
            var equipment = new EquipmentCategory
            {
                Name = txtEquipment.Text,
                Description = memoDescription.Text
            };
            await _equipmentService.EquEquipmentCategoryBaseService.AddAsync(equipment);
        }

        private async Task UpdateEquipment()
        {
            var equipment = await _equipmentService.EquEquipmentCategoryBaseService.GetByIdAsync(_equipmentCategory.Id);
            equipment.Name = txtEquipment.Text;
            equipment.Description = memoDescription.Text;
            await _equipmentService.EquEquipmentCategoryBaseService.SaveChangesAsync();
        }
    }
}