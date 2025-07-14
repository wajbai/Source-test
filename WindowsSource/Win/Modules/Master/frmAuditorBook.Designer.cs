namespace ACPP.Modules.Master
{
    partial class frmAuditorBook
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAuditorBook));
            this.ucToolBarAddressView = new Bosco.Utility.Controls.ucToolBar();
            this.gcAuditorBookView = new DevExpress.XtraGrid.GridControl();
            this.crdAuditorBook = new DevExpress.XtraGrid.Views.Card.CardView();
            this.colDonorId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAddress = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPlace = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colState = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCountry = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPinCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPhone = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFax = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEMail = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colURL = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIdentity = new DevExpress.XtraGrid.Columns.GridColumn();
            this.pntop = new DevExpress.XtraEditors.PanelControl();
            this.pnlfooter = new DevExpress.XtraEditors.PanelControl();
            this.lblRecordCount = new DevExpress.XtraEditors.LabelControl();
            this.lblAuditorBook = new DevExpress.XtraEditors.LabelControl();
            this.pnlGroup = new DevExpress.XtraEditors.PanelControl();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.rgAddressBookConstruct = new DevExpress.XtraEditors.RadioGroup();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.pnlFill = new DevExpress.XtraEditors.PanelControl();
            ((System.ComponentModel.ISupportInitialize)(this.gcAuditorBookView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.crdAuditorBook)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pntop)).BeginInit();
            this.pntop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlfooter)).BeginInit();
            this.pnlfooter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlGroup)).BeginInit();
            this.pnlGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rgAddressBookConstruct.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlFill)).BeginInit();
            this.pnlFill.SuspendLayout();
            this.SuspendLayout();
            // 
            // ucToolBarAddressView
            // 
            this.ucToolBarAddressView.ChangeAddCaption = "<u>A</u>dd";
            this.ucToolBarAddressView.ChangeCaption = "&Edit";
            this.ucToolBarAddressView.ChangeDeleteCaption = "<u>D</u>elete";
            this.ucToolBarAddressView.ChangePrintCaption = "<u>P</u>rint";
            this.ucToolBarAddressView.DisableAddButton = true;
            this.ucToolBarAddressView.DisableCloseButton = true;
            this.ucToolBarAddressView.DisableDeleteButton = true;
            this.ucToolBarAddressView.DisableEditButton = true;
            this.ucToolBarAddressView.DisableMoveTransaction = true;
            this.ucToolBarAddressView.DisablePrintButton = true;
            resources.ApplyResources(this.ucToolBarAddressView, "ucToolBarAddressView");
            this.ucToolBarAddressView.Name = "ucToolBarAddressView";
            this.ucToolBarAddressView.ShowHTML = true;
            this.ucToolBarAddressView.ShowMMT = true;
            this.ucToolBarAddressView.ShowPDF = true;
            this.ucToolBarAddressView.ShowRTF = true;
            this.ucToolBarAddressView.ShowText = true;
            this.ucToolBarAddressView.ShowXLS = true;
            this.ucToolBarAddressView.ShowXLSX = true;
            this.ucToolBarAddressView.VisibleAddButton = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucToolBarAddressView.VisibleDeleteButton = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucToolBarAddressView.VisibleEditButton = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucToolBarAddressView.VisibleMoveTrans = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBarAddressView.VisiblePrintButton = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucToolBarAddressView.VisibleRefresh = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucToolBarAddressView.AddClicked += new System.EventHandler(this.ucToolBarAddressView_AddClicked);
            this.ucToolBarAddressView.EditClicked += new System.EventHandler(this.ucToolBarAddressView_EditClicked);
            this.ucToolBarAddressView.DeleteClicked += new System.EventHandler(this.ucToolBarAddressView_DeleteClicked);
            this.ucToolBarAddressView.PrintClicked += new System.EventHandler(this.ucToolBarAddressView_PrintClicked);
            this.ucToolBarAddressView.CloseClicked += new System.EventHandler(this.ucToolBarAddressView_CloseClicked);
            // 
            // gcAuditorBookView
            // 
            resources.ApplyResources(this.gcAuditorBookView, "gcAuditorBookView");
            this.gcAuditorBookView.MainView = this.crdAuditorBook;
            this.gcAuditorBookView.Name = "gcAuditorBookView";
            this.gcAuditorBookView.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.crdAuditorBook});
            // 
            // crdAuditorBook
            // 
            this.crdAuditorBook.AppearancePrint.CardCaption.Font = ((System.Drawing.Font)(resources.GetObject("crdAuditorBook.AppearancePrint.CardCaption.Font")));
            this.crdAuditorBook.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            resources.ApplyResources(this.crdAuditorBook, "crdAuditorBook");
            this.crdAuditorBook.CardInterval = 8;
            this.crdAuditorBook.CardScrollButtonBorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.crdAuditorBook.CardWidth = 250;
            this.crdAuditorBook.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colDonorId,
            this.colName,
            this.colAddress,
            this.colPlace,
            this.colState,
            this.colCountry,
            this.colPinCode,
            this.colPhone,
            this.colFax,
            this.colEMail,
            this.colURL,
            this.colIdentity});
            this.crdAuditorBook.DetailHeight = 280;
            this.crdAuditorBook.DetailTabHeaderLocation = DevExpress.XtraTab.TabHeaderLocation.Bottom;
            this.crdAuditorBook.FocusedCardTopFieldIndex = 0;
            this.crdAuditorBook.GridControl = this.gcAuditorBookView;
            this.crdAuditorBook.MaximumCardColumns = 4;
            this.crdAuditorBook.MaximumCardRows = 2;
            this.crdAuditorBook.Name = "crdAuditorBook";
            this.crdAuditorBook.OptionsBehavior.Editable = false;
            this.crdAuditorBook.OptionsPrint.PrintCardCaption = false;
            this.crdAuditorBook.OptionsPrint.PrintEmptyFields = false;
            this.crdAuditorBook.OptionsView.ShowCardExpandButton = false;
            this.crdAuditorBook.OptionsView.ShowEmptyFields = false;
            this.crdAuditorBook.OptionsView.ShowFieldCaptions = false;
            this.crdAuditorBook.OptionsView.ShowFieldHints = false;
            this.crdAuditorBook.OptionsView.ShowHorzScrollBar = false;
            this.crdAuditorBook.OptionsView.ShowLines = false;
            this.crdAuditorBook.PrintMaximumCardColumns = 3;
            this.crdAuditorBook.DoubleClick += new System.EventHandler(this.crdAuditorBook_DoubleClick);
            this.crdAuditorBook.RowCountChanged += new System.EventHandler(this.crdAuditorBook_RowCountChanged);
            // 
            // colDonorId
            // 
            this.colDonorId.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colDonorId.AppearanceHeader.Font")));
            this.colDonorId.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colDonorId, "colDonorId");
            this.colDonorId.FieldName = "DONAUD_ID";
            this.colDonorId.MinWidth = 15;
            this.colDonorId.Name = "colDonorId";
            this.colDonorId.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colDonorId.OptionsColumn.ShowCaption = false;
            this.colDonorId.OptionsColumn.ShowInCustomizationForm = false;
            this.colDonorId.OptionsFilter.AllowFilter = false;
            // 
            // colName
            // 
            this.colName.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colName.AppearanceHeader.Font")));
            this.colName.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colName, "colName");
            this.colName.FieldName = "NAME";
            this.colName.Name = "colName";
            this.colName.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colName.OptionsFilter.AllowFilter = false;
            // 
            // colAddress
            // 
            this.colAddress.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colAddress.AppearanceHeader.Font")));
            this.colAddress.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colAddress, "colAddress");
            this.colAddress.FieldName = "ADDRESS";
            this.colAddress.Name = "colAddress";
            this.colAddress.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colAddress.OptionsFilter.AllowFilter = false;
            // 
            // colPlace
            // 
            this.colPlace.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colPlace.AppearanceHeader.Font")));
            this.colPlace.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colPlace, "colPlace");
            this.colPlace.FieldName = "PLACE";
            this.colPlace.Name = "colPlace";
            this.colPlace.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colPlace.OptionsFilter.AllowFilter = false;
            // 
            // colState
            // 
            this.colState.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colState.AppearanceHeader.Font")));
            this.colState.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colState, "colState");
            this.colState.FieldName = "STATE";
            this.colState.Name = "colState";
            this.colState.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colState.OptionsFilter.AllowFilter = false;
            // 
            // colCountry
            // 
            this.colCountry.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colCountry.AppearanceHeader.Font")));
            this.colCountry.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colCountry, "colCountry");
            this.colCountry.FieldName = "COUNTRY";
            this.colCountry.Name = "colCountry";
            this.colCountry.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colCountry.OptionsFilter.AllowFilter = false;
            // 
            // colPinCode
            // 
            this.colPinCode.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colPinCode.AppearanceHeader.Font")));
            this.colPinCode.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colPinCode, "colPinCode");
            this.colPinCode.FieldName = "PINCODE";
            this.colPinCode.Name = "colPinCode";
            this.colPinCode.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colPinCode.OptionsFilter.AllowFilter = false;
            // 
            // colPhone
            // 
            this.colPhone.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colPhone.AppearanceHeader.Font")));
            this.colPhone.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colPhone, "colPhone");
            this.colPhone.FieldName = "PHONE";
            this.colPhone.Name = "colPhone";
            this.colPhone.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colPhone.OptionsFilter.AllowFilter = false;
            // 
            // colFax
            // 
            this.colFax.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colFax.AppearanceHeader.Font")));
            this.colFax.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colFax, "colFax");
            this.colFax.FieldName = "FAX";
            this.colFax.Name = "colFax";
            this.colFax.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colFax.OptionsFilter.AllowFilter = false;
            // 
            // colEMail
            // 
            this.colEMail.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colEMail.AppearanceHeader.Font")));
            this.colEMail.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colEMail, "colEMail");
            this.colEMail.FieldName = "EMAIL";
            this.colEMail.Name = "colEMail";
            this.colEMail.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colEMail.OptionsFilter.AllowFilter = false;
            // 
            // colURL
            // 
            this.colURL.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colURL.AppearanceHeader.Font")));
            this.colURL.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colURL, "colURL");
            this.colURL.FieldName = "URL";
            this.colURL.Name = "colURL";
            this.colURL.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colURL.OptionsFilter.AllowFilter = false;
            // 
            // colIdentity
            // 
            resources.ApplyResources(this.colIdentity, "colIdentity");
            this.colIdentity.FieldName = "IDENTITYKEY";
            this.colIdentity.Name = "colIdentity";
            this.colIdentity.OptionsColumn.ShowCaption = false;
            this.colIdentity.OptionsColumn.ShowInCustomizationForm = false;
            this.colIdentity.OptionsFilter.AllowFilter = false;
            // 
            // pntop
            // 
            this.pntop.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pntop.Controls.Add(this.ucToolBarAddressView);
            resources.ApplyResources(this.pntop, "pntop");
            this.pntop.Name = "pntop";
            // 
            // pnlfooter
            // 
            this.pnlfooter.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnlfooter.Controls.Add(this.lblRecordCount);
            this.pnlfooter.Controls.Add(this.lblAuditorBook);
            resources.ApplyResources(this.pnlfooter, "pnlfooter");
            this.pnlfooter.Name = "pnlfooter";
            // 
            // lblRecordCount
            // 
            resources.ApplyResources(this.lblRecordCount, "lblRecordCount");
            this.lblRecordCount.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("lblRecordCount.Appearance.Font")));
            this.lblRecordCount.Name = "lblRecordCount";
            // 
            // lblAuditorBook
            // 
            resources.ApplyResources(this.lblAuditorBook, "lblAuditorBook");
            this.lblAuditorBook.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("lblAuditorBook.Appearance.Font")));
            this.lblAuditorBook.Name = "lblAuditorBook";
            // 
            // pnlGroup
            // 
            this.pnlGroup.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnlGroup.Controls.Add(this.layoutControl1);
            resources.ApplyResources(this.pnlGroup, "pnlGroup");
            this.pnlGroup.Name = "pnlGroup";
            // 
            // layoutControl1
            // 
            this.layoutControl1.AllowCustomizationMenu = false;
            this.layoutControl1.Controls.Add(this.rgAddressBookConstruct);
            resources.ApplyResources(this.layoutControl1, "layoutControl1");
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(183, 303, 250, 350);
            this.layoutControl1.Root = this.layoutControlGroup1;
            // 
            // rgAddressBookConstruct
            // 
            resources.ApplyResources(this.rgAddressBookConstruct, "rgAddressBookConstruct");
            this.rgAddressBookConstruct.Name = "rgAddressBookConstruct";
            this.rgAddressBookConstruct.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.rgAddressBookConstruct.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(((object)(resources.GetObject("rgAddressBookConstruct.Properties.Items"))), resources.GetString("rgAddressBookConstruct.Properties.Items1")),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(((object)(resources.GetObject("rgAddressBookConstruct.Properties.Items2"))), resources.GetString("rgAddressBookConstruct.Properties.Items3"))});
            this.rgAddressBookConstruct.StyleController = this.layoutControl1;
            this.rgAddressBookConstruct.SelectedIndexChanged += new System.EventHandler(this.rgAddressBookConstruct_SelectedIndexChanged);
            // 
            // layoutControlGroup1
            // 
            resources.ApplyResources(this.layoutControlGroup1, "layoutControlGroup1");
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.emptySpaceItem1});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(749, 26);
            this.layoutControlGroup1.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 1);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.rgAddressBookConstruct;
            resources.ApplyResources(this.layoutControlItem1, "layoutControlItem1");
            this.layoutControlItem1.Location = new System.Drawing.Point(590, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlItem1.Size = new System.Drawing.Size(159, 25);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem1, "emptySpaceItem1");
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 0);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 3);
            this.emptySpaceItem1.Size = new System.Drawing.Size(590, 25);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // pnlFill
            // 
            this.pnlFill.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnlFill.Controls.Add(this.gcAuditorBookView);
            resources.ApplyResources(this.pnlFill, "pnlFill");
            this.pnlFill.Name = "pnlFill";
            // 
            // frmAuditorBook
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlFill);
            this.Controls.Add(this.pnlGroup);
            this.Controls.Add(this.pnlfooter);
            this.Controls.Add(this.pntop);
            this.Name = "frmAuditorBook";
            this.Load += new System.EventHandler(this.frmAddressView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gcAuditorBookView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.crdAuditorBook)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pntop)).EndInit();
            this.pntop.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlfooter)).EndInit();
            this.pnlfooter.ResumeLayout(false);
            this.pnlfooter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlGroup)).EndInit();
            this.pnlGroup.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.rgAddressBookConstruct.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlFill)).EndInit();
            this.pnlFill.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Bosco.Utility.Controls.ucToolBar ucToolBarAddressView;
        private DevExpress.XtraGrid.GridControl gcAuditorBookView;
        private DevExpress.XtraEditors.PanelControl pntop;
        private DevExpress.XtraEditors.PanelControl pnlfooter;
        private DevExpress.XtraEditors.PanelControl pnlGroup;
        private DevExpress.XtraEditors.PanelControl pnlFill;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraEditors.RadioGroup rgAddressBookConstruct;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraGrid.Columns.GridColumn colDonorId;
        private DevExpress.XtraGrid.Columns.GridColumn colName;
        private DevExpress.XtraGrid.Columns.GridColumn colAddress;
        private DevExpress.XtraGrid.Columns.GridColumn colPlace;
        private DevExpress.XtraGrid.Columns.GridColumn colState;
        private DevExpress.XtraGrid.Columns.GridColumn colCountry;
        private DevExpress.XtraGrid.Columns.GridColumn colPinCode;
        private DevExpress.XtraGrid.Columns.GridColumn colPhone;
        private DevExpress.XtraGrid.Columns.GridColumn colFax;
        private DevExpress.XtraGrid.Columns.GridColumn colEMail;
        private DevExpress.XtraGrid.Columns.GridColumn colURL;
        private DevExpress.XtraGrid.Views.Card.CardView crdAuditorBook;
        private DevExpress.XtraEditors.LabelControl lblRecordCount;
        private DevExpress.XtraEditors.LabelControl lblAuditorBook;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraGrid.Columns.GridColumn colIdentity;
    }
}