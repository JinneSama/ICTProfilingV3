using ICTProfilingV3.DataTransferModels.ViewModels;
using Models.Entities;
using Models.Enums;
using Models.Repository;
using System.Drawing;
using System.Linq;

namespace ICTProfilingV3.DashboardForms
{
    public partial class UCQueue : DevExpress.XtraEditors.XtraUserControl
    {
        private IUnitOfWork unitOfWork;
        public UCQueue()
        {
            InitializeComponent();
            unitOfWork = new UnitOfWork();
            LoadData();
        }

        private void LoadData()
        {
            LoadWaiting();
            LoadOnProcess();
            LoadForRelease();
        }

        private void LoadWaiting()
        {
            var res = unitOfWork.TicketRequestRepo.FindAllAsync(x => x.TicketStatus == TicketStatus.Accepted || x.TicketStatus == TicketStatus.Assigned,
                x => x.ITStaff,
                x => x.ITStaff.Users,
                x => x.Deliveries,
                x => x.Deliveries.Actions,
                x => x.TechSpecs,
                x => x.TechSpecs.Actions,
                x => x.Repairs,
                x => x.Repairs.Actions).ToList().Select(s => new QueueViewModel
                {
                    Ticket = s
                });
            gcWaiting.DataSource = res.ToList();
        }

        private void LoadForRelease()
        {
            var res = unitOfWork.TicketRequestRepo.FindAllAsync(x => x.TicketStatus == TicketStatus.ForRelease,
                x => x.ITStaff,
                x => x.ITStaff.Users,
                x => x.Deliveries,
                x => x.Deliveries.Actions,
                x => x.TechSpecs,
                x => x.TechSpecs.Actions,
                x => x.Repairs,
                x => x.Repairs.Actions).ToList().Select(s => new QueueViewModel
                {
                    Ticket = s
                });
            gcForRelease.DataSource = res.ToList();
        }

        private void LoadOnProcess()
        {
            var res = unitOfWork.TicketRequestRepo.FindAllAsync(x => x.TicketStatus == TicketStatus.OnProcess,
                x => x.ITStaff,
                x => x.ITStaff.Users,
                x => x.Deliveries,
                x => x.Deliveries.Actions,
                x => x.TechSpecs,
                x => x.TechSpecs.Actions,
                x => x.Repairs,
                x => x.Repairs.Actions).ToList().Select(s => new QueueViewModel
                {
                    Ticket = s
                });
            gcOnProcess.DataSource = res.ToList();
        }

        private void tvWaiting_ItemCustomize(object sender, DevExpress.XtraGrid.Views.Tile.TileViewItemCustomizeEventArgs e)
        {
            var task = tvWaiting.GetRow(e.RowHandle) as QueueViewModel;
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

        private void tvForRelese_ItemCustomize(object sender, DevExpress.XtraGrid.Views.Tile.TileViewItemCustomizeEventArgs e)
        {
            var task = tvForRelease.GetRow(e.RowHandle) as QueueViewModel;
            if (task == null) return;

            e.Item["Ticket.RequestType"].Appearance.Normal.BackColor = GetLabelColor(task.Ticket.RequestType);
        }

        private void tvOnProcess_ItemCustomize(object sender, DevExpress.XtraGrid.Views.Tile.TileViewItemCustomizeEventArgs e)
        {
            var task = tvOnProcess.GetRow(e.RowHandle) as QueueViewModel;
            if (task == null) return;

            e.Item["Ticket.RequestType"].Appearance.Normal.BackColor = GetLabelColor(task.Ticket.RequestType);
        }
    }
}
