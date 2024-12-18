using Models.Models;
using System.Collections.Generic;

namespace ICTProfilingV3.ToolForms
{
    public partial class UCAssignedTo : DevExpress.XtraEditors.XtraUserControl
    {
        private readonly StaffModel staffModel;

        public UCAssignedTo(StaffModel staffModel)
        {
            InitializeComponent();
            this.staffModel = staffModel;
        }

        private void UCAssignedTo_Load(object sender, System.EventArgs e)
        {
            hccAssignedTo.DataContext = new List<StaffModel>() { staffModel };
            hccAssignedTo.FindElementById("Photo").Hidden = staffModel.PhotoVisible;
            hccAssignedTo.FindElementById("Initials").Hidden = staffModel.InitialsVisible;
        }
    }
}
