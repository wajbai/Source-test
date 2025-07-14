namespace ACPP.Modules.Transaction
{
    partial class frmProjectSelection
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmProjectSelection));
            this.btnOk = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.detVoucherDate = new DevExpress.XtraEditors.DateEdit();
            this.rgVoucherGroup = new DevExpress.XtraEditors.RadioGroup();
            this.lstProjectName = new DevExpress.XtraEditors.ListBoxControl();
            this.lblSelectProject = new DevExpress.XtraEditors.LabelControl();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lcgSelect = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.lblVoucherGroup = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.lcgVoucherDate = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.detVoucherDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.detVoucherDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rgVoucherGroup.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lstProjectName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgSelect)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblVoucherGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgVoucherDate)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.btnOk, "btnOk");
            this.btnOk.Name = "btnOk";
            this.btnOk.StyleController = this.layoutControl1;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.detVoucherDate);
            this.layoutControl1.Controls.Add(this.rgVoucherGroup);
            this.layoutControl1.Controls.Add(this.btnOk);
            this.layoutControl1.Controls.Add(this.lstProjectName);
            this.layoutControl1.Controls.Add(this.lblSelectProject);
            resources.ApplyResources(this.layoutControl1, "layoutControl1");
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(383, 224, 250, 350);
            this.layoutControl1.Root = this.layoutControlGroup1;
            // 
            // detVoucherDate
            // 
            resources.ApplyResources(this.detVoucherDate, "detVoucherDate");
            this.detVoucherDate.Name = "detVoucherDate";
            this.detVoucherDate.Properties.AllowFocused = false;
            this.detVoucherDate.Properties.AllowMouseWheel = false;
            this.detVoucherDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("detVoucherDate.Properties.Buttons"))))});
            this.detVoucherDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("detVoucherDate.Properties.CalendarTimeProperties.Buttons"))))});
            this.detVoucherDate.Properties.Mask.MaskType = ((DevExpress.XtraEditors.Mask.MaskType)(resources.GetObject("detVoucherDate.Properties.Mask.MaskType")));
            this.detVoucherDate.StyleController = this.layoutControl1;
            this.detVoucherDate.TabStop = false;
            this.detVoucherDate.EditValueChanged += new System.EventHandler(this.detVoucherDate_EditValueChanged);
            // 
            // rgVoucherGroup
            // 
            resources.ApplyResources(this.rgVoucherGroup, "rgVoucherGroup");
            this.rgVoucherGroup.Name = "rgVoucherGroup";
            this.rgVoucherGroup.Properties.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("rgVoucherGroup.Properties.Appearance.Font")));
            this.rgVoucherGroup.Properties.Appearance.Options.UseFont = true;
            this.rgVoucherGroup.Properties.Appearance.Options.UseTextOptions = true;
            this.rgVoucherGroup.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.rgVoucherGroup.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.rgVoucherGroup.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(((object)(resources.GetObject("rgVoucherGroup.Properties.Items"))), resources.GetString("rgVoucherGroup.Properties.Items1"), ((bool)(resources.GetObject("rgVoucherGroup.Properties.Items2")))),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(((object)(resources.GetObject("rgVoucherGroup.Properties.Items3"))), resources.GetString("rgVoucherGroup.Properties.Items4"))});
            this.rgVoucherGroup.StyleController = this.layoutControl1;
            this.rgVoucherGroup.KeyDown += new System.Windows.Forms.KeyEventHandler(this.rgVoucherGroup_KeyDown);
            // 
            // lstProjectName
            // 
            this.lstProjectName.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("lstProjectName.Appearance.Font")));
            this.lstProjectName.Appearance.Options.UseFont = true;
            this.lstProjectName.HighlightedItemStyle = DevExpress.XtraEditors.HighlightStyle.Standard;
            resources.ApplyResources(this.lstProjectName, "lstProjectName");
            this.lstProjectName.Name = "lstProjectName";
            this.lstProjectName.StyleController = this.layoutControl1;
            this.lstProjectName.SelectedIndexChanged += new System.EventHandler(this.lstProjectName_SelectedIndexChanged);
            this.lstProjectName.DrawItem += new DevExpress.XtraEditors.ListBoxDrawItemEventHandler(this.lstProjectName_DrawItem);
            this.lstProjectName.DoubleClick += new System.EventHandler(this.lstProjectName_DoubleClick);
            // 
            // lblSelectProject
            // 
            this.lblSelectProject.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("lblSelectProject.Appearance.Font")));
            resources.ApplyResources(this.lblSelectProject, "lblSelectProject");
            this.lblSelectProject.Name = "lblSelectProject";
            this.lblSelectProject.StyleController = this.layoutControl1;
            // 
            // layoutControlGroup1
            // 
            resources.ApplyResources(this.layoutControlGroup1, "layoutControlGroup1");
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lcgSelect,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.emptySpaceItem1,
            this.lblVoucherGroup,
            this.emptySpaceItem2,
            this.lcgVoucherDate});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.layoutControlGroup1.Size = new System.Drawing.Size(580, 204);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // lcgSelect
            // 
            this.lcgSelect.Control = this.lblSelectProject;
            resources.ApplyResources(this.lcgSelect, "lcgSelect");
            this.lcgSelect.Location = new System.Drawing.Point(0, 0);
            this.lcgSelect.Name = "lcgSelect";
            this.lcgSelect.Size = new System.Drawing.Size(75, 28);
            this.lcgSelect.TextSize = new System.Drawing.Size(0, 0);
            this.lcgSelect.TextToControlDistance = 0;
            this.lcgSelect.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.lstProjectName;
            resources.ApplyResources(this.layoutControlItem2, "layoutControlItem2");
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 28);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(570, 134);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.btnOk;
            resources.ApplyResources(this.layoutControlItem3, "layoutControlItem3");
            this.layoutControlItem3.Location = new System.Drawing.Point(499, 162);
            this.layoutControlItem3.MaxSize = new System.Drawing.Size(71, 32);
            this.layoutControlItem3.MinSize = new System.Drawing.Size(71, 32);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Padding = new DevExpress.XtraLayout.Utils.Padding(3, 3, 5, 3);
            this.layoutControlItem3.Size = new System.Drawing.Size(71, 32);
            this.layoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextToControlDistance = 0;
            this.layoutControlItem3.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem1, "emptySpaceItem1");
            this.emptySpaceItem1.Location = new System.Drawing.Point(75, 0);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(398, 28);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // lblVoucherGroup
            // 
            this.lblVoucherGroup.AppearanceItemCaption.Font = ((System.Drawing.Font)(resources.GetObject("lblVoucherGroup.AppearanceItemCaption.Font")));
            this.lblVoucherGroup.AppearanceItemCaption.Options.UseFont = true;
            this.lblVoucherGroup.Control = this.rgVoucherGroup;
            resources.ApplyResources(this.lblVoucherGroup, "lblVoucherGroup");
            this.lblVoucherGroup.Location = new System.Drawing.Point(0, 162);
            this.lblVoucherGroup.Name = "lblVoucherGroup";
            this.lblVoucherGroup.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 3, 3);
            this.lblVoucherGroup.Size = new System.Drawing.Size(489, 32);
            this.lblVoucherGroup.TextSize = new System.Drawing.Size(49, 15);
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem2, "emptySpaceItem2");
            this.emptySpaceItem2.Location = new System.Drawing.Point(489, 162);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(10, 32);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // lcgVoucherDate
            // 
            this.lcgVoucherDate.Control = this.detVoucherDate;
            resources.ApplyResources(this.lcgVoucherDate, "lcgVoucherDate");
            this.lcgVoucherDate.Location = new System.Drawing.Point(473, 0);
            this.lcgVoucherDate.Name = "lcgVoucherDate";
            this.lcgVoucherDate.Size = new System.Drawing.Size(97, 28);
            this.lcgVoucherDate.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.lcgVoucherDate.TextSize = new System.Drawing.Size(0, 0);
            this.lcgVoucherDate.TextToControlDistance = 0;
            this.lcgVoucherDate.TextVisible = false;
            // 
            // frmProjectSelection
            // 
            this.AcceptButton = this.btnOk;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnOk;
            this.Controls.Add(this.layoutControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MinimizeBox = false;
            this.Name = "frmProjectSelection";
            this.Load += new System.EventHandler(this.frmProjectSelection_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmProjectSelection_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.detVoucherDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.detVoucherDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rgVoucherGroup.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lstProjectName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgSelect)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblVoucherGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgVoucherDate)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraEditors.ListBoxControl lstProjectName;
        private DevExpress.XtraEditors.LabelControl lblSelectProject;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraEditors.SimpleButton btnOk;
        private DevExpress.XtraLayout.LayoutControlItem lcgSelect;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraEditors.RadioGroup rgVoucherGroup;
        private DevExpress.XtraLayout.LayoutControlItem lblVoucherGroup;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraEditors.DateEdit detVoucherDate;
        private DevExpress.XtraLayout.LayoutControlItem lcgVoucherDate;

    }
}