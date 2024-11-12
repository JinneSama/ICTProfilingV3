using DevExpress.XtraEditors;
using ICTProfilingV3.CustomerActionSheetForms;
using ICTProfilingV3.DeliveriesForms;
using ICTProfilingV3.PurchaseRequestForms;
using ICTProfilingV3.RepairForms;
using ICTProfilingV3.TechSpecsForms;
using Models.Entities;
using Models.Enums;
using Models.Managers.User;
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
        public UCRoutedActions()
        {
            InitializeComponent();
            unitOfWork = new UnitOfWork();
            LoadRoutedActions();
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
                filterText = row.Actions.RepairId.ToString()
            });

            if (row.Actions.RequestType == RequestType.PR) NavigateToProcess(new UCPR()
            {
                Dock = DockStyle.Fill,
                filterText = row.Actions.RepairId.ToString()
            });
        }

        private void NavigateToProcess(Control uc)
        {
            var main = Application.OpenForms["frmMain"] as frmMain;
            main.mainPanel.Controls.Clear();

            main.mainPanel.Controls.Add(uc);
        }
    }
}
