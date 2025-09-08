using ICTProfilingV3.DataTransferModels.Models;
using System.Collections.Generic;

namespace ICTProfilingV3.ToolForms
{
    public partial class UCAssignedTo : DevExpress.XtraEditors.XtraUserControl
    {
        private StaffModel _staffModel;

        public UCAssignedTo()
        {
            InitializeComponent();
        }

        public void InitUC(StaffModel staffModel)
        {
            _staffModel = staffModel;
        }

        private void UCAssignedTo_Load(object sender, System.EventArgs e)
        {
            hccAssignedTo.DataContext = new List<StaffModel>() { _staffModel };
            hccAssignedTo.FindElementById("Photo").Hidden = _staffModel.PhotoVisible;
            hccAssignedTo.FindElementById("Initials").Hidden = _staffModel.InitialsVisible;
        }
    }
}
