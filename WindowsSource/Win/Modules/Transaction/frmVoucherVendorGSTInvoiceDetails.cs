/*
 * This form is used get Vendor Invoice details like 
 * 1. Name of the vendor (its gst,pan and contact info), 
 * 2. Invoice number
 * 3. Invoice Date
 * 
 * this form will be poped up onlu GST enabled vouchers alone
 * */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Bosco.Model;
using Bosco.Utility;
using ACPP.Modules.Inventory;
using Bosco.Model.Transaction;
using Bosco.Model.UIModel;
using Bosco.Model.UIModel.Master;
using ACPP.Modules.Master;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraGrid;

namespace ACPP.Modules.Transaction
{
    public partial class frmVoucherVendorGSTInvoiceDetails : frmFinanceBaseAdd
    {

        #region Properites

        public Int32 GSTInvoiceId { get; set; }
        public Int32 ProjectId { get; set; }
        public Int32 VoucherId { get; set; }
        public DateTime TransDate { get; set; }
        AutoCompleteStringCollection collNarration = new AutoCompleteStringCollection();
        private DefaultVoucherTypes VType = DefaultVoucherTypes.Receipt;
        public Int32 VendorId
        {
            get
            {
                return glkVendor.EditValue != null ? this.UtilityMember.NumberSet.ToInteger(glkVendor.EditValue.ToString()) : 0;
            }
            set
            {
                glkVendor.EditValue = value;
            }
        }

        public string InvoiceNo { get; set; }
        public string InvoiceDate { get; set; }
        public Int32 InvoiceType { get; set; }

        private bool IsGeneralInvolice
        {
            get
            {
                return (this.AppSetting.AllowMultiCurrency == 1 || this.AppSetting.IsCountryOtherThanIndia);
            }
        }

        private double Amount { get; set; }
        private double TotalAmountWithoutTax { get; set; }
        private double TotalAmountWithTax { get; set; }
        private double CGST { get; set; }
        private double SGST { get; set; }
        private double IGST { get; set; }
        private string Purpose { get; set; }
        private double VoucherInvoiceAmount { get; set; }//For multi Currency
        private string VoucherInvoiceLedgerName { get; set; }//For multi Currency
        private double InvoiceBalanceAmount { get; set; }//For multi Currency

        private string VendorName { get; set; }
        private string VendorGSTNo { get; set; }
        private string VendorAddress { get; set; }
        private string VendorAddressCombined { get; set; }
        private Int32 VendorStateId { get; set; }
        private Int32 VendorCountryId { get; set; }
        private string VendorState { get; set; }
        private string VendorCountry { get; set; }

        //private string LedgerName
        //{
        //    get
        //    {
        //        string ledgernmae;
        //        ledgernmae = gvLedgerDetails.GetFocusedRowCellValue(colLedger) != null ? gvLedgerDetails.GetFocusedRowCellValue(colLedger).ToString() : string.Empty;
        //        return ledgernmae;
        //    }
        //}

        private double ItemAmount
        {
            get
            {
                double itemamt;
                itemamt = gvLedgerDetails.GetFocusedRowCellValue(colAmount) != null ? UtilityMember.NumberSet.ToDouble(gvLedgerDetails.GetFocusedRowCellValue(colAmount).ToString()) : 0;
                return itemamt;
            }
        }


        private string ItemName
        {
            get
            {
                string itemname;
                itemname = gvLedgerDetails.GetFocusedRowCellValue(colItemName) != null ? gvLedgerDetails.GetFocusedRowCellValue(colItemName).ToString() : string.Empty;
                return itemname;
            }
        }

        private double Quantity
        {
            get
            {
                double quantity;
                quantity = gvLedgerDetails.GetFocusedRowCellValue(colQuantity) != null ? UtilityMember.NumberSet.ToDouble(gvLedgerDetails.GetFocusedRowCellValue(colQuantity).ToString()) : 0;
                return quantity;
            }
        }

        private double UnitPrice
        {
            get
            {
                double unitprice;
                unitprice = gvLedgerDetails.GetFocusedRowCellValue(colUnitAmount) != null ? UtilityMember.NumberSet.ToDouble(gvLedgerDetails.GetFocusedRowCellValue(colUnitAmount).ToString()) : 0;
                return unitprice;
            }
        }

        private bool IsValidSource()
        {
            bool isValid = true;

            DataTable dtTrans = gcLedgerDetails.DataSource as DataTable;
            DataView dv = new DataView(dtTrans);
            dv.RowFilter = colItemName.FieldName + "='' OR " + colQuantity.FieldName + "=0 OR " + colUnitAmount.FieldName + "=0";

            if (dv.Count > 0)
            {
                isValid = false;   
            }

            if (!isValid) { FocusTransactionGrid(); }

            return isValid;
        }

        private void FocusTransactionGrid()
        {
            gcLedgerDetails.Focus();
            gvLedgerDetails.MoveFirst(); //DevExpress.XtraGrid.GridControl.NewItemRowHandle;
            gvLedgerDetails.FocusedColumn = gvLedgerDetails.Columns.ColumnByName(colItemName.Name);
            gvLedgerDetails.ShowEditor();
        }

        private bool TransacationGridNewItem
        {
            set
            {
                if (value)
                {
                    DataTable dtTransaction = gcLedgerDetails.DataSource as DataTable;
                    dtTransaction.Rows.Add(dtTransaction.NewRow());
                    gcLedgerDetails.DataSource = dtTransaction;
                                        
                    gvLedgerDetails.MoveLast();
                    gvLedgerDetails.SetRowCellValue(gvLedgerDetails.FocusedRowHandle, colLedger, VoucherInvoiceLedgerName);
                    gvLedgerDetails.SetRowCellValue(gvLedgerDetails.FocusedRowHandle, colAmount, 0);
                    gvLedgerDetails.FocusedColumn = colItemName;
                    gvLedgerDetails.ShowEditor();
                }
            }
        }

        public DataTable DtGSTInvoiceMasterDetails = null;
        public DataTable DtGSTInvoiceMasterLedgerDetails = null;
        private DataTable DtGSTInvoiceMasterLedgerDetailsPrevious = null;
        #endregion

        public frmVoucherVendorGSTInvoiceDetails(DefaultVoucherTypes vtype, Int32 projectid, Int32 gstInvoiceId, DataTable dtgstinvoicedetails, DataTable dtgstinvoiceledgerdetails, Int32 voucherId, DateTime transDate,
            Int32 vendorId, string invoiceNo, Int32 invoiceType, string invoiceDate, double amount, double cGST, double sGST, double iGST, string purpose,
            double voucherInvoiceAmount = 0, string voucherInvoiceledgername= "")
        {
            InitializeComponent();
            this.PageTitle = "Vendor Information";
            VType = vtype;
            VoucherId = voucherId;
            ProjectId = projectid;
            GSTInvoiceId = gstInvoiceId;
            DtGSTInvoiceMasterDetails = dtgstinvoicedetails;
            DtGSTInvoiceMasterLedgerDetails = dtgstinvoiceledgerdetails;
            DtGSTInvoiceMasterLedgerDetailsPrevious = dtgstinvoiceledgerdetails.DefaultView.ToTable();
            TransDate = transDate;
            VendorId = vendorId;
            InvoiceNo = invoiceNo;
            InvoiceDate = invoiceDate;
            InvoiceType = invoiceType;
            TotalAmountWithoutTax = amount;
            TotalAmountWithTax = amount + (cGST + sGST + iGST);
            amount = amount - (cGST + sGST + iGST);
            Amount = amount;
            CGST = cGST;
            SGST = sGST;
            IGST = iGST;
            Purpose = purpose;
            VoucherInvoiceAmount = voucherInvoiceAmount;
            VoucherInvoiceLedgerName = voucherInvoiceledgername;
            LoadVendor();
            LoadCountryDetails(glkpBillCountry);
            LoadCountryDetails(glkpShipCountry);
            LoadLedgerHSNSACCode();

            gcLedgerDetails.DataSource = dtgstinvoiceledgerdetails;
            gvLedgerDetails.ShowEditor();

            RealColumnQuantity();

            RealColumnUnitPrice();
            
            //Fill details
            lblVendorPAN.Text = "PAN No  : ";
            lblVendorAaddress.Text = "Address : ";
            txtInvoiceNo.Text = invoiceNo;
            dtInvoceDate.EditValue = this.UtilityMember.DateSet.ToDate(invoiceDate, false);

            //dtInvoceDate.Properties.MaxValue = transDate;
            if (string.IsNullOrEmpty(invoiceDate))
            {
                dtInvoceDate.EditValue = null; ;
                dtInvoceDate.Properties.NullDate = string.Empty;
            }
            glkVendor.EditValue = 0;
            glkVendor.EditValue = vendorId;
            cbInvoiceType.SelectedIndex = InvoiceType;
            //lblAmountValue.Text = this.UtilityMember.NumberSet.ToCurrency(amount);
            //lblCGSTValue.Text = this.UtilityMember.NumberSet.ToCurrency(cGST);
            //lblSGSTValue.Text = this.UtilityMember.NumberSet.ToCurrency(sGST);
            //lblIGSTValue.Text = this.UtilityMember.NumberSet.ToCurrency(iGST);
            //lblTotalValue.Text = this.UtilityMember.NumberSet.ToCurrency(amount + cGST + sGST + iGST);
            lblRemarks.Text = "Remarks : " + purpose;

            GetGSTInvoiceMasterDetails();
            lcReverseChargeAmt.Visibility = chkReverseCharge.Checked ? DevExpress.XtraLayout.Utils.LayoutVisibility.Always : DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

            if (vtype == DefaultVoucherTypes.Journal && !IsGeneralInvolice)
            {
                lcChequeFavour.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            }

            //On for journal invoice booking, let us lock invoice date as journal voucher date
            if (vtype == DefaultVoucherTypes.Journal)
            {
                dtInvoceDate.DateTime = transDate;
                dtInvoceDate.Enabled = false;
            }
        }

        private void btnOkay_Click(object sender, EventArgs e)
        {
            if (ValidateInputDetails())
            {
                this.InvoiceNo = txtInvoiceNo.Text.Trim();
                this.InvoiceDate = this.UtilityMember.DateSet.ToDate(dtInvoceDate.DateTime.ToShortDateString());
                this.VendorId = glkVendor.EditValue != null ? this.UtilityMember.NumberSet.ToInteger(glkVendor.EditValue.ToString()) : 0;
                this.InvoiceType = cbInvoiceType.SelectedIndex;
                SetGSTInvoiceMasterDetails();
                DtGSTInvoiceMasterLedgerDetails = null;
                if (gcLedgerDetails.DataSource != null)
                {
                    DtGSTInvoiceMasterLedgerDetails = gcLedgerDetails.DataSource as DataTable;
                }

                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
        }

        private bool ValidateInputDetails()
        {
            bool Rtn = true;
            if (string.IsNullOrEmpty(txtInvoiceNo.Text))
            {
                Rtn = false;
                MessageRender.ShowMessage("Invoice No is empty");
                txtInvoiceNo.Focus();
            }
            else if (dtInvoceDate.DateTime == DateTime.MinValue)
            {
                Rtn = false;
                MessageRender.ShowMessage("Invoice Date is empty");
                dtInvoceDate.Focus();
            }
            else if (VendorId == 0)
            {
                Rtn = false;
                MessageRender.ShowMessage("Vendor Name is empty");
                glkVendor.Select();
                glkVendor.Focus();
            }
            else if (cbInvoiceType.SelectedIndex < 0)
            {
                Rtn = false;
                MessageRender.ShowMessage("Invoice Type is empty");
                cbInvoiceType.Select();
                cbInvoiceType.Focus();
            }
            else if (dtInvoceDate.DateTime > TransDate)
            {
                Rtn = false;
                MessageRender.ShowMessage("Invoice Date must be less than or equal to Voucher Date");
                dtInvoceDate.Focus();
            }
            else if (IsGeneralInvolice && dtDueDate.DateTime!=DateTime.MinValue  && dtDueDate.DateTime < TransDate)
            {
                Rtn = false;
                MessageRender.ShowMessage("Invoice Due Date must be greater than or equal to Voucher Date");
                dtDueDate.Focus();
            }
            else if (!isExistInvoiceNo())
            {
                Rtn = false;
                MessageRender.ShowMessage("'" + txtInvoiceNo.Text + "' Invoice No is already exists for the selected Vendor '" + glkVendor.Text + "'");
                txtInvoiceNo.Focus();
            }
            else if (!checkLedgerTaxableAmount())
            {
                Rtn = false;
            }
            return Rtn;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DtGSTInvoiceMasterLedgerDetails = DtGSTInvoiceMasterLedgerDetailsPrevious;
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        // Load the Vendor details to show the immediate added value in to the Vendor Combo
        public void ShowVerdorForm()
        {
            frmVendorInfoAdd frmVendorInfoAdd = new frmVendorInfoAdd(0, VendorManufacture.Vendor);
            frmVendorInfoAdd.ShowDialog();
            if (frmVendorInfoAdd.ReturnDialog == System.Windows.Forms.DialogResult.OK)
            {
                LoadVendor();
                if (frmVendorInfoAdd.ReturnValue != null && this.UtilityMember.NumberSet.ToInteger(frmVendorInfoAdd.ReturnValue.ToString()) > 0)
                {
                    glkVendor.EditValue = this.UtilityMember.NumberSet.ToInteger(frmVendorInfoAdd.ReturnValue.ToString());
                }
            }
        }

        private void DeleteTransaction()
        {
            try
            {
                if (IsGeneralInvolice)
                {
                    if (gvLedgerDetails.RowCount>1)
                    {
                        if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETE_CONFIRMATION), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            gvLedgerDetails.DeleteRow(gvLedgerDetails.FocusedRowHandle);
                            this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETED_CONFIRMATION));
                            FocusTransactionGrid();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }

        private void LoadVendor()
        {
            try
            {
                using (VendorInfoSystem vendorSystem = new VendorInfoSystem())
                {
                    ResultArgs resultArgs = vendorSystem.FetchDetailsWithGSTNo();
                    glkVendor.Properties.DataSource = null;
                    if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        this.UtilityMember.ComboSet.BindGridLookUpCombo(glkVendor, resultArgs.DataSource.Table, vendorSystem.AppSchema.Vendors.VENDORColumn.ColumnName, vendorSystem.AppSchema.Vendors.IDColumn.ColumnName);
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageRender.ShowMessage(Ex.Message);
            }
            finally { }
        }

        private void LoadLedgerHSNSACCode()
        {
            try
            {
                using (LedgerSystem ledgerSystem = new LedgerSystem())
                {
                    ledgerSystem.ProjectId = ProjectId;
                    ResultArgs resultArgs = ledgerSystem.FetchAllHSNSACCode();
                    if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        DataTable dtLedgersHSNSACCode = resultArgs.DataSource.Table;
                        dtLedgersHSNSACCode = dtLedgersHSNSACCode.DefaultView.ToTable(true, new string[] { ledgerSystem.AppSchema.Ledger.GST_HSN_SAC_CODEColumn.ColumnName });
                        dtLedgersHSNSACCode.DefaultView.Sort = ledgerSystem.AppSchema.Ledger.GST_HSN_SAC_CODEColumn.ColumnName;
                        dtLedgersHSNSACCode = dtLedgersHSNSACCode.DefaultView.ToTable();
                        collNarration.Clear();
                        foreach (DataRow dr in dtLedgersHSNSACCode.Rows)
                        {
                            collNarration.Add(dr[ledgerSystem.AppSchema.Ledger.GST_HSN_SAC_CODEColumn.ColumnName].ToString());
                        }
                        
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageRender.ShowMessage(Ex.Message);
            }
            finally { }
        }


        private bool isExistInvoiceNo()
        {
            int counts = 0;
            if (!string.IsNullOrEmpty(txtInvoiceNo.Text))
            {
                using (VoucherTransactionSystem transSystem = new VoucherTransactionSystem())
                {
                    counts = transSystem.IsExistsGSTInvoceNno(txtInvoiceNo.Text.Trim(), VoucherId);
                }
            }
            return (counts==0);
        }

        private bool checkLedgerTaxableAmount()
        {
            bool rtn = true;

            try
            {
                if (IsGeneralInvolice)
                {
                    if (gcLedgerDetails.DataSource != null)
                    {
                        DataTable dtGSTdetails = gcLedgerDetails.DataSource as DataTable;
                        dtGSTdetails.AcceptChanges();
                        DataTable dtGSTdetailsDetails = dtGSTdetails.DefaultView.ToTable();
                        using (VoucherSystem vsystem = new VoucherSystem())
                        {
                            //Reupdate total column
                            if (dtGSTdetailsDetails != null)
                            {
                                foreach (DataRow dr in dtGSTdetails.Rows)
                                {
                                    dr.BeginEdit();
                                    double quantity = UtilityMember.NumberSet.ToDouble(dr[vsystem.AppSchema.GSTInvoiceMasterLedgerDetails.QUANTITYColumn.ColumnName].ToString());
                                    double unitprice = UtilityMember.NumberSet.ToDouble(dr[vsystem.AppSchema.GSTInvoiceMasterLedgerDetails.UNIT_AMOUNTColumn.ColumnName].ToString());
                                    dr[vsystem.AppSchema.GSTInvoiceMasterLedgerDetails.AMOUNTColumn.ColumnName] = (quantity * unitprice);
                                    dr.EndEdit();
                                }
                                dtGSTdetails.AcceptChanges();
                            }
                            
                            string filter = vsystem.AppSchema.GSTInvoiceMasterLedgerDetails.ITEM_NAMEColumn.ColumnName + " <> '' AND " +
                                                    vsystem.AppSchema.GSTInvoiceMasterLedgerDetails.QUANTITYColumn.ColumnName + " >0 AND " +
                                                    vsystem.AppSchema.GSTInvoiceMasterLedgerDetails.UNIT_AMOUNTColumn.ColumnName + " >0";
                            double SumTotalAmount = UtilityMember.NumberSet.ToDouble(dtGSTdetailsDetails.Compute("SUM(" + 
                                    vsystem.AppSchema.GSTInvoiceMasterLedgerDetails.AMOUNTColumn.ColumnName + ")", filter).ToString());

                            if (SumTotalAmount != VoucherInvoiceAmount)
                            {
                                this.ShowMessageBox("Voucher Amount is mismatching with Invoice Amount.");
                                rtn = false;
                            }
                        }
                    }
                }
                else
                {
                    if (gcLedgerDetails.DataSource != null)
                    {
                        DataTable dtGSTdetails = gcLedgerDetails.DataSource as DataTable;
                        DataTable dtGSTdetailsDetails = dtGSTdetails.DefaultView.ToTable();
                        using (VoucherSystem vsystem = new VoucherSystem())
                        {
                            foreach (DataRow dr in dtGSTdetailsDetails.Rows)
                            {
                                string ledgername = dr[vsystem.AppSchema.Ledger.LEDGER_NAMEColumn.ColumnName].ToString();
                                double ledgertaxamount = this.UtilityMember.NumberSet.ToDouble(dr[vsystem.AppSchema.GSTInvoiceMaster.AMOUNTColumn.ColumnName].ToString());
                                double quantity = this.UtilityMember.NumberSet.ToDouble(dr[vsystem.AppSchema.GSTInvoiceMasterLedgerDetails.QUANTITYColumn.ColumnName].ToString());
                                double unitamount = this.UtilityMember.NumberSet.ToDouble(dr[vsystem.AppSchema.GSTInvoiceMasterLedgerDetails.UNIT_AMOUNTColumn.ColumnName].ToString());
                                double discount = this.UtilityMember.NumberSet.ToDouble(dr[vsystem.AppSchema.GSTInvoiceMasterLedgerDetails.DISCOUNTColumn.ColumnName].ToString());
                                if (quantity > 0 || unitamount > 0)
                                {
                                    if (((quantity * unitamount) - discount) != ledgertaxamount)
                                    {
                                        rtn = false;
                                        this.ShowMessageBoxWarning("'" + ledgername + "' Taxable amount is mismatching with detail amount (Quantity, Unit Amount and Discount), " +
                                                   "Please make it equal.");
                                        gcLedgerDetails.Select();
                                        gcLedgerDetails.Focus();
                                        gvLedgerDetails.FocusedRowHandle = dtGSTdetailsDetails.Rows.IndexOf(dr); ;
                                        gvLedgerDetails.FocusedColumn = colQuantity;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception err)
            {
                rtn = false;
                this.ShowMessageBoxError(err.Message);
            }

            return rtn;
        }

        private void RealColumnQuantity()
        {
            colQuantity.RealColumnEdit.EditValueChanged += new System.EventHandler(RealColumnEditQuantity_EditValueChanged);
            this.gvLedgerDetails.MouseDown += (object sender, MouseEventArgs e) =>
            {
                GridHitInfo hitInfo = gvLedgerDetails.CalcHitInfo(e.Location);
                if (hitInfo.Column != null && hitInfo.Column == colQuantity)
                {
                    this.BeginInvoke(new MethodInvoker(delegate
                    {
                        gvLedgerDetails.ShowEditorByMouse();
                    }));
                }
            };
        }

        private void RealColumnUnitPrice()
        {
            colUnitAmount.RealColumnEdit.EditValueChanged += new System.EventHandler(RealColumnEditUnitPrice_EditValueChanged);
            this.gvLedgerDetails.MouseDown += (object sender, MouseEventArgs e) =>
            {
                GridHitInfo hitInfo = gvLedgerDetails.CalcHitInfo(e.Location);
                if (hitInfo.Column != null && hitInfo.Column == colUnitAmount)
                {
                    this.BeginInvoke(new MethodInvoker(delegate
                    {
                        gvLedgerDetails.ShowEditorByMouse();
                    }));
                }
            };
        }
        
        void RealColumnEditQuantity_EditValueChanged(object sender, System.EventArgs e)
        {
            BaseEdit edit = sender as BaseEdit;
            if (edit.EditValue == null) return;
            gvLedgerDetails.PostEditor();
            gvLedgerDetails.UpdateCurrentRow();
            if (gvLedgerDetails.ActiveEditor == null)
                gvLedgerDetails.ShowEditor();

            TextEdit txtQuantity = edit as TextEdit;
            int grpCounts = (this.UtilityMember.NumberSet.ToInteger(this.AppSetting.DecimalPlaces) + 1);
            if (txtQuantity.Text.Length > grpCounts && txtQuantity.SelectionLength == txtQuantity.Text.Length)
                txtQuantity.Select(txtQuantity.Text.Length - grpCounts, 0);

               UpdateAmount();
        }

        void RealColumnEditUnitPrice_EditValueChanged(object sender, System.EventArgs e)
        {
            BaseEdit edit = sender as BaseEdit;
            if (edit.EditValue == null) return;
            gvLedgerDetails.PostEditor();
            gvLedgerDetails.UpdateCurrentRow();
            if (gvLedgerDetails.ActiveEditor == null)
                gvLedgerDetails.ShowEditor();

            TextEdit txtUnitPrice = edit as TextEdit;
            int grpCounts = (this.UtilityMember.NumberSet.ToInteger(this.AppSetting.DecimalPlaces) + 1);
            if (txtUnitPrice.Text.Length > grpCounts && txtUnitPrice.SelectionLength == txtUnitPrice.Text.Length)
                txtUnitPrice.Select(txtUnitPrice.Text.Length - grpCounts, 0);

            UpdateAmount();
        }

        private void UpdateAmount()
        {
            if (IsGeneralInvolice)
            {
                double amt = (UnitPrice * Quantity);
                gvLedgerDetails.SetRowCellValue(gvLedgerDetails.FocusedRowHandle, colAmount, amt);
            }
        }
        
        /// <summary>
        /// Bind the country details to the lookup control
        /// </summary>
        private void LoadCountryDetails(GridLookUpEdit glkpcountry)
        {
            try
            {
                
                using (CountrySystem countrySystem = new CountrySystem())
                {
                    ResultArgs resultArgs = countrySystem.FetchCountryListDetails();
                    if (resultArgs.Success)
                    {
                        this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpcountry, resultArgs.DataSource.Table, countrySystem.AppSchema.Country.COUNTRYColumn.ColumnName, countrySystem.AppSchema.Country.COUNTRY_IDColumn.ColumnName);
                        // glkpCountry.EditValue = glkpCountry.Properties.GetKeyValue(0);
                    }
                    else
                    {
                        MessageRender.ShowMessage(resultArgs.Message, true);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }
        
        /// <summary>
        /// Bind the country details to the lookup control
        /// </summary>
        private void LoadStateDetails(GridLookUpEdit glkpstate, GridLookUpEdit glkpcountry)
        {
            try
            {
                using (StateSystem stateSystem = new StateSystem())
                {
                    stateSystem.CountryId = glkpcountry.EditValue != null ? this.UtilityMember.NumberSet.ToInteger(glkpcountry.EditValue.ToString()) : 0;
                    ResultArgs resultArgs = stateSystem.FetchStateListDetails();
                    if (resultArgs.Success)
                    {
                        this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpstate, resultArgs.DataSource.Table, stateSystem.AppSchema.State.STATE_NAMEColumn.ColumnName, stateSystem.AppSchema.State.STATE_IDColumn.ColumnName);
                        glkpstate.EditValue = glkpstate.Properties.GetDisplayValue(0);
                    }
                    else
                    {
                        MessageRender.ShowMessage(resultArgs.Message, true);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }

        public void NewState(GridLookUpEdit glkpstate, GridLookUpEdit glkpcountry)
        {
            if (this.AppSetting.LockMasters == (int)YesNo.No)
            {
                frmStateAdd frmState = new frmStateAdd();
                frmState.ShowDialog();
                if (frmState.ReturnDialog == System.Windows.Forms.DialogResult.OK)
                {
                    LoadStateDetails(glkpstate, glkpcountry);
                    if (frmState.ReturnValue != null && this.UtilityMember.NumberSet.ToInteger(frmState.ReturnValue.ToString()) > 0)
                    {
                        glkpstate.EditValue = this.UtilityMember.NumberSet.ToInteger(frmState.ReturnValue.ToString());
                    }
                }
            }
            else
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.LOCK_MASTERS_MESSAGE));
            }
        }

        private void NewCountry(GridLookUpEdit glkpcountry)
        {
            if (this.AppSetting.LockMasters == (int)YesNo.No)
            {
                frmCountry frmCountryAdd = new frmCountry();
                frmCountryAdd.ShowDialog();
                if (frmCountryAdd.ReturnDialog == System.Windows.Forms.DialogResult.OK)
                {
                    LoadCountryDetails(glkpcountry);
                    if (frmCountryAdd.ReturnValue != null && this.UtilityMember.NumberSet.ToInteger(frmCountryAdd.ReturnValue.ToString()) > 0)
                    {
                        glkpcountry.EditValue = this.UtilityMember.NumberSet.ToInteger(frmCountryAdd.ReturnValue.ToString());
                    }
                }
            }
            else
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.LOCK_MASTERS_MESSAGE));
            }
        }

        private void SetGSTInvoiceMasterDetails()
        {
            using (VoucherTransactionSystem vouchersystem = new VoucherTransactionSystem())
            {
                this.InvoiceNo = txtInvoiceNo.Text.Trim();
                this.InvoiceDate = this.UtilityMember.DateSet.ToDate(dtInvoceDate.DateTime.ToShortDateString());
                this.VendorId = glkVendor.EditValue != null ? this.UtilityMember.NumberSet.ToInteger(glkVendor.EditValue.ToString()) : 0;
                this.InvoiceType = cbInvoiceType.SelectedIndex;

                DtGSTInvoiceMasterDetails = vouchersystem.AppSchema.GSTInvoiceMaster.DefaultView.ToTable();
                DataRow dr = DtGSTInvoiceMasterDetails.NewRow();
                dr[vouchersystem.AppSchema.GSTInvoiceMaster.GST_INVOICE_IDColumn.ColumnName] = GSTInvoiceId;
                dr[vouchersystem.AppSchema.GSTInvoiceMaster.GST_VENDOR_INVOICE_NOColumn.ColumnName] = txtInvoiceNo.Text.Trim();
                dr[vouchersystem.AppSchema.GSTInvoiceMaster.GST_VENDOR_INVOICE_DATEColumn.ColumnName] = this.UtilityMember.DateSet.ToDate(dtInvoceDate.DateTime.ToShortDateString());
                dr[vouchersystem.AppSchema.GSTInvoiceMaster.GST_VENDOR_IDColumn.ColumnName] = glkVendor.EditValue != null ? this.UtilityMember.NumberSet.ToInteger(glkVendor.EditValue.ToString()) : 0;
                dr[vouchersystem.AppSchema.GSTInvoiceMaster.GST_VENDOR_INVOICE_TYPEColumn.ColumnName] = cbInvoiceType.SelectedIndex;
                dr[vouchersystem.AppSchema.GSTInvoiceMaster.DUE_DATEColumn.ColumnName] = this.UtilityMember.DateSet.ToDate(dtDueDate.DateTime.ToShortDateString());

                dr[vouchersystem.AppSchema.GSTInvoiceMaster.TRANSPORT_MODEColumn.ColumnName] = txtTransportMode.Text.Trim();
                dr[vouchersystem.AppSchema.GSTInvoiceMaster.VEHICLE_NUMBERColumn.ColumnName] = txtVechicleNumber.Text.Trim();
                dr[vouchersystem.AppSchema.GSTInvoiceMaster.SUPPLY_DATEColumn.ColumnName] = this.UtilityMember.DateSet.ToDate(dtSupplyDate.DateTime.ToShortDateString());
                dr[vouchersystem.AppSchema.GSTInvoiceMaster.SUPPLY_PLACEColumn.ColumnName] = txtPlaceOfSupply.Text.Trim();
                dr[vouchersystem.AppSchema.GSTInvoiceMaster.BILLING_NAMEColumn.ColumnName] = txtBillName.Text.Trim();
                dr[vouchersystem.AppSchema.GSTInvoiceMaster.BILLING_GST_NOColumn.ColumnName] = txtBillingGSTNo.Text.Trim();
                dr[vouchersystem.AppSchema.GSTInvoiceMaster.BILLING_ADDRESSColumn.ColumnName] = meBillingAddress.Text.Trim();
                dr[vouchersystem.AppSchema.GSTInvoiceMaster.BILLING_STATE_IDColumn.ColumnName] = glkpBillState.EditValue != null ? this.UtilityMember.NumberSet.ToInteger(glkpBillState.EditValue.ToString()) : 0;
                dr[vouchersystem.AppSchema.GSTInvoiceMaster.BILLING_STATE_NAMEColumn.ColumnName] = glkpBillState.EditValue != null ? glkpBillState.Text : string.Empty;
                if (glkpBillState.EditValue != null && glkpBillState.GetSelectedDataRow()!=null)
                {
                    DataRowView drvState = glkpBillState.GetSelectedDataRow() as DataRowView;
                    dr[vouchersystem.AppSchema.GSTInvoiceMaster.BILLING_STATE_CODEColumn.ColumnName] = drvState[vouchersystem.AppSchema.State.STATE_CODEColumn.ColumnName].ToString();
                }

                dr[vouchersystem.AppSchema.GSTInvoiceMaster.BILLING_COUNTRY_IDColumn.ColumnName] = glkpBillCountry.EditValue != null ? this.UtilityMember.NumberSet.ToInteger(glkpBillCountry.EditValue.ToString()) : 0;
                dr[vouchersystem.AppSchema.GSTInvoiceMaster.BILLING_COUNTRYColumn.ColumnName] = glkpBillCountry.EditValue != null ? glkpBillCountry.Text : string.Empty;
                dr[vouchersystem.AppSchema.GSTInvoiceMaster.SHIPPING_NAMEColumn.ColumnName] = txtShippingName.Text.Trim();
                dr[vouchersystem.AppSchema.GSTInvoiceMaster.SHIPPING_GST_NOColumn.ColumnName] = txtShippingGSTNo.Text.Trim();
                dr[vouchersystem.AppSchema.GSTInvoiceMaster.SHIPPING_ADDRESSColumn.ColumnName] = meShippingAddress.Text.Trim();
                dr[vouchersystem.AppSchema.GSTInvoiceMaster.SHIPPING_STATE_IDColumn.ColumnName] = glkpShipState.EditValue != null ? this.UtilityMember.NumberSet.ToInteger(glkpShipState.EditValue.ToString()) : 0;
                dr[vouchersystem.AppSchema.GSTInvoiceMaster.SHIPPING_STATE_NAMEColumn.ColumnName] = glkpShipState.EditValue != null ? glkpShipState.Text : string.Empty;
                if (glkpShipState.EditValue != null && glkpShipState.GetSelectedDataRow() != null)
                {
                    DataRowView drvState = glkpShipState.GetSelectedDataRow() as DataRowView;
                    dr[vouchersystem.AppSchema.GSTInvoiceMaster.SHIPPING_STATE_CODEColumn.ColumnName] = drvState[vouchersystem.AppSchema.State.STATE_CODEColumn.ColumnName].ToString();
                }
                dr[vouchersystem.AppSchema.GSTInvoiceMaster.SHIPPING_COUNTRY_IDColumn.ColumnName] = glkpShipCountry.EditValue != null ? this.UtilityMember.NumberSet.ToInteger(glkpShipCountry.EditValue.ToString()) : 0;
                dr[vouchersystem.AppSchema.GSTInvoiceMaster.CHEQUE_IN_FAVOURColumn.ColumnName] = txtChequeInFavour.Text.Trim();
                dr[vouchersystem.AppSchema.GSTInvoiceMaster.TOTAL_AMOUNTColumn.ColumnName] = (IsGeneralInvolice ? VoucherInvoiceAmount : TotalAmountWithTax);
                dr[vouchersystem.AppSchema.GSTInvoiceMaster.TOTAL_CGST_AMOUNTColumn.ColumnName] = CGST;
                dr[vouchersystem.AppSchema.GSTInvoiceMaster.TOTAL_SGST_AMOUNTColumn.ColumnName] = SGST;
                dr[vouchersystem.AppSchema.GSTInvoiceMaster.TOTAL_IGST_AMOUNTColumn.ColumnName] = IGST;

                if (VType == DefaultVoucherTypes.Journal)
                    dr[vouchersystem.AppSchema.GSTInvoiceMaster.STATUSColumn.ColumnName] = (int)GSTInvouceStatus.Active;
                else
                    dr[vouchersystem.AppSchema.GSTInvoiceMaster.STATUSColumn.ColumnName] = (int)GSTInvouceStatus.Closed;

                dr[vouchersystem.AppSchema.GSTInvoiceMaster.IS_REVERSE_CHARGEColumn.ColumnName] = 0;
                dr[vouchersystem.AppSchema.GSTInvoiceMaster.REVERSE_CHARGE_AMOUNTColumn.ColumnName] = 0;
                if (chkReverseCharge.Checked)
                {
                    dr[vouchersystem.AppSchema.GSTInvoiceMaster.IS_REVERSE_CHARGEColumn.ColumnName] = 1;
                    dr[vouchersystem.AppSchema.GSTInvoiceMaster.REVERSE_CHARGE_AMOUNTColumn.ColumnName] = txtReverseChargeAmt.Text;
                }
                DtGSTInvoiceMasterDetails.Rows.Add(dr);
            }

        }

        private void GetGSTInvoiceMasterDetails()
        {
            using (VoucherTransactionSystem vouchersystem = new VoucherTransactionSystem())
            {
                this.InvoiceNo = txtInvoiceNo.Text.Trim();
                this.InvoiceDate = this.UtilityMember.DateSet.ToDate(dtInvoceDate.DateTime.ToShortDateString());
                this.VendorId = glkVendor.EditValue != null ? this.UtilityMember.NumberSet.ToInteger(glkVendor.EditValue.ToString()) : 0;
                this.InvoiceType = cbInvoiceType.SelectedIndex;

                if (DtGSTInvoiceMasterDetails != null && DtGSTInvoiceMasterDetails.Rows.Count > 0)
                {
                    DataRow dr = DtGSTInvoiceMasterDetails.Rows[0];
                    txtInvoiceNo.Text = dr[vouchersystem.AppSchema.GSTInvoiceMaster.GST_VENDOR_INVOICE_NOColumn.ColumnName].ToString();
                    dtInvoceDate.DateTime = this.UtilityMember.DateSet.ToDate(dr[vouchersystem.AppSchema.GSTInvoiceMaster.GST_VENDOR_INVOICE_DATEColumn.ColumnName].ToString(), false);
                    glkVendor.EditValue = this.UtilityMember.NumberSet.ToInteger(dr[vouchersystem.AppSchema.GSTInvoiceMaster.GST_VENDOR_IDColumn.ColumnName].ToString());
                    cbInvoiceType.SelectedIndex = this.UtilityMember.NumberSet.ToInteger(dr[vouchersystem.AppSchema.GSTInvoiceMaster.GST_VENDOR_INVOICE_TYPEColumn.ColumnName].ToString());
                    txtTransportMode.Text = dr[vouchersystem.AppSchema.GSTInvoiceMaster.TRANSPORT_MODEColumn.ColumnName].ToString();
                    txtVechicleNumber.Text = dr[vouchersystem.AppSchema.GSTInvoiceMaster.VEHICLE_NUMBERColumn.ColumnName].ToString();
                    if (String.IsNullOrEmpty(dr[vouchersystem.AppSchema.GSTInvoiceMaster.SUPPLY_DATEColumn.ColumnName].ToString())
                        || this.UtilityMember.DateSet.ToDate(dr[vouchersystem.AppSchema.GSTInvoiceMaster.SUPPLY_DATEColumn.ColumnName].ToString(), false) == DateTime.MinValue)
                        dtSupplyDate.Text = string.Empty;
                    else
                        dtSupplyDate.DateTime = this.UtilityMember.DateSet.ToDate(dr[vouchersystem.AppSchema.GSTInvoiceMaster.SUPPLY_DATEColumn.ColumnName].ToString(), false);

                    if (String.IsNullOrEmpty(dr[vouchersystem.AppSchema.GSTInvoiceMaster.DUE_DATEColumn.ColumnName].ToString())
                        || this.UtilityMember.DateSet.ToDate(dr[vouchersystem.AppSchema.GSTInvoiceMaster.DUE_DATEColumn.ColumnName].ToString(), false) == DateTime.MinValue)
                        dtDueDate.Text = string.Empty;
                    else
                        dtDueDate.DateTime = this.UtilityMember.DateSet.ToDate(dr[vouchersystem.AppSchema.GSTInvoiceMaster.DUE_DATEColumn.ColumnName].ToString(), false);

                    txtPlaceOfSupply.Text = dr[vouchersystem.AppSchema.GSTInvoiceMaster.SUPPLY_PLACEColumn.ColumnName].ToString();
                    txtBillName.Text  = dr[vouchersystem.AppSchema.GSTInvoiceMaster.BILLING_NAMEColumn.ColumnName].ToString().Trim();
                    txtBillingGSTNo.Text = dr[vouchersystem.AppSchema.GSTInvoiceMaster.BILLING_GST_NOColumn.ColumnName].ToString().Trim();
                    string billingaddress = dr[vouchersystem.AppSchema.GSTInvoiceMaster.BILLING_ADDRESSColumn.ColumnName].ToString();
                    meBillingAddress.Text = billingaddress; //dr[vouchersystem.AppSchema.GSTInvoiceMaster.BILLING_ADDRESSColumn.ColumnName].ToString();
                    glkpBillCountry.EditValue = this.UtilityMember.NumberSet.ToInteger(dr[vouchersystem.AppSchema.GSTInvoiceMaster.BILLING_COUNTRY_IDColumn.ColumnName].ToString());
                    glkpBillState.EditValue = this.UtilityMember.NumberSet.ToInteger(dr[vouchersystem.AppSchema.GSTInvoiceMaster.BILLING_STATE_IDColumn.ColumnName].ToString());
                    txtShippingName.Text = dr[vouchersystem.AppSchema.GSTInvoiceMaster.SHIPPING_NAMEColumn.ColumnName].ToString().Trim();
                    txtShippingGSTNo.Text = dr[vouchersystem.AppSchema.GSTInvoiceMaster.SHIPPING_GST_NOColumn.ColumnName].ToString().Trim();
                    meShippingAddress.Text = dr[vouchersystem.AppSchema.GSTInvoiceMaster.SHIPPING_ADDRESSColumn.ColumnName].ToString();
                    glkpShipCountry.EditValue = this.UtilityMember.NumberSet.ToInteger(dr[vouchersystem.AppSchema.GSTInvoiceMaster.SHIPPING_COUNTRY_IDColumn.ColumnName].ToString());
                    glkpShipState.EditValue = this.UtilityMember.NumberSet.ToInteger(dr[vouchersystem.AppSchema.GSTInvoiceMaster.SHIPPING_STATE_IDColumn.ColumnName].ToString());
                    txtChequeInFavour.Text = dr[vouchersystem.AppSchema.GSTInvoiceMaster.CHEQUE_IN_FAVOURColumn.ColumnName].ToString();

                    lcReverseChargeAmt.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    chkReverseCharge.Checked = false;
                    txtReverseChargeAmt.Text = "0";
                    if (UtilityMember.NumberSet.ToInteger(dr[vouchersystem.AppSchema.GSTInvoiceMaster.IS_REVERSE_CHARGEColumn.ColumnName].ToString()) == 1)
                    {
                        chkReverseCharge.Checked = true;
                        txtReverseChargeAmt.Text = UtilityMember.NumberSet.ToDouble(dr[vouchersystem.AppSchema.GSTInvoiceMaster.REVERSE_CHARGE_AMOUNTColumn.ColumnName].ToString()).ToString();
                        lcReverseChargeAmt.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    }
                }
            }
        }

        private double GetInvoiceBalanceAmount()
        {
            double damt = 0;
            if (GSTInvoiceId > 0)
            {
                using (VoucherTransactionSystem vouchersystem = new VoucherTransactionSystem())
                {
                    ResultArgs result =  vouchersystem.FetchRandPVoucherAgainstJournalInvoiceByInvoiceId(GSTInvoiceId);
                    if (result.Success && result.DataSource.Table != null && result.DataSource.Table.Rows.Count>0)
                    {
                        damt = UtilityMember.NumberSet.ToDouble(result.DataSource.Table.Compute("SUM(" + 
                            vouchersystem.AppSchema.GSTInvoiceMasterLedgerDetails.AMOUNTColumn.ColumnName + ")", string.Empty).ToString());
                    }
                }
            }
            return damt;
        }

        private void glkVendor_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Plus)
            {
                ShowVerdorForm();
            }
        }

        private void glkVendor_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                VendorAddressCombined = string.Empty;
                VendorName = string.Empty;
                VendorGSTNo = string.Empty;
                VendorAddress = string.Empty;
                lblVendorPAN.Text = "<b>PAN No  : </b>";
                lblVendorAaddress.Text = "<b>Address : </b>";
                lblVendorGSTNo.Text = "<b>GST No : </b>";
                chkSameAsBillingAddresss.Checked = chkSameAsVendorAddress.Checked = false;
                using (VendorInfoSystem vendorSystem = new VendorInfoSystem())
                {
                    vendorSystem.Id = VendorId;
                    ResultArgs resultArgs = vendorSystem.FetchSelectedDetails();
                    if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        DataTable dtVendorDetails = resultArgs.DataSource.Table;
                        VendorName= dtVendorDetails.Rows[0][vendorSystem.AppSchema.Vendors.VENDORColumn.ColumnName].ToString();
                        VendorGSTNo= dtVendorDetails.Rows[0][vendorSystem.AppSchema.Vendors.GST_NOColumn.ColumnName].ToString();
                        VendorAddress = dtVendorDetails.Rows[0][vendorSystem.AppSchema.Vendors.ADDRESSColumn.ColumnName].ToString();
                        VendorAddressCombined = VendorAddress;
                        VendorStateId =  UtilityMember.NumberSet.ToInteger(dtVendorDetails.Rows[0][vendorSystem.AppSchema.State.STATE_IDColumn.ColumnName].ToString());
                        VendorState =  dtVendorDetails.Rows[0][vendorSystem.AppSchema.State.STATE_NAMEColumn.ColumnName].ToString();
                        VendorCountryId = UtilityMember.NumberSet.ToInteger(dtVendorDetails.Rows[0][vendorSystem.AppSchema.Country.COUNTRY_IDColumn.ColumnName].ToString());
                        VendorCountry = dtVendorDetails.Rows[0][vendorSystem.AppSchema.Country.COUNTRYColumn.ColumnName].ToString();
                        lblVendorPAN.Text +=  dtVendorDetails.Rows[0][vendorSystem.AppSchema.Vendors.PAN_NOColumn.ColumnName].ToString();
                        lblVendorGSTNo.Text += dtVendorDetails.Rows[0][vendorSystem.AppSchema.Vendors.GST_NOColumn.ColumnName].ToString();
                        if (!string.IsNullOrEmpty(VendorState))
                        {
                            VendorAddressCombined += (!string.IsNullOrEmpty(VendorAddress) ? "," : "") + VendorState;
                        }

                        if (!string.IsNullOrEmpty(VendorCountry))
                        {
                            VendorAddressCombined += (!string.IsNullOrEmpty(VendorAddress) ? "," : "") + VendorCountry;
                        }

                        lblVendorAaddress.Text += VendorAddressCombined;
                        /*if (VoucherId == 0)
                        {
                            meBillingAddress.Text = VendorAddress.Trim();
                            meShippingAddress.Text = VendorAddress.Trim();
                        }*/
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageRender.ShowMessage(Ex.Message);
            }
            finally { }
        }
               
        private void frmVoucherVendorGSTInvoiceDetails_Load(object sender, EventArgs e)
        {

           //On 17/12/2024, To Hide few properties for multi currency ------------------------------------------------------------------------
            colItemName.Visible = colItemDescription.Visible = colDelete.Visible = false;
            lcVoucherAmount.Visibility = lcDueDate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            
            if (IsGeneralInvolice)
            {
                InvoiceBalanceAmount = GetInvoiceBalanceAmount();
                lcVoucherAmount.Visibility = lcDueDate.Visibility= DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                lblVoucherAmount.Text = UtilityMember.NumberSet.ToNumber(VoucherInvoiceAmount);
                lcVoucherAmount.Text = string.IsNullOrEmpty(VoucherInvoiceLedgerName) ? " " : VoucherInvoiceLedgerName;
                if (InvoiceBalanceAmount != 0 )
                {
                    lblVoucherAmount.Text += " :: Balance Due " + UtilityMember.NumberSet.ToNumber(VoucherInvoiceAmount-InvoiceBalanceAmount);
                }
                colItemName.Visible = colItemDescription.Visible = colDelete.Visible = true;
                colAmount.Caption = "Amount";

                lcgShippingAddress.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                colLedger.Visible = colGSTLedgerClass.Visible = colUnitMeasurement.Visible = colDiscount.Visible = colLedgerHSN_SACCode.Visible = false;
                
                lcCtlVendorInvoce.HideItem(lcInvoiceType);
                lcCtlVendorInvoce.HideItem(lblVendorPAN);
                lcCtlVendorInvoce.HideItem(lblVendorGSTNo);
                lcCtlVendorInvoce.HideItem(emptyPanST);
                lcCtlVendorInvoce.HideItem(lcVechileNumber);
                lcCtlVendorInvoce.HideItem(lcTransportmode);
                lcCtlVendorInvoce.HideItem(emptyTransport);
                
                lcCtlVendorInvoce.HideItem(lcReverseCharge);
                lcCtlVendorInvoce.HideItem(lcReverseChargeAmt);
                lcCtlVendorInvoce.HideItem(lcDateOfSupply);
                lcCtlVendorInvoce.HideItem(lcPlaceOfSupply);
                lcCtlVendorInvoce.HideItem(emptySupply);
                lcCtlVendorInvoce.HideItem(emptyReverseCharge);
                lcCtlVendorInvoce.HideItem(lcShoppingSameBilling);
                lcCtlVendorInvoce.HideItem(lcBillingGSTNo);

                gvLedgerDetails.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.True;
                TransacationGridNewItem = true;
                colAmount.OptionsColumn.AllowFocus = false;
                colAmount.VisibleIndex = colUnitAmount.VisibleIndex + 1;
                colDelete.VisibleIndex = colAmount.VisibleIndex + 1; 
                this.Height = 550;
                
            }
            //----------------------------------------------------------------------------------------------------------------------------------
        }

        private void chkSameAsVendorAddress_CheckedChanged(object sender, EventArgs e)
        {
            //!string.IsNullOrEmpty(VendorAddressCombined) && 
            if (chkSameAsVendorAddress.Checked && this.ShowConfirmationMessage("Are you sure to assign Vendor Address as Billing Address ?",
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.OK)
            {
                txtBillName.Text = VendorName;
                txtBillingGSTNo.Text = VendorGSTNo;
                meBillingAddress.Text = VendorAddress;
                glkpBillCountry.EditValue = VendorCountryId;
                glkpBillState.EditValue = VendorStateId;
            }
            else
            {
                //chkSameAsVendorAddress.Checked = false;
            }
        }

        private void chkSameAsBillingAddresss_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSameAsBillingAddresss.Checked && this.ShowConfirmationMessage("Are you sure to assign Billing Address as Shipping Address ?", 
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.OK)
            {
                txtShippingName.Text = txtBillName.Text;
                txtShippingGSTNo.Text = txtBillingGSTNo.Text;
                meShippingAddress.Text = meBillingAddress.Text;
                glkpShipCountry.EditValue = glkpBillCountry.EditValue;
                glkpShipState.EditValue = glkpBillState.EditValue;
            }
            else
            {
                //chkSameAsBillingAddresss.Checked = false;
            }
        }

        private void glkpShippState_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Plus)
            {
                NewState(glkpShipState, glkpShipCountry);
            }
        }

        private void glkpBillState_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Plus)
            {
                NewState(glkpBillState, glkpBillCountry);
            }
        }

        private void glkpBillCountry_EditValueChanged(object sender, EventArgs e)
        {
            if (glkpBillCountry.EditValue != null)
            {
                using (StateSystem statesys = new StateSystem())
                {
                    statesys.CountryId = this.UtilityMember.NumberSet.ToInteger(glkpBillCountry.EditValue.ToString());
                    ResultArgs resultArgs = statesys.FetchStateListDetails();
                    if (resultArgs.Success)
                    {
                        this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpBillState, resultArgs.DataSource.Table, statesys.AppSchema.State.STATE_NAMEColumn.ColumnName, statesys.AppSchema.State.STATE_IDColumn.ColumnName);
                    }
                }
            }
        }

        private void glkpShippCountry_EditValueChanged(object sender, EventArgs e)
        {
            if (glkpShipCountry.EditValue != null)
            {
                using (StateSystem statesys = new StateSystem())
                {
                    statesys.CountryId = this.UtilityMember.NumberSet.ToInteger(glkpShipCountry.EditValue.ToString());
                    ResultArgs resultArgs = statesys.FetchStateListDetails();
                    if (resultArgs.Success)
                    {
                        this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpShipState, resultArgs.DataSource.Table, statesys.AppSchema.State.STATE_NAMEColumn.ColumnName, statesys.AppSchema.State.STATE_IDColumn.ColumnName);
                    }
                }
            }
        }

        private void chkReverseCharge_CheckedChanged(object sender, EventArgs e)
        {
            lcReverseChargeAmt.Visibility = chkReverseCharge.Checked ? DevExpress.XtraLayout.Utils.LayoutVisibility.Always : DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            if (lcReverseChargeAmt.Visibility == DevExpress.XtraLayout.Utils.LayoutVisibility.Always)
            {
                txtReverseChargeAmt.Select();
                txtReverseChargeAmt.Focus();
            }
        }

        private void gvLedgerDetails_ShownEditor(object sender, EventArgs e)
        {
            TextEdit txtnarration = (sender as GridView).ActiveEditor as TextEdit;
            if (txtnarration != null)
            {
                txtnarration.MaskBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
                txtnarration.MaskBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                txtnarration.MaskBox.AutoCompleteCustomSource = collNarration;
            }
        }

        private void gcLedgerDetails_ProcessGridKey(object sender, KeyEventArgs e)
        {
            if (IsGeneralInvolice)
            {
                 bool canFoucsCashTrnasaction = false;
                 if ((e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return || e.KeyCode == Keys.Tab) && !e.Shift && !e.Alt && !e.Control &&
                     (gvLedgerDetails.FocusedColumn == colUnitAmount))
                 {
                     gvLedgerDetails.PostEditor();
                     gvLedgerDetails.UpdateCurrentRow();

                     bool isvalidrow = !string.IsNullOrEmpty(ItemName) && Quantity > 0 && UnitPrice > 0;

                     if (!isvalidrow)
                     {
                         if (IsValidSource()) { canFoucsCashTrnasaction = true; }
                         else { FocusTransactionGrid(); }
                     }

                     /*if (isvalidrow && gvLedgerDetails.IsLastRow)
                     {
                         TransacationGridNewItem = true;
                     }*/
                     if ((e.KeyCode == Keys.Tab || e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return) &&
                        (isvalidrow && gvLedgerDetails.IsLastRow))
                     {
                         TransacationGridNewItem = true;

                         e.SuppressKeyPress = true;
                     }

                     if (canFoucsCashTrnasaction)
                     {
                         gvLedgerDetails.CloseEditor();
                         e.Handled = true;
                         e.SuppressKeyPress = true;
                         btnOkay.Select();
                         btnOkay.Focus();
                     }
                 }
            }
            else
            {
                if ((e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab))
                {
                    if (gvLedgerDetails.IsLastRow && gvLedgerDetails.FocusedColumn == colLedgerHSN_SACCode)
                    {
                        btnOkay.Select();
                        btnOkay.Focus();
                        btnOkay.ShowFocusRectangle = DevExpress.Utils.DefaultBoolean.True;
                    }
                }
            }
        }

        private void gcLedgerDetails_Click(object sender, EventArgs e)
        {

        }

        private void rbtndelete_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            DeleteTransaction();
        }

        private void meBillingAddress_EditValueChanged(object sender, EventArgs e)
        {

        }

    }
}