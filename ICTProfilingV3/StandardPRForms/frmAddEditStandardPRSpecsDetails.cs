using DevExpress.XtraBars;
using Helpers.Tools;
using Helpers.Tools.Models;
using ICTProfilingV3.BaseClasses;
using Models.Entities;
using Models.Repository;
using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ICTProfilingV3.StandardPRForms
{
    public partial class frmAddEditStandardPRSpecsDetails : BaseForm
    {
        private readonly StandardPRSpecs standardPRSpecs;
        private readonly IUnitOfWork unitOfWork;
        public frmAddEditStandardPRSpecsDetails(StandardPRSpecs specs)
        {
            InitializeComponent();
            unitOfWork = new UnitOfWork();
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
            var msgRes = MessageBox.Show("Delete this Specs", "Confirmation", MessageBoxButtons.OKCancel,
                MessageBoxIcon.Exclamation);
            if (msgRes == DialogResult.Cancel) return;

            var row = (StandardPRSpecsDetails)gridSpecsDetails.GetFocusedRow();
            unitOfWork.StandardPRSpecsDetailsRepo.DeleteByEx(x => x.Id == row.Id);
            unitOfWork.Save();
            LoadSpecs();
        }

        private void btnPasteSpecs_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var uow = new UnitOfWork();
            var specs = CopyPaste.PasteSpecs(); 
            
            var lastItemNo = 0;
            var lastSpecs = uow.StandardPRSpecsDetailsRepo.FindAllAsync(x => x.StandardPRSpecsId == standardPRSpecs.Id).ToList().LastOrDefault();
            if (lastSpecs != null) lastItemNo = lastSpecs.ItemNo;

            foreach (var item in specs)
            {
                lastItemNo += 1;
                AddPastedItems(item, lastItemNo);
            } 
            LoadSpecs();
        }

        private void AddPastedItems(CopiedSpecs copiedSpecs, int itemNo)
        {
            var unitOfWork = new UnitOfWork();
            var specs = new StandardPRSpecsDetails
            {
                StandardPRSpecsId = standardPRSpecs.Id,
                ItemNo = itemNo,
                Specs = copiedSpecs.Specs,
                Description = copiedSpecs.Description
            };

            unitOfWork.StandardPRSpecsDetailsRepo.Insert(specs);
            unitOfWork.Save();
        }

        private void gridSpecsDetails_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
        }

        private void btnDeleteAll_Click(object sender, EventArgs e)
        {
            var msgRes = MessageBox.Show("Delete all Specifications?", "Confirmation", MessageBoxButtons.OKCancel,
                MessageBoxIcon.Exclamation);
            if (msgRes == DialogResult.Cancel) return;

            unitOfWork.StandardPRSpecsDetailsRepo.DeleteRange(x => x.StandardPRSpecsId == standardPRSpecs.Id);
            unitOfWork.Save();
            LoadSpecs();
        }

        private void gridSpecsDetails_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                popupMenu.ShowPopup(MousePosition);
        }
    }
}