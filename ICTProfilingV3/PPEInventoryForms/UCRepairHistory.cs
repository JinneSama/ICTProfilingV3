using ICTProfilingV3.Interfaces;
using ICTProfilingV3.RepairForms;
using Microsoft.Extensions.DependencyInjection;
using Models.Entities;
using Models.Repository;
using System;
using System.ComponentModel;
using System.Linq;

namespace ICTProfilingV3.PPEInventoryForms
{
    public partial class UCRepairHistory : DevExpress.XtraEditors.XtraUserControl
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUCManager _ucManager;
        private readonly IServiceProvider _serviceProvider;
        private PPEs _ppe;
        public UCRepairHistory(IUCManager uCManager, IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _ucManager = uCManager;
            _serviceProvider = serviceProvider;
            this._unitOfWork = new UnitOfWork();
        }

        public void SetPPE(PPEs ppe)
        {
            _ppe = ppe;
            LoadHistory();
        }

        private void LoadHistory()
        {
            var res = _unitOfWork.RepairsRepo.FindAllAsync(x => x.PPEsId == _ppe.Id).ToList();
            if (res == null) return;
            gcHistory.DataSource = new BindingList<Repairs>(res);
        }

        private void hplRepair_Click(object sender, System.EventArgs e)
        {
            var row = (Repairs)gridHistory.GetFocusedRow(); 
            var mainForm = _serviceProvider.GetRequiredService<frmMain>();
            var navigation = _serviceProvider.GetRequiredService<IControlNavigator<UCRepair>>();
            navigation.NavigateTo(mainForm.mainPanel, act => act.filterText = row.Id.ToString());
        }
    }
}
