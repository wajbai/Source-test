namespace ACPP.Modules.UIControls
{
    partial class UcUsedCodesList
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
            this.popupContainerControl1 = new DevExpress.XtraEditors.PopupContainerControl();
            this.gcUsedCodeList = new DevExpress.XtraGrid.GridControl();
            this.gvUsedcodeList = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colUsedCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colName = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.popupContainerControl1)).BeginInit();
            this.popupContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcUsedCodeList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvUsedcodeList)).BeginInit();
            this.SuspendLayout();
            // 
            // popupContainerControl1
            // 
            this.popupContainerControl1.Controls.Add(this.gcUsedCodeList);
            this.popupContainerControl1.Location = new System.Drawing.Point(0, 0);
            this.popupContainerControl1.Name = "popupContainerControl1";
            this.popupContainerControl1.Size = new System.Drawing.Size(285, 162);
            this.popupContainerControl1.TabIndex = 0;
            // 
            // gcUsedCodeList
            // 
            this.gcUsedCodeList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcUsedCodeList.Location = new System.Drawing.Point(0, 0);
            this.gcUsedCodeList.MainView = this.gvUsedcodeList;
            this.gcUsedCodeList.Name = "gcUsedCodeList";
            this.gcUsedCodeList.Size = new System.Drawing.Size(285, 162);
            this.gcUsedCodeList.TabIndex = 0;
            this.gcUsedCodeList.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvUsedcodeList});
            // 
            // gvUsedcodeList
            // 
            this.gvUsedcodeList.Appearance.FocusedRow.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.gvUsedcodeList.Appearance.FocusedRow.Options.UseFont = true;
            this.gvUsedcodeList.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colId,
            this.colUsedCode,
            this.colName});
            this.gvUsedcodeList.DetailHeight = 77;
            this.gvUsedcodeList.GridControl = this.gcUsedCodeList;
            this.gvUsedcodeList.Name = "gvUsedcodeList";
            this.gvUsedcodeList.OptionsBehavior.Editable = false;
            this.gvUsedcodeList.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvUsedcodeList.OptionsView.ShowColumnHeaders = false;
            this.gvUsedcodeList.OptionsView.ShowGroupPanel = false;
            this.gvUsedcodeList.OptionsView.ShowIndicator = false;
            // 
            // colId
            // 
            this.colId.Caption = "Id";
            this.colId.FieldName = "ID";
            this.colId.Name = "colId";
            // 
            // colUsedCode
            // 
            this.colUsedCode.Caption = "Used Codes";
            this.colUsedCode.FieldName = "USED_CODE";
            this.colUsedCode.Name = "colUsedCode";
            this.colUsedCode.OptionsColumn.FixedWidth = true;
            this.colUsedCode.Visible = true;
            this.colUsedCode.VisibleIndex = 0;
            this.colUsedCode.Width = 60;
            // 
            // colName
            // 
            this.colName.Caption = "Name";
            this.colName.FieldName = "NAME";
            this.colName.Name = "colName";
            this.colName.Visible = true;
            this.colName.VisibleIndex = 1;
            // 
            // UcUsedCodesList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.popupContainerControl1);
            this.Name = "UcUsedCodesList";
            this.Size = new System.Drawing.Size(285, 164);
            ((System.ComponentModel.ISupportInitialize)(this.popupContainerControl1)).EndInit();
            this.popupContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcUsedCodeList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvUsedcodeList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PopupContainerControl popupContainerControl1;
        private DevExpress.XtraGrid.GridControl gcUsedCodeList;
        private DevExpress.XtraGrid.Views.Grid.GridView gvUsedcodeList;
        private DevExpress.XtraGrid.Columns.GridColumn colId;
        private DevExpress.XtraGrid.Columns.GridColumn colUsedCode;
        private DevExpress.XtraGrid.Columns.GridColumn colName;


    }
}
