using DevExpress.Data.Filtering;
using ICTProfilingV3.ActionsForms;
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
        }

        private async Task LoadCAS()
        {
            var res = await _unitOfWork.CustomerActionSheetRepo.GetAll(x => x.AssistedBy).ToListAsync();
            var cas = res.Select(x => new CASViewModel
            {
                Id = x.Id,
                DateCreated = x.DateCreated ?? System.DateTime.MinValue,
                Office = x.ClientId == null ? x.Office : HRMISEmployees.GetEmployeeById(x.ClientId).Office,
                Request = x.ClientRequest,
                AssistedBy = x.AssistedBy?.FullName,
                CustomerActionSheet = x
            });
            gcCAS.DataSource = new BindingList<CASViewModel>(cas.ToList());
        }

        private async void UCCAS_Load(object sender, System.EventArgs e)
        {
            await LoadCAS();
            if(filterText != null) gridCAS.ActiveFilterCriteria = new BinaryOperator("Id", filterText);
        }

        private void gridCAS_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            var row = (CASViewModel)gridCAS.GetFocusedRow();
            lblCASNo.Text = row.Id.ToString();
            LoadDetails();
            LoadActions();
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

        private async void btnEdit_Click(object sender, System.EventArgs e)
        {
            var row = (CASViewModel)gridCAS.GetFocusedRow();
            var frm = new frmAddEditCAS(_unitOfWork,row);
            frm.ShowDialog();

            await LoadCAS();
            LoadDetails();
        }

        private async void btnAdd_Click(object sender, System.EventArgs e)
        {
            var frm = new frmAddEditCAS(_unitOfWork);
            frm.ShowDialog();

            await LoadCAS();
        }
    }
}
