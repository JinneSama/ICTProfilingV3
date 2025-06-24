using DevExpress.XtraEditors;
using EntityManager.Managers.User;
using ICTProfilingV3.BaseClasses;
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
    public partial class frmEditSignatories : BaseForm
    {
        private readonly IICTUserManager usermanager;
        private readonly int repairId;

        public frmEditSignatories(int repairId)
        {
            InitializeComponent();
            usermanager = new ICTUserManager();
            this.repairId = repairId;
            LoadDropdowns();
        }

        private void LoadDropdowns()
        {
            var users = usermanager.GetUsers();
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
            IUnitOfWork unitOfWork = new UnitOfWork();
            var repair = await unitOfWork.RepairsRepo.FindAsync(x => x.Id == repairId);
            repair.PreparedById = (string)sluePreparedBy.EditValue;
            repair.ReviewedById = (string)slueAssesedBy.EditValue;
            repair.NotedById = (string)slueNotedBy.EditValue;
            unitOfWork.RepairsRepo.Update(repair);
            await unitOfWork.SaveChangesAsync();
        }

        private async Task LoadDetails()
        {
            IUnitOfWork unitOfWork = new UnitOfWork();
            var repair = await unitOfWork.RepairsRepo.FindAsync(x => x.Id == repairId);

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