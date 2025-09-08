using ICTProfilingV3.BaseClasses;
using ICTProfilingV3.DataTransferModels;
using ICTProfilingV3.DataTransferModels.Models;
using ICTProfilingV3.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Models.Entities;
using Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ICTProfilingV3.ActionsForms
{
    public partial class frmDocAction : BaseForm
    {
        private ActionType _actionType;
        private SaveType _saveType;
        private ActionsDTM _actions;
        private List<UsersDTM> _routedUsers;

        private readonly IDocActionsService _docActService;
        private readonly IServiceProvider _serviceProvider;
        private readonly IProcessService _processService;
        private readonly IControlMapper<ActionDTM> _controlMapper;

        private bool saveImages = false;

        public frmDocAction(IServiceProvider serviceProvider, IDocActionsService doctActService, IControlMapper<ActionDTM> controlMapper,
            IProcessService processService)
        {
            _serviceProvider = serviceProvider;
            _docActService = doctActService;
            _controlMapper = controlMapper;
            _processService = processService;

            InitializeComponent();
        }

        public void SetActionBehavior(ActionType actionType, SaveType saveType, ActionsDTM actions, Users user)
        {
            _actionType = actionType;
            _saveType = saveType;
            _actions = actions;
            LoadDropdown();
        }

        private async Task LoadDetails(int actionId)
        {
            var data = await _docActService.GetDocAction(actionId);
            _controlMapper.MapControl(data, this);
            _routedUsers = data.RoutedUsersObject;
        }

        private async Task LoadActionList()
        {
            var data = _docActService.GetActionTakenList().ToList();
            lueActionTaken.Properties.DataSource = data;
            await LoadTicketStatus();
        }
        #region TreeDropdowns
        private void LoadDropdown()
        {
            deActionDate.DateTime = DateTime.Now;
            lueProgram.Properties.DataSource = _docActService.GetActionTree(null);
            lueTicketStatus.Properties.DataSource = Enum.GetValues(typeof(TicketStatus)).Cast<TicketStatus>().Select(x => new { Type = x, TypeView = EnumHelper.GetEnumDescription(x) });
        }

        private void lueProgram_EditValueChanged(object sender, EventArgs e)
        {
            var row = (ActionTreeDTM)lueProgram.GetSelectedDataRow();
            if (row == null) return;
            lueMainActivity.Properties.DataSource = _docActService.GetActionTree(row.ActionTree.Id);
        }

        private void lueActivity_EditValueChanged(object sender, EventArgs e)
        {
            var row = (ActionTreeDTM)lueActivity.GetSelectedDataRow();
            if (row == null) return;
            lueSubActivity.Properties.DataSource = _docActService.GetActionTree(row.ActionTree.Id);
        }

        private void lueMainActivity_EditValueChanged(object sender, EventArgs e)
        {
            var row = (ActionTreeDTM)lueMainActivity.GetSelectedDataRow();
            if (row == null) return;
            lueActivity.Properties.DataSource = _docActService.GetActionTree(row.ActionTree.Id);
        }
        #endregion

        private async void btnNewActionTaken_Click(object sender, EventArgs e)
        {
            var frm = _serviceProvider.GetService<frmActionList>();
            frm.ShowDialog();
            await LoadActionList();
        }

        private void btnRouteTo_Click(object sender, EventArgs e)
        {
            var row = (ActionTreeDTM)lueMainActivity.GetSelectedDataRow();
            var frm = _serviceProvider.GetRequiredService<frmRouteToUsers>();
            frm.SetActionDetails(row, _saveType, _routedUsers);
            frm.ShowDialog();

            var routedUsers = frm._routedUsers;
            _routedUsers = routedUsers;
            txtRoutedTo.Text = string.Join(", ",routedUsers.Select(x => x.Fullname));
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void btnSaveAndClose_Click(object sender, EventArgs e)
        {
            var routedCount = _routedUsers?.Count ?? 0;
            if (routedCount == 0)
            {
                await Save(false);
            }
            else
            {
                if (MessageBox.Show("This action has been routed and will now be sent.", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                    await Save(true);
            }
        }

        private async void btnSaveAndSend_Click(object sender, EventArgs e)
        {
            await Save(true);
        }

        private async Task Save(bool send)
        {
            if (_routedUsers == null) send = false;
            var actionDTM = new ActionDTM();
            _controlMapper.MapToEntity(actionDTM, this);

            if (_saveType == SaveType.Insert)
            {
                await _docActService.AddDocAction(send, _actionType.RequestType, actionDTM,
                    _routedUsers, _actionType.Id);
            }
            else
            {
                await _docActService.UpdateDocAction(_actions.Id, send, _actionType.RequestType, actionDTM,
                    _routedUsers, _actionType.Id);
            }
            await UpdateTicketStatus();
            Close();
        }

        private async Task UpdateTicketStatus()
        {
            await _processService.UpdateProcessStatus(_actionType.Id, _actionType.RequestType, (TicketStatus)lueTicketStatus.EditValue);
            await _processService.AddProcessLog(_actionType.Id, _actionType.RequestType, (TicketStatus)lueTicketStatus.EditValue);
        }

        private async Task LoadTicketStatus()
        {
            var status = await _processService.GetProcessStatus(_actionType.Id, _actionType.RequestType);
            if (status == null) return;
            lueTicketStatus.EditValue = status;
        }

        private async void frmDocAction_Load(object sender, EventArgs e)
        {
            await LoadActionList();

            if (_actions != null)
                await LoadDetails(_actions.Id);
        }

        private async void btnLastAction_Click(object sender, EventArgs e)
        {
            var action = await _docActService.GetLastAction(_actionType.Id);
            lueProgram.EditValue = action.ProgramId;
            lueMainActivity.EditValue = action.MainActId;
            lueActivity.EditValue = action.ActivityId;
            lueSubActivity.EditValue = action.SubActivityId;
            lueActionTaken.Text = action.ActionTaken;
        }
    }
}