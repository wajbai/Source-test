namespace ACPP.Modules.Master
{
    partial class frmAddMonthAnnualBudget
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
            this.components = new System.ComponentModel.Container();
            DevExpress.XtraGrid.GridLevelNode gridLevelNode1 = new DevExpress.XtraGrid.GridLevelNode();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.glkpMonth = new DevExpress.XtraEditors.GridLookUpEdit();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gccolName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gccoldisplayMember = new DevExpress.XtraGrid.Columns.GridColumn();
            this.peImportExcelData = new DevExpress.XtraEditors.PictureEdit();
            this.gcExpenseLedger = new DevExpress.XtraGrid.GridControl();
            this.gvExpenseLedger = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colExpenseLedgerId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gccolexpenseLedgerName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gccolexpenseApprovedPreviousYR = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gccolexpenseActualincome = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gccolexpenseproposedcurrentYR = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rtxtExpenseProposedCurrentYR = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.gccolexpenseapprovedcurrentYR = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rtxtApprovedCurrentYR = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.pePrintBudget = new DevExpress.XtraEditors.PictureEdit();
            this.chkFilter = new DevExpress.XtraEditors.CheckEdit();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcBudgetGroup = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lcPrint = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcExpenseBudgetLedgers = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcExcellImport = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem12 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciMonth = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.printingSys = new DevExpress.XtraPrinting.PrintingSystem(this.components);
            this.emptySpaceItem3 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem4 = new DevExpress.XtraLayout.EmptySpaceItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.glkpMonth.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.peImportExcelData.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcExpenseLedger)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvExpenseLedger)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtExpenseProposedCurrentYR)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtApprovedCurrentYR)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pePrintBudget.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkFilter.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcBudgetGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcPrint)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcExpenseBudgetLedgers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcExcellImport)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem12)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciMonth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.printingSys)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem4)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.glkpMonth);
            this.layoutControl1.Controls.Add(this.peImportExcelData);
            this.layoutControl1.Controls.Add(this.gcExpenseLedger);
            this.layoutControl1.Controls.Add(this.pePrintBudget);
            this.layoutControl1.Controls.Add(this.chkFilter);
            this.layoutControl1.Controls.Add(this.btnCancel);
            this.layoutControl1.Controls.Add(this.btnSave);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(853, 232, 303, 560);
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(1285, 599);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // glkpMonth
            // 
            this.glkpMonth.Location = new System.Drawing.Point(1151, 7);
            this.glkpMonth.Name = "glkpMonth";
            this.glkpMonth.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.glkpMonth.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.glkpMonth.Properties.NullText = "";
            this.glkpMonth.Properties.PopupFormMinSize = new System.Drawing.Size(150, 200);
            this.glkpMonth.Properties.PopupFormSize = new System.Drawing.Size(150, 200);
            this.glkpMonth.Properties.View = this.gridView1;
            this.glkpMonth.Size = new System.Drawing.Size(103, 20);
            this.glkpMonth.StyleController = this.layoutControl1;
            this.glkpMonth.TabIndex = 36;
            this.glkpMonth.EditValueChanged += new System.EventHandler(this.glkpMonth_EditValueChanged);
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gccolName,
            this.gccoldisplayMember});
            this.gridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView1.OptionsView.ShowColumnHeaders = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.OptionsView.ShowIndicator = false;
            // 
            // gccolName
            // 
            this.gccolName.Caption = "Name";
            this.gccolName.FieldName = "NAME";
            this.gccolName.Name = "gccolName";
            // 
            // gccoldisplayMember
            // 
            this.gccoldisplayMember.Caption = "Name";
            this.gccoldisplayMember.FieldName = "NAME";
            this.gccoldisplayMember.Name = "gccoldisplayMember";
            this.gccoldisplayMember.Visible = true;
            this.gccoldisplayMember.VisibleIndex = 0;
            // 
            // peImportExcelData
            // 
            this.peImportExcelData.EditValue = global::ACPP.Properties.Resources.project_mapping;
            this.peImportExcelData.Location = new System.Drawing.Point(15, 58);
            this.peImportExcelData.Name = "peImportExcelData";
            this.peImportExcelData.Size = new System.Drawing.Size(20, 22);
            this.peImportExcelData.StyleController = this.layoutControl1;
            this.peImportExcelData.TabIndex = 35;
            this.peImportExcelData.ToolTip = "Import Budget Ledger Approved Amount from Excel";
            this.peImportExcelData.Click += new System.EventHandler(this.peImportExcelData_Click);
            // 
            // gcExpenseLedger
            // 
            gridLevelNode1.RelationName = "Level1";
            this.gcExpenseLedger.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode1});
            this.gcExpenseLedger.Location = new System.Drawing.Point(15, 84);
            this.gcExpenseLedger.MainView = this.gvExpenseLedger;
            this.gcExpenseLedger.Name = "gcExpenseLedger";
            this.gcExpenseLedger.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rtxtExpenseProposedCurrentYR,
            this.rtxtApprovedCurrentYR});
            this.gcExpenseLedger.Size = new System.Drawing.Size(1255, 474);
            this.gcExpenseLedger.TabIndex = 33;
            this.gcExpenseLedger.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvExpenseLedger});
            this.gcExpenseLedger.ProcessGridKey += new System.Windows.Forms.KeyEventHandler(this.gcExpenseLedger_ProcessGridKey);
            // 
            // gvExpenseLedger
            // 
            this.gvExpenseLedger.Appearance.FooterPanel.BackColor = System.Drawing.Color.Transparent;
            this.gvExpenseLedger.Appearance.FooterPanel.BackColor2 = System.Drawing.Color.Transparent;
            this.gvExpenseLedger.Appearance.FooterPanel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.gvExpenseLedger.Appearance.FooterPanel.ForeColor = System.Drawing.Color.Blue;
            this.gvExpenseLedger.Appearance.FooterPanel.Options.UseBackColor = true;
            this.gvExpenseLedger.Appearance.FooterPanel.Options.UseFont = true;
            this.gvExpenseLedger.Appearance.FooterPanel.Options.UseForeColor = true;
            this.gvExpenseLedger.Appearance.ViewCaption.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.gvExpenseLedger.Appearance.ViewCaption.Options.UseFont = true;
            this.gvExpenseLedger.Appearance.ViewCaption.Options.UseTextOptions = true;
            this.gvExpenseLedger.Appearance.ViewCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.gvExpenseLedger.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colExpenseLedgerId,
            this.gccolexpenseLedgerName,
            this.gccolexpenseApprovedPreviousYR,
            this.gccolexpenseActualincome,
            this.gccolexpenseproposedcurrentYR,
            this.gccolexpenseapprovedcurrentYR,
            this.gridColumn1});
            this.gvExpenseLedger.GridControl = this.gcExpenseLedger;
            this.gvExpenseLedger.Name = "gvExpenseLedger";
            this.gvExpenseLedger.OptionsView.ShowFooter = true;
            this.gvExpenseLedger.OptionsView.ShowGroupPanel = false;
            this.gvExpenseLedger.OptionsView.ShowIndicator = false;
            this.gvExpenseLedger.OptionsView.ShowViewCaption = true;
            this.gvExpenseLedger.ViewCaption = "Budget Expense Ledgers";
            // 
            // colExpenseLedgerId
            // 
            this.colExpenseLedgerId.Caption = "ExpenseLedgerId";
            this.colExpenseLedgerId.FieldName = "LEDGER_ID";
            this.colExpenseLedgerId.Name = "colExpenseLedgerId";
            // 
            // gccolexpenseLedgerName
            // 
            this.gccolexpenseLedgerName.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.gccolexpenseLedgerName.AppearanceHeader.Options.UseFont = true;
            this.gccolexpenseLedgerName.Caption = "Ledger";
            this.gccolexpenseLedgerName.FieldName = "LEDGER_NAME";
            this.gccolexpenseLedgerName.Name = "gccolexpenseLedgerName";
            this.gccolexpenseLedgerName.OptionsColumn.AllowEdit = false;
            this.gccolexpenseLedgerName.OptionsColumn.AllowFocus = false;
            this.gccolexpenseLedgerName.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.gccolexpenseLedgerName.Visible = true;
            this.gccolexpenseLedgerName.VisibleIndex = 0;
            this.gccolexpenseLedgerName.Width = 175;
            // 
            // gccolexpenseApprovedPreviousYR
            // 
            this.gccolexpenseApprovedPreviousYR.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.gccolexpenseApprovedPreviousYR.AppearanceHeader.Options.UseFont = true;
            this.gccolexpenseApprovedPreviousYR.Caption = "Approved 2014-15";
            this.gccolexpenseApprovedPreviousYR.DisplayFormat.FormatString = "n";
            this.gccolexpenseApprovedPreviousYR.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gccolexpenseApprovedPreviousYR.FieldName = "APPROVED_PREVIOUS_YR";
            this.gccolexpenseApprovedPreviousYR.Name = "gccolexpenseApprovedPreviousYR";
            this.gccolexpenseApprovedPreviousYR.OptionsColumn.AllowEdit = false;
            this.gccolexpenseApprovedPreviousYR.OptionsColumn.AllowFocus = false;
            this.gccolexpenseApprovedPreviousYR.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "APPROVED_PREVIOUS_YR", "{0:N}")});
            this.gccolexpenseApprovedPreviousYR.Visible = true;
            this.gccolexpenseApprovedPreviousYR.VisibleIndex = 1;
            this.gccolexpenseApprovedPreviousYR.Width = 121;
            // 
            // gccolexpenseActualincome
            // 
            this.gccolexpenseActualincome.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.gccolexpenseActualincome.AppearanceHeader.Options.UseFont = true;
            this.gccolexpenseActualincome.Caption = "Actual 2014-15";
            this.gccolexpenseActualincome.DisplayFormat.FormatString = "n";
            this.gccolexpenseActualincome.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gccolexpenseActualincome.FieldName = "ACTUAL";
            this.gccolexpenseActualincome.Name = "gccolexpenseActualincome";
            this.gccolexpenseActualincome.OptionsColumn.AllowEdit = false;
            this.gccolexpenseActualincome.OptionsColumn.AllowFocus = false;
            this.gccolexpenseActualincome.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "ACTUAL", "{0:N}")});
            this.gccolexpenseActualincome.Visible = true;
            this.gccolexpenseActualincome.VisibleIndex = 2;
            this.gccolexpenseActualincome.Width = 112;
            // 
            // gccolexpenseproposedcurrentYR
            // 
            this.gccolexpenseproposedcurrentYR.AppearanceCell.BackColor = System.Drawing.Color.LightYellow;
            this.gccolexpenseproposedcurrentYR.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.gccolexpenseproposedcurrentYR.AppearanceCell.Options.UseBackColor = true;
            this.gccolexpenseproposedcurrentYR.AppearanceCell.Options.UseFont = true;
            this.gccolexpenseproposedcurrentYR.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.gccolexpenseproposedcurrentYR.AppearanceHeader.Options.UseFont = true;
            this.gccolexpenseproposedcurrentYR.Caption = "Proposed 2015-16";
            this.gccolexpenseproposedcurrentYR.ColumnEdit = this.rtxtExpenseProposedCurrentYR;
            this.gccolexpenseproposedcurrentYR.DisplayFormat.FormatString = "n";
            this.gccolexpenseproposedcurrentYR.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gccolexpenseproposedcurrentYR.FieldName = "PROPOSED_CURRENT_YR";
            this.gccolexpenseproposedcurrentYR.Name = "gccolexpenseproposedcurrentYR";
            this.gccolexpenseproposedcurrentYR.OptionsColumn.AllowMove = false;
            this.gccolexpenseproposedcurrentYR.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gccolexpenseproposedcurrentYR.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "PROPOSED_CURRENT_YR", "{0:N}")});
            this.gccolexpenseproposedcurrentYR.Visible = true;
            this.gccolexpenseproposedcurrentYR.VisibleIndex = 3;
            this.gccolexpenseproposedcurrentYR.Width = 118;
            // 
            // rtxtExpenseProposedCurrentYR
            // 
            this.rtxtExpenseProposedCurrentYR.AutoHeight = false;
            this.rtxtExpenseProposedCurrentYR.Mask.EditMask = "###,###,###,##0.00;";
            this.rtxtExpenseProposedCurrentYR.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.rtxtExpenseProposedCurrentYR.Name = "rtxtExpenseProposedCurrentYR";
            this.rtxtExpenseProposedCurrentYR.KeyDown += new System.Windows.Forms.KeyEventHandler(this.rtxtExpenseProposedCurrentYR_KeyDown);
            // 
            // gccolexpenseapprovedcurrentYR
            // 
            this.gccolexpenseapprovedcurrentYR.AppearanceCell.BackColor = System.Drawing.Color.LightYellow;
            this.gccolexpenseapprovedcurrentYR.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.gccolexpenseapprovedcurrentYR.AppearanceCell.Options.UseBackColor = true;
            this.gccolexpenseapprovedcurrentYR.AppearanceCell.Options.UseFont = true;
            this.gccolexpenseapprovedcurrentYR.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.gccolexpenseapprovedcurrentYR.AppearanceHeader.Options.UseFont = true;
            this.gccolexpenseapprovedcurrentYR.Caption = "Approved 2015-16";
            this.gccolexpenseapprovedcurrentYR.ColumnEdit = this.rtxtApprovedCurrentYR;
            this.gccolexpenseapprovedcurrentYR.DisplayFormat.FormatString = "n";
            this.gccolexpenseapprovedcurrentYR.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gccolexpenseapprovedcurrentYR.FieldName = "APPROVED_CURRENT_YR";
            this.gccolexpenseapprovedcurrentYR.Name = "gccolexpenseapprovedcurrentYR";
            this.gccolexpenseapprovedcurrentYR.OptionsColumn.AllowMove = false;
            this.gccolexpenseapprovedcurrentYR.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gccolexpenseapprovedcurrentYR.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "APPROVED_CURRENT_YR", "{0:N}")});
            this.gccolexpenseapprovedcurrentYR.Visible = true;
            this.gccolexpenseapprovedcurrentYR.VisibleIndex = 4;
            this.gccolexpenseapprovedcurrentYR.Width = 126;
            // 
            // rtxtApprovedCurrentYR
            // 
            this.rtxtApprovedCurrentYR.AutoHeight = false;
            this.rtxtApprovedCurrentYR.Mask.EditMask = "###,###,###,##0.00;";
            this.rtxtApprovedCurrentYR.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.rtxtApprovedCurrentYR.Name = "rtxtApprovedCurrentYR";
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "gridColumn1";
            this.gridColumn1.FieldName = "NATURE_ID";
            this.gridColumn1.Name = "gridColumn1";
            // 
            // pePrintBudget
            // 
            this.pePrintBudget.EditValue = global::ACPP.Properties.Resources.print1;
            this.pePrintBudget.Location = new System.Drawing.Point(39, 58);
            this.pePrintBudget.Name = "pePrintBudget";
            this.pePrintBudget.Size = new System.Drawing.Size(20, 22);
            this.pePrintBudget.StyleController = this.layoutControl1;
            this.pePrintBudget.TabIndex = 22;
            this.pePrintBudget.ToolTip = "Print the Budget Amount";
            this.pePrintBudget.Click += new System.EventHandler(this.pePrintBudget_Click);
            // 
            // chkFilter
            // 
            this.chkFilter.Location = new System.Drawing.Point(7, 570);
            this.chkFilter.Name = "chkFilter";
            this.chkFilter.Properties.AllowHtmlDraw = DevExpress.Utils.DefaultBoolean.True;
            this.chkFilter.Properties.Caption = "Show <b>&F</b>ilter";
            this.chkFilter.Size = new System.Drawing.Size(78, 19);
            this.chkFilter.StyleController = this.layoutControl1;
            this.chkFilter.TabIndex = 15;
            this.chkFilter.CheckedChanged += new System.EventHandler(this.chkFilter_CheckedChanged);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(1206, 570);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(72, 22);
            this.btnCancel.StyleController = this.layoutControl1;
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "&Close";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(1126, 570);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(76, 22);
            this.btnSave.StyleController = this.layoutControl1;
            this.btnSave.TabIndex = 6;
            this.btnSave.Text = "&Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "Root";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem5,
            this.layoutControlItem6,
            this.lcBudgetGroup,
            this.emptySpaceItem1,
            this.layoutControlItem12,
            this.lciMonth,
            this.emptySpaceItem2,
            this.emptySpaceItem4});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.layoutControlGroup1.Size = new System.Drawing.Size(1285, 599);
            this.layoutControlGroup1.Text = "Root";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.btnSave;
            this.layoutControlItem5.CustomizationFormText = "layoutControlItem5";
            this.layoutControlItem5.Location = new System.Drawing.Point(1119, 563);
            this.layoutControlItem5.MaxSize = new System.Drawing.Size(80, 26);
            this.layoutControlItem5.MinSize = new System.Drawing.Size(80, 26);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(80, 26);
            this.layoutControlItem5.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem5.Text = "layoutControlItem5";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextToControlDistance = 0;
            this.layoutControlItem5.TextVisible = false;
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.btnCancel;
            this.layoutControlItem6.CustomizationFormText = "layoutControlItem6";
            this.layoutControlItem6.Location = new System.Drawing.Point(1199, 563);
            this.layoutControlItem6.MaxSize = new System.Drawing.Size(76, 26);
            this.layoutControlItem6.MinSize = new System.Drawing.Size(76, 26);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(76, 26);
            this.layoutControlItem6.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem6.Text = "layoutControlItem6";
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextToControlDistance = 0;
            this.layoutControlItem6.TextVisible = false;
            // 
            // lcBudgetGroup
            // 
            this.lcBudgetGroup.AppearanceGroup.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.lcBudgetGroup.AppearanceGroup.Options.UseFont = true;
            this.lcBudgetGroup.CustomizationFormText = "Budget for 2014-15";
            this.lcBudgetGroup.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lcPrint,
            this.lcExpenseBudgetLedgers,
            this.lcExcellImport,
            this.emptySpaceItem3});
            this.lcBudgetGroup.Location = new System.Drawing.Point(0, 24);
            this.lcBudgetGroup.Name = "lcBudgetGroup";
            this.lcBudgetGroup.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.lcBudgetGroup.Size = new System.Drawing.Size(1275, 539);
            this.lcBudgetGroup.Text = "Budget for 2014-15";
            // 
            // lcPrint
            // 
            this.lcPrint.Control = this.pePrintBudget;
            this.lcPrint.CustomizationFormText = "lcPrint";
            this.lcPrint.Location = new System.Drawing.Point(24, 0);
            this.lcPrint.MinSize = new System.Drawing.Size(24, 24);
            this.lcPrint.Name = "lcPrint";
            this.lcPrint.Size = new System.Drawing.Size(24, 26);
            this.lcPrint.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lcPrint.Text = "lcPrint";
            this.lcPrint.TextSize = new System.Drawing.Size(0, 0);
            this.lcPrint.TextToControlDistance = 0;
            this.lcPrint.TextVisible = false;
            // 
            // lcExpenseBudgetLedgers
            // 
            this.lcExpenseBudgetLedgers.Control = this.gcExpenseLedger;
            this.lcExpenseBudgetLedgers.CustomizationFormText = "lcExpenseBudgetLedgers";
            this.lcExpenseBudgetLedgers.Location = new System.Drawing.Point(0, 26);
            this.lcExpenseBudgetLedgers.Name = "lcExpenseBudgetLedgers";
            this.lcExpenseBudgetLedgers.Size = new System.Drawing.Size(1259, 478);
            this.lcExpenseBudgetLedgers.Text = "lcExpenseBudgetLedgers";
            this.lcExpenseBudgetLedgers.TextSize = new System.Drawing.Size(0, 0);
            this.lcExpenseBudgetLedgers.TextToControlDistance = 0;
            this.lcExpenseBudgetLedgers.TextVisible = false;
            // 
            // lcExcellImport
            // 
            this.lcExcellImport.Control = this.peImportExcelData;
            this.lcExcellImport.CustomizationFormText = "lcExcellImport";
            this.lcExcellImport.Location = new System.Drawing.Point(0, 0);
            this.lcExcellImport.Name = "lcExcellImport";
            this.lcExcellImport.Size = new System.Drawing.Size(24, 26);
            this.lcExcellImport.Text = "lcExcellImport";
            this.lcExcellImport.TextSize = new System.Drawing.Size(0, 0);
            this.lcExcellImport.TextToControlDistance = 0;
            this.lcExcellImport.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.CustomizationFormText = "emptySpaceItem1";
            this.emptySpaceItem1.Location = new System.Drawing.Point(1251, 0);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(24, 24);
            this.emptySpaceItem1.Text = "emptySpaceItem1";
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem12
            // 
            this.layoutControlItem12.Control = this.chkFilter;
            this.layoutControlItem12.CustomizationFormText = "layoutControlItem12";
            this.layoutControlItem12.Location = new System.Drawing.Point(0, 563);
            this.layoutControlItem12.Name = "layoutControlItem12";
            this.layoutControlItem12.Size = new System.Drawing.Size(82, 26);
            this.layoutControlItem12.Text = "layoutControlItem12";
            this.layoutControlItem12.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem12.TextToControlDistance = 0;
            this.layoutControlItem12.TextVisible = false;
            // 
            // lciMonth
            // 
            this.lciMonth.Control = this.glkpMonth;
            this.lciMonth.CustomizationFormText = "Month";
            this.lciMonth.Location = new System.Drawing.Point(1087, 0);
            this.lciMonth.MaxSize = new System.Drawing.Size(0, 24);
            this.lciMonth.MinSize = new System.Drawing.Size(109, 24);
            this.lciMonth.Name = "lciMonth";
            this.lciMonth.Padding = new DevExpress.XtraLayout.Utils.Padding(4, 2, 2, 2);
            this.lciMonth.Size = new System.Drawing.Size(164, 24);
            this.lciMonth.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lciMonth.Text = "Month";
            this.lciMonth.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.lciMonth.TextSize = new System.Drawing.Size(50, 20);
            this.lciMonth.TextToControlDistance = 5;
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.CustomizationFormText = "emptySpaceItem2";
            this.emptySpaceItem2.Location = new System.Drawing.Point(82, 563);
            this.emptySpaceItem2.MinSize = new System.Drawing.Size(104, 24);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(1037, 26);
            this.emptySpaceItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem2.Text = "emptySpaceItem2";
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem3
            // 
            this.emptySpaceItem3.AllowHotTrack = false;
            this.emptySpaceItem3.CustomizationFormText = "emptySpaceItem3";
            this.emptySpaceItem3.Location = new System.Drawing.Point(48, 0);
            this.emptySpaceItem3.Name = "emptySpaceItem3";
            this.emptySpaceItem3.Size = new System.Drawing.Size(1211, 26);
            this.emptySpaceItem3.Text = "emptySpaceItem3";
            this.emptySpaceItem3.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem4
            // 
            this.emptySpaceItem4.AllowHotTrack = false;
            this.emptySpaceItem4.CustomizationFormText = "emptySpaceItem4";
            this.emptySpaceItem4.Location = new System.Drawing.Point(0, 0);
            this.emptySpaceItem4.Name = "emptySpaceItem4";
            this.emptySpaceItem4.Size = new System.Drawing.Size(1087, 24);
            this.emptySpaceItem4.Text = "emptySpaceItem4";
            this.emptySpaceItem4.TextSize = new System.Drawing.Size(0, 0);
            // 
            // frmAddAnnualBudget
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(1285, 599);
            this.Controls.Add(this.layoutControl1);
            this.Name = "frmAddAnnualBudget";
            this.Padding = new System.Windows.Forms.Padding(0);
            this.Text = "Budget";
            this.Load += new System.EventHandler(this.frmAnnualBudget_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.glkpMonth.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.peImportExcelData.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcExpenseLedger)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvExpenseLedger)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtExpenseProposedCurrentYR)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtApprovedCurrentYR)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pePrintBudget.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkFilter.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcBudgetGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcPrint)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcExpenseBudgetLedgers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcExcellImport)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem12)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciMonth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.printingSys)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem4)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraEditors.CheckEdit chkFilter;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem12;
        private DevExpress.XtraLayout.LayoutControlGroup lcBudgetGroup;
        private DevExpress.XtraEditors.PictureEdit pePrintBudget;
        private DevExpress.XtraLayout.LayoutControlItem lcPrint;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraGrid.GridControl gcExpenseLedger;
        private DevExpress.XtraGrid.Views.Grid.GridView gvExpenseLedger;
        private DevExpress.XtraGrid.Columns.GridColumn gccolexpenseLedgerName;
        private DevExpress.XtraGrid.Columns.GridColumn gccolexpenseApprovedPreviousYR;
        private DevExpress.XtraGrid.Columns.GridColumn gccolexpenseActualincome;
        private DevExpress.XtraGrid.Columns.GridColumn gccolexpenseproposedcurrentYR;
        private DevExpress.XtraGrid.Columns.GridColumn gccolexpenseapprovedcurrentYR;
        private DevExpress.XtraLayout.LayoutControlItem lcExpenseBudgetLedgers;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit rtxtExpenseProposedCurrentYR;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit rtxtApprovedCurrentYR;
        private DevExpress.XtraGrid.Columns.GridColumn colExpenseLedgerId;
        private DevExpress.XtraPrinting.PrintingSystem printingSys;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraEditors.PictureEdit peImportExcelData;
        private DevExpress.XtraLayout.LayoutControlItem lcExcellImport;
        private DevExpress.XtraEditors.GridLookUpEdit glkpMonth;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraLayout.LayoutControlItem lciMonth;
        private DevExpress.XtraGrid.Columns.GridColumn gccolName;
        private DevExpress.XtraGrid.Columns.GridColumn gccoldisplayMember;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem3;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem4;
    }
}