namespace ICTProfilingV3.ActionsForms
{
    partial class frmActionTree
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmActionTree));
            this.panel1 = new System.Windows.Forms.Panel();
            this.labelControl16 = new DevExpress.XtraEditors.LabelControl();
            this.treeActionDropdown = new DevExpress.XtraTreeList.TreeList();
            this.treeListColumn1 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.bsActionTree = new System.Windows.Forms.BindingSource(this.components);
            this.NodeImages = new DevExpress.Utils.ImageCollection(this.components);
            this.btnAddProgram = new DevExpress.XtraEditors.SimpleButton();
            this.treeMenu = new DevExpress.XtraBars.PopupMenu(this.components);
            this.btnAddChildNode = new DevExpress.XtraBars.BarButtonItem();
            this.btnEditNode = new DevExpress.XtraBars.BarButtonItem();
            this.btnDeleteNode = new DevExpress.XtraBars.BarButtonItem();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeActionDropdown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsActionTree)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NodeImages)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeMenu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.panel1.Controls.Add(this.labelControl16);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1133, 41);
            this.panel1.TabIndex = 4;
            // 
            // labelControl16
            // 
            this.labelControl16.Appearance.Font = new System.Drawing.Font("Segoe UI", 16F);
            this.labelControl16.Appearance.ForeColor = System.Drawing.Color.White;
            this.labelControl16.Appearance.Options.UseFont = true;
            this.labelControl16.Appearance.Options.UseForeColor = true;
            this.labelControl16.Location = new System.Drawing.Point(24, 5);
            this.labelControl16.Name = "labelControl16";
            this.labelControl16.Size = new System.Drawing.Size(106, 30);
            this.labelControl16.TabIndex = 1;
            this.labelControl16.Text = "Action Tree";
            // 
            // treeActionDropdown
            // 
            this.treeActionDropdown.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeActionDropdown.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.treeListColumn1});
            this.treeActionDropdown.DataSource = this.bsActionTree;
            this.treeActionDropdown.KeyFieldName = "ActionTree.Id";
            this.treeActionDropdown.Location = new System.Drawing.Point(0, 78);
            this.treeActionDropdown.Name = "treeActionDropdown";
            this.treeActionDropdown.OptionsBehavior.Editable = false;
            this.treeActionDropdown.OptionsMenu.EnableColumnMenu = false;
            this.treeActionDropdown.OptionsMenu.EnableNodeMenu = false;
            this.treeActionDropdown.ParentFieldName = "ActionTree.ParentId";
            this.treeActionDropdown.SelectImageList = this.NodeImages;
            this.treeActionDropdown.Size = new System.Drawing.Size(1133, 716);
            this.treeActionDropdown.StateImageList = this.NodeImages;
            this.treeActionDropdown.TabIndex = 5;
            this.treeActionDropdown.MouseClick += new System.Windows.Forms.MouseEventHandler(this.treeActionDropdown_MouseClick);
            // 
            // treeListColumn1
            // 
            this.treeListColumn1.Caption = " ";
            this.treeListColumn1.FieldName = "NodeValue";
            this.treeListColumn1.Name = "treeListColumn1";
            this.treeListColumn1.Visible = true;
            this.treeListColumn1.VisibleIndex = 0;
            // 
            // bsActionTree
            // 
            this.bsActionTree.DataSource = typeof(Models.ViewModels.ActionTreeViewModel);
            // 
            // NodeImages
            // 
            this.NodeImages.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("NodeImages.ImageStream")));
            this.NodeImages.Images.SetKeyName(0, "bookmark_32x32.png");
            this.NodeImages.Images.SetKeyName(1, "notes_32x32.png");
            this.NodeImages.Images.SetKeyName(2, "editingunmergecells_32x32.png");
            this.NodeImages.Images.SetKeyName(3, "edittask_32x32.png");
            // 
            // btnAddProgram
            // 
            this.btnAddProgram.Appearance.BackColor = System.Drawing.Color.RoyalBlue;
            this.btnAddProgram.Appearance.ForeColor = System.Drawing.Color.Black;
            this.btnAddProgram.Appearance.Options.UseBackColor = true;
            this.btnAddProgram.Appearance.Options.UseForeColor = true;
            this.btnAddProgram.ImageOptions.Image = global::ICTProfilingV3.Properties.Resources.treeview_16x162;
            this.btnAddProgram.Location = new System.Drawing.Point(11, 45);
            this.btnAddProgram.Margin = new System.Windows.Forms.Padding(2);
            this.btnAddProgram.Name = "btnAddProgram";
            this.btnAddProgram.Size = new System.Drawing.Size(131, 28);
            this.btnAddProgram.TabIndex = 102;
            this.btnAddProgram.Text = "Add New Program";
            this.btnAddProgram.Click += new System.EventHandler(this.btnAddProgram_Click);
            // 
            // treeMenu
            // 
            this.treeMenu.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.btnAddChildNode),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnEditNode),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnDeleteNode)});
            this.treeMenu.Manager = this.barManager1;
            this.treeMenu.Name = "treeMenu";
            // 
            // btnAddChildNode
            // 
            this.btnAddChildNode.Caption = "Add Child Node";
            this.btnAddChildNode.Id = 0;
            this.btnAddChildNode.ImageOptions.Image = global::ICTProfilingV3.Properties.Resources.add_16x1610;
            this.btnAddChildNode.ImageOptions.LargeImage = global::ICTProfilingV3.Properties.Resources.add_32x321;
            this.btnAddChildNode.Name = "btnAddChildNode";
            this.btnAddChildNode.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnAddChildNode_ItemClick);
            // 
            // btnEditNode
            // 
            this.btnEditNode.Caption = "Edit Node";
            this.btnEditNode.Id = 1;
            this.btnEditNode.ImageOptions.Image = global::ICTProfilingV3.Properties.Resources.edittask_16x1610;
            this.btnEditNode.ImageOptions.LargeImage = global::ICTProfilingV3.Properties.Resources.edittask_32x32;
            this.btnEditNode.Name = "btnEditNode";
            this.btnEditNode.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnEditNode_ItemClick);
            // 
            // btnDeleteNode
            // 
            this.btnDeleteNode.Caption = "Delete Node";
            this.btnDeleteNode.Id = 2;
            this.btnDeleteNode.ImageOptions.Image = global::ICTProfilingV3.Properties.Resources.cancel_16x161;
            this.btnDeleteNode.ImageOptions.LargeImage = global::ICTProfilingV3.Properties.Resources.cancel_32x32;
            this.btnDeleteNode.Name = "btnDeleteNode";
            this.btnDeleteNode.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnDeleteNode_ItemClick);
            // 
            // barManager1
            // 
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.btnAddChildNode,
            this.btnEditNode,
            this.btnDeleteNode});
            this.barManager1.MaxItemId = 3;
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager1;
            this.barDockControlTop.Size = new System.Drawing.Size(1133, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 794);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Size = new System.Drawing.Size(1133, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 794);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1133, 0);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 794);
            // 
            // frmActionTree
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1133, 794);
            this.Controls.Add(this.btnAddProgram);
            this.Controls.Add(this.treeActionDropdown);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "frmActionTree";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeActionDropdown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsActionTree)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NodeImages)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeMenu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraEditors.LabelControl labelControl16;
        private DevExpress.XtraTreeList.TreeList treeActionDropdown;
        private DevExpress.XtraEditors.SimpleButton btnAddProgram;
        private DevExpress.Utils.ImageCollection NodeImages;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn1;
        private System.Windows.Forms.BindingSource actionTreeViewModelBindingSource;
        private System.Windows.Forms.BindingSource bsActionTree;
        private DevExpress.XtraBars.PopupMenu treeMenu;
        private DevExpress.XtraBars.BarButtonItem btnAddChildNode;
        private DevExpress.XtraBars.BarButtonItem btnEditNode;
        private DevExpress.XtraBars.BarButtonItem btnDeleteNode;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
    }
}