using DevExpress.Utils.Extensions;
using DevExpress.XtraEditors;
using Helpers.NetworkFolder;
using Models.Repository;
using Models.ViewModels;
using System;
using System.Data;
using System.Linq;
namespace ICTProfilingV3.UsersForms
{
    public partial class frmStaff : DevExpress.XtraEditors.XtraForm
    {
        private readonly IUnitOfWork unitOfWork;
        private DocumentHandler documentHandler;
        public frmStaff()
        {
            InitializeComponent();
            unitOfWork = new UnitOfWork();
            documentHandler = new DocumentHandler(Properties.Settings.Default.StaffNetworkPath,
                Properties.Settings.Default.NetworkUsername,
                Properties.Settings.Default.NetworkPassword);
            LoadStaff();
        }

        private void LoadAssignedTo(StaffViewModel row)
        {
            gcAssigned.DataSource = null;
            if (row == null) return;

            var res = row.Staff.TicketRequests;
            gcAssigned.DataSource = res;    
        }

        private void LoadProcessCount(StaffViewModel row)
        {
            if(row == null) return;
            var requests = row.Staff.TicketRequests;
            var res = (FormatConditionRuleDataBar)gridTickets.FormatRules[0].Rule;
            res.Maximum = 100;
            gridTickets.FormatRules[0].Rule = res;
            gcTickets.DataSource = requests.GroupBy(x => x.RequestType)
                .Select(x => new
                {
                    process = x.Key,
                    count = x.Count()
                }).ToList();
        }

        private void LoadStaff()
        {
            var res = unitOfWork.ITStaffRepo.GetAll(x => x.TicketRequests,
                x => x.Users).ToList().Select(x => new StaffViewModel
            {
                Staff = x,
                Image = documentHandler.GetImage(x.UserId + ".jpeg")
            });
            gcStaff.DataSource = res.ToList();  
        }

        private void btnAddStaff_Click(object sender, EventArgs e)
        {
            var frm = new frmAddEditStaff();
            frm.ShowDialog();

            LoadStaff();
        }

        private void tvStaff_CustomItemTemplate(object sender, DevExpress.XtraGrid.Views.Tile.TileViewCustomItemTemplateEventArgs e)
        {
            var row = (StaffViewModel)tvStaff.GetRow(e.RowHandle);
            if (row.Image == null) e.HtmlTemplate = tvStaff.TileHtmlTemplates[1];
            else e.HtmlTemplate = tvStaff.TileHtmlTemplates[0];
        }

        private void tvStaff_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            var row = (StaffViewModel)tvStaff.GetRow(e.RowHandle);
            LoadAssignedTo(row);
            LoadProcessCount(row);
        }

        private void btnEditStaff_Click(object sender, EventArgs e)
        {
            var row = (StaffViewModel)tvStaff.GetFocusedRow();
            var frm = new frmAddEditStaff(row);
            frm.ShowDialog();

            LoadStaff();
        }
    }
}