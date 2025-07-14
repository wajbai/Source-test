namespace ACPP.Modules.Master
{
    partial class frmBudgetStatisticsDetail
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
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            this.lcBudgetStatisticsDetail = new DevExpress.XtraLayout.LayoutControl();
            this.btnOk = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.gcBudgetStatisticsDetail = new DevExpress.XtraGrid.GridControl();
            this.gvBudgetStatisticsDetail = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colStatisticsType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rglkpStatisticsType = new DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit();
            this.repglkView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colcbStatisticsType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rtxtAmount = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.colBudgetId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDelete = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rbeBudgetStatistcsDetails = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.rbtn = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.lgBudgetStatisticsDetail = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.simpleLabelItem1 = new DevExpress.XtraLayout.SimpleLabelItem();
            this.simpleLabelItem2 = new DevExpress.XtraLayout.SimpleLabelItem();
            ((System.ComponentModel.ISupportInitialize)(this.lcBudgetStatisticsDetail)).BeginInit();
            this.lcBudgetStatisticsDetail.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcBudgetStatisticsDetail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvBudgetStatisticsDetail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rglkpStatisticsType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repglkView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtAmount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbeBudgetStatistcsDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbtn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lgBudgetStatisticsDetail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem2)).BeginInit();
            this.SuspendLayout();
            // 
            // lcBudgetStatisticsDetail
            // 
            this.lcBudgetStatisticsDetail.Controls.Add(this.btnOk);
            this.lcBudgetStatisticsDetail.Controls.Add(this.btnCancel);
            this.lcBudgetStatisticsDetail.Controls.Add(this.gcBudgetStatisticsDetail);
            this.lcBudgetStatisticsDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lcBudgetStatisticsDetail.Location = new System.Drawing.Point(5, 5);
            this.lcBudgetStatisticsDetail.Margin = new System.Windows.Forms.Padding(2);
            this.lcBudgetStatisticsDetail.Name = "lcBudgetStatisticsDetail";
            this.lcBudgetStatisticsDetail.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(532, 100, 250, 350);
            this.lcBudgetStatisticsDetail.Root = this.lgBudgetStatisticsDetail;
            this.lcBudgetStatisticsDetail.Size = new System.Drawing.Size(418, 410);
            this.lcBudgetStatisticsDetail.TabIndex = 0;
            this.lcBudgetStatisticsDetail.Text = "layoutControl1";
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(288, 384);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(61, 22);
            this.btnOk.StyleController = this.lcBudgetStatisticsDetail;
            this.btnOk.TabIndex = 11;
            this.btnOk.Text = "&Ok";
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(353, 384);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(61, 22);
            this.btnCancel.StyleController = this.lcBudgetStatisticsDetail;
            this.btnCancel.TabIndex = 12;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // gcBudgetStatisticsDetail
            // 
            this.gcBudgetStatisticsDetail.Location = new System.Drawing.Point(4, 4);
            this.gcBudgetStatisticsDetail.MainView = this.gvBudgetStatisticsDetail;
            this.gcBudgetStatisticsDetail.Name = "gcBudgetStatisticsDetail";
            this.gcBudgetStatisticsDetail.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rglkpStatisticsType,
            this.rtxtAmount,
            this.rbtn,
            this.rbeBudgetStatistcsDetails});
            this.gcBudgetStatisticsDetail.Size = new System.Drawing.Size(410, 376);
            this.gcBudgetStatisticsDetail.TabIndex = 1;
            this.gcBudgetStatisticsDetail.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvBudgetStatisticsDetail});
            this.gcBudgetStatisticsDetail.ProcessGridKey += new System.Windows.Forms.KeyEventHandler(this.gcBudgetStatisticsDetail_ProcessGridKey);
            // 
            // gvBudgetStatisticsDetail
            // 
            this.gvBudgetStatisticsDetail.Appearance.FocusedCell.BackColor = System.Drawing.Color.LightSteelBlue;
            this.gvBudgetStatisticsDetail.Appearance.FocusedCell.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.gvBudgetStatisticsDetail.Appearance.FocusedCell.Options.UseBackColor = true;
            this.gvBudgetStatisticsDetail.Appearance.FocusedCell.Options.UseFont = true;
            this.gvBudgetStatisticsDetail.Appearance.FocusedRow.BackColor2 = System.Drawing.Color.LightBlue;
            this.gvBudgetStatisticsDetail.Appearance.FocusedRow.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.gvBudgetStatisticsDetail.Appearance.FocusedRow.Options.UseFont = true;
            this.gvBudgetStatisticsDetail.Appearance.FooterPanel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.gvBudgetStatisticsDetail.Appearance.FooterPanel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.gvBudgetStatisticsDetail.Appearance.FooterPanel.Options.UseFont = true;
            this.gvBudgetStatisticsDetail.Appearance.FooterPanel.Options.UseForeColor = true;
            this.gvBudgetStatisticsDetail.Appearance.HeaderPanel.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            this.gvBudgetStatisticsDetail.Appearance.HeaderPanel.Options.UseFont = true;
            this.gvBudgetStatisticsDetail.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colStatisticsType,
            this.colCount,
            this.colBudgetId,
            this.colDelete});
            this.gvBudgetStatisticsDetail.GridControl = this.gcBudgetStatisticsDetail;
            this.gvBudgetStatisticsDetail.Name = "gvBudgetStatisticsDetail";
            this.gvBudgetStatisticsDetail.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.gvBudgetStatisticsDetail.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            this.gvBudgetStatisticsDetail.OptionsBehavior.EditingMode = DevExpress.XtraGrid.Views.Grid.GridEditingMode.Inplace;
            this.gvBudgetStatisticsDetail.OptionsBehavior.ImmediateUpdateRowPosition = false;
            this.gvBudgetStatisticsDetail.OptionsCustomization.AllowColumnMoving = false;
            this.gvBudgetStatisticsDetail.OptionsCustomization.AllowFilter = false;
            this.gvBudgetStatisticsDetail.OptionsCustomization.AllowGroup = false;
            this.gvBudgetStatisticsDetail.OptionsCustomization.AllowSort = false;
            this.gvBudgetStatisticsDetail.OptionsNavigation.EnterMoveNextColumn = true;
            this.gvBudgetStatisticsDetail.OptionsView.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
            this.gvBudgetStatisticsDetail.OptionsView.ShowFooter = true;
            this.gvBudgetStatisticsDetail.OptionsView.ShowGroupPanel = false;
            this.gvBudgetStatisticsDetail.OptionsView.ShowIndicator = false;
            this.gvBudgetStatisticsDetail.ShownEditor += new System.EventHandler(this.gvBudgetStatisticsDetail_ShownEditor);
            this.gvBudgetStatisticsDetail.ValidateRow += new DevExpress.XtraGrid.Views.Base.ValidateRowEventHandler(this.gvBudgetStatisticsDetail_ValidateRow);
            // 
            // colStatisticsType
            // 
            this.colStatisticsType.Caption = "Statistics Type";
            this.colStatisticsType.ColumnEdit = this.rglkpStatisticsType;
            this.colStatisticsType.FieldName = "STATISTICS_TYPE_ID";
            this.colStatisticsType.Name = "colStatisticsType";
            this.colStatisticsType.Visible = true;
            this.colStatisticsType.VisibleIndex = 0;
            this.colStatisticsType.Width = 339;
            // 
            // rglkpStatisticsType
            // 
            this.rglkpStatisticsType.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rglkpStatisticsType.DisplayMember = "STATISTICS_TYPE";
            this.rglkpStatisticsType.ImmediatePopup = true;
            this.rglkpStatisticsType.Name = "rglkpStatisticsType";
            this.rglkpStatisticsType.NullText = "";
            this.rglkpStatisticsType.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            this.rglkpStatisticsType.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.rglkpStatisticsType.ValueMember = "STATISTICS_TYPE_ID";
            this.rglkpStatisticsType.View = this.repglkView;
            // 
            // repglkView
            // 
            this.repglkView.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.repglkView.Appearance.HeaderPanel.Options.UseFont = true;
            this.repglkView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colcbStatisticsType});
            this.repglkView.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.repglkView.Name = "repglkView";
            this.repglkView.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.repglkView.OptionsView.ShowGroupPanel = false;
            this.repglkView.OptionsView.ShowIndicator = false;
            // 
            // colcbStatisticsType
            // 
            this.colcbStatisticsType.Caption = "Statistics Type";
            this.colcbStatisticsType.FieldName = "STATISTICS_TYPE";
            this.colcbStatisticsType.Name = "colcbStatisticsType";
            this.colcbStatisticsType.Visible = true;
            this.colcbStatisticsType.VisibleIndex = 0;
            // 
            // colCount
            // 
            this.colCount.AppearanceCell.BackColor = System.Drawing.Color.LightYellow;
            this.colCount.AppearanceCell.Options.UseBackColor = true;
            this.colCount.AppearanceHeader.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            this.colCount.AppearanceHeader.Options.UseFont = true;
            this.colCount.Caption = "No of Total";
            this.colCount.ColumnEdit = this.rtxtAmount;
            this.colCount.FieldName = "TOTAL_COUNT";
            this.colCount.Name = "colCount";
            this.colCount.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "TOTAL_COUNT", "{0:n0}")});
            this.colCount.Visible = true;
            this.colCount.VisibleIndex = 1;
            this.colCount.Width = 147;
            // 
            // rtxtAmount
            // 
            this.rtxtAmount.AutoHeight = false;
            this.rtxtAmount.Mask.EditMask = "n0";
            this.rtxtAmount.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.rtxtAmount.Mask.UseMaskAsDisplayFormat = true;
            this.rtxtAmount.Name = "rtxtAmount";
            // 
            // colBudgetId
            // 
            this.colBudgetId.Caption = "BudgetId";
            this.colBudgetId.FieldName = "BUDGET_ID";
            this.colBudgetId.Name = "colBudgetId";
            this.colBudgetId.OptionsColumn.AllowEdit = false;
            this.colBudgetId.OptionsColumn.AllowFocus = false;
            this.colBudgetId.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.colBudgetId.OptionsColumn.AllowMove = false;
            this.colBudgetId.OptionsColumn.AllowSize = false;
            this.colBudgetId.OptionsColumn.TabStop = false;
            this.colBudgetId.Width = 23;
            // 
            // colDelete
            // 
            this.colDelete.Caption = "Delete";
            this.colDelete.ColumnEdit = this.rbeBudgetStatistcsDetails;
            this.colDelete.Name = "colDelete";
            this.colDelete.OptionsColumn.AllowMove = false;
            this.colDelete.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colDelete.OptionsColumn.FixedWidth = true;
            this.colDelete.OptionsColumn.ShowCaption = false;
            this.colDelete.OptionsColumn.TabStop = false;
            this.colDelete.ToolTip = "Click to delete";
            this.colDelete.Visible = true;
            this.colDelete.VisibleIndex = 2;
            this.colDelete.Width = 26;
            // 
            // rbeBudgetStatistcsDetails
            // 
            this.rbeBudgetStatistcsDetails.AutoHeight = false;
            this.rbeBudgetStatistcsDetails.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, global::ACPP.Properties.Resources.Delete_Mob, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, "", null, null, true)});
            this.rbeBudgetStatistcsDetails.Name = "rbeBudgetStatistcsDetails";
            this.rbeBudgetStatistcsDetails.Click += new System.EventHandler(this.rbeBudgetStatistcsDetails_Click);
            // 
            // rbtn
            // 
            this.rbtn.AutoHeight = false;
            this.rbtn.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.rbtn.Name = "rbtn";
            // 
            // lgBudgetStatisticsDetail
            // 
            this.lgBudgetStatisticsDetail.CustomizationFormText = "Root";
            this.lgBudgetStatisticsDetail.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.lgBudgetStatisticsDetail.GroupBordersVisible = false;
            this.lgBudgetStatisticsDetail.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.simpleLabelItem1});
            this.lgBudgetStatisticsDetail.Location = new System.Drawing.Point(0, 0);
            this.lgBudgetStatisticsDetail.Name = "Root";
            this.lgBudgetStatisticsDetail.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 2, 2);
            this.lgBudgetStatisticsDetail.Size = new System.Drawing.Size(418, 410);
            this.lgBudgetStatisticsDetail.Text = "Root";
            this.lgBudgetStatisticsDetail.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gcBudgetStatisticsDetail;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(414, 380);
            this.layoutControlItem1.Text = "layoutControlItem1";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.btnCancel;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem2";
            this.layoutControlItem2.Location = new System.Drawing.Point(349, 380);
            this.layoutControlItem2.MaxSize = new System.Drawing.Size(65, 26);
            this.layoutControlItem2.MinSize = new System.Drawing.Size(65, 26);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(65, 26);
            this.layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem2.Text = "layoutControlItem2";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.btnOk;
            this.layoutControlItem3.CustomizationFormText = "layoutControlItem3";
            this.layoutControlItem3.Location = new System.Drawing.Point(284, 380);
            this.layoutControlItem3.MaxSize = new System.Drawing.Size(65, 26);
            this.layoutControlItem3.MinSize = new System.Drawing.Size(65, 26);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(65, 26);
            this.layoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem3.Text = "layoutControlItem3";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextToControlDistance = 0;
            this.layoutControlItem3.TextVisible = false;
            // 
            // simpleLabelItem1
            // 
            this.simpleLabelItem1.AllowHotTrack = false;
            this.simpleLabelItem1.AllowHtmlStringInCaption = true;
            this.simpleLabelItem1.CustomizationFormText = " <b><color=\"Blue\">  Alt + D </color> Delete Trans </b>";
            this.simpleLabelItem1.Location = new System.Drawing.Point(0, 380);
            this.simpleLabelItem1.Name = "simpleLabelItem1";
            this.simpleLabelItem1.Size = new System.Drawing.Size(284, 26);
            this.simpleLabelItem1.Text = " <b><color=\"Blue\">  Alt + D </color> Delete </b>";
            this.simpleLabelItem1.TextSize = new System.Drawing.Size(94, 13);
            // 
            // simpleLabelItem2
            // 
            this.simpleLabelItem2.AllowHotTrack = false;
            this.simpleLabelItem2.AllowHtmlStringInCaption = true;
            this.simpleLabelItem2.CustomizationFormText = " <b><color=\"Blue\">  Alt + D </color> Delete Trans </b>";
            this.simpleLabelItem2.Location = new System.Drawing.Point(0, 397);
            this.simpleLabelItem2.Name = "simpleLabelItem2";
            this.simpleLabelItem2.Size = new System.Drawing.Size(216, 26);
            this.simpleLabelItem2.Text = " <b><color=\"Blue\">  Alt + D </color> Delete Trans </b>";
            this.simpleLabelItem2.TextSize = new System.Drawing.Size(129, 13);
            // 
            // frmBudgetStatisticsDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(428, 420);
            this.Controls.Add(this.lcBudgetStatisticsDetail);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmBudgetStatisticsDetail";
            this.Text = "Budget Statistics Detail";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmBudgetStatisticsDetail_FormClosing);
            this.Load += new System.EventHandler(this.frmBudgetStatisticsDetail_Load);
            this.Shown += new System.EventHandler(this.frmBudgetStatisticsDetail_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.lcBudgetStatisticsDetail)).EndInit();
            this.lcBudgetStatisticsDetail.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcBudgetStatisticsDetail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvBudgetStatisticsDetail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rglkpStatisticsType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repglkView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtAmount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbeBudgetStatistcsDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbtn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lgBudgetStatisticsDetail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl lcBudgetStatisticsDetail;
        private DevExpress.XtraLayout.LayoutControlGroup lgBudgetStatisticsDetail;
        private DevExpress.XtraGrid.GridControl gcBudgetStatisticsDetail;
        private DevExpress.XtraGrid.Views.Grid.GridView gvBudgetStatisticsDetail;
        private DevExpress.XtraGrid.Columns.GridColumn colStatisticsType;
        private DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit rglkpStatisticsType;
        private DevExpress.XtraGrid.Views.Grid.GridView repglkView;
        private DevExpress.XtraGrid.Columns.GridColumn colcbStatisticsType;
        private DevExpress.XtraGrid.Columns.GridColumn colCount;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit rtxtAmount;
        private DevExpress.XtraGrid.Columns.GridColumn colBudgetId;
        private DevExpress.XtraGrid.Columns.GridColumn colDelete;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit rbeBudgetStatistcsDetails;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit rbtn;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraEditors.SimpleButton btnOk;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.SimpleLabelItem simpleLabelItem2;
        private DevExpress.XtraLayout.SimpleLabelItem simpleLabelItem1;

    }
}