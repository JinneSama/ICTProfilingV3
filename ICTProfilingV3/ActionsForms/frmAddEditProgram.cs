using Models.Entities;
using System;
using Models.Enums;
using ICTProfilingV3.BaseClasses;
using ICTProfilingV3.Interfaces;

namespace ICTProfilingV3.ActionsForms
{
    public partial class frmAddEditProgram : BaseForm
    {
        private readonly IRepository<int, ActionsDropdowns> _actionTreeRepo;
        public frmAddEditProgram(IRepository<int, ActionsDropdowns> actionTreeRepo)
        {
            _actionTreeRepo = actionTreeRepo;
            InitializeComponent();
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            var program = new ActionsDropdowns
            {
                ActionCategory = ActionCategory.Programs,
                Order = (int)spinOrder.Value,
                Value = txtProgram.Text
            };
            await _actionTreeRepo.AddAsync(program);
            this.Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}