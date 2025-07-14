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
    public partial class UcPaymonthStaffProfile : UserControl
    {

        #region Constructor

        public UcPaymonthStaffProfile()
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
                gcPaymonthStaffProfile.Select();
                gcPaymonthStaffProfile.Focus();
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
                lcGrpStaffPaymonthDetails.TextVisible = value;
            }
            get
            {
                return lcGrpStaffPaymonthDetails.TextVisible;
            }
        }
        #endregion

        #region Events
        private void gvFDFullHistory_RowStyle(object sender, RowStyleEventArgs e)
        {
            //if (e.RowHandle >= 0)
            //{
            //    string fdtype = gvPaymonthStaffProfile.GetRowCellDisplayText(e.RowHandle, colFDHType);
            //    string fdrenewaltype = gvPaymonthStaffProfile.GetRowCellDisplayText(e.RowHandle, colFDHRenewalType);
            //    if (fdtype == FDTypes.RN.ToString())
            //    {
            //        e.Appearance.BackColor = Color.Salmon;
            //        e.Appearance.BackColor2 = Color.SeaShell;
            //    }
            //    else if (fdrenewaltype == FDRenewalTypes.WDI.ToString() && fdtype == FDTypes.WD.ToString())
            //    {
            //        e.Appearance.BackColor = Color.LightYellow;
            //        e.Appearance.BackColor2 = Color.Yellow;
            //    }
            //    else if (fdrenewaltype == FDRenewalTypes.PWD.ToString() && fdtype == FDTypes.WD.ToString())
            //    {
            //        e.Appearance.BackColor = Color.LightGreen;
            //        e.Appearance.BackColor2 = Color.Green;
            //    }
            //}
        }
       
        #endregion

        #region Methods
        /// <summary>
        /// on 19/10/2022 to load FD full history
        /// </summary>
        private void LoadFDFullHistory()
        {
            string FDinvestedbankledger = string.Empty;
            string FDtranstype = string.Empty;
            string FDaccountNumber = string.Empty;
            colPaymentMode.Visible = (Settingproperty.EnableFlexiFD=="1");

            gcPaymonthStaffProfile.DataSource = null;
            if (FDAccountId > 0)
            {
                using (FDAccountSystem fdAccountSystem = new FDAccountSystem())
                {

                    resultArgs = fdAccountSystem.FetchFDFullHistory(FDAccountId);
                    gcPaymonthStaffProfile.DataSource = dtFDFullHistory = null;
                    if (resultArgs != null && resultArgs.Success)
                    {
                        dtFDFullHistory = resultArgs.DataSource.Table;

                        if (dtFDFullHistory != null && dtFDFullHistory.Rows.Count > 0)
                        {
                            DataTable dtFDHistory = dtFDFullHistory.DefaultView.ToTable();
                            dtFDHistory.DefaultView.RowFilter = fdAccountSystem.AppSchema.FDRenewal.FD_ACCOUNT_IDColumn.ColumnName + " =" + FDAccountId;
                            dtFDHistory.DefaultView.Sort = fdAccountSystem.AppSchema.FDRenewal.RENEWAL_DATEColumn.ColumnName + " DESC";
                            dtFDHistory = dtFDHistory.DefaultView.ToTable();
                            gcPaymonthStaffProfile.DataSource = dtFDHistory;
                            if (dtFDHistory.Rows.Count > 0)
                            {
                                FDaccountNumber = dtFDHistory.Rows[0][fdAccountSystem.AppSchema.FDAccount.FD_ACCOUNT_NUMBERColumn.ColumnName].ToString();
                                FDtranstype = dtFDHistory.Rows[0][fdAccountSystem.AppSchema.FDAccount.TRANS_TYPEColumn.ColumnName].ToString();
                                FDinvestedbankledger = dtFDHistory.Rows[0]["INVESTED_BANK_LEDGER"].ToString();

                                lblStaffCode.Text = FDaccountNumber + " - " + FDtranstype;
                                //lblInvestedBankLedger.Text = string.IsNullOrEmpty(FDinvestedbankledger)? " " : FDinvestedbankledger;
                                lblNoOfHistories.Text = "# " + dtFDHistory.Rows.Count.ToString();
                                AssignBalance();
                            }
                        }
                    }
                }
            }
            gcPaymonthStaffProfile.RefreshDataSource();
        }

        private void AssignBalance()
        {
            //lblFDAmount.Text = "0.00";
            //lblFDBalance.Text = "0.00";
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
                //lblFDAmount.Text = UtilityMember.NumberSet.ToNumber(amt);
                //lblFDBalance.Text = UtilityMember.NumberSet.ToNumber(balance);
            }
        }
        
        #endregion

        private void gcFDFullHistory_Click(object sender, EventArgs e)
        {

        }

        private void chkShowFilter_CheckedChanged(object sender, EventArgs e)
        {
            //gvPaymonthStaffProfile.OptionsView.ShowAutoFilterRow = chkShowFilter.Checked;
            //if (chkShowFilter.Checked)
            //{
            //    gvPaymonthStaffProfile.FocusedRowHandle = DevExpress.XtraGrid.GridControl.AutoFilterRowHandle;
            //    gvPaymonthStaffProfile.FocusedColumn = colFDHRenewalTypeName;
            //    gvPaymonthStaffProfile.OptionsFind.AllowFindPanel = false;
            //    gvPaymonthStaffProfile.ShowEditor();
            //}
        }

          

        
    }
}
