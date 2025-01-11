using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Tile;
using Models.Enums;
using Models.Managers.User;
using Models.Repository;
using Models.ViewModels;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace ICTProfilingV3.DashboardForms
{
    public partial class UCUserTasks : DevExpress.XtraEditors.XtraUserControl
    {
        private IUnitOfWork unitOfWork;
        public UCUserTasks()
        {
            InitializeComponent();
            unitOfWork = new UnitOfWork();
            InitKanban();
            LoadData();
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
                int count = tileTasks.GetChildRowCount(kanbanGroup);
                string cards = count == 1 ? " task" : " tasks";
                e.DisplayText += "<br><size=-2><r>" + count.ToString() + cards;
            }
        }

        private void TileView_GroupHeaderContextButtonClick(object sender, GroupHeaderContextButtonClickEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void LoadData()
        {
            var tickets = unitOfWork.TicketRequestRepo.FindAllAsync(x => x.IsRepairTechSpecs == null && x.ITStaff.UserId == UserStore.UserId,
                x => x.Repairs,
                x => x.TechSpecs,
                x => x.Deliveries).ToList().Select(x => new TasksViewModel
            {
                Ticket = x,
                Status = x.TicketStatus.ToString()
            });
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
        }

        private async Task UpdateTicketStatus(TasksViewModel model,TicketStatus status)
        {
            var task = await unitOfWork.TicketRequestRepo.FindAsync(x => x.Id == model.Ticket.Id);
            if(task == null) return;    
            task.TicketStatus = status;
            unitOfWork.Save();
        }
    }
}
