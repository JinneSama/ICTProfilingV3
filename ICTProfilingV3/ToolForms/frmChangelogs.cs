using Models.Repository;
using System.Linq;

namespace ICTProfilingV3.ToolForms
{
    public partial class frmChangelogs : DevExpress.XtraEditors.XtraForm
    {
        private readonly string version;
        private IUnitOfWork unitOfWork;
        public frmChangelogs(string version)
        {
            InitializeComponent();
            this.version = version;
            unitOfWork = new UnitOfWork();
            LoadDetails();
        }

        private void LoadDetails()
        {
            lblVersion.Text = "Current Version: " + version;
            var changes = unitOfWork.ChangeLogsRepo.GetAll().OrderByDescending(x => x.DateCreated);
            gcChangelogs.DataSource = changes.ToList();
        }

        private void btnClose_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }
    }
}