using ICTProfilingV3.Core.Common;
using ICTProfilingV3.DataTransferModels;
using ICTProfilingV3.Interfaces;
using Models.Entities;
using Models.Enums;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace ICTProfilingV3.Services
{
    public class DocActionsService : IDocActionsService
    {
        private readonly IRepository<int, Actions> _actionsRepo;
        private readonly IRepository<int, ActionsDropdowns> _actionTreeRepo;
        private readonly IRepository<int, ActionTaken> _actionTakenRepo;
        private readonly IRepository<string, Users> _usersRepo;
        private readonly IRepository<int, ActionDocuments> _actionDocumentsRepo;
        private readonly IEncryptFile _encryptFile;
        private readonly UserStore _userStore;

        public DocActionsService(IRepository<int, Actions> actionsRepo, IRepository<int, ActionTaken> actionTakenRepo,
            IRepository<int, ActionsDropdowns> actionTreeRepo, IRepository<string, Users> usersRepo, 
            UserStore userStore, IRepository<int, ActionDocuments> actionDocumentsRepo, IEncryptFile encryptFile)
        {
            _actionsRepo = actionsRepo;
            _usersRepo = usersRepo;
            _actionTreeRepo = actionTreeRepo;
            _actionTakenRepo = actionTakenRepo;
            _userStore = userStore;
            _actionDocumentsRepo = actionDocumentsRepo;
            _encryptFile = encryptFile;
        }

        public IQueryable<ActionTaken> GetActionTakenList()
        {
            return _actionTakenRepo.GetAll();
        }
        public IEnumerable<ActionTreeDTM> GetActionTree(int? parentId)
        {
            var actionTree = _actionTreeRepo.Fetch(x => x.ParentId == parentId)
                             ?? _actionTreeRepo.Fetch(x => x.ActionCategory == Models.Enums.ActionCategory.Programs);

            return actionTree.Select(s => new ActionTreeDTM
            {
                ActionTree = s,
                NodeValue = s.Order + "." + s.Value
            }).OrderBy(o => o.ActionTree.Order).ToList();
        }

        public async Task AddDocAction(bool isSend, RequestType requestType, 
            ActionDTM actionDTM, List<UsersDTM> routedUsers, int processId)
        {
            var docAction = new Actions
            {
                DateCreated = DateTime.Now,
                ActionDate = actionDTM.ActionDate,
                ActionTaken = actionDTM.ActionTaken,
                Remarks = actionDTM.Remarks,
                IsSend = isSend,
                CreatedById = _userStore.UserId,
                ProgramId = actionDTM.Program,
                MainActId = actionDTM.MainActivity,
                ActivityId = actionDTM.Activity,
                SubActivityId = actionDTM.SubActivity,
                RequestType = requestType
            };
            AssignActionToProcess(docAction, requestType, processId);

            if(routedUsers != null)
            {
                foreach (var user in routedUsers)
                {
                    var userEntity = await _usersRepo.GetById(user.Id);
                    docAction.RoutedUsers.Add(userEntity);
                }
            }
            await _actionsRepo.AddAsync(docAction);
        }
        public async Task UpdateDocAction(int actionId, bool isSend, RequestType requestType, 
            ActionDTM actionDTM, List<UsersDTM> routedUsers, int processId)
        {
            var docAction = await _actionsRepo.GetById(actionId);
            docAction.ActionDate = actionDTM.ActionDate;
            docAction.ActionTaken = actionDTM.ActionTaken;
            docAction.Remarks = actionDTM.Remarks;
            docAction.IsSend = isSend;
            docAction.CreatedById = _userStore.UserId;
            docAction.ProgramId = actionDTM.Program;
            docAction.MainActId = actionDTM.MainActivity;
            docAction.ActivityId = actionDTM.Activity;
            docAction.SubActivityId = actionDTM.SubActivity;
            docAction.RequestType = requestType;

            docAction.RoutedUsers.Clear();
            AssignActionToProcess(docAction, requestType, processId);

            if (routedUsers != null)
            {
                foreach (var user in routedUsers)
                {
                    var userEntity = await _usersRepo.GetById(user.Id);
                    docAction.RoutedUsers.Add(userEntity);
                }
            }

            await _actionsRepo.SaveChangesAsync();
        }

        public async Task<ActionDTM> GetDocAction(int actionId)
        {
            var action = await _actionsRepo.GetById(actionId);
            var actionDTM = new ActionDTM
            {
                ActionDate = action.ActionDate,
                ActionTaken = action.ActionTaken,
                Remarks = action.Remarks,
                IsSend = action.IsSend,
                CreatedBy = action.CreatedById,
                Program = action.ProgramId,
                MainActivity = action.MainActId,
                Activity = action.ActivityId,
                SubActivity = action.SubActivityId,
                RequestType = action.RequestType,
                RoutedTo = string.Join(", ", action.RoutedUsers.Select(u => u.FullName)),
                RoutedUsersObject = action.RoutedUsers
                .Select(u => new UsersDTM
                {
                    Id = u.Id,
                    Username = u.UserName,
                    Fullname = u.FullName
                }).ToList()
            };
            return actionDTM;
        }

        private void AssignActionToProcess(Actions action, RequestType requestType, int ProcessId)
        {
            switch (requestType)
            {
                case RequestType.PR:
                    action.PurchaseRequestId = ProcessId;
                    break;
                case RequestType.TechSpecs:
                    action.TechSpecsId = ProcessId;
                    break;
                case RequestType.Repairs:
                    action.RepairId = ProcessId;
                    break;
                case RequestType.Deliveries:
                    action.DeliveriesId = ProcessId;
                    break;
                case RequestType.CAS:
                    action.CustomerActionSheetId = ProcessId;
                    break;
                case RequestType.PGN:
                    action.PGNRequestId = ProcessId;
                    break;
                case RequestType.M365:
                    action.MOAccountUserId = ProcessId;
                    break;
            }
        }

        public IEnumerable<ActionDocuments> GetActionDocuments(int? actionId)
        {
            return _actionDocumentsRepo.Fetch(x => x.ActionId == actionId).OrderBy(x => x.DocOrder).ToList();
        }

        public void DeleteDocument(int documentId)
        {
            _actionDocumentsRepo.Delete(documentId);
        }

        public void ReorderDocument(int? actionId)
        {
            var docs = _actionDocumentsRepo.Fetch(x => x.ActionId == actionId);
            int order = 1;
            foreach (var doc in docs)
            {
                doc.DocOrder = order;
                order++;
            }
            _actionDocumentsRepo.SaveChangesAsync();
        }

        public async Task<string> AddActionDocument(int? actionId)
        {
            int DocOrder = 1;
            var docs = GetActionDocuments(actionId);
            if (docs.LastOrDefault() != null) DocOrder = docs.LastOrDefault().DocOrder + 1;

            var actionDocs = new ActionDocuments
            {
                ActionId = actionId,
                DocOrder = DocOrder
            };

            await _actionDocumentsRepo.AddAsync(actionDocs);
            await _actionDocumentsRepo.SaveChangesAsync();

            var actionDocsRes = await _actionDocumentsRepo.GetById(actionDocs.Id);
            var documentData = _encryptFile.EncryptFile("Action_Document_" + actionDocsRes.Id);

            if (actionDocsRes != null)
            {
                actionDocsRes.SecurityStamp = documentData.securityStamp;
                actionDocsRes.DocumentName = documentData.filename + ".jpeg";
            }
            await _actionDocumentsRepo.SaveChangesAsync();
            return actionDocsRes.DocumentName;
        }

        public IQueryable<Actions> GetActions(RequestType requestType, int id)
        {
            var query = _actionsRepo.GetAll().Where(x => x.RequestType == requestType);

            switch (requestType)
            {
                case RequestType.PR:
                    query = query.Where(x => x.PurchaseRequestId == id);
                    break;
                case RequestType.TechSpecs:
                    query = query.Where(x => x.TechSpecsId == id);
                    break;
                case RequestType.Repairs:
                    query = query.Where(x => x.RepairId == id);
                    break;
                case RequestType.Deliveries:
                    query = query.Where(x => x.DeliveriesId == id);
                    break;
                case RequestType.CAS:
                    query = query.Where(x => x.CustomerActionSheetId == id);
                    break;
                case RequestType.PGN:
                    query = query.Where(x => x.PGNRequestId == id);
                    break;
                case RequestType.M365:
                    query = query.Where(x => x.MOAccountUserId == id);
                    break;
            }
            query.Include(x => x.CreatedBy)
                 .Include(x => x.ProgramDropdowns)
                 .Include(x => x.ActivityDropdowns)
                 .Include(x => x.MainActDropdowns)
                 .Include(x => x.SubActivityDropdowns)
                 .Include(x => x.RoutedUsers)
                 .Include(x => x.ActionDocuments);

            return query;
        }

        public async Task DeleteDocAction(int actionId)
        {
            _actionsRepo.Delete(actionId);
            await _actionsRepo.SaveChangesAsync();
        }

        public IEnumerable<ActionsDTM> GetActionsByDTM(RequestType requestType, int id)
        {
            var actionQuery = GetActions(requestType, id)
                .Include(x => x.SubActivityDropdowns);

            var actions = actionQuery.ToList();

            var actionsModel = actions.Select(x => new ActionsDTM
            {
                Id = x.Id,
                ActionDate = x.ActionDate,
                CreatedBy = x.CreatedBy?.UserName,
                SubActivity = x.SubActivityDropdowns == null ? "" : x.SubActivityDropdowns.Value,
                ActionTaken = x.ActionTaken,
                RoutedTo = string.Join(",", x.RoutedUsers.Select(s => s.FullName)),
                Remarks = x.Remarks,
                CreatedById = x.CreatedById,
                Actions = x,
                IsSend = (x.IsSend ?? false) && x.RoutedUsers.Any(),
                hasDocuments = x.ActionDocuments.Any(),
                RoutedToSelf = x.RoutedUsers.Any(u => u.Id == _userStore.UserId),
                WithDiscrepancy = x.WithDiscrepancy,
                DiscrepancyRemarks = x.DiscrepancyRemarks
            }).OrderByDescending(o => o.ActionDate);

            return actionsModel.ToList();
        }

        public async Task<Actions> GetLastAction(int actionId)
        {
            return await _actionsRepo.GetAll().OrderByDescending(x => x.DateCreated).FirstOrDefaultAsync(x => x.CreatedById == _userStore.UserId);
        }

        public async Task UpdateDiscrepancy(int actionId, bool hasDiscrepancy, string remarks)
        {
            var action = await _actionsRepo.GetById(actionId);
            action.WithDiscrepancy = hasDiscrepancy;
            action.DiscrepancyRemarks = remarks;
            await _actionsRepo.SaveChangesAsync();
        }
    }
}
