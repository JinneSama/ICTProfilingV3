using DevExpress.Data.Filtering;
using ICTProfilingV3.ActionsForms;
using ICTProfilingV3.EvaluationForms;
using Models.Enums;
using Models.HRMISEntites;
using Models.Models;
using Models.Repository;
using Models.ViewModels;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace ICTProfilingV3.CustomerActionSheetForms
{
    public partial class UCCAS : DevExpress.XtraEditors.XtraUserControl
    {
        private readonly IUnitOfWork _unitOfWork;
        public string filterText { get; set; }
        public UCCAS()
        {
            InitializeComponent();
            this._unitOfWork = new UnitOfWork();
            LoadCAS();
        }

        private void LoadCAS()
        {
            var res = _unitOfWork.CustomerActionSheetRepo.GetAll(x => x.AssistedBy).OrderByDescending(x => x.DateCreated).ToList();
            var cas = res.Select(x => new CASViewModel
            {
                Id = x.Id,
                DateCreated = x.DateCreated ?? System.DateTime.MinValue,
                Office = x.ClientId == null ? x.Office : HRMISEmployees.GetEmployeeById(x.ClientId).Office,
                Request = x.ClientRequest,
                AssistedBy = x.AssistedBy?.FullName,
                CustomerActionSheet = x
            }).ToList();
            gcCAS.DataSource = new BindingList<CASViewModel>(cas);
        }

        private void UCCAS_Load(object sender, System.EventArgs e)
        {
            if(filterText != null) gridCAS.ActiveFilterCriteria = new BinaryOperator("Id", filterText);
        }

        private void LoadDetails()
        {
            var row = (CASViewModel)gridCAS.GetFocusedRow();
            txtDate.DateTime = row.DateCreated;
            txtName.Text = row.CustomerActionSheet.ClientName;
            txtOffice.Text = row.Office;
            txtContactNo.Text = row.CustomerActionSheet.ContactNo;
            rdbtnGender.SelectedIndex = (int)row.CustomerActionSheet.Gender;
            txtClientRequest.Text = row.CustomerActionSheet.ClientRequest;
            txtActionTaken.Text = row.CustomerActionSheet.ActionTaken;
            txtAssistedBy.Text = row.CustomerActionSheet.AssistedBy.FullName;
        }

        private void LoadActions()
        {
            var row = (CASViewModel)gridCAS.GetFocusedRow();
            tabAction.Controls.Clear();
            tabAction.Controls.Add(new UCActions(new ActionType { Id = row.Id, RequestType = RequestType.CAS })
            {
                Dock = System.Windows.Forms.DockStyle.Fill
            });
        }

        private void LoadEvaluationSheet()
        {
            var row = (CASViewModel)gridCAS.GetFocusedRow();
            tabEvaluation.Controls.Clear();
            tabEvaluation.Controls.Add(new UCEvaluationSheet(new ActionType { Id = row.Id, RequestType = RequestType.CAS })
            {
                Dock = System.Windows.Forms.DockStyle.Fill
            });
        }
        private void btnEdit_Click(object sender, System.EventArgs e)
        {
            var row = (CASViewModel)gridCAS.GetFocusedRow();
            var frm = new frmAddEditCAS(row);
            frm.ShowDialog();

            LoadCAS();
            LoadDetails();
        }

        private void btnAdd_Click(object sender, System.EventArgs e)
        {
            var frm = new frmAddEditCAS();
            frm.ShowDialog();

            LoadCAS();
        }

        private void gridCAS_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            var row = (CASViewModel)gridCAS.GetFocusedRow();
            if (row == null) return;

            lblCASNo.Text = row.Id.ToString();
            LoadDetails();
            LoadActions();
            LoadEvaluationSheet();
        }
    }
}
