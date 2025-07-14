namespace ACPP.Modules.UIControls
{
    partial class ucTransBalance
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucTransBalance));
            this.popupContainerControl1 = new DevExpress.XtraEditors.PopupContainerControl();
            this.gcTransBalance = new DevExpress.XtraGrid.GridControl();
            this.gvTransBalance = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colLedgerId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBaseLedgerName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLedgerName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCurrency = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.popupContainerControl1)).BeginInit();
            this.popupContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcTransBalance)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvTransBalance)).BeginInit();
            this.SuspendLayout();
            // 
            // popupContainerControl1
            // 
            this.popupContainerControl1.Appearance.BackColor = ((System.Drawing.Color)(resources.GetObject("popupContainerControl1.Appearance.BackColor")));
            this.popupContainerControl1.Appearance.Options.UseBackColor = true;
            this.popupContainerControl1.Controls.Add(this.gcTransBalance);
            resources.ApplyResources(this.popupContainerControl1, "popupContainerControl1");
            this.popupContainerControl1.Name = "popupContainerControl1";
            // 
            // gcTransBalance
            // 
            resources.ApplyResources(this.gcTransBalance, "gcTransBalance");
            this.gcTransBalance.MainView = this.gvTransBalance;
            this.gcTransBalance.Name = "gcTransBalance";
            this.gcTransBalance.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvTransBalance});
            // 
            // gvTransBalance
            // 
            this.gvTransBalance.Appearance.HeaderPanel.Font = ((System.Drawing.Font)(resources.GetObject("gvTransBalance.Appearance.HeaderPanel.Font")));
            this.gvTransBalance.Appearance.HeaderPanel.Options.UseFont = true;
            this.gvTransBalance.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colLedgerId,
            this.colBaseLedgerName,
            this.colLedgerName,
            this.colCurrency,
            this.colAmount,
            this.colType});
            this.gvTransBalance.GridControl = this.gcTransBalance;
            this.gvTransBalance.GroupCount = 1;
            this.gvTransBalance.GroupSummary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridGroupSummaryItem(((DevExpress.Data.SummaryItemType)(resources.GetObject("gvTransBalance.GroupSummary"))), resources.GetString("gvTransBalance.GroupSummary1"), ((DevExpress.XtraGrid.Columns.GridColumn)(resources.GetObject("gvTransBalance.GroupSummary2"))), resources.GetString("gvTransBalance.GroupSummary3"))});
            this.gvTransBalance.Name = "gvTransBalance";
            this.gvTransBalance.OptionsBehavior.Editable = false;
            this.gvTransBalance.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvTransBalance.OptionsView.ShowGroupPanel = false;
            this.gvTransBalance.OptionsView.ShowIndicator = false;
            this.gvTransBalance.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.colBaseLedgerName, DevExpress.Data.ColumnSortOrder.Ascending)});
            // 
            // colLedgerId
            // 
            resources.ApplyResources(this.colLedgerId, "colLedgerId");
            this.colLedgerId.FieldName = "ID";
            this.colLedgerId.Name = "colLedgerId";
            // 
            // colBaseLedgerName
            // 
            resources.ApplyResources(this.colBaseLedgerName, "colBaseLedgerName");
            this.colBaseLedgerName.FieldName = "BASE_LEDGER_NAME";
            this.colBaseLedgerName.Name = "colBaseLedgerName";
            // 
            // colLedgerName
            // 
            resources.ApplyResources(this.colLedgerName, "colLedgerName");
            this.colLedgerName.FieldName = "LEDGER_NAME";
            this.colLedgerName.Name = "colLedgerName";
            // 
            // colAmount
            // 
            this.colAmount.AppearanceHeader.Options.UseTextOptions = true;
            this.colAmount.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            resources.ApplyResources(this.colAmount, "colAmount");
            this.colAmount.DisplayFormat.FormatString = "N";
            this.colAmount.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colAmount.FieldName = "AMOUNT";
            this.colAmount.Name = "colAmount";
            // 
            // colType
            // 
            this.colType.FieldName = "TRANSMODE";
            this.colType.Name = "colType";
            this.colType.OptionsColumn.ShowCaption = false;
            this.colType.OptionsFilter.AllowAutoFilter = false;
            this.colType.OptionsFilter.AllowFilter = false;
            resources.ApplyResources(this.colType, "colType");
            // 
            // colCurrency
            // 
            resources.ApplyResources(this.colCurrency, "colCurrency");
            this.colCurrency.FieldName = "CURRENCY_SYMBOL";
            this.colCurrency.Name = "colCurrency";
            this.colCurrency.OptionsColumn.FixedWidth = true;
            // 
            // ucTransBalance
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.popupContainerControl1);
            this.Name = "ucTransBalance";
            ((System.ComponentModel.ISupportInitialize)(this.popupContainerControl1)).EndInit();
            this.popupContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcTransBalance)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvTransBalance)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PopupContainerControl popupContainerControl1;
        private DevExpress.XtraGrid.GridControl gcTransBalance;
        private DevExpress.XtraGrid.Views.Grid.GridView gvTransBalance;
        private DevExpress.XtraGrid.Columns.GridColumn colLedgerId;
        private DevExpress.XtraGrid.Columns.GridColumn colLedgerName;
        private DevExpress.XtraGrid.Columns.GridColumn colAmount;
        private DevExpress.XtraGrid.Columns.GridColumn colType;
        private DevExpress.XtraGrid.Columns.GridColumn colBaseLedgerName;
        private DevExpress.XtraGrid.Columns.GridColumn colCurrency;
    }
}
