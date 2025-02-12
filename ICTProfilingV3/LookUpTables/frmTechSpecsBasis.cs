using ICTProfilingV3.BaseClasses;
using Models.Entities;
using Models.Repository;
using Models.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace ICTProfilingV3.LookUpTables
{
    public partial class frmTechSpecsBasis : BaseForm
    {
        private IUnitOfWork unitOfWork;
        public bool Copy { get; set; }
        public IEnumerable<TechSpecsBasisDetails> SpecsDetails { get; set; }
        public frmTechSpecsBasis()
        {
            InitializeComponent();
            unitOfWork = new UnitOfWork();
        }

        private async void LoadSpecsBasis()
        {
            colCopy.Visible = Copy;
            var equipmentDropdown = unitOfWork.EquipmentSpecsRepo.GetAll(x => x.Equipment);
            equipmentSpecsBindingSource.DataSource = equipmentDropdown.ToList();

            var equipmentBasis = await unitOfWork.TechSpecsBasisRepo.GetAll(x => x.EquipmentSpecs,
                x => x.EquipmentSpecs.Equipment,
                x => x.TechSpecsBasisDetails).ToListAsync();
            gcTSBasis.DataSource = new BindingList<TechSpecsBasis>(equipmentBasis);
        }

        private async void gridTSBasis_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            var row = (TechSpecsBasis)gridTSBasis.GetFocusedRow();
            var check = await unitOfWork.TechSpecsBasisRepo.FindAsync(x => x.Id == row.Id);
            if(check == null) InsertSpecs(row);
            else UpdateSpecs(row);
        }

        private async void UpdateSpecs(TechSpecsBasis row)
        {
            var res = await unitOfWork.TechSpecsBasisRepo.FindAsync(x => x.Id == row.Id);
            res.PriceRange = row.PriceRange;
            res.PriceDate = row.PriceDate;
            res.URLBasis = row.URLBasis;
            res.Remarks = row.Remarks;
            res.Available = row.Available;
            res.EquipmentSpecsId = row.EquipmentSpecsId;

            unitOfWork.Save();
        }

        private void InsertSpecs(TechSpecsBasis row)
        {
            unitOfWork.TechSpecsBasisRepo.Insert(row);
            unitOfWork.Save();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Delete this Basis?", "Confirmation!",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) == DialogResult.Cancel) return;

            var row = (TechSpecsBasis)gridTSBasis.GetFocusedRow();
            unitOfWork.TechSpecsBasisRepo.Delete(row);
            unitOfWork.Save();
            LoadSpecsBasis();

            MessageBox.Show("Deleted!");
        }

        private void btnOpenURL_Click(object sender, EventArgs e)
        {
            try
            {
                var item = (TechSpecsBasis)gridTSBasis.GetFocusedRow();
                if (string.IsNullOrEmpty(item.URLBasis) || string.IsNullOrWhiteSpace(item.URLBasis))
                    MessageBox.Show("Invalid URL", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else
                    Process.Start(item.URLBasis);
            }
            catch (Exception)
            {
                MessageBox.Show("Invalid URL/URL does not Exist!", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
            }
        }

        private void btnShowInfo_Click(object sender, EventArgs e)
        {

        }

        private void btnAddSpecs_Click(object sender, EventArgs e)
        {
            var row = (TechSpecsBasis)gridTSBasis.GetFocusedRow();
            var frm = new frmTechSpecsBasisDetails(row);
            frm.ShowDialog();
        }

        private void btnCopySpecs_Click(object sender, EventArgs e)
        {
            var row = (TechSpecsBasis)gridTSBasis.GetFocusedRow();
            SpecsDetails = row.TechSpecsBasisDetails;

            var msgRes = MessageBox.Show("Copy Specs from this Basis?", "Confirmation!", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
            if (msgRes == DialogResult.Cancel) return;

            this.Close();
        }

        private void frmTechSpecsBasis_Load(object sender, EventArgs e)
        {
            LoadSpecsBasis();
        }
    }
}