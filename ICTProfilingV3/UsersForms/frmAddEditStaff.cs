using DevExpress.XtraEditors;
using EntityManager.Managers.User;
using Models.Entities;
using Models.Enums;
using Models.HRMISEntites;
using Models.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ICTProfilingV3.UsersForms
{
    public partial class frmAddEditStaff : DevExpress.XtraEditors.XtraForm
    {
        private readonly IICTUserManager userManager;
        private readonly IUnitOfWork unitOfWork;
        public frmAddEditStaff()
        {
            InitializeComponent();
            userManager = new ICTUserManager();
            unitOfWork = new UnitOfWork();
            LoadDropdowns();
        }

        private void LoadDropdowns()
        {
            slueUser.Properties.DataSource = userManager.GetUsers();
            lueSection.Properties.DataSource = Enum.GetValues(typeof(Sections)).Cast<Sections>().Select(x => new
            {
                Id = x,
                Section = EnumHelper.GetEnumDescription(x)
            });
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var staff = new ITStaff
            {
                ImagePath = "",
                Section = (Sections)lueSection.EditValue,
                UserId = (string)slueUser.EditValue
            };
            unitOfWork.ITStaffRepo.Insert(staff);
            unitOfWork.Save();

            this.Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}