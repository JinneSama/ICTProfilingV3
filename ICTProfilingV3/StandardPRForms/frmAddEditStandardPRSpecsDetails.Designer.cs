namespace ICTProfilingV3.StandardPRForms
{
    partial class frmAddEditStandardPRSpecsDetails
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions1 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject3 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject4 = new DevExpress.Utils.SerializableAppearanceObject();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAddEditStandardPRSpecsDetails));
            this.panel1 = new System.Windows.Forms.Panel();
            this.gcSpecsDetails = new DevExpress.XtraGrid.GridControl();
            this.gridSpecsDetails = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnDeleteSpecs = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnDeleteAll = new DevExpress.XtraEditors.SimpleButton();
            this.popupMenu = new DevExpress.XtraBars.PopupMenu(this.components);
            this.btnPasteSpecs = new DevExpress.XtraBars.BarButtonItem();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            ((System.ComponentModel.ISupportInitialize)(this.gcSpecsDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridSpecsDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnDeleteSpecs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1120, 40);
            this.panel1.TabIndex = 143;
            // 
            // gcSpecsDetails
            // 
            this.gcSpecsDetails.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gcSpecsDetails.EmbeddedNavigator.Buttons.Append.Visible = false;
            this.gcSpecsDetails.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            this.gcSpecsDetails.EmbeddedNavigator.Buttons.Edit.Visible = false;
            this.gcSpecsDetails.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            this.gcSpecsDetails.EmbeddedNavigator.Buttons.Remove.Visible = false;
            this.gcSpecsDetails.Location = new System.Drawing.Point(0, 78);
            this.gcSpecsDetails.MainView = this.gridSpecsDetails;
            this.gcSpecsDetails.Name = "gcSpecsDetails";
            this.gcSpecsDetails.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.btnDeleteSpecs});
            this.gcSpecsDetails.Size = new System.Drawing.Size(1120, 529);
            this.gcSpecsDetails.TabIndex = 144;
            this.gcSpecsDetails.UseEmbeddedNavigator = true;
            this.gcSpecsDetails.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridSpecsDetails});
            // 
            // gridSpecsDetails
            // 
            this.gridSpecsDetails.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn5,
            this.gridColumn6,
            this.gridColumn7});
            this.gridSpecsDetails.GridControl = this.gcSpecsDetails;
            this.gridSpecsDetails.Name = "gridSpecsDetails";
            this.gridSpecsDetails.NewItemRowText = "Click here to add a new PR Specification";
            this.gridSpecsDetails.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom;
            this.gridSpecsDetails.OptionsView.ShowGroupPanel = false;
            this.gridSpecsDetails.RowUpdated += new DevExpress.XtraGrid.Views.Base.RowObjectEventHandler(this.gridSpecsDetails_RowUpdated);
            this.gridSpecsDetails.MouseUp += new System.Windows.Forms.MouseEventHandler(this.gridSpecsDetails_MouseUp);
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "ItemNo";
            this.gridColumn1.FieldName = "ItemNo";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 1;
            this.gridColumn1.Width = 111;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Equipment";
            this.gridColumn2.FieldName = "Equipment";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Width = 198;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Description";
            this.gridColumn3.FieldName = "Description";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Width = 225;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "Specs";
            this.gridColumn4.FieldName = "Specs";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 2;
            this.gridColumn4.Width = 414;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "Description";
            this.gridColumn5.FieldName = "Description";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 3;
            this.gridColumn5.Width = 523;
            // 
            // gridColumn6
            // 
            this.gridColumn6.ColumnEdit = this.btnDeleteSpecs;
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.OptionsColumn.FixedWidth = true;
            this.gridColumn6.Width = 40;
            // 
            // btnDeleteSpecs
            // 
            this.btnDeleteSpecs.AutoHeight = false;
            editorButtonImageOptions1.Image = global::ICTProfilingV3.Properties.Resources.close_16x1610;
            this.btnDeleteSpecs.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, editorButtonImageOptions1, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, serializableAppearanceObject2, serializableAppearanceObject3, serializableAppearanceObject4, "", null, null, DevExpress.Utils.ToolTipAnchor.Default)});
            this.btnDeleteSpecs.ContextImageOptions.Image = global::ICTProfilingV3.Properties.Resources.close_16x1610;
            this.btnDeleteSpecs.Name = "btnDeleteSpecs";
            this.btnDeleteSpecs.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            this.btnDeleteSpecs.Click += new System.EventHandler(this.btnDeleteSpecs_Click);
            // 
            // gridColumn7
            // 
            this.gridColumn7.ColumnEdit = this.btnDeleteSpecs;
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.OptionsColumn.FixedWidth = true;
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 0;
            this.gridColumn7.Width = 41;
            // 
            // btnDeleteAll
            // 
            this.btnDeleteAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDeleteAll.Appearance.BackColor = System.Drawing.Color.RoyalBlue;
            this.btnDeleteAll.Appearance.ForeColor = System.Drawing.Color.Black;
            this.btnDeleteAll.Appearance.Options.UseBackColor = true;
            this.btnDeleteAll.Appearance.Options.UseForeColor = true;
            this.btnDeleteAll.ImageOptions.Image = global::ICTProfilingV3.Properties.Resources.close_16x1610;
            this.btnDeleteAll.Location = new System.Drawing.Point(6, 45);
            this.btnDeleteAll.Margin = new System.Windows.Forms.Padding(2);
            this.btnDeleteAll.Name = "btnDeleteAll";
            this.btnDeleteAll.Size = new System.Drawing.Size(134, 28);
            this.btnDeleteAll.TabIndex = 173;
            this.btnDeleteAll.Text = "Delete All";
            this.btnDeleteAll.Click += new System.EventHandler(this.btnDeleteAll_Click);
            // 
            // popupMenu
            // 
            this.popupMenu.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.btnPasteSpecs)});
            this.popupMenu.Manager = this.barManager1;
            this.popupMenu.Name = "popupMenu";
            // 
            // btnPasteSpecs
            // 
            this.btnPasteSpecs.Caption = "Paste Specs";
            this.btnPasteSpecs.Id = 0;
            this.btnPasteSpecs.ImageOptions.Image = global::ICTProfilingV3.Properties.Resources.paste_16x163;
            this.btnPasteSpecs.ImageOptions.LargeImage = global::ICTProfilingV3.Properties.Resources.paste_32x323;
            this.btnPasteSpecs.Name = "btnPasteSpecs";
            this.btnPasteSpecs.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnPasteSpecs_ItemClick);
            // 
            // barManager1
            // 
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.btnPasteSpecs});
            this.barManager1.MaxItemId = 1;
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager1;
            this.barDockControlTop.Size = new System.Drawing.Size(1120, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 606);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Size = new System.Drawing.Size(1120, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 606);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1120, 0);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 606);
            // 
            // frmAddEditStandardPRSpecsDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1120, 606);
            this.Controls.Add(this.btnDeleteAll);
            this.Controls.Add(this.gcSpecsDetails);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.IconOptions.Icon = ((System.Drawing.Icon)(resources.GetObject("frmAddEditStandardPRSpecsDetails.IconOptions.Icon")));
            this.Name = "frmAddEditStandardPRSpecsDetails";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            ((System.ComponentModel.ISupportInitialize)(this.gcSpecsDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridSpecsDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnDeleteSpecs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraGrid.GridControl gcSpecsDetails;
        private DevExpress.XtraGrid.Views.Grid.GridView gridSpecsDetails;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit btnDeleteSpecs;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraEditors.SimpleButton btnDeleteAll;
        private DevExpress.XtraBars.PopupMenu popupMenu;
        private DevExpress.XtraBars.BarButtonItem btnPasteSpecs;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
    }
}