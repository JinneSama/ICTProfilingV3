﻿namespace ICTProfilingV3.RepairForms
{
    partial class UCAddPPEEquipment
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCAddPPEEquipment));
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject3 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject4 = new DevExpress.Utils.SerializableAppearanceObject();
            this.gridSpecsDetails = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn11 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn12 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcEquipmentSpecs = new DevExpress.XtraGrid.GridControl();
            this.gridEquipmentSpecs = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridEditBtn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnInfo = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn20 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn21 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn13 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridMark = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gridSpecsDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcEquipmentSpecs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridEquipmentSpecs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnInfo)).BeginInit();
            this.SuspendLayout();
            // 
            // gridSpecsDetails
            // 
            this.gridSpecsDetails.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn10,
            this.gridColumn11,
            this.gridColumn12});
            this.gridSpecsDetails.GridControl = this.gcEquipmentSpecs;
            this.gridSpecsDetails.Name = "gridSpecsDetails";
            this.gridSpecsDetails.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn10
            // 
            this.gridColumn10.FieldName = "ItemNo";
            this.gridColumn10.Name = "gridColumn10";
            this.gridColumn10.OptionsColumn.FixedWidth = true;
            this.gridColumn10.Visible = true;
            this.gridColumn10.VisibleIndex = 0;
            this.gridColumn10.Width = 81;
            // 
            // gridColumn11
            // 
            this.gridColumn11.FieldName = "Specs";
            this.gridColumn11.Name = "gridColumn11";
            this.gridColumn11.Visible = true;
            this.gridColumn11.VisibleIndex = 1;
            this.gridColumn11.Width = 434;
            // 
            // gridColumn12
            // 
            this.gridColumn12.FieldName = "Description";
            this.gridColumn12.Name = "gridColumn12";
            this.gridColumn12.Visible = true;
            this.gridColumn12.VisibleIndex = 2;
            this.gridColumn12.Width = 574;
            // 
            // gcEquipmentSpecs
            // 
            this.gcEquipmentSpecs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gcEquipmentSpecs.EmbeddedNavigator.Buttons.Append.Visible = false;
            this.gcEquipmentSpecs.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            this.gcEquipmentSpecs.EmbeddedNavigator.Buttons.Edit.Visible = false;
            this.gcEquipmentSpecs.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            this.gcEquipmentSpecs.EmbeddedNavigator.Buttons.Remove.Visible = false;
            gridLevelNode1.LevelTemplate = this.gridSpecsDetails;
            gridLevelNode1.RelationName = "Specifications";
            this.gcEquipmentSpecs.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode1});
            this.gcEquipmentSpecs.Location = new System.Drawing.Point(0, 0);
            this.gcEquipmentSpecs.MainView = this.gridEquipmentSpecs;
            this.gcEquipmentSpecs.Name = "gcEquipmentSpecs";
            this.gcEquipmentSpecs.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.btnInfo});
            this.gcEquipmentSpecs.Size = new System.Drawing.Size(1266, 354);
            this.gcEquipmentSpecs.TabIndex = 90;
            this.gcEquipmentSpecs.UseEmbeddedNavigator = true;
            this.gcEquipmentSpecs.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridEquipmentSpecs,
            this.gridSpecsDetails});
            // 
            // gridEquipmentSpecs
            // 
            this.gridEquipmentSpecs.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridEditBtn,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn5,
            this.gridColumn1,
            this.gridColumn6,
            this.gridColumn7,
            this.gridColumn8,
            this.gridColumn9,
            this.gridColumn20,
            this.gridColumn21,
            this.gridColumn13,
            this.gridMark});
            this.gridEquipmentSpecs.GridControl = this.gcEquipmentSpecs;
            this.gridEquipmentSpecs.Name = "gridEquipmentSpecs";
            this.gridEquipmentSpecs.OptionsView.ShowDetailButtons = false;
            this.gridEquipmentSpecs.OptionsView.ShowGroupExpandCollapseButtons = false;
            this.gridEquipmentSpecs.OptionsView.ShowGroupPanel = false;
            // 
            // gridEditBtn
            // 
            this.gridEditBtn.Name = "gridEditBtn";
            this.gridEditBtn.OptionsColumn.FixedWidth = true;
            this.gridEditBtn.Width = 37;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Item Order";
            this.gridColumn2.FieldName = "ItemNo";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Width = 52;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Item No";
            this.gridColumn3.FieldName = "ItemNo";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.AllowEdit = false;
            this.gridColumn3.OptionsColumn.AllowFocus = false;
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 2;
            this.gridColumn3.Width = 60;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "Qty";
            this.gridColumn4.FieldName = "Quantity";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.OptionsColumn.AllowEdit = false;
            this.gridColumn4.OptionsColumn.AllowFocus = false;
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 3;
            this.gridColumn4.Width = 50;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "Unit";
            this.gridColumn5.FieldName = "Unit";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.OptionsColumn.AllowEdit = false;
            this.gridColumn5.OptionsColumn.AllowFocus = false;
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 4;
            this.gridColumn5.Width = 50;
            // 
            // gridColumn1
            // 
            this.gridColumn1.ColumnEdit = this.btnInfo;
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.FixedWidth = true;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 1;
            this.gridColumn1.Width = 35;
            // 
            // btnInfo
            // 
            this.btnInfo.AutoHeight = false;
            editorButtonImageOptions1.Image = ((System.Drawing.Image)(resources.GetObject("editorButtonImageOptions1.Image")));
            this.btnInfo.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, editorButtonImageOptions1, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, serializableAppearanceObject2, serializableAppearanceObject3, serializableAppearanceObject4, "", null, null, DevExpress.Utils.ToolTipAnchor.Default)});
            this.btnInfo.ContextImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnInfo.ContextImageOptions.Image")));
            this.btnInfo.Name = "btnInfo";
            this.btnInfo.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            this.btnInfo.Click += new System.EventHandler(this.btnInfo_Click);
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "Equipment";
            this.gridColumn6.FieldName = "Equipment";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.OptionsColumn.AllowEdit = false;
            this.gridColumn6.OptionsColumn.AllowFocus = false;
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 5;
            this.gridColumn6.Width = 125;
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "Description";
            this.gridColumn7.FieldName = "Description";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.OptionsColumn.AllowEdit = false;
            this.gridColumn7.OptionsColumn.AllowFocus = false;
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 6;
            this.gridColumn7.Width = 159;
            // 
            // gridColumn8
            // 
            this.gridColumn8.Caption = "Brand";
            this.gridColumn8.FieldName = "Brand";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.OptionsColumn.AllowEdit = false;
            this.gridColumn8.OptionsColumn.AllowFocus = false;
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 7;
            this.gridColumn8.Width = 204;
            // 
            // gridColumn9
            // 
            this.gridColumn9.Caption = "Model";
            this.gridColumn9.FieldName = "Model";
            this.gridColumn9.Name = "gridColumn9";
            this.gridColumn9.OptionsColumn.AllowEdit = false;
            this.gridColumn9.OptionsColumn.AllowFocus = false;
            this.gridColumn9.Visible = true;
            this.gridColumn9.VisibleIndex = 8;
            this.gridColumn9.Width = 217;
            // 
            // gridColumn20
            // 
            this.gridColumn20.Caption = "Unit Cost";
            this.gridColumn20.FieldName = "UnitCost";
            this.gridColumn20.Name = "gridColumn20";
            this.gridColumn20.OptionsColumn.AllowEdit = false;
            this.gridColumn20.OptionsColumn.AllowFocus = false;
            this.gridColumn20.Visible = true;
            this.gridColumn20.VisibleIndex = 9;
            this.gridColumn20.Width = 73;
            // 
            // gridColumn21
            // 
            this.gridColumn21.Caption = "Total Cost";
            this.gridColumn21.FieldName = "TotalCost";
            this.gridColumn21.Name = "gridColumn21";
            this.gridColumn21.OptionsColumn.AllowEdit = false;
            this.gridColumn21.OptionsColumn.AllowFocus = false;
            this.gridColumn21.Visible = true;
            this.gridColumn21.VisibleIndex = 10;
            this.gridColumn21.Width = 144;
            // 
            // gridColumn13
            // 
            this.gridColumn13.Name = "gridColumn13";
            this.gridColumn13.OptionsColumn.FixedWidth = true;
            this.gridColumn13.Width = 33;
            // 
            // gridMark
            // 
            this.gridMark.Caption = " ";
            this.gridMark.FieldName = "Mark";
            this.gridMark.Name = "gridMark";
            this.gridMark.Visible = true;
            this.gridMark.VisibleIndex = 0;
            this.gridMark.Width = 58;
            // 
            // UCAddPPEEquipment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gcEquipmentSpecs);
            this.Name = "UCAddPPEEquipment";
            this.Size = new System.Drawing.Size(1266, 354);
            ((System.ComponentModel.ISupportInitialize)(this.gridSpecsDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcEquipmentSpecs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridEquipmentSpecs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnInfo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gcEquipmentSpecs;
        private DevExpress.XtraGrid.Views.Grid.GridView gridSpecsDetails;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn10;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn11;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn12;
        private DevExpress.XtraGrid.Views.Grid.GridView gridEquipmentSpecs;
        private DevExpress.XtraGrid.Columns.GridColumn gridEditBtn;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit btnInfo;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn20;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn21;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn13;
        private DevExpress.XtraGrid.Columns.GridColumn gridMark;
    }
}
