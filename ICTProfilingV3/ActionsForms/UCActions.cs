using Models.Entities;
using Models.Enums;
using Models.Managers.User;
using Models.Models;
using Models.Repository;
using Models.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ICTProfilingV3.ActionsForms
{
    public partial class UCActions : DevExpress.XtraEditors.XtraUserControl
    {
        private IUnitOfWork unitOfWork;
        private ActionType _actionType;
        public UCActions(ActionType actionType)
        {
            InitializeComponent();
            unitOfWork = new UnitOfWork();
            _actionType = actionType;
            LoadActions();
        }

        private async Task<IEnumerable<Actions>> GetActions()
        {
            if (_actionType.RequestType == RequestType.Deliveries) 
                return (await unitOfWork.DeliveriesRepo.FindAsync(x => x.Id == _actionType.Id,
                    x => x.Actions.Select(a => a.CreatedBy),
                    x => x.Actions.Select(a => a.ProgramDropdowns),
                    x => x.Actions.Select(a => a.ActivityDropdowns),
                    x => x.Actions.Select(a => a.MainActDropdowns),
                    x => x.Actions.Select(a => a.SubActivityDropdowns),
                    x => x.Actions.Select(a => a.RoutedUsers))).Actions.ToList();
            
            if(_actionType.RequestType == RequestType.TechSpecs)
                return (await unitOfWork.TechSpecsRepo.FindAsync(x => x.Id == _actionType.Id,
                   x => x.Actions.Select(a => a.CreatedBy),
                   x => x.Actions.Select(a => a.ProgramDropdowns),
                   x => x.Actions.Select(a => a.ActivityDropdowns),
                   x => x.Actions.Select(a => a.MainActDropdowns),
                   x => x.Actions.Select(a => a.SubActivityDropdowns),
                   x => x.Actions.Select(a => a.RoutedUsers))).Actions.ToList();

            if (_actionType.RequestType == RequestType.Repairs)
                return (await unitOfWork.RepairsRepo.FindAsync(x => x.Id == _actionType.Id,
                   x => x.Actions.Select(a => a.CreatedBy),
                   x => x.Actions.Select(a => a.ProgramDropdowns),
                   x => x.Actions.Select(a => a.ActivityDropdowns),
                   x => x.Actions.Select(a => a.MainActDropdowns),
                   x => x.Actions.Select(a => a.SubActivityDropdowns),
                   x => x.Actions.Select(a => a.RoutedUsers))).Actions.ToList();
            return null;
        }
        private async void LoadActions()
        {
            var actions = await GetActions();
            if (actions == null) return;
            var actionsModel = actions.Select(x => new ActionsViewModel
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
                IsSend = x.IsSend
            }).OrderByDescending(o => o.ActionDate);
            gcActions.DataSource = new BindingList<ActionsViewModel>(actionsModel.ToList());
        }


        private void btnAddAction_Click(object sender, System.EventArgs e)
        {
            var frm = new frmDocAction(_actionType ,SaveType.Insert , null ,unitOfWork);
            frm.ShowDialog();

            LoadActions();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            var row = (ActionsViewModel)gridActions.GetFocusedRow();
            if (row.CreatedById != UserStore.UserId)
            {
                MessageBox.Show("Cannot Edit an Action created by other Users!");
                return;
            }
            var frm = new frmDocAction(_actionType, SaveType.Update,row, unitOfWork);
            frm.ShowDialog();

            LoadActions();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var row = (ActionsViewModel)gridActions.GetFocusedRow();
            if (row.CreatedById == UserStore.UserId)
            {
                if (MessageBox.Show("Delete this Action?", "Confirmation", MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Exclamation) == DialogResult.Cancel) return;

                unitOfWork.ActionsRepo.DeleteByEx(x => x.Id == row.Id);
                unitOfWork.Save();
            }
            else
                MessageBox.Show("Cannot Delete an Action created by other Users!");

            LoadActions();
        }
    }
}
