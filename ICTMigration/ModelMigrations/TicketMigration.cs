using ICTMigration.ICTv2Models;
using Models.Entities;
using Models.Enums;
using Models.HRMISEntites;
using Models.Repository;
using System;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.ConstrainedExecution;
using System.Threading.Tasks;

namespace ICTMigration.ModelMigrations
{
    public class TicketMigration
    {
        private readonly ICTv2Entities ictv2Model;
        private readonly IUnitOfWork unitOfWork;
        public TicketMigration()
        {
            ictv2Model = new ICTv2Entities();
            unitOfWork = new UnitOfWork();
        }
        public async Task MigrateTickets()
        {
            unitOfWork.ExecuteCommand("DBCC CHECKIDENT ('TicketRequests', RESEED, 0);");
            var tickets = ictv2Model.TicketRequests.ToList();
            var maxId = tickets.OrderBy(o => o.Id).ToList().LastOrDefault().Id;

            for(int i = 1; i <= maxId; i++)
            {
                var ticket = tickets.FirstOrDefault(x => x.Id == i);
                if(ticket == null)
                {
                    var ictTicket = new Models.Entities.TicketRequest
                    {
                        Id = i,
                        RequestType = RequestType.TechSpecs,
                        TicketStatus = TicketStatus.Accepted
                    };
                    unitOfWork.TicketRequestRepo.Insert(ictTicket);
                }
                else
                {
                    var user = await unitOfWork.UsersRepo.FindAsync(x => x.UserName == ticket.CreatedBy);

                    var ictTicket = new Models.Entities.TicketRequest
                    {
                        Id = ticket.Id,
                        DateCreated = ticket?.DateCreated,
                        CreatedBy = user?.Id,
                        RequestType = GetTicketType(ticket),
                        TicketStatus = GetStatus(ticket)
                    };
                    unitOfWork.TicketRequestRepo.Insert(ictTicket);
                }
            }
            await unitOfWork.SaveChangesAsync();

            unitOfWork.TicketRequestRepo.DeleteRange(x => x.DateCreated == null);
            await unitOfWork.SaveChangesAsync();
        }

        private TicketStatus GetStatus(ICTv2Models.TicketRequest ticket)
        {
            if (ticket.StatusLatest == "Pending") return TicketStatus.OnProcess;
            if (ticket.StatusLatest == "On-Process") return TicketStatus.OnProcess;
            if (ticket.StatusLatest == "For Release") return TicketStatus.ForRelease;
            if (ticket.StatusLatest == "Completed") return TicketStatus.Completed;
            if (ticket.StatusLatest == "Accepted") return TicketStatus.Assigned;
            return TicketStatus.Accepted;
        }
        private RequestType GetTicketType(ICTv2Models.TicketRequest ticket)
        {
            if (ticket.TypeId == 1) return RequestType.Repairs;
            if (ticket.TypeId == 2) return RequestType.TechSpecs;
            if (ticket.TypeId == 3) return RequestType.Deliveries;
            return RequestType.TechSpecs;
        }

        public async Task MigrateDeliveries()
        {
            var deliveries = ictv2Model.Deliveries.ToList();
            foreach (var deliver in deliveries)
            {
                if (deliver.Id != 149) 
                    continue;
                var supplier = await unitOfWork.SupplierRepo.FindAsync(x => x.OldPK == deliver.SupplierId);
                var ticket = await unitOfWork.TicketRequestRepo.FindAsync(x => x.Id == deliver.RequestId);
                var newDelivery = new Deliveries
                {
                    DeliveredDate = deliver.DeliveredDate,
                    DateRequested = deliver.DateRequested,
                    RequestedById = (long)deliver.ReqById,
                    ReqByChiefId = (long)deliver.ReqByChiefId,
                    DeliveredById = (long)deliver.ItemDelById,
                    Gender = deliver.Gender == "Female" ? Gender.Female : Gender.Male,
                    ContactNo = deliver.ContactNo,
                    PONo = deliver.PONo.ToString(),
                    ReceiptNo = deliver.DelReceiptNo,
                    OldPK = deliver.Id,
                    Supplier = supplier,
                    TicketRequest = ticket
                };
                unitOfWork.DeliveriesRepo.Insert(newDelivery);

                var deliveriesSpecs = deliver.DeliveriesICTSpecs.ToList();
                foreach(var deliverSpec in deliveriesSpecs)
                {
                    var model = await unitOfWork.ModelRepo.FindAsync(x => x.OldPK == deliverSpec.ModelId);
                    var newDelSpecs = new DeliveriesSpecs
                    {
                        ItemNo = deliverSpec.ItemNo,
                        Description = deliverSpec.Description,
                        Remarks = deliverSpec.Remarks,
                        Quantity = (int?)deliverSpec.Quantity,
                        Unit = GetUnit(deliverSpec.Unit),
                        UnitCost = (long?)deliverSpec.UnitCost,
                        TotalCost = (long?)deliverSpec.TotalCost,
                        ProposedBudget = (long?)deliverSpec.ProposedBudget,
                        Purpose = deliverSpec.Purpose,
                        IsActive = deliverSpec.IsActive,
                        SerialNo = deliverSpec.SerialNo,
                        Model = model,
                        Deliveries = newDelivery
                    };
                    unitOfWork.DeliveriesSpecsRepo.Insert(newDelSpecs);

                    var deliveriesSpecsDetails = deliverSpec.DeliveriesICTSpecsDetails.ToList();
                    foreach (var item in deliveriesSpecsDetails)
                    {
                        var newDelSpecsDetail = new DeliveriesSpecsDetails
                        {
                            ItemNo = (int)item.ItemNo,
                            Specs = item.EquipmentSpecs,
                            Description = item.EquipmentDescrip,
                            DeliveriesSpecs = newDelSpecs
                        };
                        unitOfWork.DeliveriesSpecsDetailsRepo.Insert(newDelSpecsDetail);
                    }
                }
                await unitOfWork.SaveChangesAsync();
            }
        }

        public async Task MigrateTechSpecs()
        {
            var ts = ictv2Model.TechSpecs.ToList();

            foreach (var item in ts)
            {
                var ticket = await unitOfWork.TicketRequestRepo.FindAsync(x => x.Id == item.RequestId);
                if (ticket == null) continue;

                var preparedBy = item.PreparedBy == null ? null : await GetUserFromHRMIS((long)item.PreparedBy);
                var notedBy = item.NotedBy == null ? null : await GetUserFromHRMIS((long)item.NotedBy);
                var reviewdBy = item.ReviewedBy == null ? null : await GetUserFromHRMIS((long)item.ReviewedBy);

                var newTS = new TechSpecs
                {
                    DateAccepted = item.DateAccepted,
                    DateRequested = item.DateRequested,
                    ReqById = (long)item.RequestedById,
                    ReqByChiefId = (long)item.RequestedByChiefId,
                    ReqByGender = item.Gender == "Female" ? Gender.Female : Gender.Male,
                    ContactNo = item.ContactNo,
                    RequestBasedApprovedAIP = item.RequestBasedApprovedAIP,
                    RequestBasedApprovedPR = item.RequestBasedApprovedPR,
                    RequestBasedApprovedAPP = item.RequestBasedApprovedAPP,
                    RequestBasedApprovedPPMP = item.RequestBasedApprovedPPMP,
                    RequestBasedRequestLetter = item.RequestBasedRequestLetter,
                    RequestBasedForReplacement = item.RequestedByForReplacement,
                    PreparedById = preparedBy,
                    NotedById = notedBy,
                    ReviewedById = reviewdBy,
                    TicketRequest = ticket
                };
                unitOfWork.TechSpecsRepo.Insert(newTS);

                var tsSpecs = item.TechSpecsICTSpecs.ToList();
                foreach(var specItem in tsSpecs)
                {
                    var ictSpecs = await unitOfWork.EquipmentSpecsRepo.FindAsync(x => x.OldPK == specItem.ICTSpecsId);
                    var newTSSpec = new TechSpecsICTSpecs
                    {
                        ItemNo = specItem.ItemNo ?? 0,
                        Quantity = (int)specItem.Quantity,
                        Unit = GetUnit(specItem.Unit),
                        UnitCost = (long)specItem.UnitCost,
                        TotalCost = (long)specItem.TotalCost,
                        Description = specItem.Description,
                        Purpose = specItem.Purpose,
                        TechSpecs = newTS,
                        EquipmentSpecs = ictSpecs
                    };
                    unitOfWork.TechSpecsICTSpecsRepo.Insert(newTSSpec);

                    var tsSpecsDetails = specItem.TechSpecsICTSpecsDetails.ToList();
                    foreach (var specDetail in tsSpecsDetails)
                    {
                        var newSpecDetail = new TechSpecsICTSpecsDetails
                        {
                            ItemNo = specDetail.ItemNo ?? 0,
                            Specs = specDetail.EquipmentSpecs,
                            Description = specDetail.EquipmentDescrip,
                            IsActive = specDetail.IsActive,
                            TechSpecsICTSpecs = newTSSpec
                        };
                        unitOfWork.TechSpecsICTSpecsDetailsRepo.Insert(newSpecDetail);
                    }
                }
                await unitOfWork.SaveChangesAsync();
            }
        }

        private async Task<string> GetUserFromHRMIS(long hrisId)
        {
            var emp = HRMISEmployees.GetEmployeeById(hrisId).Username;
            var user = await unitOfWork.UsersRepo.FindAsync(x => x.UserName == emp);
            return user.Id;
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

        public async Task MigratePPE()
        {
            var ppe = ictv2Model.PPEsNews.ToList();
            foreach (var item in ppe)
            {
                var newPPE = new PPEs
                {
                    IssuedToId = item.IssuedToId,
                    ChiefId = item.ChiefId,
                    Gender = item.Gender == "Female" ? Gender.Female : Gender.Male,
                    ContactNo = item.ContactNo,
                    PropertyNo = item.PropertyNo == 0 ? (string.IsNullOrWhiteSpace(item.PropertyNoString) ? "N/A" : item.PropertyNoString) : item.PropertyNo.ToString(),
                    SerialNo = item.SerialNo,
                    DateCreated = item.DateCreated,
                    AquisitionDate = item.InvoiceDate,
                    Status = GetStatus(item.Status),
                    Quantity = (int)item.Quantity,
                    Unit = GetUnit(item.Unit),
                    UnitValue = (long?)item.UnitCost,
                    TotalValue = (long?)item.TotalCost,
                    Remarks = item.Remarks,
                    IsDeleted = item.Deleted,
                    OldPk = item.Id
                };
                unitOfWork.PPesRepo.Insert(newPPE);
                
                var model = await unitOfWork.ModelRepo.FindAsync(x => x.OldPK == item.ModelId);
                var newPPESpec = new PPEsSpecs
                {
                    ItemNo = 1,
                    Description = item.Description,
                    Purpose = item.Remarks,
                    Quantity = (int)item.Quantity,
                    Unit = GetUnit(item.Unit),
                    UnitCost = (long)item.UnitCost,
                    TotalCost = (long)item.TotalCost,
                    Remarks = item.Remarks,
                    SerialNo = item.SerialNo,
                    Model = model,
                    PPEs = newPPE
                };
                unitOfWork.PPEsSpecsRepo.Insert(newPPESpec);
                var ppeSpecsDetail = item.PPEsNewSpecs.ToList();

                foreach(var spec in ppeSpecsDetail)
                {
                    var newPPESpecDetail = new PPEsSpecsDetails
                    {
                        ItemNo = (int)spec.ItemNo,
                        Specs = spec.EquipmentSpecs,
                        Description = spec.EquipmentDescrip,
                        PPEsSpecs = newPPESpec
                    };
                    unitOfWork.PPEsSpecsDetailsRepo.Insert(newPPESpecDetail);
                }
            }
            await unitOfWork.SaveChangesAsync();
        }

        public async Task MigrateRepairs()
        {
            var repairs = ictv2Model.Repairs.ToList();
            foreach (var repair in repairs)
            {
                var ticket = await unitOfWork.TicketRequestRepo.FindAsync(x => x.Id == repair.RequestId);
                if (ticket == null) continue;
                var ppe = await unitOfWork.PPesRepo.FindAsync(x => x.OldPk == repair.PPENo);

                Users receivedBy = null;
                string receivedById;
                if (repair.ReceivedById == 0)
                {
                    string name = repair.ReceivedBy;
                    if (name == null) receivedBy = null;
                    else
                        if (name.Contains("JAKE")) receivedBy = await unitOfWork.UsersRepo.FindAsync(x => x.UserName == "pitd.jsaladino");
                        else if (name.Contains("MA.") || name.Contains("Ma.")) receivedBy = await unitOfWork.UsersRepo.FindAsync(x => x.UserName == "pitd.epilotin");
                    
                    receivedById = receivedBy?.Id;
                }
                else
                {
                    if (repair.ReceivedById == 2135698768)
                    {
                        receivedBy = await unitOfWork.UsersRepo.FindAsync(x => x.UserName == "pitd.jsaladino");
                        receivedById = receivedBy.Id;
                    } 
                    else receivedById = repair.ReceivedById == null ? null : await GetUserFromHRMIS((long)repair.ReceivedById);
                }
                var AssesedBy = repair.AssessedById == null ? null : await GetUserFromHRMIS((long)repair.AssessedById);
                var notedBy = repair.NotedById == null ? null : await GetUserFromHRMIS((long)repair.NotedById);

                var newRepair = new Repairs
                {
                    TicketRequest = ticket,
                    RequestedById = repair.ReqByIdNo,
                    ReqByChiefId = repair.ReqByChiefIdNo,
                    DeliveredById = repair.ItemDelById,
                    DateCreated = repair.DateCreated,
                    DateDelivered = repair.DateDelivered,
                    Problems = repair.ReqProblem,
                    Findings = repair.Findings,
                    Recommendations = repair.Recommendations,
                    IsDeleted = repair.IsDeleted,
                    Gender = repair.Gender == "Female" ? Gender.Female : Gender.Male,
                    ContactNo = repair.ContactNo,
                    PPEs = ppe,
                    PreparedById = receivedById,
                    ReviewedById = AssesedBy,
                    NotedById = notedBy
                };
                unitOfWork.RepairsRepo.Insert(newRepair);
            }
            await unitOfWork.SaveChangesAsync();
        }
    }
}
