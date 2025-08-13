using DevExpress.Charts.Native;
using DevExpress.XtraCharts;
using DevExpress.XtraCharts.Native;

using Helpers.Interfaces;
using ICTProfilingV3.DashboardForms.TicketToolForms;
using ICTProfilingV3.DataTransferModels.Models;
using ICTProfilingV3.Interfaces;
using ICTProfilingV3.Services.ApiUsers;
using ICTProfilingV3.Services.Employees;
using ICTProfilingV3.UsersForms;
using Models.Entities;
using Models.Enums;
using Models.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace ICTProfilingV3.DashboardForms
{
    public partial class UCRequestDashboard : DevExpress.XtraEditors.XtraUserControl
    {
        private readonly IICTUserManager userManager;
        private IUnitOfWork unitOfWork;

        private Expression<Func<TicketRequest, bool>> dateFilter;
        private Expression<Func<TicketRequest, bool>> statusFilter;
        private Expression<Func<TicketRequest, bool>> staffFilter;

        public UCRequestDashboard()
        {
            InitializeComponent();
            userManager = new ICTUserManager();
            unitOfWork = new UnitOfWork();
            LoadDropdowns();
        }

        private void LoadData()
        {
            var data = unitOfWork.TicketRequestRepo.GetAll(x => x.TechSpecs, x => x.TechSpecs.TechSpecsICTSpecs,
                x => x.Repairs, x => x.Repairs.PPEs, x => x.Repairs.PPEs.PPEsSpecs,
                x => x.Deliveries, x => x.Deliveries.DeliveriesSpecs);

            if(dateFilter != null) data = data.Where(dateFilter);
            if (statusFilter != null) data = data.Where(statusFilter);
            if(staffFilter != null) data = data.Where(staffFilter);

            var RequestData = data.ToList().Select(x => new RequestCount
            {
                TicketStatus = x.TicketStatus,
                RequestType = x.RequestType,
                Office = GetOffice(x),
                Request = 1,
                Quantity = GetQuantity(x),
                Item = GetItem(x),
                Female = GetGender(x) == Gender.Female ? 1 : 0,
                Male = GetGender(x) == Gender.Male ? 1 : 0
            });

            lblCountOfRequest.Text = RequestData.Sum(s => s.Request).ToString();
            lblCountOfQuantity.Text = RequestData.Sum(s => s.Quantity).ToString();
            lblCountOfItem.Text = RequestData.Sum(s => s.Item).ToString();

            RefreshChart(RequestData);

        }

        private Gender GetGender(TicketRequest ticket)
        {
            if (ticket.RequestType == RequestType.TechSpecs) return ticket.TechSpecs.ReqByGender;
            if (ticket.RequestType == RequestType.Repairs) return ticket.Repairs.Gender;
            if (ticket.RequestType == RequestType.Deliveries) return ticket.Deliveries.Gender;
            return 0;
        }

        private int GetItem(TicketRequest ticket)
        {
            if (ticket.RequestType == RequestType.TechSpecs) return ticket.TechSpecs.TechSpecsICTSpecs.Count;
            if (ticket.RequestType == RequestType.Repairs) return ticket.Repairs.PPEs.PPEsSpecs.Count;
            if (ticket.RequestType == RequestType.Deliveries) return ticket.Deliveries.DeliveriesSpecs.Count;
            return 0;
        }

        private int GetQuantity(TicketRequest ticket)
        {
            if (ticket.RequestType == RequestType.TechSpecs) return ticket.TechSpecs.TechSpecsICTSpecs.Sum(x => x.Quantity ?? 0);
            if (ticket.RequestType == RequestType.Repairs) return ticket.Repairs.PPEs.PPEsSpecs.Sum(x => x.Quantity);
            if (ticket.RequestType == RequestType.Deliveries) return (int)ticket.Deliveries.DeliveriesSpecs.Sum(x => x.Quantity);
            return 0;
        }
        private string GetOffice(TicketRequest ticket)
        {
            if (ticket.RequestType == RequestType.TechSpecs) return HRMISEmployees.GetEmployeeById(ticket.TechSpecs.ReqById).Office;
            if (ticket.RequestType == RequestType.Repairs) return HRMISEmployees.GetEmployeeById(ticket.Repairs.RequestedById).Office;
            if (ticket.RequestType == RequestType.Deliveries) return HRMISEmployees.GetEmployeeById(ticket.Deliveries.RequestedById).Office;
            return null;
        }

        private void RefreshChart(IEnumerable<RequestCount> dataSource)
        {
            int counter = 0;
            var res = dataSource.GroupBy(x => new
            {
                key1 = x.Office,
                key2 = x.RequestType
            })
            .Select(s => new RequestCount
            {
                Office = s.Key.key1,
                Request = s.Sum(x => x.Request),
                RequestType = s.Key.key2,
                Quantity = s.Sum(x => x.Quantity),
                Item = s.Sum(x => x.Item),
                Index = ++counter % 5 == 0 ? 5 : counter % 5
            }).ToList();

            chartReqByOffice.DataSource = res;
            chartReqByOffice.SeriesTemplate.ChangeView(ViewType.StackedBar); 
            chartReqByOffice.SeriesTemplate.SeriesDataMember = "RequestType";
            chartReqByOffice.SeriesTemplate.ArgumentDataMember = "Office";
            chartReqByOffice.SeriesTemplate.ValueDataMembers.AddRange("Request");
            chartReqByOffice.SeriesTemplate.Label.TextPattern = "{S}: {V:0}";
            chartReqByOffice.SeriesTemplate.Label.ResolveOverlappingMode = ResolveOverlappingMode.Default;

            StackedBarTotalLabel totalLabel = ((XYDiagram)chartReqByOffice.Diagram).DefaultPane.StackedBarTotalLabel;
            totalLabel.Visible = true;
            totalLabel.ShowConnector = true;
            totalLabel.TextPattern = "Total:\n{TV:F2}";
            //chartReqByOffice.SeriesTemplate.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;

            var deliveries = res.Where(w => w.RequestType == RequestType.Deliveries);
            var ts = res.Where(w => w.RequestType == RequestType.TechSpecs);
            var repairs = res.Where(w => w.RequestType == RequestType.Repairs);

            lblRequestDeliveries.Text = deliveries.Sum(s => s.Request).ToString();
            lblRequestTechSpecs.Text = ts.Sum(s => s.Request).ToString();
            lblRequestRepairs.Text = repairs.Sum(s => s.Request).ToString();

            lblItemDel.Text = deliveries.Sum(s => s.Item).ToString();
            lblItemTS.Text = ts.Sum(s => s.Item).ToString();
            lblItemRepairs.Text = repairs.Sum(s => s.Item).ToString();

            lblQuantityDel.Text = deliveries.Sum(s => s.Quantity).ToString();
            lblQuantityTS.Text = ts.Sum(s => s.Quantity).ToString();
            lblQuantityRepair.Text = repairs.Sum(s => s.Quantity).ToString();

            ChartItemByOffice.DataSource = res;
            ChartItemByOffice.SeriesTemplate.ChangeView(ViewType.StackedBar);
            ChartItemByOffice.SeriesTemplate.SeriesDataMember = "RequestType";
            ChartItemByOffice.SeriesTemplate.ArgumentDataMember = "Office";
            ChartItemByOffice.SeriesTemplate.ValueDataMembers.AddRange("Item");
            ChartItemByOffice.SeriesTemplate.Label.TextPattern = "{S}: {V:0}";
            ChartItemByOffice.SeriesTemplate.Label.ResolveOverlappingMode = ResolveOverlappingMode.Default;

            StackedBarTotalLabel totalLabelItem = ((XYDiagram)ChartItemByOffice.Diagram).DefaultPane.StackedBarTotalLabel;
            totalLabelItem.Visible = true;
            totalLabelItem.ShowConnector = true;
            totalLabelItem.TextPattern = "Total:\n{TV:F2}";
            //ChartItemByOffice.SeriesTemplate.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;

            ChartQuantityByOffice.DataSource = res;
            ChartQuantityByOffice.SeriesTemplate.ChangeView(ViewType.StackedBar);
            ChartQuantityByOffice.SeriesTemplate.SeriesDataMember = "RequestType";
            ChartQuantityByOffice.SeriesTemplate.ArgumentDataMember = "Office";
            ChartQuantityByOffice.SeriesTemplate.ValueDataMembers.AddRange("Quantity");
            ChartQuantityByOffice.SeriesTemplate.Label.TextPattern = "{S}: {V:0}";
            ChartQuantityByOffice.SeriesTemplate.Label.ResolveOverlappingMode = ResolveOverlappingMode.Default;

            StackedBarTotalLabel totalLabelOffice = ((XYDiagram)ChartQuantityByOffice.Diagram).DefaultPane.StackedBarTotalLabel;
            totalLabelOffice.Visible = true;
            totalLabelOffice.ShowConnector = true;
            totalLabelOffice.TextPattern = "Total:\n{TV:F2}";
            //ChartQuantityByOffice.SeriesTemplate.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;

            RefreshGenderChart(dataSource);
            RefreshChartActedBy(dataSource);
        } 

        private void RefreshGenderChart(IEnumerable<RequestCount> dataSource)
        {
            var series1 = chartReqByGender.Series["Gender"];
            series1.Label.TextPattern = "{A}\nTotal:{V}\nPercentage:{VP:0.00%}";
            series1.LegendTextPattern = "{A}";
            series1.Points.Clear();
            series1.Points.Add(new SeriesPoint("Male", dataSource.Sum(x => x.Male)));
            series1.Points.Add(new SeriesPoint("Female", dataSource.Sum(x => x.Female)));
        }
        private void RefreshChartActedBy(IEnumerable<RequestCount> dataSource)
        {
            var res = dataSource.GroupBy(s => s.TicketStatus).Select(s => new RequestCount
            {
                TicketStatus = s.Key,
                Request = s.Sum(x => x.Request)
            });
            chartReqActed.DataSource = res.OrderByDescending(o => o.TicketStatus);
            chartReqActed.SeriesTemplate.ChangeView(ViewType.Bar);
            chartReqActed.SeriesDataMember = "TicketStatus";
            chartReqActed.SeriesTemplate.ArgumentDataMember = "TicketStatus";
            chartReqActed.SeriesTemplate.ValueDataMembers.AddRange("Request");
            chartReqActed.SeriesTemplate.Label.TextPattern = "{A}: {V:0}";
            chartReqActed.SeriesTemplate.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
            ((DevExpress.XtraCharts.XYDiagram)chartReqActed.Diagram).Rotated = true;
        }

        private void LoadDropdowns()
        {
            var staff = unitOfWork.ITStaffRepo.GetAll(x => x.Users).ToList();
            staff.Add(new ITStaff() { Users = new Users() { FullName = "#Unassigned", Position = "#Unassigned" } });
            var res = staff.OrderBy(o => o.Users.FullName);
            slueTaskOf.Properties.DataSource = res.ToList();

            lueStatus.Properties.DataSource = Enum.GetValues(typeof(TicketStatus)).Cast<TicketStatus>().Select(x => new
            {
                Id = x,
                Value = Models.Enums.EnumHelper.GetEnumDescription(x)
            });
        }

        private void UCRequestDashboard_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        private void slueTaskOf_EditValueChanged(object sender, EventArgs e)
        {
            int? Id = (int?)slueTaskOf.EditValue;
            if (Id == 0) staffFilter = x => x.StaffId == null;
            else staffFilter = x => x.StaffId == Id;

            if (Id == null) staffFilter = null;
            LoadData();
        }

        private void btnFilterbyDate_Click(object sender, EventArgs e)
        {
            var dateFrom = deFrom?.DateTime;
            var dateTo = deTo?.DateTime;

            if (dateFrom == null || dateTo == null) dateFilter = null;
            dateFilter = x => x.DateCreated > dateFrom && x.DateCreated < dateTo;
            LoadData();
        }

        private void lueStatus_EditValueChanged(object sender, EventArgs e)
        {
            TicketStatus? ticketStatus = (TicketStatus?)lueStatus.EditValue;
            statusFilter = x => x.TicketStatus == ticketStatus;
            if (ticketStatus == null) statusFilter = null;
            LoadData();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            dateFilter = null;
            staffFilter = null;
            statusFilter = null;

            lueStatus.EditValue = null;
            slueTaskOf.EditValue = null;
            deFrom.EditValue = null;
            deTo.EditValue = null;

            LoadData();
        }

        private void chartReqActed_ObjectSelected(object sender, HotTrackEventArgs e)
        {
            if (e.HitInfo.Series == null) return;
            var res = (TicketStatus)Enum.Parse(typeof(TicketStatus), e.HitInfo.Series.ToString(), true);

            var frm = new frmShowTickets(res);
            frm.ShowDialog();
        }
    }
}
