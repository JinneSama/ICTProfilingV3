using ICTProfilingV3.Core.Common;
using ICTProfilingV3.DataTransferModels.Models;
using ICTProfilingV3.Interfaces;
using Models.Entities;
using Models.Enums;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ICTProfilingV3.EvaluationForms
{
    public partial class UCEvaluationSheet : DevExpress.XtraEditors.XtraUserControl
    {
        private readonly IEvaluationService _evaluationService;
        private readonly UserStore _userStore;
        private ActionType _evalType;
        public UCEvaluationSheet(UserStore userStore, IEvaluationService evaluationService)
        {
            _userStore = userStore;
            _evaluationService = evaluationService;
            InitializeComponent();
        }

        public async void InitForm(ActionType evalType)
        {
            _evalType = evalType;
            await LoadEvaluation();
        }
        public async Task LoadEvaluation()
        {
            IEnumerable<EvaluationSheet> sheets = null;

            if (_evalType.RequestType == RequestType.Deliveries) 
                sheets = _evaluationService.GetAll().Where(x => x.DeliveriesId == _evalType.Id);
            if (_evalType.RequestType == RequestType.TechSpecs)
                sheets = _evaluationService.GetAll().Where(x => x.TechSpecsId == _evalType.Id);
            if (_evalType.RequestType == RequestType.Repairs)
                sheets = _evaluationService.GetAll().Where(x => x.RepairId == _evalType.Id);
                if (_evalType.RequestType == RequestType.CAS)
                    sheets = _evaluationService.GetAll().Where(x => x.CustomerActionSheetId == _evalType.Id);
            if (_evalType.RequestType == RequestType.M365)
                sheets = _evaluationService.GetAll().Where(x => x.MOAccountUserId == _evalType.Id);

            if (sheets == null || sheets.Count() <= 0)
            {
                await CreateEvaluation();
                return;
            }
            gcEvalSheet.DataSource = sheets.ToList();
        }

        private async Task CreateEvaluation()
        {
            await _evaluationService.CreateEvaluationSheet(_evalType.RequestType, _evalType.Id);
            await LoadEvaluation();
        }

        private async void gridEvalSheet_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            var row = (EvaluationSheet)gridEvalSheet.GetFocusedRow();
            if (row == null) return;

            await UpdateSheet(row);
        }

        private async Task UpdateSheet(EvaluationSheet row)
        {
            var sheet = await _evaluationService.GetByIdAsync(row.Id);
            if (sheet == null) return;

            sheet.RatingValue = row.RatingValue;    
            sheet.Remarks = row.Remarks;
            await _evaluationService.SaveChangesAsync();
        }
    }
}
