using ICTProfilingV3.Core.Common;
using ICTProfilingV3.DataTransferModels;
using ICTProfilingV3.DataTransferModels.Models;
using ICTProfilingV3.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Models.Enums;
using System;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Windows.Forms;

namespace ICTProfilingV3.ActionsForms
{
    public partial class UCActions : DevExpress.XtraEditors.XtraUserControl
    {
        private ActionType _actionType;
        private readonly IServiceProvider _serviceProvider;
        private readonly IDocActionsService _docActionsService;
        private readonly UserStore _userStore;

        public UCActions(IServiceProvider serviceProvider, IDocActionsService docActionsService, UserStore userStore)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
            _docActionsService = docActionsService;
            _userStore = userStore;
        }
        public void setActions(ActionType actionType)
        {
            _actionType = actionType;
            LoadActions();
        }

        private void LoadActions()
        {
            var actions = _docActionsService.GetActionsByDTM(_actionType.RequestType, _actionType.Id);
            if (actions == null) return;
            var actionLists = actions.ToList();
            gcActions.DataSource = new BindingList<ActionsDTM>(actionLists);
        }


        private void btnAddAction_Click(object sender, System.EventArgs e)
        {
            var frm = _serviceProvider.GetRequiredService<frmDocAction>();
            frm.SetActionBehavior(_actionType ,SaveType.Insert , null ,null);
            frm.ShowDialog();

            LoadActions();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            var row = (ActionsDTM)gridActions.GetFocusedRow();
            if (row.CreatedById != _userStore.UserId)
            {
                MessageBox.Show("Cannot Edit an Action created by other Users!");
                return;
            }
            var frm = _serviceProvider.GetRequiredService<frmDocAction>();
            frm.SetActionBehavior(_actionType, SaveType.Update,row,null);
            frm.ShowDialog();

            LoadActions();
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            var row = (ActionsDTM)gridActions.GetFocusedRow();
            if (row.CreatedById == _userStore.UserId)
            {
                if (MessageBox.Show("Delete this Action?", "Confirmation", MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Exclamation) == DialogResult.Cancel) return;

                await _docActionsService.DeleteDocAction(row.Actions.Id);
            }
            else
                MessageBox.Show("Cannot Delete an Action created by other Users!");

            LoadActions();
        }

        private void btnImages_Click(object sender, EventArgs e)
        {
            var row = (ActionsDTM)gridActions.GetFocusedRow();
            var frm = _serviceProvider.GetRequiredService<frmActionDocuments>();
            frm.SetAction(row.Actions);
            frm.ShowDialog();
        }

        private void gridActions_CustomRowCellEdit(object sender, DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventArgs e)
        {
            if (e.Column.Name == "colAct")
            {
                var row = (ActionsDTM)gridActions.GetRow(e.RowHandle);
                e.Column.OptionsColumn.AllowEdit = row.RoutedToSelf ?? false;
                e.Column.OptionsColumn.AllowFocus = row.RoutedToSelf ?? false;
                if (row.RoutedToSelf == true)
                    e.RepositoryItem = btnMarkRoute;
                else
                    e.RepositoryItem = null;
            }
        }

        private void btnMarkRoute_Click(object sender, EventArgs e)
        {
            var row = (ActionsDTM)gridActions.GetFocusedRow();
            var frm = _serviceProvider.GetRequiredService<frmMarkRoutedAction>();
            frm.InitForm(row.Id);
            frm.ShowDialog();

            LoadActions();
        }
    }
}
