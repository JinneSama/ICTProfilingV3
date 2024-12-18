using EntityManager.Managers.User;
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

namespace ICTProfilingV3.ActionsForms
{
    public partial class frmDocAction : DevExpress.XtraEditors.XtraForm
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ActionType actionType;
        private readonly SaveType saveType;
        private readonly ActionsViewModel _actions;
        private List<UsersViewModel> _routedUsers;
        public frmDocAction(ActionType _actionType, SaveType saveType, ActionsViewModel actions, IUnitOfWork uow)
        {
            InitializeComponent();
            if(uow == null) unitOfWork = new UnitOfWork();
            else unitOfWork = uow;

            actionType = _actionType;
            this.saveType = saveType;
            _actions = actions;
            LoadDropdown();
            LoadActionList();
        }

        private void LoadDetails(ActionsViewModel actions)
        {
            deActionDate.DateTime = actions.ActionDate.Value;
            lueProgram.EditValue = actions.Actions.ProgramId;
            lueMainActivity.EditValue = actions.Actions.MainActId;
            lueActivity.EditValue = actions.Actions.ActivityId;
            lueSubActivity.EditValue = actions.Actions.SubActivityId;
            lueActionTaken.EditValue = actions.Actions.ActionTaken;
            txtRoutedTo.Text = actions.RoutedTo;
            txtRemarks.Text = actions.Remarks;

            List<UsersViewModel> users = actions.Actions.RoutedUsers.Select(x => new UsersViewModel
            {
                Id = x.Id,
                Username = x.UserName,
                Fullname = x.FullName
            }).ToList();
            _routedUsers = users;
        }

        private async void LoadActionList()
        {
            lueActionTaken.Properties.DataSource = unitOfWork.ActionTakenRepo.GetAll().ToList();

            await LoadTicketStatus();
        }
        private void LoadDropdown()
        {
            deActionDate.DateTime = DateTime.UtcNow;
            var dropdown = unitOfWork.ActionsDropdownsRepo.FindAllAsync(x => x.ActionCategory == ActionCategory.Programs).Select(s => new ActionTreeViewModel
            {
                ActionTree = s,
                NodeValue = s.Order + "." + s.Value
            }).OrderBy(o => o.ActionTree.Order);
            lueProgram.Properties.DataSource = new BindingList<ActionTreeViewModel>(dropdown.ToList());
            lueTicketStatus.Properties.DataSource = Enum.GetValues(typeof(TicketStatus)).Cast<TicketStatus>().Select(x => new { Type = x, TypeView = EnumHelper.GetEnumDescription(x) });
        }

        private void lueProgram_EditValueChanged(object sender, EventArgs e)
        {
            var row = (ActionTreeViewModel)lueProgram.GetSelectedDataRow();
            var dropdown = unitOfWork.ActionsDropdownsRepo.FindAllAsync(x => x.ParentId == row.ActionTree.Id).Select(s => new ActionTreeViewModel
            {
                ActionTree = s,
                NodeValue = s.Order + "." + s.Value
            }).OrderBy(o => o.ActionTree.Order);
            lueMainActivity.Properties.DataSource = new BindingList<ActionTreeViewModel>(dropdown.ToList());
        }

        private void lueActivity_EditValueChanged(object sender, EventArgs e)
        {
            var row = (ActionTreeViewModel)lueActivity.GetSelectedDataRow();
            var dropdown = unitOfWork.ActionsDropdownsRepo.FindAllAsync(x => x.ParentId == row.ActionTree.Id).Select(s => new ActionTreeViewModel
            {
                ActionTree = s,
                NodeValue = s.Order + "." + s.Value
            }).OrderBy(o => o.ActionTree.Order);
            lueSubActivity.Properties.DataSource = new BindingList<ActionTreeViewModel>(dropdown.ToList());
        }

        private void lueMainActivity_EditValueChanged(object sender, EventArgs e)
        {
            var row = (ActionTreeViewModel)lueMainActivity.GetSelectedDataRow();
            var dropdown = unitOfWork.ActionsDropdownsRepo.FindAllAsync(x => x.ParentId == row.ActionTree.Id).Select(s => new ActionTreeViewModel
            {
                ActionTree = s,
                NodeValue = s.Order + "." + s.Value
            }).OrderBy(o => o.ActionTree.Order);
            lueActivity.Properties.DataSource = new BindingList<ActionTreeViewModel>(dropdown.ToList());
        }

        private void btnNewActionTaken_Click(object sender, EventArgs e)
        {
            var frm = new frmActionList();
            frm.ShowDialog();
            LoadActionList();
        }

        private void btnRouteTo_Click(object sender, EventArgs e)
        {
            var row = (ActionTreeViewModel)lueMainActivity.GetSelectedDataRow();
            var frm = new frmRouteToUsers(row , SaveType.Insert , _routedUsers);
            frm.ShowDialog();

            var routedUsers = frm._routedUsers;
            _routedUsers = routedUsers;
            txtRoutedTo.Text = string.Join(", ",routedUsers.Select(x => x.Fullname));
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSaveAndClose_Click(object sender, EventArgs e)
        {
            Save(false);
        }

        private void btnSaveAndSend_Click(object sender, EventArgs e)
        {
            Save(true);
        }

        private async void Save(bool send)
        {
            if (saveType == SaveType.Insert) InsertDocAction(send);
            else
            {
                await UpdateDocAction(send);
                this.Close();
            }
        }

        private async Task UpdateTicketStatus()
        {
            if (actionType.RequestType != RequestType.TechSpecs && actionType.RequestType != RequestType.Deliveries && actionType.RequestType != RequestType.Repairs) return;
                
            var ticket = await unitOfWork.TicketRequestRepo.FindAsync(x => x.Id == actionType.Id);
            if (ticket == null) return;

            ticket.TicketStatus = (TicketStatus)lueTicketStatus.EditValue;
            unitOfWork.Save();
        }

        private async Task UpdateDocAction(bool send)
        {
            var updateAction = await unitOfWork.ActionsRepo.FindAsync(x => x.Id == _actions.Id);
            updateAction.ActionTaken = lueActionTaken.Text;
            updateAction.DateCreated = DateTime.UtcNow;
            updateAction.ActionDate = deActionDate.DateTime;
            updateAction.Remarks = txtRemarks.Text;
            updateAction.IsSend = send;
            updateAction.CreatedBy = await unitOfWork.UsersRepo.FindAsync(x => x.Id == UserStore.UserId);
            updateAction.ProgramId = (int?)lueProgram.EditValue;
            updateAction.MainActId = (int?)lueMainActivity.EditValue;
            updateAction.ActivityId = (int?)lueActivity.EditValue;
            updateAction.SubActivityId = (int?)lueSubActivity.EditValue;
            updateAction.RequestType = actionType.RequestType;
            updateAction.RoutedUsers.Clear();

            if (_routedUsers == null) _routedUsers = new List<UsersViewModel>();
            foreach (var user in _routedUsers)
            {
                var rUser = await unitOfWork.UsersRepo.FindAsync(x => x.Id == user.Id);
                updateAction.RoutedUsers.Add(rUser);
            }
            unitOfWork.Save();
            await UpdateTicketStatus();
        }

        private async void InsertDocAction(bool send)
        {
            var docAction = new Actions
            {
                ActionTaken = lueActionTaken.Text,
                DateCreated = DateTime.UtcNow,
                ActionDate = deActionDate.DateTime,
                Remarks = txtRemarks.Text,
                IsSend = send,
                CreatedBy = await unitOfWork.UsersRepo.FindAsync(x => x.Id == UserStore.UserId),
                ProgramId = (int?)lueProgram.EditValue,
                MainActId = (int?)lueMainActivity.EditValue,
                ActivityId = (int?)lueActivity.EditValue,
                SubActivityId = (int?)lueSubActivity.EditValue,
                RequestType = actionType.RequestType
            };

            if (_routedUsers == null) _routedUsers = new List<UsersViewModel>();
            foreach (var user in _routedUsers)
            {
                var rUser = await unitOfWork.UsersRepo.FindAsync(x => x.Id == user.Id);
                docAction.RoutedUsers.Add(rUser);
            }
            await SaveToProcess(docAction);
            await UpdateTicketStatus();
            this.Close();
        }

        private async Task SaveToProcess(Actions Action)
        {
            if (actionType.RequestType == RequestType.Deliveries)
            {
                var deliveries = await unitOfWork.DeliveriesRepo.FindAsync(x => x.Id == actionType.Id);
                deliveries.Actions.Add(Action);
                unitOfWork.Save();
            }

            if (actionType.RequestType == RequestType.Repairs)
            {
                var repair = await unitOfWork.RepairsRepo.FindAsync(x => x.Id == actionType.Id);
                repair.Actions.Add(Action);
                unitOfWork.Save();
            }

            if (actionType.RequestType == RequestType.TechSpecs)
            {
                var techSpecs = await unitOfWork.TechSpecsRepo.FindAsync(x => x.Id == actionType.Id);
                techSpecs.Actions.Add(Action);
                unitOfWork.Save();
            }

            if (actionType.RequestType == RequestType.CAS)
            {
                var cas = await unitOfWork.CustomerActionSheetRepo.FindAsync(x => x.Id == actionType.Id);
                cas.Actions.Add(Action);
                unitOfWork.Save();
            }

            if (actionType.RequestType == RequestType.PR)
            {
                var pr = await unitOfWork.PurchaseRequestRepo.FindAsync(x => x.Id == actionType.Id);
                pr.Actions.Add(Action);
                unitOfWork.Save();
            }

            if (actionType.RequestType == RequestType.PGN)
            {
                var pgn = await unitOfWork.PGNRequestsRepo.FindAsync(x => x.Id == actionType.Id);
                pgn.Actions.Add(Action);
                unitOfWork.Save();
            }

            if (actionType.RequestType == RequestType.M365)
            {
                var mo = await unitOfWork.MOAccountUserRepo.FindAsync(x => x.Id == actionType.Id);
                mo.Actions.Add(Action);
                unitOfWork.Save();
            }
        }

        private async Task LoadTicketStatus()
        {
            if (actionType.RequestType != RequestType.TechSpecs && actionType.RequestType != RequestType.Deliveries && actionType.RequestType != RequestType.Repairs)
            {
                lueTicketStatus.Enabled = false;
                return;
            }
            var ticket = await unitOfWork.TicketRequestRepo.FindAsync(x => x.Id == actionType.Id);
            if (ticket == null) return;

            lueTicketStatus.EditValue = ticket.TicketStatus;
        }

        private void frmDocAction_Load(object sender, EventArgs e)
        {
            if (saveType == SaveType.Update) LoadDetails(_actions);
        }
    }
}