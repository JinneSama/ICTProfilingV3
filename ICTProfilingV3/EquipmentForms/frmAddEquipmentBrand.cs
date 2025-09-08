using ICTProfilingV3.BaseClasses;
using ICTProfilingV3.Interfaces;
using Models.Entities;
using Models.Enums;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ICTProfilingV3.EquipmentForms
{
    public partial class frmAddEquipmentBrand : BaseForm
    {
        private readonly IEquipmentService _equipmentService;
        private EquipmentCategory _equipmentCategory;
        private int? _equipmentCategoryBrandId;
        private SaveType _saveType;
        public frmAddEquipmentBrand(IEquipmentService equipmentService)
        {
            _equipmentService = equipmentService;
            InitializeComponent();
        }

        public void InitForm(EquipmentCategory equipmentCategory, int? equipmentCategoryBrandId = null)
        {
            if (equipmentCategoryBrandId == null)
            {
                _saveType = SaveType.Insert;
                labelControl1.Text = "Add Brand";
            }
            else
            {
                _saveType = SaveType.Update;
                labelControl1.Text = "Edit Brand";
            }
            _equipmentCategory = equipmentCategory;
            LoadDropdowns();
        }

        private void LoadDropdowns()
        {
            txtEquipment.Text = _equipmentCategory.Name;
            var data = _equipmentService.EquipmentBrandBaseService.GetAll().OrderBy(order => order.Name).ToList();
            slueBrand.Properties.DataSource = data;
        }

        private async void btnSave_Click(object sender, System.EventArgs e)
        {
            var categories = _equipmentService.EquipmentCategoryBrandBaseService
                .GetAll()
                .Where(c => c.EquipmentCategoryId == _equipmentCategory.Id)
                .Include(c => c.EquipmentBrand);

            var brand = (EquipmentBrand)slueBrand.Properties.View.GetFocusedRow();

            var exists = categories.Any(b => b.EquipmentBrandId == brand.Id);
            if (exists)
            {
                MessageBox.Show("The selected brand already exists for this Equipment.", "Duplicate Brand", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (_saveType == SaveType.Insert)
                await InsertBrand(brand);
            else
                await UpdateBrand(brand);
            this.Close();
        }

        private async Task UpdateBrand(EquipmentBrand brand)
        {
            var eBrand = await _equipmentService.EquipmentCategoryBrandBaseService.GetByIdAsync((int)_equipmentCategoryBrandId);
            eBrand.EquipmentBrandId = brand.Id;
            await _equipmentService.EquipmentCategoryBrandBaseService.SaveChangesAsync();
        }

        private async Task InsertBrand(EquipmentBrand brand)
        {
            var equipmentCategoryBrand = new EquipmentCategoryBrand
            {
                EquipmentCategoryId = _equipmentCategory.Id,
                EquipmentBrandId = brand.Id
            };
            await _equipmentService.EquipmentCategoryBrandBaseService.AddAsync(equipmentCategoryBrand);
        }
    }

}