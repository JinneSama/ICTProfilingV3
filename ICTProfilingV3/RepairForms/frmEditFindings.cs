using ICTProfilingV3.BaseClasses;
using Models.Repository;
using System;
using System.Threading.Tasks;

namespace ICTProfilingV3.RepairForms
{
    public partial class frmEditFindings : BaseForm
    {
        private readonly int repairId;

        public frmEditFindings(int repairId)
        {
            InitializeComponent();
            this.repairId = repairId;
        }

        private async Task LoadDetails()
        {
            IUnitOfWork unitOfWork = new UnitOfWork();
            var repair = await unitOfWork.RepairsRepo.FindAsync(x => x.Id == repairId);
            txtRecommendation.Text = repair.Recommendations;
            txtFindings.Text = repair.Findings;
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
            IUnitOfWork unitOfWork = new UnitOfWork();
            var res = await unitOfWork.RepairsRepo.FindAsync(x => x.Id == repairId);
            res.Findings = txtFindings.Text;
            res.Recommendations = txtRecommendation.Text;

            unitOfWork.RepairsRepo.Update(res);
            await unitOfWork.SaveChangesAsync();
        }

        private async void frmEditFindings_Load(object sender, EventArgs e)
        {
            await LoadDetails();
        }
    }
}