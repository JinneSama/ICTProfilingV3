namespace ICTProfilingV3.ToolForms
{
    partial class UCAssignedTo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCAssignedTo));
            this.hccAssignedTo = new DevExpress.XtraEditors.HtmlContentControl();
            ((System.ComponentModel.ISupportInitialize)(this.hccAssignedTo)).BeginInit();
            this.SuspendLayout();
            // 
            // hccAssignedTo
            // 
            this.hccAssignedTo.Cursor = System.Windows.Forms.Cursors.Default;
            this.hccAssignedTo.Dock = System.Windows.Forms.DockStyle.Fill;
            // 
            // 
            // 
            this.hccAssignedTo.HtmlTemplate.Styles = resources.GetString("hccAssignedTo.HtmlTemplate.Styles");
            this.hccAssignedTo.HtmlTemplate.Template = resources.GetString("hccAssignedTo.HtmlTemplate.Template");
            this.hccAssignedTo.Location = new System.Drawing.Point(0, 0);
            this.hccAssignedTo.Name = "hccAssignedTo";
            this.hccAssignedTo.Size = new System.Drawing.Size(379, 40);
            this.hccAssignedTo.TabIndex = 3;
            this.hccAssignedTo.UseDirectXPaint = DevExpress.Utils.DefaultBoolean.True;
            // 
            // UCAssignedTo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.hccAssignedTo);
            this.Name = "UCAssignedTo";
            this.Size = new System.Drawing.Size(379, 40);
            this.Load += new System.EventHandler(this.UCAssignedTo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.hccAssignedTo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.HtmlContentControl hccAssignedTo;
    }
}
