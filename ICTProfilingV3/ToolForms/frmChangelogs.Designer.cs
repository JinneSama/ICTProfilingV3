namespace ICTProfilingV3.ToolForms
{
    partial class frmChangelogs
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
            DevExpress.XtraEditors.TableLayout.TableColumnDefinition tableColumnDefinition1 = new DevExpress.XtraEditors.TableLayout.TableColumnDefinition();
            DevExpress.XtraEditors.TableLayout.TableRowDefinition tableRowDefinition1 = new DevExpress.XtraEditors.TableLayout.TableRowDefinition();
            DevExpress.XtraEditors.TableLayout.TableRowDefinition tableRowDefinition2 = new DevExpress.XtraEditors.TableLayout.TableRowDefinition();
            DevExpress.XtraEditors.TableLayout.TableRowDefinition tableRowDefinition3 = new DevExpress.XtraEditors.TableLayout.TableRowDefinition();
            DevExpress.XtraGrid.Views.Tile.TileViewItemElement tileViewItemElement1 = new DevExpress.XtraGrid.Views.Tile.TileViewItemElement();
            DevExpress.XtraGrid.Views.Tile.TileViewItemElement tileViewItemElement2 = new DevExpress.XtraGrid.Views.Tile.TileViewItemElement();
            DevExpress.XtraGrid.Views.Tile.TileViewItemElement tileViewItemElement3 = new DevExpress.XtraGrid.Views.Tile.TileViewItemElement();
            this.colDateCreated = new DevExpress.XtraGrid.Columns.TileViewColumn();
            this.colChanges = new DevExpress.XtraGrid.Columns.TileViewColumn();
            this.rteChanges = new DevExpress.XtraEditors.Repository.RepositoryItemRichTextEdit();
            this.colVersion = new DevExpress.XtraGrid.Columns.TileViewColumn();
            this.gcChangelogs = new DevExpress.XtraGrid.GridControl();
            this.tvChangelogs = new DevExpress.XtraGrid.Views.Tile.TileView();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.lblVersion = new DevExpress.XtraEditors.LabelControl();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.rteChanges)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcChangelogs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tvChangelogs)).BeginInit();
            this.SuspendLayout();
            // 
            // colDateCreated
            // 
            this.colDateCreated.FieldName = "DateCreated";
            this.colDateCreated.Name = "colDateCreated";
            this.colDateCreated.Visible = true;
            this.colDateCreated.VisibleIndex = 0;
            // 
            // colChanges
            // 
            this.colChanges.ColumnEdit = this.rteChanges;
            this.colChanges.FieldName = "Changelogs";
            this.colChanges.Name = "colChanges";
            this.colChanges.Visible = true;
            this.colChanges.VisibleIndex = 1;
            // 
            // rteChanges
            // 
            this.rteChanges.DocumentFormat = DevExpress.XtraRichEdit.DocumentFormat.Html;
            this.rteChanges.Name = "rteChanges";
            this.rteChanges.ShowCaretInReadOnly = false;
            // 
            // colVersion
            // 
            this.colVersion.FieldName = "Version";
            this.colVersion.Name = "colVersion";
            this.colVersion.Visible = true;
            this.colVersion.VisibleIndex = 2;
            // 
            // gcChangelogs
            // 
            this.gcChangelogs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gcChangelogs.Location = new System.Drawing.Point(12, 61);
            this.gcChangelogs.MainView = this.tvChangelogs;
            this.gcChangelogs.Name = "gcChangelogs";
            this.gcChangelogs.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rteChanges});
            this.gcChangelogs.Size = new System.Drawing.Size(540, 664);
            this.gcChangelogs.TabIndex = 0;
            this.gcChangelogs.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.tvChangelogs});
            // 
            // tvChangelogs
            // 
            this.tvChangelogs.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colDateCreated,
            this.colChanges,
            this.colVersion});
            this.tvChangelogs.GridControl = this.gcChangelogs;
            this.tvChangelogs.Name = "tvChangelogs";
            this.tvChangelogs.OptionsTiles.GroupTextPadding = new System.Windows.Forms.Padding(12, 8, 12, 8);
            this.tvChangelogs.OptionsTiles.IndentBetweenGroups = 0;
            this.tvChangelogs.OptionsTiles.IndentBetweenItems = 0;
            this.tvChangelogs.OptionsTiles.ItemSize = new System.Drawing.Size(654, 120);
            this.tvChangelogs.OptionsTiles.LayoutMode = DevExpress.XtraGrid.Views.Tile.TileViewLayoutMode.List;
            this.tvChangelogs.OptionsTiles.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.tvChangelogs.OptionsTiles.Padding = new System.Windows.Forms.Padding(0);
            this.tvChangelogs.OptionsTiles.RowCount = 0;
            this.tvChangelogs.TileColumns.Add(tableColumnDefinition1);
            tableRowDefinition1.AutoHeight = true;
            tableRowDefinition1.Length.Value = 20D;
            tableRowDefinition2.AutoHeight = true;
            tableRowDefinition2.Length.Value = 49D;
            tableRowDefinition2.PaddingBottom = 10;
            tableRowDefinition2.PaddingTop = 10;
            tableRowDefinition3.AutoHeight = true;
            tableRowDefinition3.Length.Value = 35D;
            this.tvChangelogs.TileRows.Add(tableRowDefinition1);
            this.tvChangelogs.TileRows.Add(tableRowDefinition2);
            this.tvChangelogs.TileRows.Add(tableRowDefinition3);
            tileViewItemElement1.Column = this.colDateCreated;
            tileViewItemElement1.ImageOptions.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
            tileViewItemElement1.ImageOptions.ImageScaleMode = DevExpress.XtraEditors.TileItemImageScaleMode.Squeeze;
            tileViewItemElement1.RowIndex = 2;
            tileViewItemElement1.Text = "colDateCreated";
            tileViewItemElement1.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
            tileViewItemElement2.Column = this.colChanges;
            tileViewItemElement2.ImageOptions.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
            tileViewItemElement2.ImageOptions.ImageScaleMode = DevExpress.XtraEditors.TileItemImageScaleMode.Squeeze;
            tileViewItemElement2.RowIndex = 1;
            tileViewItemElement2.Text = "colChanges";
            tileViewItemElement2.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.TopLeft;
            tileViewItemElement3.Appearance.Normal.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            tileViewItemElement3.Appearance.Normal.Options.UseFont = true;
            tileViewItemElement3.Column = this.colVersion;
            tileViewItemElement3.ImageOptions.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
            tileViewItemElement3.ImageOptions.ImageScaleMode = DevExpress.XtraEditors.TileItemImageScaleMode.Squeeze;
            tileViewItemElement3.Text = "colVersion";
            tileViewItemElement3.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.TopLeft;
            this.tvChangelogs.TileTemplate.Add(tileViewItemElement1);
            this.tvChangelogs.TileTemplate.Add(tileViewItemElement2);
            this.tvChangelogs.TileTemplate.Add(tileViewItemElement3);
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.ForeColor = System.Drawing.Color.White;
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Appearance.Options.UseForeColor = true;
            this.labelControl1.Location = new System.Drawing.Point(12, 12);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(90, 21);
            this.labelControl1.TabIndex = 1;
            this.labelControl1.Text = "Changelogs";
            // 
            // lblVersion
            // 
            this.lblVersion.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVersion.Appearance.ForeColor = System.Drawing.Color.White;
            this.lblVersion.Appearance.Options.UseFont = true;
            this.lblVersion.Appearance.Options.UseForeColor = true;
            this.lblVersion.Location = new System.Drawing.Point(12, 39);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(152, 17);
            this.lblVersion.TabIndex = 2;
            this.lblVersion.Text = "Current Version: 1.0.0.10";
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.btnClose.Appearance.BorderColor = System.Drawing.Color.White;
            this.btnClose.Appearance.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Appearance.ForeColor = System.Drawing.Color.Black;
            this.btnClose.Appearance.Options.UseBackColor = true;
            this.btnClose.Appearance.Options.UseBorderColor = true;
            this.btnClose.Appearance.Options.UseFont = true;
            this.btnClose.Appearance.Options.UseForeColor = true;
            this.btnClose.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
            this.btnClose.ImageOptions.Image = global::ICTProfilingV3.Properties.Resources.close_32x322;
            this.btnClose.Location = new System.Drawing.Point(511, 12);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(41, 39);
            this.btnClose.TabIndex = 4;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // frmChangelogs
            // 
            this.Appearance.BackColor = System.Drawing.Color.RoyalBlue;
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(564, 737);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lblVersion);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.gcChangelogs);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmChangelogs";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            ((System.ComponentModel.ISupportInitialize)(this.rteChanges)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcChangelogs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tvChangelogs)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gcChangelogs;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl lblVersion;
        private DevExpress.XtraGrid.Views.Tile.TileView tvChangelogs;
        private DevExpress.XtraGrid.Columns.TileViewColumn colDateCreated;
        private DevExpress.XtraGrid.Columns.TileViewColumn colChanges;
        private DevExpress.XtraGrid.Columns.TileViewColumn colVersion;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraEditors.Repository.RepositoryItemRichTextEdit rteChanges;
    }
}