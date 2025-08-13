using ICTProfilingV3.Services.Employees;
using System.Threading.Tasks;

namespace ICTProfilingV3.PurchaseRequestForms
{
    public partial class UCFDTS : DevExpress.XtraEditors.XtraUserControl
    {
        private readonly string _controlNo;
        public UCFDTS(string controlNo)
        {
            InitializeComponent();
            _controlNo = controlNo;
        }

        private async Task LoadDetails()
        {
            var data = await FDTSData.GetData(_controlNo);
            if (data == null) return;
            
            txtDate.Text = data.Date.Value.ToShortDateString();
            txtOfficeContronNo.Text = data.ControlNo;
            txtPRDesc.Text = data.PRDescription;
            txtTotalAmount.Value = data.TotalAmount.Value;
            txtPurpose.Text = data.Purpose;
            txtBudgetPR.Text = data.BudgetPRNo;

            gcPRDetails.DataSource = data.PRDetails;
            gcAction.DataSource = data.DocumentActions;
        }

        private async void UCFDTS_Load(object sender, System.EventArgs e)
        {
            await LoadDetails();
        }
    }
}
