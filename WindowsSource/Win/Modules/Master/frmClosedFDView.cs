using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Bosco.Model.Transaction;
using Bosco.Utility;
using DevExpress.XtraGrid.Views.Grid;
using Bosco.Model.UIModel;
using ACPP.Modules.Transaction;

namespace ACPP.Modules.Master
{
    public partial class frmClosedFDView : frmFinanceBase
    {
        #region Decleartion
        public bool IsDateLoaded = false;
        ResultArgs resultArgs = new ResultArgs();
        int RowIndex = 0;
        #endregion

        #region Properties
        public string ProjectId
        {
            get
            {
                string ProId = glkpProjects.EditValue != null ? glkpProjects.EditValue.ToString() : "0";
                return ProId;
            }
        }

        private int AccountId = 0;
        private int FDAccountId
        {
            get
            {
                RowIndex = gvFDClosed.FocusedRowHandle;
                AccountId = gvFDClosed.GetFocusedRowCellValue(colFdAccountId) != null ? this.UtilityMember.NumberSet.ToInteger(gvFDClosed.GetFocusedRowCellValue(colFdAccountId).ToString()) : 0;
                return AccountId;
            }
            set { AccountId = value; }
        }

        private string FDaccountnumber = string.Empty;
        private string FDAccountNumber
        {
            get
            {
                RowIndex = gvFDClosed.FocusedRowHandle;
                FDaccountnumber = gvFDClosed.GetFocusedRowCellValue(colFDAccountNumber) != null ? gvFDClosed.GetFocusedRowCellValue(colFDAccountNumber).ToString() : string.Empty;
                return FDaccountnumber;
            }
        }

        public Int32 FDInvestmentTypeId
        {
            get
            {
                Int32 InvestmentTypId = glkpFDInvestmentType.EditValue != null ? UtilityMember.NumberSet.ToInteger(glkpFDInvestmentType.EditValue.ToString()) : 0;
                return InvestmentTypId;
            }
        }

        #endregion


        public frmClosedFDView()
        {
            InitializeComponent();
        }

        #region Events

        private void frmClosedFDView_Load(object sender, EventArgs e)
        {
            //dockFDHPanel.Visibility = DevExpress.XtraBars.Docking.DockVisibility.AutoHide;
            LoadProject();
            LoadFDInvestmentType();
            LoadCurrency();
            LoadDefaultDate();
            LoadRegisters();
            SetVisibileShortCuts(true, true);
            SetAlignment();

            //31/07/2024, Other than India, let us lock TDS Amount
            colTDSAmount.Visible = !(this.AppSetting.IsCountryOtherThanIndia);
            //lcCurrency.Visibility = (this.AppSetting.AllowMultiCurrency == 1 ? DevExpress.XtraLayout.Utils.LayoutVisibility.Always : DevExpress.XtraLayout.Utils.LayoutVisibility.Never);

            gvFDClosed.OptionsView.ShowFooter = false;
            this.AttachGridContextMenu(gcFDClosed);
            this.AttachGridContextMenu(ucFDHistory1.GridFDHistory);
        }

        private void chkShowfilter_CheckedChanged(object sender, EventArgs e)
        {
            gvFDClosed.OptionsView.ShowAutoFilterRow = chkShowfilter.Checked;
            
            if (chkShowfilter.Checked)
            {
                //this.SetFocusRowFilter(gvFDClosed, colFDAccountNumber);
                this.SetFocusRowFilter(gvFDClosed, colFDNo);
            }
        }

        private void gvFDClosed_RowCountChanged(object sender, EventArgs e)
        {
            lblRowCount.Text = gvFDClosed.RowCount.ToString();
        }

        private void frmClosedFDView_ShowFilterClicked(object sender, EventArgs e)
        {
            chkShowfilter.Checked = (chkShowfilter.Checked) ? false : true;
        }

        private void ucClosedFd_DeleteClicked(object sender, EventArgs e)
        {
            if (gvFDClosed.RowCount > 0)
            {
                DeleteFDWithdrawal();
            }
            else
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.COMMON_GRID_EMPTY));
            }

        }

        private void ucClosedFd_CloseClicked(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ucClosedFd_EditClicked(object sender, EventArgs e)
        {
          
        }

        private void ucClosedFd_PrintClicked(object sender, EventArgs e)
        {
            PrintGridViewDetails(gcFDClosed, this.GetMessage(MessageCatalog.Master.Transaction.FDCLOSEDREGISTER_VIEW_PRINT_CAPTION), PrintType.DT, gvFDClosed, true);
        }

        private void ucClosedFd_RefreshClicked(object sender, EventArgs e)
        {
            LoadProject();
            LoadRegisters();
        }

        private void deDateFrom_Leave(object sender, EventArgs e)
        {
            if (IsDateLoaded)
            {
                deTo.DateTime = deDateFrom.DateTime.AddMonths(1).AddDays(-1);
                IsDateLoaded = true;
            }
        }

        private void deTo_Leave(object sender, EventArgs e)
        {
            if (deDateFrom.DateTime > deTo.DateTime)
            {
                DateTime dateTo = deTo.DateTime;
                deTo.DateTime = deDateFrom.DateTime;
                deDateFrom.DateTime = dateTo.Date;
            }
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            if (deDateFrom.DateTime > deTo.DateTime)
            {
                DateTime dt = deTo.DateTime;
                deTo.DateTime = deDateFrom.DateTime;
                deDateFrom.DateTime = dt;
                deTo.DateTime = deDateFrom.DateTime.AddMonths(-1);
                if (glkpProjects.EditValue != null)
                {
                    LoadRegisters();
                }
            }
            else
            {
                if (glkpProjects.EditValue != null)
                {
                    LoadRegisters();
                }
            }
        }

        private void glkpProjects_EditValueChanged(object sender, EventArgs e)
        {
            int ProId = glkpProjects.EditValue != null ? this.UtilityMember.NumberSet.ToInteger(glkpProjects.EditValue.ToString()) : 0;
            if (ProId > 0)
            {
                LoadRegisters();
            }
            else
            {
                LoadRegisters();
            }
        }
        #endregion

        #region Methods

        /// <summary>
        /// Load FD Investment Type
        /// </summary>
        private void LoadFDInvestmentType()
        {
            try
            {
                using (MappingSystem mappingSystem = new MappingSystem())
                {
                    resultArgs = mappingSystem.FetchInvestmentType();
                    glkpFDInvestmentType.Properties.DataSource = null;
                    if (resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        DataTable dtFDinvestmentType = resultArgs.DataSource.Table;
                        using (CommonMethod SelectAll = new CommonMethod())
                        {
                            this.UtilityMember.ComboSet.BindGridLookUpComboEmptyItem(glkpFDInvestmentType, dtFDinvestmentType,
                                    mappingSystem.AppSchema.FDInvestmentType.INVESTMENT_TYPEColumn.ColumnName, mappingSystem.AppSchema.FDInvestmentType.INVESTMENT_TYPE_IDColumn.ColumnName, true, "<--All-->");
                            glkpFDInvestmentType.EditValue = (Int32)FDInvestmentType.None;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + System.Environment.NewLine + ex.Source, true);
            }
            finally { }
        }

        private void LoadRegisters()
        {
            try
            {
                using (FDAccountSystem fdAccountSystem = new FDAccountSystem())
                {
                    resultArgs = fdAccountSystem.FetchFDRegistersView(deDateFrom.DateTime, deTo.DateTime, ProjectId, FDInvestmentTypeId);
                    if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count != 0)
                    {
                        DataView dv = new DataView(resultArgs.DataSource.Table);
                        dv.RowFilter = "CLOSING_STATUS NOT IN ('ACTIVE')";
                        if (dv != null)
                        {
                            if (this.AppSetting.AllowMultiCurrency == 1)
                            {
                                /*int CountryId = (glkpCurrencyCountry.EditValue == null ? 0 : this.UtilityMember.NumberSet.ToInteger(glkpCurrencyCountry.EditValue.ToString()));
                                dv.RowFilter += " AND " + fdAccountSystem.AppSchema.FDAccount.CURRENCY_COUNTRY_IDColumn.ColumnName + "=" + CountryId;*/
                            }

                            gcFDClosed.DataSource = dv.ToTable();
                            gcFDClosed.RefreshDataSource();
                        }
                    }
                    else
                    {
                        gcFDClosed.DataSource = null;
                        gcFDClosed.RefreshDataSource();
                    }
                    gvFDClosed.FocusedRowHandle = 0;
                    gvFDClosed.FocusRectStyle = DrawFocusRectStyle.RowFocus;

                    
                    colFDScheme.Visible = false;
                    if (this.UIAppSetting.EnableFlexiFD == "1")
                    {
                        colFDScheme.Visible = true;
                        colFDScheme.VisibleIndex = colFDAccountNumber.VisibleIndex + 1;
                    }
                    colReinvestmentAmt.Visible = (this.UIAppSetting.EnableFlexiFD == "1"); //28/11/2018, to lock reinvestment feature based on setting

                    ucFDHistory1.FDAccountId = FDAccountId;
                    ucFDHistory1.ShowPanelCaptionHeader = false;
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + "\n" + ex.Source);
            }
            finally { }
        }

        private void LoadProject()
        {
            try
            {
                using (MappingSystem mappingSystem = new MappingSystem())
                {
                    mappingSystem.ProjectClosedDate = deDateFrom.Text;
                    resultArgs = mappingSystem.FetchProjectsLookup();
                    glkpProjects.Properties.DataSource = null;
                    if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        using (CommonMethod SelectAll = new CommonMethod())
                        {
                            DataTable dtFDregisters = SelectAll.AddHeaderColumn(resultArgs.DataSource.Table, mappingSystem.AppSchema.Project.PROJECT_IDColumn.ColumnName, mappingSystem.AppSchema.Project.PROJECTColumn.ColumnName);
                            this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpProjects, dtFDregisters, mappingSystem.AppSchema.Project.PROJECTColumn.ColumnName, mappingSystem.AppSchema.Project.PROJECT_IDColumn.ColumnName);
                            glkpProjects.EditValue = this.UtilityMember.NumberSet.ToInteger(ProjectId) != 0 ? ProjectId : glkpProjects.Properties.GetKeyValue(0);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + System.Environment.NewLine + ex.Source, true);
            }
            finally { }
        }

        private void LoadCurrency()
        {
            //try
            //{
            //    using (CountrySystem countrySystem = new CountrySystem())
            //    {
            //        resultArgs = countrySystem.FetchCountryCurrencyDetails(UtilityMember.DateSet.ToDate(AppSetting.YearFrom, false));
            //        if (resultArgs.Success)
            //        {
            //            DataTable dtCurrency = resultArgs.DataSource.Table;
            //            dtCurrency.DefaultView.RowFilter = countrySystem.AppSchema.Country.EXCHANGE_RATEColumn.ColumnName + " > 0";
            //            dtCurrency = dtCurrency.DefaultView.ToTable();

            //            this.UtilityMember.ComboSet.BindGridLookUpComboEmptyItem(glkpCurrencyCountry, dtCurrency,
            //                countrySystem.AppSchema.Country.CURRENCYColumn.ToString(), countrySystem.AppSchema.Country.COUNTRY_IDColumn.ToString(), false, "");
            //            glkpCurrencyCountry.EditValue = AppSetting.Country;
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageRender.ShowMessage(ex.ToString(), true);
            //}
            //finally { }
        }

        public void LoadDefaultDate()
        {
            deDateFrom.DateTime = UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false);
            deTo.DateTime = deDateFrom.DateTime.AddMonths(1).AddDays(-1);
        }

        private void SetAlignment()
        {
            if (!this.AppSetting.LanguageId.Equals("en-US"))
            {
                if (this.AppSetting.LanguageId.Equals("pt-PT"))
                {
                    layoutControlItem4.Width = 400;
                    layoutControlItem2.Width = 190;
                    layoutControlItem3.Width = 190;
                }
                else
                {
                    layoutControlItem4.Width = 400;
                    layoutControlItem2.Width = 230;
                    layoutControlItem3.Width = 230;
                }
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys KeyData)
        {
            if (KeyData == (Keys.F3))
            {
                //  deDateFrom.Focus();
                frmDatePicker datePicker = new frmDatePicker(deDateFrom.DateTime, deTo.DateTime, DatePickerType.ChangePeriod);
                datePicker.ShowDialog();
                deDateFrom.DateTime = AppSetting.VoucherDateFrom;
                deTo.DateTime = AppSetting.VoucherDateTo;
            }
            return base.ProcessCmdKey(ref msg, KeyData);
        }
               

        private void DeleteFDWithdrawal()
        {
            string FDVoucherId = string.Empty;
            try
            {
                using (FDAccountSystem fdAccountSystem = new FDAccountSystem())
                {
                    if (FDAccountId > 0)
                    {
                        fdAccountSystem.FDAccountId = FDAccountId;
                        resultArgs = fdAccountSystem.FetchVoucherByFDAccount();
                        if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                        {
                            if (this.ShowConfirmationMessage("Are you sure to Reopen closed FD Account ?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                foreach (DataRow dr in resultArgs.DataSource.Table.Rows)
                                {
                                    FDVoucherId += dr[fdAccountSystem.AppSchema.FDRenewal.FD_INTEREST_VOUCHER_IDColumn.ColumnName] != DBNull.Value ? dr[fdAccountSystem.AppSchema.FDRenewal.FD_INTEREST_VOUCHER_IDColumn.ColumnName].ToString() + "," : string.Empty;
                                    FDVoucherId += dr[fdAccountSystem.AppSchema.FDRenewal.FD_VOUCHER_IDColumn.ColumnName] != DBNull.Value ? dr[fdAccountSystem.AppSchema.FDRenewal.FD_VOUCHER_IDColumn.ColumnName].ToString() : string.Empty;
                                    // FDVoucherId = FDVoucherId.TrimEnd(',');
                                    string[] VoucherId = FDVoucherId.Split(',');
                                    using (VoucherTransactionSystem voucherTransaction = new VoucherTransactionSystem())
                                    {
                                        foreach (string VId in VoucherId)
                                        {
                                            voucherTransaction.VoucherId = !string.IsNullOrEmpty(VId) ? this.UtilityMember.NumberSet.ToInteger(VId) : 0;
                                            if (voucherTransaction.VoucherId > 0)
                                            {
                                                resultArgs = voucherTransaction.DeleteVoucherTrans();
                                            }
                                        }
                                        if (resultArgs.Success)
                                        {
                                            fdAccountSystem.FDVoucherId = voucherTransaction.VoucherId;
                                            resultArgs = fdAccountSystem.DeleteFDRenewalsByVoucherId();
                                            if (resultArgs.Success)
                                            {
                                                LoadRegisters();
                                                this.ShowSuccessMessage("Selected FD Account is active");
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.COMMON_NO_RECORD_SELECTED));
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Source + Environment.NewLine + ex.Message);
            }
            finally { }
        }

        private int GetVoucherIdbyAccountId(int accountid)
        {
            int id = 0;
            using (FDRenewalSystem accountsystem = new FDRenewalSystem())
            {
                accountsystem.FDAccountId = accountid;
                id = accountsystem.GetVoucherId();
            }
            return id;
        }

        #endregion

        private void gvFDClosed_DoubleClick(object sender, EventArgs e)
        {

        }

        private void deDateFrom_EditValueChanged(object sender, EventArgs e)
        {
            //On 12/07/2018, For closed Projects----
            LoadProject();
            //--------------------------------------
        }

        private void gvFDClosed_RowCountChanged_1(object sender, EventArgs e)
        {
            lblRowCount.Text = gvFDClosed.RowCount.ToString();
        }

        private void gvFDClosed_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            ucFDHistory1.FDAccountId = FDAccountId;
        }
                

        private void dockManager1_ActivePanelChanged(object sender, DevExpress.XtraBars.Docking.ActivePanelChangedEventArgs e)
        {
            if (dockManager1.ActivePanel != null)
            {
                dockFDHPanel.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible; 
            }
        }

       
    }
}