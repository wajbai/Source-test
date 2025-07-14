namespace ACPP.Modules.Transaction
{
    partial class frmVoucherTransaction
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmVoucherTransaction));
            this.lcVoucherTrans = new DevExpress.XtraLayout.LayoutControl();
            this.btnMoveTrans = new DevExpress.XtraEditors.SimpleButton();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.MoveDate = new DevExpress.XtraEditors.DateEdit();
            this.gcMoveTrans = new DevExpress.XtraGrid.GridControl();
            this.gvMoveTrans = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colProjectId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSelect = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rchkCheckProject = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.colProject = new DevExpress.XtraGrid.Columns.GridColumn();
            this.lcgVocherTransaction = new DevExpress.XtraLayout.LayoutControlGroup();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblDate = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.lcVoucherTrans)).BeginInit();
            this.lcVoucherTrans.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MoveDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MoveDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcMoveTrans)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMoveTrans)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rchkCheckProject)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgVocherTransaction)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            this.SuspendLayout();
            // 
            // lcVoucherTrans
            // 
            this.lcVoucherTrans.Controls.Add(this.btnMoveTrans);
            this.lcVoucherTrans.Controls.Add(this.btnClose);
            this.lcVoucherTrans.Controls.Add(this.MoveDate);
            this.lcVoucherTrans.Controls.Add(this.gcMoveTrans);
            resources.ApplyResources(this.lcVoucherTrans, "lcVoucherTrans");
            this.lcVoucherTrans.Name = "lcVoucherTrans";
            this.lcVoucherTrans.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(668, 216, 250, 350);
            this.lcVoucherTrans.Root = this.lcgVocherTransaction;
            // 
            // btnMoveTrans
            // 
            resources.ApplyResources(this.btnMoveTrans, "btnMoveTrans");
            this.btnMoveTrans.Name = "btnMoveTrans";
            this.btnMoveTrans.StyleController = this.lcVoucherTrans;
            this.btnMoveTrans.Click += new System.EventHandler(this.btnMoveTrans_Click);
            // 
            // btnClose
            // 
            resources.ApplyResources(this.btnClose, "btnClose");
            this.btnClose.Name = "btnClose";
            this.btnClose.StyleController = this.lcVoucherTrans;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // MoveDate
            // 
            resources.ApplyResources(this.MoveDate, "MoveDate");
            this.MoveDate.Name = "MoveDate";
            this.MoveDate.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.MoveDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("MoveDate.Properties.Buttons"))))});
            this.MoveDate.Properties.Mask.MaskType = ((DevExpress.XtraEditors.Mask.MaskType)(resources.GetObject("MoveDate.Properties.Mask.MaskType")));
            this.MoveDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.MoveDate.StyleController = this.lcVoucherTrans;
            // 
            // gcMoveTrans
            // 
            resources.ApplyResources(this.gcMoveTrans, "gcMoveTrans");
            this.gcMoveTrans.MainView = this.gvMoveTrans;
            this.gcMoveTrans.Name = "gcMoveTrans";
            this.gcMoveTrans.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rchkCheckProject});
            this.gcMoveTrans.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvMoveTrans});
            // 
            // gvMoveTrans
            // 
            this.gvMoveTrans.Appearance.FocusedRow.Font = ((System.Drawing.Font)(resources.GetObject("gvMoveTrans.Appearance.FocusedRow.Font")));
            this.gvMoveTrans.Appearance.FocusedRow.Options.UseFont = true;
            this.gvMoveTrans.Appearance.HeaderPanel.Font = ((System.Drawing.Font)(resources.GetObject("gvMoveTrans.Appearance.HeaderPanel.Font")));
            this.gvMoveTrans.Appearance.HeaderPanel.Options.UseFont = true;
            this.gvMoveTrans.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colProjectId,
            this.colSelect,
            this.colProject});
            this.gvMoveTrans.GridControl = this.gcMoveTrans;
            this.gvMoveTrans.Name = "gvMoveTrans";
            this.gvMoveTrans.OptionsView.ShowGroupPanel = false;
            this.gvMoveTrans.OptionsView.ShowIndicator = false;
            // 
            // colProjectId
            // 
            resources.ApplyResources(this.colProjectId, "colProjectId");
            this.colProjectId.FieldName = "PROJECT_ID";
            this.colProjectId.Name = "colProjectId";
            // 
            // colSelect
            // 
            resources.ApplyResources(this.colSelect, "colSelect");
            this.colSelect.ColumnEdit = this.rchkCheckProject;
            this.colSelect.FieldName = "FLAG";
            this.colSelect.Name = "colSelect";
            this.colSelect.OptionsColumn.ShowCaption = false;
            // 
            // rchkCheckProject
            // 
            resources.ApplyResources(this.rchkCheckProject, "rchkCheckProject");
            this.rchkCheckProject.Name = "rchkCheckProject";
            this.rchkCheckProject.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
            this.rchkCheckProject.ValueChecked = 1;
            this.rchkCheckProject.ValueGrayed = 2;
            this.rchkCheckProject.ValueUnchecked = 0;
            this.rchkCheckProject.CheckedChanged += new System.EventHandler(this.rchkCheckProject_CheckedChanged);
            // 
            // colProject
            // 
            resources.ApplyResources(this.colProject, "colProject");
            this.colProject.FieldName = "PROJECT";
            this.colProject.Name = "colProject";
            this.colProject.OptionsColumn.AllowEdit = false;
            // 
            // lcgVocherTransaction
            // 
            resources.ApplyResources(this.lcgVocherTransaction, "lcgVocherTransaction");
            this.lcgVocherTransaction.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.lcgVocherTransaction.GroupBordersVisible = false;
            this.lcgVocherTransaction.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.emptySpaceItem1,
            this.layoutControlItem1,
            this.lblDate,
            this.emptySpaceItem2,
            this.layoutControlItem2,
            this.layoutControlItem3});
            this.lcgVocherTransaction.Location = new System.Drawing.Point(0, 0);
            this.lcgVocherTransaction.Name = "Root";
            this.lcgVocherTransaction.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.lcgVocherTransaction.Size = new System.Drawing.Size(582, 288);
            this.lcgVocherTransaction.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem1, "emptySpaceItem1");
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 262);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(444, 26);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gcMoveTrans;
            resources.ApplyResources(this.layoutControlItem1, "layoutControlItem1");
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(582, 236);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // lblDate
            // 
            this.lblDate.Control = this.MoveDate;
            resources.ApplyResources(this.lblDate, "lblDate");
            this.lblDate.Location = new System.Drawing.Point(0, 0);
            this.lblDate.Name = "lblDate";
            this.lblDate.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 3, 3);
            this.lblDate.Size = new System.Drawing.Size(109, 26);
            this.lblDate.TextSize = new System.Drawing.Size(23, 13);
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem2, "emptySpaceItem2");
            this.emptySpaceItem2.Location = new System.Drawing.Point(109, 0);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(473, 26);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.btnClose;
            resources.ApplyResources(this.layoutControlItem2, "layoutControlItem2");
            this.layoutControlItem2.Location = new System.Drawing.Point(513, 262);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(69, 26);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.btnMoveTrans;
            resources.ApplyResources(this.layoutControlItem3, "layoutControlItem3");
            this.layoutControlItem3.Location = new System.Drawing.Point(444, 262);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(69, 26);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextToControlDistance = 0;
            this.layoutControlItem3.TextVisible = false;
            // 
            // frmVoucherTransaction
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lcVoucherTrans);
            this.Name = "frmVoucherTransaction";
            this.Load += new System.EventHandler(this.frmVoucherTransaction_Load);
            ((System.ComponentModel.ISupportInitialize)(this.lcVoucherTrans)).EndInit();
            this.lcVoucherTrans.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.MoveDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MoveDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcMoveTrans)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMoveTrans)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rchkCheckProject)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgVocherTransaction)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl lcVoucherTrans;
        private DevExpress.XtraGrid.GridControl gcMoveTrans;
        private DevExpress.XtraGrid.Views.Grid.GridView gvMoveTrans;
        private DevExpress.XtraLayout.LayoutControlGroup lcgVocherTransaction;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraEditors.SimpleButton btnMoveTrans;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraEditors.DateEdit MoveDate;
        private DevExpress.XtraGrid.Columns.GridColumn colProjectId;
        private DevExpress.XtraGrid.Columns.GridColumn colProject;
        private DevExpress.XtraLayout.LayoutControlItem lblDate;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraGrid.Columns.GridColumn colSelect;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit rchkCheckProject;
    }
}