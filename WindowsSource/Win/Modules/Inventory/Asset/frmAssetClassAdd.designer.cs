namespace ACPP.Modules.Inventory
{
    partial class frmAssetClassAdd
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAssetClassAdd));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.glkpParentClassName = new DevExpress.XtraEditors.GridLookUpEdit();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colAssetClassID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAssetClass = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.txtDepreciationPercent = new DevExpress.XtraEditors.TextEdit();
            this.glkDepreciationMethod = new DevExpress.XtraEditors.GridLookUpEdit();
            this.gridLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMethodName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.txtAssetClassName = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lblAssetClass = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblDepreciationPercent = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblMethod = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblParentClass = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.glkpParentClassName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDepreciationPercent.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.glkDepreciationMethod.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAssetClassName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblAssetClass)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblDepreciationPercent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblMethod)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblParentClass)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.glkpParentClassName);
            this.layoutControl1.Controls.Add(this.btnSave);
            this.layoutControl1.Controls.Add(this.btnCancel);
            this.layoutControl1.Controls.Add(this.txtDepreciationPercent);
            this.layoutControl1.Controls.Add(this.glkDepreciationMethod);
            this.layoutControl1.Controls.Add(this.txtAssetClassName);
            resources.ApplyResources(this.layoutControl1, "layoutControl1");
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(283, 350, 250, 350);
            this.layoutControl1.Root = this.layoutControlGroup1;
            // 
            // glkpParentClassName
            // 
            resources.ApplyResources(this.glkpParentClassName, "glkpParentClassName");
            this.glkpParentClassName.EnterMoveNextControl = true;
            this.glkpParentClassName.Name = "glkpParentClassName";
            this.glkpParentClassName.Properties.AppearanceFocused.Font = ((System.Drawing.Font)(resources.GetObject("glkpParentClassName.Properties.AppearanceFocused.Font")));
            this.glkpParentClassName.Properties.AppearanceFocused.Options.UseFont = true;
            this.glkpParentClassName.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.glkpParentClassName.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("glkpParentClassName.Properties.Buttons"))))});
            this.glkpParentClassName.Properties.ImmediatePopup = true;
            this.glkpParentClassName.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            this.glkpParentClassName.Properties.PopupFormMinSize = new System.Drawing.Size(251, 0);
            this.glkpParentClassName.Properties.PopupFormSize = new System.Drawing.Size(345, 0);
            this.glkpParentClassName.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.glkpParentClassName.Properties.View = this.gridView1;
            this.glkpParentClassName.StyleController = this.layoutControl1;
            this.glkpParentClassName.EditValueChanged += new System.EventHandler(this.glkpParentClassName_EditValueChanged);
            this.glkpParentClassName.Leave += new System.EventHandler(this.glkpParentClassName_Leave);
            // 
            // gridView1
            // 
            this.gridView1.Appearance.FocusedRow.Font = ((System.Drawing.Font)(resources.GetObject("gridView1.Appearance.FocusedRow.Font")));
            this.gridView1.Appearance.FocusedRow.Options.UseFont = true;
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colAssetClassID,
            this.colAssetClass});
            this.gridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView1.OptionsView.ShowColumnHeaders = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.OptionsView.ShowIndicator = false;
            // 
            // colAssetClassID
            // 
            resources.ApplyResources(this.colAssetClassID, "colAssetClassID");
            this.colAssetClassID.FieldName = "ASSET_CLASS_ID";
            this.colAssetClassID.Name = "colAssetClassID";
            // 
            // colAssetClass
            // 
            resources.ApplyResources(this.colAssetClass, "colAssetClass");
            this.colAssetClass.FieldName = "ASSET_CLASS";
            this.colAssetClass.Name = "colAssetClass";
            // 
            // btnSave
            // 
            resources.ApplyResources(this.btnSave, "btnSave");
            this.btnSave.Name = "btnSave";
            this.btnSave.StyleController = this.layoutControl1;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.StyleController = this.layoutControl1;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // txtDepreciationPercent
            // 
            this.txtDepreciationPercent.EnterMoveNextControl = true;
            resources.ApplyResources(this.txtDepreciationPercent, "txtDepreciationPercent");
            this.txtDepreciationPercent.Name = "txtDepreciationPercent";
            this.txtDepreciationPercent.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtDepreciationPercent.Properties.Mask.EditMask = resources.GetString("txtDepreciationPercent.Properties.Mask.EditMask");
            this.txtDepreciationPercent.Properties.Mask.MaskType = ((DevExpress.XtraEditors.Mask.MaskType)(resources.GetObject("txtDepreciationPercent.Properties.Mask.MaskType")));
            this.txtDepreciationPercent.Properties.MaxLength = 5;
            this.txtDepreciationPercent.StyleController = this.layoutControl1;
            this.txtDepreciationPercent.Leave += new System.EventHandler(this.txtDepreciationPercent_Leave);
            // 
            // glkDepreciationMethod
            // 
            resources.ApplyResources(this.glkDepreciationMethod, "glkDepreciationMethod");
            this.glkDepreciationMethod.EnterMoveNextControl = true;
            this.glkDepreciationMethod.Name = "glkDepreciationMethod";
            this.glkDepreciationMethod.Properties.AppearanceFocused.Font = ((System.Drawing.Font)(resources.GetObject("glkDepreciationMethod.Properties.AppearanceFocused.Font")));
            this.glkDepreciationMethod.Properties.AppearanceFocused.Options.UseFont = true;
            this.glkDepreciationMethod.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.glkDepreciationMethod.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("glkDepreciationMethod.Properties.Buttons"))))});
            this.glkDepreciationMethod.Properties.ImmediatePopup = true;
            this.glkDepreciationMethod.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            this.glkDepreciationMethod.Properties.PopupFormSize = new System.Drawing.Size(343, 60);
            this.glkDepreciationMethod.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.glkDepreciationMethod.Properties.View = this.gridLookUpEdit1View;
            this.glkDepreciationMethod.StyleController = this.layoutControl1;
            this.glkDepreciationMethod.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.glkDepreciationMethod_ButtonClick);
            this.glkDepreciationMethod.Leave += new System.EventHandler(this.glkDepreciationMethod_Leave);
            // 
            // gridLookUpEdit1View
            // 
            this.gridLookUpEdit1View.Appearance.FocusedRow.Font = ((System.Drawing.Font)(resources.GetObject("gridLookUpEdit1View.Appearance.FocusedRow.Font")));
            this.gridLookUpEdit1View.Appearance.FocusedRow.Options.UseFont = true;
            this.gridLookUpEdit1View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colId,
            this.colMethodName});
            this.gridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridLookUpEdit1View.Name = "gridLookUpEdit1View";
            this.gridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridLookUpEdit1View.OptionsView.ShowColumnHeaders = false;
            this.gridLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            this.gridLookUpEdit1View.OptionsView.ShowIndicator = false;
            // 
            // colId
            // 
            resources.ApplyResources(this.colId, "colId");
            this.colId.FieldName = "METHOD_ID";
            this.colId.Name = "colId";
            // 
            // colMethodName
            // 
            resources.ApplyResources(this.colMethodName, "colMethodName");
            this.colMethodName.FieldName = "DEP_METHOD";
            this.colMethodName.Name = "colMethodName";
            // 
            // txtAssetClassName
            // 
            this.txtAssetClassName.EnterMoveNextControl = true;
            resources.ApplyResources(this.txtAssetClassName, "txtAssetClassName");
            this.txtAssetClassName.Name = "txtAssetClassName";
            this.txtAssetClassName.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtAssetClassName.Properties.MaxLength = 50;
            this.txtAssetClassName.StyleController = this.layoutControl1;
            this.txtAssetClassName.Leave += new System.EventHandler(this.txtAssetClass_Leave_1);
            // 
            // layoutControlGroup1
            // 
            resources.ApplyResources(this.layoutControlGroup1, "layoutControlGroup1");
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lblAssetClass,
            this.lblDepreciationPercent,
            this.lblMethod,
            this.emptySpaceItem1,
            this.emptySpaceItem2,
            this.layoutControlItem2,
            this.layoutControlItem4,
            this.lblParentClass});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(464, 123);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // lblAssetClass
            // 
            this.lblAssetClass.AllowHtmlStringInCaption = true;
            this.lblAssetClass.Control = this.txtAssetClassName;
            resources.ApplyResources(this.lblAssetClass, "lblAssetClass");
            this.lblAssetClass.Location = new System.Drawing.Point(0, 24);
            this.lblAssetClass.Name = "lblAssetClass";
            this.lblAssetClass.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 0, 0, 0);
            this.lblAssetClass.Size = new System.Drawing.Size(464, 23);
            this.lblAssetClass.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 3);
            this.lblAssetClass.TextSize = new System.Drawing.Size(102, 13);
            // 
            // lblDepreciationPercent
            // 
            this.lblDepreciationPercent.AllowHtmlStringInCaption = true;
            this.lblDepreciationPercent.Control = this.txtDepreciationPercent;
            resources.ApplyResources(this.lblDepreciationPercent, "lblDepreciationPercent");
            this.lblDepreciationPercent.Location = new System.Drawing.Point(0, 70);
            this.lblDepreciationPercent.Name = "lblDepreciationPercent";
            this.lblDepreciationPercent.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 0, 0, 0);
            this.lblDepreciationPercent.Size = new System.Drawing.Size(221, 23);
            this.lblDepreciationPercent.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 3);
            this.lblDepreciationPercent.TextSize = new System.Drawing.Size(102, 13);
            // 
            // lblMethod
            // 
            this.lblMethod.AllowHtmlStringInCaption = true;
            this.lblMethod.Control = this.glkDepreciationMethod;
            resources.ApplyResources(this.lblMethod, "lblMethod");
            this.lblMethod.Location = new System.Drawing.Point(0, 47);
            this.lblMethod.Name = "lblMethod";
            this.lblMethod.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 0, 0, 0);
            this.lblMethod.Size = new System.Drawing.Size(464, 23);
            this.lblMethod.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 3);
            this.lblMethod.TextSize = new System.Drawing.Size(102, 13);
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem1, "emptySpaceItem1");
            this.emptySpaceItem1.Location = new System.Drawing.Point(221, 70);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(243, 23);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem2, "emptySpaceItem2");
            this.emptySpaceItem2.Location = new System.Drawing.Point(0, 93);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(326, 30);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.btnCancel;
            resources.ApplyResources(this.layoutControlItem2, "layoutControlItem2");
            this.layoutControlItem2.Location = new System.Drawing.Point(395, 93);
            this.layoutControlItem2.MaxSize = new System.Drawing.Size(69, 29);
            this.layoutControlItem2.MinSize = new System.Drawing.Size(69, 29);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(69, 30);
            this.layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.btnSave;
            resources.ApplyResources(this.layoutControlItem4, "layoutControlItem4");
            this.layoutControlItem4.Location = new System.Drawing.Point(326, 93);
            this.layoutControlItem4.MaxSize = new System.Drawing.Size(69, 29);
            this.layoutControlItem4.MinSize = new System.Drawing.Size(69, 29);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(69, 30);
            this.layoutControlItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextToControlDistance = 0;
            this.layoutControlItem4.TextVisible = false;
            // 
            // lblParentClass
            // 
            this.lblParentClass.AllowHtmlStringInCaption = true;
            this.lblParentClass.Control = this.glkpParentClassName;
            resources.ApplyResources(this.lblParentClass, "lblParentClass");
            this.lblParentClass.Location = new System.Drawing.Point(0, 0);
            this.lblParentClass.MaxSize = new System.Drawing.Size(0, 24);
            this.lblParentClass.MinSize = new System.Drawing.Size(140, 24);
            this.lblParentClass.Name = "lblParentClass";
            this.lblParentClass.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 0, 0, 0);
            this.lblParentClass.Size = new System.Drawing.Size(464, 24);
            this.lblParentClass.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lblParentClass.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 3);
            this.lblParentClass.TextSize = new System.Drawing.Size(102, 13);
            // 
            // frmAssetClassAdd
            // 
            this.AcceptButton = this.btnSave;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.Controls.Add(this.layoutControl1);
            this.Name = "frmAssetClassAdd";
            this.Load += new System.EventHandler(this.frmAssetClassAdd_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.glkpParentClassName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDepreciationPercent.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.glkDepreciationMethod.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAssetClassName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblAssetClass)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblDepreciationPercent)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblMethod)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblParentClass)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraEditors.TextEdit txtDepreciationPercent;
        private DevExpress.XtraEditors.GridLookUpEdit glkDepreciationMethod;
        private DevExpress.XtraGrid.Views.Grid.GridView gridLookUpEdit1View;
        private DevExpress.XtraEditors.TextEdit txtAssetClassName;
        private DevExpress.XtraLayout.LayoutControlItem lblAssetClass;
        private DevExpress.XtraLayout.LayoutControlItem lblDepreciationPercent;
        private DevExpress.XtraLayout.LayoutControlItem lblMethod;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraGrid.Columns.GridColumn colId;
        private DevExpress.XtraGrid.Columns.GridColumn colMethodName;
        private DevExpress.XtraEditors.GridLookUpEdit glkpParentClassName;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraLayout.LayoutControlItem lblParentClass;
        private DevExpress.XtraGrid.Columns.GridColumn colAssetClassID;
        private DevExpress.XtraGrid.Columns.GridColumn colAssetClass;
    }
}