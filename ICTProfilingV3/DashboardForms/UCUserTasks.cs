using DevExpress.Data.Filtering;
using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Tile;
using DevExpress.XtraGrid.Views.Tile.ViewInfo;
using EntityManager.Managers.Role;
using EntityManager.Managers.User;
using ICTProfilingV3.ActionsForms;
using ICTProfilingV3.DeliveriesForms;
using ICTProfilingV3.RepairForms;
using ICTProfilingV3.TechSpecsForms;
using Models.Entities;
using Models.Enums;
using Models.Managers.User;
using Models.Models;
using Models.Repository;
using Models.ViewModels;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ICTProfilingV3.DashboardForms
{
    public partial class UCUserTasks : DevExpress.XtraEditors.XtraUserControl
    {
        private IUnitOfWork unitOfWork;
        private readonly IICTUserManager userManager;
        private readonly IICTRoleManager roleManager;
        public bool FromQueue { get; set; } = false;
        public UCUserTasks()
        {
            InitializeComponent();
            userManager = new ICTUserManager();
            roleManager = new ICTRoleManager();
            unitOfWork = new UnitOfWork();
            InitKanban();
            LoadDropdowns();
        }

        private void LoadDropdowns()
        {
            var data = Enum.GetValues(typeof(RequestType)).Cast<RequestType>().Where(x => x == RequestType.TechSpecs || x == RequestType.Deliveries || x == RequestType.Repairs).Select(x => new
            {
                Id = x,
                Value = EnumHelper.GetEnumDescription(x)
            }).ToList();

            data.Add(new
            {
                Id = RequestType.PR,
                Value = "(All)"
            });
            lueProcessType.Properties.DataSource = data;
        }
        private void LoadActions(ActionType actionType)
        {
            fpActions.Controls.Clear();
            fpActions.Controls.Add(new UCActions(actionType)
            {
                Dock = DockStyle.Fill
            });
        }
        private async Task CheckRole()
        {
            if (FromQueue)
            {
                await LoadData(false,null);
                tileTasks.OptionsDragDrop.AllowDrag = false;
                return;
            }
            tileTasks.OptionsDragDrop.AllowDrag = true;

            var user = await userManager.FindUserAsync(UserStore.UserId);
            if (user.Roles == null)
                await LoadData(false, x => x.IsRepairTechSpecs == null && x.ITStaff.UserId == UserStore.UserId);

            var role = await roleManager.GetRoleDesignations(user.Roles.FirstOrDefault().RoleId);
            if (role == null)
                await LoadData(false, x => x.IsRepairTechSpecs == null && x.ITStaff.UserId == UserStore.UserId);

            if (role.Select(x => x.Designation).ToList().Contains(Designation.TaskAdmin)) await LoadData(true,null);
            else await LoadData(false, x => x.IsRepairTechSpecs == null && x.ITStaff.UserId == UserStore.UserId);
        }

        void InitKanban()
        {
            tileTasks.OptionsKanban.ShowGroupBackground = DefaultBoolean.True;
            tileTasks.OptionsKanban.Groups.Add(new KanbanGroup() { GroupValue = TicketStatus.Accepted });
            tileTasks.OptionsKanban.Groups.Add(new KanbanGroup() { GroupValue = TicketStatus.Assigned });
            tileTasks.OptionsKanban.Groups.Add(new KanbanGroup() { GroupValue = TicketStatus.OnProcess });
            tileTasks.OptionsKanban.Groups.Add(new KanbanGroup() { GroupValue = TicketStatus.ForRelease });
            tileTasks.OptionsKanban.Groups.Add(new KanbanGroup() { GroupValue = TicketStatus.Completed });

            tileTasks.GroupHeaderContextButtonClick += TileView_GroupHeaderContextButtonClick;
            tileTasks.CustomColumnDisplayText += TileView_CustomColumnDisplayText;
        }

        private void TileView_CustomColumnDisplayText(object sender, CustomColumnDisplayTextEventArgs e)
        {
            if (e.IsForGroupRow)
            {
                var kanbanGroup = tileTasks.GetKanbanGroupByValue(e.Value);
                var tiles = kanbanGroup.View.TileTemplate;
                var tileData = tiles.ToList().Select(x => new
                {
                    data = (TasksViewModel)tileTasks.GetRow(x.RowIndex)
                });

                int count = tileTasks.GetChildRowCount(kanbanGroup);
                string cards = count == 1 ? " task" : " tasks";
                e.DisplayText += "<br><size=-2><r>" + count.ToString() + cards;
            }
        }

        private void TileView_GroupHeaderContextButtonClick(object sender, GroupHeaderContextButtonClickEventArgs e)
        {
            throw new NotImplementedException();
        }

        private async Task LoadData(bool taskAdmin,Expression<Func<TicketRequest,bool>> expression = null)
        {
            Expression<Func<TicketRequest, bool>> filterExpression = expression ?? (x => true);
            Expression<Func<TicketRequest, bool>> filterAdminExpression = (x  => true);
            var section = await UserStore.Section();

            if (section == null && taskAdmin) filterAdminExpression = (x => true);
            if (taskAdmin && section != null) filterAdminExpression = x => x.ITStaff.Section == section;
            if (!taskAdmin && section != null) filterAdminExpression = x => x.ITStaff.Section == section;

            var tickets = unitOfWork.TicketRequestRepo.GetAll(
                x => x.Repairs,
                x => x.Repairs.Actions,
                x => x.Repairs.PPEs, 
                x => x.Repairs.PPEs.PPEsSpecs, 
                x => x.Repairs.PPEs.PPEsSpecs.Select(s => s.Model), 
                x => x.Repairs.PPEs.PPEsSpecs.Select(s => s.Model.Brand), 
                x => x.Repairs.PPEs.PPEsSpecs.Select(s => s.Model.Brand.EquipmentSpecs), 
                x => x.Repairs.PPEs.PPEsSpecs.Select(s => s.Model.Brand.EquipmentSpecs.Equipment),
                x => x.TechSpecs, 
                x => x.TechSpecs.Actions,
                x => x.TechSpecs.TechSpecsICTSpecs, 
                x => x.TechSpecs.TechSpecsICTSpecs.Select(s => s.EquipmentSpecs), 
                x => x.TechSpecs.TechSpecsICTSpecs.Select(s => s.EquipmentSpecs.Equipment),
                x => x.Deliveries, 
                x => x.Deliveries.Actions,
                x => x.Deliveries.DeliveriesSpecs, 
                x => x.Deliveries.DeliveriesSpecs.Select(s => s.Model), 
                x => x.Deliveries.DeliveriesSpecs.Select(s => s.Model.Brand), 
                x => x.Deliveries.DeliveriesSpecs.Select(s => s.Model.Brand.EquipmentSpecs), 
                x => x.Deliveries.DeliveriesSpecs.Select(s => s.Model.Brand.EquipmentSpecs.Equipment),
                x => x.ITStaff,
                x => x.ITStaff.Users
                ).Where(filterExpression).Where(filterAdminExpression).ToList().Select(x => new TasksViewModel
            {
                Ticket = x,
                Status = x.TicketStatus.ToString()
            }).OrderByDescending(x => x.Ticket.DateCreated);

            if (tickets == null) return;
            gcTasks.DataSource = new BindingList<TasksViewModel>(tickets.ToList());
        }

        private void tileTasks_ItemCustomize(object sender, TileViewItemCustomizeEventArgs e)
        {
            var task = tileTasks.GetRow(e.RowHandle) as TasksViewModel; 
            if (task == null) return;

            e.Item["Ticket.RequestType"].Appearance.Normal.BackColor = GetLabelColor(task.Ticket.RequestType);
        }
        private Color GetLabelColor(RequestType type)
        {
            switch (type)
            {
                case RequestType.TechSpecs: return ColorTranslator.FromHtml("#f06562");
                case RequestType.Deliveries: return ColorTranslator.FromHtml("#1fb876");
                case RequestType.Repairs: return ColorTranslator.FromHtml("#fca90a");
                default: return ColorTranslator.FromHtml("#969696");
            }
        }

        private async void tileTasks_BeforeItemDrop(object sender, BeforeItemDropEventArgs e)
        {
            var tileRowHandle = e.RowHandle;
            var task = tileTasks.GetRow(tileRowHandle) as TasksViewModel;
            var newGroup = (TicketStatus)e.NewGroupColumnValue;
            await UpdateTicketStatus(task,newGroup);

            var actionType = new Models.Models.ActionType
            {
                Id = task.Ticket.Id,
                RequestType = task.Ticket.RequestType
            };

            var frm = new frmDocAction(actionType, SaveType.Insert, null, unitOfWork, null);
            frm.ShowDialog();
        }

        private async Task UpdateTicketStatus(TasksViewModel model,TicketStatus status)
        {
            var task = await unitOfWork.TicketRequestRepo.FindAsync(x => x.Id == model.Ticket.Id);
            if(task == null) return;    
            task.TicketStatus = status;
            unitOfWork.Save();
        }

        private void hplEpisNo_Click(object sender, EventArgs e)
        {

        }

        private void btnNavigate_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var task = (TasksViewModel)tileTasks.GetFocusedRow();
            if (task.Ticket.RequestType == RequestType.TechSpecs) NavigateToProcess(new UCTechSpecs()
            {
                IsTechSpecs = true,
                Dock = DockStyle.Fill,
                filterText = task.Ticket.Id.ToString()
            });

            if (task.Ticket.RequestType == RequestType.Deliveries) NavigateToProcess(new UCDeliveries()
            {
                Dock = DockStyle.Fill,
                filterText = task.Ticket.Id.ToString()
            });

            if (task.Ticket.RequestType == RequestType.Repairs) NavigateToProcess(new UCRepair()
            {
                Dock = DockStyle.Fill,
                filterText = task.Ticket.Id.ToString()
            });
        }

        private void NavigateToProcess(Control uc)
        {
            var main = Application.OpenForms["frmMain"] as frmMain;
            main.mainPanel.Controls.Clear();

            main.mainPanel.Controls.Add(uc);
        }

        private void tileTasks_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (FromQueue) return;
            TileViewHitInfo hi = this.tileTasks.CalcHitInfo(e.Location);
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                if (hi.HitTest == DevExpress.XtraEditors.TileControlHitTest.Item)
                {
                    this.popupMenu1.ShowPopup(Control.MousePosition);
                }
            }
            if (!(hi.HitTest == DevExpress.XtraEditors.TileControlHitTest.Item)) fpPanelActions.HidePopup();
        }

        private async void UCUserTasks_Load(object sender, EventArgs e)
        {
            await CheckRole();
        }

        private void lueProcessType_EditValueChanged(object sender, EventArgs e)
        {
            tileTasks.ActiveFilterCriteria = null;
            var process = (RequestType)lueProcessType.EditValue;
            if (process == RequestType.PR) return;
            var criteria = tileTasks.ActiveFilterCriteria;
            if (lueProcessType.EditValue != null) criteria = GroupOperator.And(criteria, new BinaryOperator("Ticket.RequestType", process));

            tileTasks.ActiveFilterCriteria = criteria;
        }

        private void tileTasks_ItemClick(object sender, TileViewItemClickEventArgs e)
        {
            var task = (TasksViewModel)tileTasks.GetFocusedRow();
            var actionType = new ActionType()
            {
                Id = task.Ticket.Id,
                RequestType = task.Ticket.RequestType
            };
            LoadActions(actionType);
            fpPanelActions.ShowPopup();
        }

        private void fpPanelActions_ButtonClick(object sender, FlyoutPanelButtonClickEventArgs e)
        {
            string tag = e.Button.Tag.ToString();
            switch (tag)
            {
                case "close":
                    fpPanelActions.HidePopup();
                    break;
            }
        }
    }
}
