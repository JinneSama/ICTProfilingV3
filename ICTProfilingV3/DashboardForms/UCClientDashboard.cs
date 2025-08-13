using ICTProfilingV3.ActionsForms;
using ICTProfilingV3.DataTransferModels.Models;
using Models.Entities;
using Models.Models;
using Models.Repository;
using System;
using System.Linq;
using System.Windows.Forms;

namespace ICTProfilingV3.DashboardForms
{
    public partial class UCClientDashboard : DevExpress.XtraEditors.XtraUserControl
    {
        private readonly IUnitOfWork _unitOfWork;
        public UCClientDashboard()
        {
            InitializeComponent();
            _unitOfWork = new UnitOfWork();

            LoadData();
        }

        private void LoadData()
        {
            var clientTicket = _unitOfWork.TicketRequestRepo.GetAll(x => x.TechSpecs, x => x.Repairs, x => x.Deliveries)
                .Select(x => new ClientTicket
                {
                    TicketRequest = x,
                    ReqById = GetRequestor(x),
                    ReqByChiefId = GetRequestorChief(x)
                });
        }

        private long GetRequestorChief(TicketRequest x)
        {
            if (x.RequestType == Models.Enums.RequestType.TechSpecs)
                return x.TechSpecs?.ReqByChiefId ?? 0;
            if (x.RequestType == Models.Enums.RequestType.Repairs)
                return x.Repairs?.ReqByChiefId ?? 0;
            if (x.RequestType == Models.Enums.RequestType.Deliveries)
                return x.Deliveries?.ReqByChiefId ?? 0;

            return 0;
        }

        private long GetRequestor(TicketRequest x)
        {
            if (x.RequestType == Models.Enums.RequestType.TechSpecs)
                return x.TechSpecs?.ReqById ?? 0;
            if (x.RequestType == Models.Enums.RequestType.Repairs)
                return x.Repairs?.RequestedById ?? 0;
            if (x.RequestType == Models.Enums.RequestType.Deliveries)
                return x.Deliveries?.RequestedById ?? 0;

            return 0;
        }

        private void LoadActions(ActionType actionType)
        {
        }
    }

    public class ClientTicket
    {
        public TicketRequest TicketRequest { get; set; }
        //----HRIS Data----
        public long ReqById { get; set; }
        public long ReqByChiefId { get; set; }
        //----HRIS Data----
    }
}
