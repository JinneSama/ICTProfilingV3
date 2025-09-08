namespace ICTProfilingV3.DeliveriesForms
{
    partial class frmComparisonReportMenu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmComparisonReportMenu));
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions3 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject9 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject10 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject11 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject12 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions4 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject13 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject14 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject15 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject16 = new DevExpress.Utils.SerializableAppearanceObject();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblTitle = new DevExpress.XtraEditors.LabelControl();
            this.lblEpisNo = new DevExpress.XtraEditors.LabelControl();
            this.btnUpload = new DevExpress.XtraEditors.SimpleButton();
            this.gcComparison = new DevExpress.XtraGrid.GridControl();
            this.gridComparison = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnDownload = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.btnDelete = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcComparison)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridComparison)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnDownload)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnDelete)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.panel1.Controls.Add(this.lblTitle);
            this.panel1.Controls.Add(this.lblEpisNo);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(495, 37);
            this.panel1.TabIndex = 108;
            // 
            // lblTitle
            // 
            this.lblTitle.Appearance.Font = new System.Drawing.Font("Segoe UI", 16F);
            this.lblTitle.Appearance.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Appearance.Options.UseFont = true;
            this.lblTitle.Appearance.Options.UseForeColor = true;
            this.lblTitle.Location = new System.Drawing.Point(20, 3);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(232, 30);
            this.lblTitle.TabIndex = 2;
            this.lblTitle.Text = "Comparison Report Excel";
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
            // btnUpload
            // 
            this.btnUpload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUpload.Appearance.BackColor = System.Drawing.Color.Turquoise;
            this.btnUpload.Appearance.ForeColor = System.Drawing.Color.Black;
            this.btnUpload.Appearance.Options.UseBackColor = true;
            this.btnUpload.Appearance.Options.UseForeColor = true;
            this.btnUpload.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnEdit.ImageOptions.Image")));
            this.btnUpload.Location = new System.Drawing.Point(11, 42);
            this.btnUpload.Margin = new System.Windows.Forms.Padding(2);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(125, 28);
            this.btnUpload.TabIndex = 111;
            this.btnUpload.Text = "Upload Comparison";
            this.btnUpload.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // gcComparison
            // 
            this.gcComparison.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gcComparison.EmbeddedNavigator.Buttons.Append.Visible = false;
            this.gcComparison.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            this.gcComparison.EmbeddedNavigator.Buttons.Edit.Visible = false;
            this.gcComparison.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            this.gcComparison.EmbeddedNavigator.Buttons.Remove.Visible = false;
            this.gcComparison.Location = new System.Drawing.Point(0, 75);
            this.gcComparison.MainView = this.gridComparison;
            this.gcComparison.Name = "gcComparison";
            this.gcComparison.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.btnDownload,
            this.btnDelete});
            this.gcComparison.Size = new System.Drawing.Size(495, 358);
            this.gcComparison.TabIndex = 112;
            this.gcComparison.UseEmbeddedNavigator = true;
            this.gcComparison.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridComparison});
            // 
            // gridComparison
            // 
            this.gridComparison.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4});
            this.gridComparison.GridControl = this.gcComparison;
            this.gridComparison.Name = "gridComparison";
            this.gridComparison.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Filename";
            this.gridColumn1.FieldName = "FileName";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.OptionsColumn.AllowFocus = false;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            this.gridColumn1.Width = 295;
            // 
            // gridColumn2
            // 
            this.gridColumn2.FieldName = "Version";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            this.gridColumn2.OptionsColumn.AllowFocus = false;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            this.gridColumn2.Width = 102;
            // 
            // gridColumn3
            // 
            this.gridColumn3.ColumnEdit = this.btnDownload;
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.FixedWidth = true;
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 2;
            this.gridColumn3.Width = 40;
            // 
            // gridColumn4
            // 
            this.gridColumn4.ColumnEdit = this.btnDelete;
            this.gridColumn4.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Right;
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.OptionsColumn.FixedWidth = true;
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 3;
            this.gridColumn4.Width = 35;
            // 
            // btnDownload
            // 
            this.btnDownload.AutoHeight = false;
            editorButtonImageOptions3.Image = ((System.Drawing.Image)(resources.GetObject("editorButtonImageOptions3.Image")));
            this.btnDownload.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, editorButtonImageOptions3, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject9, serializableAppearanceObject10, serializableAppearanceObject11, serializableAppearanceObject12, "", null, null, DevExpress.Utils.ToolTipAnchor.Default)});
            this.btnDownload.ContextImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnDownload.ContextImageOptions.Image")));
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            this.btnDownload.Click += new System.EventHandler(this.btnDownload_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.AutoHeight = false;
            editorButtonImageOptions4.Image = ((System.Drawing.Image)(resources.GetObject("editorButtonImageOptions4.Image")));
            this.btnDelete.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, editorButtonImageOptions4, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject13, serializableAppearanceObject14, serializableAppearanceObject15, serializableAppearanceObject16, "", null, null, DevExpress.Utils.ToolTipAnchor.Default)});
            this.btnDelete.ContextImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("repositoryItemButtonEdit2.ContextImageOptions.Image")));
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // frmComparisonReportMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(495, 431);
            this.Controls.Add(this.gcComparison);
            this.Controls.Add(this.btnUpload);
            this.Controls.Add(this.panel1);
            this.IconOptions.Icon = ((System.Drawing.Icon)(resources.GetObject("frmComparisonReportMenu.IconOptions.Icon")));
            this.Name = "frmComparisonReportMenu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcComparison)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridComparison)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnDownload)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnDelete)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraEditors.LabelControl lblTitle;
        private DevExpress.XtraEditors.LabelControl lblEpisNo;
        private DevExpress.XtraEditors.SimpleButton btnUpload;
        private DevExpress.XtraGrid.GridControl gcComparison;
        private DevExpress.XtraGrid.Views.Grid.GridView gridComparison;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit btnDownload;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit btnDelete;
    }
}