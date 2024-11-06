namespace ICTProfilingV3.ActionsForms
{
    partial class frmActionList
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.labelControl16 = new DevExpress.XtraEditors.LabelControl();
            this.gcActionTaken = new DevExpress.XtraGrid.GridControl();
            this.gridActionTaken = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcActionTaken)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridActionTaken)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.panel1.Controls.Add(this.labelControl16);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(441, 41);
            this.panel1.TabIndex = 108;
            // 
            // labelControl16
            // 
            this.labelControl16.Appearance.Font = new System.Drawing.Font("Segoe UI", 16F);
            this.labelControl16.Appearance.ForeColor = System.Drawing.Color.White;
            this.labelControl16.Appearance.Options.UseFont = true;
            this.labelControl16.Appearance.Options.UseForeColor = true;
            this.labelControl16.Location = new System.Drawing.Point(24, 5);
            this.labelControl16.Name = "labelControl16";
            this.labelControl16.Size = new System.Drawing.Size(156, 30);
            this.labelControl16.TabIndex = 1;
            this.labelControl16.Text = "Add/Edit Actions";
            // 
            // gcActionTaken
            // 
            this.gcActionTaken.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcActionTaken.EmbeddedNavigator.Buttons.Append.Visible = false;
            this.gcActionTaken.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            this.gcActionTaken.EmbeddedNavigator.Buttons.Edit.Visible = false;
            this.gcActionTaken.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            this.gcActionTaken.EmbeddedNavigator.Buttons.Remove.Visible = false;
            this.gcActionTaken.Location = new System.Drawing.Point(0, 41);
            this.gcActionTaken.MainView = this.gridActionTaken;
            this.gcActionTaken.Name = "gcActionTaken";
            this.gcActionTaken.Size = new System.Drawing.Size(441, 471);
            this.gcActionTaken.TabIndex = 109;
            this.gcActionTaken.UseEmbeddedNavigator = true;
            this.gcActionTaken.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridActionTaken});
            // 
            // gridActionTaken
            // 
            this.gridActionTaken.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1});
            this.gridActionTaken.GridControl = this.gcActionTaken;
            this.gridActionTaken.Name = "gridActionTaken";
            this.gridActionTaken.NewItemRowText = "Click Here to Add new Action";
            this.gridActionTaken.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Top;
            this.gridActionTaken.OptionsView.ShowGroupPanel = false;
            this.gridActionTaken.RowUpdated += new DevExpress.XtraGrid.Views.Base.RowObjectEventHandler(this.gridActionTaken_RowUpdated);
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Action Taken";
            this.gridColumn1.FieldName = "Action";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            // 
            // frmActionList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(441, 512);
            this.Controls.Add(this.gcActionTaken);
            this.Controls.Add(this.panel1);
            this.Name = "frmActionList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcActionTaken)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridActionTaken)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraEditors.LabelControl labelControl16;
        private DevExpress.XtraGrid.GridControl gcActionTaken;
        private DevExpress.XtraGrid.Views.Grid.GridView gridActionTaken;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
    }
}