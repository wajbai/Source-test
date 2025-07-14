namespace ACPP.Modules.Transaction
{
    partial class frmMoveTransaction
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMoveTransaction));
            this.lcVoucherTrans = new DevExpress.XtraLayout.LayoutControl();
            this.chkShowFilter = new DevExpress.XtraEditors.CheckEdit();
            this.btnMoveTrans = new DevExpress.XtraEditors.SimpleButton();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.MoveDate = new DevExpress.XtraEditors.DateEdit();
            this.gcMoveTrans = new DevExpress.XtraGrid.GridControl();
            this.gvMoveTrans = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colProjectId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSelect = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rchkCheckProject = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.colProject = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rboSelectedProject = new DevExpress.XtraEditors.Repository.RepositoryItemRadioGroup();
            this.lcgVocherTransaction = new DevExpress.XtraLayout.LayoutControlGroup();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblDate = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcgVoucherDetails = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lblCapDate = new DevExpress.XtraLayout.SimpleLabelItem();
            this.lblVoucherDate = new DevExpress.XtraLayout.SimpleLabelItem();
            this.simpleLabelItem3 = new DevExpress.XtraLayout.SimpleLabelItem();
            this.lblVoucherNo = new DevExpress.XtraLayout.SimpleLabelItem();
            this.simpleLabelItem5 = new DevExpress.XtraLayout.SimpleLabelItem();
            this.lblProject = new DevExpress.XtraLayout.SimpleLabelItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblCapVoucherType = new DevExpress.XtraLayout.SimpleLabelItem();
            this.lblVoucherType = new DevExpress.XtraLayout.SimpleLabelItem();
            ((System.ComponentModel.ISupportInitialize)(this.lcVoucherTrans)).BeginInit();
            this.lcVoucherTrans.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowFilter.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MoveDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MoveDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcMoveTrans)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMoveTrans)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rchkCheckProject)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rboSelectedProject)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgVocherTransaction)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgVoucherDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblCapDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblVoucherDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblVoucherNo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblProject)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblCapVoucherType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblVoucherType)).BeginInit();
            this.SuspendLayout();
            // 
            // lcVoucherTrans
            // 
            this.lcVoucherTrans.Controls.Add(this.chkShowFilter);
            this.lcVoucherTrans.Controls.Add(this.btnMoveTrans);
            this.lcVoucherTrans.Controls.Add(this.btnClose);
            this.lcVoucherTrans.Controls.Add(this.MoveDate);
            this.lcVoucherTrans.Controls.Add(this.gcMoveTrans);
            resources.ApplyResources(this.lcVoucherTrans, "lcVoucherTrans");
            this.lcVoucherTrans.Name = "lcVoucherTrans";
            this.lcVoucherTrans.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(411, 216, 250, 350);
            this.lcVoucherTrans.Root = this.lcgVocherTransaction;
            // 
            // chkShowFilter
            // 
            resources.ApplyResources(this.chkShowFilter, "chkShowFilter");
            this.chkShowFilter.Name = "chkShowFilter";
            this.chkShowFilter.Properties.AllowHtmlDraw = DevExpress.Utils.DefaultBoolean.True;
            this.chkShowFilter.Properties.Caption = resources.GetString("chkShowFilter.Properties.Caption");
            this.chkShowFilter.StyleController = this.lcVoucherTrans;
            this.chkShowFilter.CheckedChanged += new System.EventHandler(this.chkShowFilter_CheckedChanged);
            // 
            // btnMoveTrans
            // 
            resources.ApplyResources(this.btnMoveTrans, "btnMoveTrans");
            this.btnMoveTrans.Name = "btnMoveTrans";
            this.btnMoveTrans.StyleController = this.lcVoucherTrans;
            this.btnMoveTrans.Click += new System.EventHandler(this.btnMoveTrans_Click);
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.btnClose, "btnClose");
            this.btnClose.Name = "btnClose";
            this.btnClose.StyleController = this.lcVoucherTrans;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // MoveDate
            // 
            resources.ApplyResources(this.MoveDate, "MoveDate");
            this.MoveDate.Name = "MoveDate";
            this.MoveDate.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.MoveDate.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.MoveDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("MoveDate.Properties.Buttons"))))});
            this.MoveDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.MoveDate.Properties.Mask.MaskType = ((DevExpress.XtraEditors.Mask.MaskType)(resources.GetObject("MoveDate.Properties.Mask.MaskType")));
            this.MoveDate.StyleController = this.lcVoucherTrans;
            // 
            // gcMoveTrans
            // 
            resources.ApplyResources(this.gcMoveTrans, "gcMoveTrans");
            this.gcMoveTrans.MainView = this.gvMoveTrans;
            this.gcMoveTrans.Name = "gcMoveTrans";
            this.gcMoveTrans.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rchkCheckProject,
            this.rboSelectedProject});
            this.gcMoveTrans.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvMoveTrans});
            // 
            // gvMoveTrans
            // 
            this.gvMoveTrans.Appearance.FocusedRow.Font = ((System.Drawing.Font)(resources.GetObject("gvMoveTrans.Appearance.FocusedRow.Font")));
            this.gvMoveTrans.Appearance.FocusedRow.Options.UseFont = true;
            this.gvMoveTrans.Appearance.HeaderPanel.Font = ((System.Drawing.Font)(resources.GetObject("gvMoveTrans.Appearance.HeaderPanel.Font")));
            this.gvMoveTrans.Appearance.HeaderPanel.Options.UseFont = true;
            this.gvMoveTrans.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colProjectId,
            this.colSelect,
            this.colProject,
            this.gridColumn1});
            this.gvMoveTrans.GridControl = this.gcMoveTrans;
            this.gvMoveTrans.Name = "gvMoveTrans";
            this.gvMoveTrans.OptionsView.ShowGroupPanel = false;
            this.gvMoveTrans.OptionsView.ShowIndicator = false;
            // 
            // colProjectId
            // 
            resources.ApplyResources(this.colProjectId, "colProjectId");
            this.colProjectId.FieldName = "PROJECT_ID";
            this.colProjectId.Name = "colProjectId";
            // 
            // colSelect
            // 
            resources.ApplyResources(this.colSelect, "colSelect");
            this.colSelect.ColumnEdit = this.rchkCheckProject;
            this.colSelect.FieldName = "FLAG";
            this.colSelect.Name = "colSelect";
            this.colSelect.OptionsColumn.ShowCaption = false;
            this.colSelect.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // rchkCheckProject
            // 
            resources.ApplyResources(this.rchkCheckProject, "rchkCheckProject");
            this.rchkCheckProject.Name = "rchkCheckProject";
            this.rchkCheckProject.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
            this.rchkCheckProject.ValueChecked = 1;
            this.rchkCheckProject.ValueGrayed = 2;
            this.rchkCheckProject.ValueUnchecked = 0;
            this.rchkCheckProject.CheckedChanged += new System.EventHandler(this.rchkCheckProject_CheckedChanged);
            // 
            // colProject
            // 
            resources.ApplyResources(this.colProject, "colProject");
            this.colProject.FieldName = "PROJECT";
            this.colProject.Name = "colProject";
            this.colProject.OptionsColumn.AllowEdit = false;
            this.colProject.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // gridColumn1
            // 
            resources.ApplyResources(this.gridColumn1, "gridColumn1");
            this.gridColumn1.ColumnEdit = this.rboSelectedProject;
            this.gridColumn1.FieldName = "SELECTION";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.ShowCaption = false;
            this.gridColumn1.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.gridColumn1.Tag = 1;
            // 
            // rboSelectedProject
            // 
            this.rboSelectedProject.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(((object)(resources.GetObject("rboSelectedProject.Items"))), resources.GetString("rboSelectedProject.Items1"))});
            this.rboSelectedProject.Name = "rboSelectedProject";
            // 
            // lcgVocherTransaction
            // 
            resources.ApplyResources(this.lcgVocherTransaction, "lcgVocherTransaction");
            this.lcgVocherTransaction.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.lcgVocherTransaction.GroupBordersVisible = false;
            this.lcgVocherTransaction.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.emptySpaceItem1,
            this.layoutControlItem1,
            this.lblDate,
            this.emptySpaceItem2,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.lcgVoucherDetails,
            this.layoutControlItem4});
            this.lcgVocherTransaction.Location = new System.Drawing.Point(0, 0);
            this.lcgVocherTransaction.Name = "Root";
            this.lcgVocherTransaction.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.lcgVocherTransaction.Size = new System.Drawing.Size(613, 365);
            this.lcgVocherTransaction.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem1, "emptySpaceItem1");
            this.emptySpaceItem1.Location = new System.Drawing.Point(82, 339);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(405, 26);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gcMoveTrans;
            resources.ApplyResources(this.layoutControlItem1, "layoutControlItem1");
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 91);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(613, 248);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // lblDate
            // 
            this.lblDate.Control = this.MoveDate;
            resources.ApplyResources(this.lblDate, "lblDate");
            this.lblDate.Location = new System.Drawing.Point(0, 65);
            this.lblDate.MaxSize = new System.Drawing.Size(116, 26);
            this.lblDate.MinSize = new System.Drawing.Size(116, 26);
            this.lblDate.Name = "lblDate";
            this.lblDate.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 0, 3, 3);
            this.lblDate.Size = new System.Drawing.Size(116, 26);
            this.lblDate.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lblDate.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.lblDate.TextSize = new System.Drawing.Size(23, 13);
            this.lblDate.TextToControlDistance = 5;
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem2, "emptySpaceItem2");
            this.emptySpaceItem2.Location = new System.Drawing.Point(116, 65);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(497, 26);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.btnClose;
            resources.ApplyResources(this.layoutControlItem2, "layoutControlItem2");
            this.layoutControlItem2.Location = new System.Drawing.Point(550, 339);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(63, 26);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.btnMoveTrans;
            resources.ApplyResources(this.layoutControlItem3, "layoutControlItem3");
            this.layoutControlItem3.Location = new System.Drawing.Point(487, 339);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(63, 26);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextToControlDistance = 0;
            this.layoutControlItem3.TextVisible = false;
            // 
            // lcgVoucherDetails
            // 
            this.lcgVoucherDetails.AppearanceGroup.Font = ((System.Drawing.Font)(resources.GetObject("lcgVoucherDetails.AppearanceGroup.Font")));
            this.lcgVoucherDetails.AppearanceGroup.Options.UseFont = true;
            resources.ApplyResources(this.lcgVoucherDetails, "lcgVoucherDetails");
            this.lcgVoucherDetails.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lblCapDate,
            this.lblVoucherDate,
            this.simpleLabelItem3,
            this.lblVoucherNo,
            this.simpleLabelItem5,
            this.lblProject,
            this.lblCapVoucherType,
            this.lblVoucherType});
            this.lcgVoucherDetails.Location = new System.Drawing.Point(0, 0);
            this.lcgVoucherDetails.Name = "lcgVoucherDetails";
            this.lcgVoucherDetails.Padding = new DevExpress.XtraLayout.Utils.Padding(3, 3, 3, 3);
            this.lcgVoucherDetails.Size = new System.Drawing.Size(613, 65);
            // 
            // lblCapDate
            // 
            this.lblCapDate.AllowHotTrack = false;
            this.lblCapDate.AppearanceItemCaption.Font = ((System.Drawing.Font)(resources.GetObject("lblCapDate.AppearanceItemCaption.Font")));
            this.lblCapDate.AppearanceItemCaption.Options.UseFont = true;
            resources.ApplyResources(this.lblCapDate, "lblCapDate");
            this.lblCapDate.Location = new System.Drawing.Point(0, 0);
            this.lblCapDate.MaxSize = new System.Drawing.Size(52, 17);
            this.lblCapDate.MinSize = new System.Drawing.Size(52, 17);
            this.lblCapDate.Name = "lblCapDate";
            this.lblCapDate.Size = new System.Drawing.Size(52, 17);
            this.lblCapDate.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lblCapDate.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.lblCapDate.TextSize = new System.Drawing.Size(27, 13);
            // 
            // lblVoucherDate
            // 
            this.lblVoucherDate.AllowHotTrack = false;
            resources.ApplyResources(this.lblVoucherDate, "lblVoucherDate");
            this.lblVoucherDate.Location = new System.Drawing.Point(52, 0);
            this.lblVoucherDate.MaxSize = new System.Drawing.Size(71, 17);
            this.lblVoucherDate.MinSize = new System.Drawing.Size(71, 17);
            this.lblVoucherDate.Name = "lblVoucherDate";
            this.lblVoucherDate.Size = new System.Drawing.Size(71, 17);
            this.lblVoucherDate.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lblVoucherDate.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.lblVoucherDate.TextSize = new System.Drawing.Size(23, 13);
            // 
            // simpleLabelItem3
            // 
            this.simpleLabelItem3.AllowHotTrack = false;
            this.simpleLabelItem3.AppearanceItemCaption.Font = ((System.Drawing.Font)(resources.GetObject("simpleLabelItem3.AppearanceItemCaption.Font")));
            this.simpleLabelItem3.AppearanceItemCaption.Options.UseFont = true;
            resources.ApplyResources(this.simpleLabelItem3, "simpleLabelItem3");
            this.simpleLabelItem3.Location = new System.Drawing.Point(0, 17);
            this.simpleLabelItem3.MaxSize = new System.Drawing.Size(52, 17);
            this.simpleLabelItem3.MinSize = new System.Drawing.Size(52, 17);
            this.simpleLabelItem3.Name = "simpleLabelItem3";
            this.simpleLabelItem3.Size = new System.Drawing.Size(52, 17);
            this.simpleLabelItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.simpleLabelItem3.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.simpleLabelItem3.TextSize = new System.Drawing.Size(41, 13);
            // 
            // lblVoucherNo
            // 
            this.lblVoucherNo.AllowHotTrack = false;
            resources.ApplyResources(this.lblVoucherNo, "lblVoucherNo");
            this.lblVoucherNo.Location = new System.Drawing.Point(515, 0);
            this.lblVoucherNo.Name = "lblVoucherNo";
            this.lblVoucherNo.Size = new System.Drawing.Size(86, 17);
            this.lblVoucherNo.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.lblVoucherNo.TextSize = new System.Drawing.Size(55, 13);
            // 
            // simpleLabelItem5
            // 
            this.simpleLabelItem5.AllowHotTrack = false;
            this.simpleLabelItem5.AppearanceItemCaption.Font = ((System.Drawing.Font)(resources.GetObject("simpleLabelItem5.AppearanceItemCaption.Font")));
            this.simpleLabelItem5.AppearanceItemCaption.Options.UseFont = true;
            resources.ApplyResources(this.simpleLabelItem5, "simpleLabelItem5");
            this.simpleLabelItem5.Location = new System.Drawing.Point(411, 0);
            this.simpleLabelItem5.Name = "simpleLabelItem5";
            this.simpleLabelItem5.Size = new System.Drawing.Size(104, 17);
            this.simpleLabelItem5.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.simpleLabelItem5.TextSize = new System.Drawing.Size(93, 13);
            // 
            // lblProject
            // 
            this.lblProject.AllowHotTrack = false;
            resources.ApplyResources(this.lblProject, "lblProject");
            this.lblProject.Location = new System.Drawing.Point(52, 17);
            this.lblProject.MaxSize = new System.Drawing.Size(549, 17);
            this.lblProject.MinSize = new System.Drawing.Size(549, 17);
            this.lblProject.Name = "lblProject";
            this.lblProject.Size = new System.Drawing.Size(549, 17);
            this.lblProject.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lblProject.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.lblProject.TextSize = new System.Drawing.Size(34, 13);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.chkShowFilter;
            resources.ApplyResources(this.layoutControlItem4, "layoutControlItem4");
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 339);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(82, 26);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextToControlDistance = 0;
            this.layoutControlItem4.TextVisible = false;
            // 
            // lblCapVoucherType
            // 
            this.lblCapVoucherType.AllowHotTrack = false;
            this.lblCapVoucherType.AppearanceItemCaption.Font = ((System.Drawing.Font)(resources.GetObject("simpleLabelItem1.AppearanceItemCaption.Font")));
            this.lblCapVoucherType.AppearanceItemCaption.Options.UseFont = true;
            resources.ApplyResources(this.lblCapVoucherType, "lblCapVoucherType");
            this.lblCapVoucherType.Location = new System.Drawing.Point(123, 0);
            this.lblCapVoucherType.MaxSize = new System.Drawing.Size(85, 17);
            this.lblCapVoucherType.MinSize = new System.Drawing.Size(85, 17);
            this.lblCapVoucherType.Name = "lblCapVoucherType";
            this.lblCapVoucherType.Size = new System.Drawing.Size(85, 17);
            this.lblCapVoucherType.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lblCapVoucherType.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.lblCapVoucherType.TextSize = new System.Drawing.Size(70, 13);
            // 
            // lblVoucherType
            // 
            this.lblVoucherType.AllowHotTrack = false;
            resources.ApplyResources(this.lblVoucherType, "lblVoucherType");
            this.lblVoucherType.Location = new System.Drawing.Point(208, 0);
            this.lblVoucherType.Name = "lblVoucherType";
            this.lblVoucherType.Size = new System.Drawing.Size(203, 17);
            this.lblVoucherType.TextSize = new System.Drawing.Size(66, 13);
            // 
            // frmMoveTransaction
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.Controls.Add(this.lcVoucherTrans);
            this.Name = "frmMoveTransaction";
            this.Load += new System.EventHandler(this.frmVoucherTransaction_Load);
            ((System.ComponentModel.ISupportInitialize)(this.lcVoucherTrans)).EndInit();
            this.lcVoucherTrans.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chkShowFilter.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MoveDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MoveDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcMoveTrans)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMoveTrans)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rchkCheckProject)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rboSelectedProject)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgVocherTransaction)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgVoucherDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblCapDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblVoucherDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblVoucherNo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblProject)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblCapVoucherType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblVoucherType)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl lcVoucherTrans;
        private DevExpress.XtraGrid.GridControl gcMoveTrans;
        private DevExpress.XtraGrid.Views.Grid.GridView gvMoveTrans;
        private DevExpress.XtraLayout.LayoutControlGroup lcgVocherTransaction;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraEditors.SimpleButton btnMoveTrans;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraEditors.DateEdit MoveDate;
        private DevExpress.XtraGrid.Columns.GridColumn colProjectId;
        private DevExpress.XtraGrid.Columns.GridColumn colProject;
        private DevExpress.XtraLayout.LayoutControlItem lblDate;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraGrid.Columns.GridColumn colSelect;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit rchkCheckProject;
        private DevExpress.XtraEditors.CheckEdit chkShowFilter;
        private DevExpress.XtraLayout.LayoutControlGroup lcgVoucherDetails;
        private DevExpress.XtraLayout.SimpleLabelItem lblCapDate;
        private DevExpress.XtraLayout.SimpleLabelItem lblVoucherDate;
        private DevExpress.XtraLayout.SimpleLabelItem simpleLabelItem3;
        private DevExpress.XtraLayout.SimpleLabelItem lblVoucherNo;
        private DevExpress.XtraLayout.SimpleLabelItem simpleLabelItem5;
        private DevExpress.XtraLayout.SimpleLabelItem lblProject;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraEditors.Repository.RepositoryItemRadioGroup rboSelectedProject;
        private DevExpress.XtraLayout.SimpleLabelItem lblCapVoucherType;
        private DevExpress.XtraLayout.SimpleLabelItem lblVoucherType;
    }
}