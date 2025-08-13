using ICTProfilingV3.BaseClasses;
using ICTProfilingV3.DataTransferModels;
using ICTProfilingV3.Interfaces;
using Models.Entities;

namespace ICTProfilingV3.ActionsForms
{
    public partial class frmEditNode : BaseForm
    {
        private ActionTreeDTM _actionTree;
        private IRepository<int, ActionsDropdowns> _actionTreeRepo;
        public frmEditNode(IRepository<int, ActionsDropdowns> actionTreeRepo)
        {
            _actionTreeRepo = actionTreeRepo;
            InitializeComponent();
            LoadDetails();
        }

        public void SetEditNode(ActionTreeDTM actionTree)
        {
            _actionTree = actionTree;
        }
        private void LoadDetails()
        {
            spinOrder.Value = (decimal)_actionTree.ActionTree.Order;
            txtValue.Text = _actionTree.ActionTree.Value;
        }

        private void btnClose_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        private async void btnSave_Click(object sender, System.EventArgs e)
        {
            var res = await _actionTreeRepo.GetById(_actionTree.ActionTree.Id.Value);
            if (res == null) return;
            res.Value = txtValue.Text;
            res.Order = (int)spinOrder.Value;

            await _actionTreeRepo.SaveChangesAsync();
            this.Close();
        }
    }
}