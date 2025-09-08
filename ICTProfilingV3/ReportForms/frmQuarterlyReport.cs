using Helpers.Utility;
using ICTProfilingV3.BaseClasses;
using ICTProfilingV3.Core.Common;
using ICTProfilingV3.DataTransferModels.Models;
using ICTProfilingV3.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Models.Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;

namespace ICTProfilingV3.ReportForms
{
    public partial class frmQuarterlyReport : BaseForm
    {
        private readonly IDeliveriesService _deliveriesService;
        private readonly IRepairService _repairService;
        private readonly ITechSpecsService _techSpecsService;
        private readonly ICASService _casService;
        private readonly IServiceProvider _serviceProvider;
        private readonly UserStore _userStore;

        public frmQuarterlyReport(UserStore userStore, IServiceProvider serviceProvider, IRepairService repairService, 
            ITechSpecsService techSpecsService, ICASService casService, IDeliveriesService deliveriesService)
        {
            _serviceProvider = serviceProvider;
            _userStore = userStore;
            _repairService = repairService;
            _techSpecsService = techSpecsService;
            _casService = casService;
            _deliveriesService = deliveriesService;

            InitializeComponent();
            LoadDropdowns();
        }
        private void LoadDropdowns()
        {
            lueQuarter.Properties.DataSource = Enum.GetValues(typeof(PRQuarter)).Cast<PRQuarter>().Select(x => new
            {
                Id = x,
                QuarterName = EnumHelper.GetEnumDescription(x)
            });

            lueYear.Properties.DataSource = Enum.GetValues(typeof(Year)).Cast<Year>().Select(x => new
            {
                Id = x,
                YearName = EnumHelper.GetEnumDescription(x)
            });
            lueYear.EditValue = Enum.GetValues(typeof(Year)).Cast<Year>().Max();

            lueProcess.Properties.DataSource = Enum.GetValues(typeof(RequestType)).Cast<RequestType>().Select(x => new
            {
                Id = x,
                ProcessName = EnumHelper.GetEnumDescription(x)
            });

            lueSection.Properties.DataSource = Enum.GetValues(typeof(Sections)).Cast<Sections>().Select(x => new
            {
                Id = x,
                SectionName = EnumHelper.GetEnumDescription(x)
            });
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            int year = Convert.ToInt32(EnumHelper.GetEnumDescription((Year)lueYear.EditValue));
            int quarter = Convert.ToInt32(lueQuarter.EditValue) + 1;
            PRQuarter quaterDesc = (PRQuarter)lueQuarter.EditValue;
            RequestType requestType = (RequestType)lueProcess.EditValue;
            Sections sections = (Sections)lueSection.EditValue;

            (DateTime start, DateTime end) quarterDate = GetQuarterDates(year, quarter);
            var quarterReport = SetProcess(quarterDate, requestType, quaterDesc,sections);

            var rpt = new rptQuarterlyReport
            {
                DataSource = new List<QuarterlyReport>() { quarterReport }
            };

            var reportViewer = _serviceProvider.GetRequiredService<frmReportViewer>();
            reportViewer.InitForm(rpt);
            reportViewer.ShowDialog();
        }

        private QuarterlyReport SetProcess((DateTime start, DateTime end) quarterDate, RequestType requestType, PRQuarter quaterDesc, Sections sections)
        {
            QuarterlyReport quaterReport = new QuarterlyReport();
            if (requestType == RequestType.CAS)
            {
                var cas = _casService.GetAll()
                    .Where(x => x.RecordsRequestStatus
                    .OrderByDescending(s => s.DateStatusChanged).FirstOrDefault().DateStatusChanged >= quarterDate.start && x.RecordsRequestStatus
                    .OrderByDescending(s => s.DateStatusChanged).FirstOrDefault().DateStatusChanged <= quarterDate.end && x.Status == TicketStatus.Completed)
                    .Include(x => x.EvaluationSheets)
                    .Include(x => x.AssistedBy)
                    .ToList();

                var rating = cas.Select(x => new EvaluationRating
                {
                    Rating = (decimal)(x.EvaluationSheets.Count == 0 ? 0 : x.EvaluationSheets?.Average(s => s?.RatingValue ?? 0) ?? 0),
                    Staff = x.AssistedBy.FullName,
                    Requested = 1, 
                    Items = 1,
                    Quantity = 1,
                    Male = x.Gender == Gender.Male ? 1 : 0,
                    Female = x.Gender == Gender.Female ? 1 : 0,
                    Gender = x.Gender
                }).ToList();

                quaterReport = ComputeReportTotals(rating,requestType, quaterDesc);
            }

            if (requestType == RequestType.Deliveries)
            {
                var cas = _deliveriesService.GetAll().Where(x => x.TicketRequest.TicketRequestStatus
                            .OrderByDescending(s => s.DateStatusChanged)
                            .FirstOrDefault().DateStatusChanged >= quarterDate.start && x.TicketRequest.TicketRequestStatus
                            .OrderByDescending(s => s.DateStatusChanged)
                            .FirstOrDefault().DateStatusChanged <= quarterDate.end && x.TicketRequest.TicketStatus == TicketStatus.Completed
                            && x.TicketRequest.StaffId != null && x.TicketRequest.ITStaff.Section == sections)
                            .Include(x => x.EvaluationSheets)
                            .Include(x => x.TicketRequest)
                            .Include(x => x.TicketRequest.ITStaff)
                            .Include(x => x.TicketRequest.ITStaff.Users)
                            .Include(x => x.TicketRequest.TicketRequestStatus)
                            .Include(x => x.DeliveriesSpecs)
                            .ToList();

                var rating = cas.Select(x => new EvaluationRating
                {
                    Rating = (decimal)(x.EvaluationSheets?.Average(s => s?.RatingValue ?? 0) ?? 0),
                    Staff = x.TicketRequest.ITStaff.Users.FullName,
                    Requested = 1,
                    Items = x.DeliveriesSpecs.Count,
                    Quantity = x.DeliveriesSpecs.Sum(s => s.Quantity) ?? 0,
                    Male = x.Gender == Gender.Male ? 1 : 0,
                    Female = x.Gender == Gender.Female ? 1 : 0,
                    Gender = x.Gender
                }).ToList();

                quaterReport = ComputeReportTotals(rating, requestType, quaterDesc);
            }

            if (requestType == RequestType.TechSpecs)
            {
                var cas = _techSpecsService.GetAll().Where(x => x.TicketRequest.TicketRequestStatus
                            .OrderByDescending(s => s.DateStatusChanged)
                            .FirstOrDefault().DateStatusChanged >= quarterDate.start && x.TicketRequest.TicketRequestStatus
                            .OrderByDescending(s => s.DateStatusChanged)
                            .FirstOrDefault().DateStatusChanged <= quarterDate.end && x.TicketRequest.TicketStatus == TicketStatus.Completed
                            && x.TicketRequest.StaffId != null && x.TicketRequest.ITStaff.Section == sections)
                            .Include(x => x.EvaluationSheets)
                            .Include(x => x.TicketRequest)
                            .Include(x => x.TicketRequest.ITStaff)
                            .Include(x => x.TicketRequest.ITStaff.Users)
                            .Include(x => x.TicketRequest.TicketRequestStatus).ToList();

                var rating = cas.Select(x => new EvaluationRating
                {
                    Rating = (decimal)(x.EvaluationSheets?.Average(s => s?.RatingValue ?? 0) ?? 0),
                    Staff = x.TicketRequest.ITStaff.Users.FullName,
                    Requested = 1,
                    Items = x.TechSpecsICTSpecs.Count,
                    Quantity = x.TechSpecsICTSpecs.Sum(s => s.Quantity) ?? 0,
                    Male = x.ReqByGender == Gender.Male ? 1 : 0,
                    Female = x.ReqByGender == Gender.Female ? 1 : 0,
                    Gender = x.ReqByGender
                }).ToList();

                quaterReport = ComputeReportTotals(rating, requestType, quaterDesc);
            }

            if (requestType == RequestType.Repairs)
            {
                var repair = _repairService.GetAll().Where(x => x.TicketRequest.TicketRequestStatus
                            .OrderByDescending(s => s.DateStatusChanged)
                            .FirstOrDefault().DateStatusChanged >= quarterDate.start && x.TicketRequest.TicketRequestStatus
                            .OrderByDescending(s => s.DateStatusChanged)
                            .FirstOrDefault().DateStatusChanged <= quarterDate.end && x.TicketRequest.TicketStatus == TicketStatus.Completed
                            && x.TicketRequest.StaffId != null && x.TicketRequest.ITStaff.Section == sections)
                            .Include(x => x.EvaluationSheets)
                            .Include(x => x.TicketRequest)
                            .Include(x => x.TicketRequest.ITStaff)
                            .Include(x => x.TicketRequest.ITStaff.Users)
                            .Include(x => x.TicketRequest.TicketRequestStatus)
                            .ToList();

                var rating = repair.Select(x => new EvaluationRating
                {
                    Rating = (decimal)(x.EvaluationSheets.Count() == 0 ? 0 : x.EvaluationSheets?.Average(s => s?.RatingValue ?? 0) ?? 0),
                    Staff = x.TicketRequest.ITStaff.Users.FullName,
                    Requested = 1,
                    Items = 1,
                    Quantity = 1,
                    Male = x.Gender == Gender.Male ? 1 : 0,
                    Female = x.Gender == Gender.Female ? 1 : 0,
                    Gender = x.Gender
                }).ToList();

                quaterReport = ComputeReportTotals(rating, requestType, quaterDesc);
            }
            return quaterReport;
        }

        private QuarterlyReport ComputeReportTotals(IEnumerable<EvaluationRating> rating,RequestType requestType, PRQuarter quaterDesc)
        {
            QuarterlyReport quaterReport = new QuarterlyReport();
            var ratingByUser = rating.GroupBy(x => x.Staff).Select(s => new EvaluationRating
            {
                Staff = StringHelper.GetInitials(s.Key),
                Requested = s.Count(),
                Items = s.Count(),
                Quantity = s.Sum(o => o.Quantity),
                Rating = s.Average(o => o.Rating),
                Male = s.Sum(o => o.Male),
                Female = s.Sum(o => o.Female)
            }).ToList();

            var ratingByGender = rating.GroupBy(x => x.Gender).Select(s => new EvaluationRating
            {
                Gender = s.Key,
                Requested = s.Count(),
                Items = s.Count()

            }).ToList();

            var femaleRating = ratingByGender.FirstOrDefault(x => x.Gender == Gender.Female);
            var maleRating = ratingByGender.FirstOrDefault(x => x.Gender == Gender.Male);

            quaterReport = new QuarterlyReport
            {
                Process = EnumHelper.GetEnumDescription(requestType),
                Quarter = EnumHelper.GetEnumDescription(quaterDesc),
                PrintedBy = _userStore.Username,
                DatePrinted = DateTime.Now.ToShortDateString(),
                EvaluationRating = ratingByUser,
                RequestedByFemale = femaleRating == null ? 0 : femaleRating.Requested,
                RequestedByMale = maleRating == null ? 0 : maleRating.Requested,
                ItemsByFemale = femaleRating == null ? 0 : femaleRating.Items,
                ItemsByMale = maleRating == null ? 0 : maleRating.Items,
            };

            return quaterReport;
        }
        private (DateTime, DateTime) GetQuarterDates(int year, int quarter)
        {
            switch (quarter)
            {
                case 1: return (new DateTime(year, 1, 1), new DateTime(year, 3, 31,23,0,0));
                case 2: return (new DateTime(year, 4, 1), new DateTime(year, 6, 30,23,0,0));
                case 3: return (new DateTime(year, 7, 1), new DateTime(year, 9, 30,23,0,0));
                case 4: return (new DateTime(year, 10, 1), new DateTime(year, 12, 30, 23, 0, 0));
                default: throw new ArgumentException("Quarter must be between 1 and 4");
            }
        }
    }
}