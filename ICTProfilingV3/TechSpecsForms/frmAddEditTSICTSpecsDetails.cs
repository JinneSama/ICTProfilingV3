using ICTProfilingV3.BaseClasses;
using ICTProfilingV3.Interfaces;
using ICTProfilingV3.LookUpTables;
using Microsoft.Extensions.DependencyInjection;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ICTProfilingV3.TechSpecsForms
{
    public partial class frmAddEditTSICTSpecsDetails : BaseForm
    {
        private readonly ITechSpecsService _tsService;
        private readonly IServiceProvider _serviceProvider;
        private TechSpecsICTSpecs _specs;
        public frmAddEditTSICTSpecsDetails(ITechSpecsService tsService, IServiceProvider serviceProvider)
        {
            _tsService = tsService;
            _serviceProvider = serviceProvider;
            InitializeComponent(); 
        }

        public void InitForm(TechSpecsICTSpecs specs)
        {
            _specs = specs;
            lblEquipment.Text = specs?.EquipmentSpecs?.Equipment?.EquipmentName;
            lblDescription.Text = specs?.Description;
        }
        private void LoadSpecs()
        {
            //var data = unitOfWork.TechSpecsICTSpecsDetailsRepo.FindAllAsync(x => x.TechSpecsICTSpecsId == _specs.Id).OrderBy(o => o.ItemNo);
            var data = _tsService.GetTSICTSpecsDetails().Where(x => x.TechSpecsICTSpecsId == _specs.Id).OrderBy(o => o.ItemNo);
            gcEquipmentDetails.DataSource = new BindingList<TechSpecsICTSpecsDetails>(data.ToList());
        }

        private async Task UpdateSpecs(TechSpecsICTSpecsDetails row)
        {
            //var specs = await unitOfWork.TechSpecsICTSpecsDetailsRepo.FindAsync(x => x.Id == row.Id);
            var specs = await _tsService.GetTSICTSpecsDetailById(row.Id);
            specs.ItemNo = row.ItemNo;
            specs.Specs = row.Specs;
            specs.Description = row.Description;

            await _tsService.SaveTSICTSpecsDetailsAsync();
            //unitOfWork.TechSpecsICTSpecsDetailsRepo.Update(specs);
            //unitOfWork.Save();
        }

        private async Task InsertSpecs(TechSpecsICTSpecsDetails row)
        {
            var equipmentDetail = new TechSpecsICTSpecsDetails
            {
                ItemNo = row.ItemNo,
                Specs = row.Specs,
                Description = row.Description,
                TechSpecsICTSpecsId = _specs.Id
            };
            await _tsService.AddTechSpecsICTSpecsDetailAsync(equipmentDetail);
            LoadSpecs();
        }

        private async void gridEquipmentDetails_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            var row = (TechSpecsICTSpecsDetails)gridEquipmentDetails.GetFocusedRow();
            var res = await _tsService.GetTSICTSpecsDetailById(row.Id);
            if (res == null) await InsertSpecs(row);
            else await UpdateSpecs(row);
        }

        private async void btnDelete_Click(object sender, System.EventArgs e)
        {
            var msgRes = MessageBox.Show("Delete this Specs?", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
            if (msgRes == DialogResult.Cancel) return;

            var equipment = (TechSpecsICTSpecsDetails)gridEquipmentDetails.GetFocusedRow();
            var res = await _tsService.GetTSICTSpecsDetailById(equipment.Id);
            if (res == null) return;

            await _tsService.DeleteTechSpecsICTSpecsDetailById(equipment.Id);

            LoadSpecs();
        }

        private async void btnCopySpecs_Click(object sender, System.EventArgs e)
        {
            var msgRes = MessageBox.Show("This will Overwrite Existing Specs?", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
            if (msgRes == DialogResult.Cancel) return;
            var frm = _serviceProvider.GetRequiredService<frmEquipmentSpecs>();
            frm._copy = true;
            frm.ShowDialog();
            await OverwriteSpecs(frm.SpecsDetails);
        }

        private async Task OverwriteSpecs(IEnumerable<EquipmentSpecsDetails> specsDetails)
        {
            if (!specsDetails.Any() || specsDetails == null) return; 
            await _tsService.DeleteTechSpecsICTSpecsDetailRange(x => x.TechSpecsICTSpecsId == _specs.Id);

            foreach (var spec in specsDetails)
            {
                var equipmentDetail = new TechSpecsICTSpecsDetails
                {
                    ItemNo = spec.ItemNo,
                    Specs = spec.DetailSpecs,
                    Description = spec.DetailDescription,
                    TechSpecsICTSpecsId = _specs.Id
                };
                await _tsService.AddTechSpecsICTSpecsDetailAsync(equipmentDetail);
            }
            LoadSpecs();
        }

        private async Task OverwriteSpecsFromTSBasis(IEnumerable<TechSpecsBasisDetails> specsDetails)
        {
            if (!specsDetails.Any() || specsDetails == null) return;
            await _tsService.DeleteTechSpecsICTSpecsDetailRange(x => x.TechSpecsICTSpecsId == _specs.Id);

            foreach (var spec in specsDetails)
            {
                var equipmentDetail = new TechSpecsICTSpecsDetails
                {
                    ItemNo = spec.ItemNo,
                    Specs = spec.Specs,
                    Description = spec.Description,
                    TechSpecsICTSpecsId = _specs.Id
                };
                await _tsService.AddTechSpecsICTSpecsDetailAsync(equipmentDetail);
            }
            LoadSpecs();
        }

        private async void btnCopyTSBasis_Click(object sender, System.EventArgs e)
        {
            var msgRes = MessageBox.Show("This will Overwrite Existing Specs?", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
            if (msgRes == DialogResult.Cancel) return;

            var frm = _serviceProvider.GetRequiredService<frmTechSpecsBasis>();
            frm._copy = true;
            frm.ShowDialog();
            await OverwriteSpecsFromTSBasis(frm.SpecsDetails);
        }

        private void frmAddEditTSICTSpecsDetails_Load(object sender, EventArgs e)
        {
            LoadSpecs();
        }
    }
}