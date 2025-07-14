namespace ACPP.Modules.Master
{
    partial class frmInKindArticleAdd
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmInKindArticleAdd));
            this.lcInKindArticle = new DevExpress.XtraLayout.LayoutControl();
            this.txtmeNotes = new DevExpress.XtraEditors.MemoEdit();
            this.txtOpQuantity = new DevExpress.XtraEditors.TextEdit();
            this.txtMemoArticle = new DevExpress.XtraEditors.MemoEdit();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.txtOpValue = new DevExpress.XtraEditors.TextEdit();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.txtCode = new DevExpress.XtraEditors.TextEdit();
            this.lcgInKindArticle = new DevExpress.XtraLayout.LayoutControlGroup();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblCode = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblMemoArticle = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem3 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.lblNotes = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblOpValue = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.lcInKindArticle)).BeginInit();
            this.lcInKindArticle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtmeNotes.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOpQuantity.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMemoArticle.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOpValue.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgInKindArticle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblCode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblMemoArticle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblNotes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblOpValue)).BeginInit();
            this.SuspendLayout();
            // 
            // lcInKindArticle
            // 
            this.lcInKindArticle.AllowCustomizationMenu = false;
            this.lcInKindArticle.Controls.Add(this.txtmeNotes);
            this.lcInKindArticle.Controls.Add(this.txtOpQuantity);
            this.lcInKindArticle.Controls.Add(this.txtMemoArticle);
            this.lcInKindArticle.Controls.Add(this.btnSave);
            this.lcInKindArticle.Controls.Add(this.txtOpValue);
            this.lcInKindArticle.Controls.Add(this.btnClose);
            this.lcInKindArticle.Controls.Add(this.txtCode);
            resources.ApplyResources(this.lcInKindArticle, "lcInKindArticle");
            this.lcInKindArticle.Name = "lcInKindArticle";
            this.lcInKindArticle.Root = this.lcgInKindArticle;
            // 
            // txtmeNotes
            // 
            resources.ApplyResources(this.txtmeNotes, "txtmeNotes");
            this.txtmeNotes.Name = "txtmeNotes";
            this.txtmeNotes.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtmeNotes.Properties.MaxLength = 500;
            this.txtmeNotes.StyleController = this.lcInKindArticle;
            this.txtmeNotes.UseOptimizedRendering = true;
            // 
            // txtOpQuantity
            // 
            resources.ApplyResources(this.txtOpQuantity, "txtOpQuantity");
            this.txtOpQuantity.Name = "txtOpQuantity";
            this.txtOpQuantity.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtOpQuantity.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtOpQuantity.Properties.Mask.EditMask = resources.GetString("txtOpQuantity.Properties.Mask.EditMask");
            this.txtOpQuantity.Properties.Mask.MaskType = ((DevExpress.XtraEditors.Mask.MaskType)(resources.GetObject("txtOpQuantity.Properties.Mask.MaskType")));
            this.txtOpQuantity.Properties.MaxLength = 5;
            this.txtOpQuantity.StyleController = this.lcInKindArticle;
            // 
            // txtMemoArticle
            // 
            resources.ApplyResources(this.txtMemoArticle, "txtMemoArticle");
            this.txtMemoArticle.Name = "txtMemoArticle";
            this.txtMemoArticle.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtMemoArticle.Properties.MaxLength = 100;
            this.txtMemoArticle.Properties.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtMemoArticle.StyleController = this.lcInKindArticle;
            this.txtMemoArticle.UseOptimizedRendering = true;
            this.txtMemoArticle.Leave += new System.EventHandler(this.txtMemoArticle_Leave);
            // 
            // btnSave
            // 
            resources.ApplyResources(this.btnSave, "btnSave");
            this.btnSave.Name = "btnSave";
            this.btnSave.StyleController = this.lcInKindArticle;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtOpValue
            // 
            resources.ApplyResources(this.txtOpValue, "txtOpValue");
            this.txtOpValue.Name = "txtOpValue";
            this.txtOpValue.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtOpValue.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtOpValue.Properties.Mask.EditMask = resources.GetString("txtOpValue.Properties.Mask.EditMask");
            this.txtOpValue.Properties.Mask.MaskType = ((DevExpress.XtraEditors.Mask.MaskType)(resources.GetObject("txtOpValue.Properties.Mask.MaskType")));
            this.txtOpValue.Properties.MaxLength = 10;
            this.txtOpValue.StyleController = this.lcInKindArticle;
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.btnClose, "btnClose");
            this.btnClose.Name = "btnClose";
            this.btnClose.StyleController = this.lcInKindArticle;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // txtCode
            // 
            resources.ApplyResources(this.txtCode, "txtCode");
            this.txtCode.Name = "txtCode";
            this.txtCode.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtCode.Properties.MaxLength = 10;
            this.txtCode.StyleController = this.lcInKindArticle;
            this.txtCode.Leave += new System.EventHandler(this.txtCode_Leave);
            // 
            // lcgInKindArticle
            // 
            resources.ApplyResources(this.lcgInKindArticle, "lcgInKindArticle");
            this.lcgInKindArticle.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.lcgInKindArticle.GroupBordersVisible = false;
            this.lcgInKindArticle.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.emptySpaceItem1,
            this.layoutControlItem5,
            this.layoutControlItem6,
            this.lblCode,
            this.lblMemoArticle,
            this.layoutControlItem3,
            this.emptySpaceItem2,
            this.emptySpaceItem3,
            this.lblNotes,
            this.lblOpValue});
            this.lcgInKindArticle.Location = new System.Drawing.Point(0, 0);
            this.lcgInKindArticle.Name = "lcgInKindArticle";
            this.lcgInKindArticle.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.lcgInKindArticle.Size = new System.Drawing.Size(366, 182);
            this.lcgInKindArticle.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem1, "emptySpaceItem1");
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 156);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(230, 26);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.btnClose;
            resources.ApplyResources(this.layoutControlItem5, "layoutControlItem5");
            this.layoutControlItem5.Location = new System.Drawing.Point(299, 156);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 0, 2, 2);
            this.layoutControlItem5.Size = new System.Drawing.Size(67, 26);
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextToControlDistance = 0;
            this.layoutControlItem5.TextVisible = false;
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.btnSave;
            resources.ApplyResources(this.layoutControlItem6, "layoutControlItem6");
            this.layoutControlItem6.Location = new System.Drawing.Point(230, 156);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(69, 26);
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextToControlDistance = 0;
            this.layoutControlItem6.TextVisible = false;
            // 
            // lblCode
            // 
            this.lblCode.AllowHtmlStringInCaption = true;
            this.lblCode.Control = this.txtCode;
            resources.ApplyResources(this.lblCode, "lblCode");
            this.lblCode.Location = new System.Drawing.Point(0, 0);
            this.lblCode.Name = "lblCode";
            this.lblCode.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.lblCode.Size = new System.Drawing.Size(149, 23);
            this.lblCode.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 3);
            this.lblCode.TextSize = new System.Drawing.Size(63, 13);
            // 
            // lblMemoArticle
            // 
            this.lblMemoArticle.AllowHtmlStringInCaption = true;
            this.lblMemoArticle.Control = this.txtMemoArticle;
            resources.ApplyResources(this.lblMemoArticle, "lblMemoArticle");
            this.lblMemoArticle.Location = new System.Drawing.Point(0, 23);
            this.lblMemoArticle.Name = "lblMemoArticle";
            this.lblMemoArticle.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.lblMemoArticle.Size = new System.Drawing.Size(366, 55);
            this.lblMemoArticle.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 3);
            this.lblMemoArticle.TextSize = new System.Drawing.Size(63, 13);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.txtOpQuantity;
            resources.ApplyResources(this.layoutControlItem3, "layoutControlItem3");
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 78);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlItem3.Size = new System.Drawing.Size(149, 23);
            this.layoutControlItem3.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 3);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(63, 13);
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem2, "emptySpaceItem2");
            this.emptySpaceItem2.Location = new System.Drawing.Point(149, 0);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(217, 23);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem3
            // 
            this.emptySpaceItem3.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem3, "emptySpaceItem3");
            this.emptySpaceItem3.Location = new System.Drawing.Point(149, 78);
            this.emptySpaceItem3.Name = "emptySpaceItem3";
            this.emptySpaceItem3.Size = new System.Drawing.Size(61, 23);
            this.emptySpaceItem3.TextSize = new System.Drawing.Size(0, 0);
            // 
            // lblNotes
            // 
            this.lblNotes.Control = this.txtmeNotes;
            resources.ApplyResources(this.lblNotes, "lblNotes");
            this.lblNotes.Location = new System.Drawing.Point(0, 101);
            this.lblNotes.Name = "lblNotes";
            this.lblNotes.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 3);
            this.lblNotes.Size = new System.Drawing.Size(366, 55);
            this.lblNotes.TextSize = new System.Drawing.Size(63, 13);
            // 
            // lblOpValue
            // 
            this.lblOpValue.Control = this.txtOpValue;
            resources.ApplyResources(this.lblOpValue, "lblOpValue");
            this.lblOpValue.Location = new System.Drawing.Point(210, 78);
            this.lblOpValue.Name = "lblOpValue";
            this.lblOpValue.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.lblOpValue.Size = new System.Drawing.Size(156, 23);
            this.lblOpValue.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 3);
            this.lblOpValue.TextSize = new System.Drawing.Size(63, 13);
            // 
            // frmInKindArticleAdd
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.Controls.Add(this.lcInKindArticle);
            this.Name = "frmInKindArticleAdd";
            this.Load += new System.EventHandler(this.frmInKindArticleAdd_Load);
            ((System.ComponentModel.ISupportInitialize)(this.lcInKindArticle)).EndInit();
            this.lcInKindArticle.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtmeNotes.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOpQuantity.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMemoArticle.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOpValue.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgInKindArticle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblCode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblMemoArticle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblNotes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblOpValue)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl lcInKindArticle;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraLayout.LayoutControlGroup lcgInKindArticle;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraEditors.TextEdit txtOpValue;
        private DevExpress.XtraEditors.TextEdit txtOpQuantity;
        private DevExpress.XtraEditors.MemoEdit txtMemoArticle;
        private DevExpress.XtraEditors.TextEdit txtCode;
        private DevExpress.XtraLayout.LayoutControlItem lblCode;
        private DevExpress.XtraLayout.LayoutControlItem lblMemoArticle;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem lblOpValue;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem3;
        private DevExpress.XtraEditors.MemoEdit txtmeNotes;
        private DevExpress.XtraLayout.LayoutControlItem lblNotes;
    }
}