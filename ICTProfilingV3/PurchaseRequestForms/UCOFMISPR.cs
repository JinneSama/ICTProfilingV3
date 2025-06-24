using Models.FDTSEntities;
using System.Threading.Tasks;

namespace ICTProfilingV3.PurchaseRequestForms
{
    public partial class UCOFMISPR : DevExpress.XtraEditors.XtraUserControl
    {
        private readonly string _controlNo;
        public UCOFMISPR(string controlNo)
        {
            InitializeComponent();
            _controlNo = controlNo;
        }

        private async Task LoadDetails()
        {
            var data = await FDTSData.GetData(_controlNo);
            if (data == null) return;

            gcPRDetails.DataSource = data.PRDetails;
        }

        private async void UCOFMISPR_Load(object sender, System.EventArgs e)
        {
            await LoadDetails();
        }
    }
}
