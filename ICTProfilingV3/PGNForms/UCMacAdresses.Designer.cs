namespace ICTProfilingV3.PGNForms
{
    partial class UCMacAdresses
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
            this.gcMacAddress = new DevExpress.XtraGrid.GridControl();
            this.gridMacAddress = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.lueDeviceName = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.bsDevice = new System.Windows.Forms.BindingSource(this.components);
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.lueConnection = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.bsConnection = new System.Windows.Forms.BindingSource(this.components);
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gcMacAddress)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridMacAddress)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueDeviceName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsDevice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueConnection)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsConnection)).BeginInit();
            this.SuspendLayout();
            // 
            // gcMacAddress
            // 
            this.gcMacAddress.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcMacAddress.EmbeddedNavigator.Buttons.Append.Visible = false;
            this.gcMacAddress.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            this.gcMacAddress.EmbeddedNavigator.Buttons.Edit.Visible = false;
            this.gcMacAddress.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            this.gcMacAddress.EmbeddedNavigator.Buttons.Remove.Visible = false;
            this.gcMacAddress.Location = new System.Drawing.Point(0, 0);
            this.gcMacAddress.MainView = this.gridMacAddress;
            this.gcMacAddress.Name = "gcMacAddress";
            this.gcMacAddress.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.lueDeviceName,
            this.lueConnection});
            this.gcMacAddress.Size = new System.Drawing.Size(892, 288);
            this.gcMacAddress.TabIndex = 0;
            this.gcMacAddress.UseEmbeddedNavigator = true;
            this.gcMacAddress.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridMacAddress});
            // 
            // gridMacAddress
            // 
            this.gridMacAddress.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3});
            this.gridMacAddress.GridControl = this.gcMacAddress;
            this.gridMacAddress.Name = "gridMacAddress";
            this.gridMacAddress.NewItemRowText = "Click here to add new Mac Address";
            this.gridMacAddress.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Top;
            this.gridMacAddress.OptionsView.ShowGroupPanel = false;
            this.gridMacAddress.RowUpdated += new DevExpress.XtraGrid.Views.Base.RowObjectEventHandler(this.gridMacAddress_RowUpdated);
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Device Name";
            this.gridColumn1.ColumnEdit = this.lueDeviceName;
            this.gridColumn1.FieldName = "Device";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            this.gridColumn1.Width = 255;
            // 
            // lueDeviceName
            // 
            this.lueDeviceName.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lueDeviceName.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Device", "Device")});
            this.lueDeviceName.DataSource = this.bsDevice;
            this.lueDeviceName.DisplayMember = "Device";
            this.lueDeviceName.Name = "lueDeviceName";
            this.lueDeviceName.NullText = "";
            this.lueDeviceName.ValueMember = "Device";
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Connection Type";
            this.gridColumn2.ColumnEdit = this.lueConnection;
            this.gridColumn2.FieldName = "Connection";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            this.gridColumn2.Width = 248;
            // 
            // lueConnection
            // 
            this.lueConnection.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lueConnection.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Connection", "Connection")});
            this.lueConnection.DataSource = this.bsConnection;
            this.lueConnection.DisplayMember = "Connection";
            this.lueConnection.DropDownRows = 2;
            this.lueConnection.Name = "lueConnection";
            this.lueConnection.NullText = "";
            this.lueConnection.ValueMember = "Connection";
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Mac Address";
            this.gridColumn3.FieldName = "MacAddress";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 2;
            this.gridColumn3.Width = 368;
            // 
            // UCMacAdresses
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gcMacAddress);
            this.Name = "UCMacAdresses";
            this.Size = new System.Drawing.Size(892, 288);
            ((System.ComponentModel.ISupportInitialize)(this.gcMacAddress)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridMacAddress)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueDeviceName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsDevice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueConnection)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsConnection)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gcMacAddress;
        private DevExpress.XtraGrid.Views.Grid.GridView gridMacAddress;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit lueDeviceName;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit lueConnection;
        private System.Windows.Forms.BindingSource bsDevice;
        private System.Windows.Forms.BindingSource bsConnection;
    }
}
