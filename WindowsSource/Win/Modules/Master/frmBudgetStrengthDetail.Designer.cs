namespace ACPP.Modules.Master
{
    partial class frmBudgetStrengthDetail
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
            this.gcBudgetStrengthDetail = new DevExpress.XtraGrid.GridControl();
            this.gvBudgetStrengthDetail = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colBudgetId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCC = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rglkpCC = new DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit();
            this.repglkView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colcbCostCentre = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNewCount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rtxtNewCount = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.colPresentCount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rtxtPresentCount = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
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
            ((System.ComponentModel.ISupportInitialize)(this.gcBudgetStrengthDetail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvBudgetStrengthDetail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rglkpCC)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repglkView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtNewCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtPresentCount)).BeginInit();
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
            this.lcBudgetStatisticsDetail.Controls.Add(this.gcBudgetStrengthDetail);
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
            // gcBudgetStrengthDetail
            // 
            this.gcBudgetStrengthDetail.Location = new System.Drawing.Point(4, 4);
            this.gcBudgetStrengthDetail.MainView = this.gvBudgetStrengthDetail;
            this.gcBudgetStrengthDetail.Name = "gcBudgetStrengthDetail";
            this.gcBudgetStrengthDetail.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rglkpCC,
            this.rtxtNewCount,
            this.rbtn,
            this.rbeBudgetStatistcsDetails,
            this.rtxtPresentCount});
            this.gcBudgetStrengthDetail.Size = new System.Drawing.Size(410, 376);
            this.gcBudgetStrengthDetail.TabIndex = 1;
            this.gcBudgetStrengthDetail.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvBudgetStrengthDetail});
            this.gcBudgetStrengthDetail.ProcessGridKey += new System.Windows.Forms.KeyEventHandler(this.gcBudgetStrengthDetail_ProcessGridKey);
            // 
            // gvBudgetStrengthDetail
            // 
            this.gvBudgetStrengthDetail.Appearance.FocusedCell.BackColor = System.Drawing.Color.LightSteelBlue;
            this.gvBudgetStrengthDetail.Appearance.FocusedCell.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.gvBudgetStrengthDetail.Appearance.FocusedCell.Options.UseBackColor = true;
            this.gvBudgetStrengthDetail.Appearance.FocusedCell.Options.UseFont = true;
            this.gvBudgetStrengthDetail.Appearance.FocusedRow.BackColor2 = System.Drawing.Color.LightBlue;
            this.gvBudgetStrengthDetail.Appearance.FocusedRow.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.gvBudgetStrengthDetail.Appearance.FocusedRow.Options.UseFont = true;
            this.gvBudgetStrengthDetail.Appearance.FooterPanel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.gvBudgetStrengthDetail.Appearance.FooterPanel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.gvBudgetStrengthDetail.Appearance.FooterPanel.Options.UseFont = true;
            this.gvBudgetStrengthDetail.Appearance.FooterPanel.Options.UseForeColor = true;
            this.gvBudgetStrengthDetail.Appearance.HeaderPanel.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            this.gvBudgetStrengthDetail.Appearance.HeaderPanel.Options.UseFont = true;
            this.gvBudgetStrengthDetail.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colBudgetId,
            this.colCC,
            this.colNewCount,
            this.colPresentCount,
            this.colDelete});
            this.gvBudgetStrengthDetail.GridControl = this.gcBudgetStrengthDetail;
            this.gvBudgetStrengthDetail.Name = "gvBudgetStrengthDetail";
            this.gvBudgetStrengthDetail.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.gvBudgetStrengthDetail.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            this.gvBudgetStrengthDetail.OptionsBehavior.EditingMode = DevExpress.XtraGrid.Views.Grid.GridEditingMode.Inplace;
            this.gvBudgetStrengthDetail.OptionsBehavior.ImmediateUpdateRowPosition = false;
            this.gvBudgetStrengthDetail.OptionsCustomization.AllowColumnMoving = false;
            this.gvBudgetStrengthDetail.OptionsCustomization.AllowFilter = false;
            this.gvBudgetStrengthDetail.OptionsCustomization.AllowGroup = false;
            this.gvBudgetStrengthDetail.OptionsCustomization.AllowSort = false;
            this.gvBudgetStrengthDetail.OptionsNavigation.EnterMoveNextColumn = true;
            this.gvBudgetStrengthDetail.OptionsView.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
            this.gvBudgetStrengthDetail.OptionsView.ShowFooter = true;
            this.gvBudgetStrengthDetail.OptionsView.ShowGroupPanel = false;
            this.gvBudgetStrengthDetail.OptionsView.ShowIndicator = false;
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
            // colCC
            // 
            this.colCC.Caption = "Cost Centre";
            this.colCC.ColumnEdit = this.rglkpCC;
            this.colCC.FieldName = "COST_CENTRE_ID";
            this.colCC.Name = "colCC";
            this.colCC.Visible = true;
            this.colCC.VisibleIndex = 0;
            this.colCC.Width = 339;
            // 
            // rglkpCC
            // 
            this.rglkpCC.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rglkpCC.DisplayMember = "COST_CENTRE_NAME";
            this.rglkpCC.ImmediatePopup = true;
            this.rglkpCC.Name = "rglkpCC";
            this.rglkpCC.NullText = "";
            this.rglkpCC.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            this.rglkpCC.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.rglkpCC.ValueMember = "COST_CENTRE_ID";
            this.rglkpCC.View = this.repglkView;
            // 
            // repglkView
            // 
            this.repglkView.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.repglkView.Appearance.HeaderPanel.Options.UseFont = true;
            this.repglkView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colcbCostCentre});
            this.repglkView.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.repglkView.Name = "repglkView";
            this.repglkView.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.repglkView.OptionsView.ShowGroupPanel = false;
            this.repglkView.OptionsView.ShowIndicator = false;
            // 
            // colcbCostCentre
            // 
            this.colcbCostCentre.Caption = "Cost Centre";
            this.colcbCostCentre.FieldName = "COST_CENTRE_NAME";
            this.colcbCostCentre.Name = "colcbCostCentre";
            this.colcbCostCentre.Visible = true;
            this.colcbCostCentre.VisibleIndex = 0;
            // 
            // colNewCount
            // 
            this.colNewCount.AppearanceCell.BackColor = System.Drawing.Color.LightYellow;
            this.colNewCount.AppearanceCell.Options.UseBackColor = true;
            this.colNewCount.AppearanceHeader.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            this.colNewCount.AppearanceHeader.Options.UseFont = true;
            this.colNewCount.Caption = "New";
            this.colNewCount.ColumnEdit = this.rtxtNewCount;
            this.colNewCount.FieldName = "NEW_COUNT";
            this.colNewCount.Name = "colNewCount";
            this.colNewCount.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "NEW_COUNT", "{0:n0}")});
            this.colNewCount.Visible = true;
            this.colNewCount.VisibleIndex = 1;
            this.colNewCount.Width = 147;
            // 
            // rtxtNewCount
            // 
            this.rtxtNewCount.AutoHeight = false;
            this.rtxtNewCount.Mask.EditMask = "n0";
            this.rtxtNewCount.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.rtxtNewCount.Mask.UseMaskAsDisplayFormat = true;
            this.rtxtNewCount.Name = "rtxtNewCount";
            // 
            // colPresentCount
            // 
            this.colPresentCount.AppearanceCell.BackColor = System.Drawing.Color.LightYellow;
            this.colPresentCount.AppearanceCell.Options.UseBackColor = true;
            this.colPresentCount.AppearanceHeader.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            this.colPresentCount.AppearanceHeader.Options.UseFont = true;
            this.colPresentCount.Caption = "Present";
            this.colPresentCount.ColumnEdit = this.rtxtPresentCount;
            this.colPresentCount.FieldName = "PRESENT_COUNT";
            this.colPresentCount.Name = "colPresentCount";
            this.colPresentCount.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "PRESENT_COUNT", "{0:n0}")});
            this.colPresentCount.Visible = true;
            this.colPresentCount.VisibleIndex = 2;
            // 
            // rtxtPresentCount
            // 
            this.rtxtPresentCount.AutoHeight = false;
            this.rtxtPresentCount.Mask.EditMask = "n0";
            this.rtxtPresentCount.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.rtxtPresentCount.Mask.UseMaskAsDisplayFormat = true;
            this.rtxtPresentCount.Name = "rtxtPresentCount";
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
            this.colDelete.VisibleIndex = 3;
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
            this.layoutControlItem1.Control = this.gcBudgetStrengthDetail;
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
            // frmBudgetStrengthDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(428, 420);
            this.Controls.Add(this.lcBudgetStatisticsDetail);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmBudgetStrengthDetail";
            this.Text = "Budget Strength Detail";
            this.Load += new System.EventHandler(this.frmBudgetStrengthDetail_Load);
            this.Shown += new System.EventHandler(this.frmBudgetStrengthDetail_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.lcBudgetStatisticsDetail)).EndInit();
            this.lcBudgetStatisticsDetail.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcBudgetStrengthDetail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvBudgetStrengthDetail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rglkpCC)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repglkView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtNewCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtPresentCount)).EndInit();
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
        private DevExpress.XtraGrid.GridControl gcBudgetStrengthDetail;
        private DevExpress.XtraGrid.Views.Grid.GridView gvBudgetStrengthDetail;
        private DevExpress.XtraGrid.Columns.GridColumn colCC;
        private DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit rglkpCC;
        private DevExpress.XtraGrid.Views.Grid.GridView repglkView;
        private DevExpress.XtraGrid.Columns.GridColumn colcbCostCentre;
        private DevExpress.XtraGrid.Columns.GridColumn colNewCount;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit rtxtNewCount;
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
        private DevExpress.XtraGrid.Columns.GridColumn colPresentCount;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit rtxtPresentCount;

    }
}