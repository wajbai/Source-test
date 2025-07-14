namespace ACPP.Modules.Data_Utility
{
    partial class frmResetLedgerOPBalance
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmResetLedgerOPBalance));
            this.lctlResetOpeningBalance = new DevExpress.XtraLayout.LayoutControl();
            this.lblExcludeNote = new DevExpress.XtraEditors.LabelControl();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.BtnUpdateOpBalance = new DevExpress.XtraEditors.SimpleButton();
            this.lblNote = new DevExpress.XtraEditors.LabelControl();
            this.chkIncludeBankLedgers = new DevExpress.XtraEditors.CheckEdit();
            this.chkIncludeCashLedgers = new DevExpress.XtraEditors.CheckEdit();
            this.lkpNature = new DevExpress.XtraEditors.LookUpEdit();
            this.lkpLedgerGroup = new DevExpress.XtraEditors.LookUpEdit();
            this.glkpProject = new DevExpress.XtraEditors.GridLookUpEdit();
            this.gridLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gvColprojectId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gvColProjectName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.lgrpResetOpening = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lcProject = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem3 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.lcLedgerGroup = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcNature = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcChkCashLedgers = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcchkBankLedgers = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcNote = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.lcUpdateOpBalance = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcClose = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem4 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.lcExcludeNote = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.lctlResetOpeningBalance)).BeginInit();
            this.lctlResetOpeningBalance.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkIncludeBankLedgers.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIncludeCashLedgers.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpNature.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpLedgerGroup.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.glkpProject.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lgrpResetOpening)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcProject)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcLedgerGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcNature)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcChkCashLedgers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcchkBankLedgers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcNote)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcUpdateOpBalance)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcExcludeNote)).BeginInit();
            this.SuspendLayout();
            // 
            // lctlResetOpeningBalance
            // 
            this.lctlResetOpeningBalance.Controls.Add(this.lblExcludeNote);
            this.lctlResetOpeningBalance.Controls.Add(this.btnClose);
            this.lctlResetOpeningBalance.Controls.Add(this.BtnUpdateOpBalance);
            this.lctlResetOpeningBalance.Controls.Add(this.lblNote);
            this.lctlResetOpeningBalance.Controls.Add(this.chkIncludeBankLedgers);
            this.lctlResetOpeningBalance.Controls.Add(this.chkIncludeCashLedgers);
            this.lctlResetOpeningBalance.Controls.Add(this.lkpNature);
            this.lctlResetOpeningBalance.Controls.Add(this.lkpLedgerGroup);
            this.lctlResetOpeningBalance.Controls.Add(this.glkpProject);
            resources.ApplyResources(this.lctlResetOpeningBalance, "lctlResetOpeningBalance");
            this.lctlResetOpeningBalance.Name = "lctlResetOpeningBalance";
            this.lctlResetOpeningBalance.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(761, 129, 250, 350);
            this.lctlResetOpeningBalance.Root = this.lgrpResetOpening;
            // 
            // lblExcludeNote
            // 
            this.lblExcludeNote.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("lblExcludeNote.Appearance.Font")));
            this.lblExcludeNote.Appearance.ForeColor = ((System.Drawing.Color)(resources.GetObject("lblExcludeNote.Appearance.ForeColor")));
            resources.ApplyResources(this.lblExcludeNote, "lblExcludeNote");
            this.lblExcludeNote.Name = "lblExcludeNote";
            this.lblExcludeNote.StyleController = this.lctlResetOpeningBalance;
            // 
            // btnClose
            // 
            resources.ApplyResources(this.btnClose, "btnClose");
            this.btnClose.Name = "btnClose";
            this.btnClose.StyleController = this.lctlResetOpeningBalance;
            this.btnClose.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // BtnUpdateOpBalance
            // 
            resources.ApplyResources(this.BtnUpdateOpBalance, "BtnUpdateOpBalance");
            this.BtnUpdateOpBalance.Name = "BtnUpdateOpBalance";
            this.BtnUpdateOpBalance.StyleController = this.lctlResetOpeningBalance;
            this.BtnUpdateOpBalance.Click += new System.EventHandler(this.BtnUpdateOpBalance_Click);
            // 
            // lblNote
            // 
            this.lblNote.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("lblNote.Appearance.Font")));
            this.lblNote.Appearance.Image = global::ACPP.Properties.Resources.info;
            this.lblNote.Appearance.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblNote.Appearance.PressedImage = global::ACPP.Properties.Resources.info;
            resources.ApplyResources(this.lblNote, "lblNote");
            this.lblNote.ImageAlignToText = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.lblNote.Name = "lblNote";
            this.lblNote.StyleController = this.lctlResetOpeningBalance;
            // 
            // chkIncludeBankLedgers
            // 
            resources.ApplyResources(this.chkIncludeBankLedgers, "chkIncludeBankLedgers");
            this.chkIncludeBankLedgers.Name = "chkIncludeBankLedgers";
            this.chkIncludeBankLedgers.Properties.Caption = resources.GetString("chkIncludeBankLedgers.Properties.Caption");
            this.chkIncludeBankLedgers.StyleController = this.lctlResetOpeningBalance;
            // 
            // chkIncludeCashLedgers
            // 
            resources.ApplyResources(this.chkIncludeCashLedgers, "chkIncludeCashLedgers");
            this.chkIncludeCashLedgers.Name = "chkIncludeCashLedgers";
            this.chkIncludeCashLedgers.Properties.Caption = resources.GetString("chkIncludeCashLedgers.Properties.Caption");
            this.chkIncludeCashLedgers.StyleController = this.lctlResetOpeningBalance;
            // 
            // lkpNature
            // 
            resources.ApplyResources(this.lkpNature, "lkpNature");
            this.lkpNature.Name = "lkpNature";
            this.lkpNature.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.lkpNature.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("lkpNature.Properties.Buttons"))))});
            this.lkpNature.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo(resources.GetString("lkpNature.Properties.Columns"), resources.GetString("lkpNature.Properties.Columns1"), ((int)(resources.GetObject("lkpNature.Properties.Columns2"))), ((DevExpress.Utils.FormatType)(resources.GetObject("lkpNature.Properties.Columns3"))), resources.GetString("lkpNature.Properties.Columns4"), ((bool)(resources.GetObject("lkpNature.Properties.Columns5"))), ((DevExpress.Utils.HorzAlignment)(resources.GetObject("lkpNature.Properties.Columns6")))),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo(resources.GetString("lkpNature.Properties.Columns7"), resources.GetString("lkpNature.Properties.Columns8"))});
            this.lkpNature.Properties.ImmediatePopup = true;
            this.lkpNature.Properties.NullText = resources.GetString("lkpNature.Properties.NullText");
            this.lkpNature.Properties.PopupFormMinSize = new System.Drawing.Size(10, 0);
            this.lkpNature.Properties.ShowHeader = false;
            this.lkpNature.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.lkpNature.StyleController = this.lctlResetOpeningBalance;
            this.lkpNature.EditValueChanged += new System.EventHandler(this.lkpNature_EditValueChanged);
            // 
            // lkpLedgerGroup
            // 
            resources.ApplyResources(this.lkpLedgerGroup, "lkpLedgerGroup");
            this.lkpLedgerGroup.Name = "lkpLedgerGroup";
            this.lkpLedgerGroup.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.lkpLedgerGroup.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("lkpLedgerGroup.Properties.Buttons"))))});
            this.lkpLedgerGroup.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo(resources.GetString("lkpLedgerGroup.Properties.Columns"), resources.GetString("lkpLedgerGroup.Properties.Columns1"), ((int)(resources.GetObject("lkpLedgerGroup.Properties.Columns2"))), ((DevExpress.Utils.FormatType)(resources.GetObject("lkpLedgerGroup.Properties.Columns3"))), resources.GetString("lkpLedgerGroup.Properties.Columns4"), ((bool)(resources.GetObject("lkpLedgerGroup.Properties.Columns5"))), ((DevExpress.Utils.HorzAlignment)(resources.GetObject("lkpLedgerGroup.Properties.Columns6")))),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo(resources.GetString("lkpLedgerGroup.Properties.Columns7"), resources.GetString("lkpLedgerGroup.Properties.Columns8"))});
            this.lkpLedgerGroup.Properties.ImmediatePopup = true;
            this.lkpLedgerGroup.Properties.NullText = resources.GetString("lkpLedgerGroup.Properties.NullText");
            this.lkpLedgerGroup.Properties.PopupFormMinSize = new System.Drawing.Size(10, 0);
            this.lkpLedgerGroup.Properties.ShowHeader = false;
            this.lkpLedgerGroup.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.lkpLedgerGroup.StyleController = this.lctlResetOpeningBalance;
            this.lkpLedgerGroup.EditValueChanged += new System.EventHandler(this.lkpLedgerGroup_EditValueChanged);
            // 
            // glkpProject
            // 
            resources.ApplyResources(this.glkpProject, "glkpProject");
            this.glkpProject.Name = "glkpProject";
            this.glkpProject.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.glkpProject.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("glkpProject.Properties.Buttons"))))});
            this.glkpProject.Properties.NullText = resources.GetString("glkpProject.Properties.NullText");
            this.glkpProject.Properties.View = this.gridLookUpEdit1View;
            this.glkpProject.StyleController = this.lctlResetOpeningBalance;
            this.glkpProject.Tag = "PR";
            // 
            // gridLookUpEdit1View
            // 
            this.gridLookUpEdit1View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gvColprojectId,
            this.gvColProjectName});
            this.gridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridLookUpEdit1View.Name = "gridLookUpEdit1View";
            this.gridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridLookUpEdit1View.OptionsView.ShowColumnHeaders = false;
            this.gridLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            this.gridLookUpEdit1View.OptionsView.ShowIndicator = false;
            // 
            // gvColprojectId
            // 
            resources.ApplyResources(this.gvColprojectId, "gvColprojectId");
            this.gvColprojectId.FieldName = "PROJEC_ID";
            this.gvColprojectId.Name = "gvColprojectId";
            // 
            // gvColProjectName
            // 
            resources.ApplyResources(this.gvColProjectName, "gvColProjectName");
            this.gvColProjectName.FieldName = "PROJECT";
            this.gvColProjectName.Name = "gvColProjectName";
            // 
            // lgrpResetOpening
            // 
            resources.ApplyResources(this.lgrpResetOpening, "lgrpResetOpening");
            this.lgrpResetOpening.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.lgrpResetOpening.GroupBordersVisible = false;
            this.lgrpResetOpening.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lcProject,
            this.emptySpaceItem3,
            this.lcLedgerGroup,
            this.lcNature,
            this.lcChkCashLedgers,
            this.lcchkBankLedgers,
            this.lcNote,
            this.emptySpaceItem1,
            this.emptySpaceItem2,
            this.lcUpdateOpBalance,
            this.lcClose,
            this.emptySpaceItem4,
            this.lcExcludeNote});
            this.lgrpResetOpening.Location = new System.Drawing.Point(0, 0);
            this.lgrpResetOpening.Name = "Root";
            this.lgrpResetOpening.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.lgrpResetOpening.Size = new System.Drawing.Size(438, 150);
            this.lgrpResetOpening.TextVisible = false;
            // 
            // lcProject
            // 
            this.lcProject.AppearanceItemCaption.Options.UseTextOptions = true;
            this.lcProject.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.lcProject.Control = this.glkpProject;
            resources.ApplyResources(this.lcProject, "lcProject");
            this.lcProject.Location = new System.Drawing.Point(0, 42);
            this.lcProject.Name = "lcProject";
            this.lcProject.Size = new System.Drawing.Size(428, 24);
            this.lcProject.TextSize = new System.Drawing.Size(54, 13);
            // 
            // emptySpaceItem3
            // 
            this.emptySpaceItem3.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem3, "emptySpaceItem3");
            this.emptySpaceItem3.Location = new System.Drawing.Point(122, 114);
            this.emptySpaceItem3.Name = "emptySpaceItem3";
            this.emptySpaceItem3.Size = new System.Drawing.Size(124, 26);
            this.emptySpaceItem3.TextSize = new System.Drawing.Size(0, 0);
            // 
            // lcLedgerGroup
            // 
            this.lcLedgerGroup.AppearanceItemCaption.Options.UseTextOptions = true;
            this.lcLedgerGroup.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.lcLedgerGroup.Control = this.lkpLedgerGroup;
            resources.ApplyResources(this.lcLedgerGroup, "lcLedgerGroup");
            this.lcLedgerGroup.Location = new System.Drawing.Point(0, 90);
            this.lcLedgerGroup.MaxSize = new System.Drawing.Size(371, 24);
            this.lcLedgerGroup.MinSize = new System.Drawing.Size(371, 24);
            this.lcLedgerGroup.Name = "lcLedgerGroup";
            this.lcLedgerGroup.Size = new System.Drawing.Size(371, 24);
            this.lcLedgerGroup.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lcLedgerGroup.TextSize = new System.Drawing.Size(54, 13);
            // 
            // lcNature
            // 
            this.lcNature.AppearanceItemCaption.Options.UseTextOptions = true;
            this.lcNature.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.lcNature.Control = this.lkpNature;
            resources.ApplyResources(this.lcNature, "lcNature");
            this.lcNature.Location = new System.Drawing.Point(0, 66);
            this.lcNature.MaxSize = new System.Drawing.Size(194, 24);
            this.lcNature.MinSize = new System.Drawing.Size(194, 24);
            this.lcNature.Name = "lcNature";
            this.lcNature.Size = new System.Drawing.Size(194, 24);
            this.lcNature.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lcNature.TextSize = new System.Drawing.Size(54, 13);
            // 
            // lcChkCashLedgers
            // 
            this.lcChkCashLedgers.Control = this.chkIncludeCashLedgers;
            resources.ApplyResources(this.lcChkCashLedgers, "lcChkCashLedgers");
            this.lcChkCashLedgers.Location = new System.Drawing.Point(194, 66);
            this.lcChkCashLedgers.MaxSize = new System.Drawing.Size(89, 24);
            this.lcChkCashLedgers.MinSize = new System.Drawing.Size(89, 24);
            this.lcChkCashLedgers.Name = "lcChkCashLedgers";
            this.lcChkCashLedgers.Size = new System.Drawing.Size(89, 24);
            this.lcChkCashLedgers.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lcChkCashLedgers.TextSize = new System.Drawing.Size(0, 0);
            this.lcChkCashLedgers.TextToControlDistance = 0;
            this.lcChkCashLedgers.TextVisible = false;
            this.lcChkCashLedgers.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            // 
            // lcchkBankLedgers
            // 
            this.lcchkBankLedgers.Control = this.chkIncludeBankLedgers;
            resources.ApplyResources(this.lcchkBankLedgers, "lcchkBankLedgers");
            this.lcchkBankLedgers.Location = new System.Drawing.Point(283, 66);
            this.lcchkBankLedgers.MaxSize = new System.Drawing.Size(87, 24);
            this.lcchkBankLedgers.MinSize = new System.Drawing.Size(87, 24);
            this.lcchkBankLedgers.Name = "lcchkBankLedgers";
            this.lcchkBankLedgers.Size = new System.Drawing.Size(87, 24);
            this.lcchkBankLedgers.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lcchkBankLedgers.TextSize = new System.Drawing.Size(0, 0);
            this.lcchkBankLedgers.TextToControlDistance = 0;
            this.lcchkBankLedgers.TextVisible = false;
            this.lcchkBankLedgers.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            // 
            // lcNote
            // 
            this.lcNote.Control = this.lblNote;
            resources.ApplyResources(this.lcNote, "lcNote");
            this.lcNote.Location = new System.Drawing.Point(0, 0);
            this.lcNote.Name = "lcNote";
            this.lcNote.Size = new System.Drawing.Size(428, 32);
            this.lcNote.TextSize = new System.Drawing.Size(0, 0);
            this.lcNote.TextToControlDistance = 0;
            this.lcNote.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem1, "emptySpaceItem1");
            this.emptySpaceItem1.Location = new System.Drawing.Point(371, 90);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(57, 24);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem2, "emptySpaceItem2");
            this.emptySpaceItem2.Location = new System.Drawing.Point(370, 66);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(58, 24);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // lcUpdateOpBalance
            // 
            this.lcUpdateOpBalance.Control = this.BtnUpdateOpBalance;
            this.lcUpdateOpBalance.ControlAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            resources.ApplyResources(this.lcUpdateOpBalance, "lcUpdateOpBalance");
            this.lcUpdateOpBalance.Location = new System.Drawing.Point(246, 114);
            this.lcUpdateOpBalance.MaxSize = new System.Drawing.Size(118, 26);
            this.lcUpdateOpBalance.MinSize = new System.Drawing.Size(118, 26);
            this.lcUpdateOpBalance.Name = "lcUpdateOpBalance";
            this.lcUpdateOpBalance.Size = new System.Drawing.Size(118, 26);
            this.lcUpdateOpBalance.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lcUpdateOpBalance.TextSize = new System.Drawing.Size(0, 0);
            this.lcUpdateOpBalance.TextToControlDistance = 0;
            this.lcUpdateOpBalance.TextVisible = false;
            this.lcUpdateOpBalance.TrimClientAreaToControl = false;
            // 
            // lcClose
            // 
            this.lcClose.Control = this.btnClose;
            this.lcClose.ControlAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            resources.ApplyResources(this.lcClose, "lcClose");
            this.lcClose.Location = new System.Drawing.Point(364, 114);
            this.lcClose.MaxSize = new System.Drawing.Size(64, 26);
            this.lcClose.MinSize = new System.Drawing.Size(64, 26);
            this.lcClose.Name = "lcClose";
            this.lcClose.Size = new System.Drawing.Size(64, 26);
            this.lcClose.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lcClose.TextSize = new System.Drawing.Size(0, 0);
            this.lcClose.TextToControlDistance = 0;
            this.lcClose.TextVisible = false;
            this.lcClose.TrimClientAreaToControl = false;
            // 
            // emptySpaceItem4
            // 
            this.emptySpaceItem4.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem4, "emptySpaceItem4");
            this.emptySpaceItem4.Location = new System.Drawing.Point(0, 32);
            this.emptySpaceItem4.MaxSize = new System.Drawing.Size(411, 10);
            this.emptySpaceItem4.MinSize = new System.Drawing.Size(411, 10);
            this.emptySpaceItem4.Name = "emptySpaceItem4";
            this.emptySpaceItem4.Size = new System.Drawing.Size(428, 10);
            this.emptySpaceItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem4.TextSize = new System.Drawing.Size(0, 0);
            // 
            // lcExcludeNote
            // 
            this.lcExcludeNote.Control = this.lblExcludeNote;
            this.lcExcludeNote.ControlAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            resources.ApplyResources(this.lcExcludeNote, "lcExcludeNote");
            this.lcExcludeNote.Location = new System.Drawing.Point(0, 114);
            this.lcExcludeNote.Name = "lcExcludeNote";
            this.lcExcludeNote.Size = new System.Drawing.Size(122, 26);
            this.lcExcludeNote.TextSize = new System.Drawing.Size(0, 0);
            this.lcExcludeNote.TextToControlDistance = 0;
            this.lcExcludeNote.TextVisible = false;
            this.lcExcludeNote.TrimClientAreaToControl = false;
            this.lcExcludeNote.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            // 
            // frmResetLedgerOPBalance
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lctlResetOpeningBalance);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmResetLedgerOPBalance";
            this.Load += new System.EventHandler(this.frmBalanceRefresh_Load);
            ((System.ComponentModel.ISupportInitialize)(this.lctlResetOpeningBalance)).EndInit();
            this.lctlResetOpeningBalance.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chkIncludeBankLedgers.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIncludeCashLedgers.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpNature.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpLedgerGroup.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.glkpProject.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lgrpResetOpening)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcProject)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcLedgerGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcNature)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcChkCashLedgers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcchkBankLedgers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcNote)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcUpdateOpBalance)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcExcludeNote)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl lctlResetOpeningBalance;
        private DevExpress.XtraEditors.GridLookUpEdit glkpProject;
        private DevExpress.XtraGrid.Views.Grid.GridView gridLookUpEdit1View;
        private DevExpress.XtraLayout.LayoutControlGroup lgrpResetOpening;
        private DevExpress.XtraLayout.LayoutControlItem lcProject;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem3;
        private DevExpress.XtraGrid.Columns.GridColumn gvColprojectId;
        private DevExpress.XtraGrid.Columns.GridColumn gvColProjectName;
        private DevExpress.XtraEditors.LookUpEdit lkpLedgerGroup;
        private DevExpress.XtraLayout.LayoutControlItem lcLedgerGroup;
        private DevExpress.XtraEditors.LookUpEdit lkpNature;
        private DevExpress.XtraLayout.LayoutControlItem lcNature;
        private DevExpress.XtraEditors.CheckEdit chkIncludeBankLedgers;
        private DevExpress.XtraEditors.CheckEdit chkIncludeCashLedgers;
        private DevExpress.XtraLayout.LayoutControlItem lcChkCashLedgers;
        private DevExpress.XtraLayout.LayoutControlItem lcchkBankLedgers;
        private DevExpress.XtraEditors.LabelControl lblNote;
        private DevExpress.XtraLayout.LayoutControlItem lcNote;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraEditors.SimpleButton BtnUpdateOpBalance;
        private DevExpress.XtraLayout.LayoutControlItem lcUpdateOpBalance;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraLayout.LayoutControlItem lcClose;
        private DevExpress.XtraEditors.LabelControl lblExcludeNote;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem4;
        private DevExpress.XtraLayout.LayoutControlItem lcExcludeNote;
    }
}