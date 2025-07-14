namespace ACPP.Modules.Master
{
    partial class frmProjectAllotedFunds
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
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.gcProjectFunds = new DevExpress.XtraGrid.GridControl();
            this.gvProjectFunds = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.simpleLabelItem2 = new DevExpress.XtraLayout.SimpleLabelItem();
            this.colBudgetId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProjectId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProposedAmt = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colApprovedAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProject = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rtxtProposedAmt = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.rtxtApprovedAmount = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.btnOk = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcProjectFunds)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvProjectFunds)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtProposedAmt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtApprovedAmount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.btnOk);
            this.layoutControl1.Controls.Add(this.btnCancel);
            this.layoutControl1.Controls.Add(this.gcProjectFunds);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(724, 249, 250, 350);
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(435, 433);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "Root";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.simpleLabelItem2,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.emptySpaceItem1});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.layoutControlGroup1.Size = new System.Drawing.Size(435, 433);
            this.layoutControlGroup1.Text = "Root";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // gcProjectFunds
            // 
            this.gcProjectFunds.Location = new System.Drawing.Point(7, 31);
            this.gcProjectFunds.MainView = this.gvProjectFunds;
            this.gcProjectFunds.Name = "gcProjectFunds";
            this.gcProjectFunds.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rtxtProposedAmt,
            this.rtxtApprovedAmount});
            this.gcProjectFunds.Size = new System.Drawing.Size(421, 369);
            this.gcProjectFunds.TabIndex = 4;
            this.gcProjectFunds.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvProjectFunds});
            // 
            // gvProjectFunds
            // 
            this.gvProjectFunds.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colBudgetId,
            this.colProjectId,
            this.colProject,
            this.colProposedAmt,
            this.colApprovedAmount});
            this.gvProjectFunds.GridControl = this.gcProjectFunds;
            this.gvProjectFunds.Name = "gvProjectFunds";
            this.gvProjectFunds.OptionsBehavior.ImmediateUpdateRowPosition = false;
            this.gvProjectFunds.OptionsNavigation.EnterMoveNextColumn = true;
            this.gvProjectFunds.OptionsView.ShowGroupPanel = false;
            this.gvProjectFunds.OptionsView.ShowIndicator = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gcProjectFunds;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 24);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(425, 373);
            this.layoutControlItem1.Text = "layoutControlItem1";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // simpleLabelItem2
            // 
            this.simpleLabelItem2.AllowHotTrack = false;
            this.simpleLabelItem2.CustomizationFormText = " ";
            this.simpleLabelItem2.Location = new System.Drawing.Point(0, 0);
            this.simpleLabelItem2.MinSize = new System.Drawing.Size(111, 17);
            this.simpleLabelItem2.Name = "simpleLabelItem2";
            this.simpleLabelItem2.Size = new System.Drawing.Size(425, 24);
            this.simpleLabelItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.simpleLabelItem2.Text = " ";
            this.simpleLabelItem2.TextSize = new System.Drawing.Size(3, 13);
            // 
            // colBudgetId
            // 
            this.colBudgetId.AppearanceHeader.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            this.colBudgetId.AppearanceHeader.Options.UseFont = true;
            this.colBudgetId.Caption = "Budget Id";
            this.colBudgetId.FieldName = "BUDGET_ID";
            this.colBudgetId.Name = "colBudgetId";
            this.colBudgetId.Width = 79;
            // 
            // colProjectId
            // 
            this.colProjectId.Caption = "ProjectId";
            this.colProjectId.FieldName = "PROJECT_ID";
            this.colProjectId.Name = "colProjectId";
            // 
            // colProposedAmt
            // 
            this.colProposedAmt.AppearanceCell.BackColor = System.Drawing.Color.LightYellow;
            this.colProposedAmt.AppearanceCell.Options.UseBackColor = true;
            this.colProposedAmt.AppearanceHeader.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            this.colProposedAmt.AppearanceHeader.Options.UseFont = true;
            this.colProposedAmt.Caption = "Proposed Amt";
            this.colProposedAmt.ColumnEdit = this.rtxtProposedAmt;
            this.colProposedAmt.FieldName = "PROPOSED_AMOUNT";
            this.colProposedAmt.Name = "colProposedAmt";
            this.colProposedAmt.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "PROPOSED_AMOUNT", "{0:C}")});
            this.colProposedAmt.Visible = true;
            this.colProposedAmt.VisibleIndex = 1;
            this.colProposedAmt.Width = 127;
            // 
            // colApprovedAmount
            // 
            this.colApprovedAmount.AppearanceCell.BackColor = System.Drawing.Color.LightYellow;
            this.colApprovedAmount.AppearanceCell.Options.UseBackColor = true;
            this.colApprovedAmount.AppearanceHeader.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            this.colApprovedAmount.AppearanceHeader.Options.UseFont = true;
            this.colApprovedAmount.Caption = "Approved Amt";
            this.colApprovedAmount.ColumnEdit = this.rtxtApprovedAmount;
            this.colApprovedAmount.FieldName = "APPROVED_AMOUNT";
            this.colApprovedAmount.Name = "colApprovedAmount";
            this.colApprovedAmount.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "APPROVED_AMOUNT", "{0:C}")});
            this.colApprovedAmount.Visible = true;
            this.colApprovedAmount.VisibleIndex = 2;
            this.colApprovedAmount.Width = 134;
            // 
            // colProject
            // 
            this.colProject.AppearanceHeader.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            this.colProject.AppearanceHeader.Options.UseFont = true;
            this.colProject.Caption = "Project";
            this.colProject.FieldName = "PROJECT";
            this.colProject.Name = "colProject";
            this.colProject.Visible = true;
            this.colProject.VisibleIndex = 0;
            this.colProject.Width = 158;
            // 
            // rtxtProposedAmt
            // 
            this.rtxtProposedAmt.AutoHeight = false;
            this.rtxtProposedAmt.Name = "rtxtProposedAmt";
            // 
            // rtxtApprovedAmount
            // 
            this.rtxtApprovedAmount.AutoHeight = false;
            this.rtxtApprovedAmount.Name = "rtxtApprovedAmount";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(358, 404);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(70, 22);
            this.btnCancel.StyleController = this.layoutControl1;
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.btnCancel;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem2";
            this.layoutControlItem2.Location = new System.Drawing.Point(351, 397);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(74, 26);
            this.layoutControlItem2.Text = "layoutControlItem2";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(282, 404);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(72, 22);
            this.btnOk.StyleController = this.layoutControl1;
            this.btnOk.TabIndex = 6;
            this.btnOk.Text = "Ok";
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.btnOk;
            this.layoutControlItem3.CustomizationFormText = "layoutControlItem3";
            this.layoutControlItem3.Location = new System.Drawing.Point(275, 397);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(76, 26);
            this.layoutControlItem3.Text = "layoutControlItem3";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextToControlDistance = 0;
            this.layoutControlItem3.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.CustomizationFormText = "emptySpaceItem1";
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 397);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(275, 26);
            this.emptySpaceItem1.Text = "emptySpaceItem1";
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // frmProjectAllotedFunds
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(435, 433);
            this.Controls.Add(this.layoutControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmProjectAllotedFunds";
            this.Text = "Project Funds ";
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcProjectFunds)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvProjectFunds)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtProposedAmt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtApprovedAmount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraGrid.GridControl gcProjectFunds;
        private DevExpress.XtraGrid.Views.Grid.GridView gvProjectFunds;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraGrid.Columns.GridColumn colBudgetId;
        private DevExpress.XtraGrid.Columns.GridColumn colProjectId;
        private DevExpress.XtraGrid.Columns.GridColumn colProposedAmt;
        private DevExpress.XtraGrid.Columns.GridColumn colApprovedAmount;
        private DevExpress.XtraLayout.SimpleLabelItem simpleLabelItem2;
        private DevExpress.XtraGrid.Columns.GridColumn colProject;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit rtxtProposedAmt;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit rtxtApprovedAmount;
        private DevExpress.XtraEditors.SimpleButton btnOk;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
    }
}