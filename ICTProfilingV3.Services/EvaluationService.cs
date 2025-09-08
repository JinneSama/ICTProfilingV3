using ICTProfilingV3.Interfaces;
using ICTProfilingV3.Services.Base;
using Models.Entities;
using Models.Enums;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System;
using System.Threading.Tasks;
using ICTProfilingV3.Core.Common;

namespace ICTProfilingV3.Services
{
    public class EvaluationService : BaseDataService<EvaluationSheet, int>, IEvaluationService
    {
        private readonly UserStore _userStore;
        public EvaluationService(IRepository<int, EvaluationSheet> baseRepo,
            UserStore userStore) : base(baseRepo)
        {
            _userStore = userStore;
        }

        public async Task CreateEvaluationSheet(RequestType requestType, int sheetParentId)
        {
            var order = 1;
            foreach (SheetService item in Enum.GetValues(typeof(SheetService)))
            {
                var sheet = new EvaluationSheet
                {
                    ItemOrder = order,
                    Service = item,
                    CreatedById = _userStore.UserId
                };
                SetProcess(sheetParentId, requestType, sheet);
                await base.AddAsync(sheet);
                order++;
            }
        }

        private void SetProcess(int sheetParentId, RequestType requestType, EvaluationSheet sheet)
        {
            if (requestType == RequestType.Deliveries)
                sheet.DeliveriesId = sheetParentId;
            if (requestType == RequestType.TechSpecs)
                sheet.TechSpecsId = sheetParentId;
            if (requestType == RequestType.Repairs)
                sheet.RepairId = sheetParentId;
            if (requestType == RequestType.CAS)
                sheet.CustomerActionSheetId = sheetParentId;
            if (requestType == RequestType.M365)
                sheet.MOAccountUserId = sheetParentId;
        }
    }
}
