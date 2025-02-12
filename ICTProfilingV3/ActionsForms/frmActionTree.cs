using ICTProfilingV3.BaseClasses;
using Models.Enums;
using Models.Repository;
using Models.ViewModels;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace ICTProfilingV3.ActionsForms
{
    public partial class frmActionTree : BaseForm
    {
        private IUnitOfWork unitOfWork;
        public frmActionTree()
        {
            InitializeComponent();
            unitOfWork = new UnitOfWork();
            LoadActionTree();
        }

        private void LoadActionTree()
        {
            unitOfWork = new UnitOfWork();
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

        private void treeActionDropdown_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
                treeMenu.ShowPopup(MousePosition);
        }

        private void btnAddChildNode_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var node = (ActionTreeViewModel)treeActionDropdown.GetFocusedRow();
            if (node == null) return;
            if ((int)node.ActionTree.ActionCategory >= 3) return;
            var frm = new frmAddNode(SaveType.Insert, node);
            frm.ShowDialog();
            LoadActionTree();
        }

        private void btnDeleteNode_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var msgRes = MessageBox.Show("Delete this Node?", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
            if (msgRes == DialogResult.Cancel) return;

            var node = (ActionTreeViewModel)treeActionDropdown.GetFocusedRow();
            if (node == null) return;

            unitOfWork.ActionsDropdownsRepo.DeleteByEx(x => x.Id == node.ActionTree.Id);
            unitOfWork.Save();

            LoadActionTree();
        }

        private void btnEditNode_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var node = (ActionTreeViewModel)treeActionDropdown.GetFocusedRow();
            if (node == null) return;

            var frm = new frmEditNode(node);
            frm.ShowDialog();

            LoadActionTree();
        }
    }
}