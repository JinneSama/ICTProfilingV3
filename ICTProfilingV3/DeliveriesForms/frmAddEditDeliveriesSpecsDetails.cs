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

namespace ICTProfilingV3.DeliveriesForms
{
    public partial class frmAddEditDeliveriesSpecsDetails : BaseForm
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ITechSpecsService _techSpecsService;
        private readonly IDeliveriesService _deliveriesService;
        private DeliveriesSpecs _specs;
        public frmAddEditDeliveriesSpecsDetails(IServiceProvider serviceProvider, IDeliveriesService deliveriesService, ITechSpecsService techSpecsService)
        {
            _serviceProvider = serviceProvider;
            _deliveriesService = deliveriesService;
            _techSpecsService = techSpecsService;
            InitializeComponent();
        }

        public void InitForm(DeliveriesSpecs specs)
        {
            _specs = specs;
            lblEquipment.Text = specs?.Model?.Brand?.EquipmentSpecs?.Equipment?.EquipmentName ?? "";
            lblDescription.Text = specs?.Model?.Brand?.EquipmentSpecs?.Description ?? "";
            LoadSpecs();
        }

        private void LoadSpecs()
        {
            var data = _deliveriesService.DeliveriesSpecsDetailsBaseService.GetAll().Where(x => x.DeliveriesSpecsId == _specs.Id).OrderBy(o => o.ItemNo);
            gcEquipmentDetails.DataSource = new BindingList<DeliveriesSpecsDetails>(data.ToList());
        }

        private async Task UpdateSpecs(DeliveriesSpecsDetails row)
        {
            //var specs = await unitOfWork.DeliveriesSpecsDetailsRepo.FindAsync(x => x.Id == row.Id);
            var specs = await _deliveriesService.DeliveriesSpecsDetailsBaseService.GetByIdAsync(row.Id);
            
            specs.ItemNo = row.ItemNo;
            specs.Specs = row.Specs;
            specs.Description = row.Description;
            await _deliveriesService.DeliveriesSpecsDetailsBaseService.SaveChangesAsync();
        }

        private async Task InsertSpecs(DeliveriesSpecsDetails row)
        {
            var equipmentDetail = new DeliveriesSpecsDetails
            {
                ItemNo = row.ItemNo,
                Specs = row.Specs,
                Description = row.Description,
                DeliveriesSpecsId = _specs.Id
            };
            await _deliveriesService.DeliveriesSpecsDetailsBaseService.AddAsync(equipmentDetail);
            LoadSpecs();
        }

        private async void gridEquipmentDetails_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            var row = (DeliveriesSpecsDetails)gridEquipmentDetails.GetFocusedRow();
            var res = await _deliveriesService.DeliveriesSpecsDetailsBaseService.GetByIdAsync(row.Id);
            if (res == null) await InsertSpecs(row);
            else await UpdateSpecs(row);
        }

        private async void btnDelete_Click(object sender, System.EventArgs e)
        {
            var msgRes = MessageBox.Show("Delete this Specs?", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
            if (msgRes == DialogResult.Cancel) return;

            var equipment = (DeliveriesSpecsDetails)gridEquipmentDetails.GetFocusedRow();
            var res = await _deliveriesService.DeliveriesSpecsDetailsBaseService.GetByIdAsync(equipment.Id);
            if (res == null) return;

            await _deliveriesService.DeliveriesSpecsDetailsBaseService.DeleteAsync(equipment.Id);
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

            await _deliveriesService.DeliveriesSpecsDetailsBaseService.DeleteRangeAsync(x => x.DeliveriesSpecsId == _specs.Id);

            foreach (var spec in specsDetails)
            {
                var equipmentDetail = new DeliveriesSpecsDetails
                {
                    ItemNo = spec.ItemNo,
                    Specs = spec.DetailSpecs,
                    Description = spec.DetailDescription,
                    DeliveriesSpecsId = _specs.Id
                };
                await _deliveriesService.DeliveriesSpecsDetailsBaseService.AddAsync(equipmentDetail);
            }
            LoadSpecs();
        }

        private async Task OverwriteSpecsFromTSBasis(IEnumerable<TechSpecsBasisDetails> specsDetails)
        {
            if (!specsDetails.Any() || specsDetails == null) return;
            await _techSpecsService.DeleteTechSpecsICTSpecsDetailRange(x => x.TechSpecsICTSpecsId == _specs.Id);

            foreach (var spec in specsDetails)
            {
                var equipmentDetail = new DeliveriesSpecsDetails
                {
                    ItemNo = spec.ItemNo,
                    Specs = spec.Specs,
                    Description = spec.Description,
                    DeliveriesSpecsId = _specs.Id
                };
                await _deliveriesService.DeliveriesSpecsDetailsBaseService.AddAsync(equipmentDetail);
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
    }
}