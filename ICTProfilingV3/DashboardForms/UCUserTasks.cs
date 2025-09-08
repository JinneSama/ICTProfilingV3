using DevExpress.Data.Filtering;
using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Tile;
using DevExpress.XtraGrid.Views.Tile.ViewInfo;
using ICTProfilingV3.ActionsForms;
using ICTProfilingV3.Core.Common;
using ICTProfilingV3.DataTransferModels.Models;
using ICTProfilingV3.DataTransferModels.ViewModels;
using ICTProfilingV3.DeliveriesForms;
using ICTProfilingV3.Interfaces;
using ICTProfilingV3.RepairForms;
using ICTProfilingV3.TechSpecsForms;
using Microsoft.Extensions.DependencyInjection;
using Models.Entities;
using Models.Enums;
using System;
using System.ComponentModel;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ICTProfilingV3.DashboardForms
{
    public partial class UCUserTasks : DevExpress.XtraEditors.XtraUserControl
    {
        private readonly ITicketRequestService _ticketService;
        private readonly IICTUserManager _userManager;
        private readonly IICTRoleManager _roleManager;
        private readonly IServiceProvider _serviceProvider;
        private readonly IStaffService _staffService;
        private readonly UserStore _userStore;
        public bool FromQueue { get; set; } = false;
        public UCUserTasks(IServiceProvider serviceProvider, UserStore userStore, IICTRoleManager roleManager,
            IICTUserManager userManager, ITicketRequestService ticketService,
            IStaffService staffService)
        {
            InitializeComponent();
            _userStore = userStore;
            _userManager = userManager;
            _roleManager = roleManager;
            _ticketService = ticketService;
            _serviceProvider = serviceProvider;
            _staffService = staffService;
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
            var uc = _serviceProvider.GetRequiredService<UCActions>();
            uc.setActions(actionType);
            uc.Dock = System.Windows.Forms.DockStyle.Fill;
            fpActions.Controls.Add(uc);
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

            var user = await _userManager.FindUserAsync(_userStore.UserId);
            if (user.Roles == null)
                await LoadData(false, x => x.IsRepairTechSpecs == null && x.ITStaff.UserId == _userStore.UserId);

            var role = await _roleManager.GetRoleDesignations(user.Roles.FirstOrDefault().RoleId);
            if (role == null)
                await LoadData(false, x => x.IsRepairTechSpecs == null && x.ITStaff.UserId == _userStore.UserId);

            if (role.Select(x => x.Designation).ToList().Contains(Designation.TaskAdmin)) await LoadData(true,null);
            else await LoadData(false, x => x.IsRepairTechSpecs == null && x.ITStaff.UserId == _userStore.UserId);
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
            var section = await _staffService.Section();

            if (section == null && taskAdmin) filterAdminExpression = (x => true);
            if (taskAdmin && section != null) filterAdminExpression = x => x.ITStaff.Section == section;
            if (!taskAdmin && section != null) filterAdminExpression = x => x.ITStaff.Section == section;

            var tickets = _ticketService.GetAll()
                .Include(x => x.Repairs)
                .Include(x => x.Repairs.Actions)
                .Include(x => x.Repairs.PPEs) 
                .Include(x => x.Repairs.PPEs.PPEsSpecs) 
                .Include(x => x.Repairs.PPEs.PPEsSpecs.Select(s => s.Model)) 
                .Include(x => x.Repairs.PPEs.PPEsSpecs.Select(s => s.Model.Brand)) 
                .Include(x => x.Repairs.PPEs.PPEsSpecs.Select(s => s.Model.Brand.EquipmentSpecs)) 
                .Include(x => x.Repairs.PPEs.PPEsSpecs.Select(s => s.Model.Brand.EquipmentSpecs.Equipment))
                .Include(x => x.TechSpecs) 
                .Include(x => x.TechSpecs.Actions)
                .Include(x => x.TechSpecs.TechSpecsICTSpecs) 
                .Include(x => x.TechSpecs.TechSpecsICTSpecs.Select(s => s.EquipmentSpecs)) 
                .Include(x => x.TechSpecs.TechSpecsICTSpecs.Select(s => s.EquipmentSpecs.Equipment))
                .Include(x => x.Deliveries) 
                .Include(x => x.Deliveries.Actions)
                .Include(x => x.Deliveries.DeliveriesSpecs) 
                .Include(x => x.Deliveries.DeliveriesSpecs.Select(s => s.Model)) 
                .Include(x => x.Deliveries.DeliveriesSpecs.Select(s => s.Model.Brand)) 
                .Include(x => x.Deliveries.DeliveriesSpecs.Select(s => s.Model.Brand.EquipmentSpecs)) 
                .Include(x => x.Deliveries.DeliveriesSpecs.Select(s => s.Model.Brand.EquipmentSpecs.Equipment))
                .Include(x => x.ITStaff)
                .Include(x => x.ITStaff.Users
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

            var actionType = new ActionType
            {
                Id = task.Ticket.Id,
                RequestType = task.Ticket.RequestType
            };

            var frm = _serviceProvider.GetRequiredService<frmDocAction>();
            frm.SetActionBehavior(actionType, SaveType.Insert, null, null);
            frm.ShowDialog();
        }

        private async Task UpdateTicketStatus(TasksViewModel model,TicketStatus status)
        {
            var task = await _ticketService.GetByFilterAsync(x => x.Id == model.Ticket.Id);
            if(task == null) return;    
            task.TicketStatus = status;
            await _ticketService.SaveChangesAsync();
        }

        private void hplEpisNo_Click(object sender, EventArgs e)
        {

        }

        private void btnNavigate_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var task = (TasksViewModel)tileTasks.GetFocusedRow();
            var mainForm = _serviceProvider.GetRequiredService<frmMain>();
            if (task.Ticket.RequestType == RequestType.TechSpecs)
            {
                var navigation = _serviceProvider.GetRequiredService<IControlNavigator<UCTechSpecs>>();
                navigation.NavigateTo(mainForm.mainPanel, act =>
                {
                    act.filterText = task.Ticket.Id.ToString();
                    act.IsTechSpecs = true;
                });
            }

            if (task.Ticket.RequestType == RequestType.Deliveries)
            {
                var navigation = _serviceProvider.GetRequiredService<IControlNavigator<UCDeliveries>>();
                navigation.NavigateTo(mainForm.mainPanel, act => act.filterText = task.Ticket.Id.ToString());
            };

            if (task.Ticket.RequestType == RequestType.Repairs)
            {
                var navigation = _serviceProvider.GetRequiredService<IControlNavigator<UCRepair>>();
                navigation.NavigateTo(mainForm.mainPanel, act => act.filterText = task.Ticket.Id.ToString());
            };
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
