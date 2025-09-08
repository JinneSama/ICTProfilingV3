using ICTProfilingV3.BaseClasses;
using ICTProfilingV3.Interfaces;
using Models.Entities;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace ICTProfilingV3.ActionsForms
{
    public partial class frmActionList : BaseForm
    {
        private readonly IDocActionsService _doctActService;
        private readonly IRepository<int, ActionTaken> _actionTakenService;
        public frmActionList(IDocActionsService doctActService, IRepository<int, ActionTaken> actionTakenService)
        {
            InitializeComponent();
            _doctActService = doctActService;
            _actionTakenService = actionTakenService;
            LoadActionLists();
        }

        private void LoadActionLists()
        {
            var res = _doctActService.GetActionTakenList().ToList();
            gcActionTaken.DataSource = new BindingList<ActionTaken>(res);
        }

        private async void gridActionTaken_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            var row = (ActionTaken)gridActionTaken.GetFocusedRow();
            var res = await _actionTakenService.GetById(row.Id);
            if (res == null)
                await _actionTakenService.AddAsync(row);
            else 
                await UpdateAction(row, res);
            LoadActionLists();
        }

        private async Task UpdateAction(ActionTaken row, ActionTaken res)
        {
            res.Action = row.Action;
            await _actionTakenService.SaveChangesAsync();
        }
    }
}