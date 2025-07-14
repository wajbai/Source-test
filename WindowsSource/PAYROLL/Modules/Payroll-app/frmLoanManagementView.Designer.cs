namespace PAYROLL.Modules.Payroll_app
{
    partial class frmLoanManagementView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLoanManagementView));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.ucToolBar1 = new PAYROLL.UserControl.UcToolBar();
            this.chkShowFilter = new DevExpress.XtraEditors.CheckEdit();
            this.gcLoanMgt = new DevExpress.XtraGrid.GridControl();
            this.gvLoanMgt = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colPrLoanId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colStaffId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRateOfInterest = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPayFrom = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPayto = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colInstallments = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDepartments = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col_loanID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLoan = new DevExpress.XtraGrid.Columns.GridColumn();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.lbltext = new DevExpress.XtraLayout.SimpleLabelItem();
            this.lblRecordCount = new DevExpress.XtraLayout.SimpleLabelItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowFilter.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcLoanMgt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvLoanMgt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lbltext)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblRecordCount)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.ucToolBar1);
            this.layoutControl1.Controls.Add(this.chkShowFilter);
            this.layoutControl1.Controls.Add(this.gcLoanMgt);
            resources.ApplyResources(this.layoutControl1, "layoutControl1");
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(192, 345, 250, 350);
            this.layoutControl1.Root = this.layoutControlGroup1;
            // 
            // ucToolBar1
            // 
            this.ucToolBar1.ChangeAddCaption = "<u>&A</u>dd";
            this.ucToolBar1.DisableAddButton = true;
            this.ucToolBar1.DisableCloseButton = true;
            this.ucToolBar1.DisableDeleteButton = true;
            this.ucToolBar1.DisableEditButton = true;
            this.ucToolBar1.DisableImportButton = true;
            this.ucToolBar1.DisablePrintButton = true;
            this.ucToolBar1.DisableRefreshButton = true;
            resources.ApplyResources(this.ucToolBar1, "ucToolBar1");
            this.ucToolBar1.Name = "ucToolBar1";
            this.ucToolBar1.VisibleAddButton = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBar1.VisibleClose = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucToolBar1.VisibleDeleteButton = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBar1.VisibleEditButton = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBar1.VisibleImport = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBar1.VisiblePrintButton = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBar1.VisibleRefresh = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucToolBar1.AddClicked += new System.EventHandler(this.ucToolBar1_AddClicked);
            this.ucToolBar1.EditClicked += new System.EventHandler(this.ucToolBar1_EditClicked);
            this.ucToolBar1.DeleteClicked += new System.EventHandler(this.ucToolBar1_DeleteClicked);
            this.ucToolBar1.PrintClicked += new System.EventHandler(this.ucToolBar1_PrintClicked);
            this.ucToolBar1.CloseClicked += new System.EventHandler(this.ucToolBar1_CloseClicked);
            this.ucToolBar1.RefreshClicked += new System.EventHandler(this.ucToolBar1_RefreshClicked);
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
            // gcLoanMgt
            // 
            resources.ApplyResources(this.gcLoanMgt, "gcLoanMgt");
            this.gcLoanMgt.MainView = this.gvLoanMgt;
            this.gcLoanMgt.Name = "gcLoanMgt";
            this.gcLoanMgt.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvLoanMgt});
            // 
            // gvLoanMgt
            // 
            this.gvLoanMgt.Appearance.FocusedRow.Font = ((System.Drawing.Font)(resources.GetObject("gvLoanMgt.Appearance.FocusedRow.Font")));
            this.gvLoanMgt.Appearance.FocusedRow.Options.UseFont = true;
            this.gvLoanMgt.Appearance.HeaderPanel.Font = ((System.Drawing.Font)(resources.GetObject("gvLoanMgt.Appearance.HeaderPanel.Font")));
            this.gvLoanMgt.Appearance.HeaderPanel.Options.UseFont = true;
            this.gvLoanMgt.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colPrLoanId,
            this.colStaffId,
            this.colName,
            this.colAmount,
            this.colRateOfInterest,
            this.colPayFrom,
            this.colPayto,
            this.colInstallments,
            this.colDepartments,
            this.col_loanID,
            this.colLoan});
            this.gvLoanMgt.GridControl = this.gcLoanMgt;
            this.gvLoanMgt.Name = "gvLoanMgt";
            this.gvLoanMgt.OptionsBehavior.Editable = false;
            this.gvLoanMgt.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvLoanMgt.OptionsView.ShowGroupPanel = false;
            this.gvLoanMgt.OptionsView.ShowIndicator = false;
            this.gvLoanMgt.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.gvLoanMgt_RowCellStyle);
            this.gvLoanMgt.DoubleClick += new System.EventHandler(this.gvLoanMgt_DoubleClick);
            this.gvLoanMgt.RowCountChanged += new System.EventHandler(this.gvLoanMgt_RowCountChanged);
            // 
            // colPrLoanId
            // 
            resources.ApplyResources(this.colPrLoanId, "colPrLoanId");
            this.colPrLoanId.FieldName = "PRLOANGETID";
            this.colPrLoanId.Name = "colPrLoanId";
            this.colPrLoanId.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colStaffId
            // 
            resources.ApplyResources(this.colStaffId, "colStaffId");
            this.colStaffId.FieldName = "STAFF_ID";
            this.colStaffId.Name = "colStaffId";
            // 
            // colName
            // 
            this.colName.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colName.AppearanceHeader.Font")));
            this.colName.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colName, "colName");
            this.colName.FieldName = "Name";
            this.colName.Name = "colName";
            this.colName.OptionsColumn.FixedWidth = true;
            // 
            // colAmount
            // 
            this.colAmount.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colAmount.AppearanceHeader.Font")));
            this.colAmount.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colAmount, "colAmount");
            this.colAmount.FieldName = "Amount";
            this.colAmount.Name = "colAmount";
            this.colAmount.OptionsColumn.FixedWidth = true;
            // 
            // colRateOfInterest
            // 
            this.colRateOfInterest.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colRateOfInterest.AppearanceHeader.Font")));
            this.colRateOfInterest.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colRateOfInterest, "colRateOfInterest");
            this.colRateOfInterest.FieldName = "Rate_of_Interest";
            this.colRateOfInterest.Name = "colRateOfInterest";
            this.colRateOfInterest.OptionsColumn.FixedWidth = true;
            // 
            // colPayFrom
            // 
            this.colPayFrom.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colPayFrom.AppearanceHeader.Font")));
            this.colPayFrom.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colPayFrom, "colPayFrom");
            this.colPayFrom.FieldName = "Pay_From";
            this.colPayFrom.Name = "colPayFrom";
            this.colPayFrom.OptionsColumn.FixedWidth = true;
            // 
            // colPayto
            // 
            this.colPayto.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colPayto.AppearanceHeader.Font")));
            this.colPayto.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colPayto, "colPayto");
            this.colPayto.FieldName = "Pay_To";
            this.colPayto.Name = "colPayto";
            this.colPayto.OptionsColumn.FixedWidth = true;
            // 
            // colInstallments
            // 
            this.colInstallments.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colInstallments.AppearanceHeader.Font")));
            this.colInstallments.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colInstallments, "colInstallments");
            this.colInstallments.FieldName = "Installments";
            this.colInstallments.Name = "colInstallments";
            this.colInstallments.OptionsColumn.FixedWidth = true;
            // 
            // colDepartments
            // 
            this.colDepartments.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colDepartments.AppearanceHeader.Font")));
            this.colDepartments.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colDepartments, "colDepartments");
            this.colDepartments.FieldName = "Department";
            this.colDepartments.Name = "colDepartments";
            this.colDepartments.OptionsColumn.FixedWidth = true;
            // 
            // col_loanID
            // 
            resources.ApplyResources(this.col_loanID, "col_loanID");
            this.col_loanID.FieldName = "LOAN_ID";
            this.col_loanID.Name = "col_loanID";
            this.col_loanID.OptionsColumn.FixedWidth = true;
            // 
            // colLoan
            // 
            this.colLoan.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colLoan.AppearanceHeader.Font")));
            this.colLoan.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colLoan, "colLoan");
            this.colLoan.FieldName = "LOAN_NAME";
            this.colLoan.Name = "colLoan";
            this.colLoan.OptionsColumn.FixedWidth = true;
            // 
            // layoutControlGroup1
            // 
            resources.ApplyResources(this.layoutControlGroup1, "layoutControlGroup1");
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.emptySpaceItem1,
            this.lbltext,
            this.lblRecordCount});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(574, 328);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gcLoanMgt;
            resources.ApplyResources(this.layoutControlItem1, "layoutControlItem1");
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 29);
            this.layoutControlItem1.MinSize = new System.Drawing.Size(110, 30);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Padding = new DevExpress.XtraLayout.Utils.Padding(1, 1, 1, 1);
            this.layoutControlItem1.Size = new System.Drawing.Size(572, 273);
            this.layoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.chkShowFilter;
            resources.ApplyResources(this.layoutControlItem2, "layoutControlItem2");
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 302);
            this.layoutControlItem2.MinSize = new System.Drawing.Size(79, 23);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(79, 24);
            this.layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.ucToolBar1;
            resources.ApplyResources(this.layoutControlItem3, "layoutControlItem3");
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem3.MaxSize = new System.Drawing.Size(0, 29);
            this.layoutControlItem3.MinSize = new System.Drawing.Size(98, 29);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Padding = new DevExpress.XtraLayout.Utils.Padding(-1, -1, -1, -1);
            this.layoutControlItem3.Size = new System.Drawing.Size(572, 29);
            this.layoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextToControlDistance = 0;
            this.layoutControlItem3.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem1, "emptySpaceItem1");
            this.emptySpaceItem1.Location = new System.Drawing.Point(79, 302);
            this.emptySpaceItem1.MinSize = new System.Drawing.Size(104, 24);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(467, 24);
            this.emptySpaceItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // lbltext
            // 
            this.lbltext.AllowHotTrack = false;
            this.lbltext.AppearanceItemCaption.Font = ((System.Drawing.Font)(resources.GetObject("lbltext.AppearanceItemCaption.Font")));
            this.lbltext.AppearanceItemCaption.Options.UseFont = true;
            resources.ApplyResources(this.lbltext, "lbltext");
            this.lbltext.Location = new System.Drawing.Point(546, 302);
            this.lbltext.Name = "lbltext";
            this.lbltext.Size = new System.Drawing.Size(13, 24);
            this.lbltext.TextSize = new System.Drawing.Size(9, 13);
            // 
            // lblRecordCount
            // 
            this.lblRecordCount.AllowHotTrack = false;
            resources.ApplyResources(this.lblRecordCount, "lblRecordCount");
            this.lblRecordCount.Location = new System.Drawing.Point(559, 302);
            this.lblRecordCount.Name = "lblRecordCount";
            this.lblRecordCount.Size = new System.Drawing.Size(13, 24);
            this.lblRecordCount.TextSize = new System.Drawing.Size(9, 13);
            // 
            // frmLoanManagementView
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmLoanManagementView";
            this.ShowFilterClicked += new System.EventHandler(this.frmLoanManagementView_ShowFilterClicked);
            this.EnterClicked += new System.EventHandler(this.frmLoanManagementView_EnterClicked);
            this.Load += new System.EventHandler(this.frmLoanManagementView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chkShowFilter.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcLoanMgt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvLoanMgt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lbltext)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblRecordCount)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraEditors.CheckEdit chkShowFilter;
        private DevExpress.XtraGrid.GridControl gcLoanMgt;
        private DevExpress.XtraGrid.Views.Grid.GridView gvLoanMgt;
        private DevExpress.XtraGrid.Columns.GridColumn colName;
        private DevExpress.XtraGrid.Columns.GridColumn colAmount;
        private DevExpress.XtraGrid.Columns.GridColumn colRateOfInterest;
        private DevExpress.XtraGrid.Columns.GridColumn colPayFrom;
        private DevExpress.XtraGrid.Columns.GridColumn colPayto;
        private DevExpress.XtraGrid.Columns.GridColumn colInstallments;
        private DevExpress.XtraGrid.Columns.GridColumn colDepartments;
        private DevExpress.XtraGrid.Columns.GridColumn colLoan;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private UserControl.UcToolBar ucToolBar1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.SimpleLabelItem lbltext;
        private DevExpress.XtraLayout.SimpleLabelItem lblRecordCount;
        private DevExpress.XtraGrid.Columns.GridColumn colPrLoanId;
        private DevExpress.XtraGrid.Columns.GridColumn colStaffId;
        private DevExpress.XtraGrid.Columns.GridColumn col_loanID;
    }
}