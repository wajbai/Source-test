namespace ACPP.Modules.ProspectsDonor
{
    partial class frmProspectsView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmProspectsView));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.btnApply = new DevExpress.XtraEditors.SimpleButton();
            this.glkpRegistrationType = new DevExpress.XtraEditors.GridLookUpEdit();
            this.gridLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colRegistrationTypeId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRegistrationType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.chkShowFilter = new DevExpress.XtraEditors.CheckEdit();
            this.ucProspectsToolbar = new Bosco.Utility.Controls.ucToolBar();
            this.gcProspects = new DevExpress.XtraGrid.GridControl();
            this.gvProspects = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colProspectId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRegNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gccolRegistrationType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAddress = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPlace = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colState = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCountry = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPhone = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTelephone = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colReferedStaff = new DevExpress.XtraGrid.Columns.GridColumn();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblRowCount = new DevExpress.XtraLayout.SimpleLabelItem();
            this.simpleLabelItem2 = new DevExpress.XtraLayout.SimpleLabelItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.glkpRegistrationType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowFilter.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcProspects)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvProspects)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblRowCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.btnApply);
            this.layoutControl1.Controls.Add(this.glkpRegistrationType);
            this.layoutControl1.Controls.Add(this.chkShowFilter);
            this.layoutControl1.Controls.Add(this.ucProspectsToolbar);
            this.layoutControl1.Controls.Add(this.gcProspects);
            resources.ApplyResources(this.layoutControl1, "layoutControl1");
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(278, 328, 250, 350);
            this.layoutControl1.Root = this.layoutControlGroup1;
            // 
            // btnApply
            // 
            resources.ApplyResources(this.btnApply, "btnApply");
            this.btnApply.Name = "btnApply";
            this.btnApply.StyleController = this.layoutControl1;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // glkpRegistrationType
            // 
            resources.ApplyResources(this.glkpRegistrationType, "glkpRegistrationType");
            this.glkpRegistrationType.Name = "glkpRegistrationType";
            this.glkpRegistrationType.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.glkpRegistrationType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("glkpRegistrationType.Properties.Buttons"))))});
            this.glkpRegistrationType.Properties.ImmediatePopup = true;
            this.glkpRegistrationType.Properties.NullText = resources.GetString("glkpRegistrationType.Properties.NullText");
            this.glkpRegistrationType.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            this.glkpRegistrationType.Properties.PopupFormSize = new System.Drawing.Size(242, 0);
            this.glkpRegistrationType.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.glkpRegistrationType.Properties.View = this.gridLookUpEdit1View;
            this.glkpRegistrationType.StyleController = this.layoutControl1;
            this.glkpRegistrationType.EditValueChanged += new System.EventHandler(this.glkpRegistrationType_EditValueChanged);
            // 
            // gridLookUpEdit1View
            // 
            this.gridLookUpEdit1View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colRegistrationTypeId,
            this.colRegistrationType});
            this.gridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridLookUpEdit1View.Name = "gridLookUpEdit1View";
            this.gridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridLookUpEdit1View.OptionsView.ShowColumnHeaders = false;
            this.gridLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            this.gridLookUpEdit1View.OptionsView.ShowIndicator = false;
            // 
            // colRegistrationTypeId
            // 
            resources.ApplyResources(this.colRegistrationTypeId, "colRegistrationTypeId");
            this.colRegistrationTypeId.FieldName = "REGISTRATION_TYPE_ID";
            this.colRegistrationTypeId.Name = "colRegistrationTypeId";
            // 
            // colRegistrationType
            // 
            resources.ApplyResources(this.colRegistrationType, "colRegistrationType");
            this.colRegistrationType.FieldName = "REGISTRATION_TYPE";
            this.colRegistrationType.Name = "colRegistrationType";
            // 
            // chkShowFilter
            // 
            resources.ApplyResources(this.chkShowFilter, "chkShowFilter");
            this.chkShowFilter.Name = "chkShowFilter";
            this.chkShowFilter.Properties.Caption = resources.GetString("chkShowFilter.Properties.Caption");
            this.chkShowFilter.StyleController = this.layoutControl1;
            this.chkShowFilter.CheckedChanged += new System.EventHandler(this.chkShowFilter_CheckedChanged);
            // 
            // ucProspectsToolbar
            // 
            this.ucProspectsToolbar.ChangeAddCaption = "&Add";
            this.ucProspectsToolbar.ChangeCaption = "&Edit";
            this.ucProspectsToolbar.ChangeDeleteCaption = "&Delete";
            this.ucProspectsToolbar.ChangeNatureOfPaymentCaption = "&Nature of Payments";
            this.ucProspectsToolbar.ChangePostInterestCaption = "P&ost Interest";
            this.ucProspectsToolbar.ChangePrintCaption = "&Print";
            this.ucProspectsToolbar.ChnageRenewCaption = "Re<u>n</u>ew";
            this.ucProspectsToolbar.DisableAddButton = true;
            this.ucProspectsToolbar.DisableAMCRenew = true;
            this.ucProspectsToolbar.DisableCloseButton = true;
            this.ucProspectsToolbar.DisableDeleteButton = true;
            this.ucProspectsToolbar.DisableDownloadExcel = true;
            this.ucProspectsToolbar.DisableEditButton = true;
            this.ucProspectsToolbar.DisableMoveTransaction = false;
            this.ucProspectsToolbar.DisableNatureofPayments = false;
            this.ucProspectsToolbar.DisablePostInterest = false;
            this.ucProspectsToolbar.DisablePrintButton = true;
            this.ucProspectsToolbar.DisableRestoreVoucher = true;
            resources.ApplyResources(this.ucProspectsToolbar, "ucProspectsToolbar");
            this.ucProspectsToolbar.Name = "ucProspectsToolbar";
            this.ucProspectsToolbar.ShowHTML = true;
            this.ucProspectsToolbar.ShowMMT = true;
            this.ucProspectsToolbar.ShowPDF = true;
            this.ucProspectsToolbar.ShowRTF = true;
            this.ucProspectsToolbar.ShowText = true;
            this.ucProspectsToolbar.ShowXLS = true;
            this.ucProspectsToolbar.ShowXLSX = true;
            this.ucProspectsToolbar.VisbleInsertVoucher = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucProspectsToolbar.VisibleAddButton = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucProspectsToolbar.VisibleClose = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucProspectsToolbar.VisibleDeleteButton = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucProspectsToolbar.VisibleDownloadExcel = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucProspectsToolbar.VisibleEditButton = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucProspectsToolbar.VisibleMoveTrans = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucProspectsToolbar.VisibleNatureofPayments = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucProspectsToolbar.VisibleNegativeBalance = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucProspectsToolbar.VisiblePostInterest = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucProspectsToolbar.VisiblePrintButton = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucProspectsToolbar.VisibleRefresh = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucProspectsToolbar.VisibleRenew = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucProspectsToolbar.VisibleRestoreVoucher = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucProspectsToolbar.AddClicked += new System.EventHandler(this.ucProspectsToolbar_AddClicked);
            this.ucProspectsToolbar.EditClicked += new System.EventHandler(this.ucProspectsToolbar_EditClicked);
            this.ucProspectsToolbar.DeleteClicked += new System.EventHandler(this.ucProspectsToolbar_DeleteClicked);
            this.ucProspectsToolbar.PrintClicked += new System.EventHandler(this.ucProspectsToolbar_PrintClicked);
            this.ucProspectsToolbar.CloseClicked += new System.EventHandler(this.ucProspectsToolbar_CloseClicked);
            this.ucProspectsToolbar.RefreshClicked += new System.EventHandler(this.ucProspectsToolbar_RefreshClicked);
            this.ucProspectsToolbar.DownloadExcel += new System.EventHandler(this.ucProspectsToolbar_DownloadExcel);
            this.ucProspectsToolbar.RenewClicked += new System.EventHandler(this.ucProspectsToolbar_RenewClicked);
            // 
            // gcProspects
            // 
            resources.ApplyResources(this.gcProspects, "gcProspects");
            this.gcProspects.MainView = this.gvProspects;
            this.gcProspects.Name = "gcProspects";
            this.gcProspects.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvProspects});
            // 
            // gvProspects
            // 
            this.gvProspects.Appearance.FocusedRow.Font = ((System.Drawing.Font)(resources.GetObject("gvProspects.Appearance.FocusedRow.Font")));
            this.gvProspects.Appearance.FocusedRow.Options.UseFont = true;
            this.gvProspects.Appearance.HeaderPanel.Font = ((System.Drawing.Font)(resources.GetObject("gvProspects.Appearance.HeaderPanel.Font")));
            this.gvProspects.Appearance.HeaderPanel.Options.UseFont = true;
            this.gvProspects.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.gvProspects.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colProspectId,
            this.colName,
            this.colRegNo,
            this.gccolRegistrationType,
            this.colAddress,
            this.colPlace,
            this.colState,
            this.colCountry,
            this.colPhone,
            this.colTelephone,
            this.colReferedStaff});
            this.gvProspects.GridControl = this.gcProspects;
            this.gvProspects.Name = "gvProspects";
            this.gvProspects.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.gvProspects.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            this.gvProspects.OptionsBehavior.Editable = false;
            this.gvProspects.OptionsCustomization.AllowGroup = false;
            this.gvProspects.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvProspects.OptionsView.ShowGroupPanel = false;
            this.gvProspects.DoubleClick += new System.EventHandler(this.gvProspects_DoubleClick);
            this.gvProspects.RowCountChanged += new System.EventHandler(this.gvProspects_RowCountChanged);
            // 
            // colProspectId
            // 
            resources.ApplyResources(this.colProspectId, "colProspectId");
            this.colProspectId.FieldName = "PROSPECT_ID";
            this.colProspectId.Name = "colProspectId";
            // 
            // colName
            // 
            this.colName.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colName.AppearanceHeader.Font")));
            this.colName.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colName, "colName");
            this.colName.FieldName = "NAME";
            this.colName.Name = "colName";
            // 
            // colRegNo
            // 
            resources.ApplyResources(this.colRegNo, "colRegNo");
            this.colRegNo.FieldName = "REGISTER_NO";
            this.colRegNo.Name = "colRegNo";
            // 
            // gccolRegistrationType
            // 
            this.gccolRegistrationType.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("gccolRegistrationType.AppearanceHeader.Font")));
            this.gccolRegistrationType.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.gccolRegistrationType, "gccolRegistrationType");
            this.gccolRegistrationType.FieldName = "REGISTRATION_TYPE";
            this.gccolRegistrationType.Name = "gccolRegistrationType";
            // 
            // colAddress
            // 
            this.colAddress.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colAddress.AppearanceHeader.Font")));
            this.colAddress.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colAddress, "colAddress");
            this.colAddress.FieldName = "ADDRESS";
            this.colAddress.Name = "colAddress";
            // 
            // colPlace
            // 
            this.colPlace.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colPlace.AppearanceHeader.Font")));
            this.colPlace.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colPlace, "colPlace");
            this.colPlace.FieldName = "PLACE";
            this.colPlace.Name = "colPlace";
            // 
            // colState
            // 
            resources.ApplyResources(this.colState, "colState");
            this.colState.FieldName = "STATE_NAME";
            this.colState.Name = "colState";
            // 
            // colCountry
            // 
            resources.ApplyResources(this.colCountry, "colCountry");
            this.colCountry.FieldName = "COUNTRY";
            this.colCountry.Name = "colCountry";
            // 
            // colPhone
            // 
            resources.ApplyResources(this.colPhone, "colPhone");
            this.colPhone.FieldName = "PHONE";
            this.colPhone.Name = "colPhone";
            // 
            // colTelephone
            // 
            resources.ApplyResources(this.colTelephone, "colTelephone");
            this.colTelephone.FieldName = "TELEPHONE";
            this.colTelephone.Name = "colTelephone";
            // 
            // colReferedStaff
            // 
            resources.ApplyResources(this.colReferedStaff, "colReferedStaff");
            this.colReferedStaff.FieldName = "REFERRED_STAFF";
            this.colReferedStaff.Name = "colReferedStaff";
            // 
            // layoutControlGroup1
            // 
            resources.ApplyResources(this.layoutControlGroup1, "layoutControlGroup1");
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.emptySpaceItem2,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.lblRowCount,
            this.simpleLabelItem2,
            this.layoutControlItem4,
            this.layoutControlItem7,
            this.emptySpaceItem1});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(1038, 411);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gcProspects;
            resources.ApplyResources(this.layoutControlItem1, "layoutControlItem1");
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 69);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlItem1.Size = new System.Drawing.Size(1038, 319);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem2, "emptySpaceItem2");
            this.emptySpaceItem2.Location = new System.Drawing.Point(80, 388);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(880, 23);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.ucProspectsToolbar;
            resources.ApplyResources(this.layoutControlItem2, "layoutControlItem2");
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.MaxSize = new System.Drawing.Size(0, 37);
            this.layoutControlItem2.MinSize = new System.Drawing.Size(108, 37);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(1038, 37);
            this.layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.chkShowFilter;
            resources.ApplyResources(this.layoutControlItem3, "layoutControlItem3");
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 388);
            this.layoutControlItem3.MaxSize = new System.Drawing.Size(80, 23);
            this.layoutControlItem3.MinSize = new System.Drawing.Size(80, 23);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(80, 23);
            this.layoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextToControlDistance = 0;
            this.layoutControlItem3.TextVisible = false;
            // 
            // lblRowCount
            // 
            this.lblRowCount.AllowHotTrack = false;
            this.lblRowCount.AppearanceItemCaption.Font = ((System.Drawing.Font)(resources.GetObject("lblRowCount.AppearanceItemCaption.Font")));
            this.lblRowCount.AppearanceItemCaption.Options.UseFont = true;
            resources.ApplyResources(this.lblRowCount, "lblRowCount");
            this.lblRowCount.Location = new System.Drawing.Point(977, 388);
            this.lblRowCount.MaxSize = new System.Drawing.Size(61, 23);
            this.lblRowCount.MinSize = new System.Drawing.Size(61, 23);
            this.lblRowCount.Name = "lblRowCount";
            this.lblRowCount.Size = new System.Drawing.Size(61, 23);
            this.lblRowCount.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lblRowCount.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.lblRowCount.TextSize = new System.Drawing.Size(7, 13);
            // 
            // simpleLabelItem2
            // 
            this.simpleLabelItem2.AllowHotTrack = false;
            this.simpleLabelItem2.AppearanceItemCaption.Font = ((System.Drawing.Font)(resources.GetObject("simpleLabelItem2.AppearanceItemCaption.Font")));
            this.simpleLabelItem2.AppearanceItemCaption.Options.UseFont = true;
            resources.ApplyResources(this.simpleLabelItem2, "simpleLabelItem2");
            this.simpleLabelItem2.Location = new System.Drawing.Point(960, 388);
            this.simpleLabelItem2.MaxSize = new System.Drawing.Size(17, 23);
            this.simpleLabelItem2.MinSize = new System.Drawing.Size(17, 23);
            this.simpleLabelItem2.Name = "simpleLabelItem2";
            this.simpleLabelItem2.Size = new System.Drawing.Size(17, 23);
            this.simpleLabelItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.simpleLabelItem2.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.simpleLabelItem2.TextSize = new System.Drawing.Size(9, 13);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.AppearanceItemCaption.Font = ((System.Drawing.Font)(resources.GetObject("layoutControlItem4.AppearanceItemCaption.Font")));
            this.layoutControlItem4.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem4.Control = this.glkpRegistrationType;
            resources.ApplyResources(this.layoutControlItem4, "layoutControlItem4");
            this.layoutControlItem4.Location = new System.Drawing.Point(643, 37);
            this.layoutControlItem4.MaxSize = new System.Drawing.Size(334, 32);
            this.layoutControlItem4.MinSize = new System.Drawing.Size(334, 32);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(334, 32);
            this.layoutControlItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem4.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 3, 3);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(101, 13);
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.btnApply;
            resources.ApplyResources(this.layoutControlItem7, "layoutControlItem7");
            this.layoutControlItem7.Location = new System.Drawing.Point(977, 37);
            this.layoutControlItem7.MaxSize = new System.Drawing.Size(61, 32);
            this.layoutControlItem7.MinSize = new System.Drawing.Size(61, 32);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(61, 32);
            this.layoutControlItem7.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem7.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 3, 3);
            this.layoutControlItem7.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem7.TextToControlDistance = 0;
            this.layoutControlItem7.TextVisible = false;
            this.layoutControlItem7.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem1, "emptySpaceItem1");
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 37);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(643, 32);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // frmProspectsView
            // 
            this.AcceptButton = this.btnApply;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmProspectsView";
            this.ShowFilterClicked += new System.EventHandler(this.frmProspectsView_ShowFilterClicked);
            this.EnterClicked += new System.EventHandler(this.frmProspectsView_EnterClicked);
            this.Activated += new System.EventHandler(this.frmProspectsView_Activated);
            this.Load += new System.EventHandler(this.frmProspectsView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.glkpRegistrationType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowFilter.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcProspects)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvProspects)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblRowCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraGrid.GridControl gcProspects;
        private DevExpress.XtraGrid.Views.Grid.GridView gvProspects;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private Bosco.Utility.Controls.ucToolBar ucProspectsToolbar;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraGrid.Columns.GridColumn colProspectId;
        private DevExpress.XtraGrid.Columns.GridColumn colName;
        private DevExpress.XtraGrid.Columns.GridColumn colAddress;
        private DevExpress.XtraGrid.Columns.GridColumn colPlace;
        private DevExpress.XtraGrid.Columns.GridColumn colState;
        private DevExpress.XtraGrid.Columns.GridColumn colCountry;
        private DevExpress.XtraGrid.Columns.GridColumn colPhone;
        private DevExpress.XtraEditors.CheckEdit chkShowFilter;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.SimpleLabelItem lblRowCount;
        private DevExpress.XtraLayout.SimpleLabelItem simpleLabelItem2;
        private DevExpress.XtraEditors.SimpleButton btnApply;
        private DevExpress.XtraEditors.GridLookUpEdit glkpRegistrationType;
        private DevExpress.XtraGrid.Views.Grid.GridView gridLookUpEdit1View;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraGrid.Columns.GridColumn colRegistrationTypeId;
        private DevExpress.XtraGrid.Columns.GridColumn colRegistrationType;
        private DevExpress.XtraGrid.Columns.GridColumn gccolRegistrationType;
        private DevExpress.XtraGrid.Columns.GridColumn colTelephone;
        private DevExpress.XtraGrid.Columns.GridColumn colReferedStaff;
        private DevExpress.XtraGrid.Columns.GridColumn colRegNo;
    }
}