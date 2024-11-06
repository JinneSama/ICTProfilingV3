using DevExpress.Utils.Drawing;
using ICTProfilingV3.DeliveriesForms;
using ICTProfilingV3.PPEInventoryForms;
using Models.Entities;
using Models.Enums;
using Models.HRMISEntites;
using Models.Repository;
using Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ICTProfilingV3.RepairForms
{
    public partial class frmAddEditRepair : DevExpress.XtraEditors.XtraForm
    {
        private readonly IUnitOfWork _unitOfWork;
        private Repairs _Repairs;
        private bool IsSave = false;
        private UCAddPPEEquipment UCAddPPEEquipment;
        public frmAddEditRepair()
        {
            InitializeComponent();
            _unitOfWork = new UnitOfWork();
            CreateTicket();
            LoadDropdowns();
        }
        private void CreateTicket()
        {
            var ticket = new TicketRequest()
            {
                DateCreated = DateTime.UtcNow,
                TicketStatus = TicketStatus.Accepted
            };
            _unitOfWork.TicketRequestRepo.Insert(ticket);
            _unitOfWork.Save();

            var repairs = new Repairs()
            {
                Id = ticket.Id
            };

            _unitOfWork.RepairsRepo.Insert(repairs);
            _unitOfWork.Save();

            _Repairs = repairs;
        }

        private async void LoadDropdowns()
        {
            var employees = HRMISEmployees.GetEmployees();
            slueEmployee.Properties.DataSource = employees;
            slueDeliveredBy.Properties.DataSource = employees;
            await LoadPPEs();
        }
        private async Task LoadPPEs()
        {
            var ppe = await _unitOfWork.PPesRepo.GetAll().ToListAsync();

            var ppeModel = ppe.Select(x => new PPEsViewModel
            {
                Id = x.Id,
                PropertyNo = x.PropertyNo,
                IssuedTo = HRMISEmployees.GetEmployeeById(x.IssuedToId)?.Employee,
                DateCreated = x.DateCreated,
                Office = HRMISEmployees.GetEmployeeById(x.IssuedToId)?.Office,
                Status = x?.Status
            });
            sluePropertyNo.Properties.DataSource = ppeModel;
        }

        private async void btnAddPPE_Click(object sender, System.EventArgs e)
        {
            var frm = new frmAddEditPPEs(SaveType.Insert , null);
            frm.ShowDialog();

            await LoadPPEs();
        }

        private async void sluePropertyNo_EditValueChanged(object sender, EventArgs e)
        {
            await LoadEquipmentSpecs();
        }

        private async Task LoadEquipmentSpecs()
        {
            var row = (PPEsViewModel)sluePropertyNo.Properties.View.GetFocusedRow();
            var ppe = await _unitOfWork.PPesRepo.FindAsync(x => x.Id == row.Id);
            gcEquipmentSpecs.Controls.Clear();

            var equipmentSpecsForm = new UCAddPPEEquipment(ppe, _unitOfWork)
            {
                Dock = DockStyle.Fill
            };
            UCAddPPEEquipment = equipmentSpecsForm;

            gcEquipmentSpecs.Controls.Add(equipmentSpecsForm);

            txtIssuedTo.Text = HRMISEmployees.GetEmployeeById(ppe.IssuedToId)?.Employee;
            txtOffAcr.Text = HRMISEmployees.GetEmployeeById(ppe.IssuedToId)?.Office;
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            var resPPEEquipment = await SavePPEEquipment();
            if (resPPEEquipment == null) return;

            await Save(resPPEEquipment.FirstOrDefault());
            IsSave = true;
            this.Close();
        }

        private async Task<List<PPEsSpecsViewModel>> SavePPEEquipment()
        {
            List<PPEsSpecsViewModel> markedPPEsSpecs = await UCAddPPEEquipment.RetrieveSelectedEquipment();
            if (markedPPEsSpecs.Count > 1 || markedPPEsSpecs.Count <= 0) 
            {
                MessageBox.Show("Plese Select 1 Equipment only!");
                return null;
            }
            return markedPPEsSpecs;
        }

        private async Task Save(PPEsSpecsViewModel pPEsSpecsViewModel)
        {
            var reqEmployee = (EmployeesViewModel)slueEmployee.Properties.View.GetFocusedRow();
            var repair = await _unitOfWork.RepairsRepo.FindAsync(x => x.Id == _Repairs.Id);
            repair.RequestedById = (long)reqEmployee.Id;
            repair.ReqByChiefId = HRMISEmployees.GetChief(reqEmployee.Office, reqEmployee.Division).ChiefId;
            repair.DeliveredById = (long)slueDeliveredBy.EditValue;
            repair.Problems = txtRequestProblem.Text;
            repair.Gender = (Gender)rdbtnGender.SelectedIndex;
            repair.ContactNo = txtContactNo.Text;
            repair.PPEsId = (int?)sluePropertyNo.EditValue;
            repair.PPESpecsId = pPEsSpecsViewModel.Id;

            await _unitOfWork.SaveChangesAsync();
        }

        private async void frmAddEditRepair_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!IsSave) await DeleteRepair();
        }

        private async Task DeleteRepair()
        {
            await _unitOfWork.TicketRequestRepo.DeleteByEx(x => x.Id == _Repairs.Id);
            await _unitOfWork.SaveChangesAsync();
            await _unitOfWork.RepairsRepo.DeleteByEx(x => x.Id == _Repairs.Id);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}