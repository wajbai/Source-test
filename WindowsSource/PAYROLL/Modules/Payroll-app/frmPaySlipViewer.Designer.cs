namespace PAYROLL.Modules.Payroll_app
{
    partial class frmPaySlipViewer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPaySlipViewer));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.lblUserName = new DevExpress.XtraEditors.LabelControl();
            this.chklstStaff = new DevExpress.XtraEditors.LabelControl();
            this.chkAll = new DevExpress.XtraEditors.CheckEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.btnShow = new DevExpress.XtraEditors.SimpleButton();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.chkPrintPay = new DevExpress.XtraEditors.CheckEdit();
            this.chklstSelectStaff = new DevExpress.XtraEditors.CheckedListBoxControl();
            this.glkpGroupValue = new DevExpress.XtraEditors.GridLookUpEdit();
            this.gridLookUpEdit2View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.txtPath = new DevExpress.XtraEditors.TextEdit();
            this.btnTemp = new DevExpress.XtraEditors.SimpleButton();
            this.glkpPayPrinters = new DevExpress.XtraEditors.ComboBoxEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lblPrinter = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblRegSlipTemplate = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblSalaryGroup = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem9 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblUser = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkAll.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkPrintPay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chklstSelectStaff)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.glkpGroupValue.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit2View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPath.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.glkpPayPrinters.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblPrinter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblRegSlipTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblSalaryGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblUser)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.lblUserName);
            this.layoutControl1.Controls.Add(this.chklstStaff);
            this.layoutControl1.Controls.Add(this.chkAll);
            this.layoutControl1.Controls.Add(this.labelControl2);
            this.layoutControl1.Controls.Add(this.btnShow);
            this.layoutControl1.Controls.Add(this.btnClose);
            this.layoutControl1.Controls.Add(this.chkPrintPay);
            this.layoutControl1.Controls.Add(this.chklstSelectStaff);
            this.layoutControl1.Controls.Add(this.glkpGroupValue);
            this.layoutControl1.Controls.Add(this.txtPath);
            this.layoutControl1.Controls.Add(this.btnTemp);
            this.layoutControl1.Controls.Add(this.glkpPayPrinters);
            resources.ApplyResources(this.layoutControl1, "layoutControl1");
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(793, 211, 250, 350);
            this.layoutControl1.Root = this.layoutControlGroup1;
            // 
            // lblUserName
            // 
            this.lblUserName.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("lblUserName.Appearance.Font")));
            resources.ApplyResources(this.lblUserName, "lblUserName");
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.StyleController = this.layoutControl1;
            // 
            // chklstStaff
            // 
            resources.ApplyResources(this.chklstStaff, "chklstStaff");
            this.chklstStaff.Name = "chklstStaff";
            this.chklstStaff.StyleController = this.layoutControl1;
            // 
            // chkAll
            // 
            resources.ApplyResources(this.chkAll, "chkAll");
            this.chkAll.Name = "chkAll";
            this.chkAll.Properties.Caption = resources.GetString("chkAll.Properties.Caption");
            this.chkAll.StyleController = this.layoutControl1;
            this.chkAll.CheckedChanged += new System.EventHandler(this.chkAll_CheckedChanged);
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("labelControl2.Appearance.Font")));
            resources.ApplyResources(this.labelControl2, "labelControl2");
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.StyleController = this.layoutControl1;
            // 
            // btnShow
            // 
            this.btnShow.AllowHtmlDraw = DevExpress.Utils.DefaultBoolean.True;
            resources.ApplyResources(this.btnShow, "btnShow");
            this.btnShow.MaximumSize = new System.Drawing.Size(65, 0);
            this.btnShow.MinimumSize = new System.Drawing.Size(65, 0);
            this.btnShow.Name = "btnShow";
            this.btnShow.StyleController = this.layoutControl1;
            this.btnShow.Click += new System.EventHandler(this.btnShow_Click);
            // 
            // btnClose
            // 
            this.btnClose.AllowHtmlDraw = DevExpress.Utils.DefaultBoolean.True;
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.btnClose, "btnClose");
            this.btnClose.MaximumSize = new System.Drawing.Size(65, 0);
            this.btnClose.MinimumSize = new System.Drawing.Size(65, 0);
            this.btnClose.Name = "btnClose";
            this.btnClose.StyleController = this.layoutControl1;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // chkPrintPay
            // 
            resources.ApplyResources(this.chkPrintPay, "chkPrintPay");
            this.chkPrintPay.Name = "chkPrintPay";
            this.chkPrintPay.Properties.Caption = resources.GetString("chkPrintPay.Properties.Caption");
            this.chkPrintPay.StyleController = this.layoutControl1;
            // 
            // chklstSelectStaff
            // 
            resources.ApplyResources(this.chklstSelectStaff, "chklstSelectStaff");
            this.chklstSelectStaff.Name = "chklstSelectStaff";
            this.chklstSelectStaff.StyleController = this.layoutControl1;
            // 
            // glkpGroupValue
            // 
            resources.ApplyResources(this.glkpGroupValue, "glkpGroupValue");
            this.glkpGroupValue.Name = "glkpGroupValue";
            this.glkpGroupValue.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.glkpGroupValue.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("glkpGroupValue.Properties.Buttons"))))});
            this.glkpGroupValue.Properties.NullText = resources.GetString("glkpGroupValue.Properties.NullText");
            this.glkpGroupValue.Properties.View = this.gridLookUpEdit2View;
            this.glkpGroupValue.StyleController = this.layoutControl1;
            this.glkpGroupValue.EditValueChanged += new System.EventHandler(this.glkpGroupValue_EditValueChanged);
            // 
            // gridLookUpEdit2View
            // 
            this.gridLookUpEdit2View.Appearance.FocusedRow.Font = ((System.Drawing.Font)(resources.GetObject("gridLookUpEdit2View.Appearance.FocusedRow.Font")));
            this.gridLookUpEdit2View.Appearance.FocusedRow.Options.UseFont = true;
            this.gridLookUpEdit2View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridLookUpEdit2View.Name = "gridLookUpEdit2View";
            this.gridLookUpEdit2View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridLookUpEdit2View.OptionsView.ShowColumnHeaders = false;
            this.gridLookUpEdit2View.OptionsView.ShowGroupPanel = false;
            this.gridLookUpEdit2View.OptionsView.ShowIndicator = false;
            // 
            // txtPath
            // 
            resources.ApplyResources(this.txtPath, "txtPath");
            this.txtPath.Name = "txtPath";
            this.txtPath.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtPath.StyleController = this.layoutControl1;
            // 
            // btnTemp
            // 
            resources.ApplyResources(this.btnTemp, "btnTemp");
            this.btnTemp.Name = "btnTemp";
            this.btnTemp.StyleController = this.layoutControl1;
            this.btnTemp.Click += new System.EventHandler(this.btnTemp_Click);
            // 
            // glkpPayPrinters
            // 
            resources.ApplyResources(this.glkpPayPrinters, "glkpPayPrinters");
            this.glkpPayPrinters.Name = "glkpPayPrinters";
            this.glkpPayPrinters.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.glkpPayPrinters.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("glkpPayPrinters.Properties.Buttons"))))});
            this.glkpPayPrinters.Properties.PopupSizeable = true;
            this.glkpPayPrinters.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.glkpPayPrinters.StyleController = this.layoutControl1;
            // 
            // layoutControlGroup1
            // 
            resources.ApplyResources(this.layoutControlGroup1, "layoutControlGroup1");
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lblPrinter,
            this.lblRegSlipTemplate,
            this.lblSalaryGroup,
            this.layoutControlItem6,
            this.layoutControlItem3,
            this.layoutControlItem7,
            this.layoutControlItem8,
            this.layoutControlItem4,
            this.layoutControlItem1,
            this.emptySpaceItem2,
            this.layoutControlItem2,
            this.layoutControlItem9,
            this.lblUser,
            this.emptySpaceItem1});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(454, 434);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // lblPrinter
            // 
            this.lblPrinter.Control = this.glkpPayPrinters;
            resources.ApplyResources(this.lblPrinter, "lblPrinter");
            this.lblPrinter.Location = new System.Drawing.Point(0, 20);
            this.lblPrinter.Name = "lblPrinter";
            this.lblPrinter.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 3, 3);
            this.lblPrinter.Size = new System.Drawing.Size(454, 26);
            this.lblPrinter.TextSize = new System.Drawing.Size(69, 13);
            // 
            // lblRegSlipTemplate
            // 
            this.lblRegSlipTemplate.Control = this.txtPath;
            resources.ApplyResources(this.lblRegSlipTemplate, "lblRegSlipTemplate");
            this.lblRegSlipTemplate.Location = new System.Drawing.Point(0, 46);
            this.lblRegSlipTemplate.Name = "lblRegSlipTemplate";
            this.lblRegSlipTemplate.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 3, 3);
            this.lblRegSlipTemplate.Size = new System.Drawing.Size(416, 26);
            this.lblRegSlipTemplate.TextSize = new System.Drawing.Size(69, 13);
            // 
            // lblSalaryGroup
            // 
            this.lblSalaryGroup.Control = this.glkpGroupValue;
            resources.ApplyResources(this.lblSalaryGroup, "lblSalaryGroup");
            this.lblSalaryGroup.Location = new System.Drawing.Point(0, 72);
            this.lblSalaryGroup.Name = "lblSalaryGroup";
            this.lblSalaryGroup.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 3, 3);
            this.lblSalaryGroup.Size = new System.Drawing.Size(454, 26);
            this.lblSalaryGroup.TextSize = new System.Drawing.Size(69, 13);
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.chkPrintPay;
            resources.ApplyResources(this.layoutControlItem6, "layoutControlItem6");
            this.layoutControlItem6.Location = new System.Drawing.Point(0, 408);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(221, 26);
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextToControlDistance = 0;
            this.layoutControlItem6.TextVisible = false;
            this.layoutControlItem6.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.btnTemp;
            resources.ApplyResources(this.layoutControlItem3, "layoutControlItem3");
            this.layoutControlItem3.Location = new System.Drawing.Point(416, 46);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(38, 26);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextToControlDistance = 0;
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.btnClose;
            resources.ApplyResources(this.layoutControlItem7, "layoutControlItem7");
            this.layoutControlItem7.Location = new System.Drawing.Point(385, 408);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(69, 26);
            this.layoutControlItem7.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem7.TextToControlDistance = 0;
            this.layoutControlItem7.TextVisible = false;
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.Control = this.btnShow;
            resources.ApplyResources(this.layoutControlItem8, "layoutControlItem8");
            this.layoutControlItem8.Location = new System.Drawing.Point(316, 408);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Size = new System.Drawing.Size(69, 26);
            this.layoutControlItem8.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem8.TextToControlDistance = 0;
            this.layoutControlItem8.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.chklstSelectStaff;
            resources.ApplyResources(this.layoutControlItem4, "layoutControlItem4");
            this.layoutControlItem4.Location = new System.Drawing.Point(67, 121);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(387, 287);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextToControlDistance = 0;
            this.layoutControlItem4.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.chkAll;
            resources.ApplyResources(this.layoutControlItem1, "layoutControlItem1");
            this.layoutControlItem1.Location = new System.Drawing.Point(67, 98);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(387, 23);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem2, "emptySpaceItem2");
            this.emptySpaceItem2.Location = new System.Drawing.Point(0, 121);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(67, 287);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.chklstStaff;
            resources.ApplyResources(this.layoutControlItem2, "layoutControlItem2");
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 98);
            this.layoutControlItem2.MinSize = new System.Drawing.Size(61, 20);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(67, 23);
            this.layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem9
            // 
            this.layoutControlItem9.Control = this.lblUserName;
            resources.ApplyResources(this.layoutControlItem9, "layoutControlItem9");
            this.layoutControlItem9.Location = new System.Drawing.Point(90, 0);
            this.layoutControlItem9.MinSize = new System.Drawing.Size(85, 20);
            this.layoutControlItem9.Name = "layoutControlItem9";
            this.layoutControlItem9.Size = new System.Drawing.Size(364, 20);
            this.layoutControlItem9.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem9.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem9.TextToControlDistance = 0;
            this.layoutControlItem9.TextVisible = false;
            this.layoutControlItem9.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            // 
            // lblUser
            // 
            this.lblUser.Control = this.labelControl2;
            resources.ApplyResources(this.lblUser, "lblUser");
            this.lblUser.Location = new System.Drawing.Point(0, 0);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(90, 20);
            this.lblUser.TextSize = new System.Drawing.Size(0, 0);
            this.lblUser.TextToControlDistance = 0;
            this.lblUser.TextVisible = false;
            this.lblUser.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem1, "emptySpaceItem1");
            this.emptySpaceItem1.Location = new System.Drawing.Point(221, 408);
            this.emptySpaceItem1.MaxSize = new System.Drawing.Size(95, 26);
            this.emptySpaceItem1.MinSize = new System.Drawing.Size(95, 26);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(95, 26);
            this.emptySpaceItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // frmPaySlipViewer
            // 
            this.AcceptButton = this.btnShow;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.Controls.Add(this.layoutControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmPaySlipViewer";
            this.Load += new System.EventHandler(this.frmPaySlipViewer_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chkAll.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkPrintPay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chklstSelectStaff)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.glkpGroupValue.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit2View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPath.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.glkpPayPrinters.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblPrinter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblRegSlipTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblSalaryGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblUser)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraEditors.SimpleButton btnTemp;
        private DevExpress.XtraEditors.TextEdit txtPath;
        private DevExpress.XtraLayout.LayoutControlItem lblPrinter;
        private DevExpress.XtraLayout.LayoutControlItem lblRegSlipTemplate;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraEditors.GridLookUpEdit glkpGroupValue;
        private DevExpress.XtraGrid.Views.Grid.GridView gridLookUpEdit2View;
        private DevExpress.XtraLayout.LayoutControlItem lblSalaryGroup;
        private DevExpress.XtraEditors.SimpleButton btnShow;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraEditors.CheckEdit chkPrintPay;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
        private DevExpress.XtraEditors.CheckEdit chkAll;
        private DevExpress.XtraEditors.CheckedListBoxControl chklstSelectStaff;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraEditors.LabelControl chklstStaff;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraEditors.LabelControl lblUserName;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem9;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraLayout.LayoutControlItem lblUser;
        private DevExpress.XtraEditors.ComboBoxEdit glkpPayPrinters;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
    }
}