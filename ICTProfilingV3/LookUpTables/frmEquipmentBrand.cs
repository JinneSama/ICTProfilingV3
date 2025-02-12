using DevExpress.Utils.About;
using DevExpress.Utils.Extensions;
using DevExpress.XtraEditors;
using ICTProfilingV3.BaseClasses;
using ICTProfilingV3.Equipments;
using Models.Entities;
using Models.Repository;
using Models.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ICTProfilingV3.LookUpTables
{
    public partial class frmEquipmentBrand : BaseForm
    {
        IUnitOfWork unitOfWork;
        public frmEquipmentBrand()
        {
            InitializeComponent();
            unitOfWork = new UnitOfWork();
            LoadBrand();
        }

        private void LoadBrand()
        {
            var res = unitOfWork.BrandRepo.GetAll().Select(x => new BrandViewModel
            {
                BrandId = x.Id,
                BrandName = x.BrandName,
                Equipment = x.EquipmentSpecs.Equipment.EquipmentName,
                Description = x.EquipmentSpecs.Description,
                EquipmentSpecsId = x.EquipmenSpecsId
            });
            gcBrand.DataSource = new BindingList<BrandViewModel>(res.ToList());
        }

        private void btnAddBrand_Click(object sender, EventArgs e)
        {
            var frm = new frmAddEditBrand();
            frm.ShowDialog();

            LoadBrand();
        }

        private async void btnDeleteEquip_Click(object sender, EventArgs e)
        {
            var msgRes = MessageBox.Show("Delete Brand?", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
            if (msgRes == DialogResult.Cancel) return;

            var brand = (BrandViewModel)gridBrand.GetFocusedRow();
            var res = await unitOfWork.BrandRepo.FindAsync(x => x.Id == brand.BrandId);
            if (res == null) return;
            unitOfWork.BrandRepo.Delete(res);
            unitOfWork.Save();

            LoadBrand();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            var row = (BrandViewModel)gridBrand.GetFocusedRow();
            var frm = new frmAddEditBrand(row);
            frm.ShowDialog();

            LoadBrand();
        }
    }
}