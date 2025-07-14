namespace ACPP.Modules.UIControls
{
    partial class UcTDSSummary
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
            this.lcTransSummary = new DevExpress.XtraLayout.LayoutControl();
            this.gcTDSSummary = new DevExpress.XtraGrid.GridControl();
            this.gvTDSSummary = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colNOPID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNOP = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTransMode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.lcgTransSummary = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lciGridControl = new DevExpress.XtraLayout.LayoutControlItem();
            this.colDefaultValue = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.lcTransSummary)).BeginInit();
            this.lcTransSummary.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcTDSSummary)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvTDSSummary)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgTransSummary)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciGridControl)).BeginInit();
            this.SuspendLayout();
            // 
            // lcTransSummary
            // 
            this.lcTransSummary.Controls.Add(this.gcTDSSummary);
            this.lcTransSummary.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lcTransSummary.Location = new System.Drawing.Point(0, 0);
            this.lcTransSummary.Name = "lcTransSummary";
            this.lcTransSummary.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(529, 98, 250, 350);
            this.lcTransSummary.Root = this.lcgTransSummary;
            this.lcTransSummary.Size = new System.Drawing.Size(386, 255);
            this.lcTransSummary.TabIndex = 0;
            this.lcTransSummary.Text = "layoutControl1";
            // 
            // gcTDSSummary
            // 
            this.gcTDSSummary.Location = new System.Drawing.Point(7, 7);
            this.gcTDSSummary.MainView = this.gvTDSSummary;
            this.gcTDSSummary.Name = "gcTDSSummary";
            this.gcTDSSummary.Size = new System.Drawing.Size(372, 241);
            this.gcTDSSummary.TabIndex = 4;
            this.gcTDSSummary.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvTDSSummary});
            this.gcTDSSummary.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.gcTDSSummary_PreviewKeyDown);
            // 
            // gvTDSSummary
            // 
            this.gvTDSSummary.Appearance.FocusedRow.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            this.gvTDSSummary.Appearance.FocusedRow.Options.UseFont = true;
            this.gvTDSSummary.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.gvTDSSummary.Appearance.HeaderPanel.Options.UseFont = true;
            this.gvTDSSummary.Appearance.Row.BackColor = System.Drawing.Color.PaleGoldenrod;
            this.gvTDSSummary.Appearance.Row.Options.UseBackColor = true;
            this.gvTDSSummary.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.gvTDSSummary.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colNOPID,
            this.colNOP,
            this.colAmount,
            this.colTransMode,
            this.colDefaultValue});
            this.gvTDSSummary.GridControl = this.gcTDSSummary;
            this.gvTDSSummary.Name = "gvTDSSummary";
            this.gvTDSSummary.OptionsBehavior.Editable = false;
            this.gvTDSSummary.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvTDSSummary.OptionsView.ShowColumnHeaders = false;
            this.gvTDSSummary.OptionsView.ShowGroupPanel = false;
            this.gvTDSSummary.OptionsView.ShowIndicator = false;
            this.gvTDSSummary.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.gvTDSSummary_RowCellStyle);
            // 
            // colNOPID
            // 
            this.colNOPID.Caption = "Nature of Payment Id";
            this.colNOPID.FieldName = "NATURE_OF_PAYMENT_ID";
            this.colNOPID.Name = "colNOPID";
            // 
            // colNOP
            // 
            this.colNOP.Caption = "Nature of Payments";
            this.colNOP.FieldName = "NATURE_PAYMENTS";
            this.colNOP.Name = "colNOP";
            this.colNOP.OptionsColumn.AllowFocus = false;
            this.colNOP.OptionsColumn.ShowCaption = false;
            this.colNOP.Visible = true;
            this.colNOP.VisibleIndex = 0;
            this.colNOP.Width = 224;
            // 
            // colAmount
            // 
            this.colAmount.Caption = "Amount";
            this.colAmount.DisplayFormat.FormatString = "N";
            this.colAmount.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colAmount.FieldName = "AMOUNT";
            this.colAmount.GroupFormat.FormatString = "N";
            this.colAmount.GroupFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colAmount.Name = "colAmount";
            this.colAmount.OptionsColumn.AllowFocus = false;
            this.colAmount.OptionsColumn.FixedWidth = true;
            this.colAmount.OptionsColumn.ShowCaption = false;
            this.colAmount.Visible = true;
            this.colAmount.VisibleIndex = 1;
            this.colAmount.Width = 101;
            // 
            // colTransMode
            // 
            this.colTransMode.Caption = "Trans Mode";
            this.colTransMode.FieldName = "TRANS_MODE";
            this.colTransMode.Name = "colTransMode";
            this.colTransMode.OptionsColumn.AllowFocus = false;
            this.colTransMode.OptionsColumn.FixedWidth = true;
            this.colTransMode.OptionsColumn.ShowCaption = false;
            this.colTransMode.Visible = true;
            this.colTransMode.VisibleIndex = 2;
            this.colTransMode.Width = 45;
            // 
            // lcgTransSummary
            // 
            this.lcgTransSummary.CustomizationFormText = "Root";
            this.lcgTransSummary.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.lcgTransSummary.GroupBordersVisible = false;
            this.lcgTransSummary.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lciGridControl});
            this.lcgTransSummary.Location = new System.Drawing.Point(0, 0);
            this.lcgTransSummary.Name = "lcgTransSummary";
            this.lcgTransSummary.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.lcgTransSummary.Size = new System.Drawing.Size(386, 255);
            this.lcgTransSummary.Text = "lcgTransSummary";
            this.lcgTransSummary.TextVisible = false;
            // 
            // lciGridControl
            // 
            this.lciGridControl.Control = this.gcTDSSummary;
            this.lciGridControl.CustomizationFormText = "lciGridControl";
            this.lciGridControl.Location = new System.Drawing.Point(0, 0);
            this.lciGridControl.Name = "lciGridControl";
            this.lciGridControl.Size = new System.Drawing.Size(376, 245);
            this.lciGridControl.Text = "lciGridControl";
            this.lciGridControl.TextSize = new System.Drawing.Size(0, 0);
            this.lciGridControl.TextToControlDistance = 0;
            this.lciGridControl.TextVisible = false;
            // 
            // colDefaultValue
            // 
            this.colDefaultValue.Caption = "Default Value";
            this.colDefaultValue.FieldName = "VALUE";
            this.colDefaultValue.Name = "colDefaultValue";
            // 
            // UcTDSSummary
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lcTransSummary);
            this.Name = "UcTDSSummary";
            this.Size = new System.Drawing.Size(386, 255);
            this.Load += new System.EventHandler(this.UcTDSSummary_Load);
            ((System.ComponentModel.ISupportInitialize)(this.lcTransSummary)).EndInit();
            this.lcTransSummary.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcTDSSummary)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvTDSSummary)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgTransSummary)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciGridControl)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl lcTransSummary;
        private DevExpress.XtraGrid.Views.Grid.GridView gvTDSSummary;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem lciGridControl;
        private DevExpress.XtraGrid.Columns.GridColumn colNOP;
        private DevExpress.XtraGrid.Columns.GridColumn colAmount;
        private DevExpress.XtraGrid.Columns.GridColumn colTransMode;
        private DevExpress.XtraLayout.LayoutControlGroup lcgTransSummary;
        public DevExpress.XtraGrid.GridControl gcTDSSummary;
        private DevExpress.XtraGrid.Columns.GridColumn colNOPID;
        private DevExpress.XtraGrid.Columns.GridColumn colDefaultValue;
    }
}
