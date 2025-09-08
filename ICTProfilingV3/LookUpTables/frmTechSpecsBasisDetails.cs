using ICTProfilingV3.Interfaces;
using Models.Entities;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ICTProfilingV3.LookUpTables
{
    public partial class frmTechSpecsBasisDetails : DevExpress.XtraEditors.XtraForm
    {
        private readonly ITechSpecsService _tsService;
        private TechSpecsBasis _specs;
        public frmTechSpecsBasisDetails(ITechSpecsService tsService)
        {
            _tsService = tsService;
            InitializeComponent();
            LoadSpecs();
        }

        public void InitForm(TechSpecsBasis specs)
        {
            _specs = specs;

            lblEquipment.Text = specs.EquipmentSpecs.Equipment.EquipmentName;
            lblDescription.Text = specs.EquipmentSpecs.Description;
        }
        private void LoadSpecs()
        {
            var data = _tsService.TechSpecsBasisDetailBaseService.GetAll().Where(x => x.TechSpecsBasisId == _specs.Id).OrderBy(o => o.ItemNo);
            gcEquipmentDetails.DataSource = new BindingList<TechSpecsBasisDetails>(data.ToList());
        }

        private async void btnDelete_Click(object sender, System.EventArgs e)
        {
            var msgRes = MessageBox.Show("Delete this Specs?", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
            if (msgRes == DialogResult.Cancel) return;

            var equipment = (TechSpecsBasisDetails)gridEquipmentDetails.GetFocusedRow();
            var res = await _tsService.TechSpecsBasisDetailBaseService.GetByIdAsync(equipment.Id);
            if (res == null) return;

            await _tsService.TechSpecsBasisDetailBaseService.DeleteAsync(equipment.Id);
            LoadSpecs();
        }

        private async void gridEquipmentDetails_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            var row = (TechSpecsBasisDetails)gridEquipmentDetails.GetFocusedRow();
            var res = await _tsService.TechSpecsBasisDetailBaseService.GetByIdAsync(row.Id);
            if (res == null) await InsertSpecs(row);
            else await UpdateSpecs(row);
        }
        private async Task UpdateSpecs(TechSpecsBasisDetails row)
        {
            var specs = await _tsService.TechSpecsBasisDetailBaseService.GetByIdAsync(row.Id);
            specs.ItemNo = row.ItemNo;
            specs.Specs = row.Specs;
            specs.Description = row.Description;

            await _tsService.TechSpecsBasisDetailBaseService.SaveChangesAsync();
            LoadSpecs();
        }

        private async Task InsertSpecs(TechSpecsBasisDetails row)
        {
            var equipmentDetail = new TechSpecsBasisDetails
            {
                ItemNo = row.ItemNo,
                Specs = row.Specs,
                Description = row.Description,
                TechSpecsBasisId = _specs.Id
            };
            await _tsService.TechSpecsBasisDetailBaseService.AddAsync(equipmentDetail);
            LoadSpecs();
        }
    }
}