using DevExpress.Data.Filtering;
using DevExpress.XtraEditors;
using EntityManager.Managers.User;
using ICTProfilingV3.ActionsForms;
using ICTProfilingV3.CustomerActionSheetForms;
using ICTProfilingV3.DeliveriesForms;
using ICTProfilingV3.MOForms;
using ICTProfilingV3.PGNForms;
using ICTProfilingV3.PurchaseRequestForms;
using ICTProfilingV3.RepairForms;
using ICTProfilingV3.TechSpecsForms;
using Models.Entities;
using Models.Enums;
using Models.Managers.User;
using Models.Models;
using Models.Repository;
using Models.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ICTProfilingV3.DashboardForms
{
    public partial class UCRoutedActions : DevExpress.XtraEditors.XtraUserControl
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IICTUserManager userManager;
        public UCRoutedActions()
        {
            InitializeComponent();
            unitOfWork = new UnitOfWork();
            userManager = new ICTUserManager();
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
            var actions = unitOfWork.ActionsRepo.FindAllAsync(x => x.RoutedUsers.Any(r => r.Id == UserStore.UserId) && x.IsSend == true,
                x => x.Repairs,
                x => x.CustomerActionSheet).ToList();
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
        }

        private void hplControlNo_Click(object sender, EventArgs e)
        {
            var row = (RoutedActionsViewModel)gridRoutedActions.GetFocusedRow();
            if (row.Actions.RequestType == RequestType.TechSpecs) NavigateToProcess(new UCTechSpecs()
            {
                IsTechSpecs = true,
                Dock = DockStyle.Fill,
                filterText = row.Actions.TechSpecsId.ToString()
            });

            if (row.Actions.RequestType == RequestType.Deliveries) NavigateToProcess(new UCDeliveries()
            {
                Dock = DockStyle.Fill,
                filterText = row.Actions.DeliveriesId.ToString()
            });

            if (row.Actions.RequestType == RequestType.Repairs) NavigateToProcess(new UCRepair()
            {
                Dock = DockStyle.Fill,
                filterText = row.Actions.RepairId.ToString()
            });

            if (row.Actions.RequestType == RequestType.CAS) NavigateToProcess(new UCCAS()
            {
                Dock = DockStyle.Fill,
                filterText = row.Actions.CustomerActionSheetId.ToString()
            });

            if (row.Actions.RequestType == RequestType.PR) NavigateToProcess(new UCPR()
            {
                Dock = DockStyle.Fill,
                filterText = row.Actions.PurchaseRequestId.ToString()
            });

            if (row.Actions.RequestType == RequestType.PGN) NavigateToProcess(new UCPGNRequests()
            {
                Dock = DockStyle.Fill,
                filterText = row.Actions.PGNRequestId.ToString()
            });

            if (row.Actions.RequestType == RequestType.M365) NavigateToProcess(new UCMOAccountUserRequests()
            {
                Dock = DockStyle.Fill,
                filterText = row.Actions.MOAccountUserId.ToString()
            });
        }

        private void LoadActions(ActionType actionType)
        {
            gcActions.Controls.Clear();
            gcActions.Controls.Add(new UCActions(actionType)
            {
                Dock = DockStyle.Fill
            });
        }

        private void NavigateToProcess(Control uc)
        {
            var main = Application.OpenForms["frmMain"] as frmMain;
            main.mainPanel.Controls.Clear();

            main.mainPanel.Controls.Add(uc);
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
            if(ctrlNo != 0) criteria = GroupOperator.And(criteria,new BinaryOperator("ControlNo",ctrlNo));
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
    }
}
