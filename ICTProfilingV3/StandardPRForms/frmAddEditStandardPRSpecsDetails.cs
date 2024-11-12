using Models.Entities;
using Models.Repository;
using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ICTProfilingV3.StandardPRForms
{
    public partial class frmAddEditStandardPRSpecsDetails : DevExpress.XtraEditors.XtraForm
    {
        private readonly StandardPRSpecs standardPRSpecs;
        private readonly IUnitOfWork unitOfWork;
        public frmAddEditStandardPRSpecsDetails(IUnitOfWork uow , StandardPRSpecs specs)
        {
            InitializeComponent();
            unitOfWork = uow;
            standardPRSpecs = specs;
            LoadSpecs();
        }

        private void LoadSpecs()
        {
            var specs = unitOfWork.StandardPRSpecsDetailsRepo.FindAllAsync(x => x.StandardPRSpecsId == standardPRSpecs.Id);
            gcSpecsDetails.DataSource = new BindingList<StandardPRSpecsDetails>(specs.ToList());
        }

        private async void gridSpecsDetails_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            var row = (StandardPRSpecsDetails)gridSpecsDetails.GetFocusedRow();
            if (row == null) return;

            var ifSpecsExists = await unitOfWork.StandardPRSpecsDetailsRepo.FindAsync(x => x.Id == row.Id);
            if (ifSpecsExists == null) await InsertSpecs(row);
            else await UpdateSpecs(row);
        }

        private async Task UpdateSpecs(StandardPRSpecsDetails res)
        {
            var sprSpecDetail = await unitOfWork.StandardPRSpecsDetailsRepo.FindAsync(x => x.Id == res.Id);
            if (sprSpecDetail == null) return;  

            sprSpecDetail.StandardPRSpecsId = standardPRSpecs.Id;
            sprSpecDetail.Specs = res.Specs;
            sprSpecDetail.ItemNo = res.ItemNo;
            sprSpecDetail.Description = res.Description;

            await unitOfWork.SaveChangesAsync();
        }

        private async Task InsertSpecs(StandardPRSpecsDetails res)
        {
            res.StandardPRSpecsId = standardPRSpecs.Id;
            unitOfWork.StandardPRSpecsDetailsRepo.Insert(res);
            await unitOfWork.SaveChangesAsync();
        }

        private void btnDeleteSpecs_Click(object sender, EventArgs e)
        {
            var msgRes = MessageBox.Show("Delete this Equipment?", "Confirmation", MessageBoxButtons.OKCancel,
                MessageBoxIcon.Exclamation);
            if (msgRes == DialogResult.Cancel) return;

            var row = (StandardPRSpecsDetails)gridSpecsDetails.GetFocusedRow();
            unitOfWork.StandardPRSpecsDetailsRepo.DeleteByEx(x => x.Id == row.Id);

            LoadSpecs();
        }
    }
}