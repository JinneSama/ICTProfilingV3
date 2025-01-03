using DevExpress.Data.ODataLinq.Helpers;
using DevExpress.XtraCharts;
using DevExpress.XtraRichEdit.API.Native;
using Models.Entities;
using Models.HRMISEntites;
using Models.Models;
using Models.Repository;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace ICTProfilingV3.DashboardForms
{
    public partial class UCRepairDashboard : DevExpress.XtraEditors.XtraUserControl
    {
        private readonly IUnitOfWork unitOfWork;
        public UCRepairDashboard()
        {
            InitializeComponent();
            unitOfWork = new UnitOfWork();
            LoadData(null);
        }

        private void LoadData(Expression<Func<Repairs,bool>> expression)
        {
            var repairData = unitOfWork.RepairsRepo.GetAll(x => x.PPEs,
                x => x.PPEs.PPEsSpecs,
                x => x.PPEs.PPEsSpecs.Select(s => s.Model),
                x => x.PPEs.PPEsSpecs.Select(s => s.Model.Brand));

            if(expression != null) repairData = repairData.Where(expression);

            LoadRequestbyOfficeData(repairData);
            LoadBrandData(repairData);
            LoadItemByStatusData(repairData);
        }

        private void LoadRequestbyOfficeData(IQueryable<Repairs> repairData)
        {
            var res = repairData.ToList().Select(s => new BrandCount
            {
                Brand = s.PPEs.PPEsSpecs.Select(x => x.Model.Brand.BrandName).FirstOrDefault(),
                Office = HRMISEmployees.GetEmployeeById(s.RequestedById).Office,
                Quantity = s.PPEs.Quantity
            });
            chartRepairbyOffice.DataSource = res;
            chartRepairbyOffice.SeriesTemplate.ChangeView(ViewType.Bar);
            chartRepairbyOffice.SeriesDataMember = "Office";
            chartRepairbyOffice.SeriesTemplate.ArgumentDataMember = "Office";
            chartRepairbyOffice.SeriesTemplate.ValueDataMembers.AddRange("Quantity");
            chartRepairbyOffice.SeriesTemplate.Label.TextPattern = "{A}: {V:0}";
            chartRepairbyOffice.SeriesTemplate.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
            ((DevExpress.XtraCharts.XYDiagram)chartRepairbyOffice.Diagram).Rotated = true;
        }

        private void LoadBrandData(IQueryable<Repairs> repairData)
        {
            var res = repairData.ToList().Select(s => new BrandCount
            {
                Brand = s.PPEs.PPEsSpecs.Select(x => x.Model.Brand.BrandName).FirstOrDefault(),
                Office = HRMISEmployees.GetEmployeeById(s.RequestedById).Office,
                Quantity = s.PPEs.Quantity
            });

            var resGrouped = res.GroupBy(x => x.Brand).Select(x => new
            {
                Brand = x.Key ?? "No Brand",
                Quantity = x.Sum(s => s.Quantity)
            }).ToList();

            var series1 = chartCountByBrand.Series["Brand"];
            series1.Label.TextPattern = "{A}\nTotal:{V}\nPercentage:{VP:0.00%}";
            series1.LegendTextPattern = "{A}";
            series1.Points.Clear();
            foreach(var item in resGrouped)
            {
                if(item.Brand == null) continue;
                series1.Points.Add(new SeriesPoint(item.Brand, item.Quantity));
            }
            gcBrand.DataSource = resGrouped;
        }

        private void LoadItemByStatusData(IQueryable<Repairs> repairData)
        {
            var resGrouped = repairData.GroupBy(x => x.PPEs.Status).Select(x => new
            {
                Status = x.Key,
                Quantity = x.Sum(s => s.PPEs.Quantity)
            }).ToList(); 
            
            var series1 = chartByStatus.Series["Status"];
            series1.Label.TextPattern = "{A}\nTotal:{V}\nPercentage:{VP:0.00%}";
            series1.LegendTextPattern = "{A}";
            series1.Points.Clear();
            foreach (var item in resGrouped)
            {
                if (item.Status == null) continue;
                series1.Points.Add(new SeriesPoint(item.Status, item.Quantity));
            }

            gcStatus.DataSource = repairData.ToList().Select(s => new RepairStatusCount
            {
                DateCreated = s.DateCreated,
                RepairNo = "EPiS-" + s.Id,
                Brand = s.PPEs.PPEsSpecs.Select(x => x.Model.Brand.BrandName).FirstOrDefault() ?? "No Brand",
                Status = (Models.Enums.PPEStatus)s.PPEs.Status
            }).ToList();
        }
    }
}
