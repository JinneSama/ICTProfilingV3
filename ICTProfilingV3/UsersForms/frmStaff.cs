﻿using DevExpress.Utils.Extensions;
using DevExpress.XtraEditors;
using Helpers.NetworkFolder;
using Models.Repository;
using Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
namespace ICTProfilingV3.UsersForms
{
    public partial class frmStaff : DevExpress.XtraEditors.XtraForm
    {
        private readonly IUnitOfWork unitOfWork;
        private HTTPNetworkFolder networkFolder;
        public frmStaff()
        {
            InitializeComponent();
            unitOfWork = new UnitOfWork();
            networkFolder = new HTTPNetworkFolder();
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

        private async void LoadStaff()
        {
            var staffList = unitOfWork.ITStaffRepo.FindAllAsync(x => x.Users.IsDeleted == false ,x => x.TicketRequests, x => x.Users).ToList();
            var staffViewModels = new List<StaffViewModel>();
            foreach (var staff in staffList)
            {
                var image = await networkFolder.DownloadFile(staff.UserId + ".jpeg");
                staffViewModels.Add(new StaffViewModel
                {
                    Staff = staff,
                    Image = image
                });
            }
            gcStaff.DataSource = staffViewModels.ToList();  
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