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
            this.NodeImages = new DevExpress.Utils.ImageCollection(this.components);
            this.btnAddProgram = new DevExpress.XtraEditors.SimpleButton();
            this.btnAddNode = new DevExpress.XtraEditors.SimpleButton();
            this.btnEditNode = new DevExpress.XtraEditors.SimpleButton();
            this.btnDeleteNode = new DevExpress.XtraEditors.SimpleButton();
            this.bsActionTree = new System.Windows.Forms.BindingSource(this.components);
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeActionDropdown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NodeImages)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsActionTree)).BeginInit();
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
            this.treeActionDropdown.ParentFieldName = "ActionTree.ParentId";
            this.treeActionDropdown.SelectImageList = this.NodeImages;
            this.treeActionDropdown.Size = new System.Drawing.Size(1133, 716);
            this.treeActionDropdown.StateImageList = this.NodeImages;
            this.treeActionDropdown.TabIndex = 5;
            // 
            // treeListColumn1
            // 
            this.treeListColumn1.Caption = " ";
            this.treeListColumn1.FieldName = "NodeValue";
            this.treeListColumn1.Name = "treeListColumn1";
            this.treeListColumn1.Visible = true;
            this.treeListColumn1.VisibleIndex = 0;
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
            // btnAddNode
            // 
            this.btnAddNode.Appearance.BackColor = System.Drawing.Color.RoyalBlue;
            this.btnAddNode.Appearance.ForeColor = System.Drawing.Color.Black;
            this.btnAddNode.Appearance.Options.UseBackColor = true;
            this.btnAddNode.Appearance.Options.UseForeColor = true;
            this.btnAddNode.ImageOptions.Image = global::ICTProfilingV3.Properties.Resources.autoexpand_16x16;
            this.btnAddNode.Location = new System.Drawing.Point(146, 45);
            this.btnAddNode.Margin = new System.Windows.Forms.Padding(2);
            this.btnAddNode.Name = "btnAddNode";
            this.btnAddNode.Size = new System.Drawing.Size(131, 28);
            this.btnAddNode.TabIndex = 103;
            this.btnAddNode.Text = "Add New Node";
            this.btnAddNode.Click += new System.EventHandler(this.btnAddNode_Click);
            // 
            // btnEditNode
            // 
            this.btnEditNode.Appearance.BackColor = System.Drawing.Color.RoyalBlue;
            this.btnEditNode.Appearance.ForeColor = System.Drawing.Color.Black;
            this.btnEditNode.Appearance.Options.UseBackColor = true;
            this.btnEditNode.Appearance.Options.UseForeColor = true;
            this.btnEditNode.ImageOptions.Image = global::ICTProfilingV3.Properties.Resources.edit_16x162;
            this.btnEditNode.Location = new System.Drawing.Point(281, 45);
            this.btnEditNode.Margin = new System.Windows.Forms.Padding(2);
            this.btnEditNode.Name = "btnEditNode";
            this.btnEditNode.Size = new System.Drawing.Size(131, 28);
            this.btnEditNode.TabIndex = 104;
            this.btnEditNode.Text = "Edit Node";
            // 
            // btnDeleteNode
            // 
            this.btnDeleteNode.Appearance.BackColor = System.Drawing.Color.RoyalBlue;
            this.btnDeleteNode.Appearance.ForeColor = System.Drawing.Color.Black;
            this.btnDeleteNode.Appearance.Options.UseBackColor = true;
            this.btnDeleteNode.Appearance.Options.UseForeColor = true;
            this.btnDeleteNode.ImageOptions.Image = global::ICTProfilingV3.Properties.Resources.close_16x1610;
            this.btnDeleteNode.Location = new System.Drawing.Point(416, 45);
            this.btnDeleteNode.Margin = new System.Windows.Forms.Padding(2);
            this.btnDeleteNode.Name = "btnDeleteNode";
            this.btnDeleteNode.Size = new System.Drawing.Size(131, 28);
            this.btnDeleteNode.TabIndex = 105;
            this.btnDeleteNode.Text = "Delete Node";
            // 
            // bsActionTree
            // 
            this.bsActionTree.DataSource = typeof(Models.ViewModels.ActionTreeViewModel);
            // 
            // frmActionTree
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1133, 794);
            this.Controls.Add(this.btnDeleteNode);
            this.Controls.Add(this.btnEditNode);
            this.Controls.Add(this.btnAddNode);
            this.Controls.Add(this.btnAddProgram);
            this.Controls.Add(this.treeActionDropdown);
            this.Controls.Add(this.panel1);
            this.Name = "frmActionTree";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeActionDropdown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NodeImages)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsActionTree)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraEditors.LabelControl labelControl16;
        private DevExpress.XtraTreeList.TreeList treeActionDropdown;
        private DevExpress.XtraEditors.SimpleButton btnAddProgram;
        private DevExpress.XtraEditors.SimpleButton btnAddNode;
        private DevExpress.XtraEditors.SimpleButton btnEditNode;
        private DevExpress.XtraEditors.SimpleButton btnDeleteNode;
        private DevExpress.Utils.ImageCollection NodeImages;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn1;
        private System.Windows.Forms.BindingSource actionTreeViewModelBindingSource;
        private System.Windows.Forms.BindingSource bsActionTree;
    }
}