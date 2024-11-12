using DevExpress.XtraEditors;
using Models.Repository;
using Models.ViewModels;
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
    public partial class frmStaff : DevExpress.XtraEditors.XtraForm
    {
        private readonly IUnitOfWork unitOfWork;
        public frmStaff()
        {
            InitializeComponent();
            unitOfWork = new UnitOfWork();
            LoadStaff();
        }

        private void LoadStaff()
        {
            var res = unitOfWork.UsersRepo.GetAll().Select(x => new StaffViewModel
            {
                Users = x
            });
            gcStaff.DataSource = res.ToList();  
        }

        private void btnAddStaff_Click(object sender, EventArgs e)
        {

        }
    }
}