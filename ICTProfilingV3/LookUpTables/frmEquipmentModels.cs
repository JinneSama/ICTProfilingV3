using ICTProfilingV3.BaseClasses;
using ICTProfilingV3.DataTransferModels.ViewModels;
using ICTProfilingV3.Equipments;
using Models.Repository;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace ICTProfilingV3.LookUpTables
{
    public partial class frmEquipmentModels : BaseForm
    {
        private IUnitOfWork unitOfWork;
        public frmEquipmentModels()
        {
            InitializeComponent();
            unitOfWork = new UnitOfWork();
            LoadModels();
        }

        private void LoadModels()
        {
            var data = unitOfWork.ModelRepo.GetAll().Select(x => new BrandModelViewModel
            {
                Equipment = x.Brand.EquipmentSpecs.Equipment.EquipmentName,
                Description = x.Brand.EquipmentSpecs.Description,
                Brand = x.Brand.BrandName,
                Model = x.ModelName,
                EquipmentSpecsId = x.Brand.EquipmenSpecsId,
                BrandId = x.Brand.Id,
                ModelId = x.Id
            });
            gcModel.DataSource = new BindingList<BrandModelViewModel>(data.ToList());
        }

        private async void btnDeleteEquip_Click(object sender, System.EventArgs e)
        {
            var msgRes = MessageBox.Show("Delete Model?", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
            if (msgRes == DialogResult.Cancel) return;

            var model = (BrandModelViewModel)gridModel.GetFocusedRow();
            var res = await unitOfWork.ModelRepo.FindAsync(x => x.Id == model.ModelId);
            if (res == null) return;
            unitOfWork.ModelRepo.Delete(res);
            unitOfWork.Save();

            LoadModels();
        }

        private void btnEdit_Click(object sender, System.EventArgs e)
        {
            var res = (BrandModelViewModel)gridModel.GetFocusedRow();
            var frm = new frmAddEditModel(res);
            frm.ShowDialog();

            LoadModels();
        }

        private void btnAddModel_Click(object sender, System.EventArgs e)
        {
            var frm = new frmAddEditModel();
            frm.ShowDialog();

            LoadModels();
        }
    }
}