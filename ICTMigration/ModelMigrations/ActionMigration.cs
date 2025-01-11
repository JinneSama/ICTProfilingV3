using ICTMigration.ICTv2Models;
using Models.Entities;
using Models.Enums;
using Models.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ICTMigration.ModelMigrations
{
    public class ActionMigration
    {
        private readonly ICTv2Entities ictv2Model;
        private readonly IUnitOfWork unitOfWork;
        public ActionMigration()
        {
            ictv2Model = new ICTv2Entities();
            unitOfWork = new UnitOfWork();
        }
        public async Task MigrateTSActions()
        {
            unitOfWork.ExecuteCommand("DBCC CHECKIDENT ('Actions', RESEED, 0);");
            var tsActions = ictv2Model.DocActions.Where(x => x.TableName == "TechSpecs");
            var techSpecs = unitOfWork.TechSpecsRepo.GetAll().ToList();

            foreach (var techSpec in techSpecs)
            {
                var oldTS = ictv2Model.TechSpecs.FirstOrDefault(x => x.RequestId == techSpec.Id);
                var tsAction = tsActions.Where(x => x.RefId == oldTS.Id);

                foreach(var act in tsAction)
                {
                    var users = act.Users.ToList();
                    ICollection<Users> newUsers = new HashSet<Users>();
                    foreach(var user in users)
                    {
                        var currentUser = await unitOfWork.UsersRepo.FindAsync(x => x.UserName == user.UserName);
                        newUsers.Add(currentUser);
                    }

                    var program = await unitOfWork.ActionsDropdownsRepo.FindAsync(x => x.Id == act.ProgramId);
                    var mainAct = await unitOfWork.ActionsDropdownsRepo.FindAsync(x => x.Id == act.MainActivityId);
                    var activity = await unitOfWork.ActionsDropdownsRepo.FindAsync(x => x.Id == act.ActivityId);
                    var subAct = await unitOfWork.ActionsDropdownsRepo.FindAsync(x => x.Id == act.SubActivityId);

                    var oldcreatedBy = ictv2Model.Users.FirstOrDefault(x => x.Id == act.CreatedBy);
                    var createdby = await unitOfWork.UsersRepo.FindAsync(x => x.UserName == oldcreatedBy.UserName);

                    var newAction = new Actions
                    {
                        ActionTaken = act.ActionTaken,
                        DateCreated = act.DateCreated,
                        ActionDate = act.ActionDate,
                        Remarks = act.Remarks,
                        IsSend = act.IsSend,
                        ProgramId = program?.Id,
                        MainActId = mainAct?.Id,
                        ActivityId = activity?.Id,
                        SubActivityId = subAct?.Id,
                        TechSpecs = techSpec,
                        RequestType = RequestType.TechSpecs,
                        RoutedUsers = newUsers,
                        CreatedBy = createdby
                    };
                    unitOfWork.ActionsRepo.Insert(newAction);
                }
            }
            await unitOfWork.SaveChangesAsync();
        }

        public async Task MigrateDeliveriesActions()
        {
            //unitOfWork.ExecuteCommand("DBCC CHECKIDENT ('Actions', RESEED, 0);");
            var delActions = ictv2Model.DocActions.Where(x => x.TableName == "Deliveries");
            var deliveries = unitOfWork.DeliveriesRepo.GetAll().ToList();

            foreach (var deliver in deliveries)
            {
                if(deliver.Id != 3648) continue;
                var oldDel = ictv2Model.Deliveries.FirstOrDefault(x => x.RequestId == deliver.Id);
                if (oldDel == null) continue;
                var delAction = delActions.Where(x => x.RefId == oldDel.Id);
                if(delAction == null) continue;
                foreach (var act in delAction)
                {
                    var users = act.Users.ToList();
                    ICollection<Users> newUsers = new HashSet<Users>();
                    foreach (var user in users)
                    {
                        var currentUser = await unitOfWork.UsersRepo.FindAsync(x => x.UserName == user.UserName);
                        newUsers.Add(currentUser);
                    }

                    var program = await unitOfWork.ActionsDropdownsRepo.FindAsync(x => x.Id == act.ProgramId);
                    var mainAct = await unitOfWork.ActionsDropdownsRepo.FindAsync(x => x.Id == act.MainActivityId);
                    var activity = await unitOfWork.ActionsDropdownsRepo.FindAsync(x => x.Id == act.ActivityId);
                    var subAct = await unitOfWork.ActionsDropdownsRepo.FindAsync(x => x.Id == act.SubActivityId);

                    var oldcreatedBy = ictv2Model.Users.FirstOrDefault(x => x.Id == act.CreatedBy);
                    Users createdBy = null;
                    if (oldcreatedBy != null) createdBy = await unitOfWork.UsersRepo.FindAsync(x => x.UserName == oldcreatedBy.UserName);

                    var newAction = new Actions
                    {
                        ActionTaken = act.ActionTaken,
                        DateCreated = act.DateCreated,
                        ActionDate = act.ActionDate,
                        Remarks = act.Remarks,
                        IsSend = act.IsSend,
                        ProgramId = program?.Id,
                        MainActId = mainAct?.Id,
                        ActivityId = activity?.Id,
                        SubActivityId = subAct?.Id,
                        Deliveries = deliver,
                        RequestType = RequestType.Deliveries,
                        RoutedUsers = newUsers,
                        CreatedBy = createdBy
                    };
                    unitOfWork.ActionsRepo.Insert(newAction);
                }
            }
            await unitOfWork.SaveChangesAsync();
        }
        public async Task MigrateRepairActions()
        {
            //unitOfWork.ExecuteCommand("DBCC CHECKIDENT ('Actions', RESEED, 0);");
            var repairActions = ictv2Model.DocActions.Where(x => x.TableName == "Repairs");
            var repairs = unitOfWork.RepairsRepo.GetAll().ToList();

            foreach (var repair in repairs)
            {
                var oldDel = ictv2Model.Repairs.FirstOrDefault(x => x.RequestId == repair.Id);
                var repairAction = repairActions.Where(x => x.RefId == oldDel.Id);

                foreach (var act in repairAction)
                {
                    var users = act.Users.ToList();
                    ICollection<Users> newUsers = new HashSet<Users>();
                    foreach (var user in users)
                    {
                        var currentUser = await unitOfWork.UsersRepo.FindAsync(x => x.UserName == user.UserName);
                        newUsers.Add(currentUser);
                    }

                    var program = await unitOfWork.ActionsDropdownsRepo.FindAsync(x => x.Id == act.ProgramId);
                    var mainAct = await unitOfWork.ActionsDropdownsRepo.FindAsync(x => x.Id == act.MainActivityId);
                    var activity = await unitOfWork.ActionsDropdownsRepo.FindAsync(x => x.Id == act.ActivityId);
                    var subAct = await unitOfWork.ActionsDropdownsRepo.FindAsync(x => x.Id == act.SubActivityId);

                    var oldcreatedBy = ictv2Model.Users.FirstOrDefault(x => x.Id == act.CreatedBy);
                    Users createdBy = null;
                    if (oldcreatedBy != null) createdBy = await unitOfWork.UsersRepo.FindAsync(x => x.UserName == oldcreatedBy.UserName);

                    var newAction = new Actions
                    {
                        ActionTaken = act.ActionTaken,
                        DateCreated = act.DateCreated,
                        ActionDate = act.ActionDate,
                        Remarks = act.Remarks,
                        IsSend = act.IsSend,
                        ProgramId = program?.Id,
                        MainActId = mainAct?.Id,
                        ActivityId = activity?.Id,
                        SubActivityId = subAct?.Id,
                        Repairs = repair,
                        RequestType = RequestType.Repairs,
                        RoutedUsers = newUsers,
                        CreatedBy = createdBy
                    };
                    unitOfWork.ActionsRepo.Insert(newAction);
                }
            }
            await unitOfWork.SaveChangesAsync();
        }

        public async Task MigrateCASActions()
        {
            var casActions = ictv2Model.DocActions.Where(x => x.TableName == "CustomerActionSheet");
            var cas = unitOfWork.CustomerActionSheetRepo.GetAll().ToList();

            foreach(var item in cas)
            {
                var actions = casActions.Where(x => x.RefId == item.Id);
                
                foreach(var act in actions)
                {
                    var users = act.Users.ToList();
                    ICollection<Users> newUsers = new HashSet<Users>();
                    foreach (var user in users)
                    {
                        var currentUser = await unitOfWork.UsersRepo.FindAsync(x => x.UserName == user.UserName);
                        newUsers.Add(currentUser);
                    }

                    var program = await unitOfWork.ActionsDropdownsRepo.FindAsync(x => x.Id == act.ProgramId);
                    var mainAct = await unitOfWork.ActionsDropdownsRepo.FindAsync(x => x.Id == act.MainActivityId);
                    var activity = await unitOfWork.ActionsDropdownsRepo.FindAsync(x => x.Id == act.ActivityId);
                    var subAct = await unitOfWork.ActionsDropdownsRepo.FindAsync(x => x.Id == act.SubActivityId);

                    var oldcreatedBy = ictv2Model.Users.FirstOrDefault(x => x.Id == act.CreatedBy);
                    Users createdBy = null;
                    if (oldcreatedBy != null) createdBy = await unitOfWork.UsersRepo.FindAsync(x => x.UserName == oldcreatedBy.UserName);

                    var newAction = new Actions
                    {
                        ActionTaken = act.ActionTaken,
                        DateCreated = act.DateCreated,
                        ActionDate = act.ActionDate,
                        Remarks = act.Remarks,
                        IsSend = act.IsSend,
                        ProgramId = program?.Id,
                        MainActId = mainAct?.Id,
                        ActivityId = activity?.Id,
                        SubActivityId = subAct?.Id,
                        CustomerActionSheet = item,
                        RequestType = RequestType.CAS,
                        RoutedUsers = newUsers,
                        CreatedBy = createdBy
                    };
                    unitOfWork.ActionsRepo.Insert(newAction);
                }
            }
            await unitOfWork.SaveChangesAsync();

        }

        public async Task MigratePR()
        {
            var prActions = ictv2Model.DocActions.Where(x => x.TableName == "PR");
            var pr = unitOfWork.PurchaseRequestRepo.GetAll().ToList();

            foreach (var item in pr)
            {
                var actions = prActions.Where(x => x.RefId == item.Id);

                foreach (var act in actions)
                {
                    var users = act.Users.ToList();
                    ICollection<Users> newUsers = new HashSet<Users>();
                    foreach (var user in users)
                    {
                        var currentUser = await unitOfWork.UsersRepo.FindAsync(x => x.UserName == user.UserName);
                        newUsers.Add(currentUser);
                    }

                    var program = await unitOfWork.ActionsDropdownsRepo.FindAsync(x => x.Id == act.ProgramId);
                    var mainAct = await unitOfWork.ActionsDropdownsRepo.FindAsync(x => x.Id == act.MainActivityId);
                    var activity = await unitOfWork.ActionsDropdownsRepo.FindAsync(x => x.Id == act.ActivityId);
                    var subAct = await unitOfWork.ActionsDropdownsRepo.FindAsync(x => x.Id == act.SubActivityId);

                    var oldcreatedBy = ictv2Model.Users.FirstOrDefault(x => x.Id == act.CreatedBy);
                    Users createdBy = null;
                    if (oldcreatedBy != null) createdBy = await unitOfWork.UsersRepo.FindAsync(x => x.UserName == oldcreatedBy.UserName);

                    var newAction = new Actions
                    {
                        ActionTaken = act.ActionTaken,
                        DateCreated = act.DateCreated,
                        ActionDate = act.ActionDate,
                        Remarks = act.Remarks,
                        IsSend = act.IsSend,
                        ProgramId = program?.Id,
                        MainActId = mainAct?.Id,
                        ActivityId = activity?.Id,
                        SubActivityId = subAct?.Id,
                        PurchaseRequest = item,
                        RequestType = RequestType.CAS,
                        RoutedUsers = newUsers,
                        CreatedBy = createdBy
                    };
                    unitOfWork.ActionsRepo.Insert(newAction);
                }
            }
            await unitOfWork.SaveChangesAsync();

        }
    }
}
