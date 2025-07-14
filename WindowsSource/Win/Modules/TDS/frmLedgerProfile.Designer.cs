namespace ACPP.Modules.TDS
{
    partial class frmLedgerProfile
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
            this.ucLedgerProfile = new ACPP.Modules.UIControls.UcLedgerProfile();
            this.SuspendLayout();
            // 
            // ucLedgerProfile
            // 
            this.ucLedgerProfile.BankAcNo = null;
            this.ucLedgerProfile.BankName = null;
            this.ucLedgerProfile.BankTransId = 0;
            this.ucLedgerProfile.CreditorsProfileId = 0;
            this.ucLedgerProfile.CSTNo = null;
            this.ucLedgerProfile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucLedgerProfile.dtLedgerDetails = null;
            this.ucLedgerProfile.FavouringName = null;
            this.ucLedgerProfile.IFSNo = null;
            this.ucLedgerProfile.isBankDetails = false;
            this.ucLedgerProfile.LedgerId = 0;
            this.ucLedgerProfile.Location = new System.Drawing.Point(5, 5);
            this.ucLedgerProfile.MailAddress = null;
            this.ucLedgerProfile.MailName = null;
            this.ucLedgerProfile.Name = "ucLedgerProfile";
            this.ucLedgerProfile.NatureofPaymentsId = 0;
            this.ucLedgerProfile.NickName = null;
            this.ucLedgerProfile.PANNo = null;
            this.ucLedgerProfile.PinCode = null;
            this.ucLedgerProfile.SalesNo = null;
            this.ucLedgerProfile.Size = new System.Drawing.Size(428, 84);
            this.ucLedgerProfile.State = null;
            this.ucLedgerProfile.TabIndex = 0;
            this.ucLedgerProfile.SaveLedgerProfile += new System.EventHandler(this.ucLedgerProfile_SaveLedgerProfile);
            this.ucLedgerProfile.CloseLedgerProfile += new System.EventHandler(this.ucLedgerProfile_CloseLedgerProfile);
            this.ucLedgerProfile.BankTransEditChanged += new System.EventHandler(this.ucLedgerProfile_BankTransEditChanged);
            this.ucLedgerProfile.BankIndexChanged += new System.EventHandler(this.ucLedgerProfile_BankIndexChanged);
            // 
            // frmLedgerProfile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(438, 94);
            this.Controls.Add(this.ucLedgerProfile);
            this.Name = "frmLedgerProfile";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "TDS Info";
            this.Load += new System.EventHandler(this.frmLedgerProfile_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private UIControls.UcLedgerProfile ucLedgerProfile;
    }
}