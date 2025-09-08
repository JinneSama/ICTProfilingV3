namespace ICTProfilingV3.DeliveriesForms
{
    partial class frmAddEditDeliveriesSpecsDetails
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAddEditDeliveriesSpecsDetails));
            this.gcEquipmentDetails = new DevExpress.XtraGrid.GridControl();
            this.gridEquipmentDetails = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnDelete = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.btnCopySpecs = new DevExpress.XtraEditors.SimpleButton();
            this.lblDescription = new System.Windows.Forms.Label();
            this.lblEquipment = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.labelControl16 = new DevExpress.XtraEditors.LabelControl();
            this.lblEpisNo = new DevExpress.XtraEditors.LabelControl();
            this.btnCopyTSBasis = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.gcEquipmentDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridEquipmentDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnDelete)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gcEquipmentDetails
            // 
            this.gcEquipmentDetails.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gcEquipmentDetails.EmbeddedNavigator.Buttons.Append.Visible = false;
            this.gcEquipmentDetails.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            this.gcEquipmentDetails.EmbeddedNavigator.Buttons.Edit.Visible = false;
            this.gcEquipmentDetails.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            this.gcEquipmentDetails.EmbeddedNavigator.Buttons.Remove.Visible = false;
            this.gcEquipmentDetails.Location = new System.Drawing.Point(0, 128);
            this.gcEquipmentDetails.MainView = this.gridEquipmentDetails;
            this.gcEquipmentDetails.Name = "gcEquipmentDetails";
            this.gcEquipmentDetails.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.btnDelete});
            this.gcEquipmentDetails.Size = new System.Drawing.Size(1254, 593);
            this.gcEquipmentDetails.TabIndex = 93;
            this.gcEquipmentDetails.UseEmbeddedNavigator = true;
            this.gcEquipmentDetails.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridEquipmentDetails});
            // 
            // gridEquipmentDetails
            // 
            this.gridEquipmentDetails.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4});
            this.gridEquipmentDetails.GridControl = this.gcEquipmentDetails;
            this.gridEquipmentDetails.Name = "gridEquipmentDetails";
            this.gridEquipmentDetails.NewItemRowText = "Click here to Add new Specs";
            this.gridEquipmentDetails.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Top;
            this.gridEquipmentDetails.OptionsView.ShowGroupPanel = false;
            this.gridEquipmentDetails.RowUpdated += new DevExpress.XtraGrid.Views.Base.RowObjectEventHandler(this.gridEquipmentDetails_RowUpdated);
            // 
            // gridColumn1
            // 
            this.gridColumn1.ColumnEdit = this.btnDelete;
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.FixedWidth = true;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            this.gridColumn1.Width = 35;
            // 
            // btnDelete
            // 
            this.btnDelete.AutoHeight = false;
            editorButtonImageOptions1.Image = global::ICTProfilingV3.Properties.Resources.close_16x166;
            this.btnDelete.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, editorButtonImageOptions1, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, serializableAppearanceObject2, serializableAppearanceObject3, serializableAppearanceObject4, "", null, null, DevExpress.Utils.ToolTipAnchor.Default)});
            this.btnDelete.ContextImageOptions.Image = global::ICTProfilingV3.Properties.Resources.close_16x167;
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // gridColumn2
            // 
            this.gridColumn2.FieldName = "ItemNo";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.FixedWidth = true;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            this.gridColumn2.Width = 89;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Specs";
            this.gridColumn3.FieldName = "Specs";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 2;
            this.gridColumn3.Width = 334;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "Description";
            this.gridColumn4.FieldName = "Description";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 3;
            this.gridColumn4.Width = 454;
            // 
            // groupControl1
            // 
            this.groupControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupControl1.AppearanceCaption.FontStyleDelta = System.Drawing.FontStyle.Bold;
            this.groupControl1.AppearanceCaption.Options.UseFont = true;
            this.groupControl1.Controls.Add(this.btnCopyTSBasis);
            this.groupControl1.Controls.Add(this.btnCopySpecs);
            this.groupControl1.Controls.Add(this.lblDescription);
            this.groupControl1.Controls.Add(this.lblEquipment);
            this.groupControl1.Controls.Add(this.label2);
            this.groupControl1.Controls.Add(this.label1);
            this.groupControl1.Location = new System.Drawing.Point(0, 38);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(1254, 84);
            this.groupControl1.TabIndex = 92;
            this.groupControl1.Text = "Details";
            // 
            // btnCopySpecs
            // 
            this.btnCopySpecs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCopySpecs.Appearance.BackColor = System.Drawing.Color.RoyalBlue;
            this.btnCopySpecs.Appearance.ForeColor = System.Drawing.Color.Black;
            this.btnCopySpecs.Appearance.Options.UseBackColor = true;
            this.btnCopySpecs.Appearance.Options.UseForeColor = true;
            this.btnCopySpecs.ImageOptions.Image = global::ICTProfilingV3.Properties.Resources.copymodeldifferences_16x16;
            this.btnCopySpecs.Location = new System.Drawing.Point(1070, 17);
            this.btnCopySpecs.Margin = new System.Windows.Forms.Padding(2);
            this.btnCopySpecs.Name = "btnCopySpecs";
            this.btnCopySpecs.Size = new System.Drawing.Size(176, 28);
            this.btnCopySpecs.TabIndex = 109;
            this.btnCopySpecs.Text = "Copy from Equipment Specs";
            this.btnCopySpecs.Click += new System.EventHandler(this.btnCopySpecs_Click);
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescription.Location = new System.Drawing.Point(101, 47);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(71, 13);
            this.lblDescription.TabIndex = 3;
            this.lblDescription.Text = "Description";
            // 
            // lblEquipment
            // 
            this.lblEquipment.AutoSize = true;
            this.lblEquipment.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEquipment.Location = new System.Drawing.Point(101, 21);
            this.lblEquipment.Name = "lblEquipment";
            this.lblEquipment.Size = new System.Drawing.Size(67, 13);
            this.lblEquipment.TabIndex = 2;
            this.lblEquipment.Text = "Equipment";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Description:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Equipment:";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.panel1.Controls.Add(this.labelControl16);
            this.panel1.Controls.Add(this.lblEpisNo);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1254, 37);
            this.panel1.TabIndex = 91;
            // 
            // labelControl16
            // 
            this.labelControl16.Appearance.Font = new System.Drawing.Font("Segoe UI", 16F);
            this.labelControl16.Appearance.ForeColor = System.Drawing.Color.White;
            this.labelControl16.Appearance.Options.UseFont = true;
            this.labelControl16.Appearance.Options.UseForeColor = true;
            this.labelControl16.Location = new System.Drawing.Point(12, 3);
            this.labelControl16.Name = "labelControl16";
            this.labelControl16.Size = new System.Drawing.Size(254, 30);
            this.labelControl16.TabIndex = 2;
            this.labelControl16.Text = "Deliveries Equipment Specs";
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
            // btnCopyTSBasis
            // 
            this.btnCopyTSBasis.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCopyTSBasis.Appearance.BackColor = System.Drawing.Color.RoyalBlue;
            this.btnCopyTSBasis.Appearance.ForeColor = System.Drawing.Color.Black;
            this.btnCopyTSBasis.Appearance.Options.UseBackColor = true;
            this.btnCopyTSBasis.Appearance.Options.UseForeColor = true;
            this.btnCopyTSBasis.ImageOptions.Image = global::ICTProfilingV3.Properties.Resources.copy_16x164;
            this.btnCopyTSBasis.Location = new System.Drawing.Point(1070, 49);
            this.btnCopyTSBasis.Margin = new System.Windows.Forms.Padding(2);
            this.btnCopyTSBasis.Name = "btnCopyTSBasis";
            this.btnCopyTSBasis.Size = new System.Drawing.Size(176, 28);
            this.btnCopyTSBasis.TabIndex = 112;
            this.btnCopyTSBasis.Text = "Copy from TechSpecs Basis";
            this.btnCopyTSBasis.Click += new System.EventHandler(this.btnCopyTSBasis_Click);
            // 
            // frmAddEditDeliveriesSpecsDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1254, 720);
            this.Controls.Add(this.gcEquipmentDetails);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.panel1);
            this.IconOptions.Icon = ((System.Drawing.Icon)(resources.GetObject("frmAddEditDeliveriesSpecsDetails.IconOptions.Icon")));
            this.Name = "frmAddEditDeliveriesSpecsDetails";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            ((System.ComponentModel.ISupportInitialize)(this.gcEquipmentDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridEquipmentDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnDelete)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gcEquipmentDetails;
        private DevExpress.XtraGrid.Views.Grid.GridView gridEquipmentDetails;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit btnDelete;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.Label lblEquipment;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraEditors.LabelControl labelControl16;
        private DevExpress.XtraEditors.LabelControl lblEpisNo;
        private DevExpress.XtraEditors.SimpleButton btnCopySpecs;
        private DevExpress.XtraEditors.SimpleButton btnCopyTSBasis;
    }
}