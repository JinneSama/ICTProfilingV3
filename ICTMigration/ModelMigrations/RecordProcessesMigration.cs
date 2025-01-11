using ICTMigration.ICTv2Models;
using Models.Entities;
using Models.Enums;
using Models.Repository;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ICTMigration.ModelMigrations
{
    public class RecordProcessesMigration
    {
        private readonly ICTv2Entities ictv2Model;
        private readonly IUnitOfWork unitOfWork;
        public RecordProcessesMigration()
        {
            ictv2Model = new ICTv2Entities();
            unitOfWork = new UnitOfWork();
        }
        public async Task MigrateCAS()
        {
            var cas = ictv2Model.CustomerActionSheets.ToList();
            var maxId = cas.OrderBy(x => x.Id).LastOrDefault().Id;
            unitOfWork.ExecuteCommand("DBCC CHECKIDENT ('CustomerActionSheets', RESEED, 0);");
            for (int i = 1; i <= maxId; i++)
            {
                var sheet = ictv2Model.CustomerActionSheets.FirstOrDefault(x => x.Id == i);

                if (sheet == null)
                {
                    var newCAS = new Models.Entities.CustomerActionSheet();
                    unitOfWork.CustomerActionSheetRepo.Insert(newCAS);
                }
                else
                {
                    var lastName = GetLastWord(sheet.AssistedBy);
                    var users = unitOfWork.UsersRepo.GetAll().ToList();
                    var assisted = users.FirstOrDefault(x => x.FullName.IndexOf(lastName, StringComparison.OrdinalIgnoreCase) >= 0);
                    var createdby = await unitOfWork.UsersRepo.FindAsync(x => x.UserName == sheet.CreatedBy);

                    var newCAS = new Models.Entities.CustomerActionSheet
                    {
                        DateCreated = sheet.DateCreated,
                        ClientName = sheet.Name,
                        Office = sheet.OfficeAddress,
                        Gender = sheet.Gender == "Female" ? Gender.Female : Gender.Male,
                        ContactNo = sheet.ContactNo,
                        ClientRequest = sheet.ClientRequest,
                        ActionTaken = sheet.ActionTaken,
                        IsDeleted = sheet.IsDeleted,
                        AssistedBy = assisted,
                        CreatedBy = createdby
                    };
                    unitOfWork.CustomerActionSheetRepo.Insert(newCAS);
                }
            }
            await unitOfWork.SaveChangesAsync();

            unitOfWork.CustomerActionSheetRepo.DeleteRange(x => x.DateCreated == null);
            await unitOfWork.SaveChangesAsync();
        }

        public async Task MigratePR()
        {
            var prv2 = ictv2Model.PurchaseReqs.ToList();
            var maxId = prv2.OrderBy(x => x.Id).LastOrDefault().Id;

            unitOfWork.ExecuteCommand("DBCC CHECKIDENT ('PurchaseRequests', RESEED, 0);");
            for (int i = 1; i <= maxId; i++)
            {
                var pr = ictv2Model.PurchaseReqs.FirstOrDefault(x => x.Id == i);

                if(pr == null)
                {
                    var newPR = new PurchaseRequest();
                    unitOfWork.PurchaseRequestRepo.Insert(newPR);
                }
                else
                {
                    int? reqId = null;
                    var ts = ictv2Model.TechSpecs.FirstOrDefault(x => x.Id == pr.TechSpecsId);
                    if (ts != null) reqId = ts.RequestId;

                    var user = await unitOfWork.UsersRepo.FindAsync(x => x.UserName == pr.CreatedBy);
                    var newPR = new PurchaseRequest()
                    {
                        DateCreated = pr.DateRequested,
                        ChiefId = pr.RequestedByChiefId,
                        PRNo = pr.PRNo,
                        Quarter = GetQuarter(pr.Quarter),
                        TechSpecsId = reqId,
                        CreatedByUser = user
                    };
                    unitOfWork.PurchaseRequestRepo.Insert(newPR);
                }
            }
            await unitOfWork.SaveChangesAsync();
            unitOfWork.PurchaseRequestRepo.DeleteRange(x => x.CreatedByUser == null);
            await unitOfWork.SaveChangesAsync();
        }

        public async Task MigratePRStandardPR()
        {
            var prSPR = ictv2Model.PurchaseReqStandardEquipments.ToList();
            foreach (var pr in prSPR)
            {
                var prEquipment = await unitOfWork.StandardPRSpecsRepo.FindAsync(x => x.Id == pr.PREquipmentId);
                var newPRSPR = new PRStandardPRSpecs()
                {
                    ItemNo = pr.ItemNo,
                    Quantity = pr.Quantity,
                    TotalCost = prEquipment.UnitCost * pr.Quantity,
                    PurchaseRequestId = pr.PRId,
                    StandardPRSpecsId = pr.PREquipmentId
                };
                unitOfWork.PRStandardPRSpecsRepo.Insert(newPRSPR);
            }
            await unitOfWork.SaveChangesAsync();
        }

        private PRQuarter? GetQuarter(int? quarter)
        {
            if (quarter == null) return null;
            return PRQuarter.Fourth;
        }
        private string GetLastWord(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return string.Empty;

            string[] words = input.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            return words.Length > 0 ? words[words.Length - 1] : string.Empty;
        }
    }
}
