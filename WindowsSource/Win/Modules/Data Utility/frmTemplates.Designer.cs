namespace ACPP.Modules.Data_Utility
{
    partial class frmTemplates
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTemplates));
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.gcTemplates = new DevExpress.XtraGrid.GridControl();
            this.gvTemplates = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colModule = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFileName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDownload = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rDownload = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.colFilePath = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grdlModuleName = new DevExpress.XtraEditors.GridLookUpEdit();
            this.gridLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcTemplates)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvTemplates)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rDownload)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdlModuleName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.btnClose);
            this.layoutControl1.Controls.Add(this.gcTemplates);
            this.layoutControl1.Controls.Add(this.grdlModuleName);
            resources.ApplyResources(this.layoutControl1, "layoutControl1");
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(597, 114, 250, 350);
            this.layoutControl1.Root = this.layoutControlGroup1;
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.btnClose, "btnClose");
            this.btnClose.Name = "btnClose";
            this.btnClose.StyleController = this.layoutControl1;
            // 
            // gcTemplates
            // 
            resources.ApplyResources(this.gcTemplates, "gcTemplates");
            this.gcTemplates.MainView = this.gvTemplates;
            this.gcTemplates.Name = "gcTemplates";
            this.gcTemplates.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rDownload});
            this.gcTemplates.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvTemplates});
            // 
            // gvTemplates
            // 
            this.gvTemplates.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colModule,
            this.colFileName,
            this.colDownload,
            this.colFilePath});
            this.gvTemplates.GridControl = this.gcTemplates;
            this.gvTemplates.Name = "gvTemplates";
            this.gvTemplates.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            this.gvTemplates.OptionsView.ShowGroupPanel = false;
            this.gvTemplates.OptionsView.ShowIndicator = false;
            // 
            // colModule
            // 
            resources.ApplyResources(this.colModule, "colModule");
            this.colModule.FieldName = "Module";
            this.colModule.Name = "colModule";
            this.colModule.OptionsColumn.AllowEdit = false;
            this.colModule.OptionsColumn.AllowFocus = false;
            // 
            // colFileName
            // 
            resources.ApplyResources(this.colFileName, "colFileName");
            this.colFileName.FieldName = "Name";
            this.colFileName.Name = "colFileName";
            this.colFileName.OptionsColumn.AllowEdit = false;
            this.colFileName.OptionsColumn.AllowFocus = false;
            // 
            // colDownload
            // 
            resources.ApplyResources(this.colDownload, "colDownload");
            this.colDownload.ColumnEdit = this.rDownload;
            this.colDownload.Name = "colDownload";
            this.colDownload.OptionsColumn.ShowCaption = false;
            this.colDownload.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
            // 
            // rDownload
            // 
            resources.ApplyResources(this.rDownload, "rDownload");
            this.rDownload.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("rDownload.Buttons"))), resources.GetString("rDownload.Buttons1"), ((int)(resources.GetObject("rDownload.Buttons2"))), ((bool)(resources.GetObject("rDownload.Buttons3"))), ((bool)(resources.GetObject("rDownload.Buttons4"))), ((bool)(resources.GetObject("rDownload.Buttons5"))), ((DevExpress.XtraEditors.ImageLocation)(resources.GetObject("rDownload.Buttons6"))), global::ACPP.Properties.Resources.excel1, new DevExpress.Utils.KeyShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D)), serializableAppearanceObject1, resources.GetString("rDownload.Buttons7"), ((object)(resources.GetObject("rDownload.Buttons8"))), ((DevExpress.Utils.SuperToolTip)(resources.GetObject("rDownload.Buttons9"))), ((bool)(resources.GetObject("rDownload.Buttons10"))))});
            this.rDownload.Name = "rDownload";
            this.rDownload.Click += new System.EventHandler(this.rDownload_Click);
            // 
            // colFilePath
            // 
            resources.ApplyResources(this.colFilePath, "colFilePath");
            this.colFilePath.FieldName = "SourcePath";
            this.colFilePath.Name = "colFilePath";
            // 
            // grdlModuleName
            // 
            resources.ApplyResources(this.grdlModuleName, "grdlModuleName");
            this.grdlModuleName.Name = "grdlModuleName";
            this.grdlModuleName.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.grdlModuleName.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("grdlModuleName.Properties.Buttons"))))});
            this.grdlModuleName.Properties.NullText = resources.GetString("grdlModuleName.Properties.NullText");
            this.grdlModuleName.Properties.View = this.gridLookUpEdit1View;
            this.grdlModuleName.StyleController = this.layoutControl1;
            this.grdlModuleName.EditValueChanged += new System.EventHandler(this.grdlModuleName_EditValueChanged);
            // 
            // gridLookUpEdit1View
            // 
            this.gridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridLookUpEdit1View.Name = "gridLookUpEdit1View";
            this.gridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridLookUpEdit1View.OptionsView.ShowColumnHeaders = false;
            this.gridLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // layoutControlGroup1
            // 
            resources.ApplyResources(this.layoutControlGroup1, "layoutControlGroup1");
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.emptySpaceItem1});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.layoutControlGroup1.Size = new System.Drawing.Size(624, 293);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.grdlModuleName;
            resources.ApplyResources(this.layoutControlItem1, "layoutControlItem1");
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(614, 24);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(34, 13);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.gcTemplates;
            resources.ApplyResources(this.layoutControlItem2, "layoutControlItem2");
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 24);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(614, 233);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.btnClose;
            resources.ApplyResources(this.layoutControlItem3, "layoutControlItem3");
            this.layoutControlItem3.Location = new System.Drawing.Point(538, 257);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(76, 26);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextToControlDistance = 0;
            this.layoutControlItem3.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem1, "emptySpaceItem1");
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 257);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(538, 26);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // frmTemplates
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.Controls.Add(this.layoutControl1);
            this.Name = "frmTemplates";
            this.Load += new System.EventHandler(this.frmTemplates_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcTemplates)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvTemplates)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rDownload)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdlModuleName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraEditors.GridLookUpEdit grdlModuleName;
        private DevExpress.XtraGrid.Views.Grid.GridView gridLookUpEdit1View;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraGrid.GridControl gcTemplates;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraGrid.Views.Grid.GridView gvTemplates;
        private DevExpress.XtraGrid.Columns.GridColumn colModule;
        private DevExpress.XtraGrid.Columns.GridColumn colFileName;
        private DevExpress.XtraGrid.Columns.GridColumn colDownload;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit rDownload;
        private DevExpress.XtraGrid.Columns.GridColumn colFilePath;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
    }
}