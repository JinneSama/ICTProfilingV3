using ICTProfilingV3.BaseClasses;
using ICTProfilingV3.DataTransferModels.ViewModels;
using ICTProfilingV3.Services.Employees;
using Models.Enums;
using Models.Repository;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace ICTProfilingV3.StandardPRForms
{
    public partial class frmGeneratePRSuggestion : BaseForm
    {
        private readonly string _controlNo;
        private readonly PRQuarter _quarter;
        public frmGeneratePRSuggestion(string controlNo, PRQuarter quarter)
        {
            InitializeComponent();
            _controlNo = controlNo;
            _quarter = quarter;

            LoadStandardPR();
        }
        private void LoadStandardPR()
        {
            var unitOfWork = new UnitOfWork();
            var quarter = _quarter;
            var spr = unitOfWork.StandardPRSpecsRepo.FindAllAsync(x => x.Quarter == quarter,
                x => x.StandardPRSpecsDetails,
                x => x.EquipmentSpecs,
                x => x.EquipmentSpecs.Equipment).Select(x => new StandardPRViewModel
                {
                    Equipment = x.EquipmentSpecs.Equipment.EquipmentName,
                    StandardPRSpecs = x,
                    StandardPRSpecsDetails = x.StandardPRSpecsDetails
                });
            gcPR.DataSource = new BindingList<StandardPRViewModel>(spr.ToList());

            gridSpecs.Columns["Type"].Group();
            gridSpecs.ExpandAllGroups();
        }

        private async Task SetSuggestions()
        {
            var fdtsData = await FDTSData.GetData(_controlNo);
            if (fdtsData == null) return;

            var prDetails = fdtsData.PRDetails;
        }

        private async void frmGeneratePRSuggestion_Load(object sender, System.EventArgs e)
        {
            await SetSuggestions();
        }
    }
}