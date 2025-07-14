namespace Bosco.Report.View
{
    partial class frmTDSChallan
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTDSChallan));
            this.lcTDSChallan = new DevExpress.XtraLayout.LayoutControl();
            this.dteDateTo = new DevExpress.XtraEditors.DateEdit();
            this.btnFetchTDS = new DevExpress.XtraEditors.SimpleButton();
            this.dteDateAson = new DevExpress.XtraEditors.DateEdit();
            this.btnOk = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.gcTDSChallan = new DevExpress.XtraGrid.GridControl();
            this.gvTDSChallan = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colVoucherId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLedgerID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colVoucherDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBankAccountNumber = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDateFrom = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDateTo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colChequeNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colChequeDDDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBankName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBRSCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colChallanNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colChallanDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colVoucherNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.glkpProject = new DevExpress.XtraEditors.GridLookUpEdit();
            this.gridLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colProjectId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProject = new DevExpress.XtraGrid.Columns.GridColumn();
            this.lcgTDSChallan = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lblProject = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.lblTDSGridView = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.lblDateAson = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem3 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem4 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.lblDateTo = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.lcTDSChallan)).BeginInit();
            this.lcTDSChallan.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dteDateTo.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteDateTo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteDateAson.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteDateAson.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcTDSChallan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvTDSChallan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.glkpProject.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgTDSChallan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblProject)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblTDSGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblDateAson)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblDateTo)).BeginInit();
            this.SuspendLayout();
            // 
            // lcTDSChallan
            // 
            this.lcTDSChallan.Controls.Add(this.dteDateTo);
            this.lcTDSChallan.Controls.Add(this.btnFetchTDS);
            this.lcTDSChallan.Controls.Add(this.dteDateAson);
            this.lcTDSChallan.Controls.Add(this.btnOk);
            this.lcTDSChallan.Controls.Add(this.btnCancel);
            this.lcTDSChallan.Controls.Add(this.gcTDSChallan);
            this.lcTDSChallan.Controls.Add(this.glkpProject);
            resources.ApplyResources(this.lcTDSChallan, "lcTDSChallan");
            this.lcTDSChallan.Name = "lcTDSChallan";
            this.lcTDSChallan.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(240, 281, 250, 350);
            this.lcTDSChallan.Root = this.lcgTDSChallan;
            // 
            // dteDateTo
            // 
            resources.ApplyResources(this.dteDateTo, "dteDateTo");
            this.dteDateTo.Name = "dteDateTo";
            this.dteDateTo.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.dteDateTo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("dteDateTo.Properties.Buttons"))))});
            this.dteDateTo.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("dteDateTo.Properties.CalendarTimeProperties.Buttons"))))});
            this.dteDateTo.Properties.Mask.MaskType = ((DevExpress.XtraEditors.Mask.MaskType)(resources.GetObject("dteDateTo.Properties.Mask.MaskType")));
            this.dteDateTo.StyleController = this.lcTDSChallan;
            // 
            // btnFetchTDS
            // 
            resources.ApplyResources(this.btnFetchTDS, "btnFetchTDS");
            this.btnFetchTDS.Name = "btnFetchTDS";
            this.btnFetchTDS.StyleController = this.lcTDSChallan;
            this.btnFetchTDS.Click += new System.EventHandler(this.btnFetchTDS_Click);
            // 
            // dteDateAson
            // 
            resources.ApplyResources(this.dteDateAson, "dteDateAson");
            this.dteDateAson.Name = "dteDateAson";
            this.dteDateAson.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.dteDateAson.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("dteDateAson.Properties.Buttons"))))});
            this.dteDateAson.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("dteDateAson.Properties.CalendarTimeProperties.Buttons"))))});
            this.dteDateAson.Properties.Mask.MaskType = ((DevExpress.XtraEditors.Mask.MaskType)(resources.GetObject("dteDateAson.Properties.Mask.MaskType")));
            this.dteDateAson.Properties.Mask.UseMaskAsDisplayFormat = ((bool)(resources.GetObject("dteDateAson.Properties.Mask.UseMaskAsDisplayFormat")));
            this.dteDateAson.StyleController = this.lcTDSChallan;
            // 
            // btnOk
            // 
            resources.ApplyResources(this.btnOk, "btnOk");
            this.btnOk.Name = "btnOk";
            this.btnOk.StyleController = this.lcTDSChallan;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.StyleController = this.lcTDSChallan;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // gcTDSChallan
            // 
            resources.ApplyResources(this.gcTDSChallan, "gcTDSChallan");
            this.gcTDSChallan.MainView = this.gvTDSChallan;
            this.gcTDSChallan.Name = "gcTDSChallan";
            this.gcTDSChallan.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvTDSChallan});
            // 
            // gvTDSChallan
            // 
            this.gvTDSChallan.Appearance.FocusedRow.Font = ((System.Drawing.Font)(resources.GetObject("gvTDSChallan.Appearance.FocusedRow.Font")));
            this.gvTDSChallan.Appearance.FocusedRow.Options.UseFont = true;
            this.gvTDSChallan.Appearance.HeaderPanel.Font = ((System.Drawing.Font)(resources.GetObject("gvTDSChallan.Appearance.HeaderPanel.Font")));
            this.gvTDSChallan.Appearance.HeaderPanel.Options.UseFont = true;
            this.gvTDSChallan.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colVoucherId,
            this.colLedgerID,
            this.colVoucherDate,
            this.colBankAccountNumber,
            this.colDateFrom,
            this.colDateTo,
            this.colChequeNo,
            this.colChequeDDDate,
            this.colBankName,
            this.colBRSCode,
            this.colChallanNo,
            this.colChallanDate,
            this.colVoucherNo,
            this.colAmount});
            this.gvTDSChallan.GridControl = this.gcTDSChallan;
            this.gvTDSChallan.Name = "gvTDSChallan";
            this.gvTDSChallan.OptionsBehavior.Editable = false;
            this.gvTDSChallan.OptionsSelection.CheckBoxSelectorColumnWidth = 25;
            this.gvTDSChallan.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvTDSChallan.OptionsSelection.MultiSelect = true;
            this.gvTDSChallan.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.gvTDSChallan.OptionsView.ShowGroupPanel = false;
            // 
            // colVoucherId
            // 
            resources.ApplyResources(this.colVoucherId, "colVoucherId");
            this.colVoucherId.FieldName = "VOUCHER_ID";
            this.colVoucherId.Name = "colVoucherId";
            // 
            // colLedgerID
            // 
            resources.ApplyResources(this.colLedgerID, "colLedgerID");
            this.colLedgerID.FieldName = "LEDGER_ID";
            this.colLedgerID.Name = "colLedgerID";
            // 
            // colVoucherDate
            // 
            resources.ApplyResources(this.colVoucherDate, "colVoucherDate");
            this.colVoucherDate.DisplayFormat.FormatString = "d";
            this.colVoucherDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colVoucherDate.FieldName = "VOUCHER_DATE";
            this.colVoucherDate.Name = "colVoucherDate";
            this.colVoucherDate.OptionsColumn.AllowFocus = false;
            // 
            // colBankAccountNumber
            // 
            resources.ApplyResources(this.colBankAccountNumber, "colBankAccountNumber");
            this.colBankAccountNumber.FieldName = "LEDGER_NAME";
            this.colBankAccountNumber.Name = "colBankAccountNumber";
            this.colBankAccountNumber.OptionsColumn.AllowFocus = false;
            // 
            // colDateFrom
            // 
            resources.ApplyResources(this.colDateFrom, "colDateFrom");
            this.colDateFrom.FieldName = "DATE_FROM";
            this.colDateFrom.Name = "colDateFrom";
            // 
            // colDateTo
            // 
            resources.ApplyResources(this.colDateTo, "colDateTo");
            this.colDateTo.FieldName = "DATE_TO";
            this.colDateTo.Name = "colDateTo";
            // 
            // colChequeNo
            // 
            resources.ApplyResources(this.colChequeNo, "colChequeNo");
            this.colChequeNo.FieldName = "CHEQUE_NO";
            this.colChequeNo.Name = "colChequeNo";
            this.colChequeNo.OptionsColumn.AllowFocus = false;
            // 
            // colChequeDDDate
            // 
            resources.ApplyResources(this.colChequeDDDate, "colChequeDDDate");
            this.colChequeDDDate.DisplayFormat.FormatString = "d";
            this.colChequeDDDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colChequeDDDate.FieldName = "MATERIALIZED_ON";
            this.colChequeDDDate.Name = "colChequeDDDate";
            this.colChequeDDDate.OptionsColumn.AllowFocus = false;
            // 
            // colBankName
            // 
            resources.ApplyResources(this.colBankName, "colBankName");
            this.colBankName.FieldName = "BANK_NAME";
            this.colBankName.Name = "colBankName";
            // 
            // colBRSCode
            // 
            resources.ApplyResources(this.colBRSCode, "colBRSCode");
            this.colBRSCode.FieldName = "BRS_CODE";
            this.colBRSCode.Name = "colBRSCode";
            // 
            // colChallanNo
            // 
            resources.ApplyResources(this.colChallanNo, "colChallanNo");
            this.colChallanNo.FieldName = "CHALLAN_NO";
            this.colChallanNo.Name = "colChallanNo";
            // 
            // colChallanDate
            // 
            resources.ApplyResources(this.colChallanDate, "colChallanDate");
            this.colChallanDate.FieldName = "CHALLAN_DATE";
            this.colChallanDate.Name = "colChallanDate";
            // 
            // colVoucherNo
            // 
            resources.ApplyResources(this.colVoucherNo, "colVoucherNo");
            this.colVoucherNo.FieldName = "VOUCHER_NO";
            this.colVoucherNo.Name = "colVoucherNo";
            this.colVoucherNo.OptionsColumn.AllowFocus = false;
            // 
            // colAmount
            // 
            resources.ApplyResources(this.colAmount, "colAmount");
            this.colAmount.DisplayFormat.FormatString = "N";
            this.colAmount.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colAmount.FieldName = "AMOUNT";
            this.colAmount.Name = "colAmount";
            this.colAmount.OptionsColumn.AllowFocus = false;
            // 
            // glkpProject
            // 
            resources.ApplyResources(this.glkpProject, "glkpProject");
            this.glkpProject.Name = "glkpProject";
            this.glkpProject.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.glkpProject.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("glkpProject.Properties.Buttons"))))});
            this.glkpProject.Properties.ImmediatePopup = true;
            this.glkpProject.Properties.NullText = resources.GetString("glkpProject.Properties.NullText");
            this.glkpProject.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            this.glkpProject.Properties.PopupFormMinSize = new System.Drawing.Size(404, 0);
            this.glkpProject.Properties.PopupFormSize = new System.Drawing.Size(404, 0);
            this.glkpProject.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.glkpProject.Properties.View = this.gridLookUpEdit1View;
            this.glkpProject.StyleController = this.lcTDSChallan;
            this.glkpProject.EditValueChanged += new System.EventHandler(this.glkpProject_EditValueChanged);
            // 
            // gridLookUpEdit1View
            // 
            this.gridLookUpEdit1View.Appearance.FocusedRow.Font = ((System.Drawing.Font)(resources.GetObject("gridLookUpEdit1View.Appearance.FocusedRow.Font")));
            this.gridLookUpEdit1View.Appearance.FocusedRow.Options.UseFont = true;
            this.gridLookUpEdit1View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colProjectId,
            this.colProject});
            this.gridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridLookUpEdit1View.Name = "gridLookUpEdit1View";
            this.gridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridLookUpEdit1View.OptionsView.ShowColumnHeaders = false;
            this.gridLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            this.gridLookUpEdit1View.OptionsView.ShowIndicator = false;
            // 
            // colProjectId
            // 
            resources.ApplyResources(this.colProjectId, "colProjectId");
            this.colProjectId.FieldName = "PROJECT_ID";
            this.colProjectId.Name = "colProjectId";
            // 
            // colProject
            // 
            resources.ApplyResources(this.colProject, "colProject");
            this.colProject.FieldName = "PROJECT";
            this.colProject.Name = "colProject";
            this.colProject.OptionsColumn.ShowCaption = false;
            // 
            // lcgTDSChallan
            // 
            resources.ApplyResources(this.lcgTDSChallan, "lcgTDSChallan");
            this.lcgTDSChallan.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.lcgTDSChallan.GroupBordersVisible = false;
            this.lcgTDSChallan.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lblProject,
            this.emptySpaceItem1,
            this.lblTDSGridView,
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.emptySpaceItem2,
            this.lblDateAson,
            this.emptySpaceItem3,
            this.layoutControlItem3,
            this.emptySpaceItem4,
            this.lblDateTo});
            this.lcgTDSChallan.Location = new System.Drawing.Point(0, 0);
            this.lcgTDSChallan.Name = "Root";
            this.lcgTDSChallan.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.lcgTDSChallan.Size = new System.Drawing.Size(930, 465);
            this.lcgTDSChallan.TextVisible = false;
            // 
            // lblProject
            // 
            this.lblProject.AllowHtmlStringInCaption = true;
            this.lblProject.Control = this.glkpProject;
            resources.ApplyResources(this.lblProject, "lblProject");
            this.lblProject.Location = new System.Drawing.Point(0, 0);
            this.lblProject.Name = "lblProject";
            this.lblProject.Size = new System.Drawing.Size(474, 26);
            this.lblProject.TextSize = new System.Drawing.Size(60, 13);
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem1, "emptySpaceItem1");
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 429);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(782, 26);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // lblTDSGridView
            // 
            this.lblTDSGridView.Control = this.gcTDSChallan;
            resources.ApplyResources(this.lblTDSGridView, "lblTDSGridView");
            this.lblTDSGridView.Location = new System.Drawing.Point(0, 26);
            this.lblTDSGridView.Name = "lblTDSGridView";
            this.lblTDSGridView.Size = new System.Drawing.Size(920, 403);
            this.lblTDSGridView.TextSize = new System.Drawing.Size(0, 0);
            this.lblTDSGridView.TextToControlDistance = 0;
            this.lblTDSGridView.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.btnCancel;
            resources.ApplyResources(this.layoutControlItem1, "layoutControlItem1");
            this.layoutControlItem1.Location = new System.Drawing.Point(851, 429);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(69, 26);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.btnOk;
            resources.ApplyResources(this.layoutControlItem2, "layoutControlItem2");
            this.layoutControlItem2.Location = new System.Drawing.Point(782, 429);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(69, 26);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem2, "emptySpaceItem2");
            this.emptySpaceItem2.Location = new System.Drawing.Point(474, 0);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(10, 26);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // lblDateAson
            // 
            this.lblDateAson.AllowHtmlStringInCaption = true;
            this.lblDateAson.Control = this.dteDateAson;
            resources.ApplyResources(this.lblDateAson, "lblDateAson");
            this.lblDateAson.Location = new System.Drawing.Point(484, 0);
            this.lblDateAson.Name = "lblDateAson";
            this.lblDateAson.Size = new System.Drawing.Size(162, 26);
            this.lblDateAson.TextSize = new System.Drawing.Size(60, 13);
            // 
            // emptySpaceItem3
            // 
            this.emptySpaceItem3.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem3, "emptySpaceItem3");
            this.emptySpaceItem3.Location = new System.Drawing.Point(646, 0);
            this.emptySpaceItem3.Name = "emptySpaceItem3";
            this.emptySpaceItem3.Size = new System.Drawing.Size(10, 26);
            this.emptySpaceItem3.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.btnFetchTDS;
            resources.ApplyResources(this.layoutControlItem3, "layoutControlItem3");
            this.layoutControlItem3.Location = new System.Drawing.Point(816, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(69, 26);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextToControlDistance = 0;
            this.layoutControlItem3.TextVisible = false;
            // 
            // emptySpaceItem4
            // 
            this.emptySpaceItem4.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem4, "emptySpaceItem4");
            this.emptySpaceItem4.Location = new System.Drawing.Point(885, 0);
            this.emptySpaceItem4.Name = "emptySpaceItem4";
            this.emptySpaceItem4.Size = new System.Drawing.Size(35, 26);
            this.emptySpaceItem4.TextSize = new System.Drawing.Size(0, 0);
            // 
            // lblDateTo
            // 
            this.lblDateTo.AllowHtmlStringInCaption = true;
            this.lblDateTo.Control = this.dteDateTo;
            resources.ApplyResources(this.lblDateTo, "lblDateTo");
            this.lblDateTo.Location = new System.Drawing.Point(656, 0);
            this.lblDateTo.Name = "lblDateTo";
            this.lblDateTo.Size = new System.Drawing.Size(160, 26);
            this.lblDateTo.TextSize = new System.Drawing.Size(60, 13);
            // 
            // frmTDSChallan
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lcTDSChallan);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmTDSChallan";
            this.Load += new System.EventHandler(this.frmTDSChallan_Load);
            ((System.ComponentModel.ISupportInitialize)(this.lcTDSChallan)).EndInit();
            this.lcTDSChallan.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dteDateTo.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteDateTo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteDateAson.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteDateAson.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcTDSChallan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvTDSChallan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.glkpProject.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgTDSChallan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblProject)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblTDSGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblDateAson)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblDateTo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl lcTDSChallan;
        private DevExpress.XtraLayout.LayoutControlGroup lcgTDSChallan;
        private DevExpress.XtraEditors.GridLookUpEdit glkpProject;
        private DevExpress.XtraGrid.Views.Grid.GridView gridLookUpEdit1View;
        private DevExpress.XtraLayout.LayoutControlItem lblProject;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraEditors.SimpleButton btnOk;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraGrid.GridControl gcTDSChallan;
        private DevExpress.XtraGrid.Views.Grid.GridView gvTDSChallan;
        private DevExpress.XtraLayout.LayoutControlItem lblTDSGridView;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraEditors.DateEdit dteDateAson;
        private DevExpress.XtraGrid.Columns.GridColumn colVoucherId;
        private DevExpress.XtraGrid.Columns.GridColumn colLedgerID;
        private DevExpress.XtraGrid.Columns.GridColumn colVoucherDate;
        private DevExpress.XtraGrid.Columns.GridColumn colBankAccountNumber;
        private DevExpress.XtraGrid.Columns.GridColumn colDateFrom;
        private DevExpress.XtraGrid.Columns.GridColumn colDateTo;
        private DevExpress.XtraGrid.Columns.GridColumn colChequeNo;
        private DevExpress.XtraGrid.Columns.GridColumn colChequeDDDate;
        private DevExpress.XtraGrid.Columns.GridColumn colBankName;
        private DevExpress.XtraGrid.Columns.GridColumn colBRSCode;
        private DevExpress.XtraGrid.Columns.GridColumn colChallanNo;
        private DevExpress.XtraGrid.Columns.GridColumn colChallanDate;
        private DevExpress.XtraGrid.Columns.GridColumn colVoucherNo;
        private DevExpress.XtraGrid.Columns.GridColumn colAmount;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraLayout.LayoutControlItem lblDateAson;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem3;
        private DevExpress.XtraGrid.Columns.GridColumn colProjectId;
        private DevExpress.XtraGrid.Columns.GridColumn colProject;
        private DevExpress.XtraEditors.SimpleButton btnFetchTDS;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem4;
        private DevExpress.XtraEditors.DateEdit dteDateTo;
        private DevExpress.XtraLayout.LayoutControlItem lblDateTo;
    }
}