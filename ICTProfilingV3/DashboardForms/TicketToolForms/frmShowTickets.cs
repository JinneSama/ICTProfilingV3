using ICTProfilingV3.BaseClasses;
using Models.Enums;

namespace ICTProfilingV3.DashboardForms.TicketToolForms
{
    public partial class frmShowTickets : BaseForm
    {
        private readonly TicketStatus _ticketStatus;
        public frmShowTickets(TicketStatus status)
        {
            InitializeComponent();
            _ticketStatus = status;
        }

        private void LoadData()
        {

        }
    }
}