namespace ICTProfilingV3.StandardPRForms
{
    partial class UCStandardPR
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
            DevExpress.XtraGrid.GridLevelNode gridLevelNode1 = new DevExpress.XtraGrid.GridLevelNode();
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions1 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject3 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject4 = new DevExpress.Utils.SerializableAppearanceObject();
            this.gridSpecsDetails = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.bandedGridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.bandedGridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.bandedGridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcPR = new DevExpress.XtraGrid.GridControl();
            this.gridSpecs = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridAddSpecs = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemButtonEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnExpandDetail = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.gridDelete = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnDeleteEquipment = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.gridMark = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ceMark = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            ((System.ComponentModel.ISupportInitialize)(this.gridSpecsDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcPR)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridSpecs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExpandDetail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnDeleteEquipment)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ceMark)).BeginInit();
            this.SuspendLayout();
            // 
            // gridSpecsDetails
            // 
            this.gridSpecsDetails.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.bandedGridColumn1,
            this.bandedGridColumn2,
            this.bandedGridColumn3});
            this.gridSpecsDetails.GridControl = this.gcPR;
            this.gridSpecsDetails.Name = "gridSpecsDetails";
            this.gridSpecsDetails.OptionsView.ShowGroupPanel = false;
            // 
            // bandedGridColumn1
            // 
            this.bandedGridColumn1.Caption = "ItemNo";
            this.bandedGridColumn1.FieldName = "ItemNo";
            this.bandedGridColumn1.Name = "bandedGridColumn1";
            this.bandedGridColumn1.OptionsColumn.FixedWidth = true;
            this.bandedGridColumn1.Visible = true;
            this.bandedGridColumn1.VisibleIndex = 0;
            this.bandedGridColumn1.Width = 93;
            // 
            // bandedGridColumn2
            // 
            this.bandedGridColumn2.Caption = "Specs";
            this.bandedGridColumn2.FieldName = "Specs";
            this.bandedGridColumn2.Name = "bandedGridColumn2";
            this.bandedGridColumn2.Visible = true;
            this.bandedGridColumn2.VisibleIndex = 1;
            this.bandedGridColumn2.Width = 403;
            // 
            // bandedGridColumn3
            // 
            this.bandedGridColumn3.Caption = "Description";
            this.bandedGridColumn3.FieldName = "Description";
            this.bandedGridColumn3.Name = "bandedGridColumn3";
            this.bandedGridColumn3.Visible = true;
            this.bandedGridColumn3.VisibleIndex = 2;
            this.bandedGridColumn3.Width = 611;
            // 
            // gcPR
            // 
            this.gcPR.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcPR.EmbeddedNavigator.Buttons.Append.Visible = false;
            this.gcPR.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            this.gcPR.EmbeddedNavigator.Buttons.Edit.Visible = false;
            this.gcPR.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            this.gcPR.EmbeddedNavigator.Buttons.Remove.Visible = false;
            gridLevelNode1.LevelTemplate = this.gridSpecsDetails;
            gridLevelNode1.RelationName = "Specifications";
            this.gcPR.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode1});
            this.gcPR.Location = new System.Drawing.Point(0, 0);
            this.gcPR.MainView = this.gridSpecs;
            this.gcPR.Name = "gcPR";
            this.gcPR.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemButtonEdit1,
            this.btnExpandDetail,
            this.btnDeleteEquipment,
            this.ceMark});
            this.gcPR.Size = new System.Drawing.Size(1217, 332);
            this.gcPR.TabIndex = 155;
            this.gcPR.UseEmbeddedNavigator = true;
            this.gcPR.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridSpecs,
            this.gridSpecsDetails});
            // 
            // gridSpecs
            // 
            this.gridSpecs.Appearance.Row.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridSpecs.Appearance.Row.Options.UseFont = true;
            this.gridSpecs.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn5,
            this.gridColumn6,
            this.gridColumn7,
            this.gridAddSpecs,
            this.gridColumn8,
            this.gridDelete,
            this.gridMark,
            this.gridColumn1,
            this.gridColumn2});
            this.gridSpecs.GridControl = this.gcPR;
            this.gridSpecs.Name = "gridSpecs";
            this.gridSpecs.OptionsView.ShowDetailButtons = false;
            this.gridSpecs.OptionsView.ShowGroupedColumns = true;
            this.gridSpecs.OptionsView.ShowGroupExpandCollapseButtons = false;
            this.gridSpecs.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "ItemNo";
            this.gridColumn3.FieldName = "ItemNo";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.AllowEdit = false;
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 1;
            this.gridColumn3.Width = 42;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "Equipment";
            this.gridColumn4.FieldName = "Equipment";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.OptionsColumn.AllowEdit = false;
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 3;
            this.gridColumn4.Width = 174;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "Description";
            this.gridColumn5.FieldName = "StandardPRSpecs.Description";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.OptionsColumn.AllowEdit = false;
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 4;
            this.gridColumn5.Width = 197;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "Unit Cost Range";
            this.gridColumn6.DisplayFormat.FormatString = "#.00";
            this.gridColumn6.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn6.FieldName = "StandardPRSpecs.UnitCost";
            this.gridColumn6.FieldNameSortGroup = "PREquipment.UnitCost";
            this.gridColumn6.GroupInterval = DevExpress.XtraGrid.ColumnGroupInterval.Value;
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.OptionsColumn.AllowEdit = false;
            this.gridColumn6.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.True;
            this.gridColumn6.SortMode = DevExpress.XtraGrid.ColumnSortMode.Custom;
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 5;
            this.gridColumn6.Width = 147;
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "Remarks";
            this.gridColumn7.FieldName = "StandardPRSpecs.Remarks";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.OptionsColumn.AllowEdit = false;
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 7;
            this.gridColumn7.Width = 383;
            // 
            // gridAddSpecs
            // 
            this.gridAddSpecs.ColumnEdit = this.repositoryItemButtonEdit1;
            this.gridAddSpecs.Name = "gridAddSpecs";
            this.gridAddSpecs.OptionsColumn.FixedWidth = true;
            this.gridAddSpecs.Width = 46;
            // 
            // repositoryItemButtonEdit1
            // 
            this.repositoryItemButtonEdit1.AutoHeight = false;
            this.repositoryItemButtonEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph)});
            this.repositoryItemButtonEdit1.Name = "repositoryItemButtonEdit1";
            this.repositoryItemButtonEdit1.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            // 
            // gridColumn8
            // 
            this.gridColumn8.ColumnEdit = this.btnExpandDetail;
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.OptionsColumn.FixedWidth = true;
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 0;
            this.gridColumn8.Width = 35;
            // 
            // btnExpandDetail
            // 
            this.btnExpandDetail.AutoHeight = false;
            editorButtonImageOptions1.Image = global::ICTProfilingV3.Properties.Resources.about_16x168;
            this.btnExpandDetail.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, editorButtonImageOptions1, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, serializableAppearanceObject2, serializableAppearanceObject3, serializableAppearanceObject4, "", null, null, DevExpress.Utils.ToolTipAnchor.Default)});
            this.btnExpandDetail.ContextImageOptions.Image = global::ICTProfilingV3.Properties.Resources.about_16x169;
            this.btnExpandDetail.Name = "btnExpandDetail";
            this.btnExpandDetail.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            this.btnExpandDetail.Click += new System.EventHandler(this.btnExpandDetail_Click);
            // 
            // gridDelete
            // 
            this.gridDelete.ColumnEdit = this.btnDeleteEquipment;
            this.gridDelete.Name = "gridDelete";
            this.gridDelete.Width = 32;
            // 
            // btnDeleteEquipment
            // 
            this.btnDeleteEquipment.AutoHeight = false;
            this.btnDeleteEquipment.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph)});
            this.btnDeleteEquipment.Name = "btnDeleteEquipment";
            this.btnDeleteEquipment.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            // 
            // gridMark
            // 
            this.gridMark.FieldName = "Mark";
            this.gridMark.Name = "gridMark";
            this.gridMark.Width = 36;
            // 
            // gridColumn1
            // 
            this.gridColumn1.FieldName = "Quantity";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 2;
            this.gridColumn1.Width = 78;
            // 
            // gridColumn2
            // 
            this.gridColumn2.DisplayFormat.FormatString = "#.00";
            this.gridColumn2.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn2.FieldName = "TotalCost";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 6;
            this.gridColumn2.Width = 144;
            // 
            // ceMark
            // 
            this.ceMark.AutoHeight = false;
            this.ceMark.Name = "ceMark";
            // 
            // UCStandardPR
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gcPR);
            this.Name = "UCStandardPR";
            this.Size = new System.Drawing.Size(1217, 332);
            this.Load += new System.EventHandler(this.UCStandardPR_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridSpecsDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcPR)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridSpecs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExpandDetail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnDeleteEquipment)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ceMark)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gcPR;
        private DevExpress.XtraGrid.Views.Grid.GridView gridSpecsDetails;
        private DevExpress.XtraGrid.Columns.GridColumn bandedGridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn bandedGridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn bandedGridColumn3;
        private DevExpress.XtraGrid.Views.Grid.GridView gridSpecs;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn gridAddSpecs;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repositoryItemButtonEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit btnExpandDetail;
        private DevExpress.XtraGrid.Columns.GridColumn gridDelete;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit btnDeleteEquipment;
        private DevExpress.XtraGrid.Columns.GridColumn gridMark;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit ceMark;
    }
}
