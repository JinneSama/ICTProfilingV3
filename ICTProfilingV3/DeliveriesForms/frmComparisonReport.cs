﻿using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraGrid.Views.Grid;
using EntityManager.Managers.User;
using Models.Entities;
using Models.Repository;
using Models.ViewModels;
using System;
using System.Linq;
using System.Threading.Tasks;
using DevExpress.XtraGrid.Views.Base;
using System.ComponentModel;
using System.Windows.Forms;
using Models.HRMISEntites;
using ICTProfilingV3.ReportForms;
using System.Collections.Generic;
using Models.ReportViewModel;
using Models.Managers.User;

namespace ICTProfilingV3.DeliveriesForms
{
    public partial class frmComparisonReport : DevExpress.XtraEditors.XtraForm
    {
        private readonly Deliveries deliveries;
        private IICTUserManager userManager;
        private IUnitOfWork unitOfWork;

        private bool IsDetail = false;
        public frmComparisonReport(Deliveries deliveries)
        {
            InitializeComponent();
            userManager = new ICTUserManager();
            unitOfWork = new UnitOfWork();
            LoadDropdowns();
            this.deliveries = deliveries;
            LoadData();
        }

        private void ExpandAll()
        {
            for (var i = 0; i < gridCR.RowCount; i++)
            {
                var focusedRow = gridCR.GetRowHandle(i);
                gridCR.SetMasterRowExpanded(focusedRow, !gridCR.GetMasterRowExpanded(focusedRow));
            }
        }

        private async void LoadData()
        {
            lblEpisNo.Text = deliveries.Id.ToString();

            var employee = HRMISEmployees.GetEmployeeById(deliveries.RequestedById);
            txtDateOfDelivery.Text = deliveries.DeliveredDate.ToString();
            txtRequestingOffice.Text = employee.Office + " " + employee.Division;
            txtSupplier.Text = deliveries.Supplier.SupplierName;
            spinAmount.Value = deliveries.DeliveriesSpecs.Sum(x => x.UnitCost);
            txtInspectedDate.Text = deliveries.Actions?.FirstOrDefault()?.ActionDate?.ToString() ?? "";

            if (deliveries.ComparisonReports.FirstOrDefault() == null) CreateComparisonReport();
            else await LoadDetails();
            ExpandAll();
        }

        private async Task LoadDetails()
        {
            var cr = await unitOfWork.ComparisonReportRepo.FindAsync(x => x.DeliveryId == deliveries.Id,
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

        private async void CreateComparisonReport()
        {
            var cr = new ComparisonReport { DeliveryId = deliveries.Id };
            unitOfWork.ComparisonReportRepo.Insert(cr);
            foreach(var item in deliveries.DeliveriesSpecs)
            {
                var crSpecs = new ComparisonReportSpecs
                {
                    ComparisonReportId = cr.Id,
                    ItemNo = item.ItemNo,
                    Quantity = item.Quantity,
                    Unit = item.Unit,
                    Type = item.Model.Brand.EquipmentSpecs.Equipment.EquipmentName,
                    ActualDelivery = item.Model.Brand.BrandName + " " + item.Model.ModelName
                };
                unitOfWork.ComparisonReportSpecsRepo.Insert(crSpecs);
                foreach(var itemDetails in item.DeliveriesSpecsDetails)
                {
                    var crSpecsDetails = new ComparisonReportSpecsDetails
                    {
                        ComparisonReportSpecsId = item.Id,
                        Type = itemDetails.Specs,
                        ActualDelivery = itemDetails.Description,
                        ItemOrder = itemDetails.ItemNo
                    };
                    unitOfWork.ComparisonReportSpecsDetailsRepo.Insert(crSpecsDetails);
                }
            }
            unitOfWork.Save();
            await LoadDetails();
        }

        private void LoadDropdowns()
        {
            var users = userManager.GetUsers().ToList();
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
            await SaveDetails();
            for (int i = 0; i < gridCR.RowCount; i++)
            {
                var row = (ComparisonReportViewModel)gridCR.GetRow(i);
                unitOfWork.ComparisonReportSpecsDetailsRepo.DeleteRange(x => x.ComparisonReportSpecsId == row.CRSpecs.Id);
                unitOfWork.ComparisonReportSpecsRepo.DeleteRange(x => x.ComparisonReportId == row.CRId);
                unitOfWork.Save();
            }

            for (int i = 0; i < gridCR.RowCount; i++)
            {
                var row = (ComparisonReportViewModel)gridCR.GetRow(i);
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
                unitOfWork.ComparisonReportSpecsRepo.Insert(crSpecs);

                for (int x = 0; x < detailRow.RowCount; x++)
                {
                    var spec = (ComparisonReportSpecsDetails)detailRow.GetRow(x);
                    var crSpecsDetails = new ComparisonReportSpecsDetails
                    {
                        ItemOrder = x + 1,
                        Type = spec.Type,
                        PR = spec.PR,
                        Quotation = spec.Quotation,
                        PO = spec.PO,
                        ActualDelivery = spec.ActualDelivery,
                        IsDiscrepancy = spec.IsDiscrepancy,
                        ComparisonReportSpecsId = crSpecs.Id
                    };
                    unitOfWork.ComparisonReportSpecsDetailsRepo.Insert(crSpecsDetails);
                }
            }
            unitOfWork.Save();
            LoadData();
            MessageBox.Show("Changes Saved!", "Alert", MessageBoxButtons.OK , MessageBoxIcon.Information);
        }

        private async Task SaveDetails()
        {
            var cr = await unitOfWork.ComparisonReportRepo.FindAsync(x => x.DeliveryId == deliveries.Id);
            cr.PreparedById = (string)sluePreparedBy.EditValue;
            cr.ReviewedById = (string)slueReviewedBy.EditValue;
            cr.NotedById = (string)slueNotedBy.EditValue;
            unitOfWork.Save();
        }

        private async void btnPreview_Click(object sender, EventArgs e)
        {
            var cr = await unitOfWork.ComparisonReportRepo.FindAsync(x => x.DeliveryId == deliveries.Id,
                x => x.PreparedByUser,
                x => x.NotedByUser,
                x => x.ReviewedByUser);

            var employee = HRMISEmployees.GetEmployeeById(deliveries.RequestedById);

            var comparisonModel = new ComparisonReportPrintViewModel
            {
                DateOfDelivery = deliveries.DeliveredDate,
                RequestingOffice = employee.Office + " " + employee.Division,
                Supplier = deliveries.Supplier.SupplierName,
                Amount = (double)deliveries.DeliveriesSpecs.Sum(x => x.UnitCost),
                EpisNo = "EPiS-" + deliveries.TicketRequest.Id.ToString(),
                TechInspectedDate = deliveries.Actions?.FirstOrDefault()?.ActionDate,
                ComparisonReportSpecs = cr.ComparisonReportSpecs,
                PreparedBy = cr.PreparedByUser,
                ReviewedBy = cr.ReviewedByUser,
                NotedBy = cr.NotedByUser,
                DatePrinted = DateTime.UtcNow,
                PrintedBy = UserStore.Username
            };

            var rptCR = new rptComparisonReport
            {
                DataSource = new List<ComparisonReportPrintViewModel> { comparisonModel }
            };

            rptCR.CreateDocument();
            var frm = new frmReportViewer(rptCR);
            frm.ShowDialog();
        }
    }
    
}