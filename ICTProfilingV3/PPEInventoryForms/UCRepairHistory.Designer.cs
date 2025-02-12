namespace ICTProfilingV3.PPEInventoryForms
{
    partial class UCRepairHistory
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.gcHistory = new DevExpress.XtraGrid.GridControl();
            this.gridHistory = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.hplRepair = new DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gcHistory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridHistory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.hplRepair)).BeginInit();
            this.SuspendLayout();
            // 
            // gcHistory
            // 
            this.gcHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcHistory.EmbeddedNavigator.Buttons.Append.Visible = false;
            this.gcHistory.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            this.gcHistory.EmbeddedNavigator.Buttons.Edit.Visible = false;
            this.gcHistory.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            this.gcHistory.EmbeddedNavigator.Buttons.Remove.Visible = false;
            this.gcHistory.Location = new System.Drawing.Point(0, 0);
            this.gcHistory.MainView = this.gridHistory;
            this.gcHistory.Name = "gcHistory";
            this.gcHistory.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.hplRepair});
            this.gcHistory.Size = new System.Drawing.Size(836, 327);
            this.gcHistory.TabIndex = 0;
            this.gcHistory.UseEmbeddedNavigator = true;
            this.gcHistory.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridHistory});
            // 
            // gridHistory
            // 
            this.gridHistory.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn5});
            this.gridHistory.GridControl = this.gcHistory;
            this.gridHistory.Name = "gridHistory";
            this.gridHistory.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Repair ID";
            this.gridColumn1.ColumnEdit = this.hplRepair;
            this.gridColumn1.FieldName = "Id";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.FixedWidth = true;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 1;
            this.gridColumn1.Width = 59;
            // 
            // hplRepair
            // 
            this.hplRepair.AutoHeight = false;
            this.hplRepair.Name = "hplRepair";
            this.hplRepair.Click += new System.EventHandler(this.hplRepair_Click);
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Request/Problem";
            this.gridColumn2.FieldName = "Problems";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            this.gridColumn2.OptionsColumn.AllowFocus = false;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 2;
            this.gridColumn2.Width = 212;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Date of Repair";
            this.gridColumn3.FieldName = "DateCreated";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.AllowEdit = false;
            this.gridColumn3.OptionsColumn.AllowFocus = false;
            this.gridColumn3.OptionsColumn.FixedWidth = true;
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 0;
            this.gridColumn3.Width = 110;
            // 
            // gridColumn4
            // 
            this.gridColumn4.FieldName = "Findings";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 3;
            this.gridColumn4.Width = 213;
            // 
            // gridColumn5
            // 
            this.gridColumn5.FieldName = "Recommendations";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 4;
            this.gridColumn5.Width = 221;
            // 
            // UCRepairHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gcHistory);
            this.Name = "UCRepairHistory";
            this.Size = new System.Drawing.Size(836, 327);
            ((System.ComponentModel.ISupportInitialize)(this.gcHistory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridHistory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.hplRepair)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gcHistory;
        private DevExpress.XtraGrid.Views.Grid.GridView gridHistory;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit hplRepair;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
    }
}
