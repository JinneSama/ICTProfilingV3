using Helpers.Interfaces;
using Helpers.NetworkFolder;
using ICTProfilingV3.BaseClasses;
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
    public partial class frmDocAction : BaseForm, IModifyTicketStatus, IModifyRecordStatus
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ActionType actionType;
        private readonly SaveType saveType;
        private readonly ActionsViewModel _actions;
        private List<UsersViewModel> _routedUsers;

        private bool saveImages = false;
        public frmDocAction(ActionType _actionType, SaveType saveType, ActionsViewModel actions, IUnitOfWork uow, Users user)
        {
            InitializeComponent();
            if(uow == null) unitOfWork = new UnitOfWork();
            else unitOfWork = uow;

            actionType = _actionType;
            this.saveType = saveType;
            _actions = actions;
            LoadDropdown();
            if(user != null) SetRoutedUser(user);
        }

        private void SetRoutedUser(Users user)
        {
            var routedUsers = new List<UsersViewModel>();
            var usr = new UsersViewModel()
            {
                Id = user.Id,
                Fullname = user.FullName
            };
            routedUsers.Add(usr);
            _routedUsers = routedUsers;
            txtRoutedTo.Text = string.Join(", ", routedUsers.Select(x => x.Fullname));
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

        private async Task LoadActionList()
        {
            lueActionTaken.Properties.DataSource = unitOfWork.ActionTakenRepo.GetAll().ToList();

            await LoadTicketStatus();
        }
        private void LoadDropdown()
        {
            deActionDate.DateTime = DateTime.Now;
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
            if (row == null) return;
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
            if (row == null) return;
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
            if(row == null) return;
            var dropdown = unitOfWork.ActionsDropdownsRepo.FindAllAsync(x => x.ParentId == row.ActionTree.Id).Select(s => new ActionTreeViewModel
            {
                ActionTree = s,
                NodeValue = s.Order + "." + s.Value
            }).OrderBy(o => o.ActionTree.Order);
            lueActivity.Properties.DataSource = new BindingList<ActionTreeViewModel>(dropdown.ToList());
        }

        private async void btnNewActionTaken_Click(object sender, EventArgs e)
        {
            var frm = new frmActionList();
            frm.ShowDialog();
            await LoadActionList();
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

        private async void btnSaveAndClose_Click(object sender, EventArgs e)
        {
            saveImages = true;
            await Save(false);
        }

        private async void btnSaveAndSend_Click(object sender, EventArgs e)
        {
            saveImages = true;
            await Save(true);
        }

        private async Task Save(bool send)
        {
            if (saveType == SaveType.Insert) await InsertDocAction(send);
            else
            {
                await UpdateDocAction(send);
                this.Close();
            }
        }

        private async Task UpdateTicketStatus()
        {
            if (actionType.RequestType != RequestType.TechSpecs && actionType.RequestType != RequestType.Deliveries && actionType.RequestType != RequestType.Repairs) 
            {
                await UpdateRecordProcessStatus();
                return;
            }

            var ticket = await unitOfWork.TicketRequestRepo.FindAsync(x => x.Id == actionType.Id);
            if (ticket == null) return;

            ticket.TicketStatus = (TicketStatus)lueTicketStatus.EditValue;
            unitOfWork.TicketRequestRepo.Update(ticket);
            await unitOfWork.SaveChangesAsync();

            await ModifyTicketStatusStatus((TicketStatus)lueTicketStatus.EditValue, ticket.Id);
        }

        private async Task UpdateRecordProcessStatus()
        {
            var uow = new UnitOfWork();
            if (actionType.RequestType == RequestType.PR)
            {
                var pr = await uow.PurchaseRequestRepo.FindAsync(x => x.Id == actionType.Id);
                pr.Status = (TicketStatus)lueTicketStatus.EditValue;
                uow.PurchaseRequestRepo.Update(pr);
                await uow.SaveChangesAsync();
            }

            if (actionType.RequestType == RequestType.CAS)
            {
                var cas = await uow.CustomerActionSheetRepo.FindAsync(x => x.Id == actionType.Id);
                cas.Status = (TicketStatus)lueTicketStatus.EditValue;
                uow.CustomerActionSheetRepo.Update(cas);
                await uow.SaveChangesAsync();
            }

            if (actionType.RequestType == RequestType.M365)
            {
                var mo = await uow.MOAccountUserRepo.FindAsync(x => x.Id == actionType.Id);
                mo.Status = (TicketStatus)lueTicketStatus.EditValue;
                uow.MOAccountUserRepo.Update(mo);
                await uow.SaveChangesAsync();
            }

            if (actionType.RequestType == RequestType.PGN)
            {
                var pgn = await uow.PGNRequestsRepo.FindAsync(x => x.Id == actionType.Id);
                pgn.Status = (TicketStatus)lueTicketStatus.EditValue;
                uow.PGNRequestsRepo.Update(pgn);
                await uow.SaveChangesAsync();
            }
            await ModifyRecordStatus((TicketStatus)lueTicketStatus.EditValue, actionType.RequestType, actionType.Id);
        }

        private async Task UpdateDocAction(bool send)
        {
            if (_routedUsers.Count <= 0) send = false;
            var updateAction = await unitOfWork.ActionsRepo.FindAsync(x => x.Id == _actions.Id);
            updateAction.ActionTaken = lueActionTaken.Text;
            updateAction.DateCreated = DateTime.Now;
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
            unitOfWork.ActionsRepo.Update(updateAction);
            unitOfWork.Save();
            await UpdateTicketStatus();
        }

        private async Task InsertDocAction(bool send)
        {
            if (_routedUsers == null) send = false;
            var docAction = new Actions
            {
                ActionTaken = lueActionTaken.Text,
                DateCreated = DateTime.Now,
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
                unitOfWork.DeliveriesRepo.Update(deliveries);
                unitOfWork.Save();
            }

            if (actionType.RequestType == RequestType.Repairs)
            {
                var repair = await unitOfWork.RepairsRepo.FindAsync(x => x.Id == actionType.Id);
                repair.Actions.Add(Action);
                unitOfWork.RepairsRepo.Update(repair);
                unitOfWork.Save();
            }

            if (actionType.RequestType == RequestType.TechSpecs)
            {
                var techSpecs = await unitOfWork.TechSpecsRepo.FindAsync(x => x.Id == actionType.Id);
                techSpecs.Actions.Add(Action);
                unitOfWork.TechSpecsRepo.Update(techSpecs);
                unitOfWork.Save();
            }

            if (actionType.RequestType == RequestType.CAS)
            {
                var cas = await unitOfWork.CustomerActionSheetRepo.FindAsync(x => x.Id == actionType.Id);
                cas.Actions.Add(Action);
                unitOfWork.CustomerActionSheetRepo.Update(cas);
                unitOfWork.Save();
            }

            if (actionType.RequestType == RequestType.PR)
            {
                var pr = await unitOfWork.PurchaseRequestRepo.FindAsync(x => x.Id == actionType.Id);
                pr.Actions.Add(Action);
                unitOfWork.PurchaseRequestRepo.Update(pr);
                unitOfWork.Save();
            }

            if (actionType.RequestType == RequestType.PGN)
            {
                var pgn = await unitOfWork.PGNRequestsRepo.FindAsync(x => x.Id == actionType.Id);
                pgn.Actions.Add(Action);
                unitOfWork.PGNRequestsRepo.Update(pgn);
                unitOfWork.Save();
            }

            if (actionType.RequestType == RequestType.M365)
            {
                var mo = await unitOfWork.MOAccountUserRepo.FindAsync(x => x.Id == actionType.Id);
                mo.Actions.Add(Action);
                unitOfWork.MOAccountUserRepo.Update(mo);
                unitOfWork.Save();
            }
            SaveImages(Action);
        }

        private void SaveImages(Actions action)
        {
            var docs = unitOfWork.ActionDocumentsRepo.FindAllAsync(x => x.ActionId == null);
            foreach (var doc in docs)
            {
                doc.ActionId = action.Id;
            }
            unitOfWork.Save();
        }

        private async Task LoadTicketStatus()
        {
            if (actionType.RequestType != RequestType.TechSpecs && actionType.RequestType != RequestType.Deliveries && actionType.RequestType != RequestType.Repairs)
            {
                await LoadRecordProcessStatus();
                return;
            }
            var ticket = await unitOfWork.TicketRequestRepo.FindAsync(x => x.Id == actionType.Id);
            if (ticket == null) return;

            lueTicketStatus.EditValue = ticket.TicketStatus;
        }

        private async Task LoadRecordProcessStatus()
        {
            var uow = new UnitOfWork();
            if(actionType.RequestType == RequestType.PR)
            {
                var pr = await uow.PurchaseRequestRepo.FindAsync(x => x.Id == actionType.Id);
                lueTicketStatus.EditValue = pr.Status;
            }

            if (actionType.RequestType == RequestType.CAS)
            {
                var cas = await uow.CustomerActionSheetRepo.FindAsync(x => x.Id == actionType.Id);
                lueTicketStatus.EditValue = cas.Status;
            }

            if (actionType.RequestType == RequestType.M365)
            {
                var mo = await uow.MOAccountUserRepo.FindAsync(x => x.Id == actionType.Id);
                lueTicketStatus.EditValue = mo.Status;
            }

            if (actionType.RequestType == RequestType.PGN)
            {
                var pgn = await uow.PGNRequestsRepo.FindAsync(x => x.Id == actionType.Id);
                lueTicketStatus.EditValue = pgn.Status;
            }
        }

        private async void frmDocAction_Load(object sender, EventArgs e)
        {
            await LoadActionList();
            if (saveType == SaveType.Update) LoadDetails(_actions);
            LoadActionDocuments();
        }


        private void btnAttach_Click(object sender, EventArgs e)
        {
            if (saveType == SaveType.Update)
            {
                var frm = new frmActionDocuments(_actions.Actions);
                frm.ShowDialog();
            }
            else
            {
                var frm = new frmActionDocuments();
                frm.ShowDialog();
            }
            LoadActionDocuments();
        }

        private async void frmDocAction_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            if (saveType == SaveType.Update) return;
            if (saveImages) return;

            var res = unitOfWork.ActionDocumentsRepo.FindAllAsync(x => x.ActionId == null).ToList();
            if(res == null) return;

            HTTPNetworkFolder networkFolder = new HTTPNetworkFolder();
            foreach (var item in res)
            {
                await networkFolder.DeleteFile(item.DocumentName);
            }

            unitOfWork.ActionDocumentsRepo.DeleteRange(x => x.ActionId == null);
            unitOfWork.Save();
        }

        private void LoadActionDocuments()
        {
            int? actionId = _actions?.Id ?? null;
            var res = unitOfWork.ActionDocumentsRepo.FindAllAsync(x => x.ActionId == actionId);
            txtAttachFiles.Text = string.Join("," , res.Select(s => s.DocumentName));
        }

        public async Task ModifyTicketStatusStatus(TicketStatus status, int Id)
        {
            var ticketStatus = new TicketRequestStatus
            {
                Status = status,
                DateStatusChanged = DateTime.Now,
                ChangedByUserId = UserStore.UserId,
                TicketRequestId = Id
            };
            unitOfWork.TicketRequestStatusRepo.Insert(ticketStatus);
            await unitOfWork.SaveChangesAsync();
        }

        public async Task ModifyRecordStatus(TicketStatus status, RequestType type, int Id)
        {
            var uow = new UnitOfWork();
            var recordStatus = new RecordsRequestStatus
            {
                Status = status,
                DateStatusChanged = DateTime.Now,
                ChangedByUserId = UserStore.UserId
            };

            if (type == RequestType.PR) recordStatus.PRId = Id;
            if (type == RequestType.CAS) recordStatus.CASId = Id;
            if (type == RequestType.M365) recordStatus.MOId = Id;
            if (type == RequestType.PGN) recordStatus.PGNId = Id;

            uow.RecordsRequestStatus.Insert(recordStatus);
            await uow.SaveChangesAsync();
        }
    }
}