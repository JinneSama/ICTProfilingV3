namespace ICTProfilingV3.DashboardForms
{
    partial class UCRepairDashboard
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
            DevExpress.XtraCharts.Series series1 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.PieSeriesLabel pieSeriesLabel1 = new DevExpress.XtraCharts.PieSeriesLabel();
            DevExpress.XtraCharts.PieSeriesView pieSeriesView1 = new DevExpress.XtraCharts.PieSeriesView();
            DevExpress.XtraCharts.Series series2 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.PieSeriesLabel pieSeriesLabel2 = new DevExpress.XtraCharts.PieSeriesLabel();
            DevExpress.XtraCharts.PieSeriesView pieSeriesView2 = new DevExpress.XtraCharts.PieSeriesView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.labelControl16 = new DevExpress.XtraEditors.LabelControl();
            this.btnFilterbyDate = new DevExpress.XtraEditors.SimpleButton();
            this.deTo = new DevExpress.XtraEditors.DateEdit();
            this.label4 = new System.Windows.Forms.Label();
            this.deFrom = new DevExpress.XtraEditors.DateEdit();
            this.label3 = new System.Windows.Forms.Label();
            this.chartRepairbyOffice = new DevExpress.XtraCharts.ChartControl();
            this.barRequest = new DevExpress.XtraEditors.GroupControl();
            this.gcStatus = new DevExpress.XtraGrid.GridControl();
            this.gridStatus = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.chartByStatus = new DevExpress.XtraCharts.ChartControl();
            this.chartCountByBrand = new DevExpress.XtraCharts.ChartControl();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.gcBrand = new DevExpress.XtraGrid.GridControl();
            this.gridBrand = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.deTo.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deTo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deFrom.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deFrom.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartRepairbyOffice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barRequest)).BeginInit();
            this.barRequest.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcStatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridStatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartByStatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(pieSeriesLabel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(pieSeriesView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartCountByBrand)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(pieSeriesLabel2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(pieSeriesView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcBrand)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridBrand)).BeginInit();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.Color.RoyalBlue;
            this.panel2.Controls.Add(this.labelControl16);
            this.panel2.Controls.Add(this.btnFilterbyDate);
            this.panel2.Controls.Add(this.deTo);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.deFrom);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1763, 50);
            this.panel2.TabIndex = 86;
            // 
            // labelControl16
            // 
            this.labelControl16.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelControl16.Appearance.Font = new System.Drawing.Font("Segoe UI", 16F);
            this.labelControl16.Appearance.ForeColor = System.Drawing.Color.White;
            this.labelControl16.Appearance.Options.UseFont = true;
            this.labelControl16.Appearance.Options.UseForeColor = true;
            this.labelControl16.Location = new System.Drawing.Point(1545, 11);
            this.labelControl16.Name = "labelControl16";
            this.labelControl16.Size = new System.Drawing.Size(166, 30);
            this.labelControl16.TabIndex = 118;
            this.labelControl16.Text = "Repair Dashboard";
            // 
            // btnFilterbyDate
            // 
            this.btnFilterbyDate.Appearance.BackColor = System.Drawing.Color.Turquoise;
            this.btnFilterbyDate.Appearance.ForeColor = System.Drawing.Color.Black;
            this.btnFilterbyDate.Appearance.Options.UseBackColor = true;
            this.btnFilterbyDate.Appearance.Options.UseForeColor = true;
            this.btnFilterbyDate.ImageOptions.Image = global::ICTProfilingV3.Properties.Resources.adateoccurring_16x161;
            this.btnFilterbyDate.Location = new System.Drawing.Point(649, 11);
            this.btnFilterbyDate.Margin = new System.Windows.Forms.Padding(2);
            this.btnFilterbyDate.Name = "btnFilterbyDate";
            this.btnFilterbyDate.Size = new System.Drawing.Size(110, 28);
            this.btnFilterbyDate.TabIndex = 117;
            this.btnFilterbyDate.Text = "Filter By Date";
            // 
            // deTo
            // 
            this.deTo.EditValue = null;
            this.deTo.Location = new System.Drawing.Point(418, 14);
            this.deTo.Name = "deTo";
            this.deTo.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deTo.Properties.Appearance.Options.UseFont = true;
            this.deTo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deTo.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deTo.Properties.NullText = "(All)";
            this.deTo.Size = new System.Drawing.Size(226, 22);
            this.deTo.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(390, 17);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(22, 15);
            this.label4.TabIndex = 6;
            this.label4.Text = "To:";
            // 
            // deFrom
            // 
            this.deFrom.EditValue = null;
            this.deFrom.Location = new System.Drawing.Point(66, 14);
            this.deFrom.Name = "deFrom";
            this.deFrom.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deFrom.Properties.Appearance.Options.UseFont = true;
            this.deFrom.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deFrom.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deFrom.Properties.NullText = "(All)";
            this.deFrom.Size = new System.Drawing.Size(250, 22);
            this.deFrom.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(15, 17);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 15);
            this.label3.TabIndex = 4;
            this.label3.Text = "From:";
            // 
            // chartRepairbyOffice
            // 
            this.chartRepairbyOffice.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chartRepairbyOffice.Location = new System.Drawing.Point(5, 24);
            this.chartRepairbyOffice.Name = "chartRepairbyOffice";
            this.chartRepairbyOffice.SeriesSerializable = new DevExpress.XtraCharts.Series[0];
            this.chartRepairbyOffice.Size = new System.Drawing.Size(694, 698);
            this.chartRepairbyOffice.TabIndex = 87;
            // 
            // barRequest
            // 
            this.barRequest.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.barRequest.Appearance.BorderColor = System.Drawing.Color.RoyalBlue;
            this.barRequest.Appearance.Options.UseBorderColor = true;
            this.barRequest.AppearanceCaption.BackColor = System.Drawing.Color.Transparent;
            this.barRequest.AppearanceCaption.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.barRequest.AppearanceCaption.Options.UseBackColor = true;
            this.barRequest.AppearanceCaption.Options.UseFont = true;
            this.barRequest.Controls.Add(this.gcStatus);
            this.barRequest.Controls.Add(this.chartByStatus);
            this.barRequest.Location = new System.Drawing.Point(1252, 56);
            this.barRequest.Name = "barRequest";
            this.barRequest.Size = new System.Drawing.Size(508, 727);
            this.barRequest.TabIndex = 90;
            this.barRequest.Text = "Count of Items by Status";
            // 
            // gcStatus
            // 
            this.gcStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gcStatus.Location = new System.Drawing.Point(5, 430);
            this.gcStatus.MainView = this.gridStatus;
            this.gcStatus.Name = "gcStatus";
            this.gcStatus.Size = new System.Drawing.Size(498, 292);
            this.gcStatus.TabIndex = 44;
            this.gcStatus.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridStatus});
            // 
            // gridStatus
            // 
            this.gridStatus.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn5,
            this.gridColumn6});
            this.gridStatus.GridControl = this.gcStatus;
            this.gridStatus.Name = "gridStatus";
            this.gridStatus.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Repair No";
            this.gridColumn3.FieldName = "RepairNo";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 0;
            this.gridColumn3.Width = 127;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "Date of Repair";
            this.gridColumn4.FieldName = "DateCreated";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 1;
            this.gridColumn4.Width = 113;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "Brand";
            this.gridColumn5.FieldName = "Brand";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 2;
            this.gridColumn5.Width = 125;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "Condemned?";
            this.gridColumn6.FieldName = "Status";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 3;
            this.gridColumn6.Width = 112;
            // 
            // chartByStatus
            // 
            this.chartByStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chartByStatus.BackColor = System.Drawing.Color.Transparent;
            this.chartByStatus.BorderOptions.Visibility = DevExpress.Utils.DefaultBoolean.False;
            this.chartByStatus.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;
            this.chartByStatus.Location = new System.Drawing.Point(6, 24);
            this.chartByStatus.Name = "chartByStatus";
            pieSeriesLabel1.Position = DevExpress.XtraCharts.PieSeriesLabelPosition.Inside;
            pieSeriesLabel1.TextPattern = "{V}";
            series1.Label = pieSeriesLabel1;
            series1.Name = "Status";
            pieSeriesView1.MinAllowedSizePercentage = 75D;
            series1.View = pieSeriesView1;
            this.chartByStatus.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series1};
            this.chartByStatus.Size = new System.Drawing.Size(498, 400);
            this.chartByStatus.TabIndex = 43;
            // 
            // chartCountByBrand
            // 
            this.chartCountByBrand.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chartCountByBrand.BackColor = System.Drawing.Color.Transparent;
            this.chartCountByBrand.BorderOptions.Visibility = DevExpress.Utils.DefaultBoolean.False;
            this.chartCountByBrand.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;
            this.chartCountByBrand.Location = new System.Drawing.Point(5, 24);
            this.chartCountByBrand.Name = "chartCountByBrand";
            pieSeriesLabel2.Position = DevExpress.XtraCharts.PieSeriesLabelPosition.Inside;
            pieSeriesLabel2.TextPattern = "{V}";
            series2.Label = pieSeriesLabel2;
            series2.Name = "Brand";
            pieSeriesView2.MinAllowedSizePercentage = 75D;
            series2.View = pieSeriesView2;
            this.chartCountByBrand.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series2};
            this.chartCountByBrand.Size = new System.Drawing.Size(526, 471);
            this.chartCountByBrand.TabIndex = 42;
            // 
            // groupControl1
            // 
            this.groupControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupControl1.Appearance.BorderColor = System.Drawing.Color.RoyalBlue;
            this.groupControl1.Appearance.Options.UseBorderColor = true;
            this.groupControl1.AppearanceCaption.BackColor = System.Drawing.Color.Transparent;
            this.groupControl1.AppearanceCaption.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupControl1.AppearanceCaption.Options.UseBackColor = true;
            this.groupControl1.AppearanceCaption.Options.UseFont = true;
            this.groupControl1.Controls.Add(this.chartRepairbyOffice);
            this.groupControl1.Location = new System.Drawing.Point(3, 56);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(704, 727);
            this.groupControl1.TabIndex = 91;
            this.groupControl1.Text = "Count of Repair by Office";
            // 
            // groupControl2
            // 
            this.groupControl2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupControl2.Appearance.BorderColor = System.Drawing.Color.RoyalBlue;
            this.groupControl2.Appearance.Options.UseBorderColor = true;
            this.groupControl2.AppearanceCaption.BackColor = System.Drawing.Color.Transparent;
            this.groupControl2.AppearanceCaption.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupControl2.AppearanceCaption.Options.UseBackColor = true;
            this.groupControl2.AppearanceCaption.Options.UseFont = true;
            this.groupControl2.Controls.Add(this.gcBrand);
            this.groupControl2.Controls.Add(this.chartCountByBrand);
            this.groupControl2.Location = new System.Drawing.Point(713, 56);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(533, 727);
            this.groupControl2.TabIndex = 92;
            this.groupControl2.Text = "Count of Items by Brand";
            // 
            // gcBrand
            // 
            this.gcBrand.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gcBrand.Location = new System.Drawing.Point(5, 501);
            this.gcBrand.MainView = this.gridBrand;
            this.gcBrand.Name = "gcBrand";
            this.gcBrand.Size = new System.Drawing.Size(523, 221);
            this.gcBrand.TabIndex = 43;
            this.gcBrand.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridBrand});
            // 
            // gridBrand
            // 
            this.gridBrand.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2});
            this.gridBrand.GridControl = this.gcBrand;
            this.gridBrand.Name = "gridBrand";
            this.gridBrand.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Brand";
            this.gridColumn1.FieldName = "Brand";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            this.gridColumn1.Width = 380;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Total";
            this.gridColumn2.FieldName = "Quantity";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            this.gridColumn2.Width = 122;
            // 
            // UCRepairDashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupControl2);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.barRequest);
            this.Controls.Add(this.panel2);
            this.Name = "UCRepairDashboard";
            this.Size = new System.Drawing.Size(1763, 786);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.deTo.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deTo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deFrom.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deFrom.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartRepairbyOffice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barRequest)).EndInit();
            this.barRequest.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcStatus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridStatus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(pieSeriesLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(pieSeriesView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartByStatus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(pieSeriesLabel2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(pieSeriesView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartCountByBrand)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcBrand)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridBrand)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private DevExpress.XtraEditors.LabelControl labelControl16;
        private DevExpress.XtraEditors.SimpleButton btnFilterbyDate;
        private DevExpress.XtraEditors.DateEdit deTo;
        private System.Windows.Forms.Label label4;
        private DevExpress.XtraEditors.DateEdit deFrom;
        private System.Windows.Forms.Label label3;
        private DevExpress.XtraCharts.ChartControl chartRepairbyOffice;
        private DevExpress.XtraEditors.GroupControl barRequest;
        private DevExpress.XtraCharts.ChartControl chartCountByBrand;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraGrid.GridControl gcBrand;
        private DevExpress.XtraGrid.Views.Grid.GridView gridBrand;
        private DevExpress.XtraCharts.ChartControl chartByStatus;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.GridControl gcStatus;
        private DevExpress.XtraGrid.Views.Grid.GridView gridStatus;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
    }
}
