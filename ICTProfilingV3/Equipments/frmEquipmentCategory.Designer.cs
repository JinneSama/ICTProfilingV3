namespace ICTProfilingV3.Equipments
{
    partial class frmEquipmentCategory
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEquipmentCategory));
            this.panel1 = new System.Windows.Forms.Panel();
            this.labelControl16 = new DevExpress.XtraEditors.LabelControl();
            this.lblEpisNo = new DevExpress.XtraEditors.LabelControl();
            this.gcEquipment = new DevExpress.XtraGrid.GridControl();
            this.gridEquipment = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnDeleteEquipment = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcEquipment)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridEquipment)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnDeleteEquipment)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.panel1.Controls.Add(this.labelControl16);
            this.panel1.Controls.Add(this.lblEpisNo);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(547, 37);
            this.panel1.TabIndex = 105;
            // 
            // labelControl16
            // 
            this.labelControl16.Appearance.Font = new System.Drawing.Font("Segoe UI", 16F);
            this.labelControl16.Appearance.ForeColor = System.Drawing.Color.White;
            this.labelControl16.Appearance.Options.UseFont = true;
            this.labelControl16.Appearance.Options.UseForeColor = true;
            this.labelControl16.Location = new System.Drawing.Point(24, 4);
            this.labelControl16.Name = "labelControl16";
            this.labelControl16.Size = new System.Drawing.Size(189, 30);
            this.labelControl16.TabIndex = 2;
            this.labelControl16.Text = "Equipment Category";
            // 
            // lblEpisNo
            // 
            this.lblEpisNo.Appearance.Font = new System.Drawing.Font("Segoe UI", 16F);
            this.lblEpisNo.Appearance.ForeColor = System.Drawing.Color.White;
            this.lblEpisNo.Appearance.Options.UseFont = true;
            this.lblEpisNo.Appearance.Options.UseForeColor = true;
            this.lblEpisNo.Location = new System.Drawing.Point(24, 3);
            this.lblEpisNo.Name = "lblEpisNo";
            this.lblEpisNo.Size = new System.Drawing.Size(0, 30);
            this.lblEpisNo.TabIndex = 1;
            // 
            // gcEquipment
            // 
            this.gcEquipment.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gcEquipment.EmbeddedNavigator.Buttons.Append.Visible = false;
            this.gcEquipment.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            this.gcEquipment.EmbeddedNavigator.Buttons.Edit.Visible = false;
            this.gcEquipment.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            this.gcEquipment.EmbeddedNavigator.Buttons.Remove.Visible = false;
            this.gcEquipment.Location = new System.Drawing.Point(0, 39);
            this.gcEquipment.MainView = this.gridEquipment;
            this.gcEquipment.Name = "gcEquipment";
            this.gcEquipment.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.btnDeleteEquipment});
            this.gcEquipment.Size = new System.Drawing.Size(547, 451);
            this.gcEquipment.TabIndex = 106;
            this.gcEquipment.UseEmbeddedNavigator = true;
            this.gcEquipment.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridEquipment});
            // 
            // gridEquipment
            // 
            this.gridEquipment.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn3});
            this.gridEquipment.GridControl = this.gcEquipment;
            this.gridEquipment.Name = "gridEquipment";
            this.gridEquipment.NewItemRowText = "Click here to add a new Category";
            this.gridEquipment.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Top;
            this.gridEquipment.OptionsView.ShowGroupPanel = false;
            this.gridEquipment.RowUpdated += new DevExpress.XtraGrid.Views.Base.RowObjectEventHandler(this.gridEquipment_RowUpdated);
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Equipment Category";
            this.gridColumn1.FieldName = "Name";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 1;
            this.gridColumn1.Width = 270;
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
            // frmEquipmentCategory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(547, 489);
            this.Controls.Add(this.gcEquipment);
            this.Controls.Add(this.panel1);
            this.IconOptions.Icon = ((System.Drawing.Icon)(resources.GetObject("frmEquipmentCategory.IconOptions.Icon")));
            this.Name = "frmEquipmentCategory";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcEquipment)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridEquipment)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnDeleteEquipment)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraEditors.LabelControl labelControl16;
        private DevExpress.XtraEditors.LabelControl lblEpisNo;
        private DevExpress.XtraGrid.GridControl gcEquipment;
        private DevExpress.XtraGrid.Views.Grid.GridView gridEquipment;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit btnDeleteEquipment;
    }
}