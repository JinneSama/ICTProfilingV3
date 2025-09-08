namespace ICTProfilingV3.EvaluationForms
{
    partial class UCEvaluationSheet
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
            this.gcEvalSheet = new DevExpress.XtraGrid.GridControl();
            this.gridEvalSheet = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repItemRating = new DevExpress.XtraEditors.Repository.RepositoryItemRatingControl();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gcEvalSheet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridEvalSheet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repItemRating)).BeginInit();
            this.SuspendLayout();
            // 
            // gcEvalSheet
            // 
            this.gcEvalSheet.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gcEvalSheet.Location = new System.Drawing.Point(0, 0);
            this.gcEvalSheet.MainView = this.gridEvalSheet;
            this.gcEvalSheet.Name = "gcEvalSheet";
            this.gcEvalSheet.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repItemRating});
            this.gcEvalSheet.Size = new System.Drawing.Size(1150, 276);
            this.gcEvalSheet.TabIndex = 0;
            this.gcEvalSheet.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridEvalSheet});
            // 
            // gridEvalSheet
            // 
            this.gridEvalSheet.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3});
            this.gridEvalSheet.GridControl = this.gcEvalSheet;
            this.gridEvalSheet.Name = "gridEvalSheet";
            this.gridEvalSheet.OptionsView.ShowGroupPanel = false;
            this.gridEvalSheet.RowUpdated += new DevExpress.XtraGrid.Views.Base.RowObjectEventHandler(this.gridEvalSheet_RowUpdated);
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "PITD SERVICES";
            this.gridColumn1.FieldName = "Service";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.OptionsColumn.AllowFocus = false;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            this.gridColumn1.Width = 679;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "RATINGS";
            this.gridColumn2.ColumnEdit = this.repItemRating;
            this.gridColumn2.FieldName = "RatingValue";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.FixedWidth = true;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            this.gridColumn2.Width = 137;
            // 
            // repItemRating
            // 
            this.repItemRating.AutoHeight = false;
            this.repItemRating.ItemIndent = 3;
            this.repItemRating.Name = "repItemRating";
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "REMARKS";
            this.gridColumn3.FieldName = "Remarks";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 2;
            this.gridColumn3.Width = 313;
            // 
            // UCEvaluationSheet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gcEvalSheet);
            this.Name = "UCEvaluationSheet";
            this.Size = new System.Drawing.Size(1150, 276);
            ((System.ComponentModel.ISupportInitialize)(this.gcEvalSheet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridEvalSheet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repItemRating)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gcEvalSheet;
        private DevExpress.XtraGrid.Views.Grid.GridView gridEvalSheet;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraEditors.Repository.RepositoryItemRatingControl repItemRating;
    }
}
