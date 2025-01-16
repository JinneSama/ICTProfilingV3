using DevExpress.Data.Filtering;
using ICTProfilingV3.ActionsForms;
using Models.Entities;
using Models.Enums;
using Models.HRMISEntites;
using Models.Models;
using Models.Repository;
using Models.ViewModels;
using System.Linq;

namespace ICTProfilingV3.MOForms
{
    public partial class UCMOAccountUserRequests : DevExpress.XtraEditors.XtraUserControl
    {
        private IUnitOfWork unitOfWork;
        public string filterText { get; set; }
        public UCMOAccountUserRequests()
        {
            InitializeComponent();
            unitOfWork = new UnitOfWork();
            LoadData();
        }

        private void LoadData()
        {
            var users = unitOfWork.MOAccountUserRepo.GetAll(x => x.MOAccount,
                x => x.MOAccount.Office,
                x => x.PPE);
            gcMOAccountUsers.DataSource = users.ToList();
        }

        private void LoadDetails()
        {
            var row = (MOAccountUsers)gridMOAccountUsers.GetFocusedRow();

            txtDateOfInstallation.Text = row?.DateOfInstallation.Value.ToShortDateString();
            txtProcuredDate.Text = row?.ProcuredDate.Value.ToShortDateString();
            txtDeviceNo.Text = row?.DeviceNo.ToString();
            txtPropertyNo.Text = row?.PPE?.PropertyNo;
            txtDescription.Text = row?.PPE?.Remarks;
            
            var IssuedTo = HRMISEmployees.GetEmployeeById(row.IssuedTo);
            var User = HRMISEmployees.GetEmployeeById(row.AccountUser);

            txtIssuedTo.Text = IssuedTo.Employee;
            txtIssuedToOffice.Text = IssuedTo.Office + " " + IssuedTo.Division;
            txtUser.Text = User.Employee;
            txtUserOffice.Text = User.Office + " " + User.Division;
        }

        private void btnEdit_Click(object sender, System.EventArgs e)
        {
            var row = (MOAccountUsers)gridMOAccountUsers.GetFocusedRow();
            
            var frm = new frmAddEditAccountUsers(row);
            frm.ShowDialog();
            LoadData();
        }
        private void LoadActions()
        {
            var row = (MOAccountUsers)gridMOAccountUsers.GetFocusedRow();
            tabAction.Controls.Clear();
            tabAction.Controls.Add(new UCActions(new ActionType { Id = row.Id, RequestType = RequestType.M365 })
            {
                Dock = System.Windows.Forms.DockStyle.Fill
            });
        }

        private void gridMOAccountUsers_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            var row = (MOAccountUsers)gridMOAccountUsers.GetFocusedRow();
            if (row == null) return;

            lblEpisNo.Text = "M365-" + row.Id.ToString();
            LoadDetails();
            LoadActions();
        }

        private void UCMOAccountUserRequests_Load(object sender, System.EventArgs e)
        {
            if (filterText != null) gridMOAccountUsers.ActiveFilterCriteria = new BinaryOperator("Id", filterText);
        }
    }
}
