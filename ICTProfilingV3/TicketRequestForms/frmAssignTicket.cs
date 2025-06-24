using Helpers.Interfaces;
using Helpers.NetworkFolder;
using ICTProfilingV3.ActionsForms;
using Models.Entities;
using Models.Enums;
using Models.Managers.User;
using Models.Repository;
using Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ICTProfilingV3.TicketRequestForms
{
    public partial class frmAssignTicket : BaseClasses.BaseForm, IModifyTicketStatus
    {
        private readonly IUnitOfWork unitOfWork;
        private HTTPNetworkFolder networkFolder;
        private readonly TicketRequest ticket;
        public frmAssignTicket(TicketRequest ticket)
        {
            InitializeComponent();
            unitOfWork = new UnitOfWork();
            networkFolder = new HTTPNetworkFolder();
            this.ticket = ticket;
        }
        private async Task LoadStaff()
        {
            var staffList = unitOfWork.ITStaffRepo.FindAllAsync(x => x.Users.IsDeleted == false, x => x.TicketRequests, x => x.Users).ToList().OrderBy(x => x.Users.FullName);
            var staffViewModels = new List<StaffViewModel>();
            foreach (var staff in staffList)
            {
                //var image = await networkFolder.DownloadFile(staff.UserId + ".jpeg");
                staffViewModels.Add(new StaffViewModel
                {
                    Staff = staff,
                    UserId = staff.UserId
                    //Image = image
                });
            }
            gcStaff.DataSource = staffViewModels.ToList();
        }
        private async void frmAssignTicket_Load(object sender, System.EventArgs e)
        {
            await LoadStaff();
        }

        private async void btnAssign_Click(object sender, System.EventArgs e)
        {
            int tickets = CheckOnProcessRequest();
            if (tickets >= 1)
            {
                var dialogResult = MessageBox.Show($"This Staff has {tickets} On Process requests, you cannot assign until this staff's On Process Request was cleared. \nContinue Anyway?", "Ticket Limit Reached", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if(dialogResult == DialogResult.Cancel)
                    return;
            }
            var row = (StaffViewModel)tvStaff.GetFocusedRow();
            if (row == null)
            {
                MessageBox.Show($"Please Select a Staff to Assign", "Select Staff", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var res = MessageBox.Show($"Assign Ticket to {row.Users.FullName}?", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (res == DialogResult.Cancel) return;

            await UpdateTicket(row);
            this.Close();

            var actionType = new Models.Models.ActionType
            {
                Id = ticket.Id,
                RequestType = ticket.RequestType
            };

            var frm = new frmDocAction(actionType, SaveType.Insert, null, unitOfWork, row.Users);
            frm.ShowDialog();
        }

        private async Task UpdateTicket(StaffViewModel staffViewModel)
        {
            var _ticket = await unitOfWork.TicketRequestRepo.FindAsync(x => x.Id == ticket.Id);
            if (_ticket == null) return;

            _ticket.StaffId = staffViewModel.Staff.Id;
            _ticket.TicketStatus = TicketStatus.Assigned;
            unitOfWork.TicketRequestRepo.Update(_ticket);
            await ModifyTicketStatusStatus(TicketStatus.Assigned, _ticket.Id);
            await unitOfWork.SaveChangesAsync();
        }

        private void tvStaff_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            LoadDetails();
        }

        private int CheckOnProcessRequest()
        {
            var row = (StaffViewModel)tvStaff.GetFocusedRow();
            if (row == null) return 0;
            var tickets = unitOfWork.TicketRequestRepo.FindAllAsync(x => x.ITStaff.UserId == row.UserId && x.TicketStatus == Models.Enums.TicketStatus.OnProcess).ToList().Count;
            return tickets;
        }

        private void LoadDetails()
        {
            var row = (StaffViewModel)tvStaff.GetFocusedRow();
            if(row == null) return;
            var tickets = unitOfWork.TicketRequestRepo.FindAllAsync(x => x.ITStaff.UserId == row.UserId && x.TicketStatus != Models.Enums.TicketStatus.Completed).ToList();
            gcAssigned.DataSource = tickets;

            var requestTypeCounts = tickets
                .GroupBy(x => x.RequestType)
                .Select(g => new
                {
                    RequestType = g.Key,
                    Count = g.Count()
                }).ToList();
            gcTickets.DataSource = requestTypeCounts;
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
    }
}