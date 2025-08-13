using Helpers.Interfaces;
using ICTProfilingV3.ActionsForms;
using ICTProfilingV3.BaseClasses;
using ICTProfilingV3.Core.Common;
using ICTProfilingV3.DataTransferModels.Models;
using ICTProfilingV3.DataTransferModels.ViewModels;
using ICTProfilingV3.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Models.Entities;
using Models.Enums;
using Models.Repository;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ICTProfilingV3.TicketRequestForms
{
    public partial class frmAssignTicket : BaseForm, IModifyTicketStatus
    {
        private readonly IStaffService _staffService;
        private readonly ITicketRequestService _ticketService;
        private readonly IServiceProvider _serviceProvider;
        private readonly IUnitOfWork unitOfWork;
        private readonly UserStore _userStore;

        private int _ticketId;
        private RequestType _requestType;
        public frmAssignTicket(IStaffService staffService, ITicketRequestService ticketService, UserStore userStore, IServiceProvider serviceProvider)
        {
            _staffService = staffService;
            _ticketService = ticketService;
            _userStore = userStore;
            _serviceProvider = serviceProvider;
            unitOfWork = new UnitOfWork();
            InitializeComponent();
        }

        public void SetTicket(int ticketId, RequestType requestType)
        {
            _ticketId = ticketId;
            _requestType = requestType;
        }
        private async Task LoadStaff()
        {
            var staffDTM = _staffService.GetStaffDTM();
            gcStaff.DataSource = staffDTM;
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

            var actionType = new ActionType
            {
                Id = _ticketId,
                RequestType = _requestType
            };

            var frm = _serviceProvider.GetRequiredService<frmDocAction>();
            frm.SetActionBehavior(actionType, SaveType.Insert, null, null);
            frm.ShowDialog();
        }

        private async Task UpdateTicket(StaffViewModel staffViewModel)
        {
            var _ticket = await unitOfWork.TicketRequestRepo.FindAsync(x => x.Id == _ticketId);
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
                ChangedByUserId = _userStore.UserId,
                TicketRequestId = Id
            };
            unitOfWork.TicketRequestStatusRepo.Insert(ticketStatus);
            await unitOfWork.SaveChangesAsync();
        }
    }
}