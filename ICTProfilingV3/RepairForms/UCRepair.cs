using DevExpress.XtraEditors;
using EntityManager.Managers.User;
using ICTProfilingV3.ActionsForms;
using Models.Entities;
using Models.Enums;
using Models.HRMISEntites;
using Models.Models;
using Models.Repository;
using Models.ViewModels;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ICTProfilingV3.RepairForms
{
    public partial class UCRepair : DevExpress.XtraEditors.XtraUserControl
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IICTUserManager _userManager;  
        public UCRepair()
        {
            InitializeComponent();
            _unitOfWork = new UnitOfWork();
            _userManager = new ICTUserManager();
            LoadRepair();
        }

        private void LoadRepair()
        {
            var res = _unitOfWork.RepairsRepo.GetAll(x => x.TicketRequest,
                x => x.PPEs).ToList();
            var repair = res.Select(x => new RepairViewModel
            {
                Id = x.Id,
                Status = x.TicketRequest.TicketStatus,
                DateCreated = x.DateCreated,
                PropertyNo = x.PPEs?.PropertyNo,
                IssuedTo = HRMISEmployees.GetEmployeeById(x.RequestedById)?.Employee,
                Office = HRMISEmployees.GetEmployeeById(x.RequestedById)?.Office,
                RepairId = "EPiS-" + x.Id
            });
            gcRepair.DataSource = repair; 
        }

        private void LoadActions()
        {
            var row = (RepairViewModel)gridRepair.GetFocusedRow();
            tabAction.Controls.Clear();
            tabAction.Controls.Add(new UCActions(new ActionType
            {
                Id = row.Id,
                RequestType = RequestType.Repairs,
            })
            {
                Dock = DockStyle.Fill
            });
        }
        private async Task LoadRepairDetails()
        {
            ClearAllFields();
            var row = (RepairViewModel)gridRepair.GetFocusedRow();
            var repair = await _unitOfWork.RepairsRepo.FindAsync(x => x.Id == row.Id,
                x => x.PPEsSpecs);
            if (repair == null) return;

            var employee = HRMISEmployees.GetEmployeeById(repair.RequestedById);
            var chief = HRMISEmployees.GetChief(employee?.Office, employee?.Division);

            lblRepairNo.Text = repair.Id.ToString();
            txtOffice.Text = string.Join(" ", employee?.Office , employee?.Division);
            txtChief.Text = HRMISEmployees.GetEmployeeById(chief?.ChiefId)?.Employee;
            txtReqBy.Text = employee?.Employee;
            txtContactNo.Text = repair.ContactNo;
            txtDeliveredBy.Text = HRMISEmployees.GetEmployeeById(repair.DeliveredById)?.Employee;

            txtFindings.Text = repair.Findings;
            txtRecommendation.Text = repair.Recommendations;
            txtRequestProblem.Text = repair.Problems;
            var prepared = await _userManager.FindUserAsync(repair.PreparedById);
            txtPreparedBy.Text = prepared?.FullName;
            var assesed = await _userManager.FindUserAsync(repair.ReviewedById);
            txtAssessedBy.Text = assesed?.FullName;
            var noted = await _userManager.FindUserAsync(repair.NotedById);
            txtNotedBy.Text = noted?.FullName;

            await LoadEquipmentDetails(repair.PPEs , repair.PPEsSpecs);
        }

        private async Task LoadEquipmentDetails(PPEs ppe, PPEsSpecs ppeSpecs)
        {
            if(ppe == null) return;
            txtPropertyNo.Text = ppe.PropertyNo;   
            txtIssuedTo.Text = HRMISEmployees.GetEmployeeById(ppe.IssuedToId)?.Employee;
            txtStatus.Text = ppe.Status.ToString();

            if (ppeSpecs == null) return;
            var specs = await _unitOfWork.PPEsSpecsRepo.FindAsync(x => x.Id == ppeSpecs.Id,
                x => x.Model.Brand,
                x => x.Model.Brand.EquipmentSpecs,
                x => x.Model.Brand.EquipmentSpecs.Equipment);

            if(specs == null) return;
            txtEquipment.Text = specs.Model.Brand.EquipmentSpecs.Equipment.EquipmentName;
            txtDescription.Text = specs.Model.Brand.EquipmentSpecs.Description;
            txtBrand.Text = specs.Model.Brand.BrandName;
            txtModel.Text = specs.Model.ModelName;
            txtSerialNo.Text = specs.SerialNo;
        }

        private void ClearAllFields()
        {
            foreach (var item in this.Controls)
            {
                var ctrl = (Control)item;
                if (ctrl.HasChildren){
                    foreach (var child in ctrl.Controls)
                    {
                        if (child.GetType() == typeof(TextEdit))
                            ((TextEdit)child).Text = string.Empty;

                        if (child.GetType() == typeof(MemoEdit))
                            ((MemoEdit)child).Text = string.Empty;
                    }
                }
            }
        }

        private void btnEdit_Click(object sender, System.EventArgs e)
        {

        }

        private async void gridRepair_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            await LoadRepairDetails();
            LoadActions();
        }
    }
}
