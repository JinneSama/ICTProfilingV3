using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraGrid.Views.Grid;
using Models.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;
using DevExpress.XtraGrid.Views.Base;
using System.ComponentModel;
using System.Windows.Forms;
using ICTProfilingV3.ReportForms;
using System.Collections.Generic;
using ICTProfilingV3.BaseClasses;
using ICTProfilingV3.Interfaces;
using ICTProfilingV3.Core.Common;
using ICTProfilingV3.Services.Employees;
using ICTProfilingV3.DataTransferModels.ViewModels;
using ICTProfilingV3.DataTransferModels.ReportViewModel;
using Microsoft.Extensions.DependencyInjection;

namespace ICTProfilingV3.DeliveriesForms
{
    public partial class frmComparisonReport : BaseForm
    {
        private readonly UserStore _userStore;
        private readonly IICTUserManager _userManager;
        private readonly IComparisonReportService _comparisonReportService;
        private readonly IServiceProvider _serviceProvider;
        private readonly IDeliveriesService _deliveriesService;

        private Deliveries _deliveries;

        private bool IsDetail = false;
        public frmComparisonReport(UserStore userStore, IICTUserManager userManager, 
            IComparisonReportService comparisonReportService, IServiceProvider serviceProvider,
            IDeliveriesService deliveriesService)
        {
            _userManager = userManager;
            _userStore = userStore;
            _comparisonReportService = comparisonReportService;
            _serviceProvider = serviceProvider;
            _deliveriesService = deliveriesService;

            InitializeComponent();
            LoadDropdowns();
        }

        public void InitForm(Deliveries deliveries = null)
        {
            _deliveries = deliveries;
        }

        private void ExpandAll()
        {
            for (var i = 0; i < gridCR.RowCount; i++)
            {
                var focusedRow = gridCR.GetRowHandle(i);
                gridCR.SetMasterRowExpanded(focusedRow, !gridCR.GetMasterRowExpanded(focusedRow));
            }
        }

        private async Task LoadData()
        {
            var del = await _deliveriesService.GetByFilterAsync(x => x.Id == _deliveries.Id,
                x => x.Supplier,
                x => x.TicketRequest,
                x => x.TicketRequest.ITStaff,
                x => x.TicketRequest.ITStaff.Users,
                x => x.DeliveriesSpecs.Select(s => s.Model),
                x => x.DeliveriesSpecs.Select(s => s.Model.Brand),
                x => x.DeliveriesSpecs.Select(s => s.Model.Brand.EquipmentSpecs),
                x => x.DeliveriesSpecs.Select(s => s.Model.Brand.EquipmentSpecs.Equipment));

            _deliveries = del;
            lblEpisNo.Text = _deliveries.Id.ToString();

            var employee = HRMISEmployees.GetEmployeeById(_deliveries.RequestedById);
            txtDateOfDelivery.Text = _deliveries.DeliveredDate.ToString();
            txtRequestingOffice.Text = employee.Office + " " + employee.Division;
            txtSupplier.Text = _deliveries.Supplier.SupplierName;
            spinAmount.Value = (decimal)_deliveries.DeliveriesSpecs.Sum(x => (x.UnitCost * x.Quantity));

            var inspectActions = _deliveries?.Actions?.Where(x => x.SubActivityId == 1138 || x.SubActivityId == 1139).OrderBy(x => x.ActionDate);
            txtInspectedDate.DateTime = (DateTime)inspectActions?.FirstOrDefault()?.ActionDate;

            if (_deliveries.ComparisonReports.FirstOrDefault() == null) await CreateComparisonReport();
            else await LoadDetails();
            ExpandAll();
        }

        private async Task LoadDetails()
        {
            var cr = await _comparisonReportService.GetByFilterAsync(x => x.DeliveryId == _deliveries.Id,
                x => x.ComparisonReportSpecs,
                x => x.ComparisonReportSpecs.Select(s => s.ComparisonReportSpecsDetails));
            if (cr == null) return;

            var crViewModel = cr.ComparisonReportSpecs.Select(x => new ComparisonReportViewModel
            {
                CRId = (int)x.ComparisonReportId,
                CRSpecs = x,
                CRSpecsDetails = x.ComparisonReportSpecsDetails
            });

            sluePreparedBy.EditValue = cr.PreparedById;
            slueReviewedBy.EditValue = cr.ReviewedById;
            slueNotedBy.EditValue = cr.NotedById;

            gcCR.DataSource = new BindingList<ComparisonReportViewModel>(crViewModel.ToList());
        }

        private async Task CreateComparisonReport()
        {
            var cr = new ComparisonReport { DeliveryId = _deliveries.Id };
            await _comparisonReportService.AddAsync(cr);
            await LoadDetails();
        }

        private void LoadDropdowns()
        {
            var users = _userManager.GetUsers().ToList();
            sluePreparedBy.Properties.DataSource = users; 
            slueReviewedBy.Properties.DataSource = users;
            slueNotedBy.Properties.DataSource = users;  
        }

        private void gridCR_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
                popupMenu1.ShowPopup(MousePosition);

            GridView view = sender as GridView;
            GridHitInfo hi = view.CalcHitInfo(e.Location);
            if (!(hi.HitTest == GridHitTest.RowDetail))
                IsDetail = false;
            else
                IsDetail = true;
        }

        private void gridCRSpecs_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
                popupMenu1.ShowPopup(MousePosition);

            IsDetail = true;
        }

        private void btnSetDiscrepancy_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (IsDetail == false)
            {
                var rowHandle = gridCR.FocusedRowHandle;
                gridCR.SetRowCellValue(rowHandle, "CRSpecs.IsDiscrepancy", true);
            }
            else
            {
                var masterRowHandle = gridCR.FocusedRowHandle;
                var rowHandle = (gcCR.FocusedView as ColumnView).FocusedRowHandle;
                var row = gridCR.GetDetailView(masterRowHandle, 0) as GridView;
                row.SetRowCellValue(rowHandle, "IsDiscrepancy", true);
            }
        }

        private void btnClearDiscrepancy_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (IsDetail == false)
            {
                var rowHandle = gridCR.FocusedRowHandle;
                gridCR.SetRowCellValue(rowHandle, "CRSpecs.IsDiscrepancy", false);
            }
            else
            {
                var masterRowHandle = gridCR.FocusedRowHandle;
                var rowHandle = (gcCR.FocusedView as ColumnView).FocusedRowHandle;
                var row = gridCR.GetDetailView(masterRowHandle, 0) as GridView;
                row.SetRowCellValue(rowHandle, "IsDiscrepancy", false);
            }
        }

        private void btnShowInfo_Click(object sender, EventArgs e)
        {
            var focusedRow = gridCR.FocusedRowHandle;
            gridCR.SetMasterRowExpanded(focusedRow, !gridCR.GetMasterRowExpanded(focusedRow));
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            await SaveData();
        }

        private async Task SaveData()
        {
            await SaveDetails();
            for (int i = 0; i <= gridCR.RowCount; i++)
            {
                var row = (ComparisonReportViewModel)gridCR.GetRow(i);
                if (row == null) continue;
                await _comparisonReportService.ComparisonReportSpecsService.DeleteRangeAsync(x => x.ComparisonReportId == row.CRId);
                await _comparisonReportService.ComparisonReportSpecsDetailsService.DeleteRangeAsync(x => x.ComparisonReportSpecsId == row.CRSpecs.Id);
            }

            for (int i = 0; i <= gridCR.RowCount; i++)
            {
                var row = (ComparisonReportViewModel)gridCR.GetRow(i);
                if (row == null) continue;
                var detailRow = gridCR.GetDetailView(i, 0);

                var crSpecs = new ComparisonReportSpecs
                {
                    ComparisonReportId = row.CRId,
                    ItemNo = row.CRSpecs.ItemNo,
                    Quantity = row.CRSpecs.Quantity,
                    Unit = row.CRSpecs.Unit,
                    Type = row.CRSpecs.Type,
                    PR = row.CRSpecs.PR,
                    Quotation = row.CRSpecs.Quotation,
                    PO = row.CRSpecs.PO,
                    ActualDelivery = row.CRSpecs.ActualDelivery,
                    Remarks = row.CRSpecs.Remarks,
                    IsDiscrepancy = row.CRSpecs.IsDiscrepancy
                };
                await _comparisonReportService.ComparisonReportSpecsService.AddAsync(crSpecs);

                if (detailRow == null) continue;
                for (int x = 0; x < detailRow.RowCount; x++)
                {
                    var spec = (ComparisonReportSpecsDetails)detailRow.GetRow(x);
                    if (spec == null) continue;
                    var crSpecsDetails = new ComparisonReportSpecsDetails
                    {
                        ItemOrder = x + 1,
                        Type = spec.Type,
                        PR = spec.PR,
                        Quotation = spec.Quotation,
                        PO = spec.PO,
                        ActualDelivery = spec.ActualDelivery,
                        IsDiscrepancy = spec.IsDiscrepancy,
                        ComparisonReportSpecs = crSpecs
                    };
                    await _comparisonReportService.ComparisonReportSpecsDetailsService.AddAsync(crSpecsDetails);
                }
            }
            await LoadData();
            MessageBox.Show("Changes Saved!", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private async Task SaveDetails()
        {
            var cr = await _comparisonReportService.GetByFilterAsync(x => x.DeliveryId == _deliveries.Id);
            cr.PreparedById = (string)sluePreparedBy.EditValue;
            cr.ReviewedById = (string)slueReviewedBy.EditValue;
            cr.NotedById = (string)slueNotedBy.EditValue;
        }

        private async void btnPreview_Click(object sender, EventArgs e)
        {
            var comparisonModel = await _comparisonReportService.GetComparisonReportPrintModel(_deliveries.Id);

            var rptCR = new rptComparisonReport
            {
                DataSource = new List<ComparisonReportPrintViewModel> { comparisonModel }
            };

            rptCR.CreateDocument();
            var frm = _serviceProvider.GetRequiredService<frmReportViewer>();
            frm.InitForm(rptCR);
            frm.ShowDialog();
        }

        private async void btnRevert_Click(object sender, EventArgs e)
        {
            var cr = await _comparisonReportService.GetByFilterAsync(x => x.DeliveryId == _deliveries.Id);
            if (cr == null) return;

            var crSpecs = cr.ComparisonReportSpecs;

            foreach (var crSpec in crSpecs)
            {
                await _comparisonReportService.ComparisonReportSpecsDetailsService.DeleteRangeAsync(x => x.ComparisonReportSpecsId == crSpec.Id);
            }
            await _comparisonReportService.ComparisonReportSpecsService.DeleteRangeAsync(x => x.ComparisonReportId == cr.Id);
            await CreateComparisonReport();
            await LoadData();
        }

        private void btnAddRow_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var masterRowHandle = gridCR.FocusedRowHandle;
            var row = gridCR.GetDetailView(masterRowHandle, 0) as GridView;
            row.AddNewRow();
        }

        private async void frmComparisonReport_Load(object sender, EventArgs e)
        {
            await LoadData();
        }

        private void btnRowUp_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (IsDetail)
            {
                var masterRowHandle = gridCR.FocusedRowHandle;
                var row = gridCR.GetDetailView(masterRowHandle, 0) as GridView;

                if (row.FocusedRowHandle == 0) return;
                var sourceRowData = (ComparisonReportSpecsDetails)row.GetRow(row.FocusedRowHandle);
                var destRowData = (ComparisonReportSpecsDetails)row.GetRow(row.FocusedRowHandle - 1);

                var sourceIndex = sourceRowData.ItemOrder;
                var destIndex = destRowData.ItemOrder;

                sourceRowData.ItemOrder = destIndex;
                destRowData.ItemOrder = sourceIndex;

                row.BeginDataUpdate();
                row.ClearSorting();
                row.Columns["ItemOrder"].SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;
                row.EndDataUpdate();
            }
        }

        private void btnRowDown_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (IsDetail)
            {
                var masterRowHandle = gridCR.FocusedRowHandle;
                var row = gridCR.GetDetailView(masterRowHandle, 0) as GridView;

                if (row.FocusedRowHandle >= row.RowCount - 1) return;
                var sourceRowData = (ComparisonReportSpecsDetails)row.GetRow(row.FocusedRowHandle);
                var destRowData = (ComparisonReportSpecsDetails)row.GetRow(row.FocusedRowHandle + 1);

                var sourceIndex = sourceRowData.ItemOrder;
                var destIndex = destRowData.ItemOrder;

                sourceRowData.ItemOrder = destIndex;
                destRowData.ItemOrder = sourceIndex;

                row.BeginDataUpdate();
                row.ClearSorting();
                row.Columns["ItemOrder"].SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;
                row.EndDataUpdate();
            }
        }

        private void btnDeleteRow_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (IsDetail)
            {
                var masterRowHandle = gridCR.FocusedRowHandle;
                var rowHandle = (gcCR.FocusedView as ColumnView).FocusedRowHandle;
                var row = gridCR.GetDetailView(masterRowHandle, 0) as GridView;
                row.DeleteRow(rowHandle);
            }
        
        }

        private async void btnDeleteEquipment_Click(object sender, EventArgs e)
        {
            var row = (ComparisonReportViewModel)gridCR.GetFocusedRow();
            if(MessageBox.Show("Are you sure you want to delete this equipment?", "Alert", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;
            await _comparisonReportService.ComparisonReportSpecsService.DeleteAsync(row.CRSpecs.Id);
            await LoadData();
        }
    }
    
}