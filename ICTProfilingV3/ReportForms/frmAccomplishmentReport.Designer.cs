namespace ICTProfilingV3.ReportForms
{
    partial class frmAccomplishmentReport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAccomplishmentReport));
            this.lblNotedBy = new DevExpress.XtraEditors.LabelControl();
            this.lblReviewedBy = new DevExpress.XtraEditors.LabelControl();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.panel1 = new System.Windows.Forms.Panel();
            this.labelControl20 = new DevExpress.XtraEditors.LabelControl();
            this.lblEpisNo = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txtApprovedByPos = new DevExpress.XtraEditors.TextEdit();
            this.txtReviewedByPos = new DevExpress.XtraEditors.TextEdit();
            this.txtPreparedByPos = new DevExpress.XtraEditors.TextEdit();
            this.deDateFrom = new DevExpress.XtraEditors.DateEdit();
            this.deDateTo = new DevExpress.XtraEditors.DateEdit();
            this.slueStaff = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnPreview = new DevExpress.XtraEditors.SimpleButton();
            this.slueApprovedBy = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.gridView5 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn18 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn19 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.slueReviewedBy = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.gridView4 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn16 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn17 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.sluePreparedBy = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn14 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn15 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtApprovedByPos.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtReviewedByPos.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPreparedByPos.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deDateFrom.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deDateFrom.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deDateTo.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deDateTo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.slueStaff.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.slueApprovedBy.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.slueReviewedBy.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sluePreparedBy.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // lblNotedBy
            // 
            this.lblNotedBy.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblNotedBy.Appearance.Options.UseFont = true;
            this.lblNotedBy.Location = new System.Drawing.Point(10, 242);
            this.lblNotedBy.Name = "lblNotedBy";
            this.lblNotedBy.Size = new System.Drawing.Size(68, 15);
            this.lblNotedBy.TabIndex = 122;
            this.lblNotedBy.Text = "Approved By";
            // 
            // lblReviewedBy
            // 
            this.lblReviewedBy.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblReviewedBy.Appearance.Options.UseFont = true;
            this.lblReviewedBy.Location = new System.Drawing.Point(10, 186);
            this.lblReviewedBy.Name = "lblReviewedBy";
            this.lblReviewedBy.Size = new System.Drawing.Size(66, 15);
            this.lblReviewedBy.TabIndex = 121;
            this.lblReviewedBy.Text = "Reviewed By";
            // 
            // labelControl8
            // 
            this.labelControl8.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.labelControl8.Appearance.Options.UseFont = true;
            this.labelControl8.Location = new System.Drawing.Point(10, 130);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(63, 15);
            this.labelControl8.TabIndex = 120;
            this.labelControl8.Text = "Prepared By";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.panel1.Controls.Add(this.labelControl20);
            this.panel1.Controls.Add(this.lblEpisNo);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(466, 37);
            this.panel1.TabIndex = 128;
            // 
            // labelControl20
            // 
            this.labelControl20.Appearance.Font = new System.Drawing.Font("Segoe UI", 16F);
            this.labelControl20.Appearance.ForeColor = System.Drawing.Color.White;
            this.labelControl20.Appearance.Options.UseFont = true;
            this.labelControl20.Appearance.Options.UseForeColor = true;
            this.labelControl20.Location = new System.Drawing.Point(12, 4);
            this.labelControl20.Name = "labelControl20";
            this.labelControl20.Size = new System.Drawing.Size(223, 30);
            this.labelControl20.TabIndex = 3;
            this.labelControl20.Text = "Accomplishment Report";
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
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(10, 102);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(24, 15);
            this.labelControl1.TabIndex = 129;
            this.labelControl1.Text = "Staff";
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Location = new System.Drawing.Point(10, 74);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(40, 15);
            this.labelControl2.TabIndex = 133;
            this.labelControl2.Text = "Date To";
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.labelControl3.Appearance.Options.UseFont = true;
            this.labelControl3.Location = new System.Drawing.Point(10, 46);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(55, 15);
            this.labelControl3.TabIndex = 134;
            this.labelControl3.Text = "Date From";
            // 
            // txtApprovedByPos
            // 
            this.txtApprovedByPos.Location = new System.Drawing.Point(96, 267);
            this.txtApprovedByPos.Name = "txtApprovedByPos";
            this.txtApprovedByPos.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtApprovedByPos.Properties.Appearance.Options.UseFont = true;
            this.txtApprovedByPos.Size = new System.Drawing.Size(360, 22);
            this.txtApprovedByPos.TabIndex = 137;
            // 
            // txtReviewedByPos
            // 
            this.txtReviewedByPos.Location = new System.Drawing.Point(96, 211);
            this.txtReviewedByPos.Name = "txtReviewedByPos";
            this.txtReviewedByPos.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReviewedByPos.Properties.Appearance.Options.UseFont = true;
            this.txtReviewedByPos.Size = new System.Drawing.Size(360, 22);
            this.txtReviewedByPos.TabIndex = 136;
            // 
            // txtPreparedByPos
            // 
            this.txtPreparedByPos.Location = new System.Drawing.Point(96, 155);
            this.txtPreparedByPos.Name = "txtPreparedByPos";
            this.txtPreparedByPos.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPreparedByPos.Properties.Appearance.Options.UseFont = true;
            this.txtPreparedByPos.Properties.ReadOnly = true;
            this.txtPreparedByPos.Size = new System.Drawing.Size(360, 22);
            this.txtPreparedByPos.TabIndex = 135;
            // 
            // deDateFrom
            // 
            this.deDateFrom.EditValue = null;
            this.deDateFrom.Location = new System.Drawing.Point(96, 43);
            this.deDateFrom.Name = "deDateFrom";
            this.deDateFrom.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.deDateFrom.Properties.Appearance.Options.UseFont = true;
            this.deDateFrom.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deDateFrom.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deDateFrom.Properties.MaskSettings.Set("mask", "G");
            this.deDateFrom.Properties.UseMaskAsDisplayFormat = true;
            this.deDateFrom.Size = new System.Drawing.Size(358, 22);
            this.deDateFrom.TabIndex = 132;
            // 
            // deDateTo
            // 
            this.deDateTo.EditValue = null;
            this.deDateTo.Location = new System.Drawing.Point(96, 71);
            this.deDateTo.Name = "deDateTo";
            this.deDateTo.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.deDateTo.Properties.Appearance.Options.UseFont = true;
            this.deDateTo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deDateTo.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deDateTo.Properties.MaskSettings.Set("mask", "G");
            this.deDateTo.Properties.UseMaskAsDisplayFormat = true;
            this.deDateTo.Size = new System.Drawing.Size(358, 22);
            this.deDateTo.TabIndex = 131;
            // 
            // slueStaff
            // 
            this.slueStaff.Location = new System.Drawing.Point(96, 99);
            this.slueStaff.Name = "slueStaff";
            this.slueStaff.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.slueStaff.Properties.Appearance.Options.UseFont = true;
            this.slueStaff.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.slueStaff.Properties.DisplayMember = "FullName";
            this.slueStaff.Properties.NullText = "";
            this.slueStaff.Properties.PopupView = this.gridView1;
            this.slueStaff.Properties.ValueMember = "Id";
            this.slueStaff.Size = new System.Drawing.Size(360, 22);
            this.slueStaff.TabIndex = 130;
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2});
            this.gridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Employee";
            this.gridColumn1.FieldName = "FullName";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Position";
            this.gridColumn2.FieldName = "Position";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Appearance.BackColor = System.Drawing.Color.RoyalBlue;
            this.btnCancel.Appearance.ForeColor = System.Drawing.Color.Black;
            this.btnCancel.Appearance.Options.UseBackColor = true;
            this.btnCancel.Appearance.Options.UseForeColor = true;
            this.btnCancel.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.ImageOptions.Image")));
            this.btnCancel.Location = new System.Drawing.Point(355, 294);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 28);
            this.btnCancel.TabIndex = 127;
            this.btnCancel.Text = "Cancel";
            // 
            // btnPreview
            // 
            this.btnPreview.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPreview.Appearance.BackColor = System.Drawing.Color.RoyalBlue;
            this.btnPreview.Appearance.ForeColor = System.Drawing.Color.Black;
            this.btnPreview.Appearance.Options.UseBackColor = true;
            this.btnPreview.Appearance.Options.UseForeColor = true;
            this.btnPreview.ImageOptions.Image = global::ICTProfilingV3.Properties.Resources.printpreview_16x163;
            this.btnPreview.Location = new System.Drawing.Point(253, 294);
            this.btnPreview.Margin = new System.Windows.Forms.Padding(2);
            this.btnPreview.Name = "btnPreview";
            this.btnPreview.Size = new System.Drawing.Size(98, 28);
            this.btnPreview.TabIndex = 126;
            this.btnPreview.Text = "Preview";
            this.btnPreview.Click += new System.EventHandler(this.btnPreview_Click);
            // 
            // slueApprovedBy
            // 
            this.slueApprovedBy.Location = new System.Drawing.Point(96, 239);
            this.slueApprovedBy.Name = "slueApprovedBy";
            this.slueApprovedBy.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.slueApprovedBy.Properties.Appearance.Options.UseFont = true;
            this.slueApprovedBy.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.slueApprovedBy.Properties.DisplayMember = "FullName";
            this.slueApprovedBy.Properties.NullText = "";
            this.slueApprovedBy.Properties.PopupView = this.gridView5;
            this.slueApprovedBy.Properties.ValueMember = "Id";
            this.slueApprovedBy.Size = new System.Drawing.Size(360, 22);
            this.slueApprovedBy.TabIndex = 125;
            this.slueApprovedBy.EditValueChanged += new System.EventHandler(this.slueApprovedBy_EditValueChanged);
            // 
            // gridView5
            // 
            this.gridView5.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn18,
            this.gridColumn19});
            this.gridView5.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView5.Name = "gridView5";
            this.gridView5.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView5.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn18
            // 
            this.gridColumn18.Caption = "Employee";
            this.gridColumn18.FieldName = "FullName";
            this.gridColumn18.Name = "gridColumn18";
            this.gridColumn18.Visible = true;
            this.gridColumn18.VisibleIndex = 0;
            // 
            // gridColumn19
            // 
            this.gridColumn19.Caption = "Position";
            this.gridColumn19.FieldName = "Position";
            this.gridColumn19.Name = "gridColumn19";
            this.gridColumn19.Visible = true;
            this.gridColumn19.VisibleIndex = 1;
            // 
            // slueReviewedBy
            // 
            this.slueReviewedBy.Location = new System.Drawing.Point(96, 183);
            this.slueReviewedBy.Name = "slueReviewedBy";
            this.slueReviewedBy.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.slueReviewedBy.Properties.Appearance.Options.UseFont = true;
            this.slueReviewedBy.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.slueReviewedBy.Properties.DisplayMember = "FullName";
            this.slueReviewedBy.Properties.NullText = "";
            this.slueReviewedBy.Properties.PopupView = this.gridView4;
            this.slueReviewedBy.Properties.ValueMember = "Id";
            this.slueReviewedBy.Size = new System.Drawing.Size(360, 22);
            this.slueReviewedBy.TabIndex = 124;
            this.slueReviewedBy.EditValueChanged += new System.EventHandler(this.slueReviewedBy_EditValueChanged);
            // 
            // gridView4
            // 
            this.gridView4.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn16,
            this.gridColumn17});
            this.gridView4.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView4.Name = "gridView4";
            this.gridView4.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView4.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn16
            // 
            this.gridColumn16.Caption = "Employee";
            this.gridColumn16.FieldName = "FullName";
            this.gridColumn16.Name = "gridColumn16";
            this.gridColumn16.Visible = true;
            this.gridColumn16.VisibleIndex = 0;
            // 
            // gridColumn17
            // 
            this.gridColumn17.Caption = "Position";
            this.gridColumn17.FieldName = "Position";
            this.gridColumn17.Name = "gridColumn17";
            this.gridColumn17.Visible = true;
            this.gridColumn17.VisibleIndex = 1;
            // 
            // sluePreparedBy
            // 
            this.sluePreparedBy.Location = new System.Drawing.Point(96, 127);
            this.sluePreparedBy.Name = "sluePreparedBy";
            this.sluePreparedBy.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.sluePreparedBy.Properties.Appearance.Options.UseFont = true;
            this.sluePreparedBy.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.sluePreparedBy.Properties.DisplayMember = "FullName";
            this.sluePreparedBy.Properties.NullText = "";
            this.sluePreparedBy.Properties.PopupView = this.gridView2;
            this.sluePreparedBy.Properties.ValueMember = "Id";
            this.sluePreparedBy.Size = new System.Drawing.Size(360, 22);
            this.sluePreparedBy.TabIndex = 123;
            this.sluePreparedBy.EditValueChanged += new System.EventHandler(this.sluePreparedBy_EditValueChanged);
            // 
            // gridView2
            // 
            this.gridView2.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn14,
            this.gridColumn15});
            this.gridView2.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView2.Name = "gridView2";
            this.gridView2.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView2.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn14
            // 
            this.gridColumn14.Caption = "Employee";
            this.gridColumn14.FieldName = "FullName";
            this.gridColumn14.Name = "gridColumn14";
            this.gridColumn14.Visible = true;
            this.gridColumn14.VisibleIndex = 0;
            // 
            // gridColumn15
            // 
            this.gridColumn15.Caption = "Position";
            this.gridColumn15.FieldName = "Position";
            this.gridColumn15.Name = "gridColumn15";
            this.gridColumn15.Visible = true;
            this.gridColumn15.VisibleIndex = 1;
            // 
            // frmAccomplishmentReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(466, 337);
            this.Controls.Add(this.txtApprovedByPos);
            this.Controls.Add(this.txtReviewedByPos);
            this.Controls.Add(this.txtPreparedByPos);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.deDateFrom);
            this.Controls.Add(this.deDateTo);
            this.Controls.Add(this.slueStaff);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnPreview);
            this.Controls.Add(this.slueApprovedBy);
            this.Controls.Add(this.slueReviewedBy);
            this.Controls.Add(this.sluePreparedBy);
            this.Controls.Add(this.lblNotedBy);
            this.Controls.Add(this.lblReviewedBy);
            this.Controls.Add(this.labelControl8);
            this.Name = "frmAccomplishmentReport";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.frmAccomplishmentReport_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtApprovedByPos.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtReviewedByPos.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPreparedByPos.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deDateFrom.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deDateFrom.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deDateTo.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deDateTo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.slueStaff.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.slueApprovedBy.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.slueReviewedBy.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sluePreparedBy.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnPreview;
        private DevExpress.XtraEditors.SearchLookUpEdit slueApprovedBy;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn18;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn19;
        private DevExpress.XtraEditors.SearchLookUpEdit slueReviewedBy;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn16;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn17;
        private DevExpress.XtraEditors.SearchLookUpEdit sluePreparedBy;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn14;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn15;
        private DevExpress.XtraEditors.LabelControl lblNotedBy;
        private DevExpress.XtraEditors.LabelControl lblReviewedBy;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraEditors.LabelControl labelControl20;
        private DevExpress.XtraEditors.LabelControl lblEpisNo;
        private DevExpress.XtraEditors.SearchLookUpEdit slueStaff;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.DateEdit deDateTo;
        private DevExpress.XtraEditors.DateEdit deDateFrom;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit txtPreparedByPos;
        private DevExpress.XtraEditors.TextEdit txtReviewedByPos;
        private DevExpress.XtraEditors.TextEdit txtApprovedByPos;
    }
}