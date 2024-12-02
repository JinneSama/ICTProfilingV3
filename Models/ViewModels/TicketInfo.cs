using Models.Entities;

namespace Models.ViewModels
{
    public class TicketInfo
    {
        public StaffViewModel ITStaff { get; set; }
        public EmployeesViewModel Chief { get; set; }
        public RoutedActionsViewModel lastAction { get; set; }
    }
}
