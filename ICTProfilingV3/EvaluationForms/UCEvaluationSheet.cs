using ICTProfilingV3.Core.Common;
using ICTProfilingV3.DataTransferModels.Models;
using Models.Entities;
using Models.Enums;
using Models.Models;
using Models.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ICTProfilingV3.EvaluationForms
{
    public partial class UCEvaluationSheet : DevExpress.XtraEditors.XtraUserControl
    {
        private readonly UserStore _userStore;
        private ActionType _evalType;
        public UCEvaluationSheet(UserStore userStore)
        {
            _userStore = userStore;
            InitializeComponent();
        }

        public void InitForm(ActionType evalType)
        {
            _evalType = evalType;
            LoadEvaluation();
        }
        public void LoadEvaluation()
        {
            var uow = new UnitOfWork();
            IEnumerable<EvaluationSheet> sheets = null;

            if (_evalType.RequestType == RequestType.Deliveries) 
                sheets = uow.EvaluationSheetRepo.FindAllAsync(x => x.DeliveriesId == _evalType.Id);
            if (_evalType.RequestType == RequestType.TechSpecs)
                sheets = uow.EvaluationSheetRepo.FindAllAsync(x => x.TechSpecsId == _evalType.Id);
            if (_evalType.RequestType == RequestType.Repairs)
                sheets = uow.EvaluationSheetRepo.FindAllAsync(x => x.RepairId == _evalType.Id);
            if (_evalType.RequestType == RequestType.CAS)
                sheets = uow.EvaluationSheetRepo.FindAllAsync(x => x.CustomerActionSheetId == _evalType.Id);
            if (_evalType.RequestType == RequestType.M365)
                sheets = uow.EvaluationSheetRepo.FindAllAsync(x => x.MOAccountUserId == _evalType.Id);

            if (sheets == null || sheets.Count() <= 0)
            {
                CreateEvaluation();
                return;
            }
            gcEvalSheet.DataSource = sheets.ToList();
        }

        private void CreateEvaluation()
        {
            var uow = new UnitOfWork();
            var order = 1;
            foreach (SheetService item in Enum.GetValues(typeof(SheetService)))  
            {
                var sheet = new EvaluationSheet
                {
                    ItemOrder = order,
                    Service = item,
                    CreatedById = _userStore.UserId
                };
                SetProcess(sheet);
                uow.EvaluationSheetRepo.Insert(sheet);
                order++;
            }
            uow.Save();
            LoadEvaluation();
        }

        private void SetProcess(EvaluationSheet sheet)
        {
            if (_evalType.RequestType == RequestType.Deliveries)
                sheet.DeliveriesId = _evalType.Id;
            if (_evalType.RequestType == RequestType.TechSpecs)
                sheet.TechSpecsId = _evalType.Id;
            if (_evalType.RequestType == RequestType.Repairs)
                sheet.RepairId = _evalType.Id;
            if (_evalType.RequestType == RequestType.CAS)
                sheet.CustomerActionSheetId = _evalType.Id;
            if (_evalType.RequestType == RequestType.M365)
                sheet.MOAccountUserId = _evalType.Id;
        }

        private async void gridEvalSheet_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            var row = (EvaluationSheet)gridEvalSheet.GetFocusedRow();
            if (row == null) return;

            await UpdateSheet(row);
        }

        private async Task UpdateSheet(EvaluationSheet row)
        {
            var uow = new UnitOfWork();
            var sheet = await uow.EvaluationSheetRepo.FindAsync(x => x.Id == row.Id);
            if (sheet == null) return;

            sheet.RatingValue = row.RatingValue;    
            sheet.Remarks = row.Remarks;
            uow.EvaluationSheetRepo.Update(sheet);
            await uow.SaveChangesAsync();
        }
    }
}
