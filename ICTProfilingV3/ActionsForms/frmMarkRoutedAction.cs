using ICTProfilingV3.BaseClasses;
using ICTProfilingV3.Interfaces;

namespace ICTProfilingV3.ActionsForms
{
    public partial class frmMarkRoutedAction : BaseForm
    {
        private readonly IDocActionsService _docActService;
        private int _actionId;
        public frmMarkRoutedAction(IDocActionsService docActionsService)
        {
            _docActService = docActionsService;
            InitializeComponent();
        }

        public void InitForm(int actionId)
        {
            _actionId = actionId;
        }

        private void btnClose_Click(object sender, System.EventArgs e)
        {
            Close();
        }

        private async void btnSave_Click(object sender, System.EventArgs e)
        {
            await _docActService.UpdateDiscrepancy(_actionId, ceWithDiscrepancy.Checked, txtRemarks.Text);
            Close();
        }
    }
}