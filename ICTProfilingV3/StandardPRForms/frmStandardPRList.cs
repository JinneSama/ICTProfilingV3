using DevExpress.CodeParser;
using DevExpress.XtraEditors;
using Models.Entities;
using Models.Enums;
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

namespace ICTProfilingV3.StandardPRForms
{
    public partial class frmStandardPRList : DevExpress.XtraEditors.XtraForm
    {
        private readonly int _prId;
        private readonly PRQuarter _quarter;
        private readonly bool fromSelect;
        private readonly IUnitOfWork unitOfWork;
        public frmStandardPRList()
        {
            InitializeComponent();
            unitOfWork = new UnitOfWork();
            LoadDropdowns();
            gridMark.Visible = false;
            btnAddMarked.Enabled = false;
        }

        public frmStandardPRList(int PRId, PRQuarter Quarter , IUnitOfWork _unitOfWork)
        {
            InitializeComponent();
            this.unitOfWork = _unitOfWork;
            _prId = PRId;
            _quarter = Quarter;
            fromSelect = true;
            btnAddStandardPR.Enabled = false;
            btnPrint.Enabled = false;
            lueQuarter.EditValue = _quarter;
            lueQuarter.Enabled = false;
            gridAddSpecs.Visible = false;
            gridDelete.Visible = false;
        }
        private void LoadStandardPR()
        {
            var quarter = (PRQuarter)lueQuarter.EditValue;

            var spr = unitOfWork.StandardPRSpecsRepo.FindAllAsync(x => x.Quarter == quarter,
                x => x.StandardPRSpecsDetails,
                x => x.EquipmentSpecs,
                x => x.EquipmentSpecs.Equipment).Select(x => new StandardPRViewModel
            {
                Equipment = x.EquipmentSpecs.Equipment.EquipmentName,
                StandardPRSpecs = x,
                StandardPRSpecsDetails = x.StandardPRSpecsDetails
            });
            gcPR.DataSource = new BindingList<StandardPRViewModel>(spr.ToList());

            gridSpecs.Columns["Type"].Group();
            gridSpecs.ExpandAllGroups();
        }

        private void LoadDropdowns()
        {
            lueQuarter.Properties.DataSource = Enum.GetValues(typeof(PRQuarter)).Cast<PRQuarter>().Select(x => new
            {
                Id = x,
                QuarterName = EnumHelper.GetEnumDescription(x)
            });
            lueQuarter.EditValue = Enum.GetValues(typeof(PRQuarter)).Cast<PRQuarter>().Max();
        }

        private void frmStandardPRList_Load(object sender, EventArgs e)
        {
            LoadStandardPR();
        }

        private void btnAddStandardPR_Click(object sender, EventArgs e)
        {
            var frm = new frmAddEditStandardPRSpecs(unitOfWork);
            frm.ShowDialog();

            LoadStandardPR();
        }

        private void lueQuarter_EditValueChanged(object sender, EventArgs e)
        {
            LoadStandardPR();
        }

        private void btnAddSpecs_Click(object sender, EventArgs e)
        {
            var row = (StandardPRViewModel)gridSpecs.GetFocusedRow();
            var frm = new frmAddEditStandardPRSpecsDetails(unitOfWork , row.StandardPRSpecs);
            frm.ShowDialog();

            LoadStandardPR();
        }

        private void btnExpandDetail_Click(object sender, EventArgs e)
        {
            var focusedRow = gridSpecs.FocusedRowHandle;
            gridSpecs.SetMasterRowExpanded(focusedRow, !gridSpecs.GetMasterRowExpanded(focusedRow));
        }

        private void btnDeleteEquipment_Click(object sender, EventArgs e)
        {
            var msgRes = MessageBox.Show("Delete this Equipment?", "Confirmation", MessageBoxButtons.OKCancel,
                MessageBoxIcon.Exclamation);
            if (msgRes == DialogResult.Cancel) return;

            var row = (StandardPRViewModel)gridSpecs.GetFocusedRow();
            unitOfWork.StandardPRSpecsDetailsRepo.DeleteRange(x => x.StandardPRSpecsId == row.StandardPRSpecs.Id);
            unitOfWork.Save();

            unitOfWork.StandardPRSpecsRepo.DeleteByEx(x => x.Id == row.StandardPRSpecs.Id);
            unitOfWork.Save();

            LoadStandardPR();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            var row = (StandardPRViewModel)gridSpecs.GetFocusedRow();
            var frm = new frmAddEditStandardPRSpecs(unitOfWork , row.StandardPRSpecs);
            frm.ShowDialog();

            LoadStandardPR();
        }

        private void btnExpandCollapse_Click(object sender, EventArgs e)
        {
            for (var i = 0; i < gridSpecs.RowCount; i++)
            {
                var focusedRow = gridSpecs.GetRowHandle(i);
                gridSpecs.SetMasterRowExpanded(focusedRow, !gridSpecs.GetMasterRowExpanded(focusedRow));
            }
        }

        private async void btnAddMarked_Click(object sender, EventArgs e)
        {
            var msgRes = MessageBox.Show("Continue to Add Equipments?", "Confirmation", MessageBoxButtons.OKCancel,
                MessageBoxIcon.Exclamation);
            if (msgRes == DialogResult.Cancel) return;

            await AddMarked();
            this.Close();
        }

        private async Task AddMarked()
        {
            for (var i = 0; i < gridSpecs.RowCount - 2; i++)
            {
                if (!(bool)gridSpecs.GetRowCellValue(i, "Mark")) continue;
                var res = (StandardPRViewModel)gridSpecs.GetRow(i);

                var prSPR = new PRStandardPRSpecs
                {
                    PurchaseRequestId = _prId,
                    StandardPRSpecsId = res.StandardPRSpecs.Id
                };

                unitOfWork.PRStandardPRSpecsRepo.Insert(prSPR);
            }
            await unitOfWork.SaveChangesAsync();
        }
    }
}