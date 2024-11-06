using DevExpress.XtraEditors;
using Models.Entities;
using System;
using Models.Enums;
using Models.Repository;

namespace ICTProfilingV3.ActionsForms
{
    public partial class frmAddEditProgram : DevExpress.XtraEditors.XtraForm
    {
        private readonly IUnitOfWork unitOfWork;
        public frmAddEditProgram()
        {
            InitializeComponent();
            unitOfWork = new UnitOfWork();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var program = new ActionsDropdowns
            {
                ActionCategory = ActionCategory.Programs,
                Order = (int)spinOrder.Value,
                Value = txtProgram.Text
            };
            unitOfWork.ActionsDropdownsRepo.Insert(program);
            unitOfWork.Save();
            this.Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}