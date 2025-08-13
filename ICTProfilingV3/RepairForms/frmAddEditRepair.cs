using Helpers.Interfaces;
using ICTProfilingV3.ActionsForms;
using ICTProfilingV3.BaseClasses;
using ICTProfilingV3.Core.Common;
using ICTProfilingV3.DataTransferModels.Models;
using ICTProfilingV3.DataTransferModels.ViewModels;
using ICTProfilingV3.PPEInventoryForms;
using ICTProfilingV3.Services.Employees;
using Microsoft.Extensions.DependencyInjection;
using Models.Entities;
using Models.Enums;
using Models.Repository;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ICTProfilingV3.RepairForms
{
    public partial class frmAddEditRepair : BaseForm, IModifyTicketStatus
    {
        private readonly IUnitOfWork _unitOfWork;
        private SaveType saveType;
        private readonly UserStore _userStore;
        private readonly IServiceProvider _serviceProvider;

        private Repairs _Repairs;
        private int repairId;
        private bool IsSave = false;
        private UCAddPPEEquipment UCAddPPEEquipment;

        public frmAddEditRepair(UserStore userStore, IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _userStore = userStore;
            _serviceProvider = serviceProvider;
            _unitOfWork = new UnitOfWork();
            LoadDropdowns();
        }

        public void InitForm(int? repair = null)
        {
            if(repair == null)
            {
                saveType = SaveType.Insert;
                CreateTicket();
            }
            else
            {
                saveType = SaveType.Update;
                IsSave = true;
                repairId = repair.Value;
            }
        }

        private async Task LoadDetails()
        {
            var repair = await _unitOfWork.RepairsRepo.FindAsync(x => x.Id == repairId);    
            _Repairs = repair;

            if(repair.DateCreated != null) txtDate.DateTime = (DateTime)repair.DateCreated;
            slueEmployee.EditValue = repair.RequestedById;
            rdbtnGender.SelectedIndex = (int)repair.Gender;
            txtContactNo.Text = repair.ContactNo;
            if (repair.DateDelivered != null) txtDateofDelivery.DateTime = (DateTime)repair.DateDelivered;
            slueDeliveredBy.EditValue = repair.DeliveredById;
            sluePropertyNo.EditValue = repair.PPEsId;
            txtRequestProblem.Text = repair.Problems;
        }

        private void CreateTicket()
        {
            var ticket = new TicketRequest()
            {
                DateCreated = DateTime.Now,
                TicketStatus = TicketStatus.Accepted,
                RequestType = RequestType.Repairs,
                CreatedBy = _userStore.UserId
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

        private void LoadDropdowns()
        {
            var employees = HRMISEmployees.GetEmployees();
            slueEmployee.Properties.DataSource = employees;
            slueDeliveredBy.Properties.DataSource = employees;

            txtDate.DateTime = DateTime.Now;
            txtDateofDelivery.DateTime = DateTime.Now;
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
            }).ToList();

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

            int repId;
            if (row == null) repId = (int)_Repairs.PPEsId;
            else repId = row.Id;

            var ppe = await _unitOfWork.PPesRepo.FindAsync(x => x.Id == repId);
            gcEquipmentSpecs.Controls.Clear();

            var equipmentSpecsForm = new UCAddPPEEquipment(ppe , false)
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
            await Save();
            IsSave = true;
            this.Close();
            if (saveType == SaveType.Update) return;
            var actionType = new ActionType
            {
                Id = _Repairs.Id,
                RequestType = RequestType.Repairs
            };

            var frm = _serviceProvider.GetRequiredService<frmDocAction>();
            frm.SetActionBehavior(actionType, SaveType.Insert, null, null);
            frm.ShowDialog();
        }
        private async Task Save()
        {
            var reqEmployee = HRMISEmployees.GetEmployeeById((long?)slueEmployee.EditValue);
            var repair = await _unitOfWork.RepairsRepo.FindAsync(x => x.Id == _Repairs.Id);
            repair.RequestedById = (long)slueEmployee.EditValue;
            repair.ReqByChiefId = HRMISEmployees.GetChief(reqEmployee.Office, reqEmployee.Division, (long?)slueEmployee.EditValue).ChiefId;
            repair.DeliveredById = (long)slueDeliveredBy.EditValue;
            repair.Problems = txtRequestProblem.Text;
            repair.Gender = (Gender)rdbtnGender.SelectedIndex;
            repair.ContactNo = txtContactNo.Text;
            repair.PPEsId = (int?)sluePropertyNo.EditValue;
            repair.DateCreated = txtDate.DateTime;
            repair.DateDelivered = txtDateofDelivery.DateTime;
            _unitOfWork.RepairsRepo.Update(repair);
            await _unitOfWork.SaveChangesAsync();
            await ModifyTicketStatusStatus(TicketStatus.Accepted, repair.Id);
        }

        private async void frmAddEditRepair_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!IsSave) await DeleteRepair();
        }

        private async Task DeleteRepair()
        {
            _unitOfWork.TicketRequestRepo.DeleteByEx(x => x.Id == _Repairs.Id);
            await _unitOfWork.SaveChangesAsync();
            _unitOfWork.RepairsRepo.DeleteByEx(x => x.Id == _Repairs.Id);
            await _unitOfWork.SaveChangesAsync();
        }

        private async void frmAddEditRepair_Load(object sender, EventArgs e)
        {
            await LoadPPEs();
            if (saveType == SaveType.Update) await LoadDetails();
        }

        private void groupControl3_Paint(object sender, PaintEventArgs e)
        {

        }

        public async Task ModifyTicketStatusStatus(TicketStatus status, int Id)
        {
            var ticketStatus = new TicketRequestStatus
            {
                Status = status,
                DateStatusChanged = DateTime.Now,
                ChangedByUserId = _userStore.UserId,
                TicketRequestId = Id
            };
            _unitOfWork.TicketRequestStatusRepo.Insert(ticketStatus);
            await _unitOfWork.SaveChangesAsync();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}