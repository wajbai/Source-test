namespace ACPP.Modules.Master
{
    partial class frmSubLeder
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
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
                //if (dvVoucherType != null)
                //{
                //    dvVoucherType.Dispose();
                //    dvVoucherType = null;
                //}
                if (dtVoucherTypes != null)
                {
                    dtVoucherTypes.Dispose();
                    dtVoucherTypes = null;
                }
                //if (dtProjectVoucherTypes != null)
                //{
                //    dtProjectVoucherTypes.Dispose();
                //    dtProjectVoucherTypes = null;
                //}
                if (resultArgs != null)
                {
                    resultArgs.Dispose();
                    resultArgs = null;
                }
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSubLeder));
            this.repchkSelect = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.repchkTrans_Flag = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.tcProject = new DevExpress.XtraTab.XtraTabControl();
            this.tpForeign = new DevExpress.XtraTab.XtraTabPage();
            this.pnlForeign = new DevExpress.XtraEditors.PanelControl();
            this.tpFForeign = new DevExpress.XtraTab.XtraTabControl();
            this.tpFLedger = new DevExpress.XtraTab.XtraTabPage();
            this.pnlFLedger = new DevExpress.XtraEditors.PanelControl();
            this.gcFLedger = new DevExpress.XtraGrid.GridControl();
            this.gvFLedger = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.tpFVoucher = new DevExpress.XtraTab.XtraTabPage();
            this.pnlFVoucher = new DevExpress.XtraEditors.PanelControl();
            this.tpLocal = new DevExpress.XtraTab.XtraTabPage();
            this.pnlLocal = new DevExpress.XtraEditors.PanelControl();
            this.tcLLedgers = new DevExpress.XtraTab.XtraTabControl();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.chkForeign = new DevExpress.XtraEditors.CheckEdit();
            this.dateEdit1 = new DevExpress.XtraEditors.DateEdit();
            this.txtProjectDescription = new DevExpress.XtraEditors.TextEdit();
            this.chkLocal = new DevExpress.XtraEditors.CheckEdit();
            this.txtProjectName = new DevExpress.XtraEditors.TextEdit();
            this.dateEdit2 = new DevExpress.XtraEditors.DateEdit();
            this.txtCode = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.Code = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem3 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.simpleLabelItem1 = new DevExpress.XtraLayout.SimpleLabelItem();
            this.pnlLedger = new DevExpress.XtraEditors.PanelControl();
            this.gcLedger = new DevExpress.XtraGrid.GridControl();
            this.gvLedger = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.lcProject = new DevExpress.XtraLayout.LayoutControl();
            this.glkpPurpose = new DevExpress.XtraEditors.GridLookUpEdit();
            this.gridView4 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colContributionId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPurpose = new DevExpress.XtraGrid.Columns.GridColumn();
            this.chkShowFilter = new DevExpress.XtraEditors.CheckEdit();
            this.gcSubLedgerProject = new DevExpress.XtraGrid.GridControl();
            this.gvSubLedgerProject = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colSelect = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rchkSelect = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.colLedgerGroup = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gvColLedgerName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gvColAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.reptxtAmount = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.gvColFlag = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gvColchkSelectAll = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gvColLedgerId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gvColGroupId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnNew = new DevExpress.XtraEditors.SimpleButton();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.txtsubLedger = new DevExpress.XtraEditors.TextEdit();
            this.lciPurpose = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcgProject = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lcgSubLedger = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lblsubLedger = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem21 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem10 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem9 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem14 = new DevExpress.XtraLayout.LayoutControlItem();
            this.icSmallImageCollection = new DevExpress.Utils.ImageCollection(this.components);
            this.emptySpaceItem7 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.emptySpaceItem9 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem11 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem8 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlGroup3 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup4 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.chkLedgerSelectAll = new DevExpress.XtraEditors.CheckEdit();
            ((System.ComponentModel.ISupportInitialize)(this.repchkSelect)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repchkTrans_Flag)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tcProject)).BeginInit();
            this.tcProject.SuspendLayout();
            this.tpForeign.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlForeign)).BeginInit();
            this.pnlForeign.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tpFForeign)).BeginInit();
            this.tpFForeign.SuspendLayout();
            this.tpFLedger.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlFLedger)).BeginInit();
            this.pnlFLedger.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcFLedger)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvFLedger)).BeginInit();
            this.tpFVoucher.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlFVoucher)).BeginInit();
            this.tpLocal.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlLocal)).BeginInit();
            this.pnlLocal.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tcLLedgers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkForeign.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit1.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtProjectDescription.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkLocal.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtProjectName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit2.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Code)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlLedger)).BeginInit();
            this.pnlLedger.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcLedger)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvLedger)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcProject)).BeginInit();
            this.lcProject.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.glkpPurpose.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowFilter.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcSubLedgerProject)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvSubLedgerProject)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rchkSelect)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.reptxtAmount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtsubLedger.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciPurpose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgProject)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgSubLedger)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblsubLedger)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem21)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem14)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.icSmallImageCollection)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkLedgerSelectAll.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // repchkSelect
            // 
            resources.ApplyResources(this.repchkSelect, "repchkSelect");
            this.repchkSelect.Name = "repchkSelect";
            this.repchkSelect.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
            this.repchkSelect.ValueChecked = 1;
            this.repchkSelect.ValueGrayed = 2;
            this.repchkSelect.ValueUnchecked = 0;
            // 
            // repchkTrans_Flag
            // 
            this.repchkTrans_Flag.AppearanceFocused.BackColor = ((System.Drawing.Color)(resources.GetObject("repchkTrans_Flag.AppearanceFocused.BackColor")));
            this.repchkTrans_Flag.AppearanceFocused.Options.UseBackColor = true;
            resources.ApplyResources(this.repchkTrans_Flag, "repchkTrans_Flag");
            this.repchkTrans_Flag.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("repchkTrans_Flag.Buttons"))))});
            this.repchkTrans_Flag.Items.AddRange(new object[] {
            resources.GetString("repchkTrans_Flag.Items"),
            resources.GetString("repchkTrans_Flag.Items1")});
            this.repchkTrans_Flag.MaxLength = 3;
            this.repchkTrans_Flag.Name = "repchkTrans_Flag";
            this.repchkTrans_Flag.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            // 
            // tcProject
            // 
            resources.ApplyResources(this.tcProject, "tcProject");
            this.tcProject.Name = "tcProject";
            this.tcProject.SelectedTabPage = this.tpForeign;
            this.tcProject.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tpForeign});
            // 
            // tpForeign
            // 
            this.tpForeign.Appearance.Header.Font = ((System.Drawing.Font)(resources.GetObject("tpForeign.Appearance.Header.Font")));
            this.tpForeign.Appearance.Header.Options.UseFont = true;
            this.tpForeign.Controls.Add(this.pnlForeign);
            this.tpForeign.Name = "tpForeign";
            resources.ApplyResources(this.tpForeign, "tpForeign");
            // 
            // pnlForeign
            // 
            this.pnlForeign.Controls.Add(this.tpFForeign);
            resources.ApplyResources(this.pnlForeign, "pnlForeign");
            this.pnlForeign.Name = "pnlForeign";
            // 
            // tpFForeign
            // 
            resources.ApplyResources(this.tpFForeign, "tpFForeign");
            this.tpFForeign.Name = "tpFForeign";
            this.tpFForeign.SelectedTabPage = this.tpFLedger;
            this.tpFForeign.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tpFLedger,
            this.tpFVoucher});
            // 
            // tpFLedger
            // 
            this.tpFLedger.Appearance.Header.Font = ((System.Drawing.Font)(resources.GetObject("tpFLedger.Appearance.Header.Font")));
            this.tpFLedger.Appearance.Header.Options.UseFont = true;
            this.tpFLedger.Controls.Add(this.pnlFLedger);
            this.tpFLedger.Name = "tpFLedger";
            resources.ApplyResources(this.tpFLedger, "tpFLedger");
            // 
            // pnlFLedger
            // 
            this.pnlFLedger.Controls.Add(this.gcFLedger);
            resources.ApplyResources(this.pnlFLedger, "pnlFLedger");
            this.pnlFLedger.Name = "pnlFLedger";
            // 
            // gcFLedger
            // 
            resources.ApplyResources(this.gcFLedger, "gcFLedger");
            this.gcFLedger.MainView = this.gvFLedger;
            this.gcFLedger.Name = "gcFLedger";
            this.gcFLedger.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvFLedger});
            // 
            // gvFLedger
            // 
            this.gvFLedger.GridControl = this.gcFLedger;
            this.gvFLedger.Name = "gvFLedger";
            this.gvFLedger.OptionsBehavior.Editable = false;
            this.gvFLedger.OptionsCustomization.AllowColumnMoving = false;
            this.gvFLedger.OptionsCustomization.AllowColumnResizing = false;
            this.gvFLedger.OptionsCustomization.AllowFilter = false;
            this.gvFLedger.OptionsView.ShowAutoFilterRow = true;
            this.gvFLedger.OptionsView.ShowGroupPanel = false;
            // 
            // tpFVoucher
            // 
            this.tpFVoucher.Appearance.Header.Font = ((System.Drawing.Font)(resources.GetObject("tpFVoucher.Appearance.Header.Font")));
            this.tpFVoucher.Appearance.Header.Options.UseFont = true;
            this.tpFVoucher.Controls.Add(this.pnlFVoucher);
            this.tpFVoucher.Name = "tpFVoucher";
            resources.ApplyResources(this.tpFVoucher, "tpFVoucher");
            // 
            // pnlFVoucher
            // 
            resources.ApplyResources(this.pnlFVoucher, "pnlFVoucher");
            this.pnlFVoucher.Name = "pnlFVoucher";
            // 
            // tpLocal
            // 
            this.tpLocal.Appearance.Header.Font = ((System.Drawing.Font)(resources.GetObject("tpLocal.Appearance.Header.Font")));
            this.tpLocal.Appearance.Header.Options.UseFont = true;
            this.tpLocal.Controls.Add(this.pnlLocal);
            this.tpLocal.Name = "tpLocal";
            resources.ApplyResources(this.tpLocal, "tpLocal");
            // 
            // pnlLocal
            // 
            this.pnlLocal.Controls.Add(this.tcLLedgers);
            resources.ApplyResources(this.pnlLocal, "pnlLocal");
            this.pnlLocal.Name = "pnlLocal";
            // 
            // tcLLedgers
            // 
            resources.ApplyResources(this.tcLLedgers, "tcLLedgers");
            this.tcLLedgers.Name = "tcLLedgers";
            // 
            // layoutControl1
            // 
            this.layoutControl1.AllowCustomizationMenu = false;
            this.layoutControl1.Controls.Add(this.chkForeign);
            this.layoutControl1.Controls.Add(this.dateEdit1);
            this.layoutControl1.Controls.Add(this.txtProjectDescription);
            this.layoutControl1.Controls.Add(this.chkLocal);
            this.layoutControl1.Controls.Add(this.txtProjectName);
            this.layoutControl1.Controls.Add(this.dateEdit2);
            this.layoutControl1.Controls.Add(this.txtCode);
            resources.ApplyResources(this.layoutControl1, "layoutControl1");
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(357, 42, 250, 350);
            this.layoutControl1.OptionsCustomizationForm.ShowLayoutTreeView = false;
            this.layoutControl1.Root = this.layoutControlGroup1;
            // 
            // chkForeign
            // 
            resources.ApplyResources(this.chkForeign, "chkForeign");
            this.chkForeign.Name = "chkForeign";
            this.chkForeign.Properties.Caption = resources.GetString("chkForeign.Properties.Caption");
            this.chkForeign.StyleController = this.layoutControl1;
            // 
            // dateEdit1
            // 
            resources.ApplyResources(this.dateEdit1, "dateEdit1");
            this.dateEdit1.Name = "dateEdit1";
            this.dateEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("dateEdit1.Properties.Buttons"))))});
            this.dateEdit1.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateEdit1.StyleController = this.layoutControl1;
            // 
            // txtProjectDescription
            // 
            resources.ApplyResources(this.txtProjectDescription, "txtProjectDescription");
            this.txtProjectDescription.Name = "txtProjectDescription";
            this.txtProjectDescription.StyleController = this.layoutControl1;
            // 
            // chkLocal
            // 
            resources.ApplyResources(this.chkLocal, "chkLocal");
            this.chkLocal.Name = "chkLocal";
            this.chkLocal.Properties.Caption = resources.GetString("chkLocal.Properties.Caption");
            this.chkLocal.StyleController = this.layoutControl1;
            // 
            // txtProjectName
            // 
            resources.ApplyResources(this.txtProjectName, "txtProjectName");
            this.txtProjectName.Name = "txtProjectName";
            this.txtProjectName.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtProjectName.StyleController = this.layoutControl1;
            // 
            // dateEdit2
            // 
            resources.ApplyResources(this.dateEdit2, "dateEdit2");
            this.dateEdit2.Name = "dateEdit2";
            this.dateEdit2.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("dateEdit2.Properties.Buttons"))))});
            this.dateEdit2.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateEdit2.StyleController = this.layoutControl1;
            // 
            // txtCode
            // 
            resources.ApplyResources(this.txtCode, "txtCode");
            this.txtCode.Name = "txtCode";
            this.txtCode.Properties.Appearance.BorderColor = ((System.Drawing.Color)(resources.GetObject("txtCode.Properties.Appearance.BorderColor")));
            this.txtCode.Properties.Appearance.Options.UseBorderColor = true;
            this.txtCode.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.txtCode.StyleController = this.layoutControl1;
            // 
            // layoutControlGroup1
            // 
            resources.ApplyResources(this.layoutControlGroup1, "layoutControlGroup1");
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.emptySpaceItem1,
            this.Code,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.layoutControlItem1,
            this.layoutControlItem4,
            this.layoutControlItem5,
            this.layoutControlItem6,
            this.emptySpaceItem3,
            this.emptySpaceItem2,
            this.simpleLabelItem1});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Size = new System.Drawing.Size(550, 151);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem1, "emptySpaceItem1");
            this.emptySpaceItem1.Location = new System.Drawing.Point(272, 0);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(258, 22);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // Code
            // 
            this.Code.Control = this.txtCode;
            resources.ApplyResources(this.Code, "Code");
            this.Code.Location = new System.Drawing.Point(0, 0);
            this.Code.Name = "Code";
            this.Code.Size = new System.Drawing.Size(272, 22);
            this.Code.TextSize = new System.Drawing.Size(90, 13);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.txtProjectName;
            resources.ApplyResources(this.layoutControlItem2, "layoutControlItem2");
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 22);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(530, 24);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(90, 13);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.txtProjectDescription;
            resources.ApplyResources(this.layoutControlItem3, "layoutControlItem3");
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 46);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(530, 24);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(90, 13);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.dateEdit1;
            resources.ApplyResources(this.layoutControlItem1, "layoutControlItem1");
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 70);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(270, 24);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(90, 13);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.dateEdit2;
            resources.ApplyResources(this.layoutControlItem4, "layoutControlItem4");
            this.layoutControlItem4.Location = new System.Drawing.Point(280, 70);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(250, 24);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(90, 13);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.chkForeign;
            resources.ApplyResources(this.layoutControlItem5, "layoutControlItem5");
            this.layoutControlItem5.Location = new System.Drawing.Point(195, 94);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(335, 23);
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextToControlDistance = 0;
            this.layoutControlItem5.TextVisible = false;
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.chkLocal;
            resources.ApplyResources(this.layoutControlItem6, "layoutControlItem6");
            this.layoutControlItem6.Location = new System.Drawing.Point(94, 94);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(101, 23);
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextToControlDistance = 0;
            this.layoutControlItem6.TextVisible = false;
            // 
            // emptySpaceItem3
            // 
            this.emptySpaceItem3.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem3, "emptySpaceItem3");
            this.emptySpaceItem3.Location = new System.Drawing.Point(270, 70);
            this.emptySpaceItem3.Name = "emptySpaceItem3";
            this.emptySpaceItem3.Size = new System.Drawing.Size(10, 24);
            this.emptySpaceItem3.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem2, "emptySpaceItem2");
            this.emptySpaceItem2.Location = new System.Drawing.Point(0, 117);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(530, 14);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // simpleLabelItem1
            // 
            this.simpleLabelItem1.AllowHotTrack = false;
            resources.ApplyResources(this.simpleLabelItem1, "simpleLabelItem1");
            this.simpleLabelItem1.Location = new System.Drawing.Point(0, 94);
            this.simpleLabelItem1.Name = "simpleLabelItem1";
            this.simpleLabelItem1.Size = new System.Drawing.Size(94, 23);
            this.simpleLabelItem1.TextSize = new System.Drawing.Size(90, 13);
            // 
            // pnlLedger
            // 
            this.pnlLedger.Controls.Add(this.gcLedger);
            resources.ApplyResources(this.pnlLedger, "pnlLedger");
            this.pnlLedger.Name = "pnlLedger";
            // 
            // gcLedger
            // 
            resources.ApplyResources(this.gcLedger, "gcLedger");
            this.gcLedger.MainView = this.gvLedger;
            this.gcLedger.Name = "gcLedger";
            this.gcLedger.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvLedger,
            this.gridView1});
            // 
            // gvLedger
            // 
            this.gvLedger.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.gvLedger.GridControl = this.gcLedger;
            this.gvLedger.Name = "gvLedger";
            this.gvLedger.OptionsBehavior.Editable = false;
            this.gvLedger.OptionsCustomization.AllowColumnMoving = false;
            this.gvLedger.OptionsCustomization.AllowColumnResizing = false;
            this.gvLedger.OptionsCustomization.AllowFilter = false;
            this.gvLedger.OptionsView.ShowAutoFilterRow = true;
            this.gvLedger.OptionsView.ShowGroupPanel = false;
            // 
            // gridView1
            // 
            this.gridView1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.gridView1.GridControl = this.gcLedger;
            this.gridView1.Name = "gridView1";
            // 
            // lcProject
            // 
            this.lcProject.AllowCustomizationMenu = false;
            this.lcProject.Controls.Add(this.glkpPurpose);
            this.lcProject.Controls.Add(this.chkShowFilter);
            this.lcProject.Controls.Add(this.gcSubLedgerProject);
            this.lcProject.Controls.Add(this.btnNew);
            this.lcProject.Controls.Add(this.btnClose);
            this.lcProject.Controls.Add(this.btnSave);
            this.lcProject.Controls.Add(this.txtsubLedger);
            resources.ApplyResources(this.lcProject, "lcProject");
            this.lcProject.HiddenItems.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lciPurpose});
            this.lcProject.Name = "lcProject";
            this.lcProject.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(754, 0, 295, 447);
            this.lcProject.Root = this.lcgProject;
            // 
            // glkpPurpose
            // 
            resources.ApplyResources(this.glkpPurpose, "glkpPurpose");
            this.glkpPurpose.Name = "glkpPurpose";
            this.glkpPurpose.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.glkpPurpose.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("glkpPurpose.Properties.Buttons"))))});
            this.glkpPurpose.Properties.ImmediatePopup = true;
            this.glkpPurpose.Properties.NullText = resources.GetString("glkpPurpose.Properties.NullText");
            this.glkpPurpose.Properties.PopupFormSize = new System.Drawing.Size(367, 0);
            this.glkpPurpose.Properties.View = this.gridView4;
            this.glkpPurpose.StyleController = this.lcProject;
            this.glkpPurpose.EditValueChanged += new System.EventHandler(this.glkpPurpose_EditValueChanged);
            this.glkpPurpose.Leave += new System.EventHandler(this.glkpPurpose_Leave);
            // 
            // gridView4
            // 
            this.gridView4.Appearance.FocusedRow.Font = ((System.Drawing.Font)(resources.GetObject("gridView4.Appearance.FocusedRow.Font")));
            this.gridView4.Appearance.FocusedRow.Options.UseFont = true;
            this.gridView4.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colContributionId,
            this.colCode,
            this.colPurpose});
            this.gridView4.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView4.Name = "gridView4";
            this.gridView4.OptionsBehavior.Editable = false;
            this.gridView4.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView4.OptionsView.ShowColumnHeaders = false;
            this.gridView4.OptionsView.ShowGroupPanel = false;
            this.gridView4.OptionsView.ShowIndicator = false;
            // 
            // colContributionId
            // 
            resources.ApplyResources(this.colContributionId, "colContributionId");
            this.colContributionId.FieldName = "CONTRIBUTION_ID";
            this.colContributionId.Name = "colContributionId";
            this.colContributionId.OptionsColumn.AllowEdit = false;
            this.colContributionId.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colCode
            // 
            resources.ApplyResources(this.colCode, "colCode");
            this.colCode.FieldName = "CODE";
            this.colCode.Name = "colCode";
            this.colCode.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colPurpose
            // 
            resources.ApplyResources(this.colPurpose, "colPurpose");
            this.colPurpose.FieldName = "FC_PURPOSE";
            this.colPurpose.Name = "colPurpose";
            this.colPurpose.OptionsColumn.AllowEdit = false;
            this.colPurpose.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // chkShowFilter
            // 
            resources.ApplyResources(this.chkShowFilter, "chkShowFilter");
            this.chkShowFilter.Name = "chkShowFilter";
            this.chkShowFilter.Properties.AllowHtmlDraw = DevExpress.Utils.DefaultBoolean.True;
            this.chkShowFilter.Properties.Caption = resources.GetString("chkShowFilter.Properties.Caption");
            this.chkShowFilter.StyleController = this.lcProject;
            this.chkShowFilter.CheckedChanged += new System.EventHandler(this.chkShowFilter_CheckedChanged);
            // 
            // gcSubLedgerProject
            // 
            resources.ApplyResources(this.gcSubLedgerProject, "gcSubLedgerProject");
            this.gcSubLedgerProject.MainView = this.gvSubLedgerProject;
            this.gcSubLedgerProject.Name = "gcSubLedgerProject";
            this.gcSubLedgerProject.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.reptxtAmount,
            this.rchkSelect});
            this.gcSubLedgerProject.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvSubLedgerProject});
            this.gcSubLedgerProject.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.gcLedgerProject_PreviewKeyDown);
            // 
            // gvSubLedgerProject
            // 
            this.gvSubLedgerProject.Appearance.HeaderPanel.Font = ((System.Drawing.Font)(resources.GetObject("gvSubLedgerProject.Appearance.HeaderPanel.Font")));
            this.gvSubLedgerProject.Appearance.HeaderPanel.Options.UseFont = true;
            this.gvSubLedgerProject.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colSelect,
            this.colLedgerGroup,
            this.gvColLedgerName,
            this.gvColAmount,
            this.gvColFlag,
            this.gvColchkSelectAll,
            this.gvColLedgerId,
            this.gvColGroupId});
            this.gvSubLedgerProject.GridControl = this.gcSubLedgerProject;
            this.gvSubLedgerProject.Name = "gvSubLedgerProject";
            this.gvSubLedgerProject.OptionsFind.AllowFindPanel = false;
            this.gvSubLedgerProject.OptionsView.ShowGroupPanel = false;
            this.gvSubLedgerProject.OptionsView.ShowIndicator = false;
            this.gvSubLedgerProject.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(this.gvLedgerProject_RowClick);
            this.gvSubLedgerProject.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.gvLedgerProject_RowStyle);
            this.gvSubLedgerProject.CustomRowCellEdit += new DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventHandler(this.gvLedgerProject_CustomRowCellEdit);
            this.gvSubLedgerProject.ShowingEditor += new System.ComponentModel.CancelEventHandler(this.gvLedgerProject_ShowingEditor);
            // 
            // colSelect
            // 
            resources.ApplyResources(this.colSelect, "colSelect");
            this.colSelect.ColumnEdit = this.rchkSelect;
            this.colSelect.FieldName = "SELECT";
            this.colSelect.Name = "colSelect";
            this.colSelect.OptionsColumn.ShowCaption = false;
            // 
            // rchkSelect
            // 
            resources.ApplyResources(this.rchkSelect, "rchkSelect");
            this.rchkSelect.Name = "rchkSelect";
            this.rchkSelect.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
            this.rchkSelect.ValueChecked = 1;
            this.rchkSelect.ValueGrayed = 2;
            this.rchkSelect.ValueUnchecked = 0;
            this.rchkSelect.CheckedChanged += new System.EventHandler(this.rchkSelect_CheckedChanged);
            this.rchkSelect.Click += new System.EventHandler(this.rchkSelect_Click);
            // 
            // colLedgerGroup
            // 
            resources.ApplyResources(this.colLedgerGroup, "colLedgerGroup");
            this.colLedgerGroup.FieldName = "LEDGER_GROUP";
            this.colLedgerGroup.Name = "colLedgerGroup";
            this.colLedgerGroup.OptionsColumn.AllowEdit = false;
            this.colLedgerGroup.OptionsColumn.AllowFocus = false;
            this.colLedgerGroup.OptionsColumn.AllowSize = false;
            // 
            // gvColLedgerName
            // 
            resources.ApplyResources(this.gvColLedgerName, "gvColLedgerName");
            this.gvColLedgerName.FieldName = "LEDGER_NAME";
            this.gvColLedgerName.Name = "gvColLedgerName";
            this.gvColLedgerName.OptionsColumn.AllowEdit = false;
            this.gvColLedgerName.OptionsColumn.AllowFocus = false;
            // 
            // gvColAmount
            // 
            resources.ApplyResources(this.gvColAmount, "gvColAmount");
            this.gvColAmount.ColumnEdit = this.reptxtAmount;
            this.gvColAmount.FieldName = "AMOUNT";
            this.gvColAmount.Name = "gvColAmount";
            this.gvColAmount.OptionsColumn.AllowMove = false;
            this.gvColAmount.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gvColAmount.OptionsColumn.FixedWidth = true;
            this.gvColAmount.OptionsFilter.AllowAutoFilter = false;
            this.gvColAmount.OptionsFilter.AllowFilter = false;
            this.gvColAmount.OptionsFilter.ImmediateUpdateAutoFilter = false;
            // 
            // reptxtAmount
            // 
            resources.ApplyResources(this.reptxtAmount, "reptxtAmount");
            this.reptxtAmount.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.reptxtAmount.DisplayFormat.FormatString = "C";
            this.reptxtAmount.Mask.EditMask = resources.GetString("reptxtAmount.Mask.EditMask");
            this.reptxtAmount.Mask.MaskType = ((DevExpress.XtraEditors.Mask.MaskType)(resources.GetObject("reptxtAmount.Mask.MaskType")));
            this.reptxtAmount.Mask.UseMaskAsDisplayFormat = ((bool)(resources.GetObject("reptxtAmount.Mask.UseMaskAsDisplayFormat")));
            this.reptxtAmount.MaxLength = 13;
            this.reptxtAmount.Name = "reptxtAmount";
            // 
            // gvColFlag
            // 
            resources.ApplyResources(this.gvColFlag, "gvColFlag");
            this.gvColFlag.ColumnEdit = this.repchkTrans_Flag;
            this.gvColFlag.FieldName = "TRANS_MODE";
            this.gvColFlag.Name = "gvColFlag";
            this.gvColFlag.OptionsColumn.AllowSize = false;
            this.gvColFlag.OptionsColumn.ShowCaption = false;
            this.gvColFlag.ShowUnboundExpressionMenu = true;
            // 
            // gvColchkSelectAll
            // 
            resources.ApplyResources(this.gvColchkSelectAll, "gvColchkSelectAll");
            this.gvColchkSelectAll.ColumnEdit = this.repchkSelect;
            this.gvColchkSelectAll.FieldName = "SELECT1";
            this.gvColchkSelectAll.Name = "gvColchkSelectAll";
            this.gvColchkSelectAll.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gvColchkSelectAll.OptionsColumn.ShowCaption = false;
            this.gvColchkSelectAll.OptionsFilter.AllowAutoFilter = false;
            this.gvColchkSelectAll.OptionsFilter.AllowFilter = false;
            // 
            // gvColLedgerId
            // 
            resources.ApplyResources(this.gvColLedgerId, "gvColLedgerId");
            this.gvColLedgerId.FieldName = "LEDGER_ID";
            this.gvColLedgerId.Name = "gvColLedgerId";
            // 
            // gvColGroupId
            // 
            resources.ApplyResources(this.gvColGroupId, "gvColGroupId");
            this.gvColGroupId.FieldName = "GROUP_ID";
            this.gvColGroupId.Name = "gvColGroupId";
            this.gvColGroupId.OptionsColumn.ShowCaption = false;
            // 
            // btnNew
            // 
            this.btnNew.AllowHtmlDraw = DevExpress.Utils.DefaultBoolean.True;
            resources.ApplyResources(this.btnNew, "btnNew");
            this.btnNew.Name = "btnNew";
            this.btnNew.StyleController = this.lcProject;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // btnClose
            // 
            this.btnClose.AllowHtmlDraw = DevExpress.Utils.DefaultBoolean.True;
            resources.ApplyResources(this.btnClose, "btnClose");
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Name = "btnClose";
            this.btnClose.StyleController = this.lcProject;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSave
            // 
            this.btnSave.AllowHtmlDraw = DevExpress.Utils.DefaultBoolean.True;
            resources.ApplyResources(this.btnSave, "btnSave");
            this.btnSave.Name = "btnSave";
            this.btnSave.StyleController = this.lcProject;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtsubLedger
            // 
            resources.ApplyResources(this.txtsubLedger, "txtsubLedger");
            this.txtsubLedger.Name = "txtsubLedger";
            this.txtsubLedger.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtsubLedger.Properties.MaxLength = 100;
            this.txtsubLedger.StyleController = this.lcProject;
            this.txtsubLedger.Leave += new System.EventHandler(this.txtProject_Leave);
            // 
            // lciPurpose
            // 
            this.lciPurpose.AllowHtmlStringInCaption = true;
            this.lciPurpose.Control = this.glkpPurpose;
            resources.ApplyResources(this.lciPurpose, "lciPurpose");
            this.lciPurpose.Location = new System.Drawing.Point(0, 162);
            this.lciPurpose.Name = "lciPurpose";
            this.lciPurpose.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.lciPurpose.Size = new System.Drawing.Size(467, 23);
            this.lciPurpose.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 3);
            this.lciPurpose.TextSize = new System.Drawing.Size(97, 13);
            this.lciPurpose.TextToControlDistance = 5;
            // 
            // lcgProject
            // 
            resources.ApplyResources(this.lcgProject, "lcgProject");
            this.lcgProject.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.lcgProject.GroupBordersVisible = false;
            this.lcgProject.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lcgSubLedger,
            this.emptySpaceItem21,
            this.layoutControlItem7,
            this.layoutControlItem8,
            this.layoutControlItem10,
            this.layoutControlItem9,
            this.layoutControlItem14});
            this.lcgProject.Location = new System.Drawing.Point(0, 0);
            this.lcgProject.Name = "Root";
            this.lcgProject.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.lcgProject.Size = new System.Drawing.Size(420, 436);
            this.lcgProject.TextVisible = false;
            // 
            // lcgSubLedger
            // 
            this.lcgSubLedger.AppearanceGroup.Font = ((System.Drawing.Font)(resources.GetObject("lcgSubLedger.AppearanceGroup.Font")));
            this.lcgSubLedger.AppearanceGroup.Options.UseFont = true;
            resources.ApplyResources(this.lcgSubLedger, "lcgSubLedger");
            this.lcgSubLedger.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lblsubLedger});
            this.lcgSubLedger.Location = new System.Drawing.Point(0, 0);
            this.lcgSubLedger.Name = "lcgSubLedger";
            this.lcgSubLedger.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.lcgSubLedger.ShowInCustomizationForm = false;
            this.lcgSubLedger.Size = new System.Drawing.Size(420, 59);
            this.lcgSubLedger.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            // 
            // lblsubLedger
            // 
            this.lblsubLedger.AllowHtmlStringInCaption = true;
            this.lblsubLedger.Control = this.txtsubLedger;
            resources.ApplyResources(this.lblsubLedger, "lblsubLedger");
            this.lblsubLedger.Location = new System.Drawing.Point(0, 0);
            this.lblsubLedger.Name = "lblsubLedger";
            this.lblsubLedger.ShowInCustomizationForm = false;
            this.lblsubLedger.Size = new System.Drawing.Size(408, 27);
            this.lblsubLedger.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 3);
            this.lblsubLedger.TextSize = new System.Drawing.Size(66, 13);
            // 
            // emptySpaceItem21
            // 
            this.emptySpaceItem21.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem21, "emptySpaceItem21");
            this.emptySpaceItem21.Location = new System.Drawing.Point(0, 410);
            this.emptySpaceItem21.Name = "emptySpaceItem21";
            this.emptySpaceItem21.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.emptySpaceItem21.Size = new System.Drawing.Size(215, 26);
            this.emptySpaceItem21.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.gcSubLedgerProject;
            resources.ApplyResources(this.layoutControlItem7, "layoutControlItem7");
            this.layoutControlItem7.Location = new System.Drawing.Point(0, 59);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(420, 328);
            this.layoutControlItem7.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem7.TextToControlDistance = 0;
            this.layoutControlItem7.TextVisible = false;
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.Control = this.chkShowFilter;
            resources.ApplyResources(this.layoutControlItem8, "layoutControlItem8");
            this.layoutControlItem8.Location = new System.Drawing.Point(0, 387);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Size = new System.Drawing.Size(420, 23);
            this.layoutControlItem8.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem8.TextToControlDistance = 0;
            this.layoutControlItem8.TextVisible = false;
            // 
            // layoutControlItem10
            // 
            this.layoutControlItem10.Control = this.btnClose;
            resources.ApplyResources(this.layoutControlItem10, "layoutControlItem10");
            this.layoutControlItem10.Location = new System.Drawing.Point(353, 410);
            this.layoutControlItem10.Name = "layoutControlItem10";
            this.layoutControlItem10.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 0, 2, 2);
            this.layoutControlItem10.Size = new System.Drawing.Size(67, 26);
            this.layoutControlItem10.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem10.TextToControlDistance = 0;
            this.layoutControlItem10.TextVisible = false;
            // 
            // layoutControlItem9
            // 
            this.layoutControlItem9.Control = this.btnSave;
            resources.ApplyResources(this.layoutControlItem9, "layoutControlItem9");
            this.layoutControlItem9.Location = new System.Drawing.Point(215, 410);
            this.layoutControlItem9.Name = "layoutControlItem9";
            this.layoutControlItem9.Size = new System.Drawing.Size(69, 26);
            this.layoutControlItem9.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem9.TextToControlDistance = 0;
            this.layoutControlItem9.TextVisible = false;
            // 
            // layoutControlItem14
            // 
            this.layoutControlItem14.Control = this.btnNew;
            resources.ApplyResources(this.layoutControlItem14, "layoutControlItem14");
            this.layoutControlItem14.Location = new System.Drawing.Point(284, 410);
            this.layoutControlItem14.Name = "layoutControlItem14";
            this.layoutControlItem14.Size = new System.Drawing.Size(69, 26);
            this.layoutControlItem14.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem14.TextToControlDistance = 0;
            this.layoutControlItem14.TextVisible = false;
            // 
            // icSmallImageCollection
            // 
            this.icSmallImageCollection.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("icSmallImageCollection.ImageStream")));
            this.icSmallImageCollection.Images.SetKeyName(0, "ArrowLeft.png");
            this.icSmallImageCollection.Images.SetKeyName(1, "ArrowRight.png");
            // 
            // emptySpaceItem7
            // 
            this.emptySpaceItem7.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem7, "emptySpaceItem7");
            this.emptySpaceItem7.Location = new System.Drawing.Point(0, 0);
            this.emptySpaceItem7.Name = "emptySpaceItem7";
            this.emptySpaceItem7.Size = new System.Drawing.Size(465, 98);
            this.emptySpaceItem7.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlGroup2
            // 
            resources.ApplyResources(this.layoutControlGroup2, "layoutControlGroup2");
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.emptySpaceItem9});
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 125);
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.OptionsItemText.TextToControlDistance = 5;
            this.layoutControlGroup2.Size = new System.Drawing.Size(465, 53);
            // 
            // emptySpaceItem9
            // 
            this.emptySpaceItem9.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem9, "emptySpaceItem9");
            this.emptySpaceItem9.Location = new System.Drawing.Point(0, 0);
            this.emptySpaceItem9.Name = "emptySpaceItem9";
            this.emptySpaceItem9.Size = new System.Drawing.Size(441, 10);
            this.emptySpaceItem9.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem11
            // 
            this.emptySpaceItem11.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem11, "emptySpaceItem11");
            this.emptySpaceItem11.Location = new System.Drawing.Point(0, 0);
            this.emptySpaceItem11.Name = "emptySpaceItem11";
            this.emptySpaceItem11.Size = new System.Drawing.Size(246, 30);
            this.emptySpaceItem11.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem8
            // 
            this.emptySpaceItem8.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem8, "emptySpaceItem8");
            this.emptySpaceItem8.Location = new System.Drawing.Point(230, 156);
            this.emptySpaceItem8.Name = "emptySpaceItem8";
            this.emptySpaceItem8.Size = new System.Drawing.Size(249, 69);
            this.emptySpaceItem8.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlGroup3
            // 
            resources.ApplyResources(this.layoutControlGroup3, "layoutControlGroup3");
            this.layoutControlGroup3.Location = new System.Drawing.Point(0, 156);
            this.layoutControlGroup3.Name = "layoutControlGroup3";
            this.layoutControlGroup3.OptionsItemText.TextToControlDistance = 5;
            this.layoutControlGroup3.Size = new System.Drawing.Size(230, 69);
            // 
            // layoutControlGroup4
            // 
            resources.ApplyResources(this.layoutControlGroup4, "layoutControlGroup4");
            this.layoutControlGroup4.Location = new System.Drawing.Point(0, 174);
            this.layoutControlGroup4.Name = "layoutControlGroup4";
            this.layoutControlGroup4.OptionsItemText.TextToControlDistance = 5;
            this.layoutControlGroup4.Size = new System.Drawing.Size(479, 89);
            // 
            // chkLedgerSelectAll
            // 
            resources.ApplyResources(this.chkLedgerSelectAll, "chkLedgerSelectAll");
            this.chkLedgerSelectAll.Name = "chkLedgerSelectAll";
            this.chkLedgerSelectAll.Properties.Caption = resources.GetString("chkLedgerSelectAll.Properties.Caption");
            this.chkLedgerSelectAll.CheckedChanged += new System.EventHandler(this.chkLedgerSelectAll_CheckedChanged);
            // 
            // frmSubLeder
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.Controls.Add(this.chkLedgerSelectAll);
            this.Controls.Add(this.lcProject);
            this.Name = "frmSubLeder";
            this.ShowIcon = false;
            this.ShowFilterClicked += new System.EventHandler(this.frmProjectAdd_ShowFilterClicked);
            this.Load += new System.EventHandler(this.frmProjectAdd_Load);
            ((System.ComponentModel.ISupportInitialize)(this.repchkSelect)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repchkTrans_Flag)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tcProject)).EndInit();
            this.tcProject.ResumeLayout(false);
            this.tpForeign.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlForeign)).EndInit();
            this.pnlForeign.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tpFForeign)).EndInit();
            this.tpFForeign.ResumeLayout(false);
            this.tpFLedger.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlFLedger)).EndInit();
            this.pnlFLedger.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcFLedger)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvFLedger)).EndInit();
            this.tpFVoucher.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlFVoucher)).EndInit();
            this.tpLocal.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlLocal)).EndInit();
            this.pnlLocal.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tcLLedgers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chkForeign.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit1.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtProjectDescription.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkLocal.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtProjectName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit2.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Code)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlLedger)).EndInit();
            this.pnlLedger.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcLedger)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvLedger)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcProject)).EndInit();
            this.lcProject.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.glkpPurpose.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowFilter.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcSubLedgerProject)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvSubLedgerProject)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rchkSelect)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.reptxtAmount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtsubLedger.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciPurpose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgProject)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgSubLedger)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblsubLedger)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem21)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem14)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.icSmallImageCollection)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkLedgerSelectAll.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraEditors.CheckEdit chkForeign;
        private DevExpress.XtraEditors.DateEdit dateEdit2;
        private DevExpress.XtraEditors.DateEdit dateEdit1;
        private DevExpress.XtraEditors.TextEdit txtProjectDescription;
        private DevExpress.XtraEditors.CheckEdit chkLocal;
        private DevExpress.XtraEditors.TextEdit txtProjectName;
        private DevExpress.XtraEditors.TextEdit txtCode;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.LayoutControlItem Code;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem3;
        private DevExpress.XtraTab.XtraTabControl tcProject;
        private DevExpress.XtraTab.XtraTabPage tpLocal;
        private DevExpress.XtraTab.XtraTabPage tpForeign;
        private DevExpress.XtraEditors.PanelControl pnlLocal;
        private DevExpress.XtraTab.XtraTabControl tcLLedgers;
        //private DevExpress.XtraTab.XtraTabPage tpVoucher;
        //private DevExpress.XtraTab.XtraTabPage tpLedger;
        private DevExpress.XtraEditors.PanelControl pnlForeign;
        private DevExpress.XtraTab.XtraTabControl tpFForeign;
        private DevExpress.XtraTab.XtraTabPage tpFLedger;
        private DevExpress.XtraEditors.PanelControl pnlFLedger;
        private DevExpress.XtraGrid.GridControl gcFLedger;
        private DevExpress.XtraGrid.Views.Grid.GridView gvFLedger;
        private DevExpress.XtraTab.XtraTabPage tpFVoucher;
        private DevExpress.XtraEditors.PanelControl pnlFVoucher;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraLayout.SimpleLabelItem simpleLabelItem1;
        private DevExpress.XtraEditors.PanelControl pnlLedger;
        private DevExpress.XtraGrid.GridControl gcLedger;
        private DevExpress.XtraGrid.Views.Grid.GridView gvLedger;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraLayout.LayoutControl lcProject;
        private DevExpress.XtraLayout.LayoutControlGroup lcgProject;
        private DevExpress.XtraEditors.TextEdit txtsubLedger;
        private DevExpress.XtraLayout.LayoutControlItem lblsubLedger;
        private DevExpress.XtraLayout.LayoutControlGroup lcgSubLedger;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem9;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem10;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem21;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem7;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem9;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem11;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem8;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup3;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup4;
        private DevExpress.Utils.ImageCollection icSmallImageCollection;
        private DevExpress.XtraEditors.SimpleButton btnNew;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem14;
        private DevExpress.XtraGrid.GridControl gcSubLedgerProject;
        private DevExpress.XtraGrid.Views.Grid.GridView gvSubLedgerProject;
        private DevExpress.XtraGrid.Columns.GridColumn gvColchkSelectAll;
        private DevExpress.XtraGrid.Columns.GridColumn gvColLedgerId;
        private DevExpress.XtraGrid.Columns.GridColumn gvColLedgerName;
        private DevExpress.XtraGrid.Columns.GridColumn gvColAmount;
        private DevExpress.XtraGrid.Columns.GridColumn gvColFlag;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repchkSelect;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repchkTrans_Flag;
        private DevExpress.XtraGrid.Columns.GridColumn gvColGroupId;
        private DevExpress.XtraEditors.CheckEdit chkShowFilter;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit reptxtAmount;
        private DevExpress.XtraGrid.Columns.GridColumn colSelect;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit rchkSelect;
        private DevExpress.XtraEditors.CheckEdit chkLedgerSelectAll;
        private DevExpress.XtraGrid.Columns.GridColumn colLedgerGroup;
        private DevExpress.XtraEditors.GridLookUpEdit glkpPurpose;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView4;
        private DevExpress.XtraLayout.LayoutControlItem lciPurpose;
        private DevExpress.XtraGrid.Columns.GridColumn colContributionId;
        private DevExpress.XtraGrid.Columns.GridColumn colPurpose;
        private DevExpress.XtraGrid.Columns.GridColumn colCode;


    }
}

