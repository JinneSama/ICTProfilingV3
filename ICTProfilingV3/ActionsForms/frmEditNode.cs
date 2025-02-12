using ICTProfilingV3.BaseClasses;
using Models.Repository;
using Models.ViewModels;

namespace ICTProfilingV3.ActionsForms
{
    public partial class frmEditNode : BaseForm
    {
        private readonly ActionTreeViewModel actionTree;
        private IUnitOfWork unitOfOfWork;
        public frmEditNode(ActionTreeViewModel actionTree)
        {
            InitializeComponent();
            this.actionTree = actionTree;
            unitOfOfWork = new UnitOfWork();
            LoadDetails();
        }

        private void LoadDetails()
        {
            spinOrder.Value = (decimal)actionTree.ActionTree.Order;
            txtValue.Text = actionTree.ActionTree.Value;
        }

        private void btnClose_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        private async void btnSave_Click(object sender, System.EventArgs e)
        {
            var res = await unitOfOfWork.ActionsDropdownsRepo.FindAsync(x => x.Id == actionTree.ActionTree.Id);
            if (res == null) return;
            res.Value = txtValue.Text;
            res.Order = (int)spinOrder.Value;
            await unitOfOfWork.SaveChangesAsync();

            this.Close();
        }
    }
}