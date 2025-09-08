using ICTProfilingV3.BaseClasses;
using ICTProfilingV3.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ICTProfilingV3.LookUpTables
{
    public partial class frmTechSpecsBasis : BaseForm
    {
        private readonly ITechSpecsService _tsService;
        private readonly IEquipmentService _equipmentService;
        private readonly IServiceProvider _serviceProvider;
        public bool _copy { get; set; }
        public IEnumerable<TechSpecsBasisDetails> SpecsDetails { get; set; }
        public frmTechSpecsBasis(ITechSpecsService techSpecsService, IEquipmentService equipmentService,
            IServiceProvider serviceProvider)
        {
            _tsService = techSpecsService;
            _equipmentService = equipmentService;
            _serviceProvider = serviceProvider;
            InitializeComponent();
        }

        private async void LoadSpecsBasis()
        {
            colCopy.Visible = _copy;
            var equipmentDropdown = _equipmentService.EquipmentSpecsBaseService.GetAll().Include(x => x.Equipment);
            equipmentSpecsBindingSource.DataSource = equipmentDropdown.ToList();

            var equipmentBasis = await _tsService.TechSpecsBasisBaseService.GetAll()
                .Include(x => x.EquipmentSpecs)
                .Include(x => x.EquipmentSpecs.Equipment)
                .Include(x => x.TechSpecsBasisDetails).ToListAsync();
            gcTSBasis.DataSource = new BindingList<TechSpecsBasis>(equipmentBasis);
        }

        private async void gridTSBasis_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            var row = (TechSpecsBasis)gridTSBasis.GetFocusedRow();
            var check = await _tsService.TechSpecsBasisBaseService.GetByIdAsync(row.Id);
            if(check == null) InsertSpecs(row);
            else await UpdateSpecs(row);
        }

        private async Task UpdateSpecs(TechSpecsBasis row)
        {
            var res = await _tsService.TechSpecsBasisBaseService.GetByIdAsync(row.Id);
            res.PriceRange = row.PriceRange;
            res.PriceDate = row.PriceDate;
            res.URLBasis = row.URLBasis;
            res.Remarks = row.Remarks;
            res.Available = row.Available;
            res.EquipmentSpecsId = row.EquipmentSpecsId;
            await _tsService.TechSpecsBasisBaseService.SaveChangesAsync();
        }

        private void InsertSpecs(TechSpecsBasis row)
        {
            _tsService.TechSpecsBasisBaseService.AddAsync(row);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Delete this Basis?", "Confirmation!",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) == DialogResult.Cancel) return;

            var row = (TechSpecsBasis)gridTSBasis.GetFocusedRow();
            _tsService.TechSpecsBasisBaseService.DeleteAsync(row.Id);
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
            var frm = _serviceProvider.GetRequiredService<frmTechSpecsBasisDetails>();
            frm.InitForm(row);
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