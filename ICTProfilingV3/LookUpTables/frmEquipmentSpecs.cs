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
    public partial class frmEquipmentSpecs : BaseForm
    {
        private IUnitOfWork unitOfWork;
        public bool Copy { get; set; }
        public IEnumerable<EquipmentSpecsDetails> SpecsDetails { get; set; } 
        public frmEquipmentSpecs()
        {
            InitializeComponent();
            unitOfWork = new UnitOfWork();
            LoadDropdowns();
        }

        private void LoadDropdowns()
        {
            lueEquipment.DataSource = unitOfWork.EquipmentRepo.GetAll().ToList();
        }

        private void LoadEquipmentSpecs()
        {
            colCopy.Visible = Copy;

            var res = unitOfWork.EquipmentSpecsRepo.GetAll(x => x.EquipmentSpecsDetails).ToList();
            var esvm = res.Select(x => new EquipmentSpecsViewModel
            {
                Id = x.Id,
                Description = x.Description,
                Remarks= x.Remarks,
                EquipmentId = x.EquipmentId,
                EquipmentSpecsDetails = x.EquipmentSpecsDetails
            });
            gcEquipment.DataSource = new BindingList<EquipmentSpecsViewModel>(esvm.ToList());
        }

        private void btnDeleteEquipment_Click(object sender, EventArgs e)
        {
            var msgRes = MessageBox.Show("Delete Specs?", "Confirmation!", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
            if (msgRes == DialogResult.Cancel) return;

            var row = (EquipmentSpecsViewModel)gridEquipment.GetFocusedRow();
            if (row == null) return;

            unitOfWork.EquipmentSpecsRepo.DeleteByEx(x => x.Id == row.Id);
            unitOfWork.Save();

            LoadEquipmentSpecs();
        }

        private async void gridEquipment_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            var row = (EquipmentSpecsViewModel)gridEquipment.GetFocusedRow();
            var res = await unitOfWork.EquipmentSpecsRepo.FindAsync(x => x.Id == row.Id);
            if (res == null) InsertEquipment(row);
            else UpdateEquipment(row);

            LoadEquipmentSpecs();
        }

        private async void UpdateEquipment(EquipmentSpecsViewModel row)
        {
            var equipment = await unitOfWork.EquipmentSpecsRepo.FindAsync(x => x.Id == row.Id);
            equipment.EquipmentId = row.EquipmentId.Value;
            equipment.Description = row.Description; 
            equipment.Remarks = row.Remarks;
            unitOfWork.EquipmentSpecsRepo.Update(equipment);
            unitOfWork.Save();
        }

        private void InsertEquipment(EquipmentSpecsViewModel row)
        {
            var equipmentSpecs = new EquipmentSpecs
            {
                Description = row.Description,
                Remarks = row.Remarks,
                EquipmentId = row.EquipmentId.Value
            };
            unitOfWork.EquipmentSpecsRepo.Insert(equipmentSpecs);
            unitOfWork.Save();
        }

        private async void btnAddSpecs_Click(object sender, EventArgs e)
        {
            var row = (EquipmentSpecsViewModel)gridEquipment.GetFocusedRow(); 
            var res = await unitOfWork.EquipmentSpecsRepo.FindAsync(x => x.Id == row.Id);
            var frm = new frmEquipmentSpecsDetails(res,unitOfWork);
            frm.ShowDialog();

            LoadEquipmentSpecs();
        }

        private void btnShowInfo_Click(object sender, EventArgs e)
        {
            var focusedRow = gridEquipment.FocusedRowHandle;
            gridEquipment.SetMasterRowExpanded(focusedRow, !gridEquipment.GetMasterRowExpanded(focusedRow));
        }

        private void btnCopySpecs_Click(object sender, EventArgs e)
        {
            var row = (EquipmentSpecsViewModel)gridEquipment.GetFocusedRow();
            SpecsDetails = row.EquipmentSpecsDetails;

            var msgRes = MessageBox.Show("Copy Specs from this Equipment?", "Confirmation!", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
            if (msgRes == DialogResult.Cancel) return;

            this.Close();
        }

        private void frmEquipmentSpecs_Load(object sender, EventArgs e)
        {
            LoadEquipmentSpecs();
        }
    }
}