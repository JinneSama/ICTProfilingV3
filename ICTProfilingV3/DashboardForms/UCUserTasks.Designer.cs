namespace ICTProfilingV3.DashboardForms
{
    partial class UCUserTasks
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
            this.components = new System.ComponentModel.Container();
            DevExpress.Utils.SimpleContextButton simpleContextButton1 = new DevExpress.Utils.SimpleContextButton();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCUserTasks));
            DevExpress.XtraEditors.TableLayout.TableColumnDefinition tableColumnDefinition1 = new DevExpress.XtraEditors.TableLayout.TableColumnDefinition();
            DevExpress.XtraEditors.TableLayout.TableRowDefinition tableRowDefinition1 = new DevExpress.XtraEditors.TableLayout.TableRowDefinition();
            DevExpress.XtraEditors.TableLayout.TableRowDefinition tableRowDefinition2 = new DevExpress.XtraEditors.TableLayout.TableRowDefinition();
            DevExpress.XtraEditors.TableLayout.TableRowDefinition tableRowDefinition3 = new DevExpress.XtraEditors.TableLayout.TableRowDefinition();
            DevExpress.XtraEditors.TableLayout.TableRowDefinition tableRowDefinition4 = new DevExpress.XtraEditors.TableLayout.TableRowDefinition();
            DevExpress.XtraEditors.TableLayout.TableRowDefinition tableRowDefinition5 = new DevExpress.XtraEditors.TableLayout.TableRowDefinition();
            DevExpress.XtraEditors.TableLayout.TableRowDefinition tableRowDefinition6 = new DevExpress.XtraEditors.TableLayout.TableRowDefinition();
            DevExpress.XtraEditors.TableLayout.TableRowDefinition tableRowDefinition7 = new DevExpress.XtraEditors.TableLayout.TableRowDefinition();
            DevExpress.XtraGrid.Views.Tile.TileViewItemElement tileViewItemElement1 = new DevExpress.XtraGrid.Views.Tile.TileViewItemElement();
            DevExpress.XtraGrid.Views.Tile.TileViewItemElement tileViewItemElement2 = new DevExpress.XtraGrid.Views.Tile.TileViewItemElement();
            DevExpress.XtraGrid.Views.Tile.TileViewItemElement tileViewItemElement3 = new DevExpress.XtraGrid.Views.Tile.TileViewItemElement();
            DevExpress.XtraGrid.Views.Tile.TileViewItemElement tileViewItemElement4 = new DevExpress.XtraGrid.Views.Tile.TileViewItemElement();
            DevExpress.XtraGrid.Views.Tile.TileViewItemElement tileViewItemElement5 = new DevExpress.XtraGrid.Views.Tile.TileViewItemElement();
            DevExpress.XtraGrid.Views.Tile.TileViewItemElement tileViewItemElement6 = new DevExpress.XtraGrid.Views.Tile.TileViewItemElement();
            DevExpress.XtraGrid.Views.Tile.TileViewItemElement tileViewItemElement7 = new DevExpress.XtraGrid.Views.Tile.TileViewItemElement();
            DevExpress.XtraGrid.Views.Tile.TileViewItemElement tileViewItemElement8 = new DevExpress.XtraGrid.Views.Tile.TileViewItemElement();
            this.colDateCreated = new DevExpress.XtraGrid.Columns.TileViewColumn();
            this.colAttachedImage = new DevExpress.XtraGrid.Columns.TileViewColumn();
            this.colEpisNo = new DevExpress.XtraGrid.Columns.TileViewColumn();
            this.hplEpisNo = new DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit();
            this.colOffice = new DevExpress.XtraGrid.Columns.TileViewColumn();
            this.colReqBy = new DevExpress.XtraGrid.Columns.TileViewColumn();
            this.colReqByPos = new DevExpress.XtraGrid.Columns.TileViewColumn();
            this.colLine = new DevExpress.XtraGrid.Columns.TileViewColumn();
            this.colLabel = new DevExpress.XtraGrid.Columns.TileViewColumn();
            this.colCaption = new DevExpress.XtraGrid.Columns.TileViewColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.labelControl16 = new DevExpress.XtraEditors.LabelControl();
            this.lblEpisNo = new DevExpress.XtraEditors.LabelControl();
            this.gcTasks = new DevExpress.XtraGrid.GridControl();
            this.tileTasks = new DevExpress.XtraGrid.Views.Tile.TileView();
            this.colStatus = new DevExpress.XtraGrid.Columns.TileViewColumn();
            this.colMembers = new DevExpress.XtraGrid.Columns.TileViewColumn();
            this.popupMenu1 = new DevExpress.XtraBars.PopupMenu(this.components);
            this.btnNavigate = new DevExpress.XtraBars.BarButtonItem();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            ((System.ComponentModel.ISupportInitialize)(this.hplEpisNo)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcTasks)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tileTasks)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            this.SuspendLayout();
            // 
            // colDateCreated
            // 
            this.colDateCreated.FieldName = "Ticket.DateCreated";
            this.colDateCreated.Name = "colDateCreated";
            this.colDateCreated.Visible = true;
            this.colDateCreated.VisibleIndex = 4;
            // 
            // colAttachedImage
            // 
            this.colAttachedImage.FieldName = "AttachedImage";
            this.colAttachedImage.Name = "colAttachedImage";
            this.colAttachedImage.Visible = true;
            this.colAttachedImage.VisibleIndex = 5;
            // 
            // colEpisNo
            // 
            this.colEpisNo.ColumnEdit = this.hplEpisNo;
            this.colEpisNo.FieldName = "EPiSNo";
            this.colEpisNo.Name = "colEpisNo";
            this.colEpisNo.Visible = true;
            this.colEpisNo.VisibleIndex = 6;
            // 
            // hplEpisNo
            // 
            this.hplEpisNo.AutoHeight = false;
            this.hplEpisNo.Name = "hplEpisNo";
            this.hplEpisNo.Click += new System.EventHandler(this.hplEpisNo_Click);
            // 
            // colOffice
            // 
            this.colOffice.FieldName = "Office";
            this.colOffice.Name = "colOffice";
            this.colOffice.Visible = true;
            this.colOffice.VisibleIndex = 7;
            // 
            // colReqBy
            // 
            this.colReqBy.FieldName = "ReqByName";
            this.colReqBy.Name = "colReqBy";
            this.colReqBy.Visible = true;
            this.colReqBy.VisibleIndex = 8;
            // 
            // colReqByPos
            // 
            this.colReqByPos.FieldName = "ReqByPos";
            this.colReqByPos.Name = "colReqByPos";
            this.colReqByPos.Visible = true;
            this.colReqByPos.VisibleIndex = 9;
            // 
            // colLine
            // 
            this.colLine.Caption = "tileViewColumn1";
            this.colLine.Name = "colLine";
            this.colLine.Visible = true;
            this.colLine.VisibleIndex = 10;
            // 
            // colLabel
            // 
            this.colLabel.Caption = "Label";
            this.colLabel.FieldName = "Ticket.RequestType";
            this.colLabel.Name = "colLabel";
            this.colLabel.Visible = true;
            this.colLabel.VisibleIndex = 3;
            // 
            // colCaption
            // 
            this.colCaption.Caption = "Caption";
            this.colCaption.FieldName = "Ticket.RequestType";
            this.colCaption.Name = "colCaption";
            this.colCaption.Visible = true;
            this.colCaption.VisibleIndex = 1;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.panel1.Controls.Add(this.labelControl16);
            this.panel1.Controls.Add(this.lblEpisNo);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1164, 37);
            this.panel1.TabIndex = 83;
            // 
            // labelControl16
            // 
            this.labelControl16.Appearance.Font = new System.Drawing.Font("Segoe UI", 16F);
            this.labelControl16.Appearance.ForeColor = System.Drawing.Color.White;
            this.labelControl16.Appearance.Options.UseFont = true;
            this.labelControl16.Appearance.Options.UseForeColor = true;
            this.labelControl16.Location = new System.Drawing.Point(24, 4);
            this.labelControl16.Name = "labelControl16";
            this.labelControl16.Size = new System.Drawing.Size(50, 30);
            this.labelControl16.TabIndex = 2;
            this.labelControl16.Text = "Tasks";
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
            // gcTasks
            // 
            this.gcTasks.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gcTasks.Location = new System.Drawing.Point(0, 39);
            this.gcTasks.MainView = this.tileTasks;
            this.gcTasks.Name = "gcTasks";
            this.gcTasks.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.hplEpisNo});
            this.gcTasks.Size = new System.Drawing.Size(1164, 631);
            this.gcTasks.TabIndex = 84;
            this.gcTasks.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.tileTasks});
            // 
            // tileTasks
            // 
            this.tileTasks.Appearance.Group.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tileTasks.Appearance.Group.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.tileTasks.Appearance.Group.Options.UseFont = true;
            this.tileTasks.Appearance.Group.Options.UseForeColor = true;
            this.tileTasks.Appearance.ItemNormal.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tileTasks.Appearance.ItemNormal.Options.UseFont = true;
            this.tileTasks.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colStatus,
            this.colCaption,
            this.colMembers,
            this.colLabel,
            this.colDateCreated,
            this.colAttachedImage,
            this.colEpisNo,
            this.colOffice,
            this.colReqBy,
            this.colReqByPos,
            this.colLine});
            this.tileTasks.ColumnSet.GroupColumn = this.colStatus;
            this.tileTasks.GridControl = this.gcTasks;
            this.tileTasks.Name = "tileTasks";
            this.tileTasks.OptionsBehavior.AllowSmoothScrolling = true;
            this.tileTasks.OptionsBehavior.EditingMode = DevExpress.XtraGrid.Views.Tile.TileViewEditingMode.EditForm;
            this.tileTasks.OptionsDragDrop.AllowDrag = true;
            this.tileTasks.OptionsEditForm.ActionOnModifiedRowChange = DevExpress.XtraGrid.Views.Grid.EditFormModifiedAction.Nothing;
            this.tileTasks.OptionsEditForm.PopupEditFormWidth = 500;
            this.tileTasks.OptionsEditForm.ShowUpdateCancelPanel = DevExpress.Utils.DefaultBoolean.True;
            this.tileTasks.OptionsFind.AllowFindPanel = false;
            this.tileTasks.OptionsKanban.GroupFooterButton.Visible = DevExpress.Utils.DefaultBoolean.False;
            simpleContextButton1.AlignmentOptions.Panel = DevExpress.Utils.ContextItemPanel.Center;
            simpleContextButton1.AlignmentOptions.Position = DevExpress.Utils.ContextItemPosition.Far;
            simpleContextButton1.Id = new System.Guid("cb2e2e03-435e-4146-921e-8679c0fc7372");
            simpleContextButton1.ImageOptionsCollection.ItemNormal.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("resource.SvgImage")));
            simpleContextButton1.ImageOptionsCollection.ItemNormal.SvgImageSize = new System.Drawing.Size(16, 16);
            simpleContextButton1.Name = "btnAdd";
            simpleContextButton1.ToolTip = "Add a new card";
            simpleContextButton1.Visibility = DevExpress.Utils.ContextItemVisibility.Visible;
            this.tileTasks.OptionsKanban.GroupHeaderContextButtons.Add(simpleContextButton1);
            this.tileTasks.OptionsTiles.GroupTextPadding = new System.Windows.Forms.Padding(0, 12, 0, 12);
            this.tileTasks.OptionsTiles.HighlightFocusedTileStyle = DevExpress.XtraGrid.Views.Tile.HighlightFocusedTileStyle.Content;
            this.tileTasks.OptionsTiles.HorizontalContentAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.tileTasks.OptionsTiles.IndentBetweenGroups = 20;
            this.tileTasks.OptionsTiles.IndentBetweenItems = 5;
            this.tileTasks.OptionsTiles.ItemPadding = new System.Windows.Forms.Padding(10);
            this.tileTasks.OptionsTiles.ItemSize = new System.Drawing.Size(290, 188);
            this.tileTasks.OptionsTiles.LayoutMode = DevExpress.XtraGrid.Views.Tile.TileViewLayoutMode.Kanban;
            this.tileTasks.OptionsTiles.Padding = new System.Windows.Forms.Padding(20, 30, 20, 15);
            this.tileTasks.OptionsTiles.VerticalContentAlignment = DevExpress.Utils.VertAlignment.Top;
            this.tileTasks.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.colStatus, DevExpress.Data.ColumnSortOrder.Ascending)});
            this.tileTasks.TileColumns.Add(tableColumnDefinition1);
            tableRowDefinition1.AutoHeight = true;
            tableRowDefinition1.Length.Type = DevExpress.XtraEditors.TableLayout.TableDefinitionLengthType.Pixel;
            tableRowDefinition1.Length.Value = 24D;
            tableRowDefinition1.PaddingBottom = 5;
            tableRowDefinition2.AutoHeight = true;
            tableRowDefinition2.Length.Value = 12D;
            tableRowDefinition3.AutoHeight = true;
            tableRowDefinition3.Length.Value = 43D;
            tableRowDefinition3.PaddingBottom = 5;
            tableRowDefinition4.AutoHeight = true;
            tableRowDefinition4.Length.Type = DevExpress.XtraEditors.TableLayout.TableDefinitionLengthType.Pixel;
            tableRowDefinition4.Length.Value = 23D;
            tableRowDefinition4.PaddingBottom = 5;
            tableRowDefinition5.AutoHeight = true;
            tableRowDefinition5.Length.Type = DevExpress.XtraEditors.TableLayout.TableDefinitionLengthType.Pixel;
            tableRowDefinition5.Length.Value = 23D;
            tableRowDefinition6.AutoHeight = true;
            tableRowDefinition6.Length.Type = DevExpress.XtraEditors.TableLayout.TableDefinitionLengthType.Pixel;
            tableRowDefinition6.Length.Value = 28D;
            tableRowDefinition6.PaddingBottom = 10;
            tableRowDefinition7.AutoHeight = true;
            tableRowDefinition7.Length.Type = DevExpress.XtraEditors.TableLayout.TableDefinitionLengthType.Pixel;
            tableRowDefinition7.Length.Value = 9D;
            this.tileTasks.TileRows.Add(tableRowDefinition1);
            this.tileTasks.TileRows.Add(tableRowDefinition2);
            this.tileTasks.TileRows.Add(tableRowDefinition3);
            this.tileTasks.TileRows.Add(tableRowDefinition4);
            this.tileTasks.TileRows.Add(tableRowDefinition5);
            this.tileTasks.TileRows.Add(tableRowDefinition6);
            this.tileTasks.TileRows.Add(tableRowDefinition7);
            tileViewItemElement1.Appearance.Normal.Options.UseFont = true;
            tileViewItemElement1.Column = this.colDateCreated;
            tileViewItemElement1.ImageOptions.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
            tileViewItemElement1.ImageOptions.ImageScaleMode = DevExpress.XtraEditors.TileItemImageScaleMode.Squeeze;
            tileViewItemElement1.RowIndex = 3;
            tileViewItemElement1.Text = "colDateCreated";
            tileViewItemElement1.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.TopLeft;
            tileViewItemElement2.Column = this.colAttachedImage;
            tileViewItemElement2.ImageOptions.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
            tileViewItemElement2.ImageOptions.ImageScaleMode = DevExpress.XtraEditors.TileItemImageScaleMode.Squeeze;
            tileViewItemElement2.RowIndex = 2;
            tileViewItemElement2.Text = "colAttachedImage";
            tileViewItemElement2.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
            tileViewItemElement3.Column = this.colEpisNo;
            tileViewItemElement3.ImageOptions.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
            tileViewItemElement3.ImageOptions.ImageScaleMode = DevExpress.XtraEditors.TileItemImageScaleMode.Squeeze;
            tileViewItemElement3.RowIndex = 6;
            tileViewItemElement3.Text = "colEpisNo";
            tileViewItemElement3.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.TopRight;
            tileViewItemElement4.Column = this.colOffice;
            tileViewItemElement4.ImageOptions.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
            tileViewItemElement4.ImageOptions.ImageScaleMode = DevExpress.XtraEditors.TileItemImageScaleMode.Squeeze;
            tileViewItemElement4.RowIndex = 3;
            tileViewItemElement4.Text = "colOffice";
            tileViewItemElement4.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.TopRight;
            tileViewItemElement5.Column = this.colReqBy;
            tileViewItemElement5.ImageOptions.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
            tileViewItemElement5.ImageOptions.ImageScaleMode = DevExpress.XtraEditors.TileItemImageScaleMode.Squeeze;
            tileViewItemElement5.RowIndex = 4;
            tileViewItemElement5.Text = "colReqBy";
            tileViewItemElement5.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
            tileViewItemElement6.Appearance.Normal.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            tileViewItemElement6.Appearance.Normal.Options.UseFont = true;
            tileViewItemElement6.Column = this.colReqByPos;
            tileViewItemElement6.ImageOptions.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
            tileViewItemElement6.ImageOptions.ImageScaleMode = DevExpress.XtraEditors.TileItemImageScaleMode.Squeeze;
            tileViewItemElement6.RowIndex = 5;
            tileViewItemElement6.Text = "colReqByPos";
            tileViewItemElement6.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
            tileViewItemElement7.Appearance.Normal.BackColor = System.Drawing.Color.RoyalBlue;
            tileViewItemElement7.Appearance.Normal.Options.UseBackColor = true;
            tileViewItemElement7.Column = this.colLine;
            tileViewItemElement7.Height = 2;
            tileViewItemElement7.ImageOptions.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
            tileViewItemElement7.ImageOptions.ImageScaleMode = DevExpress.XtraEditors.TileItemImageScaleMode.Squeeze;
            tileViewItemElement7.RowIndex = 1;
            tileViewItemElement7.Text = "colLine";
            tileViewItemElement7.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.TopCenter;
            tileViewItemElement7.Width = 275;
            tileViewItemElement8.Column = this.colLabel;
            tileViewItemElement8.ImageOptions.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
            tileViewItemElement8.ImageOptions.ImageScaleMode = DevExpress.XtraEditors.TileItemImageScaleMode.Squeeze;
            tileViewItemElement8.Text = "colLabel";
            tileViewItemElement8.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.TopLeft;
            this.tileTasks.TileTemplate.Add(tileViewItemElement1);
            this.tileTasks.TileTemplate.Add(tileViewItemElement2);
            this.tileTasks.TileTemplate.Add(tileViewItemElement3);
            this.tileTasks.TileTemplate.Add(tileViewItemElement4);
            this.tileTasks.TileTemplate.Add(tileViewItemElement5);
            this.tileTasks.TileTemplate.Add(tileViewItemElement6);
            this.tileTasks.TileTemplate.Add(tileViewItemElement7);
            this.tileTasks.TileTemplate.Add(tileViewItemElement8);
            this.tileTasks.BeforeItemDrop += new DevExpress.XtraGrid.Views.Tile.TileViewBeforeItemDropEventHandler(this.tileTasks_BeforeItemDrop);
            this.tileTasks.ItemCustomize += new DevExpress.XtraGrid.Views.Tile.TileViewItemCustomizeEventHandler(this.tileTasks_ItemCustomize);
            this.tileTasks.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tileTasks_MouseDown);
            // 
            // colStatus
            // 
            this.colStatus.Caption = "Status";
            this.colStatus.FieldName = "Ticket.TicketStatus";
            this.colStatus.Name = "colStatus";
            this.colStatus.SortMode = DevExpress.XtraGrid.ColumnSortMode.Value;
            this.colStatus.Visible = true;
            this.colStatus.VisibleIndex = 0;
            // 
            // colMembers
            // 
            this.colMembers.Caption = "Members";
            this.colMembers.FieldName = "Members";
            this.colMembers.Name = "colMembers";
            this.colMembers.Visible = true;
            this.colMembers.VisibleIndex = 2;
            // 
            // popupMenu1
            // 
            this.popupMenu1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.btnNavigate)});
            this.popupMenu1.Manager = this.barManager1;
            this.popupMenu1.Name = "popupMenu1";
            // 
            // btnNavigate
            // 
            this.btnNavigate.Caption = "Navigate To Process";
            this.btnNavigate.Id = 0;
            this.btnNavigate.ImageOptions.Image = global::ICTProfilingV3.Properties.Resources.doublenext_16x16;
            this.btnNavigate.ImageOptions.LargeImage = global::ICTProfilingV3.Properties.Resources.doublenext_32x32;
            this.btnNavigate.Name = "btnNavigate";
            this.btnNavigate.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnNavigate_ItemClick);
            // 
            // barManager1
            // 
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.btnNavigate});
            this.barManager1.MaxItemId = 1;
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager1;
            this.barDockControlTop.Size = new System.Drawing.Size(1164, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 670);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Size = new System.Drawing.Size(1164, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 670);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1164, 0);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 670);
            // 
            // UCUserTasks
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gcTasks);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "UCUserTasks";
            this.Size = new System.Drawing.Size(1164, 670);
            this.Load += new System.EventHandler(this.UCUserTasks_Load);
            ((System.ComponentModel.ISupportInitialize)(this.hplEpisNo)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcTasks)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tileTasks)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraEditors.LabelControl labelControl16;
        private DevExpress.XtraEditors.LabelControl lblEpisNo;
        private DevExpress.XtraGrid.GridControl gcTasks;
        private DevExpress.XtraGrid.Views.Tile.TileView tileTasks;
        private DevExpress.XtraGrid.Columns.TileViewColumn colStatus;
        private DevExpress.XtraGrid.Columns.TileViewColumn colCaption;
        private DevExpress.XtraGrid.Columns.TileViewColumn colMembers;
        private DevExpress.XtraGrid.Columns.TileViewColumn colLabel;
        private DevExpress.XtraGrid.Columns.TileViewColumn colDateCreated;
        private DevExpress.XtraGrid.Columns.TileViewColumn colAttachedImage;
        private DevExpress.XtraGrid.Columns.TileViewColumn colEpisNo;
        private DevExpress.XtraGrid.Columns.TileViewColumn colOffice;
        private DevExpress.XtraGrid.Columns.TileViewColumn colReqBy;
        private DevExpress.XtraGrid.Columns.TileViewColumn colReqByPos;
        private DevExpress.XtraGrid.Columns.TileViewColumn colLine;
        private DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit hplEpisNo;
        private DevExpress.XtraBars.PopupMenu popupMenu1;
        private DevExpress.XtraBars.BarButtonItem btnNavigate;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
    }
}
