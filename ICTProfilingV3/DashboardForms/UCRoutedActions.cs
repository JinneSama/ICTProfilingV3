using DevExpress.Data.Filtering;
using Helpers.Interfaces;
using ICTProfilingV3.ActionsForms;
using ICTProfilingV3.Core.Common;
using ICTProfilingV3.CustomerActionSheetForms;
using ICTProfilingV3.DataTransferModels.Models;
using ICTProfilingV3.DataTransferModels.ViewModels;
using ICTProfilingV3.DeliveriesForms;
using ICTProfilingV3.Interfaces;
using ICTProfilingV3.MOForms;
using ICTProfilingV3.PGNForms;
using ICTProfilingV3.PurchaseRequestForms;
using ICTProfilingV3.RepairForms;
using ICTProfilingV3.Services.ApiUsers;
using ICTProfilingV3.TechSpecsForms;
using Microsoft.Extensions.DependencyInjection;
using Models.Entities;
using Models.Enums;
using Models.Models;
using Models.Repository;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace ICTProfilingV3.DashboardForms
{
    public partial class UCRoutedActions : DevExpress.XtraEditors.XtraUserControl, IDisposeUC
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IICTUserManager userManager;
        private readonly IServiceProvider _serviceProvider;
        private readonly UserStore _userStore;
        public UCRoutedActions(IServiceProvider serviceProvider, UserStore userStore)
        {
            InitializeComponent();
            unitOfWork = new UnitOfWork();
            userManager = new ICTUserManager();
            _serviceProvider = serviceProvider;
            _userStore = userStore;
            LoadDropdowns();
            LoadRoutedActions();
        }

        private void LoadDropdowns()
        {
            var users = userManager.GetUsers().ToList();
            slueTaskOf.Properties.DataSource = users;

            lueProcessType.Properties.DataSource = Enum.GetValues(typeof(RequestType)).Cast<RequestType>().Select(x => new
            {
                Id = x,
                Value = EnumHelper.GetEnumDescription(x)
            });
        }

        private void LoadRoutedActions()
        {
            var actions = unitOfWork.ActionsRepo.FindAllAsync(x => x.RoutedUsers.Any(r => r.Id == _userStore.UserId) && x.IsSend == true,
                x => x.Repairs,
                x => x.Repairs.TicketRequest,
                x => x.Deliveries.TicketRequest,
                x => x.TechSpecs.TicketRequest,
                x => x.MOAccountUsers,
                x => x.CustomerActionSheet,
                x => x.PurchaseRequest,
                x => x.PGNRequests).ToList();
            var actionsModel = actions.Select(x => new RoutedActionsViewModel
            {
                Id = x.Id,
                ActionDate = x.ActionDate,
                RoutedTo = string.Join(",", x.RoutedUsers.Select(s => s.FullName)),
                Remarks = x.Remarks,
                Actions = x,
                From = x.CreatedBy.UserName
            }).OrderByDescending(o => o.ActionDate).ToList();
            gcRoutedActions.DataSource = actionsModel;
            FilterGrid();
        }

        private void hplControlNo_Click(object sender, EventArgs e)
        {
            var mainForm = _serviceProvider.GetRequiredService<frmMain>();
            var row = (RoutedActionsViewModel)gridRoutedActions.GetFocusedRow();
            if (row.Actions.RequestType == RequestType.TechSpecs)
            {
                var navigation = _serviceProvider.GetRequiredService<IControlNavigator<UCTechSpecs>>();
                navigation.NavigateTo(mainForm.mainPanel, act =>
                {
                    act.filterText = row.Actions.DeliveriesId.ToString();
                    act.IsTechSpecs = true;
                });
            }

            if (row.Actions.RequestType == RequestType.Deliveries)
            {
                var navigation = _serviceProvider.GetRequiredService<IControlNavigator<UCDeliveries>>();
                navigation.NavigateTo(mainForm.mainPanel, act => act.filterText = row.Actions.DeliveriesId.ToString());
            }

            if (row.Actions.RequestType == RequestType.Repairs) 
            {
                var navigation = _serviceProvider.GetRequiredService<IControlNavigator<UCRepair>>();
                navigation.NavigateTo(mainForm.mainPanel, act => act.filterText = row.Actions.RepairId.ToString());
            };

            if (row.Actions.RequestType == RequestType.CAS)
            {
                var navigation = _serviceProvider.GetRequiredService<IControlNavigator<UCCAS>>();
                navigation.NavigateTo(mainForm.mainPanel, act => act.filterText = row.Actions.CustomerActionSheetId.ToString());
            }

            if (row.Actions.RequestType == RequestType.PR)
            {
                var navigation = _serviceProvider.GetRequiredService<IControlNavigator<UCPR>>();
                navigation.NavigateTo(mainForm.mainPanel, act => act.filterText = row.Actions.PurchaseRequestId.ToString());
            }

            if (row.Actions.RequestType == RequestType.PGN)
            {
                var navigation = _serviceProvider.GetRequiredService<IControlNavigator<UCPGNRequests>>();
                navigation.NavigateTo(mainForm.mainPanel, act => act.filterText = row.Actions.PGNRequestId.ToString());
            }

            if (row.Actions.RequestType == RequestType.M365)
            {
                var navigation = _serviceProvider.GetRequiredService<IControlNavigator<UCMOAccountUserRequests>>();
                navigation.NavigateTo(mainForm.mainPanel, act => act.filterText = row.Actions.MOAccountUserId.ToString());
            }
        }

        private void LoadActions(ActionType actionType)
        {
            gcActions.Controls.Clear();
            var uc = _serviceProvider.GetRequiredService<UCActions>();
            uc.setActions(actionType);
            uc.Dock = System.Windows.Forms.DockStyle.Fill;
            gcActions.Controls.Add(uc);
        }

        private void gridRoutedActions_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            var row = (RoutedActionsViewModel)gridRoutedActions.GetFocusedRow();
            if (row == null) 
            {
                gcActions.Controls.Clear();
                return;
            }
            LoadActions(new ActionType
            {
                RequestType = row.Actions.RequestType,
                Id = GetActionId(row) ?? 0
            });
        }

        private int? GetActionId(RoutedActionsViewModel actions)
        {
            int? Id = null;
            switch (actions.Actions.RequestType)
            {
                case RequestType.TechSpecs:
                    Id = actions.Actions.TechSpecsId;
                    break;
                case RequestType.Deliveries:
                    Id = actions.Actions.DeliveriesId;
                    break;
                case RequestType.Repairs:
                    Id = actions.Actions.RepairId;
                    break;
                case RequestType.PR:
                    Id = actions.Actions.PurchaseRequestId;
                    break;
                case RequestType.CAS:
                    Id = actions.Actions.CustomerActionSheetId;
                    break;
                case RequestType.PGN:
                    Id = actions.Actions.PGNRequestId;
                    break;
                case RequestType.M365:
                    Id = actions.Actions.MOAccountUserId;
                    break;
                default:
                    break;
            }
            return Id;
        }

        private void btnFilterbyDate_Click(object sender, EventArgs e)
        {
            FilterGrid();
        }

        private void FilterGrid()
        {
            gridRoutedActions.ActiveFilterCriteria = null;

            var row = (Users)slueTaskOf.Properties.View.GetFocusedRow();
            var process = lueProcessType.EditValue;
            var ctrlNo = spinCtrlNo.Value;
            var dateFrom = deFrom.DateTime;
            var dateTo = deTo.DateTime;

            var criteria = gridRoutedActions.ActiveFilterCriteria;
            if (lueProcessType.EditValue != null) criteria = GroupOperator.And(criteria, new BinaryOperator("ProcessType", EnumHelper.GetEnumDescription((RequestType)process)));
            if(slueTaskOf.EditValue != null) criteria = GroupOperator.And(criteria, new BinaryOperator("From", row.UserName));
            if(ctrlNo != 0) 
                criteria = GroupOperator.And(criteria,new BinaryOperator("ControlNo",ctrlNo));

            if(ceCompleted.Checked == true) 
                criteria = GroupOperator.And(criteria, GroupOperator.Or(new BinaryOperator("Completed", false), new BinaryOperator("Completed", true)));

            if (ceCompleted.Checked == false)
                criteria = GroupOperator.And(criteria,new BinaryOperator("Completed", false));

            if (deFrom.EditValue != null && deTo.EditValue != null)
            {
                var fromFilter = new BinaryOperator("ActionDate", dateFrom, BinaryOperatorType.GreaterOrEqual);
                var toFilter = new BinaryOperator("ActionDate", dateTo, BinaryOperatorType.LessOrEqual);
                criteria = GroupOperator.And(criteria,GroupOperator.And(fromFilter, toFilter));
            }

            gridRoutedActions.ActiveFilterCriteria = criteria;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            slueTaskOf.EditValue = null;
            lueProcessType.EditValue = null;
            deFrom.EditValue = null;
            deTo.EditValue = null;
            spinCtrlNo.Value = 0;
            gridRoutedActions.ActiveFilterCriteria = null;
        }

        private void slueTaskOf_EditValueChanged(object sender, EventArgs e)
        {
            FilterGrid();
        }

        private void lueProcessType_EditValueChanged(object sender, EventArgs e)
        {
            FilterGrid();
        }

        private void spinCtrlNo_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) FilterGrid();
        }

        public void DisposeUC(Control parent)
        {
            foreach (Control ctrl in parent.Controls)
            {
                ctrl.Dispose();
                GC.Collect();
            }
            parent.Controls.Clear();
        }

        private void ceCompleted_CheckedChanged(object sender, EventArgs e)
        {
            FilterGrid();
        }
    }
}
