using ICTProfilingV3.BaseClasses;
using ICTProfilingV3.Interfaces;
using ICTProfilingV3.Services.Employees;
using Microsoft.Extensions.DependencyInjection;
using Models.Entities;
using Models.Enums;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ICTProfilingV3.PPEInventoryForms
{
    public partial class frmAddEditPPEs : BaseForm
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IPPEInventoryService _ppeService;
        private readonly IControlMapper<PPEs> _ppeControlMapper;
        private PPEs _ppe;
        private SaveType _saveType;
        private bool _isSave = false;

        public frmAddEditPPEs(IServiceProvider serviceProvider, IPPEInventoryService ppeService,
            IControlMapper<PPEs> ppeControlMapper)
        {
            _serviceProvider = serviceProvider;
            _ppeService = ppeService;
            _ppeControlMapper = ppeControlMapper;
            InitializeComponent();
        }

        public async Task InitForm(SaveType saveType, PPEs ppe)
        {
            LoadDropdowns();
            _saveType = saveType;
            if (saveType == SaveType.Insert) await CreatePPE();
            else
            {
                _ppe = ppe;
                _isSave = true;
            }
            LoadDetails();
        }
        private async Task CreatePPE()
        {
            var ppe = await _ppeService.AddAsync(new PPEs());
            _ppe = ppe;
        }

        private void LoadDetails()
        {
            LoadEquipmentSpecs();
            if (_saveType == SaveType.Insert) return;
            _ppeControlMapper.MapControl(_ppe, groupControl2);

            //slueEmployee.EditValue = _PPEs.IssuedToId;
            //txtContactNo.Text = _PPEs.ContactNo;
            //rdbtnGender.SelectedIndex = (int)(_PPEs?.Gender ?? 0);
            //txtPropertyNo.Text = _PPEs.PropertyNo;
            //spinQty.Value = _PPEs.Quantity;
            //spinUnitCost.Value = (decimal)(_PPEs.UnitValue ?? 0);
            //spintTotal.Value = (decimal)(_PPEs.TotalValue ?? 0);
            //cboUnit.EditValue = _PPEs.Unit;
            //txtInvoiceDate.DateTime = _PPEs?.DateCreated ?? DateTime.Now;
            //cboStatus.EditValue = _PPEs.Status;
            //txtRemarks.Text = _PPEs.Remarks;    
        }

        private void LoadDropdowns()
        {
            var employees = HRMISEmployees.GetEmployees();
            slueIssuedToId.Properties.DataSource = employees;

            lueUnit.Properties.DataSource = Enum.GetValues(typeof(Unit)).Cast<Unit>().Select(x => new
            {
                Unit = x
            });

            lueStatus.Properties.DataSource = Enum.GetValues(typeof(PPEStatus)).Cast<PPEStatus>().Select(x => new
            {
                Status = x
            });
        }

        private void LoadEquipmentSpecs()
        {
            var navigation = _serviceProvider.GetRequiredService<IControlNavigator<UCPPEsSpecs>>();
            navigation.NavigateTo(gcEquipmentSpecs, act => act.InitUC(_ppe));
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            var editPPE = await _ppeService.GetByIdAsync(_ppe.Id);
            _ppeControlMapper.MapToEntity(editPPE, groupControl2);
            await _ppeService.SaveChangesAsync();

            _isSave = true;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void frmAddEditPPEs_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!_isSave) await DeletePPE();
        }

        private async Task DeletePPE()
        {
            await _ppeService.DeleteAsync(_ppe.Id);
        }
        private void CalcTotalValue()
        {
            spintTotalValue.Value = spinUnitValue.Value * spinQuantity.Value;
        }

        private void spinUnitCost_EditValueChanged(object sender, EventArgs e)
        {
            CalcTotalValue();
        }

        private void spinQty_EditValueChanged(object sender, EventArgs e)
        {
            CalcTotalValue();
        }
    }
}