namespace ACPP.Modules.Data_Utility
{
    partial class frmRestoreMultipleDB
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRestoreMultipleDB));
            this.btnRestore = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.lblBranchDetails = new DevExpress.XtraEditors.LabelControl();
            this.btnMultipleLicenseKey = new DevExpress.XtraEditors.SimpleButton();
            this.txtMultipleLicenseKey = new DevExpress.XtraEditors.TextEdit();
            this.rgOptionNewExist = new DevExpress.XtraEditors.RadioGroup();
            this.glkExistingDB = new DevExpress.XtraEditors.GridLookUpEdit();
            this.gridLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.txtDatabaseName = new DevExpress.XtraEditors.TextEdit();
            this.btnBrowse = new DevExpress.XtraEditors.SimpleButton();
            this.txtFilePath = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblNewDBNameData = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem3 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.lblCurrentDBName = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblDataBasePath = new DevExpress.XtraLayout.SimpleLabelItem();
            this.lblradio = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblNewDBName = new DevExpress.XtraLayout.SimpleLabelItem();
            this.lciMultipleLicenseKey = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciMultipleLicenseKeyBrowse = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcBranchDetails = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtMultipleLicenseKey.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rgOptionNewExist.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.glkExistingDB.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDatabaseName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFilePath.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblNewDBNameData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblCurrentDBName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblDataBasePath)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblradio)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblNewDBName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciMultipleLicenseKey)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciMultipleLicenseKeyBrowse)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcBranchDetails)).BeginInit();
            this.SuspendLayout();
            // 
            // btnRestore
            // 
            this.btnRestore.AllowHtmlDraw = DevExpress.Utils.DefaultBoolean.True;
            resources.ApplyResources(this.btnRestore, "btnRestore");
            this.btnRestore.Name = "btnRestore";
            this.btnRestore.StyleController = this.layoutControl1;
            this.btnRestore.Click += new System.EventHandler(this.btnRestore_Click);
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.lblBranchDetails);
            this.layoutControl1.Controls.Add(this.btnMultipleLicenseKey);
            this.layoutControl1.Controls.Add(this.txtMultipleLicenseKey);
            this.layoutControl1.Controls.Add(this.rgOptionNewExist);
            this.layoutControl1.Controls.Add(this.glkExistingDB);
            this.layoutControl1.Controls.Add(this.btnRestore);
            this.layoutControl1.Controls.Add(this.btnClose);
            this.layoutControl1.Controls.Add(this.txtDatabaseName);
            this.layoutControl1.Controls.Add(this.btnBrowse);
            this.layoutControl1.Controls.Add(this.txtFilePath);
            resources.ApplyResources(this.layoutControl1, "layoutControl1");
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(637, 234, 250, 350);
            this.layoutControl1.Root = this.layoutControlGroup1;
            // 
            // lblBranchDetails
            // 
            this.lblBranchDetails.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("lblBranchDetails.Appearance.Font")));
            this.lblBranchDetails.Appearance.ForeColor = ((System.Drawing.Color)(resources.GetObject("lblBranchDetails.Appearance.ForeColor")));
            resources.ApplyResources(this.lblBranchDetails, "lblBranchDetails");
            this.lblBranchDetails.Name = "lblBranchDetails";
            this.lblBranchDetails.StyleController = this.layoutControl1;
            // 
            // btnMultipleLicenseKey
            // 
            resources.ApplyResources(this.btnMultipleLicenseKey, "btnMultipleLicenseKey");
            this.btnMultipleLicenseKey.Name = "btnMultipleLicenseKey";
            this.btnMultipleLicenseKey.StyleController = this.layoutControl1;
            this.btnMultipleLicenseKey.Click += new System.EventHandler(this.btnMultipleLicenseKey_Click);
            // 
            // txtMultipleLicenseKey
            // 
            resources.ApplyResources(this.txtMultipleLicenseKey, "txtMultipleLicenseKey");
            this.txtMultipleLicenseKey.Name = "txtMultipleLicenseKey";
            this.txtMultipleLicenseKey.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtMultipleLicenseKey.StyleController = this.layoutControl1;
            // 
            // rgOptionNewExist
            // 
            resources.ApplyResources(this.rgOptionNewExist, "rgOptionNewExist");
            this.rgOptionNewExist.Name = "rgOptionNewExist";
            this.rgOptionNewExist.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.rgOptionNewExist.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(((object)(resources.GetObject("rgOptionNewExist.Properties.Items"))), resources.GetString("rgOptionNewExist.Properties.Items1")),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(((object)(resources.GetObject("rgOptionNewExist.Properties.Items2"))), resources.GetString("rgOptionNewExist.Properties.Items3"))});
            this.rgOptionNewExist.StyleController = this.layoutControl1;
            this.rgOptionNewExist.SelectedIndexChanged += new System.EventHandler(this.rgOptionNewExist_SelectedIndexChanged);
            // 
            // glkExistingDB
            // 
            resources.ApplyResources(this.glkExistingDB, "glkExistingDB");
            this.glkExistingDB.Name = "glkExistingDB";
            this.glkExistingDB.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.glkExistingDB.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("glkExistingDB.Properties.Buttons"))))});
            this.glkExistingDB.Properties.NullText = resources.GetString("glkExistingDB.Properties.NullText");
            this.glkExistingDB.Properties.View = this.gridLookUpEdit1View;
            this.glkExistingDB.StyleController = this.layoutControl1;
            // 
            // gridLookUpEdit1View
            // 
            this.gridLookUpEdit1View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1});
            this.gridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridLookUpEdit1View.Name = "gridLookUpEdit1View";
            this.gridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridLookUpEdit1View.OptionsView.ShowColumnHeaders = false;
            this.gridLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn1
            // 
            resources.ApplyResources(this.gridColumn1, "gridColumn1");
            this.gridColumn1.FieldName = "Restore_Db";
            this.gridColumn1.Name = "gridColumn1";
            // 
            // btnClose
            // 
            this.btnClose.AllowHtmlDraw = DevExpress.Utils.DefaultBoolean.True;
            resources.ApplyResources(this.btnClose, "btnClose");
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Name = "btnClose";
            this.btnClose.StyleController = this.layoutControl1;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // txtDatabaseName
            // 
            resources.ApplyResources(this.txtDatabaseName, "txtDatabaseName");
            this.txtDatabaseName.Name = "txtDatabaseName";
            this.txtDatabaseName.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtDatabaseName.Properties.Mask.EditMask = resources.GetString("txtDatabaseName.Properties.Mask.EditMask");
            this.txtDatabaseName.Properties.Mask.MaskType = ((DevExpress.XtraEditors.Mask.MaskType)(resources.GetObject("txtDatabaseName.Properties.Mask.MaskType")));
            this.txtDatabaseName.Properties.MaxLength = 20;
            this.txtDatabaseName.StyleController = this.layoutControl1;
            // 
            // btnBrowse
            // 
            resources.ApplyResources(this.btnBrowse, "btnBrowse");
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.StyleController = this.layoutControl1;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // txtFilePath
            // 
            resources.ApplyResources(this.txtFilePath, "txtFilePath");
            this.txtFilePath.Name = "txtFilePath";
            this.txtFilePath.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtFilePath.StyleController = this.layoutControl1;
            // 
            // layoutControlGroup1
            // 
            resources.ApplyResources(this.layoutControlGroup1, "layoutControlGroup1");
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.lblNewDBNameData,
            this.layoutControlItem6,
            this.layoutControlItem7,
            this.emptySpaceItem3,
            this.lblCurrentDBName,
            this.lblDataBasePath,
            this.lblradio,
            this.lblNewDBName,
            this.lciMultipleLicenseKey,
            this.lciMultipleLicenseKeyBrowse,
            this.lcBranchDetails});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(442, 151);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.txtFilePath;
            resources.ApplyResources(this.layoutControlItem1, "layoutControlItem1");
            this.layoutControlItem1.Location = new System.Drawing.Point(90, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 2, 0);
            this.layoutControlItem1.Size = new System.Drawing.Size(293, 26);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.btnBrowse;
            resources.ApplyResources(this.layoutControlItem2, "layoutControlItem2");
            this.layoutControlItem2.Location = new System.Drawing.Point(383, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(59, 26);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // lblNewDBNameData
            // 
            this.lblNewDBNameData.Control = this.txtDatabaseName;
            resources.ApplyResources(this.lblNewDBNameData, "lblNewDBNameData");
            this.lblNewDBNameData.Location = new System.Drawing.Point(89, 74);
            this.lblNewDBNameData.Name = "lblNewDBNameData";
            this.lblNewDBNameData.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 2, 0);
            this.lblNewDBNameData.Size = new System.Drawing.Size(353, 25);
            this.lblNewDBNameData.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 3);
            this.lblNewDBNameData.TextSize = new System.Drawing.Size(0, 0);
            this.lblNewDBNameData.TextToControlDistance = 0;
            this.lblNewDBNameData.TextVisible = false;
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.btnClose;
            resources.ApplyResources(this.layoutControlItem6, "layoutControlItem6");
            this.layoutControlItem6.Location = new System.Drawing.Point(373, 125);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(69, 26);
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextToControlDistance = 0;
            this.layoutControlItem6.TextVisible = false;
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.btnRestore;
            resources.ApplyResources(this.layoutControlItem7, "layoutControlItem7");
            this.layoutControlItem7.Location = new System.Drawing.Point(304, 125);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(69, 26);
            this.layoutControlItem7.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem7.TextToControlDistance = 0;
            this.layoutControlItem7.TextVisible = false;
            // 
            // emptySpaceItem3
            // 
            this.emptySpaceItem3.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem3, "emptySpaceItem3");
            this.emptySpaceItem3.Location = new System.Drawing.Point(163, 125);
            this.emptySpaceItem3.Name = "emptySpaceItem3";
            this.emptySpaceItem3.Size = new System.Drawing.Size(141, 26);
            this.emptySpaceItem3.TextSize = new System.Drawing.Size(0, 0);
            // 
            // lblCurrentDBName
            // 
            this.lblCurrentDBName.Control = this.glkExistingDB;
            resources.ApplyResources(this.lblCurrentDBName, "lblCurrentDBName");
            this.lblCurrentDBName.Location = new System.Drawing.Point(0, 52);
            this.lblCurrentDBName.Name = "lblCurrentDBName";
            this.lblCurrentDBName.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 0, 2, 0);
            this.lblCurrentDBName.Size = new System.Drawing.Size(442, 22);
            this.lblCurrentDBName.TextSize = new System.Drawing.Size(83, 13);
            // 
            // lblDataBasePath
            // 
            this.lblDataBasePath.AllowHotTrack = false;
            resources.ApplyResources(this.lblDataBasePath, "lblDataBasePath");
            this.lblDataBasePath.Location = new System.Drawing.Point(0, 0);
            this.lblDataBasePath.Name = "lblDataBasePath";
            this.lblDataBasePath.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 0, 0, 0);
            this.lblDataBasePath.Size = new System.Drawing.Size(90, 26);
            this.lblDataBasePath.TextSize = new System.Drawing.Size(83, 13);
            // 
            // lblradio
            // 
            this.lblradio.Control = this.rgOptionNewExist;
            resources.ApplyResources(this.lblradio, "lblradio");
            this.lblradio.Location = new System.Drawing.Point(0, 26);
            this.lblradio.MaxSize = new System.Drawing.Size(442, 26);
            this.lblradio.MinSize = new System.Drawing.Size(442, 26);
            this.lblradio.Name = "lblradio";
            this.lblradio.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 0, 2, 0);
            this.lblradio.Size = new System.Drawing.Size(442, 26);
            this.lblradio.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lblradio.TextSize = new System.Drawing.Size(83, 13);
            // 
            // lblNewDBName
            // 
            this.lblNewDBName.AllowHotTrack = false;
            resources.ApplyResources(this.lblNewDBName, "lblNewDBName");
            this.lblNewDBName.Location = new System.Drawing.Point(0, 74);
            this.lblNewDBName.Name = "lblNewDBName";
            this.lblNewDBName.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 0, 0, 0);
            this.lblNewDBName.Size = new System.Drawing.Size(89, 25);
            this.lblNewDBName.TextSize = new System.Drawing.Size(83, 13);
            // 
            // lciMultipleLicenseKey
            // 
            this.lciMultipleLicenseKey.Control = this.txtMultipleLicenseKey;
            resources.ApplyResources(this.lciMultipleLicenseKey, "lciMultipleLicenseKey");
            this.lciMultipleLicenseKey.Location = new System.Drawing.Point(0, 99);
            this.lciMultipleLicenseKey.Name = "lciMultipleLicenseKey";
            this.lciMultipleLicenseKey.Size = new System.Drawing.Size(381, 26);
            this.lciMultipleLicenseKey.TextSize = new System.Drawing.Size(83, 13);
            // 
            // lciMultipleLicenseKeyBrowse
            // 
            this.lciMultipleLicenseKeyBrowse.Control = this.btnMultipleLicenseKey;
            resources.ApplyResources(this.lciMultipleLicenseKeyBrowse, "lciMultipleLicenseKeyBrowse");
            this.lciMultipleLicenseKeyBrowse.Location = new System.Drawing.Point(381, 99);
            this.lciMultipleLicenseKeyBrowse.Name = "lciMultipleLicenseKeyBrowse";
            this.lciMultipleLicenseKeyBrowse.Size = new System.Drawing.Size(61, 26);
            this.lciMultipleLicenseKeyBrowse.TextSize = new System.Drawing.Size(0, 0);
            this.lciMultipleLicenseKeyBrowse.TextToControlDistance = 0;
            this.lciMultipleLicenseKeyBrowse.TextVisible = false;
            // 
            // lcBranchDetails
            // 
            this.lcBranchDetails.Control = this.lblBranchDetails;
            this.lcBranchDetails.ControlAlignment = System.Drawing.ContentAlignment.BottomLeft;
            resources.ApplyResources(this.lcBranchDetails, "lcBranchDetails");
            this.lcBranchDetails.Location = new System.Drawing.Point(0, 125);
            this.lcBranchDetails.Name = "lcBranchDetails";
            this.lcBranchDetails.Size = new System.Drawing.Size(163, 26);
            this.lcBranchDetails.TextSize = new System.Drawing.Size(0, 0);
            this.lcBranchDetails.TextToControlDistance = 0;
            this.lcBranchDetails.TextVisible = false;
            this.lcBranchDetails.TrimClientAreaToControl = false;
            // 
            // frmRestoreMultipleDB
            // 
            this.AcceptButton = this.btnRestore;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.Controls.Add(this.layoutControl1);
            this.MinimizeBox = false;
            this.Name = "frmRestoreMultipleDB";
            this.Load += new System.EventHandler(this.frmRestoreMultipleDB_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtMultipleLicenseKey.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rgOptionNewExist.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.glkExistingDB.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDatabaseName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFilePath.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblNewDBNameData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblCurrentDBName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblDataBasePath)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblradio)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblNewDBName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciMultipleLicenseKey)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciMultipleLicenseKeyBrowse)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcBranchDetails)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraEditors.SimpleButton btnRestore;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraEditors.TextEdit txtDatabaseName;
        private DevExpress.XtraEditors.SimpleButton btnBrowse;
        private DevExpress.XtraEditors.TextEdit txtFilePath;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem lblNewDBNameData;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem3;
        private DevExpress.XtraEditors.GridLookUpEdit glkExistingDB;
        private DevExpress.XtraGrid.Views.Grid.GridView gridLookUpEdit1View;
        private DevExpress.XtraLayout.LayoutControlItem lblCurrentDBName;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraLayout.SimpleLabelItem lblDataBasePath;
        private DevExpress.XtraEditors.RadioGroup rgOptionNewExist;
        private DevExpress.XtraLayout.LayoutControlItem lblradio;
        private DevExpress.XtraLayout.SimpleLabelItem lblNewDBName;
        private DevExpress.XtraEditors.TextEdit txtMultipleLicenseKey;
        private DevExpress.XtraLayout.LayoutControlItem lciMultipleLicenseKey;
        private DevExpress.XtraEditors.SimpleButton btnMultipleLicenseKey;
        private DevExpress.XtraLayout.LayoutControlItem lciMultipleLicenseKeyBrowse;
        private DevExpress.XtraEditors.LabelControl lblBranchDetails;
        private DevExpress.XtraLayout.LayoutControlItem lcBranchDetails;
    }
}