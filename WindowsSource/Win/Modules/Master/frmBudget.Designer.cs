namespace ACPP.Modules.Master
{
    partial class frmBudget
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
            this.pgfMonthName = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pgcBudget = new DevExpress.XtraPivotGrid.PivotGridControl();
            this.pgfGroup = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pgfLedgerCode = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pgfLedgerName = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pgfAmount = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pgfMonthName1 = new DevExpress.XtraPivotGrid.PivotGridField();
            this.dockPanel1_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            ((System.ComponentModel.ISupportInitialize)(this.pgcBudget)).BeginInit();
            this.SuspendLayout();
            // 
            // pgfMonthName
            // 
            this.pgfMonthName.Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea;
            this.pgfMonthName.AreaIndex = 0;
            this.pgfMonthName.FieldName = "MONTH_NAME";
            this.pgfMonthName.Name = "pgfMonthName";
            // 
            // pgcBudget
            // 
            this.pgcBudget.Appearance.FieldHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.pgcBudget.Appearance.FieldHeader.Options.UseFont = true;
            this.pgcBudget.Appearance.FieldValueGrandTotal.BackColor = System.Drawing.Color.Wheat;
            this.pgcBudget.Appearance.FieldValueGrandTotal.BackColor2 = System.Drawing.Color.Goldenrod;
            this.pgcBudget.Appearance.FieldValueGrandTotal.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.pgcBudget.Appearance.FieldValueGrandTotal.ForeColor = System.Drawing.Color.Black;
            this.pgcBudget.Appearance.FieldValueGrandTotal.Options.UseBackColor = true;
            this.pgcBudget.Appearance.FieldValueGrandTotal.Options.UseFont = true;
            this.pgcBudget.Appearance.FieldValueGrandTotal.Options.UseForeColor = true;
            this.pgcBudget.Appearance.GrandTotalCell.BackColor = System.Drawing.Color.Orange;
            this.pgcBudget.Appearance.GrandTotalCell.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.pgcBudget.Appearance.GrandTotalCell.Options.UseBackColor = true;
            this.pgcBudget.Appearance.GrandTotalCell.Options.UseFont = true;
            this.pgcBudget.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pgcBudget.Fields.AddRange(new DevExpress.XtraPivotGrid.PivotGridField[] {
            this.pgfGroup,
            this.pgfLedgerCode,
            this.pgfLedgerName,
            this.pgfAmount,
            this.pgfMonthName1});
            this.pgcBudget.Location = new System.Drawing.Point(0, 0);
            this.pgcBudget.Name = "pgcBudget";
            this.pgcBudget.OptionsView.ShowColumnHeaders = false;
            this.pgcBudget.OptionsView.ShowDataHeaders = false;
            this.pgcBudget.OptionsView.ShowFilterHeaders = false;
            this.pgcBudget.Size = new System.Drawing.Size(836, 419);
            this.pgcBudget.TabIndex = 0;
            this.pgcBudget.CustomFieldSort += new DevExpress.XtraPivotGrid.PivotGridCustomFieldSortEventHandler(this.pgcBudget_CustomFieldSort);
            this.pgcBudget.CellDoubleClick += new DevExpress.XtraPivotGrid.PivotCellEventHandler(this.pgcBudget_CellDoubleClick);
            // 
            // pgfGroup
            // 
            this.pgfGroup.Appearance.Header.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.pgfGroup.Appearance.Header.Options.UseFont = true;
            this.pgfGroup.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.pgfGroup.AreaIndex = 0;
            this.pgfGroup.Caption = "Group";
            this.pgfGroup.ExpandedInFieldsGroup = false;
            this.pgfGroup.FieldName = "LEDGER_GROUP";
            this.pgfGroup.Name = "pgfGroup";
            this.pgfGroup.Options.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.pgfGroup.SortMode = DevExpress.XtraPivotGrid.PivotSortMode.None;
            this.pgfGroup.Width = 110;
            // 
            // pgfLedgerCode
            // 
            this.pgfLedgerCode.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.pgfLedgerCode.AreaIndex = 1;
            this.pgfLedgerCode.ExpandedInFieldsGroup = false;
            this.pgfLedgerCode.FieldName = "LEDGER_CODE";
            this.pgfLedgerCode.Name = "pgfLedgerCode";
            this.pgfLedgerCode.Options.AllowExpand = DevExpress.Utils.DefaultBoolean.False;
            this.pgfLedgerCode.Options.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.pgfLedgerCode.SortMode = DevExpress.XtraPivotGrid.PivotSortMode.None;
            this.pgfLedgerCode.Width = 110;
            // 
            // pgfLedgerName
            // 
            this.pgfLedgerName.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.pgfLedgerName.AreaIndex = 2;
            this.pgfLedgerName.Caption = "Ledger Name";
            this.pgfLedgerName.FieldName = "LEDGER_NAME";
            this.pgfLedgerName.Name = "pgfLedgerName";
            this.pgfLedgerName.Options.AllowDrag = DevExpress.Utils.DefaultBoolean.False;
            this.pgfLedgerName.Options.AllowEdit = false;
            this.pgfLedgerName.Options.AllowExpand = DevExpress.Utils.DefaultBoolean.False;
            this.pgfLedgerName.Options.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.pgfLedgerName.SortMode = DevExpress.XtraPivotGrid.PivotSortMode.None;
            this.pgfLedgerName.Width = 130;
            // 
            // pgfAmount
            // 
            this.pgfAmount.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea;
            this.pgfAmount.AreaIndex = 0;
            this.pgfAmount.Caption = "Projected Amount";
            this.pgfAmount.FieldName = "AMOUNT";
            this.pgfAmount.Name = "pgfAmount";
            this.pgfAmount.Options.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.pgfAmount.SortMode = DevExpress.XtraPivotGrid.PivotSortMode.None;
            // 
            // pgfMonthName1
            // 
            this.pgfMonthName1.Appearance.Header.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.pgfMonthName1.Appearance.Header.Options.UseFont = true;
            this.pgfMonthName1.Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea;
            this.pgfMonthName1.AreaIndex = 0;
            this.pgfMonthName1.FieldName = "DURATION";
            this.pgfMonthName1.GroupInterval = DevExpress.XtraPivotGrid.PivotGroupInterval.Custom;
            this.pgfMonthName1.GroupIntervalNumericRange = 12;
            this.pgfMonthName1.Name = "pgfMonthName1";
            this.pgfMonthName1.Options.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.pgfMonthName1.SortMode = DevExpress.XtraPivotGrid.PivotSortMode.Custom;
            this.pgfMonthName1.UnboundFieldName = "pgfMonthName1";
            // 
            // dockPanel1_Container
            // 
            this.dockPanel1_Container.Location = new System.Drawing.Point(3, 22);
            this.dockPanel1_Container.Name = "dockPanel1_Container";
            this.dockPanel1_Container.Size = new System.Drawing.Size(194, 175);
            this.dockPanel1_Container.TabIndex = 0;
            // 
            // frmBudget
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(836, 419);
            this.Controls.Add(this.pgcBudget);
            this.Name = "frmBudget";
            this.Text = "Budget";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmBudget_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pgcBudget)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraPivotGrid.PivotGridControl pgcBudget;
        private DevExpress.XtraPivotGrid.PivotGridField pgfLedgerCode;
        private DevExpress.XtraPivotGrid.PivotGridField pgfLedgerName;
        private DevExpress.XtraPivotGrid.PivotGridField pgfAmount;
        private DevExpress.XtraPivotGrid.PivotGridField pgfMonthName1;
        private DevExpress.XtraPivotGrid.PivotGridField pgfMonthName;
        private DevExpress.XtraPivotGrid.PivotGridField pgfGroup;
        private DevExpress.XtraBars.Docking.ControlContainer dockPanel1_Container;




    }
}