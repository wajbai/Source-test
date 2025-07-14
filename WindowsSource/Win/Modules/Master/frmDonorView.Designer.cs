namespace ACPP.Modules.Master
{
    partial class frmDonorView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDonorView));
            this.ucDonorView = new Bosco.Utility.Controls.ucToolBar();
            this.gcDonorDetails = new DevExpress.XtraGrid.GridControl();
            this.gvDonorDetails = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colDonAudId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDonorName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRegtype = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAddress = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPlace = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colcompanyName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colState = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCountry = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEmail = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPhone = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colReferredStaff = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gccolDOJ = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gccolDOE = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colStatus = new DevExpress.XtraGrid.Columns.GridColumn();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.btnApply = new DevExpress.XtraEditors.SimpleButton();
            this.chkInactive = new DevExpress.XtraEditors.CheckEdit();
            this.chkActive = new DevExpress.XtraEditors.CheckEdit();
            this.glkpRegistrationType = new DevExpress.XtraEditors.GridLookUpEdit();
            this.gridLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colRegTypeId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRegistrationType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.chkShowFilter = new DevExpress.XtraEditors.CheckEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblCount = new DevExpress.XtraLayout.SimpleLabelItem();
            this.simpleLabelItem2 = new DevExpress.XtraLayout.SimpleLabelItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem3 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem4 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.colRegNo = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gcDonorDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvDonorDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkInactive.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkActive.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.glkpRegistrationType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowFilter.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            this.SuspendLayout();
            // 
            // ucDonorView
            // 
            this.ucDonorView.ChangeAddCaption = "&Add";
            this.ucDonorView.ChangeCaption = "&Edit";
            this.ucDonorView.ChangeDeleteCaption = "&Delete";
            this.ucDonorView.ChangeNatureOfPaymentCaption = "&Nature of Payments";
            this.ucDonorView.ChangePostInterestCaption = "P&ost Interest";
            this.ucDonorView.ChangePrintCaption = "&Print";
            this.ucDonorView.ChnageRenewCaption = "Re<u>n</u>ew";
            this.ucDonorView.DisableAddButton = true;
            this.ucDonorView.DisableAMCRenew = true;
            this.ucDonorView.DisableCloseButton = true;
            this.ucDonorView.DisableDeleteButton = true;
            this.ucDonorView.DisableDownloadExcel = true;
            this.ucDonorView.DisableEditButton = true;
            this.ucDonorView.DisableMoveTransaction = true;
            this.ucDonorView.DisableNatureofPayments = true;
            this.ucDonorView.DisablePostInterest = true;
            this.ucDonorView.DisablePrintButton = true;
            this.ucDonorView.DisableRestoreVoucher = true;
            resources.ApplyResources(this.ucDonorView, "ucDonorView");
            this.ucDonorView.Name = "ucDonorView";
            this.ucDonorView.ShowHTML = true;
            this.ucDonorView.ShowMMT = true;
            this.ucDonorView.ShowPDF = true;
            this.ucDonorView.ShowRTF = true;
            this.ucDonorView.ShowText = true;
            this.ucDonorView.ShowXLS = true;
            this.ucDonorView.ShowXLSX = true;
            this.ucDonorView.VisbleInsertVoucher = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucDonorView.VisibleAddButton = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucDonorView.VisibleClose = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucDonorView.VisibleDeleteButton = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucDonorView.VisibleDownloadExcel = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucDonorView.VisibleEditButton = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucDonorView.VisibleMoveTrans = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucDonorView.VisibleNatureofPayments = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucDonorView.VisibleNegativeBalance = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucDonorView.VisiblePostInterest = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucDonorView.VisiblePrintButton = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucDonorView.VisibleRefresh = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucDonorView.VisibleRenew = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucDonorView.VisibleRestoreVoucher = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucDonorView.AddClicked += new System.EventHandler(this.ucDonorView_AddClicked);
            this.ucDonorView.EditClicked += new System.EventHandler(this.ucDonorView_EditClicked);
            this.ucDonorView.DeleteClicked += new System.EventHandler(this.ucDonorView_DeleteClicked);
            this.ucDonorView.PrintClicked += new System.EventHandler(this.ucDonorView_PrintClicked);
            this.ucDonorView.CloseClicked += new System.EventHandler(this.ucDonorView_CloseClicked);
            this.ucDonorView.RefreshClicked += new System.EventHandler(this.ucDonorView_RefreshClicked);
            this.ucDonorView.DownloadExcel += new System.EventHandler(this.ucDonorView_DownloadExcel);
            // 
            // gcDonorDetails
            // 
            resources.ApplyResources(this.gcDonorDetails, "gcDonorDetails");
            this.gcDonorDetails.MainView = this.gvDonorDetails;
            this.gcDonorDetails.Name = "gcDonorDetails";
            this.gcDonorDetails.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvDonorDetails});
            this.gcDonorDetails.Click += new System.EventHandler(this.gcDonorDetails_Click);
            // 
            // gvDonorDetails
            // 
            this.gvDonorDetails.Appearance.FocusedRow.Font = ((System.Drawing.Font)(resources.GetObject("gvDonorDetails.Appearance.FocusedRow.Font")));
            this.gvDonorDetails.Appearance.FocusedRow.Options.UseFont = true;
            this.gvDonorDetails.Appearance.HeaderPanel.Font = ((System.Drawing.Font)(resources.GetObject("gvDonorDetails.Appearance.HeaderPanel.Font")));
            this.gvDonorDetails.Appearance.HeaderPanel.Options.UseFont = true;
            this.gvDonorDetails.AppearancePrint.HeaderPanel.Font = ((System.Drawing.Font)(resources.GetObject("gvDonorDetails.AppearancePrint.HeaderPanel.Font")));
            this.gvDonorDetails.AppearancePrint.HeaderPanel.Options.UseFont = true;
            this.gvDonorDetails.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.gvDonorDetails.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colDonAudId,
            this.colDonorName,
            this.colRegNo,
            this.colRegtype,
            this.colAddress,
            this.colPlace,
            this.colcompanyName,
            this.colState,
            this.colCountry,
            this.colEmail,
            this.colPhone,
            this.colReferredStaff,
            this.gccolDOJ,
            this.gccolDOE,
            this.colStatus});
            this.gvDonorDetails.GridControl = this.gcDonorDetails;
            this.gvDonorDetails.Name = "gvDonorDetails";
            this.gvDonorDetails.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.gvDonorDetails.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            this.gvDonorDetails.OptionsBehavior.Editable = false;
            this.gvDonorDetails.OptionsCustomization.AllowGroup = false;
            this.gvDonorDetails.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvDonorDetails.OptionsView.ShowGroupPanel = false;
            this.gvDonorDetails.DoubleClick += new System.EventHandler(this.gvDonorDetails_DoubleClick);
            this.gvDonorDetails.RowCountChanged += new System.EventHandler(this.gvDonorDetails_RowCountChanged);
            // 
            // colDonAudId
            // 
            this.colDonAudId.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colDonAudId.AppearanceHeader.Font")));
            this.colDonAudId.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colDonAudId, "colDonAudId");
            this.colDonAudId.FieldName = "DONAUD_ID";
            this.colDonAudId.Name = "colDonAudId";
            // 
            // colDonorName
            // 
            this.colDonorName.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colDonorName.AppearanceHeader.Font")));
            this.colDonorName.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colDonorName, "colDonorName");
            this.colDonorName.FieldName = "NAME";
            this.colDonorName.Name = "colDonorName";
            this.colDonorName.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colRegtype
            // 
            resources.ApplyResources(this.colRegtype, "colRegtype");
            this.colRegtype.FieldName = "REGISTRATION_TYPE";
            this.colRegtype.Name = "colRegtype";
            this.colRegtype.OptionsColumn.FixedWidth = true;
            // 
            // colAddress
            // 
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
            this.colPlace.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colcompanyName
            // 
            resources.ApplyResources(this.colcompanyName, "colcompanyName");
            this.colcompanyName.FieldName = "COMPANY_NAME";
            this.colcompanyName.Name = "colcompanyName";
            this.colcompanyName.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colState
            // 
            this.colState.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colState.AppearanceHeader.Font")));
            this.colState.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colState, "colState");
            this.colState.FieldName = "STATE_NAME";
            this.colState.Name = "colState";
            this.colState.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colCountry
            // 
            this.colCountry.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colCountry.AppearanceHeader.Font")));
            this.colCountry.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colCountry, "colCountry");
            this.colCountry.FieldName = "COUNTRY";
            this.colCountry.Name = "colCountry";
            this.colCountry.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colEmail
            // 
            resources.ApplyResources(this.colEmail, "colEmail");
            this.colEmail.FieldName = "EMAIL";
            this.colEmail.Name = "colEmail";
            // 
            // colPhone
            // 
            this.colPhone.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colPhone.AppearanceHeader.Font")));
            this.colPhone.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colPhone, "colPhone");
            this.colPhone.FieldName = "PHONE";
            this.colPhone.Name = "colPhone";
            this.colPhone.OptionsColumn.FixedWidth = true;
            this.colPhone.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colReferredStaff
            // 
            resources.ApplyResources(this.colReferredStaff, "colReferredStaff");
            this.colReferredStaff.FieldName = "REFERRED_STAFF";
            this.colReferredStaff.Name = "colReferredStaff";
            // 
            // gccolDOJ
            // 
            resources.ApplyResources(this.gccolDOJ, "gccolDOJ");
            this.gccolDOJ.FieldName = "DATE_OF_JOIN";
            this.gccolDOJ.Name = "gccolDOJ";
            // 
            // gccolDOE
            // 
            resources.ApplyResources(this.gccolDOE, "gccolDOE");
            this.gccolDOE.FieldName = "DATE_OF_EXIT";
            this.gccolDOE.Name = "gccolDOE";
            // 
            // colStatus
            // 
            resources.ApplyResources(this.colStatus, "colStatus");
            this.colStatus.FieldName = "STATUS";
            this.colStatus.Name = "colStatus";
            this.colStatus.OptionsColumn.FixedWidth = true;
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.btnApply);
            this.layoutControl1.Controls.Add(this.chkInactive);
            this.layoutControl1.Controls.Add(this.chkActive);
            this.layoutControl1.Controls.Add(this.glkpRegistrationType);
            this.layoutControl1.Controls.Add(this.chkShowFilter);
            this.layoutControl1.Controls.Add(this.gcDonorDetails);
            this.layoutControl1.Controls.Add(this.ucDonorView);
            resources.ApplyResources(this.layoutControl1, "layoutControl1");
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(181, 263, 250, 350);
            this.layoutControl1.Root = this.layoutControlGroup1;
            // 
            // btnApply
            // 
            resources.ApplyResources(this.btnApply, "btnApply");
            this.btnApply.Name = "btnApply";
            this.btnApply.StyleController = this.layoutControl1;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // chkInactive
            // 
            resources.ApplyResources(this.chkInactive, "chkInactive");
            this.chkInactive.Name = "chkInactive";
            this.chkInactive.Properties.Caption = resources.GetString("chkInactive.Properties.Caption");
            this.chkInactive.StyleController = this.layoutControl1;
            this.chkInactive.CheckedChanged += new System.EventHandler(this.chkInactive_CheckedChanged);
            // 
            // chkActive
            // 
            resources.ApplyResources(this.chkActive, "chkActive");
            this.chkActive.Name = "chkActive";
            this.chkActive.Properties.Caption = resources.GetString("chkActive.Properties.Caption");
            this.chkActive.StyleController = this.layoutControl1;
            this.chkActive.CheckedChanged += new System.EventHandler(this.chkActive_CheckedChanged);
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
            this.glkpRegistrationType.Properties.PopupFormSize = new System.Drawing.Size(290, 0);
            this.glkpRegistrationType.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.glkpRegistrationType.Properties.View = this.gridLookUpEdit1View;
            this.glkpRegistrationType.StyleController = this.layoutControl1;
            // 
            // gridLookUpEdit1View
            // 
            this.gridLookUpEdit1View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colRegTypeId,
            this.colRegistrationType});
            this.gridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridLookUpEdit1View.Name = "gridLookUpEdit1View";
            this.gridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridLookUpEdit1View.OptionsView.ShowColumnHeaders = false;
            this.gridLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            this.gridLookUpEdit1View.OptionsView.ShowIndicator = false;
            // 
            // colRegTypeId
            // 
            resources.ApplyResources(this.colRegTypeId, "colRegTypeId");
            this.colRegTypeId.FieldName = "REGISTRATION_TYPE_ID";
            this.colRegTypeId.Name = "colRegTypeId";
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
            this.chkShowFilter.Properties.AllowHtmlDraw = DevExpress.Utils.DefaultBoolean.True;
            this.chkShowFilter.Properties.Caption = resources.GetString("chkShowFilter.Properties.Caption");
            this.chkShowFilter.StyleController = this.layoutControl1;
            this.chkShowFilter.CheckedChanged += new System.EventHandler(this.chkShowFilter_CheckedChanged);
            // 
            // layoutControlGroup1
            // 
            resources.ApplyResources(this.layoutControlGroup1, "layoutControlGroup1");
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.emptySpaceItem1,
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.lblCount,
            this.simpleLabelItem2,
            this.layoutControlItem4,
            this.layoutControlItem7,
            this.emptySpaceItem2,
            this.emptySpaceItem3,
            this.emptySpaceItem4,
            this.layoutControlGroup2});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(1010, 447);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem1, "emptySpaceItem1");
            this.emptySpaceItem1.Location = new System.Drawing.Point(79, 424);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(841, 23);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.ucDonorView;
            resources.ApplyResources(this.layoutControlItem1, "layoutControlItem1");
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.MaxSize = new System.Drawing.Size(0, 35);
            this.layoutControlItem1.MinSize = new System.Drawing.Size(201, 35);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(1010, 35);
            this.layoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.gcDonorDetails;
            resources.ApplyResources(this.layoutControlItem2, "layoutControlItem2");
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 67);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(1010, 357);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.chkShowFilter;
            resources.ApplyResources(this.layoutControlItem3, "layoutControlItem3");
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 424);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(79, 23);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextToControlDistance = 0;
            this.layoutControlItem3.TextVisible = false;
            // 
            // lblCount
            // 
            this.lblCount.AllowHotTrack = false;
            this.lblCount.AppearanceItemCaption.Font = ((System.Drawing.Font)(resources.GetObject("lblCount.AppearanceItemCaption.Font")));
            this.lblCount.AppearanceItemCaption.Options.UseFont = true;
            resources.ApplyResources(this.lblCount, "lblCount");
            this.lblCount.Location = new System.Drawing.Point(933, 424);
            this.lblCount.MaxSize = new System.Drawing.Size(77, 23);
            this.lblCount.MinSize = new System.Drawing.Size(77, 23);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(77, 23);
            this.lblCount.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lblCount.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.lblCount.TextSize = new System.Drawing.Size(7, 13);
            // 
            // simpleLabelItem2
            // 
            this.simpleLabelItem2.AllowHotTrack = false;
            this.simpleLabelItem2.AppearanceItemCaption.Font = ((System.Drawing.Font)(resources.GetObject("simpleLabelItem2.AppearanceItemCaption.Font")));
            this.simpleLabelItem2.AppearanceItemCaption.Options.UseFont = true;
            resources.ApplyResources(this.simpleLabelItem2, "simpleLabelItem2");
            this.simpleLabelItem2.Location = new System.Drawing.Point(920, 424);
            this.simpleLabelItem2.MaxSize = new System.Drawing.Size(13, 23);
            this.simpleLabelItem2.MinSize = new System.Drawing.Size(13, 23);
            this.simpleLabelItem2.Name = "simpleLabelItem2";
            this.simpleLabelItem2.Size = new System.Drawing.Size(13, 23);
            this.simpleLabelItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.simpleLabelItem2.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.simpleLabelItem2.TextSize = new System.Drawing.Size(9, 13);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.glkpRegistrationType;
            resources.ApplyResources(this.layoutControlItem4, "layoutControlItem4");
            this.layoutControlItem4.Location = new System.Drawing.Point(411, 35);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(383, 32);
            this.layoutControlItem4.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 3, 3);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(85, 13);
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.btnApply;
            resources.ApplyResources(this.layoutControlItem7, "layoutControlItem7");
            this.layoutControlItem7.Location = new System.Drawing.Point(947, 35);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(63, 32);
            this.layoutControlItem7.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 3, 3);
            this.layoutControlItem7.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem7.TextToControlDistance = 0;
            this.layoutControlItem7.TextVisible = false;
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem2, "emptySpaceItem2");
            this.emptySpaceItem2.Location = new System.Drawing.Point(0, 35);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(411, 32);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem3
            // 
            this.emptySpaceItem3.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem3, "emptySpaceItem3");
            this.emptySpaceItem3.Location = new System.Drawing.Point(794, 35);
            this.emptySpaceItem3.Name = "emptySpaceItem3";
            this.emptySpaceItem3.Size = new System.Drawing.Size(10, 32);
            this.emptySpaceItem3.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem4
            // 
            this.emptySpaceItem4.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem4, "emptySpaceItem4");
            this.emptySpaceItem4.Location = new System.Drawing.Point(937, 35);
            this.emptySpaceItem4.Name = "emptySpaceItem4";
            this.emptySpaceItem4.Size = new System.Drawing.Size(10, 32);
            this.emptySpaceItem4.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlGroup2
            // 
            resources.ApplyResources(this.layoutControlGroup2, "layoutControlGroup2");
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem6,
            this.layoutControlItem5});
            this.layoutControlGroup2.Location = new System.Drawing.Point(804, 35);
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup2.Size = new System.Drawing.Size(133, 32);
            this.layoutControlGroup2.TextVisible = false;
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.chkInactive;
            resources.ApplyResources(this.layoutControlItem6, "layoutControlItem6");
            this.layoutControlItem6.Location = new System.Drawing.Point(62, 0);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(65, 26);
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextToControlDistance = 0;
            this.layoutControlItem6.TextVisible = false;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.chkActive;
            resources.ApplyResources(this.layoutControlItem5, "layoutControlItem5");
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(62, 26);
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextToControlDistance = 0;
            this.layoutControlItem5.TextVisible = false;
            // 
            // colRegNo
            // 
            resources.ApplyResources(this.colRegNo, "colRegNo");
            this.colRegNo.FieldName = "REGISTER_NO";
            this.colRegNo.Name = "colRegNo";
            // 
            // frmDonorView
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmDonorView";
            this.ShowFilterClicked += new System.EventHandler(this.frmDonorView_ShowFilterClicked);
            this.EnterClicked += new System.EventHandler(this.frmDonorView_EnterClicked);
            this.Activated += new System.EventHandler(this.frmDonorView_Activated);
            this.Load += new System.EventHandler(this.frmDonorView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gcDonorDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvDonorDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chkInactive.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkActive.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.glkpRegistrationType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowFilter.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Bosco.Utility.Controls.ucToolBar ucDonorView;
        private DevExpress.XtraGrid.GridControl gcDonorDetails;
        private DevExpress.XtraGrid.Views.Grid.GridView gvDonorDetails;
        private DevExpress.XtraGrid.Columns.GridColumn colDonAudId;
        private DevExpress.XtraGrid.Columns.GridColumn colDonorName;
        private DevExpress.XtraGrid.Columns.GridColumn colPlace;
        private DevExpress.XtraGrid.Columns.GridColumn colState;
        private DevExpress.XtraGrid.Columns.GridColumn colCountry;
        private DevExpress.XtraGrid.Columns.GridColumn colPhone;
        private DevExpress.XtraGrid.Columns.GridColumn colcompanyName;
        private DevExpress.XtraGrid.Columns.GridColumn colAddress;
        private DevExpress.XtraGrid.Columns.GridColumn colRegtype;
        private DevExpress.XtraGrid.Columns.GridColumn colStatus;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraEditors.CheckEdit chkShowFilter;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.SimpleLabelItem lblCount;
        private DevExpress.XtraLayout.SimpleLabelItem simpleLabelItem2;
        private DevExpress.XtraEditors.SimpleButton btnApply;
        private DevExpress.XtraEditors.CheckEdit chkInactive;
        private DevExpress.XtraEditors.CheckEdit chkActive;
        private DevExpress.XtraEditors.GridLookUpEdit glkpRegistrationType;
        private DevExpress.XtraGrid.Views.Grid.GridView gridLookUpEdit1View;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem3;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem4;
        private DevExpress.XtraGrid.Columns.GridColumn colRegTypeId;
        private DevExpress.XtraGrid.Columns.GridColumn colRegistrationType;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraGrid.Columns.GridColumn colEmail;
        private DevExpress.XtraGrid.Columns.GridColumn colReferredStaff;
        private DevExpress.XtraGrid.Columns.GridColumn gccolDOJ;
        private DevExpress.XtraGrid.Columns.GridColumn gccolDOE;
        private DevExpress.XtraGrid.Columns.GridColumn colRegNo;
    }
}