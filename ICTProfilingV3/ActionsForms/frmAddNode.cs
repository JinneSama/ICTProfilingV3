using ICTProfilingV3.BaseClasses;
using ICTProfilingV3.DataTransferModels;
using ICTProfilingV3.Interfaces;
using Models.Entities;
using Models.Enums;
using System.Linq;

namespace ICTProfilingV3.ActionsForms
{
    public partial class frmAddNode : BaseForm
    {
        private ActionTreeDTM _actionTree;
        private readonly IRepository<int, ActionsDropdowns> _actionTreeRepo;

        public frmAddNode(IRepository<int, ActionsDropdowns> actionTreeRepo)
        {
            _actionTreeRepo = actionTreeRepo;
            InitializeComponent();
            LoadDropdown();
        }

        public void SetAddNode(ActionTreeDTM actionTree)
        {
            _actionTree = actionTree;
        }

        private void LoadDropdown()
        {
            actionsDropdownsBindingSource.DataSource = _actionTreeRepo.GetAll().ToList();
            lueParentNode.EditValue = _actionTree.ActionTree.Id;
        }

        private async void btnSave_Click(object sender, System.EventArgs e)
        {
            var nodeCategory = ((int)_actionTree.ActionTree.ActionCategory) + 1;
            var node = new ActionsDropdowns
            {
                ParentId = _actionTree.ActionTree.Id ?? 0,
                ActionCategory = (ActionCategory)nodeCategory,
                Order = (int)spinOrder.Value,
                Value = txtValue.Text
            };
            await _actionTreeRepo.AddAsync(node);
            this.Close();
        }

        private void btnClose_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }
    }
}