using Models.Entities;
using Models.Enums;
using System;
using System.Drawing;

namespace Models.ViewModels
{
    public class RoutedActionsViewModel
    {
        private Actions _action;
        public Actions Actions 
        {
            get { return this._action; }
            set { this._action = value; }
        }
        public int Id { get; set; }
        public DateTime? ActionDate { get; set; }
        public string ControlNo => GetControlNo();
        public string ProcessType => EnumHelper.GetEnumDescription(_action.RequestType);
        public string Description => GetDescription();
        public string From { get; set; }
        public string RoutedTo { get; set; }
        public string Remarks { get; set; }
        public bool Completed => Status == TicketStatus.Completed ? true : false;
        public TicketStatus? Status => GetStatus();

        private TicketStatus? GetStatus()
        {
            if (_action?.RequestType == Enums.RequestType.TechSpecs) return _action?.TechSpecs?.TicketRequest.TicketStatus;
            if (_action?.RequestType == Enums.RequestType.Deliveries) return _action?.Deliveries?.TicketRequest.TicketStatus;
            if (_action?.RequestType == Enums.RequestType.Repairs) return _action?.Repairs?.TicketRequest.TicketStatus;
            if (_action?.RequestType == Enums.RequestType.PR) return _action?.PurchaseRequest?.Status;
            if (_action?.RequestType == Enums.RequestType.CAS) return _action?.CustomerActionSheet?.Status;
            if (_action?.RequestType == Enums.RequestType.PGN) return _action?.PGNRequests?.Status;
            if (_action?.RequestType == Enums.RequestType.M365) return _action?.MOAccountUsers?.Status;
            return null;
        }

        private string GetControlNo()
        {
            if (_action?.RequestType == Enums.RequestType.TechSpecs) return _action?.TechSpecsId?.ToString();
            if (_action?.RequestType == Enums.RequestType.Deliveries) return _action?.DeliveriesId?.ToString();
            if (_action?.RequestType == Enums.RequestType.Repairs) return _action?.RepairId?.ToString();
            if (_action?.RequestType == Enums.RequestType.PR) return _action?.PurchaseRequestId?.ToString();
            if (_action?.RequestType == Enums.RequestType.CAS) return _action?.CustomerActionSheetId?.ToString();
            if (_action?.RequestType == Enums.RequestType.PGN) return _action?.PGNRequestId?.ToString();
            if (_action?.RequestType == Enums.RequestType.M365) return _action?.MOAccountUserId?.ToString();
            return null;
        }

        private string GetDescription()
        {
            if (_action?.RequestType == Enums.RequestType.Repairs) return _action?.Repairs?.Problems;
            if (_action?.RequestType == Enums.RequestType.CAS) return _action?.CustomerActionSheet?.ClientRequest;
            return null;
        }
    }
}
