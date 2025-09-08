namespace ICTProfilingV3.Equipments
{
    partial class frmAddEditModel
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
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.memoDesc = new DevExpress.XtraEditors.MemoEdit();
            this.slueEquipment = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.searchLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.labelControl16 = new DevExpress.XtraEditors.LabelControl();
            this.lblEpisNo = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.txtModel = new DevExpress.XtraEditors.TextEdit();
            this.lueBrand = new DevExpress.XtraEditors.LookUpEdit();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.memoDesc.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.slueEquipment.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtModel.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueBrand.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(16, 183);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(28, 13);
            this.labelControl3.TabIndex = 102;
            this.labelControl3.Text = "Brand";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(16, 80);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(53, 13);
            this.labelControl2.TabIndex = 99;
            this.labelControl2.Text = "Description";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(16, 55);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(50, 13);
            this.labelControl1.TabIndex = 98;
            this.labelControl1.Text = "Equipment";
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Appearance.BackColor = System.Drawing.Color.RoyalBlue;
            this.btnSave.Appearance.ForeColor = System.Drawing.Color.Black;
            this.btnSave.Appearance.Options.UseBackColor = true;
            this.btnSave.Appearance.Options.UseForeColor = true;
            this.btnSave.ImageOptions.Image = global::ICTProfilingV3.Properties.Resources.save_16x161;
            this.btnSave.Location = new System.Drawing.Point(325, 231);
            this.btnSave.Margin = new System.Windows.Forms.Padding(2);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(97, 28);
            this.btnSave.TabIndex = 103;
            this.btnSave.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_ClickAsync);
            // 
            // memoDesc
            // 
            this.memoDesc.Location = new System.Drawing.Point(91, 78);
            this.memoDesc.Name = "memoDesc";
            this.memoDesc.Properties.ReadOnly = true;
            this.memoDesc.Size = new System.Drawing.Size(331, 96);
            this.memoDesc.TabIndex = 100;
            // 
            // slueEquipment
            // 
            this.slueEquipment.EditValue = "";
            this.slueEquipment.Location = new System.Drawing.Point(91, 52);
            this.slueEquipment.Name = "slueEquipment";
            this.slueEquipment.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.slueEquipment.Properties.DisplayMember = "Equipment";
            this.slueEquipment.Properties.NullText = "";
            this.slueEquipment.Properties.PopupView = this.searchLookUpEdit1View;
            this.slueEquipment.Properties.ValueMember = "Id";
            this.slueEquipment.Size = new System.Drawing.Size(331, 20);
            this.slueEquipment.TabIndex = 97;
            this.slueEquipment.EditValueChanged += new System.EventHandler(this.slueEquipment_EditValueChanged);
            // 
            // searchLookUpEdit1View
            // 
            this.searchLookUpEdit1View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2});
            this.searchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.searchLookUpEdit1View.Name = "searchLookUpEdit1View";
            this.searchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.searchLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.panel1.Controls.Add(this.labelControl16);
            this.panel1.Controls.Add(this.lblEpisNo);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(439, 37);
            this.panel1.TabIndex = 104;
            // 
            // labelControl16
            // 
            this.labelControl16.Appearance.Font = new System.Drawing.Font("Segoe UI", 16F);
            this.labelControl16.Appearance.ForeColor = System.Drawing.Color.White;
            this.labelControl16.Appearance.Options.UseFont = true;
            this.labelControl16.Appearance.Options.UseForeColor = true;
            this.labelControl16.Location = new System.Drawing.Point(24, 4);
            this.labelControl16.Name = "labelControl16";
            this.labelControl16.Size = new System.Drawing.Size(146, 30);
            this.labelControl16.TabIndex = 2;
            this.labelControl16.Text = "Add/Edit Model";
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
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(16, 209);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(28, 13);
            this.labelControl4.TabIndex = 106;
            this.labelControl4.Text = "Model";
            // 
            // txtModel
            // 
            this.txtModel.Location = new System.Drawing.Point(91, 206);
            this.txtModel.Name = "txtModel";
            this.txtModel.Size = new System.Drawing.Size(331, 20);
            this.txtModel.TabIndex = 105;
            // 
            // lueBrand
            // 
            this.lueBrand.Location = new System.Drawing.Point(91, 180);
            this.lueBrand.Name = "lueBrand";
            this.lueBrand.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lueBrand.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("BrandName", "Brand")});
            this.lueBrand.Properties.DisplayMember = "BrandName";
            this.lueBrand.Properties.NullText = "";
            this.lueBrand.Properties.ValueMember = "Id";
            this.lueBrand.Size = new System.Drawing.Size(331, 20);
            this.lueBrand.TabIndex = 101;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Equipment";
            this.gridColumn1.FieldName = "Equipment";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Description";
            this.gridColumn2.FieldName = "Description";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            // 
            // frmAddEditModel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(439, 268);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.txtModel);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.memoDesc);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.slueEquipment);
            this.Controls.Add(this.lueBrand);
            this.Name = "frmAddEditModel";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            ((System.ComponentModel.ISupportInitialize)(this.memoDesc.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.slueEquipment.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtModel.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueBrand.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.MemoEdit memoDesc;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.SearchLookUpEdit slueEquipment;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEdit1View;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraEditors.LabelControl labelControl16;
        private DevExpress.XtraEditors.LabelControl lblEpisNo;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.TextEdit txtModel;
        private DevExpress.XtraEditors.LookUpEdit lueBrand;
    }
}