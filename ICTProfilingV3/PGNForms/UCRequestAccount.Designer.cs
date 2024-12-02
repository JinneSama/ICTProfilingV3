namespace ICTProfilingV3.PGNForms
{
    partial class UCRequestAccount
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCRequestAccount));
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions1 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject3 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject4 = new DevExpress.Utils.SerializableAppearanceObject();
            this.btnPGNAccount = new DevExpress.XtraEditors.SimpleButton();
            this.gcAccount = new DevExpress.XtraGrid.GridControl();
            this.gridAccount = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnDelete = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gcAccount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridAccount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnDelete)).BeginInit();
            this.SuspendLayout();
            // 
            // btnPGNAccount
            // 
            this.btnPGNAccount.Appearance.BackColor = System.Drawing.Color.RoyalBlue;
            this.btnPGNAccount.Appearance.ForeColor = System.Drawing.Color.Black;
            this.btnPGNAccount.Appearance.Options.UseBackColor = true;
            this.btnPGNAccount.Appearance.Options.UseForeColor = true;
            this.btnPGNAccount.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnPGNAccount.ImageOptions.Image")));
            this.btnPGNAccount.Location = new System.Drawing.Point(2, 2);
            this.btnPGNAccount.Margin = new System.Windows.Forms.Padding(2);
            this.btnPGNAccount.Name = "btnPGNAccount";
            this.btnPGNAccount.Size = new System.Drawing.Size(130, 28);
            this.btnPGNAccount.TabIndex = 89;
            this.btnPGNAccount.Text = "Add PGN Account";
            this.btnPGNAccount.Click += new System.EventHandler(this.btnPGNAccount_Click);
            // 
            // gcAccount
            // 
            this.gcAccount.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gcAccount.EmbeddedNavigator.Buttons.Append.Visible = false;
            this.gcAccount.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            this.gcAccount.EmbeddedNavigator.Buttons.Edit.Visible = false;
            this.gcAccount.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            this.gcAccount.EmbeddedNavigator.Buttons.Remove.Visible = false;
            this.gcAccount.Location = new System.Drawing.Point(0, 35);
            this.gcAccount.MainView = this.gridAccount;
            this.gcAccount.Name = "gcAccount";
            this.gcAccount.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.btnDelete});
            this.gcAccount.Size = new System.Drawing.Size(1217, 429);
            this.gcAccount.TabIndex = 90;
            this.gcAccount.UseEmbeddedNavigator = true;
            this.gcAccount.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridAccount});
            // 
            // gridAccount
            // 
            this.gridAccount.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4});
            this.gridAccount.GridControl = this.gcAccount;
            this.gridAccount.Name = "gridAccount";
            this.gridAccount.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn1
            // 
            this.gridColumn1.ColumnEdit = this.btnDelete;
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.FixedWidth = true;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            this.gridColumn1.Width = 38;
            // 
            // btnDelete
            // 
            this.btnDelete.AutoHeight = false;
            editorButtonImageOptions1.Image = global::ICTProfilingV3.Properties.Resources.close_16x1610;
            this.btnDelete.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, editorButtonImageOptions1, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, serializableAppearanceObject2, serializableAppearanceObject3, serializableAppearanceObject4, "", null, null, DevExpress.Utils.ToolTipAnchor.Default)});
            this.btnDelete.ContextImageOptions.Image = global::ICTProfilingV3.Properties.Resources.close_16x1610;
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Fullname";
            this.gridColumn2.FieldName = "Name";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            this.gridColumn2.OptionsColumn.AllowFocus = false;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            this.gridColumn2.Width = 385;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Username";
            this.gridColumn3.FieldName = "PGNAccount.Username";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.AllowEdit = false;
            this.gridColumn3.OptionsColumn.AllowFocus = false;
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 2;
            this.gridColumn3.Width = 385;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "Office/Group";
            this.gridColumn4.FieldName = "PGNAccount.PGNGroupOffices.OfficeAcr";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.OptionsColumn.AllowEdit = false;
            this.gridColumn4.OptionsColumn.AllowFocus = false;
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 3;
            this.gridColumn4.Width = 388;
            // 
            // UCRequestAccount
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gcAccount);
            this.Controls.Add(this.btnPGNAccount);
            this.Name = "UCRequestAccount";
            this.Size = new System.Drawing.Size(1217, 464);
            ((System.ComponentModel.ISupportInitialize)(this.gcAccount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridAccount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnDelete)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnPGNAccount;
        private DevExpress.XtraGrid.GridControl gcAccount;
        private DevExpress.XtraGrid.Views.Grid.GridView gridAccount;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit btnDelete;
    }
}
