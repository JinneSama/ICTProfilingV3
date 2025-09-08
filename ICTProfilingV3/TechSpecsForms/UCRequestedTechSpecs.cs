using ICTProfilingV3.DataTransferModels.ViewModels;
using ICTProfilingV3.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Models.Entities;
using Models.Enums;
using System;
using System.Linq;
using System.Windows.Forms;

namespace ICTProfilingV3.TechSpecsForms
{
    public partial class UCRequestedTechSpecs : DevExpress.XtraEditors.XtraUserControl
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ITechSpecsService _tsService;
        private TechSpecs _techSpecs;

        public UCRequestedTechSpecs(IServiceProvider serviceProvider, ITechSpecsService techSpecsService)
        {
            _serviceProvider = serviceProvider;
            _tsService = techSpecsService;
            InitializeComponent();
        }

        public void InitUC(TechSpecs techSpecs, bool forViewing = true)
        {
            _techSpecs = techSpecs;
            SetHiddenButtons(!forViewing);
            LoadTSEquipments();
        }

        private void SetHiddenButtons(bool forViewing)
        {
            colDelete.Visible = forViewing;
            colEdit.Visible = forViewing;
            btnAddTS.Visible = forViewing;
            colAddSpecs.Visible = forViewing;
        }

        private void LoadTSEquipments()
        {
            var ictSpecs = _tsService.GetTSICTSpecs(_techSpecs.Id);
            gcICTSpecs.DataSource = ictSpecs.ToList();
        }

        private void btnAddTS_Click(object sender, System.EventArgs e)
        {
            var row = new TechSpecsICTSpecsViewModel { TechSpecsId = _techSpecs.Id };
            var frm = _serviceProvider.GetRequiredService<frmAddEditTechSpecsICTSpecs>();
            frm.InitForm(row, SaveType.Insert);
            frm.ShowDialog();
            
            LoadTSEquipments();
        }

        private void btnInfo_Click(object sender, System.EventArgs e)
        {
            var focusedRow = gridICTSpecs.FocusedRowHandle;
            gridICTSpecs.SetMasterRowExpanded(focusedRow, !gridICTSpecs.GetMasterRowExpanded(focusedRow));
        }

        private void btnEditData_Click(object sender, System.EventArgs e)
        {
            var row = (TechSpecsICTSpecsViewModel)gridICTSpecs.GetFocusedRow();
            var frm = _serviceProvider.GetRequiredService<frmAddEditTechSpecsICTSpecs>();
            frm.InitForm(row, SaveType.Update);
            frm.ShowDialog();

            LoadTSEquipments();
        }

        private async void btnAddSpecs_Click(object sender, System.EventArgs e)
        {
            var row = (TechSpecsICTSpecsViewModel)gridICTSpecs.GetFocusedRow();
            //var ictSpecs = await unitOfWork.TechSpecsICTSpecsRepo.FindAsync(x => x.Id == row.Id,
            //    x => x.EquipmentSpecs.Equipment);

            var ictSpecs = await _tsService.GetTSICTSpecsById(row.Id);
            var frm = _serviceProvider.GetRequiredService<frmAddEditTSICTSpecsDetails>();
            frm.InitForm(ictSpecs);
            frm.ShowDialog();

            LoadTSEquipments();
        }

        private void btnDelete_Click(object sender, System.EventArgs e)
        {
            if (MessageBox.Show("Delete this Specs?", "Confirmation", MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Exclamation) == DialogResult.Cancel) return;

            var row = (TechSpecsICTSpecsViewModel)gridICTSpecs.GetFocusedRow();
            _tsService.DeleteTechSpecsICTSpecsById(row.Id);
        }
    }
}
