using Models.Entities;
using Models.Repository;
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace ICTProfilingV3.ActionsForms
{
    public partial class frmActionList : DevExpress.XtraEditors.XtraForm
    {
        private readonly IUnitOfWork unitOfWork;
        public frmActionList()
        {
            InitializeComponent();
            unitOfWork = new UnitOfWork();
            LoadActionLists();
        }

        private void LoadActionLists()
        {
            var res = unitOfWork.ActionTakenRepo.GetAll().ToList();
            gcActionTaken.DataSource = new BindingList<ActionTaken>(res);
        }

        private async void gridActionTaken_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            var row = (ActionTaken)gridActionTaken.GetFocusedRow();
            var res = await unitOfWork.ActionsRepo.FindAsync(x => x.Id == row.Id);
            if (res == null) InsertAction(row);
            else UpdateAction(row);
            LoadActionLists();
        }

        private async void UpdateAction(ActionTaken row)
        {
            var res = await unitOfWork.ActionTakenRepo.FindAsync(x => x.Id == row.Id);
            res.Action = row.Action;
            unitOfWork.Save();
        }

        private void InsertAction(ActionTaken row)
        {
            unitOfWork.ActionTakenRepo.Insert(row);
            unitOfWork.Save();
        }
    }
}