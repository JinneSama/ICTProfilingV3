using ICTProfilingV3.DataTransferModels;
using ICTProfilingV3.DataTransferModels.ReportViewModel;
using Models.Entities;
using Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ICTProfilingV3.Interfaces
{
    public interface IDocActionsService : IBaseDataService<Actions, int>
    {
        IQueryable<Actions> GetActions(RequestType requestType, int id);
        Task DeleteDocAction(int actionId);
        IEnumerable<ActionTreeDTM> GetActionTree(int? parentId);
        IQueryable<ActionTaken> GetActionTakenList();
        Task AddDocAction(bool isSend, RequestType requestType, ActionDTM actionDTM,
            List<UsersDTM> routedUsers, int processId);
        Task UpdateDocAction(int actionId, bool isSend, RequestType requestType, 
            ActionDTM actionDTM, List<UsersDTM> routedUsers, int processId);
        Task<ActionDTM> GetDocAction(int actionId);
        IEnumerable<ActionDocuments> GetActionDocuments(int? actionId);
        void DeleteDocument(int documentId);
        Task ReorderDocument(int? actionId);
        IEnumerable<ActionsDTM> GetActionsByDTM(RequestType requestType, int id);
        Task<string> AddActionDocument(int? actionId);
        Task<Actions> GetLastAction(int actionId);
        Task UpdateDiscrepancy(int actionId, bool hasDiscrepancy, string remarks);
        IEnumerable<ActionReport> GetActionReport(string staffId,
            DateTime dateFrom, DateTime dateTo);
    }
}
