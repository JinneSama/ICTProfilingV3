using Microsoft.AspNet.Identity.EntityFramework;
using Models.Enums;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entities
{
    public class Users : IdentityUser
    {
        public Users()
        {
            ITStaffs = new HashSet<ITStaff>();
            Actions = new HashSet<Actions>();
            CreatedActions = new HashSet<Actions>();
            TechSpecsPreaparedBy = new HashSet<TechSpecs>();
            TechSpecsReviewedBy = new HashSet<TechSpecs>();
            TechSpecsNotedBy = new HashSet<TechSpecs>();
            CustomerActionSheet = new HashSet<CustomerActionSheet>();
            RepairsPreparedBy = new HashSet<Repairs>();
            RepairsReviewedBy = new HashSet<Repairs>();
            RepairsNotedBy = new HashSet<Repairs>();
            PurchaseRequests = new HashSet<PurchaseRequest>();
            TicketRequestStatus = new HashSet<TicketRequestStatus>();
            PGNRequests = new HashSet<PGNRequests>();
            CRPreparedBy = new HashSet<ComparisonReport>();
            CRReviewedBy = new HashSet<ComparisonReport>();
            CRNotedBy = new HashSet<ComparisonReport>();
            ChangeLogsBy = new HashSet<ChangeLogs>();
            MOAccounts = new HashSet<MOAccounts>();
            MOAccountUsers = new HashSet<MOAccountUsers>();
            RecordsRequestStatus = new HashSet<RecordsRequestStatus>();
            EvaluationSheets = new HashSet<EvaluationSheet>();
            LogEntry = new HashSet<LogEntry>();
        }
        public string FullName { get; set; }
        public string Position { get; set; }
        public string OFMISUsername { get; set; }
        public bool? IsDeleted { get; set; }
        [JsonIgnore]
        public virtual ICollection<ITStaff> ITStaffs { get; set; }
        [JsonIgnore]
        [InverseProperty("CreatedBy")]
        public ICollection<Actions> CreatedActions { get; set; }
        [JsonIgnore]
        public ICollection<Actions> Actions { get; set; }
        [JsonIgnore]
        public ICollection<TechSpecs> TechSpecsPreaparedBy { get; set; }
        [JsonIgnore]
        public ICollection<TechSpecs> TechSpecsReviewedBy { get; set; }
        [JsonIgnore]
        public ICollection<TechSpecs> TechSpecsNotedBy { get; set; }
        [JsonIgnore]
        public ICollection<CustomerActionSheet> CustomerActionSheet { get; set; }
        [JsonIgnore]
        public ICollection<Repairs> RepairsPreparedBy { get; set; }
        [JsonIgnore]
        public ICollection<Repairs> RepairsReviewedBy { get; set; }
        [JsonIgnore]
        public ICollection<Repairs> RepairsNotedBy { get; set; }
        [JsonIgnore]
        public ICollection<PurchaseRequest> PurchaseRequests { get; set; }
        [JsonIgnore]
        public ICollection<TicketRequestStatus> TicketRequestStatus { get; set; }
        [JsonIgnore]
        public ICollection<PGNRequests> PGNRequests { get; set; }
        [JsonIgnore]
        public ICollection<ComparisonReport> CRPreparedBy { get; set; }
        [JsonIgnore]
        public ICollection<ComparisonReport> CRReviewedBy { get; set; }
        [JsonIgnore]
        public ICollection<ComparisonReport> CRNotedBy { get; set; }
        [JsonIgnore]
        public ICollection<ChangeLogs> ChangeLogsBy { get; set; }
        [JsonIgnore]
        public ICollection<MOAccountUsers> MOAccountUsers { get; set; }
        [JsonIgnore]
        public ICollection<MOAccounts> MOAccounts { get; set; }
        [JsonIgnore]
        public ICollection<RecordsRequestStatus> RecordsRequestStatus { get; set; }
        [JsonIgnore]
        public virtual ICollection<EvaluationSheet> EvaluationSheets { get; set; }
        [JsonIgnore]
        public virtual ICollection<LogEntry> LogEntry { get; set; }
    }
}
