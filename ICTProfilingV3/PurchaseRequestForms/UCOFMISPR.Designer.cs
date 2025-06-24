namespace ICTProfilingV3.PurchaseRequestForms
{
    partial class UCOFMISPR
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
            this.gcPRDetails = new DevExpress.XtraGrid.GridControl();
            this.gridPRDetails = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn11 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn12 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn13 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemRichTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemRichTextEdit();
            this.gridColumn14 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn15 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn16 = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gcPRDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridPRDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemRichTextEdit1)).BeginInit();
            this.SuspendLayout();
            // 
            // gcPRDetails
            // 
            this.gcPRDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcPRDetails.EmbeddedNavigator.Buttons.Append.Visible = false;
            this.gcPRDetails.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            this.gcPRDetails.EmbeddedNavigator.Buttons.Edit.Visible = false;
            this.gcPRDetails.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            this.gcPRDetails.EmbeddedNavigator.Buttons.Remove.Visible = false;
            this.gcPRDetails.Location = new System.Drawing.Point(0, 0);
            this.gcPRDetails.MainView = this.gridPRDetails;
            this.gcPRDetails.Name = "gcPRDetails";
            this.gcPRDetails.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemRichTextEdit1});
            this.gcPRDetails.Size = new System.Drawing.Size(1030, 424);
            this.gcPRDetails.TabIndex = 117;
            this.gcPRDetails.UseEmbeddedNavigator = true;
            this.gcPRDetails.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridPRDetails});
            // 
            // gridPRDetails
            // 
            this.gridPRDetails.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn10,
            this.gridColumn11,
            this.gridColumn12,
            this.gridColumn13,
            this.gridColumn14,
            this.gridColumn15,
            this.gridColumn16});
            this.gridPRDetails.GridControl = this.gcPRDetails;
            this.gridPRDetails.Name = "gridPRDetails";
            this.gridPRDetails.OptionsView.RowAutoHeight = true;
            this.gridPRDetails.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn10
            // 
            this.gridColumn10.Caption = "Item No";
            this.gridColumn10.FieldName = "ItemNo";
            this.gridColumn10.Name = "gridColumn10";
            this.gridColumn10.OptionsColumn.AllowEdit = false;
            this.gridColumn10.OptionsColumn.AllowFocus = false;
            this.gridColumn10.OptionsColumn.FixedWidth = true;
            this.gridColumn10.Visible = true;
            this.gridColumn10.VisibleIndex = 0;
            this.gridColumn10.Width = 50;
            // 
            // gridColumn11
            // 
            this.gridColumn11.Caption = "Quantity";
            this.gridColumn11.FieldName = "Quantity";
            this.gridColumn11.Name = "gridColumn11";
            this.gridColumn11.OptionsColumn.AllowEdit = false;
            this.gridColumn11.OptionsColumn.AllowFocus = false;
            this.gridColumn11.OptionsColumn.FixedWidth = true;
            this.gridColumn11.Visible = true;
            this.gridColumn11.VisibleIndex = 1;
            this.gridColumn11.Width = 49;
            // 
            // gridColumn12
            // 
            this.gridColumn12.Caption = "UOM";
            this.gridColumn12.FieldName = "UOM";
            this.gridColumn12.Name = "gridColumn12";
            this.gridColumn12.OptionsColumn.AllowEdit = false;
            this.gridColumn12.OptionsColumn.AllowFocus = false;
            this.gridColumn12.OptionsColumn.FixedWidth = true;
            this.gridColumn12.Visible = true;
            this.gridColumn12.VisibleIndex = 2;
            this.gridColumn12.Width = 48;
            // 
            // gridColumn13
            // 
            this.gridColumn13.Caption = "Item";
            this.gridColumn13.ColumnEdit = this.repositoryItemRichTextEdit1;
            this.gridColumn13.FieldName = "Item";
            this.gridColumn13.Name = "gridColumn13";
            this.gridColumn13.OptionsColumn.AllowEdit = false;
            this.gridColumn13.OptionsColumn.AllowFocus = false;
            this.gridColumn13.Visible = true;
            this.gridColumn13.VisibleIndex = 3;
            this.gridColumn13.Width = 423;
            // 
            // repositoryItemRichTextEdit1
            // 
            this.repositoryItemRichTextEdit1.Name = "repositoryItemRichTextEdit1";
            this.repositoryItemRichTextEdit1.ShowCaretInReadOnly = false;
            // 
            // gridColumn14
            // 
            this.gridColumn14.Caption = "Category";
            this.gridColumn14.FieldName = "Category";
            this.gridColumn14.Name = "gridColumn14";
            this.gridColumn14.OptionsColumn.AllowEdit = false;
            this.gridColumn14.OptionsColumn.AllowFocus = false;
            this.gridColumn14.Visible = true;
            this.gridColumn14.VisibleIndex = 4;
            this.gridColumn14.Width = 188;
            // 
            // gridColumn15
            // 
            this.gridColumn15.Caption = "Cost";
            this.gridColumn15.Name = "gridColumn15";
            this.gridColumn15.OptionsColumn.AllowEdit = false;
            this.gridColumn15.OptionsColumn.AllowFocus = false;
            this.gridColumn15.OptionsColumn.FixedWidth = true;
            this.gridColumn15.Visible = true;
            this.gridColumn15.VisibleIndex = 5;
            this.gridColumn15.Width = 84;
            // 
            // gridColumn16
            // 
            this.gridColumn16.Caption = "Total";
            this.gridColumn16.FieldName = "TotalCost";
            this.gridColumn16.Name = "gridColumn16";
            this.gridColumn16.OptionsColumn.AllowEdit = false;
            this.gridColumn16.OptionsColumn.AllowFocus = false;
            this.gridColumn16.OptionsColumn.FixedWidth = true;
            this.gridColumn16.Visible = true;
            this.gridColumn16.VisibleIndex = 6;
            this.gridColumn16.Width = 86;
            // 
            // UCOFMISPR
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gcPRDetails);
            this.Name = "UCOFMISPR";
            this.Size = new System.Drawing.Size(1030, 424);
            this.Load += new System.EventHandler(this.UCOFMISPR_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gcPRDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridPRDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemRichTextEdit1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gcPRDetails;
        private DevExpress.XtraGrid.Views.Grid.GridView gridPRDetails;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn10;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn11;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn12;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn13;
        private DevExpress.XtraEditors.Repository.RepositoryItemRichTextEdit repositoryItemRichTextEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn14;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn15;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn16;
    }
}
