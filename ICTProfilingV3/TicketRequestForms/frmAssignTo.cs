using Models.Entities;
using Models.Repository;
using Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Models.Enums;
using Models.Managers;
using Models.Managers.User;

namespace ICTProfilingV3.TicketRequestForms
{
    public partial class frmAssignTo : DevExpress.XtraEditors.XtraForm, ITicketStatus
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly TicketRequest ticket;

        public frmAssignTo(TicketRequest ticket)
        {
            InitializeComponent();
            unitOfWork = new UnitOfWork();
            lblTicketNo.Text = ticket.Id.ToString();
            this.ticket = ticket;
            LoadStaff();
        }
        private void LoadStaff()
        {
            var res = unitOfWork.ITStaffRepo.GetAll().Select(x => new StaffViewModel
            {
                Mark = x.Id == ticket.StaffId ? true : false,
                Users = x.Users,
                Staff = x
            });
            gcAssign.DataSource = res.ToList();
            SetFocusToMarked();
        }

        private void SetFocusToMarked()
        {
            int? rowHandle = null;
            for (int i = 0; i < tvAssign.RowCount; i++)
            {
                var row = (StaffViewModel)tvAssign.GetRow(i);
                if (row == null) continue;
                if (row.Mark ?? false) rowHandle = i;
            }
            if (rowHandle == null) return;
            tvAssign.FocusedRowHandle = rowHandle ?? 0;
        }

        private async void btnSave_Click(object sender, System.EventArgs e)
        {
            List<StaffViewModel> staff = new List<StaffViewModel>();
            for (int i = 0; i < tvAssign.RowCount; i++)
            {
                var row = (StaffViewModel)tvAssign.GetRow(i);
                if(row == null) continue;
                if (row.Mark ?? false) staff.Add(row);
            }

            if (staff.Count > 1 || staff.Count < 1)
                MessageBox.Show("Please Select Only 1 Staff to Assign");
            else
            {
                await UpdateTicket(staff.FirstOrDefault());
                this.Close();
            }
        }

        private async Task UpdateTicket(StaffViewModel staffViewModel)
        {
            var _ticket = await unitOfWork.TicketRequestRepo.FindAsync(x => x.Id == ticket.Id);
            if (_ticket == null) return;

            _ticket.StaffId = staffViewModel.Staff.Id;
            _ticket.TicketStatus = TicketStatus.Assigned;
            await ModifyStatus(TicketStatus.Assigned , _ticket.Id);
            await unitOfWork.SaveChangesAsync();
        }

        public async Task ModifyStatus(TicketStatus status,int ticketId)
        {
            var ticketStatus = new TicketRequestStatus
            {
                Status = status,
                DateStatusChanged = DateTime.UtcNow,
                ChangedByUserId = UserStore.UserId,
                TicketRequestId = ticketId
            };
            unitOfWork.TicketRequestStatusRepo.Insert(ticketStatus);
            await unitOfWork.SaveChangesAsync();    
        }
    }
}