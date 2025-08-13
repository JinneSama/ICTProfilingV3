using ICTProfilingV3.BaseClasses;
using ICTProfilingV3.DataTransferModels;
using ICTProfilingV3.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Models.Entities;
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace ICTProfilingV3.ActionsForms
{
    public partial class frmActionTree : BaseForm
    {
        private readonly IRepository<int, ActionsDropdowns> _actionTreeRepo;
        private readonly IServiceProvider _serviceProvider;
        public frmActionTree(IRepository<int, ActionsDropdowns> actionTreeRepo, IServiceProvider service)
        {
            _actionTreeRepo = actionTreeRepo;
            _serviceProvider = service;
            InitializeComponent();
            LoadActionTree();
        }

        private void LoadActionTree()
        {
            var tree = _actionTreeRepo.GetAll().ToList();
            var treeViewModel = tree.Select(x => new ActionTreeDTM
            {
                ActionTree = x,
                ImageIndex = (int)x.ActionCategory,
                NodeValue = x.Order + ". " + x.Value
            }).OrderBy(o => o.ActionTree.Order);
            bsActionTree.DataSource = new BindingList<ActionTreeDTM>(treeViewModel.ToList());
            treeActionDropdown.ExpandAll();
        }

        private void btnAddProgram_Click(object sender, System.EventArgs e)
        {
            var frm = _serviceProvider.GetRequiredService<frmAddEditProgram>();
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
            var node = (ActionTreeDTM)treeActionDropdown.GetFocusedRow();
            if (node == null) return;
            if ((int)node.ActionTree.ActionCategory >= 3) return;
            var frm = _serviceProvider.GetRequiredService<frmAddNode>();
            frm.SetAddNode(node);
            frm.ShowDialog();
            LoadActionTree();
        }

        private async void btnDeleteNode_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var msgRes = MessageBox.Show("Delete this Node?", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
            if (msgRes == DialogResult.Cancel) return;

            var node = (ActionTreeDTM)treeActionDropdown.GetFocusedRow();
            if (node == null) return;

            _actionTreeRepo.Delete(node.ActionTree.Id.Value);
            await _actionTreeRepo.SaveChangesAsync();
            LoadActionTree();
        }

        private void btnEditNode_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var node = (ActionTreeDTM)treeActionDropdown.GetFocusedRow();
            if (node == null) return;

            var frm = _serviceProvider.GetRequiredService<frmEditNode>();
            frm.SetEditNode(node);
            frm.ShowDialog();

            LoadActionTree();
        }
    }
}