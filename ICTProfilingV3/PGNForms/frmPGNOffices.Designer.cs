namespace ICTProfilingV3.PGNForms
{
    partial class frmPGNOffices
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
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions1 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject3 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject4 = new DevExpress.Utils.SerializableAppearanceObject();
            this.panel1 = new System.Windows.Forms.Panel();
            this.labelControl16 = new DevExpress.XtraEditors.LabelControl();
            this.gcOffice = new DevExpress.XtraGrid.GridControl();
            this.gridOffice = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnDeleteEquipment = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcOffice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridOffice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnDeleteEquipment)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.panel1.Controls.Add(this.labelControl16);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(553, 41);
            this.panel1.TabIndex = 8;
            // 
            // labelControl16
            // 
            this.labelControl16.Appearance.Font = new System.Drawing.Font("Segoe UI", 16F);
            this.labelControl16.Appearance.ForeColor = System.Drawing.Color.White;
            this.labelControl16.Appearance.Options.UseFont = true;
            this.labelControl16.Appearance.Options.UseForeColor = true;
            this.labelControl16.Location = new System.Drawing.Point(12, 5);
            this.labelControl16.Name = "labelControl16";
            this.labelControl16.Size = new System.Drawing.Size(178, 30);
            this.labelControl16.TabIndex = 1;
            this.labelControl16.Text = "PGN Group/Offices";
            // 
            // gcOffice
            // 
            this.gcOffice.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gcOffice.EmbeddedNavigator.Buttons.Append.Visible = false;
            this.gcOffice.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            this.gcOffice.EmbeddedNavigator.Buttons.Edit.Visible = false;
            this.gcOffice.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            this.gcOffice.EmbeddedNavigator.Buttons.Remove.Visible = false;
            this.gcOffice.Location = new System.Drawing.Point(0, 41);
            this.gcOffice.MainView = this.gridOffice;
            this.gcOffice.Name = "gcOffice";
            this.gcOffice.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.btnDeleteEquipment});
            this.gcOffice.Size = new System.Drawing.Size(553, 565);
            this.gcOffice.TabIndex = 88;
            this.gcOffice.UseEmbeddedNavigator = true;
            this.gcOffice.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridOffice});
            // 
            // gridOffice
            // 
            this.gridOffice.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn3,
            this.gridColumn2});
            this.gridOffice.GridControl = this.gcOffice;
            this.gridOffice.Name = "gridOffice";
            this.gridOffice.NewItemRowText = "Click here to add a new Office/Group";
            this.gridOffice.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Top;
            this.gridOffice.OptionsView.ShowGroupPanel = false;
            this.gridOffice.RowUpdated += new DevExpress.XtraGrid.Views.Base.RowObjectEventHandler(this.gridOffice_RowUpdated);
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Office Acronym";
            this.gridColumn1.FieldName = "OfficeAcr";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 1;
            this.gridColumn1.Width = 174;
            // 
            // gridColumn3
            // 
            this.gridColumn3.ColumnEdit = this.btnDeleteEquipment;
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.FixedWidth = true;
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 0;
            this.gridColumn3.Width = 36;
            // 
            // btnDeleteEquipment
            // 
            this.btnDeleteEquipment.AutoHeight = false;
            editorButtonImageOptions1.Image = global::ICTProfilingV3.Properties.Resources.close_16x165;
            this.btnDeleteEquipment.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, editorButtonImageOptions1, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, serializableAppearanceObject2, serializableAppearanceObject3, serializableAppearanceObject4, "", null, null, DevExpress.Utils.ToolTipAnchor.Default)});
            this.btnDeleteEquipment.ContextImageOptions.Image = global::ICTProfilingV3.Properties.Resources.close_16x164;
            this.btnDeleteEquipment.Name = "btnDeleteEquipment";
            this.btnDeleteEquipment.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            this.btnDeleteEquipment.Click += new System.EventHandler(this.btnDeleteEquipment_Click);
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Office";
            this.gridColumn2.FieldName = "Office";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 2;
            this.gridColumn2.Width = 322;
            // 
            // frmPGNOffices
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(553, 605);
            this.Controls.Add(this.gcOffice);
            this.Controls.Add(this.panel1);
            this.Name = "frmPGNOffices";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcOffice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridOffice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnDeleteEquipment)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraEditors.LabelControl labelControl16;
        private DevExpress.XtraGrid.GridControl gcOffice;
        private DevExpress.XtraGrid.Views.Grid.GridView gridOffice;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit btnDeleteEquipment;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
    }
}