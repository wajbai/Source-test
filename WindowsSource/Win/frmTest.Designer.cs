namespace ACPP
{
    partial class frmTest
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
            this.label1 = new System.Windows.Forms.Label();
            this.dtTransDateBS = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.cboTransModeBS = new System.Windows.Forms.ComboBox();
            this.txtProjectId = new System.Windows.Forms.TextBox();
            this.txtAmountBS = new System.Windows.Forms.TextBox();
            this.btnUpdateBalanceBS = new System.Windows.Forms.Button();
            this.btnUpdateBalanceAS = new System.Windows.Forms.Button();
            this.txtAmountAS = new System.Windows.Forms.TextBox();
            this.cboTransModeAS = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtLedgerId = new System.Windows.Forms.TextBox();
            this.dtTransDateAS = new System.Windows.Forms.DateTimePicker();
            this.btnShowReport = new System.Windows.Forms.Button();
            this.buttonEdit1 = new DevExpress.XtraEditors.ButtonEdit();
            this.btnShowBalance = new System.Windows.Forms.Button();
            this.btnUpdateBulkBalance = new System.Windows.Forms.Button();
            this.btnReadXML = new DevExpress.XtraEditors.SimpleButton();
            this.dateEdit2 = new DevExpress.XtraEditors.DateEdit();
            this.dateEdit1 = new DevExpress.XtraEditors.DateEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtEncText = new DevExpress.XtraEditors.TextEdit();
            this.btnEncrypt = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txtEncAnsText = new DevExpress.XtraEditors.TextEdit();
            this.txtDecAnsText = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.btnDecrypt = new DevExpress.XtraEditors.SimpleButton();
            this.txtDecryptText = new DevExpress.XtraEditors.TextEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.btnEncClear = new DevExpress.XtraEditors.SimpleButton();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.btnDecClear = new DevExpress.XtraEditors.SimpleButton();
            this.ucToolBar1 = new Bosco.Utility.Controls.ucToolBar();
            ((System.ComponentModel.ISupportInitialize)(this.buttonEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit2.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit1.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEncText.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEncAnsText.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDecAnsText.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDecryptText.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Location = new System.Drawing.Point(27, 114);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 15);
            this.label1.TabIndex = 17;
            this.label1.Text = "Date";
            // 
            // dtTransDateBS
            // 
            this.dtTransDateBS.CustomFormat = "";
            this.dtTransDateBS.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtTransDateBS.Location = new System.Drawing.Point(96, 110);
            this.dtTransDateBS.Name = "dtTransDateBS";
            this.dtTransDateBS.Size = new System.Drawing.Size(100, 21);
            this.dtTransDateBS.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(36, 51);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 13);
            this.label3.TabIndex = 19;
            this.label3.Text = "Project Id";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(223, 48);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 13);
            this.label4.TabIndex = 19;
            this.label4.Text = "Ledger Id";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(27, 140);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(44, 13);
            this.label5.TabIndex = 20;
            this.label5.Text = "Amount";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(27, 167);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(63, 13);
            this.label7.TabIndex = 21;
            this.label7.Text = "Trans Mode";
            // 
            // cboTransModeBS
            // 
            this.cboTransModeBS.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTransModeBS.FormattingEnabled = true;
            this.cboTransModeBS.Items.AddRange(new object[] {
            "CR",
            "DR"});
            this.cboTransModeBS.Location = new System.Drawing.Point(96, 164);
            this.cboTransModeBS.Name = "cboTransModeBS";
            this.cboTransModeBS.Size = new System.Drawing.Size(98, 21);
            this.cboTransModeBS.TabIndex = 3;
            // 
            // txtProjectId
            // 
            this.txtProjectId.Location = new System.Drawing.Point(96, 48);
            this.txtProjectId.Name = "txtProjectId";
            this.txtProjectId.Size = new System.Drawing.Size(100, 21);
            this.txtProjectId.TabIndex = 0;
            this.txtProjectId.Text = "1";
            // 
            // txtAmountBS
            // 
            this.txtAmountBS.Location = new System.Drawing.Point(96, 137);
            this.txtAmountBS.Name = "txtAmountBS";
            this.txtAmountBS.Size = new System.Drawing.Size(100, 21);
            this.txtAmountBS.TabIndex = 2;
            // 
            // btnUpdateBalanceBS
            // 
            this.btnUpdateBalanceBS.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnUpdateBalanceBS.Location = new System.Drawing.Point(34, 205);
            this.btnUpdateBalanceBS.Name = "btnUpdateBalanceBS";
            this.btnUpdateBalanceBS.Size = new System.Drawing.Size(186, 23);
            this.btnUpdateBalanceBS.TabIndex = 8;
            this.btnUpdateBalanceBS.Text = "Update Balance Before Save";
            this.btnUpdateBalanceBS.UseVisualStyleBackColor = true;
            this.btnUpdateBalanceBS.Click += new System.EventHandler(this.btnUpdateBalanceBS_Click);
            // 
            // btnUpdateBalanceAS
            // 
            this.btnUpdateBalanceAS.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnUpdateBalanceAS.Location = new System.Drawing.Point(245, 205);
            this.btnUpdateBalanceAS.Name = "btnUpdateBalanceAS";
            this.btnUpdateBalanceAS.Size = new System.Drawing.Size(188, 23);
            this.btnUpdateBalanceAS.TabIndex = 9;
            this.btnUpdateBalanceAS.Text = "Update Balance After Save";
            this.btnUpdateBalanceAS.UseVisualStyleBackColor = true;
            this.btnUpdateBalanceAS.Click += new System.EventHandler(this.btnUpdateBalanceAS_Click);
            // 
            // txtAmountAS
            // 
            this.txtAmountAS.Location = new System.Drawing.Point(289, 132);
            this.txtAmountAS.Name = "txtAmountAS";
            this.txtAmountAS.Size = new System.Drawing.Size(100, 21);
            this.txtAmountAS.TabIndex = 6;
            // 
            // cboTransModeAS
            // 
            this.cboTransModeAS.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTransModeAS.FormattingEnabled = true;
            this.cboTransModeAS.Items.AddRange(new object[] {
            "CR",
            "DR"});
            this.cboTransModeAS.Location = new System.Drawing.Point(289, 164);
            this.cboTransModeAS.Name = "cboTransModeAS";
            this.cboTransModeAS.Size = new System.Drawing.Size(98, 21);
            this.cboTransModeAS.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(110, 85);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 13);
            this.label2.TabIndex = 25;
            this.label2.Text = "Before Save";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.label6.Location = new System.Drawing.Point(302, 85);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(67, 13);
            this.label6.TabIndex = 25;
            this.label6.Text = "After Save";
            // 
            // txtLedgerId
            // 
            this.txtLedgerId.Location = new System.Drawing.Point(287, 45);
            this.txtLedgerId.Name = "txtLedgerId";
            this.txtLedgerId.Size = new System.Drawing.Size(100, 21);
            this.txtLedgerId.TabIndex = 4;
            this.txtLedgerId.Text = "5";
            // 
            // dtTransDateAS
            // 
            this.dtTransDateAS.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtTransDateAS.Location = new System.Drawing.Point(287, 105);
            this.dtTransDateAS.Name = "dtTransDateAS";
            this.dtTransDateAS.Size = new System.Drawing.Size(100, 21);
            this.dtTransDateAS.TabIndex = 5;
            // 
            // btnShowReport
            // 
            this.btnShowReport.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnShowReport.Location = new System.Drawing.Point(113, 261);
            this.btnShowReport.Name = "btnShowReport";
            this.btnShowReport.Size = new System.Drawing.Size(276, 23);
            this.btnShowReport.TabIndex = 26;
            this.btnShowReport.Text = "Show Report";
            this.btnShowReport.UseVisualStyleBackColor = true;
            this.btnShowReport.Click += new System.EventHandler(this.btnShowReport_Click);
            // 
            // buttonEdit1
            // 
            this.buttonEdit1.Location = new System.Drawing.Point(522, 138);
            this.buttonEdit1.Name = "buttonEdit1";
            this.buttonEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.buttonEdit1.Size = new System.Drawing.Size(100, 20);
            this.buttonEdit1.TabIndex = 27;
            // 
            // btnShowBalance
            // 
            this.btnShowBalance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnShowBalance.Location = new System.Drawing.Point(113, 321);
            this.btnShowBalance.Name = "btnShowBalance";
            this.btnShowBalance.Size = new System.Drawing.Size(276, 23);
            this.btnShowBalance.TabIndex = 26;
            this.btnShowBalance.Text = "Show Balance";
            this.btnShowBalance.UseVisualStyleBackColor = true;
            this.btnShowBalance.Click += new System.EventHandler(this.btnShowBalance_Click);
            // 
            // btnUpdateBulkBalance
            // 
            this.btnUpdateBulkBalance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnUpdateBulkBalance.Location = new System.Drawing.Point(111, 378);
            this.btnUpdateBulkBalance.Name = "btnUpdateBulkBalance";
            this.btnUpdateBulkBalance.Size = new System.Drawing.Size(276, 23);
            this.btnUpdateBulkBalance.TabIndex = 28;
            this.btnUpdateBulkBalance.Text = "Update Bulk Balance";
            this.btnUpdateBulkBalance.UseVisualStyleBackColor = true;
            this.btnUpdateBulkBalance.Click += new System.EventHandler(this.btnUpdateBulkBalance_Click);
            // 
            // btnReadXML
            // 
            this.btnReadXML.Location = new System.Drawing.Point(529, 284);
            this.btnReadXML.Name = "btnReadXML";
            this.btnReadXML.Size = new System.Drawing.Size(75, 23);
            this.btnReadXML.TabIndex = 29;
            this.btnReadXML.Text = "Read XML";
            this.btnReadXML.Click += new System.EventHandler(this.btnReadXML_Click);
            // 
            // dateEdit2
            // 
            this.dateEdit2.EditValue = null;
            this.dateEdit2.Location = new System.Drawing.Point(722, 258);
            this.dateEdit2.Name = "dateEdit2";
            this.dateEdit2.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEdit2.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEdit2.Properties.Mask.EditMask = "G";
            this.dateEdit2.Size = new System.Drawing.Size(174, 20);
            this.dateEdit2.TabIndex = 31;
            // 
            // dateEdit1
            // 
            this.dateEdit1.EditValue = null;
            this.dateEdit1.Location = new System.Drawing.Point(522, 258);
            this.dateEdit1.Name = "dateEdit1";
            this.dateEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEdit1.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEdit1.Properties.Mask.EditMask = "G";
            this.dateEdit1.Size = new System.Drawing.Size(159, 20);
            this.dateEdit1.TabIndex = 30;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(5, 37);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(79, 13);
            this.labelControl1.TabIndex = 32;
            this.labelControl1.Text = "Text to Encrypt:";
            // 
            // txtEncText
            // 
            this.txtEncText.Location = new System.Drawing.Point(90, 34);
            this.txtEncText.Name = "txtEncText";
            this.txtEncText.Size = new System.Drawing.Size(239, 20);
            this.txtEncText.TabIndex = 33;
            // 
            // btnEncrypt
            // 
            this.btnEncrypt.Location = new System.Drawing.Point(340, 32);
            this.btnEncrypt.Name = "btnEncrypt";
            this.btnEncrypt.Size = new System.Drawing.Size(75, 23);
            this.btnEncrypt.TabIndex = 34;
            this.btnEncrypt.Text = "Encrypt";
            this.btnEncrypt.Click += new System.EventHandler(this.btnEncrypt_Click);
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(8, 62);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(74, 13);
            this.labelControl3.TabIndex = 36;
            this.labelControl3.Text = "Encrypted Text";
            // 
            // txtEncAnsText
            // 
            this.txtEncAnsText.Location = new System.Drawing.Point(90, 63);
            this.txtEncAnsText.Name = "txtEncAnsText";
            this.txtEncAnsText.Properties.ReadOnly = true;
            this.txtEncAnsText.Size = new System.Drawing.Size(239, 20);
            this.txtEncAnsText.TabIndex = 37;
            // 
            // txtDecAnsText
            // 
            this.txtDecAnsText.Location = new System.Drawing.Point(106, 66);
            this.txtDecAnsText.Name = "txtDecAnsText";
            this.txtDecAnsText.Properties.ReadOnly = true;
            this.txtDecAnsText.Size = new System.Drawing.Size(239, 20);
            this.txtDecAnsText.TabIndex = 42;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(8, 68);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(75, 13);
            this.labelControl2.TabIndex = 41;
            this.labelControl2.Text = "Decrypted Text";
            // 
            // btnDecrypt
            // 
            this.btnDecrypt.Location = new System.Drawing.Point(356, 35);
            this.btnDecrypt.Name = "btnDecrypt";
            this.btnDecrypt.Size = new System.Drawing.Size(75, 23);
            this.btnDecrypt.TabIndex = 40;
            this.btnDecrypt.Text = "Decrypt";
            this.btnDecrypt.Click += new System.EventHandler(this.btnDecrypt_Click);
            // 
            // txtDecryptText
            // 
            this.txtDecryptText.Location = new System.Drawing.Point(106, 37);
            this.txtDecryptText.Name = "txtDecryptText";
            this.txtDecryptText.Size = new System.Drawing.Size(239, 20);
            this.txtDecryptText.TabIndex = 39;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(7, 40);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(80, 13);
            this.labelControl4.TabIndex = 38;
            this.labelControl4.Text = "Text to Decrypt:";
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.btnEncClear);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Controls.Add(this.labelControl3);
            this.groupControl1.Controls.Add(this.txtEncText);
            this.groupControl1.Controls.Add(this.btnEncrypt);
            this.groupControl1.Controls.Add(this.txtEncAnsText);
            this.groupControl1.Location = new System.Drawing.Point(529, 321);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(435, 102);
            this.groupControl1.TabIndex = 43;
            this.groupControl1.Text = "Encrypt";
            // 
            // btnEncClear
            // 
            this.btnEncClear.Location = new System.Drawing.Point(340, 61);
            this.btnEncClear.Name = "btnEncClear";
            this.btnEncClear.Size = new System.Drawing.Size(75, 23);
            this.btnEncClear.TabIndex = 38;
            this.btnEncClear.Text = "Clear";
            this.btnEncClear.Click += new System.EventHandler(this.btnEncClear_Click);
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.btnDecClear);
            this.groupControl2.Controls.Add(this.labelControl4);
            this.groupControl2.Controls.Add(this.labelControl2);
            this.groupControl2.Controls.Add(this.txtDecAnsText);
            this.groupControl2.Controls.Add(this.btnDecrypt);
            this.groupControl2.Controls.Add(this.txtDecryptText);
            this.groupControl2.Location = new System.Drawing.Point(529, 435);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(435, 102);
            this.groupControl2.TabIndex = 44;
            this.groupControl2.Text = "Decrypt";
            // 
            // btnDecClear
            // 
            this.btnDecClear.Location = new System.Drawing.Point(355, 63);
            this.btnDecClear.Name = "btnDecClear";
            this.btnDecClear.Size = new System.Drawing.Size(75, 23);
            this.btnDecClear.TabIndex = 39;
            this.btnDecClear.Text = "Clear";
            this.btnDecClear.Click += new System.EventHandler(this.btnDecClear_Click);
            // 
            // ucToolBar1
            // 
            this.ucToolBar1.ChangeAddCaption = "&Add";
            this.ucToolBar1.ChangeCaption = "&Edit";
            this.ucToolBar1.ChangeDeleteCaption = "&Delete";
            this.ucToolBar1.ChangeNatureOfPaymentCaption = "&Nature of Payments";
            this.ucToolBar1.ChangePostInterestCaption = "P&ost Interest";
            this.ucToolBar1.ChangePrintCaption = "&Print";
            this.ucToolBar1.ChnageRenewCaption = "Re<u>n</u>ew";
            this.ucToolBar1.DisableAddButton = true;
            this.ucToolBar1.DisableAMCRenew = true;
            this.ucToolBar1.DisableCloseButton = true;
            this.ucToolBar1.DisableDeleteButton = true;
            this.ucToolBar1.DisableDownloadExcel = true;
            this.ucToolBar1.DisableEditButton = true;
            this.ucToolBar1.DisableMoveTransaction = true;
            this.ucToolBar1.DisableNatureofPayments = true;
            this.ucToolBar1.DisablePostInterest = true;
            this.ucToolBar1.DisablePrintButton = true;
            this.ucToolBar1.DisableRestoreVoucher = true;
            this.ucToolBar1.Location = new System.Drawing.Point(2, 12);
            this.ucToolBar1.Name = "ucToolBar1";
            this.ucToolBar1.Padding = new System.Windows.Forms.Padding(0, 2, 0, 0);
            this.ucToolBar1.ShowHTML = true;
            this.ucToolBar1.ShowMMT = true;
            this.ucToolBar1.ShowPDF = true;
            this.ucToolBar1.ShowRTF = true;
            this.ucToolBar1.ShowText = true;
            this.ucToolBar1.ShowXLS = true;
            this.ucToolBar1.ShowXLSX = true;
            this.ucToolBar1.Size = new System.Drawing.Size(1455, 29);
            this.ucToolBar1.TabIndex = 45;
            this.ucToolBar1.VisbleInsertVoucher = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBar1.VisibleAddButton = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBar1.VisibleClose = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucToolBar1.VisibleDeleteButton = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBar1.VisibleDownloadExcel = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBar1.VisibleEditButton = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBar1.VisibleMoveTrans = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBar1.VisibleNatureofPayments = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBar1.VisibleNegativeBalance = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBar1.VisiblePostInterest = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBar1.VisiblePrintButton = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBar1.VisibleRefresh = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucToolBar1.VisibleRenew = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBar1.VisibleRestoreVoucher = DevExpress.XtraBars.BarItemVisibility.Never;
            // 
            // frmTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1036, 549);
            this.Controls.Add(this.ucToolBar1);
            this.Controls.Add(this.groupControl2);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.dateEdit2);
            this.Controls.Add(this.dateEdit1);
            this.Controls.Add(this.btnReadXML);
            this.Controls.Add(this.btnUpdateBulkBalance);
            this.Controls.Add(this.buttonEdit1);
            this.Controls.Add(this.btnShowBalance);
            this.Controls.Add(this.btnShowReport);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnUpdateBalanceAS);
            this.Controls.Add(this.btnUpdateBalanceBS);
            this.Controls.Add(this.txtAmountAS);
            this.Controls.Add(this.txtAmountBS);
            this.Controls.Add(this.txtLedgerId);
            this.Controls.Add(this.cboTransModeAS);
            this.Controls.Add(this.txtProjectId);
            this.Controls.Add(this.cboTransModeBS);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dtTransDateAS);
            this.Controls.Add(this.dtTransDateBS);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "frmTest";
            this.Text = "Test";
            this.Load += new System.EventHandler(this.frmTest_Load);
            ((System.ComponentModel.ISupportInitialize)(this.buttonEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit2.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit1.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEncText.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEncAnsText.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDecAnsText.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDecryptText.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            this.groupControl2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtTransDateBS;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cboTransModeBS;
        private System.Windows.Forms.TextBox txtProjectId;
        private System.Windows.Forms.TextBox txtAmountBS;
        private System.Windows.Forms.Button btnUpdateBalanceBS;
        private System.Windows.Forms.Button btnUpdateBalanceAS;
        private System.Windows.Forms.TextBox txtAmountAS;
        private System.Windows.Forms.ComboBox cboTransModeAS;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtLedgerId;
        private System.Windows.Forms.DateTimePicker dtTransDateAS;
        private System.Windows.Forms.Button btnShowReport;
        private DevExpress.XtraEditors.ButtonEdit buttonEdit1;
        private System.Windows.Forms.Button btnShowBalance;
        private System.Windows.Forms.Button btnUpdateBulkBalance;
        private DevExpress.XtraEditors.SimpleButton btnReadXML;
        private DevExpress.XtraEditors.DateEdit dateEdit2;
        private DevExpress.XtraEditors.DateEdit dateEdit1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit txtEncText;
        private DevExpress.XtraEditors.SimpleButton btnEncrypt;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit txtEncAnsText;
        private DevExpress.XtraEditors.TextEdit txtDecAnsText;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.SimpleButton btnDecrypt;
        private DevExpress.XtraEditors.TextEdit txtDecryptText;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraEditors.SimpleButton btnEncClear;
        private DevExpress.XtraEditors.SimpleButton btnDecClear;
        private Bosco.Utility.Controls.ucToolBar ucToolBar1;



    }
}