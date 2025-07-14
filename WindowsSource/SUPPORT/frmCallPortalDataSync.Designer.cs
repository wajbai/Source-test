namespace SUPPORT
{
    partial class frmCallPortalDataSync
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
            this.btnCallPortalDataSync = new DevExpress.XtraEditors.SimpleButton();
            this.SuspendLayout();
            // 
            // btnCallPortalDataSync
            // 
            this.btnCallPortalDataSync.Location = new System.Drawing.Point(12, 12);
            this.btnCallPortalDataSync.Name = "btnCallPortalDataSync";
            this.btnCallPortalDataSync.Size = new System.Drawing.Size(147, 23);
            this.btnCallPortalDataSync.TabIndex = 0;
            this.btnCallPortalDataSync.Text = "Call Portal DataSync";
            this.btnCallPortalDataSync.Click += new System.EventHandler(this.btnCallPortalDataSync_Click);
            // 
            // frmCallPortalDataSync
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.btnCallPortalDataSync);
            this.Name = "frmCallPortalDataSync";
            this.Text = "Call Portal Data Sync";
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnCallPortalDataSync;
    }
}