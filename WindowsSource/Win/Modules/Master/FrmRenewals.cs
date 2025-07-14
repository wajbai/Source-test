//This form is to show the List of renewals/Post Interest that the selected FD Account contains
//Make user to select recnt Renewal/Post Interest
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
using DevExpress.XtraEditors.Repository;

namespace ACPP.Modules.Master
{
    public partial class frmRenewals : frmFinanceBase
    {

        #region Event Handlers
        public event EventHandler UpdateHeld;
        #endregion

        #region Variables
        private int FDAccountId { get; set; }
        DataSet dsRenewal = null;
        private int FDRenewalId { get; set; }
        private int FDVoucherId { get; set; }
        private int FDWithdrwalInterestVoucherId { get; set; }
        private string TempId { get; set; }
        int FDRenewalCount = 0;
        FDTypes fdTypes;
        private DataTable dtMasters { get; set; }
        private DataTable dtRenewals { get; set; }
        public DataTable dtFDRenewals { get; set; }
        private int FDProjectId { get; set; }
        private string RenewalMode { get; set; }
        private int MaxRenewalId { get; set; }
        private DateTime RenewalDate { get; set; }
        ResultArgs resultArgs = null;       
        #endregion

        #region Constructor

        public frmRenewals()
        {
            InitializeComponent();

            //31/07/2024, Other than India, let us lock TDS Amount
            colTDSAmount.Visible = !(this.AppSetting.IsCountryOtherThanIndia);
        }
        /// <summary>
        /// This is load the renewals/Post Interest exits in FD Account
        /// </summary>
        /// <param name="fdAccountId">FD Account Id</param>
        /// <param name="fdLedger">FD Ledger name to show in the label</param>
        /// <param name="project">Project name of selected FD to show in the Label</param>
        /// <param name="fdAccount">FD Account Number /param>
        /// <param name="renewal">Dataset which contains "FDrenewal History/Post Interest Hsitory" and "master Data"</param>
        /// <param name="fdTypes">Fd types such as "RN?POI?WD"</param>
        /// <param name="Mode">Mode=Edit/Delete</param>
        public frmRenewals(int fdAccountId, string fdLedger, string project, string fdAccount, DataSet renewal, FDTypes fdTypes, string Mode, string fdtranstype)
            : this()
        {
            FDAccountId = fdAccountId;
            dsRenewal = renewal;
            this.fdTypes = fdTypes;
            this.RenewalMode = Mode;
            this.FDTransType = fdtranstype;
            lblProject.Text = !string.IsNullOrEmpty(project) ? project : "0";
            lblFDLedger.Text = !string.IsNullOrEmpty(fdLedger) ? fdLedger : "0";
            lblFDAccount.Text = !string.IsNullOrEmpty(fdAccount) ? fdAccount : "0";
            lblFDType.Text = fdtranstype;
        }

        #endregion

        #region Properties

        public DateTime FDModifyAccountCreatedDate { get; set; }
        public decimal FDModifyAccountInsRate { get; set; }
        public DateTime FDModifyPostMaturityDate { get; set; }
        public int FDModifyPostInterestType { get; set; }
        public string FDModifyPostRenewalType { get; set; }
        public string FDTransType { get; set; }

        // private int FDAccountId { get; set; }
        #endregion

        #region Events

        private void frmRenewals_Load(object sender, EventArgs e)
        {
            LoadFDRenewals();
            if (RenewalMode == "Delete")
            {
                this.Text = this.fdTypes == FDTypes.RN ? this.GetMessage(MessageCatalog.Master.FDRenewal.FD_RENEWAL_DELETE) : this.fdTypes == FDTypes.RIN ? "FD Re-Investment Delete" : this.fdTypes == FDTypes.WD ? "FD Withdrawal Delete" : this.GetMessage(MessageCatalog.Master.FDRenewal.FDPOST_INTEREST_DELETE);
            }
            else
            {
                this.Text = this.fdTypes == FDTypes.RN ? this.GetMessage(MessageCatalog.Master.FDRenewal.FD_RENEWAL_MODIFY) : this.fdTypes == FDTypes.POI ? this.GetMessage(MessageCatalog.Master.FDRenewal.FDPOST_INTEREST_MODIFY) : fdTypes == FDTypes.RIN ? "FD Re-Investment Modify" : "FD Withdrawal Modify";
            }

            colWithdrawalAmount.Visible = colChargeMode.Visible = colChargeAmount.Visible = false;
            if (this.fdTypes == FDTypes.WD)
            {
                colRenewalDate.Caption = "Withdrawal On";
                colMaturedOn.Visible = colReceiptNo.Visible = colInterestRate.Visible = false;
                colWithdrawalAmount.Visible = colChargeMode.Visible = colChargeAmount.Visible = true;
                colWithdrawalAmount.VisibleIndex = 2;
            }
            else if (this.fdTypes == FDTypes.RIN)
            {
                colReceiptNo.Visible = colTDSAmount.Visible = colInterestRate.Visible = false;
                colInterestAmount.Caption = "Re-Investment Amount";
            }
            else if (this.fdTypes == FDTypes.POI)
            {
                colReceiptNo.Visible = false;
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.Close();
            /*if (GetSelectedRecord())
            {
                this.Close();
                ShowFdRenewalEditForm();
            }*/
        }
        /// <summary>
        /// To show FD Edit edit/Delete form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkSelect_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                int fdRenewalId = gvRenewals.GetFocusedRowCellValue(colFDRenewalId) != null ? this.UtilityMember.NumberSet.ToInteger(gvRenewals.GetFocusedRowCellValue(colFDRenewalId).ToString()) : 0;
                CheckEdit chkEdit = sender as CheckEdit;
                int status = Convert.ToInt32(chkEdit.CheckState);
                //to set selected status as "1" while select the renewal/post interest for Modify/Delete
                dtFDRenewals = MoveTransToSelectedProject(fdRenewalId, status);
                if (GetSelectedRecord())
                {
                    if (RenewalMode == "Edit")
                    {
                        if (gvRenewals.RowCount > 0)
                        {
                            this.DialogResult = DialogResult.OK;
                            ShowFdRenewalEditForm();
                        }
                    }
                    else
                    {
                        //Delete the Selected renewal/Post Interest
                        using (FDAccountSystem fdAccountSystem = new FDAccountSystem())
                        {
                            if (fdAccountSystem.CheckFDWithdrawal().Equals(0))
                            {
                                if (!base.IsVoucherLockedForDate(FDProjectId,  RenewalDate, true))
                                {
                                    if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETE_CONFIRMATION), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                    {
                                        fdAccountSystem.RenewedDate = RenewalDate; //gvRenewals.GetFocusedRowCellValue(colRenewalDate) != null ? this.UtilityMember.DateSet.ToDate(gvRenewals.GetFocusedRowCellValue(colRenewalDate).ToString(), false) : DateTime.MinValue;
                                        TempId += FDRenewalId + ",";
                                        fdAccountSystem.FDRenewalId = FDRenewalId = fdRenewalId;
                                        fdAccountSystem.FDWithdrwalInterestVoucherId = 0;
                                        if (fdTypes == FDTypes.WD)
                                        {
                                            fdAccountSystem.FDWithdrwalInterestVoucherId = FDWithdrwalInterestVoucherId;
                                        }

                                        resultArgs = fdAccountSystem.DeleteFDRenewals();
                                        if (resultArgs.Success)
                                        {
                                            this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETED_CONFIRMATION));
                                            LoadFDRenewals();
                                            if (gvRenewals.RowCount == 1)
                                            {
                                                this.Close();
                                                if (UpdateHeld != null)
                                                {
                                                    UpdateHeld(this, e);
                                                }
                                            }
                                        }
                                        else
                                        {
                                            this.ShowMessageBox(resultArgs.Message);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.FDLedger.FD_CANNOT_DELETE_ASSCOCIATE_RENEWAL));
                            }
                            if (UpdateHeld != null)
                            {
                                if (resultArgs!=null && resultArgs.Success)
                                {
                                    LoadFDRenewals();
                                }
                            }
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

        #endregion

        #region Methods
        /// <summary>
        /// This is method is used to load the no of renewals based FD Account ID
        /// </summary>
        /// <params>FD_ACCOUNT_ID</params>
        private void LoadFDRenewals()
        {
            DataView dvRenewal = null;
            //ds renewal is a dattaset which contains fd Renewal History/Post Interest history for the selected FD Account and Master data of FD Account
            dtMasters = dsRenewal.Tables[0];//master Data
            DataView dvMasters = dtMasters.DefaultView;
            dvMasters.RowFilter = "FD_ACCOUNT_ID=" + FDAccountId + "";
            dtMasters = dvMasters.ToTable();
            FDProjectId = dtMasters.Rows[0]["PROJECT_ID"] != DBNull.Value ? this.UtilityMember.NumberSet.ToInteger(dtMasters.Rows[0]["PROJECT_ID"].ToString()) : 0;
            dtRenewals = dsRenewal.Tables[1];//Renewal history/Post Interest history table
            if (FDRenewalId == 0)
            {
                dvRenewal = dtRenewals.DefaultView;
                dvRenewal.RowFilter = "FD_ACCOUNT_ID=" + FDAccountId + "";
            }
            else
            {
                dvRenewal = dtRenewals.DefaultView;
                if (!string.IsNullOrEmpty(TempId) && TempId != null)
                {
                    TempId = TempId.TrimEnd(',');
                    //dvRenewal.RowFilter = "FD_RENEWAL_ID<>" + FDRenewalId + " AND FD_ACCOUNT_ID=" + FDAccountId + "";
                    dvRenewal.RowFilter = "FD_RENEWAL_ID NOT IN(" + TempId + ") AND FD_ACCOUNT_ID=" + FDAccountId + "";
                    TempId = TempId + ",";
                }
            }

            var MaxValue = dvRenewal.ToTable().Select("FD_RENEWAL_ID=MAX(FD_RENEWAL_ID)");

            //On 28/09/2022, To get Actual Recent Post Interest Voucher
            if (this.fdTypes == FDTypes.POI)
            {
                MaxValue = dvRenewal.ToTable().Select("RENEWAL_DATE=MAX(RENEWAL_DATE)");
            }

            //to show recent renewal in first row in item array
            MaxRenewalId = 0;
            if (MaxValue.GetLength(0) > 0)
            {
                MaxRenewalId = this.UtilityMember.NumberSet.ToInteger(MaxValue[0].ItemArray[0].ToString());
            }

            gcRenewalsView.DataSource = dvRenewal.ToTable();
            gcRenewalsView.RefreshDataSource();
            gvRenewals.BestFitColumns();
        }
        /// <summary>
        /// to fetch renewal/post interest details of selected renewal
        /// </summary>
        /// <returns></returns>
        private int SelectedRenewal()
        {
            DataTable dtRenewal = (DataTable)gcRenewalsView.DataSource;
            foreach (DataRow dr in dtRenewal.Rows)
            {
                if (this.UtilityMember.NumberSet.ToInteger(dr["SELECT"].ToString()) == (int)Status.Active)
                {
                    FDRenewalId = this.UtilityMember.NumberSet.ToInteger(dr["FD_RENEWAL_ID"].ToString());
                    FDAccountId = this.UtilityMember.NumberSet.ToInteger(dr["FD_ACCOUNT_ID"].ToString());
                    FDRenewalCount++;
                    break;
                }
            }
            return FDRenewalCount;
        }
        /// <summary>
        /// 
        /// </summary>
        private void ShowFdRenewalEditForm()
        {
            frmFDAccount frmFDAccount = new frmFDAccount(FDRenewalId, FDAccountId, fdTypes, dtMasters, dtRenewals);
            frmFDAccount.PostInterestCreatedDate = FDModifyAccountCreatedDate;
            frmFDAccount.PostInterestRate = FDModifyAccountInsRate;
            frmFDAccount.PostInterestMaturityDate = FDModifyPostMaturityDate;
            frmFDAccount.PostRenewalType = FDModifyPostRenewalType;

            frmFDAccount.ProjectId = FDProjectId;
            frmFDAccount.ShowDialog();
        }

        private DataTable MoveTransToSelectedProject(int projectId, int status)
        {
            DataTable dtFDRenewals = (DataTable)gcRenewalsView.DataSource;

            for (int i = 0; i < dtFDRenewals.Rows.Count; i++)
            {
                int ProjectId = dtFDRenewals.Rows[i]["FD_RENEWAL_ID"] != DBNull.Value ? this.UtilityMember.NumberSet.ToInteger(dtFDRenewals.Rows[i]["FD_RENEWAL_ID"].ToString()) : 0;
                if (ProjectId == projectId)
                {
                    dtFDRenewals.Rows[i]["SELECT"] = status;
                }
                else
                {
                    dtFDRenewals.Rows[i]["SELECT"] = (int)YesNo.No;
                }
            }
            return dtFDRenewals;
        }

        private bool GetSelectedRecord()
        {
            bool hasSelectedRenewal = true;
            try
            {
                if (dtFDRenewals != null && dtFDRenewals.Rows.Count != 0)
                {
                    DataView dvRenewal = dtFDRenewals.DefaultView;
                    dvRenewal.RowFilter = "SELECT =1";
                    if (dvRenewal.Count != 0)
                    {
                        foreach (DataRow dr in dvRenewal.ToTable().Rows)
                        {
                            FDRenewalId = dr["FD_RENEWAL_ID"] != DBNull.Value ? this.UtilityMember.NumberSet.ToInteger(dr["FD_RENEWAL_ID"].ToString()) : 0;
                            FDAccountId = dr["FD_ACCOUNT_ID"] != DBNull.Value ? this.UtilityMember.NumberSet.ToInteger(dr["FD_ACCOUNT_ID"].ToString()) : 0;
                            FDVoucherId = dr["FD_VOUCHER_ID"] != DBNull.Value ? this.UtilityMember.NumberSet.ToInteger(dr["FD_VOUCHER_ID"].ToString()) : 0;
                            FDWithdrwalInterestVoucherId =  0;
                            if (fdTypes == FDTypes.WD)
                            {
                                FDWithdrwalInterestVoucherId = dr["FD_INTEREST_VOUCHER_ID"] != DBNull.Value ? this.UtilityMember.NumberSet.ToInteger(dr["FD_INTEREST_VOUCHER_ID"].ToString()) : 0;
                            }
                            FDModifyAccountInsRate = this.UtilityMember.NumberSet.ToDecimal(dr["INTEREST_RATE"].ToString());
                            RenewalDate = this.UtilityMember.DateSet.ToDate(dr["RENEWAL_DATE"].ToString(), false);
                            dvRenewal.RowFilter = "";
                            
                            //On 08/08/2019, In Edit mode, If selected renewal records is not blong to current finance year, 
                            //Prompt proper message and lock to edit
                            if (RenewalMode=="Edit" && dr["RENEWAL_DATE"] != DBNull.Value)
                            {
                                if (!(RenewalDate >= this.UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false) 
                                    && RenewalDate <= this.UtilityMember.DateSet.ToDate(this.AppSetting.YearTo, false)))
                                {
                                    this.ShowMessageBox("Selected Renewal detail doesn't fall on the Current Financial Year, Change Financial Year and modify Renewal detail");
                                    hasSelectedRenewal = false;
                                    this.Close();
                                }
                                else if (base.IsVoucherLockedForDate(FDProjectId,  RenewalDate, true))
                                {
                                    hasSelectedRenewal = false;
                                }
                            }
                        }
                    }
                    else
                    {
                        dvRenewal.RowFilter = "";
                        if (RenewalMode != "Delete")
                        {
                            this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.FDRenewal.RENEWAL_NO_RECORD));
                        }
                        hasSelectedRenewal = false;
                    }
                }
                else
                {
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.FDRenewal.RENEWAL_NO_RECORD));
                    hasSelectedRenewal = false;
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + System.Environment.NewLine + ex.Source, true);
            }
            finally { }
            return hasSelectedRenewal;
        }
        #endregion

        private void gvRenewals_ShowingEditor(object sender, CancelEventArgs e)
        {
            /*DataTable dt = (DataTable)gcRenewalsView.DataSource;
            if (dt != null && dt.Rows.Count > 0)
            {
                if (fdTypes == FDTypes.POI && RenewalMode == "Delete")
                {
                    if (gvRenewals.FocusedRowHandle == 0)
                    {
                        e.Cancel = false;
                    }
                    else
                    {
                        e.Cancel = true;
                    }
                }
            }*/
        }

           

        private void gvRenewals_CustomRowCellEdit(object sender, CustomRowCellEditEventArgs e)
        {
            if (e.Column == colSelect)
            {
                RepositoryItem item = new RepositoryItem();
                Int32 renewlid = UtilityMember.NumberSet.ToInteger(gvRenewals.GetRowCellValue(e.RowHandle, "FD_RENEWAL_ID").ToString());
                if (renewlid != MaxRenewalId)
                    e.RepositoryItem = item;
                else
                    e.RepositoryItem = this.chkSelect;
            }
        }

        private void gvRenewals_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            //on 04/10/2022, Change Renewl Type text
            if (e.Column.FieldName == colRenewalType.FieldName)
            {
                if (e.Value != null)
                {
                    string fdtype = e.Value.ToString().ToUpper();
                    string displaytext = fdtype;
                    GridView gv = sender as GridView;
                    DataRow dr = gv.GetDataRow(gv.GetRowHandle(e.ListSourceRowIndex)) as DataRow;
                    string transmode = TransactionMode.DR.ToString().ToUpper();
                    if (dr != null)
                    {
                        transmode = dr[colFDTransMode.FieldName].ToString().Trim().ToUpper();
                    }

                    if (e.Value.ToString().ToUpper() == Bosco.Utility.FDRenewalTypes.IRI.ToString().ToUpper())
                    {
                        displaytext = "Interest Received";
                    }
                    else if (e.Value.ToString().ToUpper() == Bosco.Utility.FDRenewalTypes.ACI.ToString().ToUpper())
                    {
                        displaytext = (transmode == TransactionMode.CR.ToString().ToUpper() ? "Fixed Deposit Adjustment" : "Interest Accumulated");
                    }
                    else if (e.Value.ToString().ToUpper() == Bosco.Utility.FDRenewalTypes.PWD.ToString().ToUpper())
                    {
                        displaytext = "Partially Withdrawal";
                    }
                    else if (e.Value.ToString().ToUpper() == Bosco.Utility.FDRenewalTypes.RIN.ToString().ToUpper())
                    {
                        displaytext = "Re-Investment";
                    }
                    e.DisplayText = displaytext;

                    /*e.DisplayText = (e.Value.ToString().ToUpper() == Bosco.Utility.FDRenewalTypes.IRI.ToString().ToUpper() ?
                        "Interest Received" : (e.Value.ToString().ToUpper() == Bosco.Utility.FDRenewalTypes.ACI.ToString().ToUpper() ? "Interest Accumulated" :
                         (e.Value.ToString().ToUpper() == Bosco.Utility.FDRenewalTypes.PWD.ToString().ToUpper() ? "Partially Withdrawal" : 
                         (e.Value.ToString().ToUpper() == Bosco.Utility.FDRenewalTypes.RIN.ToString().ToUpper() ? "Re-Investment" : string.Empty))));*/
                }
            }
            else if (e.Column.FieldName == colInterestAmount.FieldName)
            {
                if (sender != null && e.Value != null)
                {
                    GridView gv = sender as GridView;
                    DataRow dr = gv.GetDataRow(gv.GetRowHandle(e.ListSourceRowIndex)) as DataRow;

                    if (dr != null)
                    {
                        string renewaltype = dr[colRenewalType.FieldName].ToString().Trim().ToUpper();
                        string transmode = dr[colFDTransMode.FieldName].ToString().Trim().ToUpper();
                        if (renewaltype.ToUpper() == FDRenewalTypes.ACI.ToString().ToUpper() && transmode == TransactionMode.CR.ToString().ToUpper())
                        {
                            double amt = UtilityMember.NumberSet.ToDouble(e.Value.ToString()) * (transmode == TransactionMode.CR.ToString().ToUpper() ? -1 : 1);
                            e.DisplayText = UtilityMember.NumberSet.ToNumber(amt);
                        }
                    }
                }
            }

        }
    }
}