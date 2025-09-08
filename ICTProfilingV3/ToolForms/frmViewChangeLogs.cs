using ICTProfilingV3.BaseClasses;
using ICTProfilingV3.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Models.Entities;
using System;
using System.Linq;
using System.Windows.Forms;

namespace ICTProfilingV3.ToolForms
{
    public partial class frmViewChangeLogs : BaseForm
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IChangeLogService _changeLogService;
        public frmViewChangeLogs(IServiceProvider serviceProvider, IChangeLogService changeLogService)
        {
            _serviceProvider = serviceProvider;
            _changeLogService = changeLogService;
            InitializeComponent();
            LoadDetails();
        }

        private void LoadDetails()
        {
            var changes = _changeLogService.GetAll().OrderByDescending(x => x.DateCreated);
            gcChangelogs.DataSource = changes.ToList();
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            var row = (ChangeLogs)gridChangelogs.GetFocusedRow();
            if (MessageBox.Show("Delete this Changelog?", "Confirmation?", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel) return;

            await _changeLogService.DeleteAsync(row.Id);

            LoadDetails();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            var row = (ChangeLogs)gridChangelogs.GetFocusedRow();
            var frm = _serviceProvider.GetRequiredService<frmAddEditChangeLogs>();
            frm.InitForm(row);
            frm.ShowDialog();

            LoadDetails();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            var frm = _serviceProvider.GetRequiredService<frmAddEditChangeLogs>();
            frm.InitForm();
            frm.ShowDialog();

            LoadDetails();
        }
    }
}