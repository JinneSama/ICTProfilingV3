using ICTMigration.ICTv2Models;
using Models.Entities;
using Models.Enums;
using Models.HRMISEntites;
using Models.OFMISEntities;
using Models.Repository;
using System;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Runtime;
using System.Threading.Tasks;

namespace ICTMigration.PPEMigration
{
    public class MigratePPE
    {
        private OleDbConnection access = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0; Data Source = C:\Users\Scott\Desktop\PPE_BE.mdb;");
        private readonly IUnitOfWork unitOfWork;
        public MigratePPE()
        {
            unitOfWork = new UnitOfWork();
        }
        public async Task MigratePPEs()
        {
            access.Open();
            string query = "SELECT tblProperties.fldPropertyNo AS PropertyNo, tblOffices.fldOfficeID AS Office, " +
                        "tblEmployees.fldFirstname & ' ' & tblEmployees.fldMIddleName & ' ' & tblEmployees.fldLastname AS Fullname, tblEmployees.fldFirstname AS FirstName, tblEmployees.fldMIddleName, " +
                        "tblEmployees.fldLastname, tblItemsRecDetails.fldUnitCost AS UnitCost, tblItemsRecDetails.fldQty AS Quantity, tblItemsRecDetails.fldUnit AS Unit, " +
                        "tblItemsRecDetails.fldDescription AS Specs, tblItemsReceived.fldInvoiceDate AS InvoiceDate, tblArticles.fldArticleName AS Article, " +
                        "tblArticles.fldDescription AS Description, tblClass.fldClassification, tblType.fldType, tblAccounts.*, tblStatus.fldStatus " +
                        "FROM tblStatus INNER JOIN (tblOffices INNER JOIN (tblItemsReceived INNER JOIN ((tblClass INNER JOIN ((tblAccounts " +
                        "INNER JOIN (tblArticles INNER JOIN tblType ON tblArticles.fldTypeID = tblType.fldTypeID) ON tblAccounts.fldAccountCode = tblArticles.fldAccountCode) " +
                        "INNER JOIN tblItemsRecDetails ON tblArticles.fldArticleCode = tblItemsRecDetails.fldArticleCode) ON tblClass.fldClassCode = tblAccounts.fldClassCode) " +
                        "INNER JOIN (tblProperties INNER JOIN tblEmployees ON tblProperties.fldEmpID = tblEmployees.fldEmpID) ON tblItemsRecDetails.fldDRDNo = tblProperties.fldDRDNo) " +
                        "ON tblItemsReceived.fldIRTransNo = tblItemsRecDetails.fldIRTransNo) ON tblOffices.fldOfficeID = tblEmployees.fldOfficeID) ON tblStatus.fldSID = tblProperties.fldSID " +
                        "WHERE(((tblAccounts.fldAccountTitle) = 'IT Equipment and Software')) " +
                        "ORDER BY tblProperties.fldPropertyNo ";

            var cmd = new OleDbCommand(query, access);
            var da = new OleDbDataAdapter(cmd);
            var dt = new DataTable();
            da.Fill(dt);

            var result = dt.AsEnumerable().Select(row => new PPEItem
            {
                PropertyNo = row["PropertyNo"].ToString(),
                Office = row["Office"].ToString(),
                Fullname = row["Fullname"].ToString(),
                FirstName = row["FirstName"].ToString(),
                MiddleName = row["fldMIddleName"].ToString(),
                LastName = row["fldLastname"].ToString(),
                UnitCost = Convert.ToDecimal(row["UnitCost"]),
                Quantity = Convert.ToInt32(row["Quantity"]),
                Unit = row["Unit"].ToString(),
                Specs = row["Specs"].ToString(),
                InvoiceDate = (DateTime.TryParse(row["InvoiceDate"].ToString(), out var date) && date >= new DateTime(1753, 1, 1)
              ? date
              : (DateTime?)null),
                Article = row["Article"].ToString(),
                Description = row["Description"].ToString(),
                Classification = row["fldClassification"].ToString(),
                Type = row["fldType"].ToString(),
                Status = row["fldStatus"].ToString()
            }).ToList();

            var employees = HRMISEmployees.GetEmployees();
            foreach (var row in result)
            {
                var emp = employees.FirstOrDefault(x => x?.FirstName?.ToLower() == row?.FirstName?.ToLower() && x.LastName?.ToLower() == row?.LastName?.ToLower());
                if (emp == null) continue;
                row.EmpId = emp.Id;
                row.ChiefId = HRMISEmployees.GetChief(emp.Office, emp.Division, null)?.ChiefId;
            }
            foreach (var row in result)
            {
                var ppe = await unitOfWork.PPesRepo.FindAsync(x => x.PropertyNo == row.PropertyNo);
                if (ppe == null)
                {
                    var newPPE = new PPEs
                    {
                        IssuedToId = row.EmpId,
                        ChiefId = row.ChiefId,
                        Gender = Models.Enums.Gender.Male,
                        DateCreated = DateTime.Now,
                        AquisitionDate = row.InvoiceDate,
                        Status = GetStatus(row.Status),
                        Quantity = row.Quantity,
                        Unit = GetUnit(row.Unit),
                        UnitValue = (long?)row.UnitCost,
                        TotalValue = row.Quantity * ((long?)row.UnitCost),
                        Remarks = row.Specs,
                        PropertyNo = row.PropertyNo
                    };
                    unitOfWork.PPesRepo.Insert(newPPE);
                }
                else continue;
            }
            await unitOfWork.SaveChangesAsync();
        }
        private Unit GetUnit(string unit)
        {
            Enum.TryParse(unit, out Unit enumUnit);
            return enumUnit;
        }
        private PPEStatus GetStatus(string status)
        {
            if (status == "Condemned") return PPEStatus.Condemned;
            if (status == "Issued") return PPEStatus.Issued;
            if (status == "Lost") return PPEStatus.Lost;
            if (status == "On Stock") return PPEStatus.OnStock;
            return PPEStatus.Condemned;
        }
        public async Task FixMigratedPPEEmployee()
        {
            access.Open();
            string query = "SELECT tblProperties.fldPropertyNo AS PropertyNo, tblOffices.fldOfficeID AS Office, " +
                        "tblEmployees.fldFirstname & ' ' & tblEmployees.fldMIddleName & ' ' & tblEmployees.fldLastname AS Fullname, tblEmployees.fldFirstname AS FirstName, tblEmployees.fldMIddleName, " +
                        "tblEmployees.fldLastname, tblItemsRecDetails.fldUnitCost AS UnitCost, tblItemsRecDetails.fldQty AS Quantity, tblItemsRecDetails.fldUnit AS Unit, " +
                        "tblItemsRecDetails.fldDescription AS Specs, tblItemsReceived.fldInvoiceDate AS InvoiceDate, tblArticles.fldArticleName AS Article, " +
                        "tblArticles.fldDescription AS Description, tblClass.fldClassification, tblType.fldType, tblAccounts.*, tblStatus.fldStatus " +
                        "FROM tblStatus INNER JOIN (tblOffices INNER JOIN (tblItemsReceived INNER JOIN ((tblClass INNER JOIN ((tblAccounts " +
                        "INNER JOIN (tblArticles INNER JOIN tblType ON tblArticles.fldTypeID = tblType.fldTypeID) ON tblAccounts.fldAccountCode = tblArticles.fldAccountCode) " +
                        "INNER JOIN tblItemsRecDetails ON tblArticles.fldArticleCode = tblItemsRecDetails.fldArticleCode) ON tblClass.fldClassCode = tblAccounts.fldClassCode) " +
                        "INNER JOIN (tblProperties INNER JOIN tblEmployees ON tblProperties.fldEmpID = tblEmployees.fldEmpID) ON tblItemsRecDetails.fldDRDNo = tblProperties.fldDRDNo) " +
                        "ON tblItemsReceived.fldIRTransNo = tblItemsRecDetails.fldIRTransNo) ON tblOffices.fldOfficeID = tblEmployees.fldOfficeID) ON tblStatus.fldSID = tblProperties.fldSID " +
                        "WHERE(((tblAccounts.fldAccountTitle) = 'IT Equipment and Software')) " +
                        "ORDER BY tblProperties.fldPropertyNo ";

            var cmd = new OleDbCommand(query, access);
            var da = new OleDbDataAdapter(cmd);
            var dt = new DataTable();
            da.Fill(dt);

            var result = dt.AsEnumerable().Select(row => new PPEItem
            {
                PropertyNo = row["PropertyNo"].ToString(),
                Office = row["Office"].ToString(),
                Fullname = row["Fullname"].ToString(),
                FirstName = row["FirstName"].ToString(),
                MiddleName = row["fldMIddleName"].ToString(),
                LastName = row["fldLastname"].ToString(),
                UnitCost = Convert.ToDecimal(row["UnitCost"]),
                Quantity = Convert.ToInt32(row["Quantity"]),
                Unit = row["Unit"].ToString(),
                Specs = row["Specs"].ToString(),
                InvoiceDate = (DateTime.TryParse(row["InvoiceDate"].ToString(), out var date) && date >= new DateTime(1753, 1, 1)
              ? date
              : (DateTime?)null),
                Article = row["Article"].ToString(),
                Description = row["Description"].ToString(),
                Classification = row["fldClassification"].ToString(),
                Type = row["fldType"].ToString(),
                Status = row["fldStatus"].ToString()
            }).ToList();

            var ppes = unitOfWork.PPesRepo.FindAllAsync(x => x.IssuedToId == null);
            var ofmisEmp = OFMISEmployees.GetAllEmployees();
            foreach (var ppe in ppes)
            {
                var res = result.FirstOrDefault(x => x.PropertyNo == ppe.PropertyNo);
                var emp = ofmisEmp.FirstOrDefault(x => x?.FirstName?.ToLower() == res?.FirstName?.ToLower() && x?.LastName?.ToLower() == res?.LastName?.ToLower());
                if (emp == null) continue;

                ppe.IssuedToId = emp.Id;
                ppe.ChiefId = OFMISEmployees.GetChief((int)emp.Id)?.Id;
            }
            await unitOfWork.SaveChangesAsync();
        }
    }

    public class PPEItem
    {
        public string PropertyNo { get; set; }
        public string Office { get; set; }
        public string Fullname { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public decimal UnitCost { get; set; }
        public int Quantity { get; set; }
        public string Unit { get; set; }
        public string Specs { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public string Article { get; set; }
        public string Description { get; set; }
        public string Classification { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public long? EmpId { get; set; }
        public long? ChiefId { get; set; }
    }
}
