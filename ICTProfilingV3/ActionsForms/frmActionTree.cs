using Models.Enums;
using Models.Repository;
using Models.ViewModels;
using System.ComponentModel;
using System.Linq;

namespace ICTProfilingV3.ActionsForms
{
    public partial class frmActionTree : DevExpress.XtraEditors.XtraForm
    {
        private readonly IUnitOfWork unitOfWork;
        public frmActionTree()
        {
            InitializeComponent();
            unitOfWork = new UnitOfWork();
            LoadActionTree();
        }

        private void LoadActionTree()
        {
            var tree = unitOfWork.ActionsDropdownsRepo.GetAll().ToList();
            var treeViewModel = tree.Select(x => new ActionTreeViewModel
            {
                ActionTree = x,
                ImageIndex = (int)x.ActionCategory,
                NodeValue = x.Order + ". " + x.Value
            }).OrderBy(o => o.ActionTree.Order);
            bsActionTree.DataSource = new BindingList<ActionTreeViewModel>(treeViewModel.ToList());
            treeActionDropdown.ExpandAll();
        }

        private void btnAddProgram_Click(object sender, System.EventArgs e)
        {
            var frm = new frmAddEditProgram();
            frm.ShowDialog();
            LoadActionTree();
        }

        private void btnAddNode_Click(object sender, System.EventArgs e)
        {
            var node = (ActionTreeViewModel)treeActionDropdown.GetFocusedRow();
            if (node == null) return;
            if ((int)node.ActionTree.ActionCategory >= 3) return;
            var frm = new frmAddNode(SaveType.Insert , node);
            frm.ShowDialog();
            LoadActionTree();
        }
    }
}