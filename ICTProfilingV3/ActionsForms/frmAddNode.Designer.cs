namespace ICTProfilingV3.ActionsForms
{
    partial class frmAddNode
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
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.panel1 = new System.Windows.Forms.Panel();
            this.labelControl16 = new DevExpress.XtraEditors.LabelControl();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.spinOrder = new DevExpress.XtraEditors.SpinEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txtValue = new DevExpress.XtraEditors.TextEdit();
            this.lueParentNode = new DevExpress.XtraEditors.LookUpEdit();
            this.actionsDropdownsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spinOrder.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtValue.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueParentNode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.actionsDropdownsBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(9, 88);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(28, 13);
            this.labelControl2.TabIndex = 113;
            this.labelControl2.Text = "Order";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(9, 62);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(60, 13);
            this.labelControl1.TabIndex = 111;
            this.labelControl1.Text = "Parent Node";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.panel1.Controls.Add(this.labelControl16);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(461, 41);
            this.panel1.TabIndex = 107;
            // 
            // labelControl16
            // 
            this.labelControl16.Appearance.Font = new System.Drawing.Font("Segoe UI", 16F);
            this.labelControl16.Appearance.ForeColor = System.Drawing.Color.White;
            this.labelControl16.Appearance.Options.UseFont = true;
            this.labelControl16.Appearance.Options.UseForeColor = true;
            this.labelControl16.Location = new System.Drawing.Point(24, 5);
            this.labelControl16.Name = "labelControl16";
            this.labelControl16.Size = new System.Drawing.Size(138, 30);
            this.labelControl16.TabIndex = 1;
            this.labelControl16.Text = "Add/Edit Node";
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Appearance.BackColor = System.Drawing.Color.RoyalBlue;
            this.btnSave.Appearance.ForeColor = System.Drawing.Color.Black;
            this.btnSave.Appearance.Options.UseBackColor = true;
            this.btnSave.Appearance.Options.UseForeColor = true;
            this.btnSave.ImageOptions.Image = global::ICTProfilingV3.Properties.Resources.save_16x163;
            this.btnSave.Location = new System.Drawing.Point(246, 139);
            this.btnSave.Margin = new System.Windows.Forms.Padding(2);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(93, 28);
            this.btnSave.TabIndex = 109;
            this.btnSave.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Appearance.BackColor = System.Drawing.Color.RoyalBlue;
            this.btnClose.Appearance.ForeColor = System.Drawing.Color.Black;
            this.btnClose.Appearance.Options.UseBackColor = true;
            this.btnClose.Appearance.Options.UseForeColor = true;
            this.btnClose.ImageOptions.Image = global::ICTProfilingV3.Properties.Resources.close_16x1610;
            this.btnClose.Location = new System.Drawing.Point(343, 139);
            this.btnClose.Margin = new System.Windows.Forms.Padding(2);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(97, 28);
            this.btnClose.TabIndex = 108;
            this.btnClose.Text = "Close";
            // 
            // spinOrder
            // 
            this.spinOrder.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spinOrder.Location = new System.Drawing.Point(87, 85);
            this.spinOrder.Name = "spinOrder";
            this.spinOrder.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.spinOrder.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Default;
            this.spinOrder.Size = new System.Drawing.Size(353, 20);
            this.spinOrder.TabIndex = 112;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(9, 114);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(25, 13);
            this.labelControl3.TabIndex = 115;
            this.labelControl3.Text = "Node";
            // 
            // txtValue
            // 
            this.txtValue.Location = new System.Drawing.Point(87, 111);
            this.txtValue.Name = "txtValue";
            this.txtValue.Size = new System.Drawing.Size(353, 20);
            this.txtValue.TabIndex = 114;
            // 
            // lueParentNode
            // 
            this.lueParentNode.Location = new System.Drawing.Point(87, 59);
            this.lueParentNode.Name = "lueParentNode";
            this.lueParentNode.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lueParentNode.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Value", "Value"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("ActionCategory", "Category")});
            this.lueParentNode.Properties.DataSource = this.actionsDropdownsBindingSource;
            this.lueParentNode.Properties.DisplayMember = "Value";
            this.lueParentNode.Properties.NullText = "";
            this.lueParentNode.Properties.ValueMember = "Id";
            this.lueParentNode.Size = new System.Drawing.Size(353, 20);
            this.lueParentNode.TabIndex = 110;
            // 
            // actionsDropdownsBindingSource
            // 
            this.actionsDropdownsBindingSource.DataSource = typeof(Models.Entities.ActionsDropdowns);
            // 
            // frmAddNode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(461, 177);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.txtValue);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.spinOrder);
            this.Controls.Add(this.lueParentNode);
            this.Name = "frmAddNode";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spinOrder.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtValue.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueParentNode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.actionsDropdownsBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraEditors.LabelControl labelControl16;
        private DevExpress.XtraEditors.SpinEdit spinOrder;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit txtValue;
        private DevExpress.XtraEditors.LookUpEdit lueParentNode;
        private System.Windows.Forms.BindingSource actionsDropdownsBindingSource;
    }
}