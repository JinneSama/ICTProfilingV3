using DevExpress.XtraBars;
using ICTProfilingV3.BaseClasses;
using Microsoft.Extensions.DependencyInjection;
using Models.Entities;
using Models.Repository;
using System;
using System.Linq;
using System.Windows.Forms;

namespace ICTProfilingV3.ToolForms
{
    public partial class frmViewChangeLogs : BaseForm
    {
        private IUnitOfWork unitOfWork;
        private readonly IServiceProvider _serviceProvider;
        public frmViewChangeLogs(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            InitializeComponent();
            unitOfWork = new UnitOfWork();
            LoadDetails();
        }

        private void LoadDetails()
        {
            var changes = unitOfWork.ChangeLogsRepo.GetAll().OrderByDescending(x => x.DateCreated);
            gcChangelogs.DataSource = changes.ToList();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var row = (ChangeLogs)gridChangelogs.GetFocusedRow();
            if (MessageBox.Show("Delete this Changelog?", "Confirmation?", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel) return;

            unitOfWork.ChangeLogsRepo.Delete(row);
            unitOfWork.Save();

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