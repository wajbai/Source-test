namespace SUPPORT
{
    partial class frmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.navBarControl1 = new DevExpress.XtraNavBar.NavBarControl();
            this.nbiBranchData = new DevExpress.XtraNavBar.NavBarGroup();
            this.nbiExportBranchData = new DevExpress.XtraNavBar.NavBarItem();
            this.nbiImportBranchData = new DevExpress.XtraNavBar.NavBarItem();
            this.navBarGroup1 = new DevExpress.XtraNavBar.NavBarGroup();
            this.navBarItem1 = new DevExpress.XtraNavBar.NavBarItem();
            this.navBarItem2 = new DevExpress.XtraNavBar.NavBarItem();
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // navBarControl1
            // 
            this.navBarControl1.ActiveGroup = this.nbiBranchData;
            resources.ApplyResources(this.navBarControl1, "navBarControl1");
            this.navBarControl1.Groups.AddRange(new DevExpress.XtraNavBar.NavBarGroup[] {
            this.nbiBranchData,
            this.navBarGroup1});
            this.navBarControl1.Items.AddRange(new DevExpress.XtraNavBar.NavBarItem[] {
            this.nbiExportBranchData,
            this.nbiImportBranchData,
            this.navBarItem1,
            this.navBarItem2});
            this.navBarControl1.Name = "navBarControl1";
            this.navBarControl1.OptionsNavPane.ExpandedWidth = ((int)(resources.GetObject("resource.ExpandedWidth")));
            // 
            // nbiBranchData
            // 
            this.nbiBranchData.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("nbiBranchData.Appearance.Font")));
            this.nbiBranchData.Appearance.Options.UseFont = true;
            resources.ApplyResources(this.nbiBranchData, "nbiBranchData");
            this.nbiBranchData.Expanded = true;
            this.nbiBranchData.ItemLinks.AddRange(new DevExpress.XtraNavBar.NavBarItemLink[] {
            new DevExpress.XtraNavBar.NavBarItemLink(this.nbiExportBranchData),
            new DevExpress.XtraNavBar.NavBarItemLink(this.nbiImportBranchData)});
            this.nbiBranchData.Name = "nbiBranchData";
            // 
            // nbiExportBranchData
            // 
            resources.ApplyResources(this.nbiExportBranchData, "nbiExportBranchData");
            this.nbiExportBranchData.Name = "nbiExportBranchData";
            this.nbiExportBranchData.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.nbiExportBranchData_LinkClicked);
            // 
            // nbiImportBranchData
            // 
            resources.ApplyResources(this.nbiImportBranchData, "nbiImportBranchData");
            this.nbiImportBranchData.Name = "nbiImportBranchData";
            this.nbiImportBranchData.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.nbiImportBranchData_LinkClicked);
            // 
            // navBarGroup1
            // 
            this.navBarGroup1.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("navBarGroup1.Appearance.Font")));
            this.navBarGroup1.Appearance.Options.UseFont = true;
            resources.ApplyResources(this.navBarGroup1, "navBarGroup1");
            this.navBarGroup1.Expanded = true;
            this.navBarGroup1.ItemLinks.AddRange(new DevExpress.XtraNavBar.NavBarItemLink[] {
            new DevExpress.XtraNavBar.NavBarItemLink(this.navBarItem1),
            new DevExpress.XtraNavBar.NavBarItemLink(this.navBarItem2)});
            this.navBarGroup1.Name = "navBarGroup1";
            // 
            // navBarItem1
            // 
            resources.ApplyResources(this.navBarItem1, "navBarItem1");
            this.navBarItem1.Name = "navBarItem1";
            this.navBarItem1.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.navBarItem1_LinkClicked);
            // 
            // navBarItem2
            // 
            resources.ApplyResources(this.navBarItem2, "navBarItem2");
            this.navBarItem2.Name = "navBarItem2";
            // 
            // frmMain
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.navBarControl1);
            this.IsMdiContainer = true;
            this.Name = "frmMain";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraNavBar.NavBarControl navBarControl1;
        private DevExpress.XtraNavBar.NavBarGroup nbiBranchData;
        private DevExpress.XtraNavBar.NavBarItem nbiExportBranchData;
        private DevExpress.XtraNavBar.NavBarItem nbiImportBranchData;
        private DevExpress.XtraNavBar.NavBarGroup navBarGroup1;
        private DevExpress.XtraNavBar.NavBarItem navBarItem1;
        private DevExpress.XtraNavBar.NavBarItem navBarItem2;

    }
}