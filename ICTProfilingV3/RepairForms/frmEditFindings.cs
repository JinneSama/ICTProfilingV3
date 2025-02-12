using DevExpress.XtraEditors;
using ICTProfilingV3.BaseClasses;
using Models.Entities;
using Models.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ICTProfilingV3.RepairForms
{
    public partial class frmEditFindings : BaseForm
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly int repairId;

        public frmEditFindings(int repairId)
        {
            InitializeComponent();
            unitOfWork = new UnitOfWork();
            this.repairId = repairId;
        }

        private async Task LoadDetails()
        {
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
            var res = await unitOfWork.RepairsRepo.FindAsync(x => x.Id == repairId);
            res.Findings = txtFindings.Text;
            res.Recommendations = txtRecommendation.Text;

            await unitOfWork.SaveChangesAsync();
        }

        private async void frmEditFindings_Load(object sender, EventArgs e)
        {
            await LoadDetails();
        }
    }
}