using Helpers.Interfaces;
using ICTProfilingV3.RepairForms;
using ICTProfilingV3.ReportForms;
using ICTProfilingV3.TechSpecsForms;
using ICTProfilingV3.TicketRequestForms;
using Models.Entities;
using Models.Repository;
using Models.ViewModels;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace ICTProfilingV3.PPEInventoryForms
{
    public partial class UCRepairHistory : DevExpress.XtraEditors.XtraUserControl
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUCManager<Control> _ucManager;
        private readonly PPEs ppe;

        public UCRepairHistory(PPEs _ppe)
        {
            InitializeComponent();
            var main = Application.OpenForms["frmMain"] as frmMain;
            _ucManager = main._ucManager;
            this._unitOfWork = new UnitOfWork();
            ppe = _ppe;
            LoadHistory();
        }

        private void LoadHistory()
        {
            var res = _unitOfWork.RepairsRepo.FindAllAsync(x => x.PPEsId == ppe.Id);
            gcHistory.DataSource = new BindingList<Repairs>(res.ToList());
        }

        private void hplRepair_Click(object sender, System.EventArgs e)
        {
            var row = (Repairs)gridHistory.GetFocusedRow();
            _ucManager.ShowUCSystemDetails(hplRepair.Name, new UCRepair()
            {
                Dock = DockStyle.Fill,
                filterText = row.Id.ToString()
            }, new string[] {"filterText" });
        }
    }
}
