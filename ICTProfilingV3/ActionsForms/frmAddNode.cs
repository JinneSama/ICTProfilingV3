using ICTProfilingV3.BaseClasses;
using Models.Entities;
using Models.Enums;
using Models.Repository;
using Models.ViewModels;
using System.Linq;

namespace ICTProfilingV3.ActionsForms
{
    public partial class frmAddNode : BaseForm
    {
        private readonly SaveType saveType;
        private readonly ActionTreeViewModel actionTreeViewModel;
        private readonly IUnitOfWork unitOfWork;

        public frmAddNode(SaveType saveType , ActionTreeViewModel actionTreeViewModel)
        {
            InitializeComponent();
            this.saveType = saveType;
            this.actionTreeViewModel = actionTreeViewModel;
            unitOfWork = new UnitOfWork();
            LoadDropdown();
        }

        private void LoadDropdown()
        {
            actionsDropdownsBindingSource.DataSource = unitOfWork.ActionsDropdownsRepo.GetAll().ToList();
            lueParentNode.EditValue = actionTreeViewModel.ActionTree.Id;
        }

        private void btnSave_Click(object sender, System.EventArgs e)
        {
            var nodeCategory = ((int)actionTreeViewModel.ActionTree.ActionCategory) + 1;
            var node = new ActionsDropdowns
            {
                ParentId = actionTreeViewModel.ActionTree.Id ?? 0,
                ActionCategory = (ActionCategory)nodeCategory,
                Order = (int)spinOrder.Value,
                Value = txtValue.Text
            };
            unitOfWork.ActionsDropdownsRepo.Insert(node);
            unitOfWork.Save();
            this.Close();
        }

        private void btnClose_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }
    }
}