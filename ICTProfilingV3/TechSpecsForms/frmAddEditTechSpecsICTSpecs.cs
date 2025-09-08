using ICTProfilingV3.BaseClasses;
using ICTProfilingV3.DataTransferModels.ViewModels;
using ICTProfilingV3.Interfaces;
using ICTProfilingV3.LookUpTables;
using Microsoft.Extensions.DependencyInjection;
using Models.Entities;
using Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ICTProfilingV3.TechSpecsForms
{
    public partial class frmAddEditTechSpecsICTSpecs : BaseForm
    {
        private readonly IControlMapper<TechSpecsICTSpecsViewModel> _controlMapper;
        private readonly IControlMapper<TechSpecsICTSpecs> _tsICTMapper;
        private readonly IEquipmentService _equipmentService;
        private readonly IServiceProvider _serviceProvider;
        private readonly ITechSpecsService _techSpecsService;
        private TechSpecsICTSpecsViewModel _specs;
        private SaveType _saveType;

        public frmAddEditTechSpecsICTSpecs(IServiceProvider serviceProvider, IControlMapper<TechSpecsICTSpecsViewModel> controlMapper,
            ITechSpecsService techSpecsService, IEquipmentService equipmentService, IControlMapper<TechSpecsICTSpecs> tsICTMapper)
        {
            _serviceProvider = serviceProvider;
            _techSpecsService = techSpecsService;
            _controlMapper = controlMapper;
            _equipmentService = equipmentService;
            _tsICTMapper = tsICTMapper;
            InitializeComponent();
            LoadDropdowns();
        }

        public void InitForm(TechSpecsICTSpecsViewModel specs, SaveType saveType)
        {
            _specs = specs;
            _saveType = saveType;
            LoadDetails();
        }

        private async Task LoadItemNo()
        {
            if (_saveType == SaveType.Update) return;

            var ts = await _techSpecsService.GetByIdAsync(_specs.TechSpecsId);
            if(ts == null) return;
            var itemNos = ts.TechSpecsICTSpecs?.OrderBy(x => x.ItemNo)?.LastOrDefault();

            seItemNo.Value = (decimal)(itemNos == null ? 0 : itemNos.ItemNo + 1);
        }

        private void LoadDetails()
        {
            if (_saveType == SaveType.Insert) return;

            _controlMapper.MapControl(_specs, this);
        }

        private void LoadDropdowns()
        {
            var unitDropdownData = Enum.GetValues(typeof(Unit)).Cast<Unit>().Select(x => new
            {
                UnitType = x
            });

            slueUnit.Properties.DataSource = unitDropdownData;
            var res = _equipmentService.GetEquipmentVM();
            slueEquipmentSpecsId.Properties.DataSource = res;
        }

        private void slueEquipment_EditValueChanged(object sender, System.EventArgs e)
        {
            var row = (EquipmentSpecsViewModel)slueEquipmentSpecsId.Properties.View.GetFocusedRow();
            if (row == null) row = _equipmentService.GetEquipmentVM().Where(x => x.Id == _specs.EquipmentSpecsId).FirstOrDefault();

            txtDescription.Text = row.Description;
        }

        private void spinUnitCost_EditValueChanged(object sender, EventArgs e)
        {
            seTotalCost.Value = seQuantity.Value * seUnitCost.Value;
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            if (_saveType == SaveType.Insert) InsertSpecs();
            else await UpdateSpecs();
            this.Close();
        }

        private async Task UpdateSpecs()
        {
            var specs = await _techSpecsService.GetTSICTSpecsById(_specs.Id);
            _tsICTMapper.MapToEntity(specs, this);
        }

        private void InsertSpecs()
        {
            var specs = new TechSpecsICTSpecs();
            _tsICTMapper.MapToEntity(specs, this);
            _techSpecsService.AddTechSpecsICTSpecsAsync(specs);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void frmAddEditTechSpecsICTSpecs_Load(object sender, EventArgs e)
        {
            await LoadItemNo();
        }

        private void btnAddICTSpecs_Click(object sender, EventArgs e)
        {
            var frm = _serviceProvider.GetRequiredService<frmEquipmentSpecs>();
            frm.ShowDialog();
            LoadDropdowns();
        }

        private void btnAddEquipment_Click(object sender, EventArgs e)
        {
            var frm = _serviceProvider.GetRequiredService<frmEquipment>();
            frm.ShowDialog();
            LoadDropdowns();
        }

        private void btnClearUOM_Click(object sender, EventArgs e)
        {
            slueUnit.EditValue = null;
        }

        private void btnClearQuantity_Click(object sender, EventArgs e)
        {
            seQuantity.EditValue = null;
        }

        private void spinQuantity_EditValueChanged_1(object sender, EventArgs e)
        {
            seTotalCost.Value = seQuantity.Value * seUnitCost.Value;
        }
    }
}