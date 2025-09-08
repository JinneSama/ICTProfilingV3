using ICTProfilingV3.BaseClasses;
using ICTProfilingV3.Interfaces;
using System;
using System.Threading.Tasks;

namespace ICTProfilingV3.RepairForms
{
    public partial class frmEditSignatories : BaseForm
    {
        private readonly IICTUserManager _usermanager;
        private readonly IRepairService _repairService;
        private int _repairId;

        public frmEditSignatories(IICTUserManager userManager, IRepairService repairService)
        {
            _usermanager = userManager;
            _repairService = repairService;
            InitializeComponent();
            LoadDropdowns();
        }
        
        public void InitForm(int repairId)
        {
            _repairId = repairId;
        }
        private void LoadDropdowns()
        {
            var users = _usermanager.GetUsers();
            sluePreparedBy.Properties.DataSource = users;
            slueAssesedBy.Properties.DataSource = users;
            slueNotedBy.Properties.DataSource = users;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            await Save();
            this.Close();
        }

        private async Task Save()
        {
            var repair = await _repairService.GetByIdAsync(_repairId);
            repair.PreparedById = (string)sluePreparedBy.EditValue;
            repair.ReviewedById = (string)slueAssesedBy.EditValue;
            repair.NotedById = (string)slueNotedBy.EditValue;
            await _repairService.SaveChangesAsync();
        }

        private async Task LoadDetails()
        {
            var repair = await _repairService.GetByIdAsync(_repairId);

            slueAssesedBy.EditValue = repair.ReviewedById;
            slueNotedBy.EditValue= repair.NotedById;
            sluePreparedBy.EditValue = repair.PreparedById;
        }

        private async void frmEditSignatories_Load(object sender, EventArgs e)
        {
            await LoadDetails();
        }
    }
}