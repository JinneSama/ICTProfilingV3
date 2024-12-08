using Models.Enums;
using Models.Repository;
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
            var res = unitOfWork.TicketRequestRepo.FindAllAsync(x => x.TicketStatus == TicketStatus.OnProcess);
            gcOnProcess.DataSource = res.ToList();

            var res2 = unitOfWork.TicketRequestRepo.GetAll();
            gcWaiting.DataSource = res2.ToList();
        }
    }
}
