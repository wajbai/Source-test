namespace ACPP.Modules.UIControls
{
    partial class UcBankAccountDetails
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UcBankAccountDetails));
            this.lcBankDetails = new DevExpress.XtraLayout.LayoutControl();
            this.lcgBankDetails = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lcgAccountDetails = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lblBankName = new DevExpress.XtraLayout.SimpleLabelItem();
            this.lblTextBankName = new DevExpress.XtraLayout.SimpleLabelItem();
            this.lblBranchName = new DevExpress.XtraLayout.SimpleLabelItem();
            this.lblTextBranchName = new DevExpress.XtraLayout.SimpleLabelItem();
            this.lblAccountNumber = new DevExpress.XtraLayout.SimpleLabelItem();
            this.lblTextAccountNumber = new DevExpress.XtraLayout.SimpleLabelItem();
            this.lblCreatedOn = new DevExpress.XtraLayout.SimpleLabelItem();
            this.lblTextCreatedOn = new DevExpress.XtraLayout.SimpleLabelItem();
            this.lblProject = new DevExpress.XtraLayout.SimpleLabelItem();
            this.lblProjectName = new DevExpress.XtraLayout.SimpleLabelItem();
            ((System.ComponentModel.ISupportInitialize)(this.lcBankDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgBankDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgAccountDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblBankName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblTextBankName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblBranchName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblTextBranchName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblAccountNumber)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblTextAccountNumber)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblCreatedOn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblTextCreatedOn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblProject)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblProjectName)).BeginInit();
            this.SuspendLayout();
            // 
            // lcBankDetails
            // 
            this.lcBankDetails.AllowCustomizationMenu = false;
            resources.ApplyResources(this.lcBankDetails, "lcBankDetails");
            this.lcBankDetails.Name = "lcBankDetails";
            this.lcBankDetails.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(660, 229, 250, 350);
            this.lcBankDetails.Root = this.lcgBankDetails;
            // 
            // lcgBankDetails
            // 
            resources.ApplyResources(this.lcgBankDetails, "lcgBankDetails");
            this.lcgBankDetails.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.lcgBankDetails.GroupBordersVisible = false;
            this.lcgBankDetails.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lcgAccountDetails});
            this.lcgBankDetails.Location = new System.Drawing.Point(0, 0);
            this.lcgBankDetails.Name = "Root";
            this.lcgBankDetails.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.lcgBankDetails.Size = new System.Drawing.Size(431, 96);
            this.lcgBankDetails.TextVisible = false;
            // 
            // lcgAccountDetails
            // 
            this.lcgAccountDetails.AppearanceGroup.Font = ((System.Drawing.Font)(resources.GetObject("lcgAccountDetails.AppearanceGroup.Font")));
            this.lcgAccountDetails.AppearanceGroup.Options.UseFont = true;
            resources.ApplyResources(this.lcgAccountDetails, "lcgAccountDetails");
            this.lcgAccountDetails.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lblBankName,
            this.lblTextBankName,
            this.lblBranchName,
            this.lblTextBranchName,
            this.lblAccountNumber,
            this.lblTextAccountNumber,
            this.lblCreatedOn,
            this.lblTextCreatedOn,
            this.lblProject,
            this.lblProjectName});
            this.lcgAccountDetails.Location = new System.Drawing.Point(0, 0);
            this.lcgAccountDetails.Name = "lcgAccountDetails";
            this.lcgAccountDetails.Padding = new DevExpress.XtraLayout.Utils.Padding(3, 3, 3, 3);
            this.lcgAccountDetails.Size = new System.Drawing.Size(431, 96);
            this.lcgAccountDetails.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            // 
            // lblBankName
            // 
            this.lblBankName.AllowHotTrack = false;
            this.lblBankName.AppearanceItemCaption.Font = ((System.Drawing.Font)(resources.GetObject("lblBankName.AppearanceItemCaption.Font")));
            this.lblBankName.AppearanceItemCaption.Options.UseFont = true;
            resources.ApplyResources(this.lblBankName, "lblBankName");
            this.lblBankName.Location = new System.Drawing.Point(0, 17);
            this.lblBankName.Name = "lblBankName";
            this.lblBankName.Size = new System.Drawing.Size(96, 17);
            this.lblBankName.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.lblBankName.TextSize = new System.Drawing.Size(63, 13);
            // 
            // lblTextBankName
            // 
            this.lblTextBankName.AllowHotTrack = false;
            resources.ApplyResources(this.lblTextBankName, "lblTextBankName");
            this.lblTextBankName.Location = new System.Drawing.Point(96, 17);
            this.lblTextBankName.Name = "lblTextBankName";
            this.lblTextBankName.Padding = new DevExpress.XtraLayout.Utils.Padding(3, 0, 0, 3);
            this.lblTextBankName.Size = new System.Drawing.Size(327, 17);
            this.lblTextBankName.TextSize = new System.Drawing.Size(50, 13);
            // 
            // lblBranchName
            // 
            this.lblBranchName.AllowHotTrack = false;
            this.lblBranchName.AppearanceItemCaption.Font = ((System.Drawing.Font)(resources.GetObject("lblBranchName.AppearanceItemCaption.Font")));
            this.lblBranchName.AppearanceItemCaption.Options.UseFont = true;
            resources.ApplyResources(this.lblBranchName, "lblBranchName");
            this.lblBranchName.Location = new System.Drawing.Point(0, 34);
            this.lblBranchName.Name = "lblBranchName";
            this.lblBranchName.Size = new System.Drawing.Size(96, 17);
            this.lblBranchName.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.lblBranchName.TextSize = new System.Drawing.Size(39, 13);
            // 
            // lblTextBranchName
            // 
            this.lblTextBranchName.AllowHotTrack = false;
            resources.ApplyResources(this.lblTextBranchName, "lblTextBranchName");
            this.lblTextBranchName.Location = new System.Drawing.Point(96, 34);
            this.lblTextBranchName.Name = "lblTextBranchName";
            this.lblTextBranchName.Padding = new DevExpress.XtraLayout.Utils.Padding(3, 0, 0, 3);
            this.lblTextBranchName.Size = new System.Drawing.Size(114, 17);
            this.lblTextBranchName.TextSize = new System.Drawing.Size(50, 13);
            // 
            // lblAccountNumber
            // 
            this.lblAccountNumber.AllowHotTrack = false;
            this.lblAccountNumber.AppearanceItemCaption.Font = ((System.Drawing.Font)(resources.GetObject("lblAccountNumber.AppearanceItemCaption.Font")));
            this.lblAccountNumber.AppearanceItemCaption.Options.UseFont = true;
            resources.ApplyResources(this.lblAccountNumber, "lblAccountNumber");
            this.lblAccountNumber.Location = new System.Drawing.Point(210, 34);
            this.lblAccountNumber.Name = "lblAccountNumber";
            this.lblAccountNumber.Size = new System.Drawing.Size(97, 17);
            this.lblAccountNumber.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.lblAccountNumber.TextSize = new System.Drawing.Size(93, 13);
            // 
            // lblTextAccountNumber
            // 
            this.lblTextAccountNumber.AllowHotTrack = false;
            resources.ApplyResources(this.lblTextAccountNumber, "lblTextAccountNumber");
            this.lblTextAccountNumber.Location = new System.Drawing.Point(307, 34);
            this.lblTextAccountNumber.Name = "lblTextAccountNumber";
            this.lblTextAccountNumber.Size = new System.Drawing.Size(116, 17);
            this.lblTextAccountNumber.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.lblTextAccountNumber.TextSize = new System.Drawing.Size(50, 13);
            // 
            // lblCreatedOn
            // 
            this.lblCreatedOn.AllowHotTrack = false;
            this.lblCreatedOn.AppearanceItemCaption.Font = ((System.Drawing.Font)(resources.GetObject("lblCreatedOn.AppearanceItemCaption.Font")));
            this.lblCreatedOn.AppearanceItemCaption.Options.UseFont = true;
            resources.ApplyResources(this.lblCreatedOn, "lblCreatedOn");
            this.lblCreatedOn.Location = new System.Drawing.Point(0, 51);
            this.lblCreatedOn.Name = "lblCreatedOn";
            this.lblCreatedOn.Size = new System.Drawing.Size(96, 17);
            this.lblCreatedOn.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.lblCreatedOn.TextSize = new System.Drawing.Size(63, 13);
            // 
            // lblTextCreatedOn
            // 
            this.lblTextCreatedOn.AllowHotTrack = false;
            resources.ApplyResources(this.lblTextCreatedOn, "lblTextCreatedOn");
            this.lblTextCreatedOn.Location = new System.Drawing.Point(96, 51);
            this.lblTextCreatedOn.Name = "lblTextCreatedOn";
            this.lblTextCreatedOn.Padding = new DevExpress.XtraLayout.Utils.Padding(3, 0, 0, 3);
            this.lblTextCreatedOn.Size = new System.Drawing.Size(327, 17);
            this.lblTextCreatedOn.TextSize = new System.Drawing.Size(50, 13);
            // 
            // lblProject
            // 
            this.lblProject.AllowHotTrack = false;
            this.lblProject.AppearanceItemCaption.Font = ((System.Drawing.Font)(resources.GetObject("lblProject.AppearanceItemCaption.Font")));
            this.lblProject.AppearanceItemCaption.Options.UseFont = true;
            resources.ApplyResources(this.lblProject, "lblProject");
            this.lblProject.Location = new System.Drawing.Point(0, 0);
            this.lblProject.Name = "lblProject";
            this.lblProject.Size = new System.Drawing.Size(96, 17);
            this.lblProject.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.lblProject.TextSize = new System.Drawing.Size(41, 13);
            // 
            // lblProjectName
            // 
            this.lblProjectName.AllowHotTrack = false;
            resources.ApplyResources(this.lblProjectName, "lblProjectName");
            this.lblProjectName.Location = new System.Drawing.Point(96, 0);
            this.lblProjectName.Name = "lblProjectName";
            this.lblProjectName.Size = new System.Drawing.Size(327, 17);
            this.lblProjectName.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.lblProjectName.TextSize = new System.Drawing.Size(50, 13);
            // 
            // UcBankAccountDetails
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lcBankDetails);
            this.Name = "UcBankAccountDetails";
            ((System.ComponentModel.ISupportInitialize)(this.lcBankDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgBankDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgAccountDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblBankName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblTextBankName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblBranchName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblTextBranchName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblAccountNumber)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblTextAccountNumber)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblCreatedOn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblTextCreatedOn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblProject)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblProjectName)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl lcBankDetails;
        private DevExpress.XtraLayout.LayoutControlGroup lcgBankDetails;
        private DevExpress.XtraLayout.LayoutControlGroup lcgAccountDetails;
        private DevExpress.XtraLayout.SimpleLabelItem lblBankName;
        private DevExpress.XtraLayout.SimpleLabelItem lblTextBankName;
        private DevExpress.XtraLayout.SimpleLabelItem lblBranchName;
        private DevExpress.XtraLayout.SimpleLabelItem lblTextBranchName;
        private DevExpress.XtraLayout.SimpleLabelItem lblAccountNumber;
        private DevExpress.XtraLayout.SimpleLabelItem lblTextAccountNumber;
        private DevExpress.XtraLayout.SimpleLabelItem lblCreatedOn;
        private DevExpress.XtraLayout.SimpleLabelItem lblTextCreatedOn;
        private DevExpress.XtraLayout.SimpleLabelItem lblProject;
        private DevExpress.XtraLayout.SimpleLabelItem lblProjectName;
    }
}
