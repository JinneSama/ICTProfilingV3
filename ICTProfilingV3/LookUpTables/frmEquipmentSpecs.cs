using ICTProfilingV3.BaseClasses;
using ICTProfilingV3.DataTransferModels.ViewModels;
using ICTProfilingV3.Equipments;
using ICTProfilingV3.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ICTProfilingV3.LookUpTables
{
    public partial class frmEquipmentSpecs : BaseForm
    {
        private readonly ITechSpecsService _tsService;
        private readonly IEquipmentService _equipmentService;
        private readonly IServiceProvider _serviceProvider;
        public bool _copy { get; set; }
        public IEnumerable<EquipmentSpecsDetails> SpecsDetails { get; set; } 
        public frmEquipmentSpecs(ITechSpecsService techSpecsService, IEquipmentService equipmentService,
            IServiceProvider serviceProvider)
        {
            _tsService = techSpecsService;
            _equipmentService = equipmentService;
            _serviceProvider = serviceProvider;
            InitializeComponent();
            LoadDropdowns();
        }

        private void LoadDropdowns()
        {
            var dropdownData = _equipmentService.GetAll();
            lueEquipment.DataSource = dropdownData.ToList();
        }

        private void LoadEquipmentSpecs()
        {
            colCopy.Visible = _copy;

            var res = _equipmentService.EquipmentSpecsBaseService.GetAll().Include(x => x.EquipmentSpecsDetails).ToList();
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

        private async void btnDeleteEquipment_ClickAsync(object sender, EventArgs e)
        {
            var msgRes = MessageBox.Show("Delete Specs?", "Confirmation!", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
            if (msgRes == DialogResult.Cancel) return;

            var row = (EquipmentSpecsViewModel)gridEquipment.GetFocusedRow();
            if (row == null) return;

            await _equipmentService.EquipmentSpecsBaseService.DeleteAsync(row.Id);

            LoadEquipmentSpecs();
        }

        private async void gridEquipment_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            var row = (EquipmentSpecsViewModel)gridEquipment.GetFocusedRow();
            var res = await _equipmentService.EquipmentSpecsBaseService.GetByIdAsync(row.Id);
            if (res == null) await InsertEquipment(row);
            else await UpdateEquipment(row);

            LoadEquipmentSpecs();
        }

        private async Task UpdateEquipment(EquipmentSpecsViewModel row)
        {
            var equipment = await _equipmentService.EquipmentSpecsBaseService.GetByIdAsync(row.Id);
            equipment.EquipmentId = row.EquipmentId.Value;
            equipment.Description = row.Description; 
            equipment.Remarks = row.Remarks;
            await _equipmentService.EquipmentSpecsBaseService.SaveChangesAsync();
        }

        private async Task InsertEquipment(EquipmentSpecsViewModel row)
        {
            var equipmentSpecs = new EquipmentSpecs
            {
                Description = row.Description,
                Remarks = row.Remarks,
                EquipmentId = row.EquipmentId.Value
            };
            await _equipmentService.EquipmentSpecsBaseService.AddAsync(equipmentSpecs);
        }

        private async void btnAddSpecs_Click(object sender, EventArgs e)
        {
            var row = (EquipmentSpecsViewModel)gridEquipment.GetFocusedRow(); 
            var res = await _equipmentService.EquipmentSpecsBaseService.GetByIdAsync(row.Id);
            var frm = _serviceProvider.GetRequiredService<frmEquipmentSpecsDetails>();
            frm.InitForm(res);
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