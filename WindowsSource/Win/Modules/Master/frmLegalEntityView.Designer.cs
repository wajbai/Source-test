namespace ACPP.Modules.Master
{
    partial class frmLegalEntityView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLegalEntityView));
            DevExpress.Utils.SuperToolTip superToolTip1 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem1 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem1 = new DevExpress.Utils.ToolTipItem();
            DevExpress.Utils.SuperToolTip superToolTip2 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem2 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem2 = new DevExpress.Utils.ToolTipItem();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.lblRowCountCaption = new DevExpress.XtraEditors.LabelControl();
            this.lblRowCount = new DevExpress.XtraEditors.LabelControl();
            this.ucToolBarBankView = new Bosco.Utility.Controls.ucToolBar();
            this.chkShowFilter = new DevExpress.XtraEditors.CheckEdit();
            this.gcLegalEntity = new DevExpress.XtraGrid.GridControl();
            this.gvLegalEntity = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gvColCustomerId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gvColInstituteName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gvColSocietyName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gvColRegNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rdtRgdate = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            this.gvColRegDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gvColPermissionNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gvColPermissionDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gvContactPerson = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gvColAddress = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gvColSate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gvColCountry = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gvColPincode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gvColPhone = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gvColFax = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gvColemail = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gvColURL = new DevExpress.XtraGrid.Columns.GridColumn();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowFilter.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcLegalEntity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvLegalEntity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdtRgdate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdtRgdate.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.lblRowCountCaption);
            this.layoutControl1.Controls.Add(this.lblRowCount);
            this.layoutControl1.Controls.Add(this.ucToolBarBankView);
            this.layoutControl1.Controls.Add(this.chkShowFilter);
            this.layoutControl1.Controls.Add(this.gcLegalEntity);
            resources.ApplyResources(this.layoutControl1, "layoutControl1");
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(847, 144, 250, 350);
            this.layoutControl1.Root = this.layoutControlGroup1;
            // 
            // lblRowCountCaption
            // 
            this.lblRowCountCaption.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("lblRowCountCaption.Appearance.Font")));
            resources.ApplyResources(this.lblRowCountCaption, "lblRowCountCaption");
            this.lblRowCountCaption.Name = "lblRowCountCaption";
            this.lblRowCountCaption.StyleController = this.layoutControl1;
            // 
            // lblRowCount
            // 
            this.lblRowCount.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("lblRowCount.Appearance.Font")));
            resources.ApplyResources(this.lblRowCount, "lblRowCount");
            this.lblRowCount.Name = "lblRowCount";
            this.lblRowCount.StyleController = this.layoutControl1;
            // 
            // ucToolBarBankView
            // 
            this.ucToolBarBankView.ChangeAddCaption = "&Add";
            this.ucToolBarBankView.ChangeCaption = "&Edit";
            this.ucToolBarBankView.ChangeDeleteCaption = "&Delete";
            this.ucToolBarBankView.ChangeMoveVoucherCaption = "&Move Voucher";
            resources.ApplyResources(toolTipTitleItem1, "toolTipTitleItem1");
            toolTipItem1.LeftIndent = 6;
            resources.ApplyResources(toolTipItem1, "toolTipItem1");
            superToolTip1.Items.Add(toolTipTitleItem1);
            superToolTip1.Items.Add(toolTipItem1);
            this.ucToolBarBankView.ChangeMoveVoucherTooltip = superToolTip1;
            this.ucToolBarBankView.ChangeNatureOfPaymentCaption = "&Nature of Payments";
            this.ucToolBarBankView.ChangePostInterestCaption = "P&ost Interest";
            toolTipTitleItem2.Appearance.Image = ((System.Drawing.Image)(resources.GetObject("resource.Image")));
            toolTipTitleItem2.Appearance.Options.UseImage = true;
            resources.ApplyResources(toolTipTitleItem2, "toolTipTitleItem2");
            toolTipItem2.LeftIndent = 6;
            resources.ApplyResources(toolTipItem2, "toolTipItem2");
            superToolTip2.Items.Add(toolTipTitleItem2);
            superToolTip2.Items.Add(toolTipItem2);
            this.ucToolBarBankView.ChangePostInterestSuperToolTip = superToolTip2;
            this.ucToolBarBankView.ChangePrintCaption = "&Print";
            this.ucToolBarBankView.ChnageRenewCaption = "Re<u>n</u>ew";
            this.ucToolBarBankView.DisableAddButton = true;
            this.ucToolBarBankView.DisableAMCRenew = true;
            this.ucToolBarBankView.DisableCloseButton = true;
            this.ucToolBarBankView.DisableDeleteButton = true;
            this.ucToolBarBankView.DisableDownloadExcel = true;
            this.ucToolBarBankView.DisableEditButton = true;
            this.ucToolBarBankView.DisableInsertVoucher = true;
            this.ucToolBarBankView.DisableMoveTransaction = true;
            this.ucToolBarBankView.DisableNatureofPayments = true;
            this.ucToolBarBankView.DisablePostInterest = true;
            this.ucToolBarBankView.DisablePrintButton = true;
            this.ucToolBarBankView.DisableRestoreVoucher = true;
            resources.ApplyResources(this.ucToolBarBankView, "ucToolBarBankView");
            this.ucToolBarBankView.Name = "ucToolBarBankView";
            this.ucToolBarBankView.ShowHTML = true;
            this.ucToolBarBankView.ShowMMT = true;
            this.ucToolBarBankView.ShowPDF = true;
            this.ucToolBarBankView.ShowRTF = true;
            this.ucToolBarBankView.ShowText = true;
            this.ucToolBarBankView.ShowXLS = true;
            this.ucToolBarBankView.ShowXLSX = true;
            this.ucToolBarBankView.VisbleInsertVoucher = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarBankView.VisibleAddButton = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarBankView.VisibleClose = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucToolBarBankView.VisibleDeleteButton = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucToolBarBankView.VisibleDownloadExcel = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarBankView.VisibleEditButton = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucToolBarBankView.VisibleMoveTrans = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarBankView.VisibleNatureofPayments = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarBankView.VisibleNegativeBalance = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarBankView.VisiblePostInterest = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarBankView.VisiblePrintButton = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarBankView.VisibleRefresh = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucToolBarBankView.VisibleRenew = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarBankView.VisibleRestoreVoucher = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarBankView.AddClicked += new System.EventHandler(this.UcLegalEntity_AddClicked);
            this.ucToolBarBankView.EditClicked += new System.EventHandler(this.UcLegalEntity_EditClicked);
            this.ucToolBarBankView.DeleteClicked += new System.EventHandler(this.UcLegalEntity_DeleteClicked);
            this.ucToolBarBankView.PrintClicked += new System.EventHandler(this.UcLegalEntity_PrintClicked);
            this.ucToolBarBankView.CloseClicked += new System.EventHandler(this.ucToolBarBankView_CloseClicked);
            this.ucToolBarBankView.RefreshClicked += new System.EventHandler(this.UcLegalEntity_RefreshClicked);
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
            // gcLegalEntity
            // 
            resources.ApplyResources(this.gcLegalEntity, "gcLegalEntity");
            this.gcLegalEntity.MainView = this.gvLegalEntity;
            this.gcLegalEntity.Name = "gcLegalEntity";
            this.gcLegalEntity.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rdtRgdate});
            this.gcLegalEntity.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvLegalEntity});
            // 
            // gvLegalEntity
            // 
            this.gvLegalEntity.Appearance.FocusedRow.Font = ((System.Drawing.Font)(resources.GetObject("gvLegalEntity.Appearance.FocusedRow.Font")));
            this.gvLegalEntity.Appearance.FocusedRow.Options.UseFont = true;
            this.gvLegalEntity.Appearance.HeaderPanel.Font = ((System.Drawing.Font)(resources.GetObject("gvLegalEntity.Appearance.HeaderPanel.Font")));
            this.gvLegalEntity.Appearance.HeaderPanel.Options.UseFont = true;
            this.gvLegalEntity.AppearancePrint.HeaderPanel.Font = ((System.Drawing.Font)(resources.GetObject("gvLegalEntity.AppearancePrint.HeaderPanel.Font")));
            this.gvLegalEntity.AppearancePrint.HeaderPanel.Options.UseFont = true;
            this.gvLegalEntity.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.gvLegalEntity.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gvColCustomerId,
            this.gvColInstituteName,
            this.gvColSocietyName,
            this.gvColRegNo,
            this.gvColRegDate,
            this.gvColPermissionNo,
            this.gvColPermissionDate,
            this.gvContactPerson,
            this.gvColAddress,
            this.gvColSate,
            this.gvColCountry,
            this.gvColPincode,
            this.gvColPhone,
            this.gvColFax,
            this.gvColemail,
            this.gvColURL});
            this.gvLegalEntity.GridControl = this.gcLegalEntity;
            this.gvLegalEntity.Name = "gvLegalEntity";
            this.gvLegalEntity.OptionsBehavior.Editable = false;
            this.gvLegalEntity.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvLegalEntity.OptionsView.ShowGroupPanel = false;
            this.gvLegalEntity.DoubleClick += new System.EventHandler(this.gvLegalEntity_DoubleClick);
            // 
            // gvColCustomerId
            // 
            resources.ApplyResources(this.gvColCustomerId, "gvColCustomerId");
            this.gvColCustomerId.FieldName = "CUSTOMERID";
            this.gvColCustomerId.Name = "gvColCustomerId";
            // 
            // gvColInstituteName
            // 
            resources.ApplyResources(this.gvColInstituteName, "gvColInstituteName");
            this.gvColInstituteName.FieldName = "INSTITUTENAME";
            this.gvColInstituteName.Name = "gvColInstituteName";
            this.gvColInstituteName.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // gvColSocietyName
            // 
            resources.ApplyResources(this.gvColSocietyName, "gvColSocietyName");
            this.gvColSocietyName.FieldName = "SOCIETYNAME";
            this.gvColSocietyName.Name = "gvColSocietyName";
            this.gvColSocietyName.OptionsColumn.FixedWidth = true;
            this.gvColSocietyName.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // gvColRegNo
            // 
            resources.ApplyResources(this.gvColRegNo, "gvColRegNo");
            this.gvColRegNo.ColumnEdit = this.rdtRgdate;
            this.gvColRegNo.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gvColRegNo.FieldName = "REGNO";
            this.gvColRegNo.Name = "gvColRegNo";
            this.gvColRegNo.OptionsColumn.FixedWidth = true;
            this.gvColRegNo.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // rdtRgdate
            // 
            this.rdtRgdate.AllowNullInput = DevExpress.Utils.DefaultBoolean.True;
            resources.ApplyResources(this.rdtRgdate, "rdtRgdate");
            this.rdtRgdate.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("rdtRgdate.Buttons"))))});
            this.rdtRgdate.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("rdtRgdate.CalendarTimeProperties.Buttons"))))});
            this.rdtRgdate.Name = "rdtRgdate";
            // 
            // gvColRegDate
            // 
            resources.ApplyResources(this.gvColRegDate, "gvColRegDate");
            this.gvColRegDate.ColumnEdit = this.rdtRgdate;
            this.gvColRegDate.DisplayFormat.FormatString = "d";
            this.gvColRegDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.gvColRegDate.FieldName = "REGDATE";
            this.gvColRegDate.Name = "gvColRegDate";
            this.gvColRegDate.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gvColRegDate.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // gvColPermissionNo
            // 
            resources.ApplyResources(this.gvColPermissionNo, "gvColPermissionNo");
            this.gvColPermissionNo.FieldName = "PERMISSIONNO";
            this.gvColPermissionNo.Name = "gvColPermissionNo";
            // 
            // gvColPermissionDate
            // 
            resources.ApplyResources(this.gvColPermissionDate, "gvColPermissionDate");
            this.gvColPermissionDate.FieldName = "PERMISSIONDATE";
            this.gvColPermissionDate.Name = "gvColPermissionDate";
            // 
            // gvContactPerson
            // 
            resources.ApplyResources(this.gvContactPerson, "gvContactPerson");
            this.gvContactPerson.FieldName = "CONTACTPERSON";
            this.gvContactPerson.Name = "gvContactPerson";
            // 
            // gvColAddress
            // 
            this.gvColAddress.AppearanceCell.Options.UseTextOptions = true;
            this.gvColAddress.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            resources.ApplyResources(this.gvColAddress, "gvColAddress");
            this.gvColAddress.FieldName = "ADDRESS";
            this.gvColAddress.Name = "gvColAddress";
            // 
            // gvColSate
            // 
            resources.ApplyResources(this.gvColSate, "gvColSate");
            this.gvColSate.FieldName = "STATE_NAME";
            this.gvColSate.Name = "gvColSate";
            // 
            // gvColCountry
            // 
            resources.ApplyResources(this.gvColCountry, "gvColCountry");
            this.gvColCountry.FieldName = "COUNTRY";
            this.gvColCountry.Name = "gvColCountry";
            // 
            // gvColPincode
            // 
            resources.ApplyResources(this.gvColPincode, "gvColPincode");
            this.gvColPincode.FieldName = "PINCODE";
            this.gvColPincode.Name = "gvColPincode";
            // 
            // gvColPhone
            // 
            resources.ApplyResources(this.gvColPhone, "gvColPhone");
            this.gvColPhone.FieldName = "PHONE";
            this.gvColPhone.Name = "gvColPhone";
            // 
            // gvColFax
            // 
            resources.ApplyResources(this.gvColFax, "gvColFax");
            this.gvColFax.FieldName = "FAX";
            this.gvColFax.Name = "gvColFax";
            // 
            // gvColemail
            // 
            resources.ApplyResources(this.gvColemail, "gvColemail");
            this.gvColemail.FieldName = "EMAIL";
            this.gvColemail.Name = "gvColemail";
            // 
            // gvColURL
            // 
            resources.ApplyResources(this.gvColURL, "gvColURL");
            this.gvColURL.FieldName = "URL";
            this.gvColURL.Name = "gvColURL";
            // 
            // layoutControlGroup1
            // 
            resources.ApplyResources(this.layoutControlGroup1, "layoutControlGroup1");
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.layoutControlItem1,
            this.layoutControlItem4,
            this.layoutControlItem5});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 2, 2);
            this.layoutControlGroup1.Size = new System.Drawing.Size(957, 456);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.gcLegalEntity;
            resources.ApplyResources(this.layoutControlItem2, "layoutControlItem2");
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 34);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlItem2.Size = new System.Drawing.Size(953, 395);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.chkShowFilter;
            resources.ApplyResources(this.layoutControlItem3, "layoutControlItem3");
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 429);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(933, 23);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextToControlDistance = 0;
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.ucToolBarBankView;
            resources.ApplyResources(this.layoutControlItem1, "layoutControlItem1");
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.MaxSize = new System.Drawing.Size(0, 34);
            this.layoutControlItem1.MinSize = new System.Drawing.Size(104, 34);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(953, 34);
            this.layoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.lblRowCount;
            resources.ApplyResources(this.layoutControlItem4, "layoutControlItem4");
            this.layoutControlItem4.Location = new System.Drawing.Point(946, 429);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(7, 23);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextToControlDistance = 0;
            this.layoutControlItem4.TextVisible = false;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.lblRowCountCaption;
            resources.ApplyResources(this.layoutControlItem5, "layoutControlItem5");
            this.layoutControlItem5.Location = new System.Drawing.Point(933, 429);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(13, 23);
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextToControlDistance = 0;
            this.layoutControlItem5.TextVisible = false;
            // 
            // frmLegalEntityView
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmLegalEntityView";
            this.ShowFilterClicked += new System.EventHandler(this.frmLegalEntityView_ShowFilterClicked);
            this.EnterClicked += new System.EventHandler(this.frmLegalEntityView_EnterClicked);
            this.Activated += new System.EventHandler(this.frmLegalEntityView_Activated);
            this.Load += new System.EventHandler(this.frmLegalEntityView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chkShowFilter.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcLegalEntity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvLegalEntity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdtRgdate.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdtRgdate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraGrid.GridControl gcLegalEntity;
        private DevExpress.XtraGrid.Views.Grid.GridView gvLegalEntity;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraEditors.CheckEdit chkShowFilter;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraGrid.Columns.GridColumn gvColInstituteName;
        private DevExpress.XtraGrid.Columns.GridColumn gvColSocietyName;
        private DevExpress.XtraGrid.Columns.GridColumn gvColRegNo;
        private DevExpress.XtraGrid.Columns.GridColumn gvColRegDate;
        private DevExpress.XtraGrid.Columns.GridColumn gvColCustomerId;
        private Bosco.Utility.Controls.ucToolBar ucToolBarBankView;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraEditors.LabelControl lblRowCount;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraEditors.LabelControl lblRowCountCaption;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit rdtRgdate;
        private DevExpress.XtraGrid.Columns.GridColumn gvColPermissionNo;
        private DevExpress.XtraGrid.Columns.GridColumn gvColPermissionDate;
        private DevExpress.XtraGrid.Columns.GridColumn gvContactPerson;
        private DevExpress.XtraGrid.Columns.GridColumn gvColAddress;
        private DevExpress.XtraGrid.Columns.GridColumn gvColSate;
        private DevExpress.XtraGrid.Columns.GridColumn gvColCountry;
        private DevExpress.XtraGrid.Columns.GridColumn gvColPincode;
        private DevExpress.XtraGrid.Columns.GridColumn gvColPhone;
        private DevExpress.XtraGrid.Columns.GridColumn gvColFax;
        private DevExpress.XtraGrid.Columns.GridColumn gvColemail;
        private DevExpress.XtraGrid.Columns.GridColumn gvColURL;
    }
}