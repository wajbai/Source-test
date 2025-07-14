namespace ACPP.Modules.Inventory
{
    partial class frmVendorInfoAdd
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmVendorInfoAdd));
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.glkpCountry = new DevExpress.XtraEditors.GridLookUpEdit();
            this.glvCountry = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colCountryId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCountry = new DevExpress.XtraGrid.Columns.GridColumn();
            this.glkpState = new DevExpress.XtraEditors.GridLookUpEdit();
            this.glvAddress = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colStateId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colStateName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.txtGSTNo = new DevExpress.XtraEditors.TextEdit();
            this.meAddress = new DevExpress.XtraEditors.MemoEdit();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.txtEmail = new DevExpress.XtraEditors.TextEdit();
            this.txtTelephoneNo = new DevExpress.XtraEditors.TextEdit();
            this.txtPanNo = new DevExpress.XtraEditors.TextEdit();
            this.txtName = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lblName = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem9 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem10 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem11 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.lblPan = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcGSTNo = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.glkpCountry.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.glvCountry)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.glkpState.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.glvAddress)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtGSTNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.meAddress.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEmail.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTelephoneNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPanNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblPan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcGSTNo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.glkpCountry);
            this.layoutControl1.Controls.Add(this.glkpState);
            this.layoutControl1.Controls.Add(this.txtGSTNo);
            this.layoutControl1.Controls.Add(this.meAddress);
            this.layoutControl1.Controls.Add(this.btnClose);
            this.layoutControl1.Controls.Add(this.btnSave);
            this.layoutControl1.Controls.Add(this.txtEmail);
            this.layoutControl1.Controls.Add(this.txtTelephoneNo);
            this.layoutControl1.Controls.Add(this.txtPanNo);
            this.layoutControl1.Controls.Add(this.txtName);
            resources.ApplyResources(this.layoutControl1, "layoutControl1");
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(527, 166, 250, 350);
            this.layoutControl1.Root = this.layoutControlGroup1;
            // 
            // glkpCountry
            // 
            resources.ApplyResources(this.glkpCountry, "glkpCountry");
            this.glkpCountry.Name = "glkpCountry";
            this.glkpCountry.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.glkpCountry.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("glkpCountry.Properties.Buttons")))),
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("glkpCountry.Properties.Buttons1"))), resources.GetString("glkpCountry.Properties.Buttons2"), ((int)(resources.GetObject("glkpCountry.Properties.Buttons3"))), ((bool)(resources.GetObject("glkpCountry.Properties.Buttons4"))), ((bool)(resources.GetObject("glkpCountry.Properties.Buttons5"))), ((bool)(resources.GetObject("glkpCountry.Properties.Buttons6"))), ((DevExpress.XtraEditors.ImageLocation)(resources.GetObject("glkpCountry.Properties.Buttons7"))), ((System.Drawing.Image)(resources.GetObject("glkpCountry.Properties.Buttons8"))), new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, resources.GetString("glkpCountry.Properties.Buttons9"), ((object)(resources.GetObject("glkpCountry.Properties.Buttons10"))), ((DevExpress.Utils.SuperToolTip)(resources.GetObject("glkpCountry.Properties.Buttons11"))), ((bool)(resources.GetObject("glkpCountry.Properties.Buttons12"))))});
            this.glkpCountry.Properties.ImmediatePopup = true;
            this.glkpCountry.Properties.NullText = resources.GetString("glkpCountry.Properties.NullText");
            this.glkpCountry.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            this.glkpCountry.Properties.PopupFormMinSize = new System.Drawing.Size(285, 0);
            this.glkpCountry.Properties.PopupFormSize = new System.Drawing.Size(285, 0);
            this.glkpCountry.Properties.View = this.glvCountry;
            this.glkpCountry.Properties.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.glkpCountry_Properties_ButtonClick);
            this.glkpCountry.StyleController = this.layoutControl1;
            this.glkpCountry.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.glkpCountry_ButtonClick);
            this.glkpCountry.EditValueChanged += new System.EventHandler(this.glkpCountry_EditValueChanged);
            // 
            // glvCountry
            // 
            this.glvCountry.Appearance.FocusedRow.Font = ((System.Drawing.Font)(resources.GetObject("glvCountry.Appearance.FocusedRow.Font")));
            this.glvCountry.Appearance.FocusedRow.Options.UseFont = true;
            this.glvCountry.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colCountryId,
            this.colCountry});
            this.glvCountry.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.glvCountry.Name = "glvCountry";
            this.glvCountry.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.glvCountry.OptionsView.ShowColumnHeaders = false;
            this.glvCountry.OptionsView.ShowGroupPanel = false;
            this.glvCountry.OptionsView.ShowIndicator = false;
            // 
            // colCountryId
            // 
            resources.ApplyResources(this.colCountryId, "colCountryId");
            this.colCountryId.FieldName = "COUTNRY_ID";
            this.colCountryId.Name = "colCountryId";
            // 
            // colCountry
            // 
            resources.ApplyResources(this.colCountry, "colCountry");
            this.colCountry.FieldName = "COUNTRY";
            this.colCountry.Name = "colCountry";
            // 
            // glkpState
            // 
            resources.ApplyResources(this.glkpState, "glkpState");
            this.glkpState.Name = "glkpState";
            this.glkpState.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.glkpState.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("glkpState.Properties.Buttons")))),
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("glkpState.Properties.Buttons1"))), resources.GetString("glkpState.Properties.Buttons2"), ((int)(resources.GetObject("glkpState.Properties.Buttons3"))), ((bool)(resources.GetObject("glkpState.Properties.Buttons4"))), ((bool)(resources.GetObject("glkpState.Properties.Buttons5"))), ((bool)(resources.GetObject("glkpState.Properties.Buttons6"))), ((DevExpress.XtraEditors.ImageLocation)(resources.GetObject("glkpState.Properties.Buttons7"))), ((System.Drawing.Image)(resources.GetObject("glkpState.Properties.Buttons8"))), new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject2, resources.GetString("glkpState.Properties.Buttons9"), ((object)(resources.GetObject("glkpState.Properties.Buttons10"))), ((DevExpress.Utils.SuperToolTip)(resources.GetObject("glkpState.Properties.Buttons11"))), ((bool)(resources.GetObject("glkpState.Properties.Buttons12"))))});
            this.glkpState.Properties.NullText = resources.GetString("glkpState.Properties.NullText");
            this.glkpState.Properties.PopupFormMinSize = new System.Drawing.Size(283, 0);
            this.glkpState.Properties.PopupFormSize = new System.Drawing.Size(283, 0);
            this.glkpState.Properties.View = this.glvAddress;
            this.glkpState.Properties.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.glkpState_Properties_ButtonClick);
            this.glkpState.StyleController = this.layoutControl1;
            this.glkpState.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.glkpState_ButtonClick);
            // 
            // glvAddress
            // 
            this.glvAddress.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colStateId,
            this.colStateName});
            this.glvAddress.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.glvAddress.Name = "glvAddress";
            this.glvAddress.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.glvAddress.OptionsView.ShowColumnHeaders = false;
            this.glvAddress.OptionsView.ShowGroupPanel = false;
            this.glvAddress.OptionsView.ShowIndicator = false;
            // 
            // colStateId
            // 
            resources.ApplyResources(this.colStateId, "colStateId");
            this.colStateId.FieldName = "STATE_ID";
            this.colStateId.Name = "colStateId";
            // 
            // colStateName
            // 
            resources.ApplyResources(this.colStateName, "colStateName");
            this.colStateName.FieldName = "STATE_NAME";
            this.colStateName.Name = "colStateName";
            // 
            // txtGSTNo
            // 
            this.txtGSTNo.EnterMoveNextControl = true;
            resources.ApplyResources(this.txtGSTNo, "txtGSTNo");
            this.txtGSTNo.Name = "txtGSTNo";
            this.txtGSTNo.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtGSTNo.Properties.MaxLength = 25;
            this.txtGSTNo.StyleController = this.layoutControl1;
            // 
            // meAddress
            // 
            this.meAddress.EnterMoveNextControl = true;
            resources.ApplyResources(this.meAddress, "meAddress");
            this.meAddress.Name = "meAddress";
            this.meAddress.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.meAddress.Properties.MaxLength = 200;
            this.meAddress.StyleController = this.layoutControl1;
            this.meAddress.UseOptimizedRendering = true;
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.btnClose, "btnClose");
            this.btnClose.Name = "btnClose";
            this.btnClose.StyleController = this.layoutControl1;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSave
            // 
            resources.ApplyResources(this.btnSave, "btnSave");
            this.btnSave.Name = "btnSave";
            this.btnSave.StyleController = this.layoutControl1;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtEmail
            // 
            this.txtEmail.EnterMoveNextControl = true;
            resources.ApplyResources(this.txtEmail, "txtEmail");
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtEmail.Properties.Mask.EditMask = resources.GetString("txtEmail.Properties.Mask.EditMask");
            this.txtEmail.Properties.Mask.MaskType = ((DevExpress.XtraEditors.Mask.MaskType)(resources.GetObject("txtEmail.Properties.Mask.MaskType")));
            this.txtEmail.Properties.Mask.ShowPlaceHolders = ((bool)(resources.GetObject("txtEmail.Properties.Mask.ShowPlaceHolders")));
            this.txtEmail.Properties.MaxLength = 50;
            this.txtEmail.StyleController = this.layoutControl1;
            // 
            // txtTelephoneNo
            // 
            this.txtTelephoneNo.EnterMoveNextControl = true;
            resources.ApplyResources(this.txtTelephoneNo, "txtTelephoneNo");
            this.txtTelephoneNo.Name = "txtTelephoneNo";
            this.txtTelephoneNo.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtTelephoneNo.Properties.Mask.ShowPlaceHolders = ((bool)(resources.GetObject("txtTelephoneNo.Properties.Mask.ShowPlaceHolders")));
            this.txtTelephoneNo.Properties.MaxLength = 14;
            this.txtTelephoneNo.StyleController = this.layoutControl1;
            // 
            // txtPanNo
            // 
            this.txtPanNo.EnterMoveNextControl = true;
            resources.ApplyResources(this.txtPanNo, "txtPanNo");
            this.txtPanNo.Name = "txtPanNo";
            this.txtPanNo.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtPanNo.Properties.Mask.AutoComplete = ((DevExpress.XtraEditors.Mask.AutoCompleteType)(resources.GetObject("txtPanNo.Properties.Mask.AutoComplete")));
            this.txtPanNo.Properties.Mask.EditMask = resources.GetString("txtPanNo.Properties.Mask.EditMask");
            this.txtPanNo.Properties.Mask.MaskType = ((DevExpress.XtraEditors.Mask.MaskType)(resources.GetObject("txtPanNo.Properties.Mask.MaskType")));
            this.txtPanNo.Properties.MaxLength = 25;
            this.txtPanNo.StyleController = this.layoutControl1;
            // 
            // txtName
            // 
            this.txtName.EnterMoveNextControl = true;
            resources.ApplyResources(this.txtName, "txtName");
            this.txtName.Name = "txtName";
            this.txtName.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtName.Properties.MaxLength = 50;
            this.txtName.StyleController = this.layoutControl1;
            this.txtName.Leave += new System.EventHandler(this.txtName_Leave);
            // 
            // layoutControlGroup1
            // 
            resources.ApplyResources(this.layoutControlGroup1, "layoutControlGroup1");
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lblName,
            this.layoutControlItem8,
            this.layoutControlItem9,
            this.layoutControlItem10,
            this.layoutControlItem11,
            this.layoutControlItem1,
            this.emptySpaceItem1,
            this.lblPan,
            this.lcGSTNo,
            this.layoutControlItem2,
            this.layoutControlItem3});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(369, 225);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // lblName
            // 
            this.lblName.AllowHtmlStringInCaption = true;
            this.lblName.Control = this.txtName;
            resources.ApplyResources(this.lblName, "lblName");
            this.lblName.Location = new System.Drawing.Point(0, 0);
            this.lblName.Name = "lblName";
            this.lblName.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 0, 2, 0);
            this.lblName.Size = new System.Drawing.Size(369, 30);
            this.lblName.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 5, 3);
            this.lblName.TextSize = new System.Drawing.Size(54, 13);
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.Control = this.txtTelephoneNo;
            resources.ApplyResources(this.layoutControlItem8, "layoutControlItem8");
            this.layoutControlItem8.Location = new System.Drawing.Point(0, 79);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 0, 2, 0);
            this.layoutControlItem8.Size = new System.Drawing.Size(369, 25);
            this.layoutControlItem8.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 3);
            this.layoutControlItem8.TextSize = new System.Drawing.Size(54, 13);
            // 
            // layoutControlItem9
            // 
            this.layoutControlItem9.Control = this.txtEmail;
            resources.ApplyResources(this.layoutControlItem9, "layoutControlItem9");
            this.layoutControlItem9.Location = new System.Drawing.Point(0, 104);
            this.layoutControlItem9.Name = "layoutControlItem9";
            this.layoutControlItem9.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 0, 2, 0);
            this.layoutControlItem9.Size = new System.Drawing.Size(369, 25);
            this.layoutControlItem9.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 3);
            this.layoutControlItem9.TextSize = new System.Drawing.Size(54, 13);
            // 
            // layoutControlItem10
            // 
            this.layoutControlItem10.Control = this.btnSave;
            resources.ApplyResources(this.layoutControlItem10, "layoutControlItem10");
            this.layoutControlItem10.Location = new System.Drawing.Point(227, 199);
            this.layoutControlItem10.MaxSize = new System.Drawing.Size(71, 26);
            this.layoutControlItem10.MinSize = new System.Drawing.Size(71, 26);
            this.layoutControlItem10.Name = "layoutControlItem10";
            this.layoutControlItem10.Size = new System.Drawing.Size(71, 26);
            this.layoutControlItem10.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem10.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem10.TextToControlDistance = 0;
            this.layoutControlItem10.TextVisible = false;
            // 
            // layoutControlItem11
            // 
            this.layoutControlItem11.Control = this.btnClose;
            resources.ApplyResources(this.layoutControlItem11, "layoutControlItem11");
            this.layoutControlItem11.Location = new System.Drawing.Point(298, 199);
            this.layoutControlItem11.MaxSize = new System.Drawing.Size(71, 26);
            this.layoutControlItem11.MinSize = new System.Drawing.Size(71, 26);
            this.layoutControlItem11.Name = "layoutControlItem11";
            this.layoutControlItem11.Size = new System.Drawing.Size(71, 26);
            this.layoutControlItem11.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem11.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem11.TextToControlDistance = 0;
            this.layoutControlItem11.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.meAddress;
            resources.ApplyResources(this.layoutControlItem1, "layoutControlItem1");
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 129);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 0, 2, 0);
            this.layoutControlItem1.Size = new System.Drawing.Size(369, 22);
            this.layoutControlItem1.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 3);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(54, 13);
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem1, "emptySpaceItem1");
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 199);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(227, 26);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // lblPan
            // 
            this.lblPan.Control = this.txtPanNo;
            resources.ApplyResources(this.lblPan, "lblPan");
            this.lblPan.Location = new System.Drawing.Point(0, 30);
            this.lblPan.Name = "lblPan";
            this.lblPan.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 0, 2, 0);
            this.lblPan.Size = new System.Drawing.Size(369, 25);
            this.lblPan.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 3);
            this.lblPan.TextSize = new System.Drawing.Size(54, 13);
            // 
            // lcGSTNo
            // 
            this.lcGSTNo.Control = this.txtGSTNo;
            resources.ApplyResources(this.lcGSTNo, "lcGSTNo");
            this.lcGSTNo.Location = new System.Drawing.Point(0, 55);
            this.lcGSTNo.Name = "lcGSTNo";
            this.lcGSTNo.Size = new System.Drawing.Size(369, 24);
            this.lcGSTNo.TextSize = new System.Drawing.Size(54, 13);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.glkpState;
            resources.ApplyResources(this.layoutControlItem2, "layoutControlItem2");
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 175);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(369, 24);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(54, 13);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.glkpCountry;
            resources.ApplyResources(this.layoutControlItem3, "layoutControlItem3");
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 151);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(369, 24);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(54, 13);
            // 
            // frmVendorInfoAdd
            // 
            this.AcceptButton = this.btnSave;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.Controls.Add(this.layoutControl1);
            this.Name = "frmVendorInfoAdd";
            this.Load += new System.EventHandler(this.frmVendorInfoAdd_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.glkpCountry.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.glvCountry)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.glkpState.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.glvAddress)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtGSTNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.meAddress.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEmail.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTelephoneNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPanNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblPan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcGSTNo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.TextEdit txtEmail;
        private DevExpress.XtraEditors.TextEdit txtTelephoneNo;
        private DevExpress.XtraEditors.TextEdit txtPanNo;
        private DevExpress.XtraEditors.TextEdit txtName;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem lblName;
        private DevExpress.XtraLayout.LayoutControlItem lblPan;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem9;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem10;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem11;
        private DevExpress.XtraEditors.MemoEdit meAddress;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraEditors.TextEdit txtGSTNo;
        private DevExpress.XtraLayout.LayoutControlItem lcGSTNo;
        private DevExpress.XtraEditors.GridLookUpEdit glkpState;
        private DevExpress.XtraGrid.Views.Grid.GridView glvAddress;
        private DevExpress.XtraGrid.Columns.GridColumn colStateId;
        private DevExpress.XtraGrid.Columns.GridColumn colStateName;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraEditors.GridLookUpEdit glkpCountry;
        private DevExpress.XtraGrid.Views.Grid.GridView glvCountry;
        private DevExpress.XtraGrid.Columns.GridColumn colCountryId;
        private DevExpress.XtraGrid.Columns.GridColumn colCountry;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
    }
}