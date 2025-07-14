namespace ACPP
{
    partial class frmFinanceBaseAdd
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmFinanceBaseAdd));
            this.hlpAcmeErp = new System.Windows.Forms.HelpProvider();
            this.SuspendLayout();
            // 
            // hlpAcmeErp
            // 
            resources.ApplyResources(this.hlpAcmeErp, "hlpAcmeErp");
            // 
            // frmFinanceBaseAdd
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.KeyPreview = true;
            this.Name = "frmFinanceBaseAdd";
            this.hlpAcmeErp.SetShowHelp(this, ((bool)(resources.GetObject("$this.ShowHelp"))));
            this.Load += new System.EventHandler(this.frmFinanceBaseAdd_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmFinanceBaseAdd_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.HelpProvider hlpAcmeErp;


    }
}