using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Bosco.Utility;
using Bosco.Utility.ConfigSetting;
using Bosco.Model.UIModel;
using AcMEDSync.Model;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraGrid;
using Bosco.Model;
using Bosco.Model.Transaction;
using DevExpress.XtraGrid.Views.Grid;
using Bosco.DAO.Schema;

namespace ACPP.Modules.UIControls
{
    public partial class UcFDHistory : UserControl
    {

        #region Constructor

        public UcFDHistory()
        {
            InitializeComponent();
        }

        #endregion

        #region Declaration

        private ResultArgs resultArgs = null;
        private DataTable dtFDFullHistory = null;
        CommonMember UtilityMember = new CommonMember();
        SettingProperty Settingproperty = new SettingProperty();
        MessageCatalog messageCatolough = new MessageCatalog();
        AppSchemaSet.ApplicationSchemaSet AppSchema = new AppSchemaSet.ApplicationSchemaSet();

        #endregion

        #region Properties
        private Int32 fdaccountId = 0;
        public Int32 FDAccountId
        {
            set {
                fdaccountId = value;
                LoadFDFullHistory();
                this.Dock = DockStyle.Fill;
                gcFDFullHistory.Select();
                gcFDFullHistory.Focus();
            }
            get
            {
                return fdaccountId;
            }
        }

        public bool ShowPanelCaptionHeader
        {
            set
            {
                lcGrpFDHistory.TextVisible = value;
            }
            get
            {
                return lcGrpFDHistory.TextVisible;
            }
        }

        public GridControl GridFDHistory
        {
            get
            {
                return gcFDFullHistory;
            }
        }

        #endregion

        #region Events
        private void gvFDFullHistory_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                string fdtype = gvFDFullHistory.GetRowCellDisplayText(e.RowHandle, colFDHType);
                string fdrenewaltype = gvFDFullHistory.GetRowCellDisplayText(e.RowHandle, colFDHRenewalType);
                double interestamt = UtilityMember.NumberSet.ToDouble(gvFDFullHistory.GetRowCellDisplayText(e.RowHandle, colFDHInterestAccumulated).ToString());
                if (fdtype.ToUpper() == FDTypes.RN.ToString().ToUpper())
                {
                    e.Appearance.BackColor = Color.Salmon;
                    e.Appearance.BackColor2 = Color.SeaShell;
                }
                else if (fdrenewaltype.ToUpper() == FDRenewalTypes.WDI.ToString() && fdtype == FDTypes.WD.ToString().ToUpper())
                {
                    e.Appearance.BackColor = Color.LightYellow;
                    e.Appearance.BackColor2 = Color.Yellow;
                }
                else if (fdrenewaltype.ToUpper() == FDRenewalTypes.PWD.ToString() && fdtype == FDTypes.WD.ToString().ToUpper())
                {
                    e.Appearance.BackColor = Color.LightGreen;
                    e.Appearance.BackColor2 = Color.Green;
                }
                else if (interestamt<0 && fdrenewaltype.ToString().ToUpper() == FDRenewalTypes.ACI.ToString().ToUpper())
                {
                    e.Appearance.BackColor = Color.OrangeRed;
                    e.Appearance.BackColor2 = Color.Orange;
                }
            }
        }
       
        #endregion

        #region Methods
        /// <summary>
        /// on 19/10/2022 to load FD full history
        /// </summary>
        private void LoadFDFullHistory()
        {
            string ProjectName = string.Empty;
            string FDaccountNumber = string.Empty;
            string FDInvestedOn = string.Empty;
            string FDLedgerName = string.Empty;
            string FDinvestedbankledger = string.Empty;
            string FDtranstype = string.Empty;
            string CurrencyName = string.Empty;
            //31/07/2024, Other than India, let us lock TDS Amount
            colFDHTDSAmount.Visible = !(Settingproperty.IsCountryOtherThanIndia);

            string FDInvestedType = FDInvestmentType.FD.ToString();
            colFDHReInvestmentAmount.Visible = (Settingproperty.EnableFlexiFD=="1");
            lblFDNumber.Text = " ";
            lblInvestedBankLedger.Text = " ";
            lblNoOfHistories.Text = " ";
            lblFDAmount.Text = " ";
            lblFDBalance.Text = " ";
            lblProject.Text = " ";
            lblFDLedgerName.Text = " ";
            lblInvestedType.Text = " ";
            
            gcFDFullHistory.DataSource = null;
            if (FDAccountId > 0)
            {
                using (FDAccountSystem fdAccountSystem = new FDAccountSystem())
                {
                    resultArgs = fdAccountSystem.FetchFDFullHistory(FDAccountId);
                    gcFDFullHistory.DataSource = dtFDFullHistory = null;
                    if (resultArgs != null && resultArgs.Success)
                    {
                        dtFDFullHistory = resultArgs.DataSource.Table;

                        if (dtFDFullHistory != null && dtFDFullHistory.Rows.Count > 0)
                        {
                            DataTable dtFDHistory = dtFDFullHistory.DefaultView.ToTable();
                            dtFDHistory.DefaultView.RowFilter = fdAccountSystem.AppSchema.FDRenewal.FD_ACCOUNT_IDColumn.ColumnName + " =" + FDAccountId;
                            dtFDHistory.DefaultView.Sort = fdAccountSystem.AppSchema.FDRenewal.RENEWAL_DATEColumn.ColumnName + " DESC";
                            dtFDHistory = dtFDHistory.DefaultView.ToTable();
                            gcFDFullHistory.DataSource = dtFDHistory;
                            if (dtFDHistory.Rows.Count > 0)
                            {
                                FDaccountNumber = dtFDHistory.Rows[0][fdAccountSystem.AppSchema.FDAccount.FD_ACCOUNT_NUMBERColumn.ColumnName].ToString();
                                ProjectName = dtFDFullHistory.Rows[0][fdAccountSystem.AppSchema.Project.PROJECTColumn.ColumnName].ToString();
                                FDInvestedOn = UtilityMember.DateSet.ToDate(dtFDFullHistory.Rows[0][fdAccountSystem.AppSchema.FDAccount.INVESTMENT_DATEColumn.ColumnName].ToString(), false).ToShortDateString();
                                FDLedgerName = dtFDHistory.Rows[0]["FD_LEDGER"].ToString();
                                FDinvestedbankledger = dtFDHistory.Rows[0]["INVESTED_BANK_LEDGER"].ToString();
                                FDtranstype = dtFDHistory.Rows[0][fdAccountSystem.AppSchema.FDAccount.TRANS_TYPEColumn.ColumnName].ToString();
                                FDInvestedType = dtFDHistory.Rows[0][fdAccountSystem.AppSchema.FDInvestmentType.INVESTMENT_TYPEColumn.ColumnName].ToString();
                                CurrencyName = dtFDHistory.Rows[0][fdAccountSystem.AppSchema.Country.CURRENCY_NAMEColumn.ColumnName].ToString();

                                lblFDNumber.Text = FDaccountNumber + " - " + FDtranstype + " - " + FDInvestedOn;
                                lblProject.Text = ProjectName;
                                lblInvestedBankLedger.Text = string.IsNullOrEmpty(FDinvestedbankledger) ? " " : FDinvestedbankledger;
                                lblFDLedgerName.Text = FDLedgerName;
                                lblInvestedType.Text = FDInvestedType + (string.IsNullOrEmpty(CurrencyName) ? " " : " (" + CurrencyName + ")"); 
                                //lblCurrency.Text = string.IsNullOrEmpty(CurrencyName) ? " " : " (" + CurrencyName + ")";
                                lblNoOfHistories.Text = "# " + dtFDHistory.Rows.Count.ToString();
                                AssignBalance();
                            }
                        }
                        else
                        {
                            fdAccountSystem.FDAccountId = FDAccountId;
                            resultArgs = fdAccountSystem.FetchFDAccountDetailsByFDAccountId();
                            if (resultArgs != null && resultArgs.Success)
                            {
                                dtFDFullHistory = resultArgs.DataSource.Table;
                                if (dtFDFullHistory != null && dtFDFullHistory.Rows.Count > 0)
                                {
                                    FDaccountNumber = dtFDFullHistory.Rows[0][fdAccountSystem.AppSchema.FDAccount.FD_ACCOUNT_NUMBERColumn.ColumnName].ToString();
                                    FDInvestedOn = UtilityMember.DateSet.ToDate(dtFDFullHistory.Rows[0][fdAccountSystem.AppSchema.FDAccount.INVESTMENT_DATEColumn.ColumnName].ToString(), false).ToShortDateString();
                                    ProjectName = dtFDFullHistory.Rows[0][fdAccountSystem.AppSchema.Project.PROJECTColumn.ColumnName].ToString();
                                    FDtranstype = dtFDFullHistory.Rows[0][fdAccountSystem.AppSchema.FDAccount.TRANS_TYPEColumn.ColumnName].ToString();
                                    FDInvestedType = dtFDFullHistory.Rows[0][fdAccountSystem.AppSchema.FDInvestmentType.INVESTMENT_TYPEColumn.ColumnName].ToString();
                                    CurrencyName = dtFDFullHistory.Rows[0][fdAccountSystem.AppSchema.Country.CURRENCY_NAMEColumn.ColumnName].ToString();
                                    Int32 FDLedgerid = UtilityMember.NumberSet.ToInteger(dtFDFullHistory.Rows[0][fdAccountSystem.AppSchema.FDAccount.LEDGER_IDColumn.ColumnName].ToString());
                                    Int32 BankLedgerid = UtilityMember.NumberSet.ToInteger(dtFDFullHistory.Rows[0][fdAccountSystem.AppSchema.FDRenewal.BANK_LEDGER_IDColumn.ColumnName].ToString());

                                    if (BankLedgerid > 0)
                                    {
                                        using (LedgerSystem ledgersystem = new LedgerSystem())
                                        {
                                            FDinvestedbankledger  = ledgersystem.GetLegerName(BankLedgerid);
                                        }
                                    }

                                    if (FDLedgerid > 0)
                                    {
                                        using (LedgerSystem ledgersystem = new LedgerSystem())
                                        {
                                            FDLedgerName = ledgersystem.GetLegerName(FDLedgerid);
                                        }
                                    }
                                    
                                    lblFDNumber.Text = FDaccountNumber + " - " + FDtranstype + " - "  + FDInvestedOn;
                                    lblProject.Text = ProjectName;
                                    lblInvestedBankLedger.Text = string.IsNullOrEmpty(FDinvestedbankledger) ? " " : FDinvestedbankledger;
                                    lblFDLedgerName.Text = FDLedgerName;
                                    //lblCurrency.Text = string.IsNullOrEmpty(CurrencyName) ? " " : " (" + CurrencyName + ")";
                                    lblInvestedType.Text = FDInvestedType + (string.IsNullOrEmpty(CurrencyName) ? " " : " (" + CurrencyName + ")"); 
                                    lblFDAmount.Text = UtilityMember.NumberSet.ToNumber(UtilityMember.NumberSet.ToDouble(dtFDFullHistory.Rows[0][fdAccountSystem.AppSchema.FDAccount.AMOUNTColumn.ColumnName].ToString()));
                                    lblFDBalance.Text = lblFDAmount.Text;
                                    lblNoOfHistories.Text = "# " + dtFDFullHistory.Rows.Count.ToString();
                                }
                            }
                        }
                    }
                }
            }
            gcFDFullHistory.RefreshDataSource();
        }

        private void AssignBalance()
        {
            lblFDAmount.Text = "0.00";
            lblFDBalance.Text = "0.00";
            if (dtFDFullHistory != null)
            {
                string filter = string.Empty;
                double amt = UtilityMember.NumberSet.ToDouble(dtFDFullHistory.Rows[0][AppSchema.FDAccount.AMOUNTColumn.ColumnName].ToString());
                filter = AppSchema.FDRenewal.RENEWAL_TYPEColumn.ColumnName + " = '" + FDRenewalTypes.ACI.ToString() + "'";
                double accumulatedinterestamount = UtilityMember.NumberSet.ToDouble(dtFDFullHistory.Compute("SUM(" + AppSchema.FDRegisters.ACCUMULATED_INTEREST_AMOUNTColumn.ColumnName + ")", filter).ToString());
                filter = AppSchema.FDRenewal.FD_TYPEColumn.ColumnName + " = '" + FDRenewalTypes.RIN.ToString() + "'";
                double reinvestedamount = UtilityMember.NumberSet.ToDouble(dtFDFullHistory.Compute("SUM("+ AppSchema.FDRenewal.REINVESTED_AMOUNTColumn.ColumnName +")", filter).ToString());
                filter = AppSchema.FDRenewal.FD_TYPEColumn.ColumnName + " = '" + FDTypes.WD.ToString() + "'";
                double withdrwalamount = UtilityMember.NumberSet.ToDouble(dtFDFullHistory.Compute("SUM("+ AppSchema.FDRenewal.WITHDRAWAL_AMOUNTColumn.ColumnName  +")", filter).ToString());
                double balance = (amt + accumulatedinterestamount + reinvestedamount) - withdrwalamount;
                lblFDAmount.Text = UtilityMember.NumberSet.ToNumber(amt);
                lblFDBalance.Text = UtilityMember.NumberSet.ToNumber(balance);
            }
        }
        
        #endregion

        private void gcFDFullHistory_Click(object sender, EventArgs e)
        {

        }

        private void chkShowFilter_CheckedChanged(object sender, EventArgs e)
        {
            gvFDFullHistory.OptionsView.ShowAutoFilterRow = chkShowFilter.Checked;
            if (chkShowFilter.Checked)
            {
                gvFDFullHistory.FocusedRowHandle = DevExpress.XtraGrid.GridControl.AutoFilterRowHandle;
                gvFDFullHistory.FocusedColumn = colFDHRenewalTypeName;
                gvFDFullHistory.OptionsFind.AllowFindPanel = false;
                gvFDFullHistory.ShowEditor();
            }
        }

        private void chkFooterSum_CheckedChanged(object sender, EventArgs e)
        {
            gvFDFullHistory.OptionsView.ShowFooter= (chkShowFooterTotal.Checked);
        }

     

        
    }
}
