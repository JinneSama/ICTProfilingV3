using ICTProfilingV3.DataTransferModels.ViewModels;
using ICTProfilingV3.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Models.Entities;
using System;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ICTProfilingV3.DeliveriesForms
{
    public partial class UCDeliveriesSpecs : DevExpress.XtraEditors.XtraUserControl
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IDeliveriesService _deliveriesService;
        private int _deliveriesId;

        public UCDeliveriesSpecs(IServiceProvider serviceProvider, IDeliveriesService deliveriesService)
        {
            _serviceProvider = serviceProvider;
            _deliveriesService = deliveriesService;
            InitializeComponent();
        }

        public void InitUC(int deliveriesId, bool forViewing = true)
        {
            _deliveriesId = deliveriesId;
            SetHiddenButtons(!forViewing);
        }

        private void SetHiddenButtons(bool forViewing)
        {
            colDelete.Visible = forViewing;
            colEdit.Visible = forViewing;
            btnAddEquipment.Visible = forViewing;
            colAddSpecs.Visible = forViewing;
        }

        private void LoadEquipmentSpecs()
        {
            var res = _deliveriesService.DeliveriesSpecsBaseService.GetAll()
                .Include(x => x.Model)
                .Include(x => x.Model.Brand)
                .Include(x => x.Model.Brand.EquipmentSpecs)
                .Include(x => x.Model.Brand.EquipmentSpecs.Equipment)
                .Where(x => x.DeliveriesId == _deliveriesId)
                .ToList();
            var specs = res.Select(x => new DeliveriesSpecsViewModel
            {
                Id = x.Id,
                ItemNo = (int)x.ItemNo,
                Quantity = (int)x.Quantity,
                Unit = x.Unit,
                Equipment = x.Model.Brand.EquipmentSpecs.Equipment.EquipmentName,
                Description = x.Description,
                Brand = x.Model.Brand.BrandName,
                Model = x.Model.ModelName,
                UnitCost = (long)x.UnitCost,
                TotalCost = (long)x.TotalCost,
                DeliveriesSpecsDetails = x.DeliveriesSpecsDetails.OrderBy(o => o.ItemNo).ToList()
            });
            gcEquipmentSpecs.DataSource = new BindingList<DeliveriesSpecsViewModel>(specs.ToList());
        }

        private async void btnAddEquipment_Click(object sender, System.EventArgs e)
        {

            var deliveries = await _deliveriesService.GetByFilterAsync(x => x.Id == _deliveriesId, x => x.DeliveriesSpecs);
            var frm = _serviceProvider.GetRequiredService<frmAddEquipment>();
            frm.InitForm(deliveries);
            frm.ShowDialog();

            LoadEquipmentSpecs();
        }

        private void btnInfo_Click(object sender, System.EventArgs e)
        {
            var focusedRow = gridEquipmentSpecs.FocusedRowHandle;
            gridEquipmentSpecs.SetMasterRowExpanded(focusedRow, !gridEquipmentSpecs.GetMasterRowExpanded(focusedRow));
        }

        private async void btnEditData_Click(object sender, System.EventArgs e)
        {
            var row = (DeliveriesSpecsViewModel)gridEquipmentSpecs.GetFocusedRow();
            var delSpecs = await _deliveriesService.DeliveriesSpecsBaseService.GetByIdAsync(row.Id);
            var frm = _serviceProvider.GetRequiredService<frmAddEquipment>();
            frm.InitForm(delSpecs);
            frm.ShowDialog();

            LoadEquipmentSpecs();
        }

        private async void btnAddSpecs_Click(object sender, System.EventArgs e)
        {
            var row = (DeliveriesSpecsViewModel)gridEquipmentSpecs.GetFocusedRow();
            var delSpecs = await _deliveriesService.DeliveriesSpecsBaseService.GetByIdAsync(row.Id);
            var frm = _serviceProvider.GetRequiredService<frmAddEditDeliveriesSpecsDetails>();
            frm.InitForm(delSpecs);
            frm.ShowDialog();

            LoadEquipmentSpecs();
        }

        private async void UCDeliveriesSpecs_Load(object sender, System.EventArgs e)
        {
            LoadEquipmentSpecs();
        }

        private async void btnDelete_Click(object sender, System.EventArgs e)
        {
            
            if (MessageBox.Show("Delete this Specs?", "Confirmation", MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Exclamation) == DialogResult.Cancel) return;

            var row = (DeliveriesSpecsViewModel)gridEquipmentSpecs.GetFocusedRow();
            await _deliveriesService.DeliveriesSpecsBaseService.DeleteAsync(row.Id);
        }
    }
}
