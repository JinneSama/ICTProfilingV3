namespace ICTProfilingV3.CustomerActionSheetForms
{
    partial class frmAddEditCAS
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAddEditCAS));
            this.panel1 = new System.Windows.Forms.Panel();
            this.labelControl16 = new DevExpress.XtraEditors.LabelControl();
            this.lblEpisNo = new DevExpress.XtraEditors.LabelControl();
            this.gcDetails = new DevExpress.XtraEditors.GroupControl();
            this.txtClientName = new DevExpress.XtraEditors.TextEdit();
            this.slueAssistedBy = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn14 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rdbtnGender = new DevExpress.XtraEditors.RadioGroup();
            this.labelControl10 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.txtActionTaken = new DevExpress.XtraEditors.MemoEdit();
            this.txtClientRequest = new DevExpress.XtraEditors.MemoEdit();
            this.slueEmployee = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.gridView6 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn15 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn16 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn17 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn18 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn19 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.txtOffice = new DevExpress.XtraEditors.TextEdit();
            this.txtDateCreated = new DevExpress.XtraEditors.DateEdit();
            this.labelControl20 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl21 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl22 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl23 = new DevExpress.XtraEditors.LabelControl();
            this.txtContactNo = new DevExpress.XtraEditors.TextEdit();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcDetails)).BeginInit();
            this.gcDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtClientName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.slueAssistedBy.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdbtnGender.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtActionTaken.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtClientRequest.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.slueEmployee.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOffice.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDateCreated.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDateCreated.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtContactNo.Properties)).BeginInit();
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
            this.panel1.Size = new System.Drawing.Size(426, 37);
            this.panel1.TabIndex = 86;
            // 
            // labelControl16
            // 
            this.labelControl16.Appearance.Font = new System.Drawing.Font("Segoe UI", 16F);
            this.labelControl16.Appearance.ForeColor = System.Drawing.Color.White;
            this.labelControl16.Appearance.Options.UseFont = true;
            this.labelControl16.Appearance.Options.UseForeColor = true;
            this.labelControl16.Location = new System.Drawing.Point(24, 4);
            this.labelControl16.Name = "labelControl16";
            this.labelControl16.Size = new System.Drawing.Size(125, 30);
            this.labelControl16.TabIndex = 2;
            this.labelControl16.Text = "Add/Edit CAS";
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
            // gcDetails
            // 
            this.gcDetails.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.gcDetails.Appearance.Options.UseFont = true;
            this.gcDetails.AppearanceCaption.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.gcDetails.AppearanceCaption.Options.UseFont = true;
            this.gcDetails.Controls.Add(this.txtClientName);
            this.gcDetails.Controls.Add(this.slueAssistedBy);
            this.gcDetails.Controls.Add(this.rdbtnGender);
            this.gcDetails.Controls.Add(this.labelControl10);
            this.gcDetails.Controls.Add(this.labelControl8);
            this.gcDetails.Controls.Add(this.labelControl7);
            this.gcDetails.Controls.Add(this.labelControl6);
            this.gcDetails.Controls.Add(this.txtActionTaken);
            this.gcDetails.Controls.Add(this.txtClientRequest);
            this.gcDetails.Controls.Add(this.slueEmployee);
            this.gcDetails.Controls.Add(this.txtOffice);
            this.gcDetails.Controls.Add(this.txtDateCreated);
            this.gcDetails.Controls.Add(this.labelControl20);
            this.gcDetails.Controls.Add(this.labelControl21);
            this.gcDetails.Controls.Add(this.labelControl22);
            this.gcDetails.Controls.Add(this.labelControl23);
            this.gcDetails.Controls.Add(this.txtContactNo);
            this.gcDetails.Location = new System.Drawing.Point(12, 43);
            this.gcDetails.Name = "gcDetails";
            this.gcDetails.Size = new System.Drawing.Size(401, 368);
            this.gcDetails.TabIndex = 87;
            this.gcDetails.Text = "Repair Request Details";
            // 
            // txtClientName
            // 
            this.txtClientName.Location = new System.Drawing.Point(104, 52);
            this.txtClientName.Name = "txtClientName";
            this.txtClientName.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtClientName.Properties.Appearance.Options.UseFont = true;
            this.txtClientName.Properties.UseReadOnlyAppearance = false;
            this.txtClientName.Size = new System.Drawing.Size(269, 22);
            this.txtClientName.TabIndex = 134;
            // 
            // slueAssistedBy
            // 
            this.slueAssistedBy.Location = new System.Drawing.Point(104, 329);
            this.slueAssistedBy.Name = "slueAssistedBy";
            this.slueAssistedBy.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.slueAssistedBy.Properties.Appearance.Options.UseFont = true;
            this.slueAssistedBy.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.slueAssistedBy.Properties.DisplayMember = "FullName";
            this.slueAssistedBy.Properties.NullText = "";
            this.slueAssistedBy.Properties.PopupView = this.gridView2;
            this.slueAssistedBy.Properties.ValueMember = "Id";
            this.slueAssistedBy.Size = new System.Drawing.Size(286, 22);
            this.slueAssistedBy.TabIndex = 133;
            // 
            // gridView2
            // 
            this.gridView2.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn14,
            this.gridColumn1});
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
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Position";
            this.gridColumn1.FieldName = "Position";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 1;
            // 
            // rdbtnGender
            // 
            this.rdbtnGender.Location = new System.Drawing.Point(105, 138);
            this.rdbtnGender.Margin = new System.Windows.Forms.Padding(2);
            this.rdbtnGender.Name = "rdbtnGender";
            this.rdbtnGender.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.rdbtnGender.Properties.Appearance.Options.UseBackColor = true;
            this.rdbtnGender.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.rdbtnGender.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem("Male", "Male", true, null, "rdbtnMale"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("Female", "Female", true, null, "rdbtnFemale")});
            this.rdbtnGender.Size = new System.Drawing.Size(153, 18);
            this.rdbtnGender.TabIndex = 126;
            // 
            // labelControl10
            // 
            this.labelControl10.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.labelControl10.Appearance.Options.UseFont = true;
            this.labelControl10.Location = new System.Drawing.Point(6, 332);
            this.labelControl10.Name = "labelControl10";
            this.labelControl10.Size = new System.Drawing.Size(59, 15);
            this.labelControl10.TabIndex = 132;
            this.labelControl10.Text = "Assisted By";
            // 
            // labelControl8
            // 
            this.labelControl8.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.labelControl8.Appearance.Options.UseFont = true;
            this.labelControl8.Location = new System.Drawing.Point(6, 250);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(69, 15);
            this.labelControl8.TabIndex = 131;
            this.labelControl8.Text = "Action Taken";
            // 
            // labelControl7
            // 
            this.labelControl7.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.labelControl7.Appearance.Options.UseFont = true;
            this.labelControl7.Location = new System.Drawing.Point(7, 167);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(76, 15);
            this.labelControl7.TabIndex = 130;
            this.labelControl7.Text = "Client Request";
            // 
            // labelControl6
            // 
            this.labelControl6.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.labelControl6.Appearance.Options.UseFont = true;
            this.labelControl6.Location = new System.Drawing.Point(7, 138);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(38, 15);
            this.labelControl6.TabIndex = 129;
            this.labelControl6.Text = "Gender";
            // 
            // txtActionTaken
            // 
            this.txtActionTaken.Location = new System.Drawing.Point(104, 249);
            this.txtActionTaken.Margin = new System.Windows.Forms.Padding(2);
            this.txtActionTaken.Name = "txtActionTaken";
            this.txtActionTaken.Size = new System.Drawing.Size(286, 75);
            this.txtActionTaken.TabIndex = 128;
            // 
            // txtClientRequest
            // 
            this.txtClientRequest.Location = new System.Drawing.Point(104, 166);
            this.txtClientRequest.Margin = new System.Windows.Forms.Padding(2);
            this.txtClientRequest.Name = "txtClientRequest";
            this.txtClientRequest.Size = new System.Drawing.Size(286, 77);
            this.txtClientRequest.TabIndex = 127;
            // 
            // slueEmployee
            // 
            this.slueEmployee.EditValue = "";
            this.slueEmployee.Location = new System.Drawing.Point(372, 52);
            this.slueEmployee.Name = "slueEmployee";
            this.slueEmployee.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.slueEmployee.Properties.Appearance.Options.UseFont = true;
            this.slueEmployee.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.slueEmployee.Properties.DisplayMember = "Employee";
            this.slueEmployee.Properties.NullText = "";
            this.slueEmployee.Properties.PopupFormSize = new System.Drawing.Size(975, 0);
            this.slueEmployee.Properties.PopupView = this.gridView6;
            this.slueEmployee.Properties.UseReadOnlyAppearance = false;
            this.slueEmployee.Properties.ValueMember = "Id";
            this.slueEmployee.Size = new System.Drawing.Size(19, 22);
            this.slueEmployee.TabIndex = 125;
            this.slueEmployee.EditValueChanged += new System.EventHandler(this.slueEmployee_EditValueChanged);
            // 
            // gridView6
            // 
            this.gridView6.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn15,
            this.gridColumn16,
            this.gridColumn17,
            this.gridColumn18,
            this.gridColumn19});
            this.gridView6.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView6.Name = "gridView6";
            this.gridView6.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView6.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn15
            // 
            this.gridColumn15.Caption = "Id";
            this.gridColumn15.FieldName = "EmpId";
            this.gridColumn15.MinWidth = 15;
            this.gridColumn15.Name = "gridColumn15";
            this.gridColumn15.Width = 56;
            // 
            // gridColumn16
            // 
            this.gridColumn16.Caption = "Employee";
            this.gridColumn16.FieldName = "Employee";
            this.gridColumn16.MinWidth = 225;
            this.gridColumn16.Name = "gridColumn16";
            this.gridColumn16.Visible = true;
            this.gridColumn16.VisibleIndex = 0;
            this.gridColumn16.Width = 225;
            // 
            // gridColumn17
            // 
            this.gridColumn17.Caption = "Position";
            this.gridColumn17.FieldName = "Position";
            this.gridColumn17.MinWidth = 202;
            this.gridColumn17.Name = "gridColumn17";
            this.gridColumn17.Visible = true;
            this.gridColumn17.VisibleIndex = 1;
            this.gridColumn17.Width = 202;
            // 
            // gridColumn18
            // 
            this.gridColumn18.Caption = "Office";
            this.gridColumn18.FieldName = "Office";
            this.gridColumn18.MinWidth = 67;
            this.gridColumn18.Name = "gridColumn18";
            this.gridColumn18.Visible = true;
            this.gridColumn18.VisibleIndex = 2;
            this.gridColumn18.Width = 67;
            // 
            // gridColumn19
            // 
            this.gridColumn19.Caption = "Division";
            this.gridColumn19.FieldName = "Division";
            this.gridColumn19.MinWidth = 382;
            this.gridColumn19.Name = "gridColumn19";
            this.gridColumn19.Visible = true;
            this.gridColumn19.VisibleIndex = 3;
            this.gridColumn19.Width = 382;
            // 
            // txtOffice
            // 
            this.txtOffice.Location = new System.Drawing.Point(105, 80);
            this.txtOffice.Name = "txtOffice";
            this.txtOffice.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtOffice.Properties.Appearance.Options.UseFont = true;
            this.txtOffice.Properties.UseReadOnlyAppearance = false;
            this.txtOffice.Size = new System.Drawing.Size(285, 22);
            this.txtOffice.TabIndex = 124;
            // 
            // txtDateCreated
            // 
            this.txtDateCreated.EditValue = null;
            this.txtDateCreated.Location = new System.Drawing.Point(105, 24);
            this.txtDateCreated.Name = "txtDateCreated";
            this.txtDateCreated.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtDateCreated.Properties.Appearance.Options.UseFont = true;
            this.txtDateCreated.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtDateCreated.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtDateCreated.Properties.MaskSettings.Set("mask", "G");
            this.txtDateCreated.Properties.UseMaskAsDisplayFormat = true;
            this.txtDateCreated.Size = new System.Drawing.Size(286, 22);
            this.txtDateCreated.TabIndex = 1;
            // 
            // labelControl20
            // 
            this.labelControl20.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.labelControl20.Appearance.Options.UseFont = true;
            this.labelControl20.Location = new System.Drawing.Point(7, 55);
            this.labelControl20.Name = "labelControl20";
            this.labelControl20.Size = new System.Drawing.Size(32, 15);
            this.labelControl20.TabIndex = 63;
            this.labelControl20.Text = "Name";
            // 
            // labelControl21
            // 
            this.labelControl21.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.labelControl21.Appearance.Options.UseFont = true;
            this.labelControl21.Location = new System.Drawing.Point(7, 111);
            this.labelControl21.Name = "labelControl21";
            this.labelControl21.Size = new System.Drawing.Size(64, 15);
            this.labelControl21.TabIndex = 56;
            this.labelControl21.Text = "Contact No.";
            // 
            // labelControl22
            // 
            this.labelControl22.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.labelControl22.Appearance.Options.UseFont = true;
            this.labelControl22.Location = new System.Drawing.Point(7, 83);
            this.labelControl22.Name = "labelControl22";
            this.labelControl22.Size = new System.Drawing.Size(85, 15);
            this.labelControl22.TabIndex = 57;
            this.labelControl22.Text = "Office / Address";
            // 
            // labelControl23
            // 
            this.labelControl23.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.labelControl23.Appearance.Options.UseFont = true;
            this.labelControl23.Location = new System.Drawing.Point(7, 27);
            this.labelControl23.Name = "labelControl23";
            this.labelControl23.Size = new System.Drawing.Size(24, 15);
            this.labelControl23.TabIndex = 58;
            this.labelControl23.Text = "Date";
            // 
            // txtContactNo
            // 
            this.txtContactNo.Location = new System.Drawing.Point(105, 108);
            this.txtContactNo.Name = "txtContactNo";
            this.txtContactNo.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtContactNo.Properties.Appearance.Options.UseFont = true;
            this.txtContactNo.Properties.UseReadOnlyAppearance = false;
            this.txtContactNo.Size = new System.Drawing.Size(286, 22);
            this.txtContactNo.TabIndex = 4;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Appearance.BackColor = System.Drawing.Color.RoyalBlue;
            this.btnCancel.Appearance.ForeColor = System.Drawing.Color.Black;
            this.btnCancel.Appearance.Options.UseBackColor = true;
            this.btnCancel.Appearance.Options.UseForeColor = true;
            this.btnCancel.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.ImageOptions.Image")));
            this.btnCancel.Location = new System.Drawing.Point(315, 416);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 28);
            this.btnCancel.TabIndex = 91;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Appearance.BackColor = System.Drawing.Color.RoyalBlue;
            this.btnSave.Appearance.ForeColor = System.Drawing.Color.Black;
            this.btnSave.Appearance.Options.UseBackColor = true;
            this.btnSave.Appearance.Options.UseForeColor = true;
            this.btnSave.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.ImageOptions.Image")));
            this.btnSave.Location = new System.Drawing.Point(213, 416);
            this.btnSave.Margin = new System.Windows.Forms.Padding(2);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(98, 28);
            this.btnSave.TabIndex = 90;
            this.btnSave.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // frmAddEditCAS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(426, 458);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.gcDetails);
            this.Controls.Add(this.panel1);
            this.IconOptions.Icon = ((System.Drawing.Icon)(resources.GetObject("frmAddEditCAS.IconOptions.Icon")));
            this.Name = "frmAddEditCAS";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcDetails)).EndInit();
            this.gcDetails.ResumeLayout(false);
            this.gcDetails.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtClientName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.slueAssistedBy.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdbtnGender.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtActionTaken.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtClientRequest.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.slueEmployee.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOffice.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDateCreated.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDateCreated.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtContactNo.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraEditors.LabelControl labelControl16;
        private DevExpress.XtraEditors.LabelControl lblEpisNo;
        private DevExpress.XtraEditors.GroupControl gcDetails;
        private DevExpress.XtraEditors.TextEdit txtOffice;
        private DevExpress.XtraEditors.DateEdit txtDateCreated;
        private DevExpress.XtraEditors.LabelControl labelControl20;
        private DevExpress.XtraEditors.LabelControl labelControl21;
        private DevExpress.XtraEditors.LabelControl labelControl22;
        private DevExpress.XtraEditors.LabelControl labelControl23;
        private DevExpress.XtraEditors.TextEdit txtContactNo;
        private DevExpress.XtraEditors.RadioGroup rdbtnGender;
        private DevExpress.XtraEditors.LabelControl labelControl10;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.MemoEdit txtActionTaken;
        private DevExpress.XtraEditors.MemoEdit txtClientRequest;
        private DevExpress.XtraEditors.SearchLookUpEdit slueEmployee;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn15;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn16;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn17;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn18;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn19;
        private DevExpress.XtraEditors.SearchLookUpEdit slueAssistedBy;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn14;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.TextEdit txtClientName;
    }
}