namespace ACPP.Modules.Dsync
{
    partial class frmAmendmentNotes
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAmendmentNotes));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.ucAmendments = new Bosco.Utility.Controls.ucToolBar();
            this.chkShowFilter = new System.Windows.Forms.CheckBox();
            this.gcAmendmentNotes = new DevExpress.XtraGrid.GridControl();
            this.gvAmendmentNotes = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gcolVoucherId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcolVoucherDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcolAmentmentDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcolVoucherNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcolVoucherType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcolParticulars = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcolAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcolNotes = new DevExpress.XtraGrid.Columns.GridColumn();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.simpleLabelItem1 = new DevExpress.XtraLayout.SimpleLabelItem();
            this.lblRecordCount = new DevExpress.XtraLayout.SimpleLabelItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcAmendmentNotes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvAmendmentNotes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblRecordCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.ucAmendments);
            this.layoutControl1.Controls.Add(this.chkShowFilter);
            this.layoutControl1.Controls.Add(this.gcAmendmentNotes);
            resources.ApplyResources(this.layoutControl1, "layoutControl1");
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(171, 223, 250, 350);
            this.layoutControl1.Root = this.layoutControlGroup1;
            // 
            // ucAmendments
            // 
            this.ucAmendments.ChangeAddCaption = "&Add";
            this.ucAmendments.ChangeCaption = "&Edit";
            this.ucAmendments.ChangeDeleteCaption = "&Delete";
            this.ucAmendments.ChangeNatureOfPaymentCaption = "&Nature of Payments";
            this.ucAmendments.ChangePrintCaption = "&Print";
            this.ucAmendments.DisableAddButton = true;
            this.ucAmendments.DisableCloseButton = true;
            this.ucAmendments.DisableDeleteButton = true;
            this.ucAmendments.DisableDownloadExcel = true;
            this.ucAmendments.DisableEditButton = true;
            this.ucAmendments.DisableMoveTransaction = true;
            this.ucAmendments.DisableNatureofPayments = true;
            this.ucAmendments.DisablePrintButton = true;
            resources.ApplyResources(this.ucAmendments, "ucAmendments");
            this.ucAmendments.Name = "ucAmendments";
            this.ucAmendments.ShowHTML = true;
            this.ucAmendments.ShowMMT = true;
            this.ucAmendments.ShowPDF = true;
            this.ucAmendments.ShowRTF = true;
            this.ucAmendments.ShowText = true;
            this.ucAmendments.ShowXLS = true;
            this.ucAmendments.ShowXLSX = true;
            this.ucAmendments.VisbleInsertVoucher = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucAmendments.VisibleAddButton = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucAmendments.VisibleDeleteButton = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucAmendments.VisibleDownloadExcel = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucAmendments.VisibleEditButton = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucAmendments.VisibleMoveTrans = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucAmendments.VisibleNatureofPayments = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucAmendments.VisibleNegativeBalance = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucAmendments.VisiblePrintButton = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucAmendments.VisibleRefresh = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucAmendments.CloseClicked += new System.EventHandler(this.ucAmendments_CloseClicked);
            this.ucAmendments.RefreshClicked += new System.EventHandler(this.ucAmendments_RefreshClicked);
            // 
            // chkShowFilter
            // 
            resources.ApplyResources(this.chkShowFilter, "chkShowFilter");
            this.chkShowFilter.Name = "chkShowFilter";
            this.chkShowFilter.UseVisualStyleBackColor = true;
            this.chkShowFilter.CheckedChanged += new System.EventHandler(this.chkShowFilter_CheckedChanged);
            // 
            // gcAmendmentNotes
            // 
            resources.ApplyResources(this.gcAmendmentNotes, "gcAmendmentNotes");
            this.gcAmendmentNotes.MainView = this.gvAmendmentNotes;
            this.gcAmendmentNotes.Name = "gcAmendmentNotes";
            this.gcAmendmentNotes.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvAmendmentNotes});
            // 
            // gvAmendmentNotes
            // 
            this.gvAmendmentNotes.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gcolVoucherId,
            this.gcolVoucherDate,
            this.gcolAmentmentDate,
            this.gcolVoucherNo,
            this.gcolVoucherType,
            this.gcolParticulars,
            this.gcolAmount,
            this.gcolNotes});
            this.gvAmendmentNotes.GridControl = this.gcAmendmentNotes;
            this.gvAmendmentNotes.Name = "gvAmendmentNotes";
            this.gvAmendmentNotes.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvAmendmentNotes.OptionsView.ShowGroupPanel = false;
            this.gvAmendmentNotes.RowCountChanged += new System.EventHandler(this.gvAmendmentNotes_RowCountChanged);
            // 
            // gcolVoucherId
            // 
            resources.ApplyResources(this.gcolVoucherId, "gcolVoucherId");
            this.gcolVoucherId.FieldName = "VOUCHER_ID";
            this.gcolVoucherId.Name = "gcolVoucherId";
            this.gcolVoucherId.OptionsColumn.AllowEdit = false;
            this.gcolVoucherId.OptionsColumn.ReadOnly = true;
            // 
            // gcolVoucherDate
            // 
            this.gcolVoucherDate.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("gcolVoucherDate.AppearanceHeader.Font")));
            this.gcolVoucherDate.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.gcolVoucherDate, "gcolVoucherDate");
            this.gcolVoucherDate.FieldName = "VOUCHER_DATE";
            this.gcolVoucherDate.Name = "gcolVoucherDate";
            this.gcolVoucherDate.OptionsColumn.AllowEdit = false;
            this.gcolVoucherDate.OptionsColumn.ReadOnly = true;
            // 
            // gcolAmentmentDate
            // 
            this.gcolAmentmentDate.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("gcolAmentmentDate.AppearanceHeader.Font")));
            this.gcolAmentmentDate.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.gcolAmentmentDate, "gcolAmentmentDate");
            this.gcolAmentmentDate.FieldName = "AMENDMENT_DATE";
            this.gcolAmentmentDate.Name = "gcolAmentmentDate";
            this.gcolAmentmentDate.OptionsColumn.AllowEdit = false;
            this.gcolAmentmentDate.OptionsColumn.ReadOnly = true;
            // 
            // gcolVoucherNo
            // 
            this.gcolVoucherNo.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("gcolVoucherNo.AppearanceHeader.Font")));
            this.gcolVoucherNo.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.gcolVoucherNo, "gcolVoucherNo");
            this.gcolVoucherNo.FieldName = "VOUCHER_NO";
            this.gcolVoucherNo.Name = "gcolVoucherNo";
            this.gcolVoucherNo.OptionsColumn.AllowEdit = false;
            this.gcolVoucherNo.OptionsColumn.ReadOnly = true;
            // 
            // gcolVoucherType
            // 
            this.gcolVoucherType.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("gcolVoucherType.AppearanceHeader.Font")));
            this.gcolVoucherType.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.gcolVoucherType, "gcolVoucherType");
            this.gcolVoucherType.FieldName = "VOUCHER_TYPE";
            this.gcolVoucherType.Name = "gcolVoucherType";
            this.gcolVoucherType.OptionsColumn.AllowEdit = false;
            this.gcolVoucherType.OptionsColumn.ReadOnly = true;
            // 
            // gcolParticulars
            // 
            this.gcolParticulars.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("gcolParticulars.AppearanceHeader.Font")));
            this.gcolParticulars.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.gcolParticulars, "gcolParticulars");
            this.gcolParticulars.FieldName = "LEDGER_NAME";
            this.gcolParticulars.Name = "gcolParticulars";
            this.gcolParticulars.OptionsColumn.AllowEdit = false;
            this.gcolParticulars.OptionsColumn.ReadOnly = true;
            // 
            // gcolAmount
            // 
            this.gcolAmount.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("gcolAmount.AppearanceHeader.Font")));
            this.gcolAmount.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.gcolAmount, "gcolAmount");
            this.gcolAmount.FieldName = "AMOUNT";
            this.gcolAmount.Name = "gcolAmount";
            this.gcolAmount.OptionsColumn.AllowEdit = false;
            this.gcolAmount.OptionsColumn.ReadOnly = true;
            // 
            // gcolNotes
            // 
            this.gcolNotes.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("gcolNotes.AppearanceHeader.Font")));
            this.gcolNotes.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.gcolNotes, "gcolNotes");
            this.gcolNotes.FieldName = "REMARKS";
            this.gcolNotes.Name = "gcolNotes";
            this.gcolNotes.OptionsColumn.AllowEdit = false;
            this.gcolNotes.OptionsColumn.ReadOnly = true;
            // 
            // layoutControlGroup1
            // 
            resources.ApplyResources(this.layoutControlGroup1, "layoutControlGroup1");
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2,
            this.emptySpaceItem1,
            this.layoutControlItem3,
            this.simpleLabelItem1,
            this.lblRecordCount,
            this.layoutControlItem1});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(853, 383);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.gcAmendmentNotes;
            resources.ApplyResources(this.layoutControlItem2, "layoutControlItem2");
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 31);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(853, 326);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem1, "emptySpaceItem1");
            this.emptySpaceItem1.Location = new System.Drawing.Point(109, 357);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(687, 26);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.chkShowFilter;
            resources.ApplyResources(this.layoutControlItem3, "layoutControlItem3");
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 357);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Padding = new DevExpress.XtraLayout.Utils.Padding(3, 3, 3, 3);
            this.layoutControlItem3.Size = new System.Drawing.Size(109, 26);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextToControlDistance = 0;
            this.layoutControlItem3.TextVisible = false;
            // 
            // simpleLabelItem1
            // 
            this.simpleLabelItem1.AllowHotTrack = false;
            this.simpleLabelItem1.AppearanceItemCaption.Font = ((System.Drawing.Font)(resources.GetObject("simpleLabelItem1.AppearanceItemCaption.Font")));
            this.simpleLabelItem1.AppearanceItemCaption.Options.UseFont = true;
            resources.ApplyResources(this.simpleLabelItem1, "simpleLabelItem1");
            this.simpleLabelItem1.Location = new System.Drawing.Point(796, 357);
            this.simpleLabelItem1.Name = "simpleLabelItem1";
            this.simpleLabelItem1.Size = new System.Drawing.Size(21, 26);
            this.simpleLabelItem1.TextSize = new System.Drawing.Size(9, 13);
            // 
            // lblRecordCount
            // 
            this.lblRecordCount.AllowHotTrack = false;
            this.lblRecordCount.AppearanceItemCaption.Font = ((System.Drawing.Font)(resources.GetObject("lblRecordCount.AppearanceItemCaption.Font")));
            this.lblRecordCount.AppearanceItemCaption.Options.UseFont = true;
            resources.ApplyResources(this.lblRecordCount, "lblRecordCount");
            this.lblRecordCount.Location = new System.Drawing.Point(817, 357);
            this.lblRecordCount.Name = "lblRecordCount";
            this.lblRecordCount.Size = new System.Drawing.Size(36, 26);
            this.lblRecordCount.TextSize = new System.Drawing.Size(9, 13);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.ucAmendments;
            resources.ApplyResources(this.layoutControlItem1, "layoutControlItem1");
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlItem1.Size = new System.Drawing.Size(853, 31);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // frmAmendmentNotes
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutControl1);
            this.Name = "frmAmendmentNotes";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ShowFilterClicked += new System.EventHandler(this.frmAmendmentNotes_ShowFilterClicked);
            this.Load += new System.EventHandler(this.frmAmendmentNotes_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcAmendmentNotes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvAmendmentNotes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblRecordCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraGrid.GridControl gcAmendmentNotes;
        private DevExpress.XtraGrid.Views.Grid.GridView gvAmendmentNotes;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraGrid.Columns.GridColumn gcolVoucherId;
        private DevExpress.XtraGrid.Columns.GridColumn gcolVoucherDate;
        private DevExpress.XtraGrid.Columns.GridColumn gcolVoucherNo;
        private DevExpress.XtraGrid.Columns.GridColumn gcolVoucherType;
        private DevExpress.XtraGrid.Columns.GridColumn gcolParticulars;
        private DevExpress.XtraGrid.Columns.GridColumn gcolAmount;
        private DevExpress.XtraGrid.Columns.GridColumn gcolNotes;
        private System.Windows.Forms.CheckBox chkShowFilter;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.SimpleLabelItem simpleLabelItem1;
        private DevExpress.XtraLayout.SimpleLabelItem lblRecordCount;
        private Bosco.Utility.Controls.ucToolBar ucAmendments;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraGrid.Columns.GridColumn gcolAmentmentDate;

    }
}